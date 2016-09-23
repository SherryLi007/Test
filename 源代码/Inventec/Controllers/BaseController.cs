using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventec.Models;
using System.Reflection;
using Inventec.Ultimus.Entity;
using Inventec.Ultimus.Interface;
using System.Transactions;
using System.Data.Entity;
using Inventec.DAL;
using System.Collections;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Web.Script.Serialization;
using Inventec.Common;
using System.Text;

namespace Inventec.Controllers
{
    [AuthorizeFilter]
    public class BaseController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("Submitter");
        public OrgUser CurrentUser;
        public int BaseCurrencyID = 1;
        public ITask _task = new Ultimus.Implementation.UltimusTask();
        private InventecbizContext db = new InventecbizContext();
        private Result result = new Result();
        public List<VarEntity> varlist;
       
        protected override void Initialize(RequestContext rc)
        {
            base.Initialize(rc);
            if (Session["User"] != null)
            {
                CurrentUser = (OrgUser)Session["User"];
                CurrentUser.deparmentid = Convert.ToInt32(Session["Deptmentid"]);
                if (Session["Currency"] != null)
                    BaseCurrencyID = Convert.ToInt32(Session["Currency"]);
            }
            else
            {
                CurrentUser = new OrgUser();
            }
        }

        //将实体类转化成参数
        public List<VarEntity> GetVarList<T>(T entity)
        {
            PropertyInfo[] fields = entity.GetType().GetProperties();//获取指定对象的所有公共属性

            List<VarEntity> list = new List<VarEntity>();
            foreach (PropertyInfo ety in fields)
            {
                VarEntity p = new VarEntity();
                p.Name = Convert.ToString(ety.Name);
                p.Value = Convert.ToString(ety.GetValue(entity, null));
                list.Add(p);
            }
            return list;
        }

        //生成序列号
        public string GetSerialno(string strComCode, string strFormType)
        {
            string strNo = "";
            try
            {
                ComSerialno _model = BaseDAL<ComSerialno>.Find(db, p => p.serialtype == strFormType && p.serialyear == DateTime.Now.Year && p.serialmonth == DateTime.Now.Month);
                if (_model == null)
                {
                    _model = new ComSerialno();
                    _model.serialtype = strFormType;
                    _model.serialyear = DateTime.Now.Year;
                    _model.serialmonth = DateTime.Now.Month;
                    _model.serialday = 1;
                    _model.serialno = 1;
                    _model.updatedate = DateTime.Now;
                    BaseDAL<ComSerialno>.Add(db, _model);
                    strNo = strComCode + '-' + _model.serialtype.ToUpper() + '-' + DateTime.Now.ToString("yyyyMMdd") + '-' + Convert.ToInt32(_model.serialno).ToString("00000");
                }
                else
                {
                    _model.serialno = _model.serialno + 1;
                    strNo = strComCode + '-' + _model.serialtype.ToUpper() + '-' + DateTime.Now.ToString("yyyyMMdd") + '-' + Convert.ToInt32(_model.serialno).ToString("00000");
                    _model.updatedate = DateTime.Now;
                    BaseDAL<ComSerialno>.Update(db, _model);
                }

            }
            catch (Exception ex)
            {
                log.Error(CurrentUser.username + "获取序列号异常！", ex);
            }
            return strNo;
        }

        ////保存草稿
        public Result SaveDraft(dynamic apply)
        {
            int id = 0;
            Models.Result result = new Models.Result();
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    var error = ModelState.Values;
                    if (ModelState.IsValid)
                    {
                        string typename = apply.GetType().Name;
                        SaveData(apply, typename, out id);
                        if (id > 0)
                        {
                            result.status = 1;
                            result.info = LangString("Savesuccessful");
                            result.url = "close";

                            WfDraft draft = new WfDraft();
                            draft.processname = typename.Replace("Proc", "PROC_");
                            draft.tablename = typename.Replace("Proc", "Form");
                            draft.formid = id;
                            draft.createby = CurrentUser.id;
                            draft.createdate = DateTime.Now;
                            WfDraft olddraft = BaseDAL<WfDraft>.Find(db, p => p.formid == id && p.processname == draft.processname);
                            if (olddraft == null)
                            {
                                BaseDAL<WfDraft>.Add(db, draft);
                            }

                            //保存附件
                            if (Request.Params.GetValues("attachmentid") != null)
                            {
                                for (int i = 0; i < Request.Params.GetValues("attachmentid").Length; i++)
                                {
                                    WfAttachment attchment = BaseDAL<WfAttachment>.Find(db, Convert.ToInt32(Request.Params.GetValues("attachmentid")[i]));
                                    if (attchment != null)
                                    {
                                        attchment.formid = id.ToString();
                                        attchment.processname = typename.Replace("Proc", "PROC_");

                                        BaseDAL<WfAttachment>.Update(db, attchment);
                                    }
                                }
                            }
                        }
                        transaction.Complete();

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(CurrentUser.username + "保存草稿异常！", ex);
                result.info = LangString("SystemException");
            }
            return result;
        }

        ////保存数据
        private dynamic SaveData(dynamic apply, string typename, out int id)
        {
            try
            {
                decimal AuthAmount = 0;
                string applican = Request["applicant"].ToString();
                apply.dept = Session["Deptment"].ToString();
                apply.deptid = Convert.ToInt32(Session["Deptmentid"]);
                OrgUser applicant = BaseDAL<OrgUser>.Find(db, p => p.username == applican, true);
                if (applicant != null)
                {
                    apply.applicant = applicant.username;
                    apply.applicantaccount = applicant.loginname;
                    apply.applicantcode = applicant.usercode;
                }
                apply.loginuser = CurrentUser.username;
                apply.loginuseraccount = CurrentUser.loginname;
                apply.loginusercode = CurrentUser.usercode;
                apply.formid = Guid.NewGuid();
                apply.requestdate = DateTime.Now;
                int costcenterid = Convert.ToInt32(CurrentUser.costcenter);

            }
            catch (Exception ex)
            {
                log.Error(CurrentUser.username + "保存数据错误！", ex);
            }
            id = 0;
            return null;
        }

        ////提交时保存
        //public Result SaveSubmit(dynamic apply)
        //{
        //    int id = 0;
        //    Models.Result result = new Models.Result();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //using (TransactionScope transaction = new TransactionScope())
        //            {
        //                string formcontent = new JavaScriptSerializer().Serialize(apply);
        //                string typename = apply.GetType().Name;
        //                string processname = typename.Replace("Proc", "PROC_");


        //                dynamic resultapply = SaveData(apply, typename, out id);
        //                //保存附件
        //                if (id > 0)
        //                {
        //                    int incident = Submit(apply, processname, formcontent);

        //                    if (incident > 0)
        //                    {
        //                        result.status = 1;
        //                        result.info = LangString("Submittedsuccessfully");
        //                        result.url = "close";
        //                    }

        //                    if (Request.Params.GetValues("attachmentid") != null)
        //                    {
        //                        for (int i = 0; i < Request.Params.GetValues("attachmentid").Length; i++)
        //                        {
        //                            WfAttachment attchment = BaseDAL<WfAttachment>.Find(db, Convert.ToInt32(Request.Params.GetValues("attachmentid")[i]));
        //                            if (attchment != null)
        //                            {
        //                                attchment.taskid = incident.ToString();
        //                                attchment.formid = id.ToString();
        //                                attchment.processname = typename.Replace("Proc", "PROC_");
        //                                BaseDAL<WfAttachment>.Update(db, attchment);
        //                            }
        //                        }
        //                    }

        //                    //transaction.Complete();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            result.info = ModelState.IsValid.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "提交申请异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return result;
        //}

        ////提交表单
        //public int Submit<T>(T enity, string processName, string formContent)
        //{
        //    string mailSubmit = GetMailTemplate("SUBIMTFORM");
        //    string mailAgentSubmit = GetMailTemplate("AGENTSUMBIT");
        //    try
        //    {
        //        string serialNo = ((dynamic)enity).formno;
        //        int? taskid = ((dynamic)enity).incident;
        //        TaskEntity task;
        //        task = _task.GetInitTaskEntityByProcessName(processName);
        //        task.ASSIGNEDTOUSER = ((dynamic)enity).applicantaccount.Replace("\\", "/");
        //        task.SUMMARY = "";
        //        task.VarList = GetVarList<T>(enity);
        //        VarEntity var = new VarEntity();
        //        var.Name = "curuser";
        //        OrgUser superiorUser = new RuleController().GetSuperior(((dynamic)enity).applicantaccount, ((dynamic)enity).deptid);
        //        if (superiorUser != null)
        //        {
        //            var.Value = SetUserPrefix(superiorUser.loginname);
        //        }
        //        task.VarList.Add(var);

        //        int outIncident = 0;
        //        outIncident = _task.SubmitTask(task);
        //        string error = task.ERRORMESSAGE;
        //        int incident = outIncident;
        //        if (incident > 0)
        //        {
        //            using (TransactionScope transaction = new TransactionScope())
        //            {
        //                if (!taskid.HasValue)
        //                {
        //                    WfProcess wfProcesstemp = new WfProcess();
        //                    wfProcesstemp = BaseDAL<WfProcess>.Find(db, p => p.module == processName);

        //                    if (string.IsNullOrEmpty(serialNo))
        //                    {
        //                        serialNo = GetSerialno(((dynamic)enity).companycode, wfProcesstemp.ext01);
        //                        db.Database.ExecuteSqlCommand("Update " + processName + " set Status=1,incident=" + incident + ",FORMNO='" + serialNo + "' where id=" + ((dynamic)enity).id);
        //                    }

        //                    Models.WfForm form = new Models.WfForm();
        //                    form.processname = processName;
        //                    form.status = 1;
        //                    form.incident = incident;
        //                    form.starttime = DateTime.Now;
        //                    form.formno = serialNo;
        //                    form.formamount = 0;
        //                    form.costcenter = ((dynamic)enity).costcenter;
        //                    form.companycode = ((dynamic)enity).companycode;
        //                    form.dept = ((dynamic)enity).dept;
        //                    form.initiator = ((dynamic)enity).applicantaccount;
        //                    if (((dynamic)enity).applicantaccount != CurrentUser.loginname)
        //                    {
        //                        form.agent = CurrentUser.loginname;
        //                    }
        //                    try
        //                    {
        //                        if (form.processname.ToLower() == "proc_pettycash")
        //                        {
        //                            form.formamount = ((dynamic)enity).cnyamount;
        //                        }
        //                        else if (form.processname.ToLower() == "proc_goodsreceipt")
        //                        {
        //                            form.formamount = ((dynamic)enity).totalamount;
        //                        }
        //                        else if (form.processname.ToLower() == "proc_entertainmentrequest")
        //                        {
        //                            form.formamount = ((dynamic)enity).applyingamount;
        //                        }
        //                        else
        //                        {
        //                            form.formamount = ((dynamic)enity).totalpayment;
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }
        //                    if (!String.IsNullOrEmpty(wfProcesstemp.ext03) && wfProcesstemp.ext03.ToUpper() == "Y")
        //                    {
        //                        form.isurgent = ((dynamic)enity).isurgent;
        //                    }
        //                    else
        //                    {
        //                        form.isurgent = false;
        //                    }
        //                    form.paymentstatus = 0;
        //                    if (!String.IsNullOrEmpty(wfProcesstemp.ext02) && wfProcesstemp.ext02.ToUpper() == "Y")
        //                    {
        //                        form.paymentstatus = 1;
        //                    }
        //                    form.summary = formContent;
        //                    BaseDAL<WfForm>.Add(db, form);

        //                    if (((dynamic)enity).applicant != ((dynamic)enity).loginuser)
        //                    {
        //                        ComMessagequeue message = new ComMessagequeue();
        //                        message.fromuser = ((dynamic)enity).loginuseraccount;
        //                        message.touser = ((dynamic)enity).applicantaccount;
        //                        message.subject = "Application No.  " + serialNo + " has been applied by " + ((dynamic)enity).loginuser + " on behalf of you";
        //                        message.body = String.Format(mailAgentSubmit, ((dynamic)enity).applicant, ((dynamic)enity).loginuser.Trim(), serialNo);
        //                        message.isread = 0;
        //                        message.sendtype = "0";
        //                        message.createdate = DateTime.Now;
        //                        BaseDAL<ComMessagequeue>.Add(db, message);
        //                    }
        //                }
        //                else
        //                {
        //                    db.Database.ExecuteSqlCommand("Update WF_APPROVALHISTORY set incident=" + incident + ",taskid='" + task.TASKID + "' where incident=" + taskid.Value + " and processname='" + processName + "'");
        //                    WfForm form = BaseDAL<WfForm>.Find(db, p => p.incident == taskid.Value && p.processname == processName);
        //                    //db.Database.ExecuteSqlCommand("Update WF_FORM set incident=" + incident + ",status=1,EndTime=null where incident=" + taskid.Value + " and processname='" + processName + "'");
        //                    form.incident = incident;
        //                    form.costcenter = ((dynamic)enity).costcenter;
        //                    form.companycode = ((dynamic)enity).companycode;
        //                    form.dept = ((dynamic)enity).dept;
        //                    form.initiator = ((dynamic)enity).applicantaccount;
        //                    form.status = 1;
        //                    form.endtime = null;
        //                    if (((dynamic)enity).applicantaccount != CurrentUser.loginname)
        //                    {
        //                        form.agent = CurrentUser.loginname;
        //                    }
        //                    try
        //                    {
        //                        if (form.processname.ToLower() == "proc_pettycash")
        //                        {
        //                            form.formamount = ((dynamic)enity).cnyamount;
        //                        }
        //                        else if (form.processname.ToLower() == "proc_goodsreceipt")
        //                        {
        //                            form.formamount = ((dynamic)enity).totalamount;
        //                        }
        //                        else if (form.processname.ToLower() == "proc_entertainmentrequest")
        //                        {
        //                            form.formamount = ((dynamic)enity).applyingamount;
        //                        }
        //                        else
        //                        {
        //                            form.formamount = ((dynamic)enity).totalpayment;
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }
        //                    form.summary = formContent;
        //                    BaseDAL<WfForm>.Update(db, form);

        //                    db.Database.ExecuteSqlCommand("Update " + processName + " set Status=1,incident=" + incident + ",FORMNO='" + form.formno + "' where id=" + ((dynamic)enity).id);
        //                    _task.DeleteIncident(processName, taskid.Value);
        //                }

        //                db.Database.ExecuteSqlCommand("Update TB_SubProcessApprover set incident=" + incident + " where formid=" + ((dynamic)enity).id + " and processname='" + processName + "'");
        //                Models.WfApprovalhistory history = new WfApprovalhistory();
        //                history.incident = incident;
        //                history.processname = processName;
        //                history.taskid = task.TASKID;
        //                if (!taskid.HasValue)
        //                {
        //                    history.stepname = "Submit";
        //                    history.action = "Submit / 提交";
        //                }
        //                else
        //                {
        //                    history.stepname = "ReSubmit";
        //                    history.action = "ReSubmit / 重新提交";
        //                }
        //                history.status = 1;
        //                history.approveraccount = CurrentUser.loginname;
        //                history.approvername = CurrentUser.username;
        //                history.createdate = DateTime.Now;
        //                history.enddate = DateTime.Now;
        //                BaseDAL<WfApprovalhistory>.Add(db, history);

        //                int id = ((dynamic)enity).id;
        //                BaseDAL<WfDraft>.Delete(db, p => p.processname == processName && p.formid == id);

        //                //发送邮件
        //                string currentApprover = _task.GetCurrentApprover(processName, incident);
        //                int m = 0;
        //                while (String.IsNullOrEmpty(currentApprover) && m < 10)
        //                {
        //                    Thread.Sleep(1000);
        //                    currentApprover = _task.GetCurrentApprover(processName, incident);
        //                    m = m + 1;
        //                }
        //                if (!String.IsNullOrEmpty(currentApprover))
        //                {
        //                    string[] currentApproverList = currentApprover.Split(':')[1].Split(',');
        //                    for (int i = 0; i < currentApproverList.Length; i++)
        //                    {
        //                        ComMessagequeue message1 = new ComMessagequeue();
        //                        message1.fromuser = ((dynamic)enity).loginuseraccount;
        //                        message1.touser = "Porsche\\" + currentApproverList[i];
        //                        message1.subject = "Application No. " + serialNo + " to be approved.";
        //                        message1.body = String.Format(mailSubmit, currentApproverList[i].Replace(".", " "), ((dynamic)enity).loginuser.Trim(), serialNo);
        //                        message1.isread = 0;
        //                        message1.sendtype = "1";
        //                        message1.createdate = DateTime.Now;
        //                        BaseDAL<ComMessagequeue>.Add(db, message1);
        //                    }
        //                }

        //                transaction.Complete();
        //            }
        //        }
        //        return incident;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int SubmitMasterDate<T>(T entity, string entityOld, int type = 0, string isOrganization = "")
        //{
        //    ProcBusinessdata data = new ProcBusinessdata();
        //    //删除操作 获得的类型名 不是正确的， typeName要去掉 “_”以及后面的字符 
        //    string typeName = type == 2 ? entity.GetType().BaseType.Name : entity.GetType().Name;
        //    data.typename = isOrganization != "" ? isOrganization : typeName + (typeName == "VarAmountpositioncode" ? "" : "s");

        //    data.datacontent = new JavaScriptSerializer().Serialize(entity);
        //    if (!String.IsNullOrEmpty(entityOld))
        //        data.historydatacontent = entityOld;
        //    OrgUser applicant = CurrentUser;
        //    if (applicant != null)
        //    {
        //        data.applicant = applicant.username;
        //        data.applicantaccount = applicant.loginname;
        //        data.applicantcode = applicant.usercode;
        //    }
        //    data.formid = Guid.NewGuid();
        //    data.requestdate = DateTime.Now;
        //    data.processstatus = type == 0 ? "Create" : (type == 2 ? "Delete" : "Modify");
        //    data.status = 0;
        //    string processName = "Proc_BusinessData";
        //    string serialNo = data.documentno;
        //    TaskEntity task;
        //    task = _task.GetInitTaskEntityByProcessName(processName);
        //    task.ASSIGNEDTOUSER = data.applicantaccount.Replace("\\", "/");
        //    task.SUMMARY = "";

        //    VarEntity var = new VarEntity();
        //    var.Name = "superior";
        //    OrgUser superiorUser = new RuleController().GetSuperior(applicant.loginname, applicant.deparmentid.Value);
        //    if (superiorUser != null)
        //    {
        //        var.Value = SetUserPrefix(superiorUser.loginname);
        //    }
        //    task.VarList.Add(var);

        //    int outIncident = 0;
        //    outIncident = _task.SubmitTask(task);
        //    string error = task.ERRORMESSAGE;
        //    int incident = outIncident;
        //    if (incident > 0)
        //    {
        //        using (TransactionScope transaction = new TransactionScope())
        //        {
        //            data.incident = incident;
        //            data.status = 1;
        //            data.formno = GetSerialno(Session["Company"].ToString().Split('_')[0], "MD");
        //            serialNo = data.formno;
        //            BaseDAL<ProcBusinessdata>.Add(db, data);
        //            Models.WfApprovalhistory history = new WfApprovalhistory();
        //            history.incident = incident;
        //            history.processname = processName;
        //            history.taskid = task.TASKID;
        //            history.stepname = "Submit";
        //            history.action = "Submit / 提交";
        //            history.status = 1;
        //            history.approveraccount = CurrentUser.loginname;
        //            history.approvername = CurrentUser.username;
        //            history.createdate = DateTime.Now;
        //            history.enddate = DateTime.Now;
        //            BaseDAL<WfApprovalhistory>.Add(db, history);

        //            string currentApprover = _task.GetCurrentApprover(processName, incident);
        //            int m = 0;
        //            while (String.IsNullOrEmpty(currentApprover) && m < 10)
        //            {
        //                Thread.Sleep(1000);
        //                currentApprover = _task.GetCurrentApprover(processName, incident);
        //                m = m + 1;
        //            }
        //            if (!String.IsNullOrEmpty(currentApprover))
        //            {
        //                string[] currentApproverList = currentApprover.Split(':')[1].Split(',');
        //                for (int i = 0; i < currentApproverList.Length; i++)
        //                {
        //                    ComMessagequeue message1 = new ComMessagequeue();
        //                    message1.fromuser = data.applicantaccount;
        //                    message1.touser = "Porsche\\" + currentApproverList[i];
        //                    message1.subject = "Business data application No. " + serialNo + " to be approved.";
        //                    message1.body = String.Format(GetMailTemplate("SUBIMTFORM"), currentApproverList[i].Replace(".", " "), data.applicant.Trim(), serialNo);

        //                    message1.isread = 0;
        //                    message1.sendtype = "1";
        //                    message1.createdate = DateTime.Now;
        //                    BaseDAL<ComMessagequeue>.Add(db, message1);
        //                }
        //            }

        //            transaction.Complete();

        //            return 1;
        //        }
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// 新增和删除
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="type">0:create 1:modify 2:delete</param>
        ///// <returns></returns>
        //public int SubmitMasterDate<T>(T entity, int type = 0)
        //{
        //    return SubmitMasterDate(entity, null, type);
        //}

        ////审批操作
        //public virtual ActionResult ApporveAction()
        //{
        //    try
        //    {
        //        string mailApprove = GetMailTemplate("APPROVEFORM");
        //        string mailApproved = GetMailTemplate("APPROVEDFORM");

        //        if (varlist == null)
        //        {
        //            varlist = new List<VarEntity>();
        //        }

        //        int incident = Convert.ToInt32(Request["incident"]);
        //        string processName = Request["processname"];

        //        WfForm form = BaseDAL<WfForm>.Find(db, p => p.incident == incident && p.processname == processName);

        //        TbSubprocessapprover subprocess = BaseDAL<TbSubprocessapprover>.Find(db, p => p.incident == incident && p.processname == processName && p.approvalstatus == 0 && p.approver == CurrentUser.loginname);

        //        TaskEntity task = new TaskEntity();
        //        string taskUser = CurrentUser.loginname;
        //        if (Request["TaskUser"] != null && !String.IsNullOrEmpty(Request["TaskUser"].ToString()))
        //        {
        //            taskUser = Request["TaskUser"].ToString();
        //            task = _task.GetTaskEntityByName(processName, incident, (Request["TaskUser"].ToString().Replace("\\", "/")));

        //            subprocess = BaseDAL<TbSubprocessapprover>.Find(db, p => p.incident == incident && p.processname == processName && p.approvalstatus == 0 && p.approver == taskUser);
        //        }
        //        else
        //        {
        //            task = _task.GetTaskEntityByName(processName, incident, CurrentUser.loginname.Replace("\\", "/"));
        //        }

        //        VarEntity var = varlist.Find(p => p.Name == "curuser");
        //        OrgUser superiorUser = new RuleController().GetSuperior(taskUser, CurrentUser.deparmentid.Value);
        //        if (var == null)
        //        {
        //            var = new VarEntity();
        //            var.Name = "curuser";
        //            varlist.Add(var);
        //        }
        //        if (superiorUser != null)
        //        {
        //            var.Value = SetUserPrefix(superiorUser.loginname);
        //        }

        //        var = varlist.Find(p => p.Name == "ismanager");
        //        OrgJob deptjob = BaseDAL<OrgJob>.Find(db, p => p.org_user.loginname == taskUser && p.ismanager == "1");
        //        if (var == null)
        //        {
        //            var = new VarEntity();
        //        }
        //        var.Name = "ismanager";
        //        var.Value = "0";

        //        if (deptjob != null)
        //        {
        //            var.Value = "1";
        //        }
        //        varlist.Add(var);

        //        if (subprocess != null)
        //        {
        //            var = varlist.Find(p => p.Name == "isneednextapproval");
        //            if (var == null)
        //            {
        //                var = new VarEntity();
        //                var.Name = "isneednextapproval";
        //                varlist.Add(var);
        //            }
        //            var.Value = subprocess.isneednextapproval;


        //            var = varlist.Find(p => p.Name == "supervisorid");
        //            if (var == null)
        //            {
        //                var = new VarEntity();
        //                varlist.Add(var);
        //                var.Name = "supervisorid";
        //            }
        //            if (subprocess.nextuser != null)
        //            {
        //                var.Value = SetUserPrefix(subprocess.nextuser);
        //            }

        //            SetNextSuperiorApprover(processName, incident, subprocess.approver);
        //        }
        //        else
        //        {
        //            var = varlist.Find(p => p.Name == "isneednextapproval");
        //            if (var == null)
        //            {
        //                var = new VarEntity();
        //                varlist.Add(var);
        //                var.Name = "isneednextapproval";
        //            }
        //            var.Value = "N";


        //            var = varlist.Find(p => p.Name == "supervisorid");
        //            if (var == null)
        //            {
        //                var = new VarEntity();
        //                varlist.Add(var);
        //                var.Name = "supervisorid";
        //            }
        //            var.Value = "";

        //        }

        //        if (!String.IsNullOrEmpty(task.TASKID))
        //        {
        //            task.REASON = Request["intorderno"];
        //            task.VarList = varlist;

        //            int outcident = _task.SubmitTask(task);
        //            if (outcident > 0)
        //            {
        //                //using (TransactionScope transaction = new TransactionScope())
        //                {
        //                    if (subprocess != null)
        //                    {
        //                        subprocess.approvalstatus = 1;
        //                        BaseDAL<TbSubprocessapprover>.Update(db, subprocess);
        //                    }

        //                    Models.WfApprovalhistory history = new WfApprovalhistory();
        //                    history.incident = incident;
        //                    history.processname = processName;
        //                    history.stepname = task.STEPLABEL;
        //                    history.taskid = task.TASKID;
        //                    history.taskuser = Request["ByAgentUser"];
        //                    history.assigendtouser = Request["TaskUser"];
        //                    history.createdate = task.STARTTIME;
        //                    history.enddate = DateTime.Now;
        //                    history.comments = Request["intorderno"];
        //                    history.status = 2;
        //                    history.action = "Approved / 通过";
        //                    history.approveraccount = Request["ApproveUserAccount"];
        //                    history.approvername = Request["ApproveUserName"];

        //                    BaseDAL<WfApprovalhistory>.Add(db, history);

        //                    bool nextApprove = true;
        //                    //int approvnum = 0;
        //                    //多次循环，直到当前审批人没有审批过或流程结束,最多流程六次
        //                    while (nextApprove)
        //                    {
        //                        //approvnum = approvnum + 1;

        //                        int status = _task.GetIncidentStatus(processName, incident);
        //                        int m = 0;
        //                        while (status == 0 && m < 10)
        //                        {
        //                            Thread.Sleep(1000);
        //                            status = _task.GetIncidentStatus(processName, incident);
        //                            m = m + 1;
        //                        }
        //                        if (status == 2)
        //                        {
        //                            nextApprove = false;

        //                            db.Database.ExecuteSqlCommand("Update " + processName + " set Status=2 where incident=" + incident);

        //                            db.Database.ExecuteSqlCommand("Update WF_FORM set Status=2,EndTime=GetDate() where incident=" + incident + "  and processname='" + processName + "'");

        //                            ComMessagequeue message = new ComMessagequeue();
        //                            message.fromuser = CurrentUser.loginname;
        //                            message.touser = form.initiator;
        //                            message.subject = "Application No. " + form.formno + " has been approved.";
        //                            message.body = String.Format(mailApproved, form.initiator.Replace("Porsche\\", "").Replace(".", " "), form.formno);
        //                            message.isread = 0;
        //                            message.sendtype = "1";
        //                            message.createdate = DateTime.Now;
        //                            BaseDAL<ComMessagequeue>.Add(db, message);
        //                        }
        //                        else
        //                        {
        //                            string currentApprover = _task.GetCurrentApprover(processName, incident);
        //                            if (!String.IsNullOrEmpty(currentApprover))
        //                            {
        //                                nextApprove = false;
        //                                string[] currentApproverList = currentApprover.Split(':')[1].Split(',');
        //                                //开票申请，在Finance check阶段必须审批，不能跳过
        //                                if (!(currentApprover.ToLower().IndexOf("check") > 0 && processName.ToLower() == "proc_invoiceissuance"))
        //                                {
        //                                    for (int i = 0; i < currentApproverList.Length; i++)
        //                                    {
        //                                        decimal checkamount = 100000000000;
        //                                        string approve = "Porsche\\" + currentApproverList[i];
        //                                        //查询相关人员的审批金额权限
        //                                        string approver = currentApproverList[i].ToString();
        //                                        string organzation = form.companycode;
        //                                        OrgUser user = BaseDAL<OrgUser>.Find(db, p => p.loginname.Contains(approver));
        //                                        if (user != null)
        //                                        {
        //                                            OrgJob job = BaseDAL<OrgJob>.Find(db, p => p.userid == user.id && p.org_department.org_organization.companycode == organzation);
        //                                            if (job != null)
        //                                            {
        //                                                VarAmountpositioncode amount = BaseDAL<VarAmountpositioncode>.Find(db, p => p.positioncode == job.jobgrade && p.process.Contains(processName));
        //                                                if (amount != null)
        //                                                {
        //                                                    checkamount = amount.amount.Value;
        //                                                }
        //                                            }
        //                                        }
        //                                        //代理权限，等于授权人可审批金额全额是表示全权代理，不受金额限制
        //                                        string[] agent = BaseDAL<WfAgent>.FindList(db, p => p.org_user.loginname == approve && p.startdate <= DateTime.Now && p.enddate >= DateTime.Now && p.wf_agent_process.Where(c => c.canapprove == true && c.wf_process.module.ToLower() == processName.ToLower() && (c.approveamount >= form.formamount || c.approveamount >= checkamount)).Count() > 0 && p.agenttype == 1, true, p => p.id).Select(p => p.org_user1.loginname).ToArray();

        //                                        //查询是否有重新提交的情况
        //                                        WfApprovalhistory reSumbitter = BaseDAL<WfApprovalhistory>.FindList(db, p => p.incident == incident && p.processname.ToLower() == processName.ToLower() && p.stepname == "ReSubmit", false, p => p.id).FirstOrDefault();
        //                                        int historyid = 0;
        //                                        if (reSumbitter != null)
        //                                        {
        //                                            historyid = reSumbitter.id;
        //                                        }

        //                                        WfApprovalhistory last = BaseDAL<WfApprovalhistory>.Find(db, p => p.incident == incident && p.processname.ToLower() == processName.ToLower() && (p.approveraccount == approve || agent.Contains(p.approveraccount)) && (p.status == 2 || p.status == 1) && p.id > historyid);
        //                                        if (last != null)
        //                                        {
        //                                            task = _task.GetTaskEntityByName(processName, incident, approve.Replace("\\", "/"));
        //                                            if (!String.IsNullOrEmpty(task.TASKID))
        //                                            {
        //                                                subprocess = BaseDAL<TbSubprocessapprover>.Find(db, p => p.incident == incident && p.processname == processName && p.approver == approve && p.approvalstatus == 0);
        //                                                task.REASON = "";
        //                                                task.VarList = varlist;

        //                                                if (BaseDAL<OrgJob>.Find(db, p => p.org_user.loginname == approve && p.ismanager == "1") != null)
        //                                                {
        //                                                    varlist.Find(p => p.Name == "ismanager").Value = "1";
        //                                                }
        //                                                varlist.Find(p => p.Name == "isneednextapproval").Value = "N";

        //                                                if (subprocess != null)
        //                                                {
        //                                                    varlist.Find(p => p.Name == "isneednextapproval").Value = subprocess.isneednextapproval;
        //                                                    if (subprocess.nextuser != null)
        //                                                    {
        //                                                        varlist.Find(p => p.Name == "supervisorid").Value = SetUserPrefix(subprocess.nextuser);
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        varlist.Find(p => p.Name == "supervisorid").Value = "";
        //                                                    }

        //                                                    SetNextSuperiorApprover(processName, incident, subprocess.approver);
        //                                                }
        //                                                else
        //                                                {
        //                                                    subprocess = BaseDAL<TbSubprocessapprover>.Find(db, p => p.incident == incident && p.processname == processName && p.approver == approve && p.approvalstatus == 1);
        //                                                    if (subprocess != null)
        //                                                    {
        //                                                        subprocess = BaseDAL<TbSubprocessapprover>.Find(db, p => p.incident == incident && p.processname == processName && p.approver == subprocess.nextuser && p.approvalstatus == 0);
        //                                                        if (subprocess != null)
        //                                                        {
        //                                                            varlist.Find(p => p.Name == "isneednextapproval").Value = "Y";
        //                                                            varlist.Find(p => p.Name == "supervisorid").Value = SetUserPrefix(subprocess.approver);
        //                                                        }
        //                                                    }
        //                                                }

        //                                                if (_task.SubmitTask(task) > 0)
        //                                                {
        //                                                    history = new WfApprovalhistory();
        //                                                    history.incident = incident;
        //                                                    history.processname = processName;
        //                                                    history.stepname = task.STEPLABEL;
        //                                                    history.taskid = task.TASKID;
        //                                                    history.createdate = task.STARTTIME;
        //                                                    history.enddate = DateTime.Now;
        //                                                    history.taskuser = "";
        //                                                    history.assigendtouser = "";
        //                                                    history.createdate = null;
        //                                                    history.comments = "System Completed";
        //                                                    history.status = 2;
        //                                                    history.action = "Approved / 通过";
        //                                                    history.approveraccount = last.approveraccount;
        //                                                    history.approvername = last.approvername;
        //                                                    if (last.approveraccount != approve)
        //                                                    {
        //                                                        history.assigendtouser = approve;
        //                                                        history.taskuser = BaseDAL<OrgUser>.Find(db, p => p.loginname == approve).username;
        //                                                    }
        //                                                    BaseDAL<WfApprovalhistory>.Add(db, history);
        //                                                    nextApprove = true;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }

        //                                if (nextApprove == false)
        //                                {
        //                                    for (int i = 0; i < currentApproverList.Length; i++)
        //                                    {
        //                                        ComMessagequeue message1 = new ComMessagequeue();
        //                                        message1.fromuser = CurrentUser.loginname;
        //                                        message1.touser = "Porsche\\" + currentApproverList[i];
        //                                        message1.subject = "Application No. " + form.formno + " to be approved.";
        //                                        message1.body = String.Format(mailApprove, currentApproverList[i].Replace(".", " "), form.initiator.Replace("Porsche\\", "").Replace(".", " "), form.formno);
        //                                        message1.isread = 0;
        //                                        message1.sendtype = "1";
        //                                        message1.createdate = DateTime.Now;
        //                                        BaseDAL<ComMessagequeue>.Add(db, message1);
        //                                    }
        //                                }

        //                            }
        //                        }
        //                    }

        //                    result.info = LangString("Marlboro");
        //                    result.url = "close";
        //                    result.status = 1;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "提交单据异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return Json(result);
        //}

        ////拒绝操作
        //public virtual ActionResult RejectAction()
        //{
        //    try
        //    {
        //        bool flag = true;
        //        int incident = Convert.ToInt32(Request["incident"]);
        //        string processName = Request["processname"];
        //        WfForm form = BaseDAL<WfForm>.Find(db, p => p.incident == incident && p.processname == processName);
        //        TaskEntity task = new TaskEntity();
        //        if (Request["TaskUser"] != null && !String.IsNullOrEmpty(Request["TaskUser"].ToString()))
        //        {
        //            task = _task.GetTaskEntityByName(processName, incident, (Request["TaskUser"].ToString().Replace("\\", "/")));
        //        }
        //        else
        //        {
        //            task = _task.GetTaskEntityByName(processName, incident, CurrentUser.loginname.Replace("\\", "/"));
        //        }

        //        task.REASON = Request["intorderno"];
        //        task.VarList = varlist;
        //        if (_task.RejectTask(task))
        //        {
        //            using (TransactionScope transaction = new TransactionScope())
        //            {
        //                Models.WfApprovalhistory history = new WfApprovalhistory();
        //                history.incident = incident;
        //                history.processname = processName;
        //                history.stepname = task.STEPLABEL;
        //                history.taskid = task.TASKID;
        //                history.createdate = task.STARTTIME;
        //                history.enddate = DateTime.Now;
        //                history.taskuser = Request["ByAgentUser"];
        //                history.assigendtouser = Request["TaskUser"];
        //                history.comments = Request["intorderno"];
        //                history.action = "Rejected / 拒绝";
        //                history.status = 3;
        //                history.approveraccount = Request["ApproveUserAccount"];
        //                history.approvername = Request["ApproveUserName"];

        //                BaseDAL<WfApprovalhistory>.Add(db, history);

        //                db.Database.ExecuteSqlCommand("Update " + processName + " set Status=3 where incident=" + incident);

        //                db.Database.ExecuteSqlCommand("Update WF_FORM set Status=3,EndTime=GetDate() where incident=" + incident + "  and processname='" + processName + "'");


        //                ComMessagequeue message = new ComMessagequeue();
        //                message.fromuser = CurrentUser.loginname;
        //                message.touser = form.initiator;
        //                message.subject = "Application No. " + form.formno + " has been rejected.";
        //                message.body = String.Format(GetMailTemplate("REJECTFORM"), form.initiator.Replace("Porsche\\", "").Replace(".", " "), form.formno);
        //                message.isread = 0;
        //                message.sendtype = "1";
        //                message.createdate = DateTime.Now;
        //                BaseDAL<ComMessagequeue>.Add(db, message);

        //                transaction.Complete();
        //            }
        //        }

        //        if (flag)
        //        {
        //            result.info = LangString("Refuse");
        //            result.url = "close";
        //            result.status = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "拒绝单据异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return Json(result);
        //}

        //public virtual ActionResult CancelAction()
        //{
        //    try
        //    {
        //        bool flag = true;
        //        int incident = Convert.ToInt32(Request["incident"]);
        //        string processName = Request["processname"];
        //        WfForm form = BaseDAL<WfForm>.Find(db, p => p.incident == incident && p.processname == processName);
        //        if (form != null)
        //        {
        //            if (form.status != 2)
        //            {
        //                TaskEntity task = _task.GetTaskEntityByName(processName, incident);
        //                task.REASON = Request["intorderno"];
        //                task.VarList = varlist;
        //                if (_task.AbortIncident(task))
        //                {
        //                    Models.WfApprovalhistory history = new WfApprovalhistory();
        //                    history.incident = incident;
        //                    history.processname = processName;
        //                    history.stepname = "Submitter";
        //                    history.taskid = task.TASKID;
        //                    history.createdate = task.STARTTIME;
        //                    history.enddate = DateTime.Now;
        //                    history.comments = Request["intorderno"];
        //                    history.action = "Cancel / 撤销";
        //                    history.status = 0;
        //                    history.approveraccount = CurrentUser.loginname;
        //                    history.approvername = CurrentUser.username;
        //                    BaseDAL<WfApprovalhistory>.Add(db, history);
        //                    db.Database.ExecuteSqlCommand("Update " + processName + " set Status=4 where incident=" + incident);
        //                    db.Database.ExecuteSqlCommand("Update WF_FORM set Status=4 where incident=" + incident + "  and processname='" + processName + "'");
        //                }
        //            }
        //            else
        //            {
        //                Models.WfApprovalhistory history = new WfApprovalhistory();
        //                history.incident = incident;
        //                history.processname = processName;
        //                history.stepname = "Submitter";
        //                history.createdate = DateTime.Now;
        //                history.enddate = DateTime.Now;
        //                history.comments = "";
        //                history.action = "Cancel / 撤销";
        //                history.status = 0;
        //                history.approveraccount = CurrentUser.loginname;
        //                history.approvername = CurrentUser.username;
        //                BaseDAL<WfApprovalhistory>.Add(db, history);
        //                db.Database.ExecuteSqlCommand("Update " + processName + " set Status=4 where incident=" + incident);
        //                db.Database.ExecuteSqlCommand("Update WF_FORM set Status=4 where incident=" + incident + "  and processname='" + processName + "'");
        //            }
        //            if (flag)
        //            {
        //                result.info = LangString("CancelSuccess");
        //                result.url = "close";
        //                result.status = 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "取消单据异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return Json(result);
        //}

        //public virtual ActionResult BusinessApporveAction()
        //{
        //    string mailApprove = GetMailTemplate("APPROVEFORM");
        //    string mailApproved = GetMailTemplate("APPROVEDFORM");

        //    try
        //    {
        //        if (varlist == null)
        //        {
        //            varlist = new List<VarEntity>();
        //        }

        //        int incident = Convert.ToInt32(Request["incident"]);
        //        string processName = Request["processname"];
        //        ProcBusinessdata form = BaseDAL<ProcBusinessdata>.Find(db, p => p.incident == incident);
        //        TaskEntity task = new TaskEntity();
        //        string taskUser = CurrentUser.loginname;
        //        task = _task.GetTaskEntityByName(processName, incident, CurrentUser.loginname.Replace("\\", "/"));

        //        VarEntity var = new VarEntity();

        //        if (!String.IsNullOrEmpty(task.TASKID))
        //        {
        //            task.REASON = Request["intorderno"];
        //            task.VarList = varlist;

        //            int outcident = _task.SubmitTask(task);
        //            if (outcident > 0)
        //            {
        //                //using (TransactionScope transaction = new TransactionScope())
        //                {

        //                    Models.WfApprovalhistory history = new WfApprovalhistory();
        //                    history.incident = incident;
        //                    history.processname = processName;
        //                    history.stepname = task.STEPLABEL;
        //                    history.taskid = task.TASKID;
        //                    history.taskuser = Request["ByAgentUser"];
        //                    history.assigendtouser = Request["TaskUser"];
        //                    history.createdate = task.STARTTIME;
        //                    history.enddate = DateTime.Now;
        //                    history.comments = Request["intorderno"];
        //                    history.status = 2;
        //                    history.action = "Approved / 通过";
        //                    history.approveraccount = Request["ApproveUserAccount"];
        //                    history.approvername = Request["ApproveUserName"];

        //                    BaseDAL<WfApprovalhistory>.Add(db, history);

        //                    bool nextApprove = true;
        //                    //多次循环，直到当前审批人没有审批过或流程结束
        //                    while (nextApprove)
        //                    {
        //                        Thread.Sleep(1000);
        //                        int status = _task.GetIncidentStatus(processName, incident);
        //                        if (status == 0)
        //                        {
        //                            Thread.Sleep(3000);
        //                            status = _task.GetIncidentStatus(processName, incident);
        //                        }
        //                        if (status == 2)
        //                        {
        //                            nextApprove = false;
        //                            form.status = 2;
        //                            BaseDAL<ProcBusinessdata>.Update(db, form);
        //                        }
        //                        else
        //                        {
        //                            string currentApprover = _task.GetCurrentApprover(processName, incident);
        //                            if (!String.IsNullOrEmpty(currentApprover))
        //                            {
        //                                nextApprove = false;
        //                                string[] currentApproverList = currentApprover.Split(':')[1].Split('|');
        //                                for (int i = 0; i < currentApproverList.Length; i++)
        //                                {
        //                                    string approve = "Porsche\\" + currentApproverList[i];
        //                                    WfApprovalhistory last = BaseDAL<WfApprovalhistory>.Find(db, p => p.incident == incident && p.processname.ToLower() == processName.ToLower() && (p.approveraccount == approve) && (p.status == 2 || p.status == 1));
        //                                    if (last != null)
        //                                    {
        //                                        task = _task.GetTaskEntityByName(processName, incident, approve.Replace("\\", "/"));
        //                                        if (!String.IsNullOrEmpty(task.TASKID))
        //                                        {

        //                                            task.REASON = "";
        //                                            task.VarList = varlist;

        //                                            if (_task.SubmitTask(task) > 0)
        //                                            {
        //                                                history = new WfApprovalhistory();
        //                                                history.incident = incident;
        //                                                history.processname = processName;
        //                                                history.stepname = task.STEPLABEL;
        //                                                history.taskid = task.TASKID;
        //                                                history.createdate = task.STARTTIME;
        //                                                history.enddate = DateTime.Now;
        //                                                history.taskuser = "";
        //                                                history.assigendtouser = "";
        //                                                history.createdate = null;
        //                                                history.comments = "System Completed";
        //                                                history.status = 2;
        //                                                history.action = "Approved / 通过";
        //                                                history.approveraccount = last.approveraccount;
        //                                                history.approvername = last.approvername;
        //                                                if (last.approveraccount != approve)
        //                                                {
        //                                                    history.assigendtouser = approve;
        //                                                    history.taskuser = BaseDAL<OrgUser>.Find(db, p => p.loginname == approve).username;
        //                                                }
        //                                                BaseDAL<WfApprovalhistory>.Add(db, history);

        //                                                nextApprove = true;

        //                                            }
        //                                        }
        //                                    }
        //                                }

        //                                if (nextApprove == false)
        //                                {
        //                                    for (int i = 0; i < currentApproverList.Length; i++)
        //                                    {
        //                                        ComMessagequeue message1 = new ComMessagequeue();
        //                                        message1.fromuser = CurrentUser.loginname;
        //                                        message1.touser = "Porsche\\" + currentApproverList[i];
        //                                        message1.subject = "Application No. " + form.formno + " to be approved.";
        //                                        message1.body = String.Format(mailApprove, currentApproverList[i].Replace(".", " "), form.applicant, form.formno);
        //                                        message1.isread = 0;
        //                                        message1.sendtype = "1";
        //                                        message1.createdate = DateTime.Now;
        //                                        BaseDAL<ComMessagequeue>.Add(db, message1);
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    result.info = LangString("Marlboro");
        //                    result.url = "close";
        //                    result.status = 1;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "主数据单据审批异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return Json(result);
        //}

        //public virtual ActionResult BusinessRejectAction()
        //{
        //    try
        //    {
        //        bool flag = true;
        //        int incident = Convert.ToInt32(Request["incident"]);
        //        string processName = Request["processname"];
        //        ProcBusinessdata form = BaseDAL<ProcBusinessdata>.Find(db, p => p.incident == incident, true);
        //        TaskEntity task = new TaskEntity();
        //        task = _task.GetTaskEntityByName(processName, incident, CurrentUser.loginname.Replace("\\", "/"));

        //        task.REASON = Request["intorderno"];
        //        task.VarList = varlist;
        //        if (_task.RejectTask(task))
        //        {
        //            using (TransactionScope transaction = new TransactionScope())
        //            {
        //                //拒绝后, 更新审核表单状态,  
        //                form.status = 3;
        //                BaseDAL<ProcBusinessdata>.Update(db, form);

        //                Models.WfApprovalhistory history = new WfApprovalhistory();
        //                history.incident = incident;
        //                history.processname = processName;
        //                history.stepname = task.STEPLABEL;
        //                history.taskid = task.TASKID;
        //                history.createdate = task.STARTTIME;
        //                history.enddate = DateTime.Now;
        //                history.taskuser = Request["ByAgentUser"];
        //                history.assigendtouser = Request["TaskUser"];
        //                history.comments = Request["intorderno"];
        //                history.action = "Rejected / 拒绝";
        //                history.status = 3;
        //                history.approveraccount = Request["ApproveUserAccount"];
        //                history.approvername = Request["ApproveUserName"];

        //                BaseDAL<WfApprovalhistory>.Add(db, history);


        //                ComMessagequeue message = new ComMessagequeue();
        //                message.fromuser = CurrentUser.loginname;
        //                message.touser = "Porsche\\" + form.applicantaccount;
        //                message.subject = "Business data applicaiton No. " + form.formno + " has been rejected.";
        //                message.body = String.Format(GetMailTemplate("REJECTFORM"), form.applicant, form.formno);
        //                message.isread = 0;
        //                message.sendtype = "1";
        //                message.createdate = DateTime.Now;
        //                BaseDAL<ComMessagequeue>.Add(db, message);

        //                transaction.Complete();
        //            }
        //        }

        //        if (flag)
        //        {
        //            result.info = LangString("Refuse");
        //            result.url = "close";
        //            result.status = 1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(CurrentUser.username + "主数据拒绝审批异常！", ex);
        //        result.info = LangString("SystemException");
        //    }
        //    return Json(result);
        //}

        //上传图片
        public Result UploadFie(bool saveTable, int? formid = 0, string processname = null)
        {
            Result result = new Result();
            result.info = "Please select upload file / 请选择上传文件";

            //文件保存目录URL
            String saveUrl = "/uploads/";

            //定义允许上传的文件扩展名
            string[] typename = { "image", "media", "file" };

            Hashtable extTable = new Hashtable();
            extTable.Add("image", "gif,jpg,jpeg,png,bmp");
            extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
            extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2,pdf,eml");

            //最大文件大小
            int maxSize = 10 * 1024 * 1024;
            try
            {

                if (HttpContext.Request.Files.Count == 0)
                {
                    result.info = "Upload file size over limit / 上传文件大小超过限制";
                    return result;
                }

                HttpPostedFileBase imgFile = HttpContext.Request.Files[0];
                if (imgFile == null)
                {
                    result.info = "Please select upload file / 请选择上传文件";
                    return result;
                }
                String dirPath = Server.MapPath(saveUrl);
                if (!Directory.Exists(dirPath))
                {
                    result.info = "Please select upload file / 上传目录不存在！";
                    return result;
                }

                String fileName = imgFile.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();
                if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
                {
                    result.info = "Upload file size over limit / 上传文件大小超过限制";
                    return result;
                }

                String dirName = "file";
                bool isInclude = false;
                for (int i = 0; i < extTable.Count; i++)
                {
                    if (!String.IsNullOrEmpty(fileExt) && Array.IndexOf(((String)extTable[typename[i]]).Split(','), fileExt.Substring(1).ToLower()) >= 0)
                    {
                        isInclude = true;
                        dirName = typename[i];
                        break;
                    }
                }

                if (!isInclude)
                {
                    result.info = "Extended name not allowed! / 上传文件扩展名是不允许的扩展名!";
                    return result;
                }

                //创建文件夹
                dirPath += dirName + "/";
                saveUrl += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + "/";
                saveUrl += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                String filePath = dirPath + newFileName;

                imgFile.SaveAs(filePath);
                string fileUrl = "";
                if (saveTable)
                {
                    WfAttachment attachment = new WfAttachment();
                    if (formid.HasValue && formid != 0)
                    {
                        attachment.formid = formid.ToString();
                    }
                    if (!String.IsNullOrEmpty(processname))
                    {
                        attachment.processname = processname;
                    }
                    attachment.filesize = Convert.ToInt32(imgFile.InputStream.Length);
                    attachment.createby = CurrentUser.username;
                    attachment.createdate = DateTime.Now;
                    attachment.newname = imgFile.FileName;
                    attachment.status = "0";
                    attachment.filename = saveUrl + newFileName;
                    attachment = BaseDAL<WfAttachment>.Add(db, attachment);
                    fileUrl = saveUrl + newFileName + "|" + imgFile.InputStream.Length.ToString() + "|" + CurrentUser.username + "|" + attachment.createdate.Value.ToString("yyyy-MM-dd") + "|" + imgFile.FileName + "|" + attachment.id;
                }
                else
                {
                    fileUrl = saveUrl + newFileName + "|" + imgFile.InputStream.Length.ToString() + "|" + CurrentUser.username + "|" + DateTime.Now.ToString("yyyy-MM-dd") + "|" + imgFile.FileName;
                }

                result.status = 1;
                result.url = fileUrl;
            }
            catch (Exception ex)
            {
                result.info = "Upload file size over limit / 上传文件大小超过限制";
            }

            return result;
        }

        //多语言
        public string LangString(string key)
        {
            Type resourceType = (Thread.CurrentThread.CurrentUICulture.Name == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            if (Session["Lang"] != null)
            {
                resourceType = (Session["Lang"].ToString() == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            }
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
                return p.GetValue(null, null).ToString();
            else
                return "undefined";
        }

        //条形码生成
        [HttpGet]
        public void GetBarCode(string code)
        {
            MemoryStream ms = new MemoryStream();
            new Common.BarCodeHelper().GenerateBarCode(code).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(ms.ToArray());
        }

        //public void AddLog<T>(int modifyType, string tableName, T enity, int? formid = 0)
        //{
        //    BdLog log = new BdLog();
        //    if (modifyType < 3)
        //    {
        //        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        //        System.Text.StringBuilder json = new System.Text.StringBuilder();
        //        jsonSerializer.Serialize(enity, json);
        //        log.newdata = json.ToString();
        //    }
        //    log.modifyby = CurrentUser.username;
        //    log.modifytime = DateTime.Now;
        //    log.tablename = tableName;
        //    log.modifytype = modifyType;
        //    log.dataid = tableName == "OrgOrganizations" ? 0 : ((dynamic)enity).id;
        //    log.formid = formid;
        //    BaseDAL<BdLog>.Add(db, log);

        //}

        //public string GetMailTemplate(string templateName)
        //{
        //    if (!String.IsNullOrEmpty(templateName))
        //    {
        //        FrmTemplate template = BaseDAL<FrmTemplate>.Find(db, p => p.templatename == templateName, true);
        //        if (template != null)
        //        {
        //            return template.templatesource;
        //        }
        //    }
        //    return null;
        //}

        //public string GetApproverByCostcenter(string processName, int? id, string CostCenterCode, string InterOrderNo, Decimal cnyamount, string strApplicant, bool isTotal)
        //{
        //    string strApprover = "";
        //    string organization = "";
        //    RoleController role = new RoleController();
        //    if (!string.IsNullOrEmpty(CostCenterCode))
        //    {
        //        BdCostcenter cc = BaseDAL<BdCostcenter>.Find(db, p => p.costcentercode == CostCenterCode && p.isdelete == 0);//获取成本中心
        //        if (cc != null)
        //        {
        //            if (cc.org_user != null)
        //            {
        //                strApprover = cc.org_user.loginname;//成本中心负责人账号  
        //            }
        //            organization = cc.organization;
        //        }
        //    }
        //    else
        //    {
        //        BdInternalorder inter = BaseDAL<BdInternalorder>.Find(db, p => p.internalorderno == InterOrderNo && p.isdelete == 0);
        //        if (inter != null)
        //        {
        //            OrgJob interjob = BaseDAL<OrgJob>.Find(db, p => p.jobgrade == inter.positioncode);
        //            if (interjob != null)
        //            {
        //                if (interjob.org_user != null)
        //                {
        //                    strApprover = interjob.org_user.loginname;
        //                }
        //                organization = interjob.org_department.organization;
        //            }
        //        }
        //    }

        //    if (strApprover == strApplicant)
        //    {
        //        strApprover = new RuleController().GetSuperior(strApplicant, organization).loginname;
        //    }

        //    if (String.IsNullOrEmpty(strApprover))
        //    {
        //        return "";
        //    }

        //    if (role.IsCeo(strApprover) == 0 && role.IsCfo(strApprover) == 0)
        //    {
        //        TbSubprocessapprover spa = BaseDAL<TbSubprocessapprover>.Find(db, p => p.processname == processName && p.formid == id && p.approver == strApprover);
        //        if (spa != null)
        //        {
        //            //如果是总金额，则不累加
        //            if (isTotal)
        //            {
        //                spa.subamount = cnyamount;
        //            }
        //            else
        //            {
        //                spa.subamount += cnyamount;
        //            }
        //            if (spa.isneednextapproval == "N")
        //            {
        //                if (spa.subamount > spa.authamount)
        //                {
        //                    OrgUser superiorUser = new OrgUser();
        //                    superiorUser = new RuleController().GetSuperior(strApprover, organization);

        //                    if (superiorUser != null && role.IsCeo(superiorUser.loginname) == 0 && role.IsCfo(superiorUser.loginname) == 0)
        //                    {
        //                        spa.nextuser = superiorUser.loginname;
        //                        spa.isneednextapproval = "Y";
        //                    }
        //                }
        //            }
        //            BaseDAL<TbSubprocessapprover>.Update(db, spa);  //判断如果当前的负责人已存在，则累加审批金额更新 
        //        }
        //        else
        //        {
        //            OrgJob job = BaseDAL<OrgJob>.Find(db, p => p.org_user.loginname == strApprover && p.org_department.organization == organization);
        //            if (job != null)
        //            {
        //                string intPositionId = job.jobgrade;//获取当前审批人的职级
        //                decimal AuthAmount = new RuleController().GetAmountByProcessAndPosition(processName, intPositionId); //根据当前人的职级和流程名获取审批金额权限 

        //                spa = new TbSubprocessapprover();
        //                spa.processname = processName;
        //                spa.formid = id;
        //                spa.approver = strApprover;
        //                spa.subamount = cnyamount;
        //                spa.authamount = AuthAmount;
        //                spa.isneednextapproval = "N";
        //                if (spa.subamount > spa.authamount)
        //                {
        //                    OrgUser superiorUser = new OrgUser();
        //                    superiorUser = new RuleController().GetSuperior(strApprover, organization);

        //                    if (superiorUser != null && role.IsCeo(superiorUser.loginname) == 0 && role.IsCfo(superiorUser.loginname) == 0)
        //                    {
        //                        spa.nextuser = superiorUser.loginname;
        //                        spa.isneednextapproval = "Y";
        //                    }
        //                }
        //                spa.approvalstatus = 0;//待审批
        //                BaseDAL<TbSubprocessapprover>.Add(db, spa);  //获取成本中心负责人，获取授权金额，审批金额 新增一条
        //                return SetUserPrefix(strApprover);
        //            }
        //        }
        //    }

        //    return null;
        //}

        //public string SetUserPrefix(string strLoginName)
        //{
        //    if (!String.IsNullOrEmpty(strLoginName))
        //        return strLoginName.Replace(@"\", @"/") + "[USER]";
        //    else
        //        return null;
        //}

        //public void SetNextSuperiorApprover(string processName, int incident, string strCurrentUser)
        //{
        //    int intDeparmentId = BaseDAL<OrgUser>.Find(db, p => p.loginname == strCurrentUser).deparmentid.Value;
        //    String org = CurrentUser.organization;
        //    int? costcenter = BaseDAL<OrgUser>.Find(db, p => p.loginname == strCurrentUser).costcenter;
        //    if (costcenter.HasValue)
        //    {
        //        org = BaseDAL<BdCostcenter>.Find(db, costcenter.Value).organization;
        //    }
        //    TbSubprocessapprover spa = new TbSubprocessapprover();
        //    spa = BaseDAL<TbSubprocessapprover>.Find(db, p => p.processname == processName && p.incident == incident && p.approver == strCurrentUser && p.approvalstatus == 0);
        //    RoleController role = new RoleController();
        //    if (spa != null)
        //    {
        //        spa.approvalstatus = 1;//已审批
        //        BaseDAL<TbSubprocessapprover>.Update(db, spa);//更新下已经审批的状态

        //        if (spa.isneednextapproval == "Y")
        //        {
        //            TbSubprocessapprover nextspa = new TbSubprocessapprover();
        //            nextspa = BaseDAL<TbSubprocessapprover>.Find(db, p => p.processname == processName && p.incident == incident && p.approver == spa.nextuser);
        //            //已经有该审批人 
        //            if (nextspa != null)
        //            {
        //                if (nextspa.approvalstatus == 0) //待审批的状态 
        //                {
        //                    nextspa.subamount += spa.subamount;
        //                    if (nextspa.subamount > nextspa.authamount && nextspa.isneednextapproval == "N")
        //                    {
        //                        OrgUser superiorUser = new RuleController().GetSuperior(nextspa.approver, intDeparmentId);
        //                        if (superiorUser != null && role.IsCeo(superiorUser.loginname) == 0 && role.IsCfo(superiorUser.loginname) == 0)
        //                        {
        //                            nextspa.nextuser = superiorUser.loginname;
        //                            nextspa.isneednextapproval = "Y";
        //                        }
        //                    }
        //                    BaseDAL<TbSubprocessapprover>.Update(db, nextspa);  //判断如果当前的人已存在，则累加审批金额更新 
        //                }
        //            }
        //            else
        //            {
        //                //SetSuperiorApprover(spa.processname, spa.formid.Value, spa.incident, strCurrentUser, spa.subamount);
        //                nextspa = new TbSubprocessapprover();
        //                //无此审批人则添加审批人信息
        //                nextspa.processname = spa.processname;
        //                nextspa.formid = spa.formid;
        //                nextspa.approver = spa.nextuser;
        //                nextspa.subamount = spa.subamount;
        //                nextspa.incident = spa.incident;
        //                nextspa.isneednextapproval = "N";
        //                if (spa.nextuser != "")
        //                {
        //                    OrgUser nextUser = new OrgUser();
        //                    nextUser = BaseDAL<OrgUser>.Find(db, p => p.loginname == spa.nextuser);
        //                    OrgJob job = BaseDAL<OrgJob>.Find(db, p => p.userid == nextUser.id && p.org_department.organization == org);
        //                    if (job != null)
        //                    {
        //                        string intPositionId = job.jobgrade;  //获取上级主管职级 
        //                        decimal AuthAmount = new RuleController().GetAmountByProcessAndPosition(processName, intPositionId); //根据上级主管的职级和流程名获取审批金额权限  

        //                        nextspa.authamount = AuthAmount;
        //                        if ((nextspa.subamount > nextspa.authamount))
        //                        {
        //                            OrgUser superiorUser = new RuleController().GetSuperior(nextspa.approver, intDeparmentId);
        //                            if (superiorUser != null && role.IsCeo(superiorUser.loginname) == 0 && role.IsCfo(superiorUser.loginname) == 0)
        //                            {
        //                                nextspa.nextuser = superiorUser.loginname;
        //                                nextspa.isneednextapproval = "Y";
        //                            }
        //                        }
        //                        nextspa.approvalstatus = 0;//待审批
        //                        BaseDAL<TbSubprocessapprover>.Add(db, nextspa);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //}

        //public string SetSuperiorApprover(string processName, int formid, int? incident, string approver, decimal? subamount)
        //{
        //    string strCurrentUser = approver;
        //    //int intCurrentUserId = CurrentUser.id;
        //    OrgUser approverUser = BaseDAL<OrgUser>.Find(db, p => p.loginname == approver);
        //    try
        //    {
        //        int intDeparmentId = approverUser.deparmentid.Value;
        //        OrgUser superiorUser = new RuleController().GetSuperior(approver, intDeparmentId);
        //        RoleController role = new RoleController();
        //        if (superiorUser != null && ((role.IsCeo(superiorUser.loginname) == 0 && role.IsCfo(superiorUser.loginname) == 0) || ((role.IsCfo(superiorUser.loginname) == 1 || role.IsCeo(superiorUser.loginname) == 1) && subamount < 1000000)))
        //        {
        //            OrgJob job = BaseDAL<OrgJob>.Find(db, p => p.userid == superiorUser.id);
        //            string intPositionId = job.jobgrade;  //获取上级主管职级 
        //            decimal AuthAmount = new RuleController().GetAmountByProcessAndPosition(processName, intPositionId); //根据上级主管的职级和流程名获取审批金额权限  

        //            TbSubprocessapprover nextspa = new TbSubprocessapprover();
        //            nextspa.processname = processName;
        //            nextspa.formid = formid;
        //            nextspa.approver = superiorUser.loginname;
        //            nextspa.subamount = subamount;
        //            nextspa.isneednextapproval = "N";
        //            nextspa.authamount = AuthAmount;
        //            nextspa.incident = incident;
        //            if (superiorUser != null && (nextspa.subamount > nextspa.authamount))
        //            {
        //                OrgUser spa = new RuleController().GetSuperior(superiorUser.loginname, intDeparmentId);
        //                if (spa != null && ((role.IsCeo(spa.loginname) == 0 && role.IsCfo(spa.loginname) == 0) || ((role.IsCfo(spa.loginname) == 1 || role.IsCeo(spa.loginname) == 1) && subamount < 1000000)))
        //                {
        //                    nextspa.nextuser = spa.loginname;
        //                    nextspa.isneednextapproval = "Y";
        //                }
        //            }
        //            nextspa.approvalstatus = 0;//待审批
        //            BaseDAL<TbSubprocessapprover>.Add(db, nextspa);
        //            return SetUserPrefix(nextspa.approver);
        //        }
        //        else
        //            return null;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public bool isDummyCostcenter(string costcenterCode)
        //{
        //    if (!String.IsNullOrEmpty(costcenterCode))
        //    {
        //        //BdCostcenter costcenter = BaseDAL<BdCostcenter>.Find(db, p => p.costcentercode == costcenterCode && p.isdelete == 0);
        //        return (costcenterCode.ToLower().IndexOf("dummy") >= 0) ? true : false;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public void CheckRight()
        //{
        //    string url = Url.Action("");

        //    ViewBag.CanEdit = db.SecMenurightsobject.Where(n => db.SecMenurightsmember.Where(m => m.groupname.ToLower() == "all users" || m.memberid == CurrentUser.id || db.OrgGroupmember.Where(g => g.memberid == CurrentUser.id).Select(g => g.groupid).Contains(m.groupid)).Select(m => m.rightsid).Contains(n.rightsid) && n.canedit == 1 && n.sec_menu.url.IndexOf(url) >= 0).Count() > 0 ? true : false;
        //    ViewBag.CanDelete = db.SecMenurightsobject.Where(n => db.SecMenurightsmember.Where(m => m.groupname.ToLower() == "all users" || m.memberid == CurrentUser.id || db.OrgGroupmember.Where(g => g.memberid == CurrentUser.id).Select(g => g.groupid).Contains(m.groupid)).Select(m => m.rightsid).Contains(n.rightsid) && n.candelete == 1 && n.sec_menu.url.IndexOf(url) >= 0).Count() > 0 ? true : false;
        //    // ViewBag.CanEdit = BaseDAL<SecMenurightsmember>.Find(db, p => (p.memberid == CurrentUser.id && p.sec_menurights.sec_menurightsobject.Where(d => d.sec_menu.url.IndexOf(url) >= 0 && d.canedit == 1).Count() > 0)) != null ? true : false;
        //    // ViewBag.CanDelete = BaseDAL<SecMenurightsmember>.Find(db, p => p.memberid == CurrentUser.id && p.sec_menurights.sec_menurightsobject.Where(d => d.sec_menu.url.IndexOf(url) >= 0 && d.candelete == 1).Count() > 0) != null ? true : false;
        //}

    }
}