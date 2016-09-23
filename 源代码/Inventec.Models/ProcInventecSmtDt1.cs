using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecSmtDt1
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
      /// machinetype
      /// </summary>
      [Display(Name = "machinetype")]
       	   		public string machinetype { get; set; }
      /// <summary>
      /// machinetypeid
      /// </summary>
      [Display(Name = "machinetypeid")]
       	   		public Nullable<int> machinetypeid { get; set; }
      /// <summary>
      /// machinecapacity
      /// </summary>
      [Display(Name = "machinecapacity")]
       	   		public string machinecapacity { get; set; }
      /// <summary>
      /// yieldratea
      /// </summary>
      [Display(Name = "yieldratea")]
       	   		public string yieldratea { get; set; }
      /// <summary>
      /// yieldrateb
      /// </summary>
      [Display(Name = "yieldrateb")]
       	   		public string yieldrateb { get; set; }
      /// <summary>
      /// totaldowntime
      /// </summary>
      [Display(Name = "totaldowntime")]
       	   		public string totaldowntime { get; set; }
        public virtual ProcInventecSmt proc_inventec_smt { get; set; }
    }
}

