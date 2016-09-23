using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecPcbaDt
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
      /// inspectiontype
      /// </summary>
      [Display(Name = "inspectiontype")]
       	   		public string inspectiontype { get; set; }
      /// <summary>
      /// inspectiontypeid
      /// </summary>
      [Display(Name = "inspectiontypeid")]
       	   		public Nullable<int> inspectiontypeid { get; set; }
      /// <summary>
      /// inspectiondate
      /// </summary>
      [Display(Name = "inspectiondate")]
       	   		public Nullable<System.DateTime> inspectiondate { get; set; }
      /// <summary>
      /// productname
      /// </summary>
      [Display(Name = "productname")]
       	   		public string productname { get; set; }
      /// <summary>
      /// serialnumber
      /// </summary>
      [Display(Name = "serialnumber")]
       	   		public string serialnumber { get; set; }
      /// <summary>
      /// proofer
      /// </summary>
      [Display(Name = "proofer")]
       	   		public string proofer { get; set; }
      /// <summary>
      /// inspectionresultid
      /// </summary>
      [Display(Name = "inspectionresultid")]
       	   		public Nullable<int> inspectionresultid { get; set; }
      /// <summary>
      /// badlocation
      /// </summary>
      [Display(Name = "badlocation")]
       	   		public string badlocation { get; set; }
      /// <summary>
      /// abnormaldescription
      /// </summary>
      [Display(Name = "abnormaldescription")]
       	   		public string abnormaldescription { get; set; }
      /// <summary>
      /// handler
      /// </summary>
      [Display(Name = "handler")]
       	   		public string handler { get; set; }
      /// <summary>
      /// resultcodeid
      /// </summary>
      [Display(Name = "resultcodeid")]
       	   		public Nullable<int> resultcodeid { get; set; }
      /// <summary>
      /// ngreason
      /// </summary>
      [Display(Name = "ngreason")]
       	   		public string ngreason { get; set; }
        public virtual ProcInventecPcba proc_inventec_pcba { get; set; }
    }
}

