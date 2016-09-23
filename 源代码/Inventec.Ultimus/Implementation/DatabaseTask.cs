using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;
using System.Data;
using System.Xml;
using Inventec.Ultimus.Entity;
using Ultimus.UWF.Workflow.Logic;
using Inventec.Models;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Inventec.Ultimus.Implementation
{
    public class DatabaseTask : Inventec.Ultimus.Interface.ITask
    {
        private UltimusContext Ultimusdb = new UltimusContext();
        private InventecbizContext Ultimusbizdb = new InventecbizContext();
        //调用接口实现
        public virtual List<TaskEntity> GetInitTaskList(string loginName, string filter)
        {
            string sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'" + GetServerName() + "' AS SERVERNAME FROM INITIATE WITH(NOLOCK)";
            if (!string.IsNullOrEmpty(filter))
            {
                sql = "SELECT INITIATEID AS TASKID,PROCESSNAME,PROCESSVERSION,PROCESSHELPURL AS HELPURL,INITIATOR AS ASSIGNEDTOUSER,'" + GetServerName() + "' AS SERVERNAME FROM INITIATE  WITH(NOLOCK) where processname like '%" + filter + "%'";
            }
            return Ultimusdb.Database.SqlQuery<TaskEntity>(sql).ToListAsync().Result;
        }

        //调用接口实现
        public virtual List<TaskEntity> GetDraftTaskList(string loginName, string filter)
        {
            return null;
        }

        //获取我的任务列表
        public virtual List<TaskEntity> GetMyTaskList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SERVERNAME", GetServerName());
            param[1] = new SqlParameter("@ASSIGNEDTOUSER", loginName.Replace("\\", "/"));

            string sql = @"SELECT distinct TASKID,
                  b.PROCESSNAME,
                  a.INCIDENT,
                  '' as SUMMARY,
                  f.Username as INITIATOR,
                  a.STEPLABEL,
                  a.TASKUSER,
                  a.ASSIGNEDTOUSER,
                  a.STATUS,
                  a.SUBSTATUS,
                  a.STARTTIME,
                  a.ENDTIME,
                  a.STEPID,
                  a.OVERDUETIME,
                  b.STATUS as PROCESSSTATUS,
                  @SERVERNAME as SERVERNAME,
                  d.PROCESSNAME as PROCESSNAMEEN,
                  d.DISPLAYNAME as PROCESSNAMECN,
                  E.DISPLAYNAME as STEPLABELNAME,
                  b.FORMNO,
                  b.ISURGENT,
                  b.PAYMENTSTATUS,b.ISURGENT,
                  b.STARTTIME,
                  b.DEPT as DEPARTMENT,
                  b.FORMAMOUNT,
                  b.INVOICEDATE 
                  FROM UltimusBiz.dbo.[WF_FORM] b 
                  LEFT JOIN vw_TASKS a ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT
                  LEFT JOIN UltimusBiz.dbo.[WF_PROCESS] as d on b.PROCESSNAME=d.MODULE
                  LEFT JOIN UltimusBiz.dbo.[WF_PROCESSSTEP] as e on a.PROCESSNAME=e.PROCESSNAME and a.STEPLABEL=e.STEPNAME 
                  LEFT JOIN (select * from UltimusBiz.dbo.[ORG_USER] where ISACTIVE=1) as f on REPLACE(b.INITIATOR,'/','\') = f.LOGINNAME
                  LEFT JOIN UltimusBiz.dbo.[V_AgentUser] as g on b.PROCESSNAME=g.Module
                  and G.UserName = REPLACE(a.TASKUSER,'/','\')
                  WHERE a.STATUS=1 and b.STATUS=1 AND (a.ASSIGNEDTOUSER=@ASSIGNEDTOUSER OR g.AgentUserName=REPLACE(@ASSIGNEDTOUSER,'/','\'))";
            if (!String.IsNullOrEmpty(filter))
            {
                sql = sql + filter;
            }
            sql = sql + " Order by b.ISURGENT desc,a.STARTTIME";
            List<TaskEntity> list = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
            return list;
        }

        public virtual List<TaskEntity> GetMyBusinessDataList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SERVERNAME", GetServerName());
            param[1] = new SqlParameter("@ASSIGNEDTOUSER", loginName.Replace("\\", "/"));

            string sql = @"Select TASKID, b.TYPENAME as PROCESSNAME,
                  a.INCIDENT,
                  f.Username as INITIATOR,
                  a.STEPLABEL,
                  a.TASKUSER,
                  a.ASSIGNEDTOUSER,
                  a.STATUS,
                  a.SUBSTATUS,
                  a.STARTTIME,
                  a.ENDTIME,
                  a.STEPID,
                  a.OVERDUETIME,
                  b.STATUS as PROCESSSTATUS,
                  d.PROCESSNAME as PROCESSNAMEEN,
                  d.DISPLAYNAME as PROCESSNAMECN,
                  E.DISPLAYNAME as STEPLABELNAME,
                  b.FORMNO,
                  b.REQUESTDATE as STARTDATE,
                  h.Statusen as STATUSNAME
                  FROM UltimusBiz.dbo.PROC_BusinessData b 
                  LEFT JOIN vw_TASKS a ON a.PROCESSNAME = 'PROC_BusinessData' AND a.INCIDENT = b.INCIDENT 
                  LEFT JOIN UltimusBiz.dbo.[WF_PROCESS] as d on a.PROCESSNAME=d.MODULE 
                  LEFT JOIN UltimusBiz.dbo.[WF_PROCESSSTEP] as e on a.PROCESSNAME=e.PROCESSNAME and a.STEPLABEL=e.STEPNAME 
                  LEFT JOIN (select * from UltimusBiz.dbo.[ORG_USER] where ISACTIVE=1) as f on REPLACE(b.APPLICANTACCOUNT,'/','\') = f.LOGINNAME
                  LEFT JOIN UltimusBiz.dbo.[V_AgentUser] as g on a.PROCESSNAME=g.Module 
                  and G.UserName = REPLACE(a.TASKUSER,'/','\') 
                  LEFT JOIN UltimusBiz.dbo.[WF_PROCESSSTEPSSTATUS] as h on b.STATUS=h.Status
                  WHERE (a.ASSIGNEDTOUSER=@ASSIGNEDTOUSER OR g.AgentUserName=REPLACE(@ASSIGNEDTOUSER,'/','\'))";
            if (!String.IsNullOrEmpty(filter))
            {
                sql = sql + filter;
            }
            sql = sql + " Order by b.REQUESTDATE";
            List<TaskEntity> list = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
            return list;
        }

        string GetServerName()
        {
            return "Porsche";
        }

        string GetDBName()
        {
            string str = GetServerEntity().DBNAME;
            if (string.IsNullOrEmpty(str))
            {
                str = "Ultimus";
            }
            return str;
        }

        public virtual int GetMyTaskCount(string loginName, string filter, List<ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            //return Convert.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyTaskCount", table));
            return 0;
        }

        //获取我审批的列表
        public virtual List<TaskEntity> GetMyApprovalList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SERVERNAME", GetServerName());
            param[1] = new SqlParameter("@ASSIGNEDTOUSER", loginName.Replace("\\", "/"));

            string sql = @"SELECT distinct a.TASKID,
                  b.PROCESSNAME,
                  a.INCIDENT,
                  '' as SUMMARY,
                  f.Username as INITIATOR,
                  a.STEPNAME,
                  a.APPROVERACCOUNT as TASKUSER,
                  a.ASSIGENDTOUSER as ASSIGNEDTOUSER,
                  a.STATUS,                  
                  a.CREATEDATE as STARTTIME,
                  a.ENDDATE as ENDTIME,
                  a.taskID as STEPID,
				  b.STATUS as PROCESSSTATUS,
                  d.PROCESSNAME as PROCESSNAMEEN,
                  d.DISPLAYNAME as PROCESSNAMECN,
                  E.DISPLAYNAME as STEPLABELNAME,
                  b.FORMNO,
                  b.ISURGENT,
                  b.PAYMENTSTATUS,
                  b.STARTTIME,
                  b.DEPT as DEPARTMENT,
                  b.FORMAMOUNT,
                  b.INVOICEDATE,a.STEPNAME as STEPLABEL
                  FROM  [WF_APPROVALHISTORY] as a 
				  left join [WF_FORM] b on a.PROCESSNAME=b.PROCESSNAME
                  and a.INCIDENT=b.INCIDENT 
				  LEFT JOIN [WF_PROCESS] as d on b.PROCESSNAME=d.MODULE
                  LEFT JOIN [WF_PROCESSSTEP] as e on a.PROCESSNAME=e.PROCESSNAME and a.STEPNAME=e.STEPNAME 
                  LEFT JOIN (select * from [ORG_USER] where ISACTIVE=1) as f on REPLACE(b.INITIATOR,'/','\') = f.LOGINNAME  
				   where a.PROCESSNAME<>'Proc_BusinessData' and (a.status in (2,3)) and (a.Comments<>'System Completed'  or a.COMMENTS is null or (a.Comments='System Completed' and (a.TaskUser<>'' or a.TaskUser is not null)))  and b.formno is not null  AND (REPLACE(a.APPROVERACCOUNT,'\','/')=@ASSIGNEDTOUSER or REPLACE(a.ASSIGENDTOUSER,'\','/')=@ASSIGNEDTOUSER)";
            if (!String.IsNullOrEmpty(filter))
            {
                sql = sql + filter;
            }
            sql = sql + " Order by b.STARTTIME ";
            return Ultimusbizdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
        }

        public virtual int GetMyApprovalCount(string loginName, string filter, List<ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            //return Convert.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyApprovalCount", table));
            return 0;
        }

        //获取我的请求列表
        public virtual List<TaskEntity> GetMyRequestList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SERVERNAME", GetServerName());
            param[1] = new SqlParameter("@ASSIGNEDTOUSER", loginName.Replace("\\", "/"));

            string sql = @"Select b.PROCESSNAME,
                          b.INCIDENT,
                          b.SUMMARY,
                          f.Username as INITIATOR,
                          a.STEPLABEL,
                          b.STARTTIME,
                          Case when b.PAYMENTSTATUS=3 then b.PaymentDate else b.ENDTIME end as EndTime,
                          a.STEPID,
                          b.STATUS as PROCESSSTATUS,
                          @SERVERNAME as SERVERNAME,
                          d.PROCESSNAME as PROCESSNAMEEN,
                          d.DISPLAYNAME as PROCESSNAMECN,
                          E.DISPLAYNAME as STEPLABELNAME,
                          b.FORMNO,
                          b.ISURGENT,
                          b.PAYMENTSTATUS,
                          g.Statusen as STATUSNAME,
                            b.VoucherNo,
                            b.IsDownload,
                           b.FormAmount
                          FROM UltimusBiz.dbo.[WF_FORM] b 
						   LEFT JOIN (SELECT distinct PROCESSNAME,INCIDENT,STEPID,PROCESSVERSION,STEPLABEL FROM TASKS where STATUS=1) as a
						   ON a.PROCESSNAME = b.PROCESSNAME AND a.INCIDENT = b.INCIDENT 
                           LEFT JOIN PROCESSSTEPS c  ON a.PROCESSNAME=c.PROCESSNAME and   a.PROCESSVERSION=c.PROCESSVERSION and a.STEPID=c.STEPID 
                           Left join UltimusBiz.dbo.[WF_PROCESS] as d on b.PROCESSNAME=d.MODULE
                           Left join UltimusBiz.dbo.[WF_PROCESSSTEP] as e on a.PROCESSNAME=e.PROCESSNAME and a.STEPLABEL=e.STEPNAME 
                           LEFT JOIN (select * from UltimusBiz.dbo.[ORG_USER] where ISACTIVE=1) as f on REPLACE(b.INITIATOR,'/','\') = f.LOGINNAME
                           LEFT JOIN UltimusBiz.dbo.[WF_PROCESSSTEPSSTATUS] as g on b.STATUS=g.Status
                           WHERE 1=1  AND (B.INITIATOR=REPLACE(@ASSIGNEDTOUSER,'/','\') OR B.AGENT=REPLACE(@ASSIGNEDTOUSER,'/','\'))";
            if (!String.IsNullOrEmpty(filter))
            {
                sql = sql + filter;
            }
            sql = sql + " Order by b.STARTTIME desc";
            return Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
        }

        public virtual int GetMyRequestCount(string loginName, string filter, List<ParameterEntity> paras)
        {
            Hashtable table = TypeUtil.GetHashtable(paras);
            if (table == null)
            {
                table = new Hashtable();
            }
            table.Add("filter", filter);
            table.Add("ASSIGNEDTOUSER", "'" + loginName.Replace("\\", "/") + "'");
            //return Convert.ToInt32(DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetMyRequestCount", table));
            return 0;
        }

        public virtual List<TaskEntity> GetTaskList(string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SERVERNAME", GetServerName());

            string sql = @"Select b.PROCESSNAME,
                                  b.INCIDENT,
                                  b.SUMMARY,
                                  f.Username as INITIATOR,
                                  b.STARTTIME,
                                 Case when b.PAYMENTSTATUS=3 then b.PaymentDate else b.ENDTIME end as EndTime,
                                  b.STATUS as PROCESSSTATUS,                          
                                  d.PROCESSNAME as PROCESSNAMEEN,
                                  d.DISPLAYNAME as PROCESSNAMECN,
                                  b.FORMNO,
                                  b.ISURGENT,
                                  b.PAYMENTSTATUS,
                                  b.INVOICEDATE,
                                  b.FormAmount,
                                  b.VoucherNo,
                                  b.IsDownload
                                  FROM WF_FORM b 						   
                                  Left join WF_PROCESS as d on b.PROCESSNAME=d.MODULE                        
                                  LEFT JOIN (select * from [ORG_USER] where ISACTIVE=1) as f on REPLACE(b.INITIATOR,'/','\') = f.LOGINNAME
                                  WHERE 1=1 ";
            if (!String.IsNullOrEmpty(filter))
            {
                sql = sql + filter;
            }

            string sorting = " Order by b.ISURGENT desc,b.STARTTIME ";
            //当 Request["category"]==1(staff  application)  时 
            //如果筛选条件选择了 staff  application 查询出来的结果按b.invoicedate 排序

            if (System.Web.HttpContext.Current.Request["category"] != null && !String.IsNullOrEmpty(System.Web.HttpContext.Current.Request["category"]))
            {
                if (System.Web.HttpContext.Current.Request["category"] == "1")
                {
                    sorting = " Order by b.INVOICEDATE ";
                }
            }
            
            sql = sql + sorting;

            return Ultimusbizdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
        }

        public virtual int GetTaskListCount(string filter, List<ParameterEntity> paras)
        {
            return 0;
        }

        public virtual TaskEntity GetTaskEntity(string taskID)
        {
            if (taskID.StartsWith("S"))
            {
                List<TaskEntity> task = Ultimusdb.Database.SqlQuery<TaskEntity>("SELECT INITIATEID AS TASKID,PROCESSNAME FROM INITIATE  with(nolock) WHERE INITIATEID=@INITIATEID", new SqlParameter("@INITIATEID", taskID)).ToListAsync().Result;
                if (task.Count > 0)
                {
                    return task[0];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Hashtable table = new Hashtable();
                table.Add("taskID", taskID);
                table.Add("SERVERNAME", "'" + GetServerName() + "'");
                //return DataAccess.Instance(GetDBName()).GetEntity<TaskEntity>("TaskLogic_GetEntity", table);
                return new TaskEntity();
            }
        }

        public virtual TaskEntity GetTaskEntityByName(string processName, int incident, string loginName)
        {
            String sql = "SELECT a.*,b.INITIATOR,b.STATUS AS PROCESSSTATUS FROM vw_TASKS AS A left join INCIDENTS  AS B on A.Processname=B.Processname and A.INCIDENT=B.INCIDENT WHERE A.PROCESSNAME=@PROCESSNAME AND A.INCIDENT=@INCIDENT AND A.ASSIGNEDTOUSER=@ASSIGENDTOUSER AND A.STATUS=1 ";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);
            param[2] = new SqlParameter("@ASSIGENDTOUSER", loginName);

            List<TaskEntity> list = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
            if (list.Count > 0)
            {
                return list[0];
            }
            else
                return new TaskEntity();
        }

        public virtual TaskEntity GetTaskEntityByName(string processName, int incident)
        {
            String sql = "SELECT a.*,b.INITIATOR,b.STATUS AS PROCESSSTATUS FROM vw_TASKS AS A left join INCIDENTS  AS B on A.Processname=B.Processname and A.INCIDENT=B.INCIDENT WHERE a.PROCESSNAME=@PROCESSNAME AND a.INCIDENT=@INCIDENT AND a.STATUS=1 ";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);

            List<TaskEntity> list = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
            if (list.Count > 0)
            {
                return list[0];
            }
            else
                return new TaskEntity();
        }

        public virtual TaskEntity GetIncidentEntityByName(string processName, int incident)
        {
            String sql = "SELECT a.*,b.INITIATOR,b.STATUS AS PROCESSSTATUS FROM vw_TASKS AS A left join INCIDENTS  AS B on A.Processname=B.Processname and A.INCIDENT=B.INCIDENT WHERE a.PROCESSNAME=@PROCESSNAME AND a.INCIDENT=@INCIDENT";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);

            List<TaskEntity> list = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
            if (list.Count > 0)
            {
                return list[0];
            }
            else
                return new TaskEntity();
        }

        //调用接口实现
        public virtual string GetTaskUrl(string taskID, string type, string loginName)
        {
            TaskEntity entity = new TaskEntity();
            if (taskID.StartsWith("S"))
            {
                entity = GetInitTaskEntity(taskID);
            }
            else
            {
                entity = GetTaskEntity(taskID);
            }
            string processName = "";
            string stepLabel = "";
            int incident = 0;
            if (entity == null)
            {

            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";

            url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
               + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;

            return url;
        }

        public virtual TaskEntity GetInitTaskEntity(string taskID)
        {
            return new TaskEntity();
            //return DataAccess.Instance(GetDBName()).GetEntity<TaskEntity>("TaskLogic_GetStartEntity", taskID);
        }


        //调用接口实现
        public virtual int SubmitTask(TaskEntity entity)
        {
            return 0;
        }



        //调用接口实现
        public virtual bool ReturnTask(TaskEntity entity)
        {
            return false;
        }

        //调用接口实现
        public virtual bool RejectTask(TaskEntity task)
        {
            return AbortIncident(task);
        }

        //调用接口实现
        public virtual bool AbortIncident(TaskEntity entity)
        {
            return false;
        }

        public virtual string GetCurrentApprover(string processName, int incident)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);

            String sql = "select STEPLABEL,ASSIGNEDTOUSER from vw_TASKS with(nolock) where processname=@PROCESSNAME and incident=@INCIDENT and status=1 and ORGANIZATION is not null";

            List<Approver> list = Ultimusdb.Database.SqlQuery<Approver>(sql, param).ToListAsync().Result;
            string str = "";
            if (list.Count > 0)
            {
                foreach (Approver dr in list)
                {
                    if (!String.IsNullOrEmpty(dr.ASSIGNEDTOUSER))
                    {
                        if (String.IsNullOrEmpty(str))
                        {
                            str += "[" + Convert.ToString(dr.STEPLABEL).Trim() + "]:" + Convert.ToString(dr.ASSIGNEDTOUSER).Replace("Porsche/", "").Trim() + ",";
                        }
                        else
                        {
                            str += Convert.ToString(dr.ASSIGNEDTOUSER).Replace("Porsche/", "").Trim() + ",";
                        }
                    }
                }

            }

            return str.TrimEnd(',');
        }

        public virtual string GetViewTaskId(string processName, int incident, string loginName)
        {
            object obj = null;
            if (string.IsNullOrEmpty(loginName))
            {
                //obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select taskid from tasks  with(nolock) where processname=@processname and incident=@incident ", processName, incident);
            }
            else
            {

                //obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select taskid from tasks  with(nolock) where processname=@processname and incident=@incident and assignedtouser=@assignedtouser ", processName, incident, loginName);
            }
            return Convert.ToString(obj);
        }

        public int GetProcessVersion(string processName)
        {
            object obj = null;

            //obj = DataAccess.Instance(GetDBName()).ExecuteScalar("select top 1 PROCESSVERSION from INITIATE  with(nolock) where processname=@processname  ", processName);

            return Convert.ToInt32(obj);
        }

        //调用接口实现
        public virtual byte[] GetGraphicalStatus(string processName, int incident)
        {
            return null;
        }

        public virtual int GetStepType(string taskID, string stepID)
        {
            //object obj = DataAccess.Instance(GetDBName()).GetObject("TaskLogic_GetStepType", stepID);
            //return Convert.ToInt32(obj);
            return 0;
        }

        //调用接口实现
        public virtual bool AssignTask(string taskId, string toUser)
        {
            return false;
        }

        //调用接口实现
        public virtual bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            return false;
        }

        //调用接口实现
        public virtual bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            return false;
        }

        //调用接口实现
        public virtual List<VarEntity> GetVariableList(string taskID)
        {
            return null;
        }


        //调用接口实现
        public virtual bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return false;
        }

        //调用接口实现
        public virtual bool DeleteTask(TaskEntity task)
        {
            return false;
        }

        ServerEntity _server = new ServerEntity();
        public ServerEntity GetServerEntity()
        {
            return _server;
        }

        public void SetServerEntity(ServerEntity entity)
        {
            _server = entity;
        }


        public TaskEntity GetInitTaskEntityByProcessName(string processName)
        {
            TaskEntity task = new TaskEntity();
            try
            {
                string sql = "SELECT top 1 INITIATEID AS TASKID,PROCESSNAME FROM INITIATE  with(nolock) WHERE PROCESSNAME=@PROCESSNAME Order by PROCESSVERSION desc";
                SqlParameter param = new SqlParameter("@PROCESSNAME", System.Data.SqlDbType.NVarChar, 50);
                param.Value = processName;
                task = Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result[0];
                task.SERVERNAME = GetServerEntity().SERVERNAME;

            }
            catch (Exception ex)
            {

            }
            return task;
        }

        public virtual void LogoutUser(string sessionId)
        {
        }

        public virtual List<TaskEntity> GetTaskList(string processName, int incident)
        {
            String sql = @"SELECT a.*,b.displayname as STEPLABELNAME,c.USERNAME+' / '+c.USERNAMECN as TASKUSERNAME, d.StatusEn as STATUSNAME
                          FROM vw_TASKS as a left join 
                          UltimusBiz.dbo.[WF_PROCESSSTEP] as b on a.StepLabel = b.STEPNAME and a.ProcessName=b.ProcessName Left join UltimusBiz.dbo.[ORG_USER] as c on REPLACE(a.ASSIGNEDTOUSER,'/','\') = c.LOGINNAME
                          LEFT JOIN UltimusBiz.dbo.[WF_PROCESSSTEPSSTATUS] as d on a.STATUS=d.Status
                          where a.PROCESSNAME=@PROCESSNAME
                          and a.INCIDENT=@INCIDENT and a.ORGANIZATION is not null
                          order by a.starttime";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);

            return Ultimusdb.Database.SqlQuery<TaskEntity>(sql, param).ToListAsync().Result;
        }

        public virtual int GetIncidentStatus(string strProcessName, int nIncidentNo)
        {
            return 0;
        }

        public virtual int DeleteIncident(string processName, int incident)
        {
            String sql = @"update INCIDENTS set Status=33 where PROCESSNAME=@PROCESSNAME
                          and INCIDENT=@INCIDENT";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PROCESSNAME", processName);
            param[1] = new SqlParameter("@INCIDENT", incident);

            return Ultimusdb.Database.ExecuteSqlCommandAsync(sql, param).Result;
        }
    }
}