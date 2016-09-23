using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecSocket
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// formid
      /// </summary>
      [Display(Name = "formid")]
              [Required(ErrorMessage = "*")]
	   	   		public System.Guid formid { get; set; }
      /// <summary>
      /// incident
      /// </summary>
      [Display(Name = "incident")]
       	   		public Nullable<int> incident { get; set; }
      /// <summary>
      /// statuss
      /// </summary>
      [Display(Name = "statuss")]
       	   		public string statuss { get; set; }
      /// <summary>
      /// formno
      /// </summary>
      [Display(Name = "formno")]
       	   		public string formno { get; set; }
      /// <summary>
      /// department
      /// </summary>
      [Display(Name = "department")]
       	   		public string department { get; set; }
      /// <summary>
      /// departmentid
      /// </summary>
      [Display(Name = "departmentid")]
       	   		public Nullable<int> departmentid { get; set; }
      /// <summary>
      /// factoryname
      /// </summary>
      [Display(Name = "factoryname")]
       	   		public string factoryname { get; set; }
      /// <summary>
      /// applicationdate
      /// </summary>
      [Display(Name = "applicationdate")]
       	   		public Nullable<System.DateTime> applicationdate { get; set; }
      /// <summary>
      /// applicantpeople
      /// </summary>
      [Display(Name = "applicantpeople")]
       	   		public string applicantpeople { get; set; }
      /// <summary>
      /// employeenumber
      /// </summary>
      [Display(Name = "employeenumber")]
       	   		public string employeenumber { get; set; }
      /// <summary>
      /// pcasmtid
      /// </summary>
      [Display(Name = "pcasmtid")]
       	   		public Nullable<int> pcasmtid { get; set; }
      /// <summary>
      /// 线别
      /// </summary>
      [Display(Name = "线别")]
       	   		public Nullable<int> linetypeid { get; set; }
      /// <summary>
      /// 点检人
      /// </summary>
      [Display(Name = "点检人")]
       	   		public string tally { get; set; }
    }
}

