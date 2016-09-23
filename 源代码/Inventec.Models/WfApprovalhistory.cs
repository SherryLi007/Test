using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfApprovalhistory
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// incident
      /// </summary>
      [Display(Name = "incident")]
       	   		public Nullable<int> incident { get; set; }
      /// <summary>
      /// stepname
      /// </summary>
      [Display(Name = "stepname")]
       	   		public string stepname { get; set; }
      /// <summary>
      /// displaystepname
      /// </summary>
      [Display(Name = "displaystepname")]
       	   		public string displaystepname { get; set; }
      /// <summary>
      /// approvername
      /// </summary>
      [Display(Name = "approvername")]
       	   		public string approvername { get; set; }
      /// <summary>
      /// approveraccount
      /// </summary>
      [Display(Name = "approveraccount")]
       	   		public string approveraccount { get; set; }
      /// <summary>
      /// action
      /// </summary>
      [Display(Name = "action")]
       	   		public string action { get; set; }
      /// <summary>
      /// comments
      /// </summary>
      [Display(Name = "comments")]
       	   		public string comments { get; set; }
      /// <summary>
      /// createdate
      /// </summary>
      [Display(Name = "createdate")]
       	   		public Nullable<System.DateTime> createdate { get; set; }
      /// <summary>
      /// enddate
      /// </summary>
      [Display(Name = "enddate")]
       	   		public Nullable<System.DateTime> enddate { get; set; }
      /// <summary>
      /// taskid
      /// </summary>
      [Display(Name = "taskid")]
       	   		public string taskid { get; set; }
      /// <summary>
      /// taskuser
      /// </summary>
      [Display(Name = "taskuser")]
       	   		public string taskuser { get; set; }
      /// <summary>
      /// assigendtouser
      /// </summary>
      [Display(Name = "assigendtouser")]
       	   		public string assigendtouser { get; set; }
      /// <summary>
      /// status
      /// </summary>
      [Display(Name = "status")]
       	   		public Nullable<int> status { get; set; }
      /// <summary>
      /// childprocessname
      /// </summary>
      [Display(Name = "childprocessname")]
       	   		public string childprocessname { get; set; }
      /// <summary>
      /// childincident
      /// </summary>
      [Display(Name = "childincident")]
       	   		public Nullable<int> childincident { get; set; }
      /// <summary>
      /// ext01
      /// </summary>
      [Display(Name = "ext01")]
       	   		public string ext01 { get; set; }
      /// <summary>
      /// ext02
      /// </summary>
      [Display(Name = "ext02")]
       	   		public string ext02 { get; set; }
      /// <summary>
      /// ext03
      /// </summary>
      [Display(Name = "ext03")]
       	   		public string ext03 { get; set; }
      /// <summary>
      /// ext04
      /// </summary>
      [Display(Name = "ext04")]
       	   		public string ext04 { get; set; }
      /// <summary>
      /// ext05
      /// </summary>
      [Display(Name = "ext05")]
       	   		public string ext05 { get; set; }
    }
}

