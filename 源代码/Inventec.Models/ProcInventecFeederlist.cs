using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecFeederlist
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
      /// status
      /// </summary>
      [Display(Name = "status")]
       	   		public string status { get; set; }
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
      /// subject
      /// </summary>
      [Display(Name = "subject")]
       	   		public string subject { get; set; }
      /// <summary>
      /// rev
      /// </summary>
      [Display(Name = "rev")]
       	   		public string rev { get; set; }
      /// <summary>
      /// model
      /// </summary>
      [Display(Name = "model")]
       	   		public string model { get; set; }
      /// <summary>
      /// mctype
      /// </summary>
      [Display(Name = "mctype")]
       	   		public string mctype { get; set; }
      /// <summary>
      /// pn
      /// </summary>
      [Display(Name = "pn")]
       	   		public string pn { get; set; }
      /// <summary>
      /// pcbno
      /// </summary>
      [Display(Name = "pcbno")]
       	   		public string pcbno { get; set; }
      /// <summary>
      /// mode
      /// </summary>
      [Display(Name = "mode")]
       	   		public string mode { get; set; }
      /// <summary>
      /// pcbrev
      /// </summary>
      [Display(Name = "pcbrev")]
       	   		public string pcbrev { get; set; }
      /// <summary>
      /// array
      /// </summary>
      [Display(Name = "array")]
       	   		public string array { get; set; }
    }
}

