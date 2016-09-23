using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecMfg
    {
        public ProcInventecMfg()
        {
            this.proc_inventec_mfg_dt = new List<ProcInventecMfgDt>();
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
      /// 线别
      /// </summary>
      [Display(Name = "线别")]
       	   		public string linetype { get; set; }
      /// <summary>
      /// 班别
      /// </summary>
      [Display(Name = "班别")]
       	   		public string worktype { get; set; }
      /// <summary>
      /// worktypeid
      /// </summary>
      [Display(Name = "worktypeid")]
       	   		public Nullable<int> worktypeid { get; set; }
      /// <summary>
      /// pcapn
      /// </summary>
      [Display(Name = "pcapn")]
       	   		public string pcapn { get; set; }
      /// <summary>
      /// 使用时机
      /// </summary>
      [Display(Name = "使用时机")]
       	   		public string usetimingid { get; set; }
      /// <summary>
      /// 站别
      /// </summary>
      [Display(Name = "站别")]
       	   		public string sitetype { get; set; }
      /// <summary>
      /// sitetypeid
      /// </summary>
      [Display(Name = "sitetypeid")]
       	   		public Nullable<int> sitetypeid { get; set; }
        public virtual ICollection<ProcInventecMfgDt> proc_inventec_mfg_dt { get; set; }
    }
}

