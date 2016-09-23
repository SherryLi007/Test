using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Drawing;
using System.Threading;
using Inventec.Ultimus.Entity;
using Ultimus.WFServer;
using Ultimus.ClientServices;
using System.Configuration;
using Ultimus.OC;
using Ultimus.Configuration;
using Ultimus.UWF.Workflow.Logic;

namespace Inventec.Ultimus.Implementation
{
    public class UltimusTask : DatabaseTask
    {
        public override List<TaskEntity> GetInitTaskList(string loginName, string str)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            TasklistFilter filter = new TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Tasklist tl = new Tasklist();
            filter.nFiltersMask = Filters.nFilter_Initiate;
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(str))
            {
                string[] sz = str.Split(',');
                foreach (string ss in sz)
                {
                    list.Add(ss);
                }
            }
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                if (list.Count > 0)
                {
                    if (list.Exists(p => tl.GetAt(i).strProcessName.ToUpper().Trim().IndexOf(p.ToUpper().Trim()) >= 0))
                    {
                        TaskEntity task = new TaskEntity();
                        task.PROCESSNAME = tl.GetAt(i).strProcessName;
                        task.TASKID = tl.GetAt(i).strTaskId;
                        task.SUMMARY = tl.GetAt(i).strSummary;
                        task.HELPURL = tl.GetAt(i).strHelpUrl;
                        if (string.IsNullOrEmpty(task.SUMMARY))
                        {
                            initProcessList.Add(task);
                        }
                        task.SERVERNAME = GetServerEntity().SERVERNAME;
                    }
                }
                else
                {
                    TaskEntity task = new TaskEntity();
                    task.PROCESSNAME = tl.GetAt(i).strProcessName;
                    task.TASKID = tl.GetAt(i).strTaskId;
                    task.SUMMARY = tl.GetAt(i).strSummary;
                    task.HELPURL = tl.GetAt(i).strHelpUrl;
                    if (string.IsNullOrEmpty(task.SUMMARY))
                    {
                        initProcessList.Add(task);
                    }
                    task.SERVERNAME = GetServerEntity().SERVERNAME;
                }
            }
            return initProcessList;
        }


        public override List<TaskEntity> GetDraftTaskList(string loginName, string str)
        {
            List<TaskEntity> initProcessList = new List<TaskEntity>();
            //load init process
            TasklistFilter filter = new TasklistFilter();
            filter.strArrUserName = new string[1] { loginName };
            Tasklist tl = new Tasklist();
            filter.nFiltersMask = Filters.nFilter_Initiate;
            string error = "";
            tl.LoadFilteredTasks(filter, out error);
            for (int i = 0; i < tl.GetTasksCount(); i++)
            {
                TaskEntity task = new TaskEntity();
                task.PROCESSNAME = tl.GetAt(i).strProcessName;
                task.TASKID = tl.GetAt(i).strTaskId;
                task.SUMMARY = tl.GetAt(i).strSummary;
                task.HELPURL = tl.GetAt(i).strHelpUrl;
                if (!string.IsNullOrEmpty(task.SUMMARY))
                {
                    initProcessList.Add(task);
                }
                task.SERVERNAME = GetServerEntity().SERVERNAME;
            }
            return initProcessList;
        }

        public override string GetTaskUrl(string taskID, string type, string loginName)
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
            int incident;
            if (entity == null) //表里面没有该Task，从EIK中拿
            {
                Task task = new Task();
                task.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                processName = task.strProcessName.Trim();
                stepLabel = task.strStepName.Trim();
                incident = task.nIncidentNo;
            }
            else
            {
                processName = entity.PROCESSNAME.Trim();
                stepLabel = entity.STEPLABEL.Trim();
                incident = entity.INCIDENT;
                if (string.IsNullOrEmpty(loginName))
                {
                    loginName = entity.ASSIGNEDTOUSER;
                }
            }

            string page = StepSettingsLogic.GetStepPage(processName, stepLabel);
            string url = "";
            if (string.IsNullOrEmpty(page)) //Standard Form
            {
                string result = "";
                Task t = new Task();
                t.InitializeFromTaskId(loginName, taskID);
                t.ExtractFormURL(out result);
                if (!string.IsNullOrEmpty(result)) //EIK没有调用到该task
                {
                    if (result.StartsWith("."))
                    {
                        result = result.Replace("./", "");
                    }
                    url = GetStandardClientUrl(result);
                    Services srv = new Services();
                    string sessionid = "";
                    if (type.StartsWith("view"))
                    {
                        sessionid = type.Replace("view", "").Replace("sid=", "");
                    }
                    string error = "";
                    if (string.IsNullOrEmpty(sessionid))
                    {
                        srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                        try
                        {
                            t.CheckOutTask(loginName, out error);
                            error = "";
                        }
                        catch
                        {
                        }
                    }


                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception("Ultimus Login Error:" + error);
                    }
                    url += "&sid=" + sessionid;
                    //HttpContext.Current.Response.AddHeader("Ultimus Workflow1", "Ultimus Workflow");
                    //HttpContext.Current.Response.Cookies["TaskID"].Value = taskID;
                    //HttpContext.Current.Response.Cookies["TaskID"].Path = @"/";
                    //HttpContext.Current.Response.Cookies["UserID"].Value = userName;
                    //HttpContext.Current.Response.Cookies["UserID"].Path = @"/";
                }
                else
                {
                    if (entity != null) //有这个task,把该task再插入回来
                    {
                        InsertBackFromArchive(entity.PROCESSNAME, entity.INCIDENT);
                        t.InitializeFromTaskId(loginName.Replace("\\", "/"), taskID);
                        t.ExtractFormURL(out result);
                        if (!string.IsNullOrEmpty(result))
                        {
                            if (result.StartsWith("."))
                            {
                                result = result.Replace("./", "");
                            }
                            url = GetStandardClientUrl(result);
                            Services srv = new Services();
                            string sessionid = "";
                            string error = "";
                            srv.LoginUser(loginName.Split('/')[0], loginName.Split('/')[1], "", out sessionid, out error);
                            if (!string.IsNullOrEmpty(error))
                            {
                                throw new Exception("Ultimus Login Error:" + error);
                            }
                            url += "&sid=" + sessionid;
                        }
                    }
                    else
                    {
                        throw new Exception("OpenForm_CannotLoadTask");
                    }
                }
            }
            else //.net Form
            {
                url = "http://" + HttpContext.Current.Request.Url.Host + ":"
                    + HttpContext.Current.Request.Url.Port + "/" + page + "?ProcessName=" + processName.Trim() + "&StepName=" + stepLabel.Trim() + "&Incident="
                   + incident + "&TaskID=" + taskID.Trim() + "&UserName=" + HttpContext.Current.Server.UrlEncode(loginName) + "&Type=" + type;
            }
            return url;
        }



        public void InsertBackFromArchive(string processName, int incident)
        {
            //string archiveDBName = ConfigurationManager.AppSettings["ArchiveDBName"];
            //if (!string.IsNullOrEmpty(archiveDBName))
            //{
            //    Hashtable table = new Hashtable();
            //    table.Add("processName", processName);
            //    table.Add("incident", incident);
            //    table.Add("ArchiveDBName", archiveDBName);
            //    if (DataAccess.Instance("UltDB").SqlMapper.DataSource.DbProvider.Name.ToUpper().IndexOf("ORACLE") >= 0)
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchiveOracle", table);
            //    }
            //    else
            //    {
            //        DataAccess.Instance("UltDB").Insert("TaskLogic_InsertBackFromArchive", table);
            //    }
            //}
        }

        string GetStandardClientUrl(string pUrl)
        {
            string url = ConfigurationManager.AppSettings["StandardClientUrl"];
            if (string.IsNullOrEmpty(url))
            {
                string ServerName = HttpContext.Current.Request.Url.Host;

                string URL = "http://" + ServerName + "/Ultweb/" + pUrl;
                return URL;
            }
            else
            {
                return url + pUrl;
            }
        }

        public override int SubmitTask(TaskEntity entity)
        {
            Task task = new Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            int incident = 0;
            if (flag)
            {
                if (entity.VarList != null)
                {
                    SetVariables(task, entity.VarList);
                }
                if (!entity.SYNC)
                {
                    incident = -1;
                }
                if (string.IsNullOrEmpty(entity.SUMMARY))
                {
                    entity.SUMMARY = task.strSummary;
                }
                if (!task.SendFrom(entity.ASSIGNEDTOUSER.Replace("\\", "/"), "", entity.SUMMARY, false, ref incident, out strError))
                {
                    if (string.IsNullOrEmpty(strError) || incident == 0)
                    {
                        incident = 0;
                        strError = "请稍候从草稿箱打开，重新提交！";
                    }
                }
            }
            return incident;
        }

        string SetVariables(Task task, List<VarEntity> vars)
        {
            string error = "";
            string lasterror = "";
            if (vars != null)
            {
                foreach (VarEntity ety in vars) //USER:org=Business Organization,user=WIN-O7BVH1JUCSN/Administrator
                {
                    if (ety.Value != null)
                    {
                        // modify by sky 2013/12/5 添加用户赋值
                        string varValue;
                        varValue = ety.Value.ToString();
                        object val = new object[] { varValue };

                        //_----------------------------------------------
                        string value = varValue;
                        string[] sz = value.Split('|');

                        string etyName = ety.Name.ToString();
                        if (etyName.IndexOf("TaskData.") != 0 && etyName.IndexOf("IncidentData.") != 0)
                        {
                            etyName = "TaskData.Global." + etyName;
                        }
                        for (int i = 0; i < sz.Length; i++)
                        {
                            if (sz[i].ToString().Contains("[USER]"))
                            {
                                string org = "Porsche";
                                sz[i] = "USER:org=" + org + ",user=" + sz[i].ToString().Replace("[USER]", "");
                            }
                        }

                        if (sz.Length > 1)
                        {
                            task.SetNodeValue(etyName, sz, out error);
                        }
                        else
                        {
                            task.SetNodeValue(etyName, sz[0], out error);
                        }
                        if (!string.IsNullOrEmpty(error))
                        {
                            lasterror = error;
                        }
                    }

                }
            }
            return lasterror;
        }

        public override bool ReturnTask(TaskEntity entity)
        {
            Task task = new Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string strError = "";
            if (flag)
            {
                SetVariables(task, entity.VarList);
                if (string.IsNullOrEmpty(entity.SUMMARY))
                {
                    entity.SUMMARY = task.strSummary;
                }
                return task.Return(entity.REASON, entity.SUMMARY, false, out strError);
            }
            return false;
        }

        public override bool RejectTask(TaskEntity entity)
        {
            Task task = new Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string userName = entity.ASSIGNEDTOUSER.Replace("\\", "/");
            string strError = "";
            if (flag)
            {
                Incident incident = new Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                if (task.AbortStep(out strError))
                {
                    //return incident.DirectActivateStep("Begin");
                    string initName = entity.INITIATOR.Replace("\\", "/").Trim();
                    if (entity.PROCESSNAME == task.strProcessName)
                    {
                        return incident.AbortIncident(initName, entity.REASON, out strError);
                    }
                    else
                    {
                        Incident mainIncident = new Incident();
                        mainIncident.LoadIncident(entity.PROCESSNAME, entity.INCIDENT);
                        if (incident.AbortIncident(initName, entity.REASON, out strError))
                        {
                            return mainIncident.AbortIncident(initName, entity.REASON, out strError);
                        }
                        else
                            return false;
                    }                    
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetIncidentStatus(string strProcessName, int nIncidentNo)
        {
            try
            {
                Incident incident = new Incident();
                incident.LoadIncident(strProcessName, nIncidentNo);
                Incident.Status objStatus = new Incident.Status();
                if (incident.GetIncidentStatus(out objStatus))
                {
                    return objStatus.nIncidentStatus;
                }
                else
                    return 0;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public override bool AbortIncident(TaskEntity entity)
        {
            Task task = new Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            string userName = entity.INITIATOR.Replace("\\", "/").Trim();
            string strError = "";
            if (flag)
            {
                Incident incident = new Incident();
                incident.LoadIncident(task.strProcessName, task.nIncidentNo);
                if (entity.PROCESSNAME == task.strProcessName)
                {
                    return incident.AbortIncident(userName, entity.REASON, out strError);
                }
                else
                {
                    Incident mainIncident = new Incident();
                    mainIncident.LoadIncident(entity.PROCESSNAME, entity.INCIDENT);
                    if (incident.AbortIncident(userName, entity.REASON, out strError))
                    {
                        return mainIncident.AbortIncident(userName, entity.REASON, out strError);
                    }
                    else
                        return false;
                }      
            }
            return false;
        }

        public override bool DeleteTask(TaskEntity entity)
        {
            Task task = new Task();
            bool flag = task.InitializeFromTaskId(entity.ASSIGNEDTOUSER.Replace("\\", "/"), entity.TASKID);
            if (flag)
            {
                return task.DeleteTask();
            }
            return false;
        }

        public override byte[] GetGraphicalStatus(string processName, int incident)
        {
            byte[] bytesGif;
            HistoryPlayback hpb = new HistoryPlayback();
            string error = "";
            if (incident <= 0)
            {
                incident = 1;
            }
            hpb.LoadHistoryEvents(processName, incident, out error);
            if (!string.IsNullOrEmpty(error))
            {
                Incident.Status pstatus = new Incident.Status();
                Incident pincident = new Incident();

                if (incident <= 0)
                {
                    int version = GetProcessVersion(processName);
                    pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif); //取3次，防止取不到图
                    if (bytesGif == null)
                    {
                        pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                        Thread.Sleep(500);
                        if (bytesGif == null)
                        {
                            pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                            Thread.Sleep(500);
                            if (bytesGif == null)
                            {
                                pstatus.GetGraphicalStatus(processName, incident, version, out bytesGif);
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
                else
                {
                    pincident.LoadIncident(processName, incident);
                    pincident.GetIncidentStatus(out pstatus);
                    pstatus.GetGraphicalStatus(pincident.strProcessName, pincident.nIncidentNo, pincident.nVersion, out bytesGif);
                }
                return bytesGif;
            }
            Bitmap bmp = hpb.GetEventImage();
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytesGif = stream.GetBuffer();
            stream.Close();
            stream = null;
            bmp.Dispose();
            bmp = null;
            return bytesGif;

        }

        public override bool AssignTask(string taskId, string toUser)
        {
            Task pTask = new Task();
            pTask.InitializeFromTaskId(taskId);
            return pTask.AssignTask(toUser);
        }

        public override bool AssignAllCurrentTasks(string fromUser, string toUser)
        {
            User pUserTask = null;
            OrgChart porg = new OrgChart();
            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllCurrentTasks(toUser);
        }

        public override bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate)
        {
            User pUserTask = null;
            OrgChart porg = new OrgChart();
            if (fromUser.StartsWith("Ultimus"))
            {
                porg = new OrgChart("Ultimus");
            }

            porg.FindUser(fromUser, "", "", out pUserTask);
            return pUserTask.AssignAllFutureTasks(toUser, toDate.ToOADate());
        }

        public override bool AssignProcessFutureTasks(string processName, string stepName, string fromUser, string toUser, DateTime toDate)
        {
            return base.AssignProcessFutureTasks(processName, stepName, fromUser, toUser, toDate);
        }

        public override void LogoutUser(string sessionId)
        {
            Services srv = new Services();
            string error = "";
            srv.LogoutUser(sessionId, out error);
        }
    }
}