using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecFeederlistDt
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
      /// zno
      /// </summary>
      [Display(Name = "zno")]
       	   		public string zno { get; set; }
      /// <summary>
      /// partno
      /// </summary>
      [Display(Name = "partno")]
       	   		public string partno { get; set; }
      /// <summary>
      /// description
      /// </summary>
      [Display(Name = "description")]
       	   		public string description { get; set; }
      /// <summary>
      /// qty
      /// </summary>
      [Display(Name = "qty")]
       	   		public string qty { get; set; }
      /// <summary>
      /// feeder
      /// </summary>
      [Display(Name = "feeder")]
       	   		public string feeder { get; set; }
      /// <summary>
      /// remark
      /// </summary>
      [Display(Name = "remark")]
       	   		public string remark { get; set; }
    }
}

