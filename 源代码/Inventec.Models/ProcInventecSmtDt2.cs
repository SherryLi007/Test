using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecSmtDt2
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
	   	   		public int formid { get; set; }
      /// <summary>
      /// rowid
      /// </summary>
      [Display(Name = "rowid")]
       	   		public Nullable<int> rowid { get; set; }
      /// <summary>
      /// plannedoutput
      /// </summary>
      [Display(Name = "plannedoutput")]
       	   		public string plannedoutput { get; set; }
      /// <summary>
      /// realoutput
      /// </summary>
      [Display(Name = "realoutput")]
       	   		public string realoutput { get; set; }
      /// <summary>
      /// badnumber
      /// </summary>
      [Display(Name = "badnumber")]
       	   		public string badnumber { get; set; }
      /// <summary>
      /// yieldrate
      /// </summary>
      [Display(Name = "yieldrate")]
       	   		public string yieldrate { get; set; }
      /// <summary>
      /// throwingrate
      /// </summary>
      [Display(Name = "throwingrate")]
       	   		public string throwingrate { get; set; }
      /// <summary>
      /// operationdeclaration
      /// </summary>
      [Display(Name = "operationdeclaration")]
       	   		public string operationdeclaration { get; set; }
        public virtual ProcInventecSmt proc_inventec_smt { get; set; }
    }
}

