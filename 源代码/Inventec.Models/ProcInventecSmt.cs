using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecSmt
    {
        public ProcInventecSmt()
        {
            this.proc_inventec_smt_dt1 = new List<ProcInventecSmtDt1>();
            this.proc_inventec_smt_dt2 = new List<ProcInventecSmtDt2>();
        }

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
      /// linetype
      /// </summary>
      [Display(Name = "linetype")]
       	   		public string linetype { get; set; }
      /// <summary>
      /// worktype
      /// </summary>
      [Display(Name = "worktype")]
       	   		public string worktype { get; set; }
      /// <summary>
      /// worktypeid
      /// </summary>
      [Display(Name = "worktypeid")]
       	   		public Nullable<int> worktypeid { get; set; }
      /// <summary>
      /// workcontent
      /// </summary>
      [Display(Name = "workcontent")]
       	   		public string workcontent { get; set; }
      /// <summary>
      /// engineerhandover
      /// </summary>
      [Display(Name = "engineerhandover")]
       	   		public string engineerhandover { get; set; }
        public virtual ICollection<ProcInventecSmtDt1> proc_inventec_smt_dt1 { get; set; }
        public virtual ICollection<ProcInventecSmtDt2> proc_inventec_smt_dt2 { get; set; }
    }
}

