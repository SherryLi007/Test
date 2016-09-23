using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Inventec.Ultimus.Entity;
using System.Collections;


namespace Inventec.Ultimus.Interface
{
    public interface ITask
    {
        /// <summary>
        /// 获取可发起的流程
        /// </summary>
        /// <param name="loginName">登录账户</param>
        /// <returns></returns>
        List<TaskEntity> GetInitTaskList(string loginName, string filter);
        List<TaskEntity> GetDraftTaskList(string loginName, string filter);

        List<TaskEntity> GetMyTaskList(string loginName,string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults);
        int GetMyTaskCount(string loginName, string filter, List<ParameterEntity> paras);

        List<TaskEntity> GetMyApprovalList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults);
        int GetMyApprovalCount(string loginName, string filter, List<ParameterEntity> paras);

        List<TaskEntity> GetMyRequestList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults);
        int GetMyRequestCount(string loginName, string filter, List<ParameterEntity> paras);

        List<TaskEntity> GetTaskList(string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults);
        int GetTaskListCount(string filter, List<ParameterEntity> paras);

        TaskEntity GetTaskEntity(string taskID);
        TaskEntity GetInitTaskEntity(string taskID);

        TaskEntity GetTaskEntityByName(string processName, int incident, string loginName);

        TaskEntity GetTaskEntityByName(string processName, int incident);
        TaskEntity GetIncidentEntityByName(string processName, int incident);
        string GetTaskUrl(string taskID, string type, string loginName);

        int SubmitTask(TaskEntity task);

        bool ReturnTask(TaskEntity task);

        bool RejectTask(TaskEntity task);

        bool AbortIncident(TaskEntity task);

        string GetCurrentApprover(string processName, int incident);
        /// <summary>
        /// 获取查看表单的TaskId
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="incident"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        string GetViewTaskId(string processName, int incident, string loginName);

        List<TaskEntity> GetTaskList(string processName, int incident);

        byte[] GetGraphicalStatus(string processName, int incident);

        int GetStepType(string taskID,string stepID);

        /// <summary>
        /// 指派，把当前Task指派给ToUser
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="toUser"></param>
        /// <returns></returns>
        bool AssignTask(string taskId, string toUser);

        bool DeleteTask(TaskEntity task);

        /// <summary>
        /// 把当前fromUser 的流程指派给 toUser
        /// </summary>
        /// <param name="fromUser"></param>
        /// <param name="toUser"></param>
        /// <returns></returns>
        bool AssignAllCurrentTasks(string fromUser, string toUser);

        /// <summary>
        /// 把所有fromUser 的流程指派给 toUser
        /// </summary>
        /// <param name="fromUser"></param>
        /// <param name="toUser"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        bool AssignAllFutureTasks(string fromUser, string toUser, DateTime toDate);
        bool AssignProcessFutureTasks(string processName,string stepName, string fromUser, string toUser, DateTime toDate);

        List<VarEntity> GetVariableList(string taskID);

        TaskEntity GetInitTaskEntityByProcessName(string processName);

        ServerEntity GetServerEntity ();
        void SetServerEntity(ServerEntity entity);

        void LogoutUser(string sessionId);

        int GetIncidentStatus(string strProcessName, int nIncidentNo);

        int DeleteIncident(string processName, int incident);

        List<TaskEntity> GetMyBusinessDataList(string loginName, string filter, List<ParameterEntity> paras, string sort, int skipResults, int maxResults);

    }
}
