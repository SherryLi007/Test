using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Inventec.Ultimus.Entity
{
    public class TaskEntity : IComparable
    {
        string taskID;

        public string TASKID
        {
            get { return trimString(taskID); }
            set { taskID = value; }
        }

        string _processName;

        public string PROCESSNAME
        {
            get { return trimString(_processName); }
            set { _processName = value; }
        }

        string _processNameEn;

        public string PROCESSNAMEEN
        {
            get { return trimString(_processNameEn); }
            set { _processNameEn = value; }
        }

        string _processNameCN;

        public string PROCESSNAMECN
        {
            get { return trimString(_processNameCN); }
            set { _processNameCN = value; }
        }

        string _formNo;

        public string FORMNO
        {
            get { return trimString(_formNo); }
            set { _formNo = value; }
        }

        int _incident;

        public int INCIDENT
        {
            get { return _incident; }
            set { _incident = value; }
        }

        string _summary = "";

        public string SUMMARY
        {
            get
            {

                return trimString(_summary);
            }
            set { _summary = value; }
        }

        string _sourceTaskID = "";

        public string SourceTaskID
        {
            get
            {
                if (!String.IsNullOrEmpty(_summary) && _summary.IndexOf(",") > 0)
                {
                    string[] sz = Convert.ToString(_summary).Split(',');
                    if (sz.Length == 3)
                    {
                        _sourceTaskID = sz[2].Replace("]", "");
                    }
                }
                return _sourceTaskID;
            }
            set { _sourceTaskID = value; }
        }

        string _displaySummary = "";

        public string DisplaySummary
        {
            get
            {
                _displaySummary = SUMMARY;
                if (!String.IsNullOrEmpty(_displaySummary) && _displaySummary.IndexOf(",") > 0)
                {
                    string[] sz = Convert.ToString(_summary).Split(',');
                    if (sz.Length == 3)
                    {
                        _displaySummary = sz[0] + "," + sz[1] + "]";
                    }
                }
                return _displaySummary;
            }
            set { _displaySummary = value; }
        }

        string _HELPURL;

        public string HELPURL
        {
            get { return _HELPURL; }
            set { _HELPURL = value; }
        }

        string _stepLabel;

        public string STEPLABEL
        {
            get { return trimString(_stepLabel); }
            set { _stepLabel = value; }
        }

        string _stepLabelName;

        public string STEPLABELNAME
        {
            get { return trimString(_stepLabelName); }
            set { _stepLabelName = value; }
        }


        bool _SYNC = true;

        public bool SYNC
        {
            get { return _SYNC; }
            set { _SYNC = value; }
        }

        string _stepID;

        public string STEPID
        {
            get { return trimString(_stepID); }
            set { _stepID = value; }
        }

        int? _status;

        public int? STATUS
        {
            get { return _status; }
            set { _status = value; }
        }

        string _statusname;

        public string STATUSNAME
        {
            get { return trimString(_statusname); }
            set { _statusname = value; }
        }

        int _processStatus;

        public int PROCESSSTATUS
        {
            get { return _processStatus; }
            set { _processStatus = value; }
        }


        int? _subStatus;

        public int? SUBSTATUS
        {
            get { return _subStatus; }
            set { _subStatus = value; }
        }

        DateTime? _startTime;

        public DateTime? STARTTIME
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        DateTime? _endTime;

        public DateTime? ENDTIME
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        DateTime? _overdueTime;

        public DateTime? OVERDUETIME
        {
            get { return _overdueTime; }
            set { _overdueTime = value; }
        }

        string _taskUser;

        public string TASKUSER
        {
            get { return trimString(_taskUser); }
            set { _taskUser = value; }
        }

        string _taskUserName;

        public string TASKUSERNAME
        {
            get { return trimString(_taskUserName); }
            set { _taskUserName = value; }
        }


        string _assignedtoUser;

        public string ASSIGNEDTOUSER
        {
            get { return trimString(_assignedtoUser); }
            set { _assignedtoUser = value; }
        }

        string _INITIATOR;

        public string INITIATOR
        {
            get { return _INITIATOR; }
            set { _INITIATOR = value; }
        }

        string _APLICANT;

        public string APLICANT
        {
            get { return trimString(_APLICANT); }
            set { _APLICANT = value; }
        }

        string _DEPARTMENT;

        public string DEPARTMENT
        {
            get { return trimString(_DEPARTMENT); }
            set { _DEPARTMENT = value; }
        }


        string _ServiceName;
        /// <summary>
        /// 数据库表 COM_APPSETTINGS 中 Name 配置项值，对应各个版本所在服务器的名称。
        /// </summary>
        public string SERVERNAME
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }

        private string _SERVERTASKID;

        public string SERVERTASKID
        {
            get
            {
                if (string.IsNullOrEmpty(_SERVERTASKID))
                {
                    _SERVERTASKID = SERVERNAME + "_" + TASKID;
                }
                return _SERVERTASKID;
            }
            set { _SERVERTASKID = value; }
        }

        string _FORMURL;

        public string FORMURL
        {
            get
            {
                if (string.IsNullOrEmpty(_FORMURL))
                {
                    _FORMURL = ConfigurationManager.AppSettings["RootPath"] + "/" + PROCESSNAME.Replace("PROC_", "Form") + "/Index?taskid=" + PROCESSNAME + "&ServerName=" + SERVERNAME;
                }
                return _FORMURL;
            }
            set { _FORMURL = value; }
        }

        string _APPROVEURL;

        public string APPROVEURL
        {
            get
            {
                if (string.IsNullOrEmpty(_APPROVEURL))
                {
                    _APPROVEURL = ConfigurationManager.AppSettings["RootPath"] + "/" + PROCESSNAME.Replace("PROC_", "Form") + "/Approval?taskid=" + INCIDENT + "&ServerName=" + SERVERNAME;
                }
                return _APPROVEURL.Replace("Wf", "");
            }
            set { _APPROVEURL = value; }
        }

        string _ERRORMESSAGE;

        public string ERRORMESSAGE
        {
            get { return _ERRORMESSAGE; }
            set { _ERRORMESSAGE = value; }
        }

        List<VarEntity> _vars = new List<VarEntity>();

        public List<VarEntity> VarList
        {
            get { return _vars; }
            set { _vars = value; }
        }

        string _REASON;

        public string REASON
        {
            get { return _REASON; }
            set { _REASON = value; }
        }

        string _COMMENTS;

        public string COMMENTS
        {
            get { return _COMMENTS; }
            set { _COMMENTS = value; }
        }


        string _TYPE;

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        private string trimString(string val)
        {
            if (val != null) return val.Trim();
            return val;
        }

        public int CompareTo(object obj)
        {
            TaskEntity pe = obj as TaskEntity;
            return string.Compare(this.PROCESSNAME, pe.PROCESSNAME);
        }

        bool? _IsUrgent;
        public bool? ISURGENT
        {
            get { return _IsUrgent; }
            set { _IsUrgent = value; }
        }

        decimal? _FormAmount;
        public decimal? FORMAMOUNT
        {
            get { return _FormAmount; }
            set { _FormAmount = value; }
        }

        DateTime? _invoiceDate;

        public DateTime? INVOICEDATE
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }

        Int16? _paymentStatus;

        public Int16? PAYMENTSTATUS
        {
            get { return _paymentStatus; }
            set { _paymentStatus = value; }
        }

        //voucherno
        string voucherno;
        public string VoucherNo
        {
            get { return voucherno; }
            set { voucherno = value; }
        }

        bool? isDownload;
        public bool? IsDownload
        {
            get { return isDownload; }
            set { isDownload = value; }
        }

    }
}
