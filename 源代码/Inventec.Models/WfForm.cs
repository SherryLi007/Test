using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfForm
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public long id { get; set; }
      /// <summary>
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// formno
      /// </summary>
      [Display(Name = "formno")]
       	   		public string formno { get; set; }
      /// <summary>
      /// formamount
      /// </summary>
      [Display(Name = "formamount")]
       	   		public Nullable<decimal> formamount { get; set; }
      /// <summary>
      /// incident
      /// </summary>
      [Display(Name = "incident")]
       	   		public Nullable<int> incident { get; set; }
      /// <summary>
      /// priority
      /// </summary>
      [Display(Name = "priority")]
       	   		public Nullable<int> priority { get; set; }
      /// <summary>
      /// summary
      /// </summary>
      [Display(Name = "summary")]
       	   		public string summary { get; set; }
      /// <summary>
      /// starttime
      /// </summary>
      [Display(Name = "starttime")]
       	   		public Nullable<System.DateTime> starttime { get; set; }
      /// <summary>
      /// endtime
      /// </summary>
      [Display(Name = "endtime")]
       	   		public Nullable<System.DateTime> endtime { get; set; }
      /// <summary>
      /// 所属公司
      /// </summary>
      [Display(Name = "所属公司")]
       	   		public string companycode { get; set; }
      /// <summary>
      /// 部门
      /// </summary>
      [Display(Name = "部门")]
       	   		public string dept { get; set; }
      /// <summary>
      /// 成本中心
      /// </summary>
      [Display(Name = "成本中心")]
       	   		public string costcenter { get; set; }
      /// <summary>
      /// status
      /// </summary>
      [Display(Name = "status")]
       	   		public Nullable<int> status { get; set; }
      /// <summary>
      /// initiator
      /// </summary>
      [Display(Name = "initiator")]
       	   		public string initiator { get; set; }
      /// <summary>
      /// agent
      /// </summary>
      [Display(Name = "agent")]
       	   		public string agent { get; set; }
      /// <summary>
      /// approver
      /// </summary>
      [Display(Name = "approver")]
       	   		public string approver { get; set; }
      /// <summary>
      /// isurgent
      /// </summary>
      [Display(Name = "isurgent")]
       	   		public Nullable<bool> isurgent { get; set; }
      /// <summary>
      /// paymentstatus
      /// </summary>
      [Display(Name = "paymentstatus")]
       	   		public Nullable<short> paymentstatus { get; set; }
      /// <summary>
      /// invoicedate
      /// </summary>
      [Display(Name = "invoicedate")]
       	   		public Nullable<System.DateTime> invoicedate { get; set; }
      /// <summary>
      /// paymentdate
      /// </summary>
      [Display(Name = "paymentdate")]
       	   		public Nullable<System.DateTime> paymentdate { get; set; }
      /// <summary>
      /// voucherno
      /// </summary>
      [Display(Name = "voucherno")]
       	   		public string voucherno { get; set; }
      /// <summary>
      /// isdownload
      /// </summary>
      [Display(Name = "isdownload")]
       	   		public Nullable<bool> isdownload { get; set; }
    }
}

