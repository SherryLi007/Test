using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfDraft
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
      /// formid
      /// </summary>
      [Display(Name = "formid")]
       	   		public Nullable<int> formid { get; set; }
      /// <summary>
      /// tablename
      /// </summary>
      [Display(Name = "tablename")]
       	   		public string tablename { get; set; }
      /// <summary>
      /// summary
      /// </summary>
      [Display(Name = "summary")]
       	   		public string summary { get; set; }
      /// <summary>
      /// createdate
      /// </summary>
      [Display(Name = "createdate")]
       	   		public Nullable<System.DateTime> createdate { get; set; }
      /// <summary>
      /// createby
      /// </summary>
      [Display(Name = "createby")]
       	   		public Nullable<int> createby { get; set; }
      /// <summary>
      /// information
      /// </summary>
      [Display(Name = "information")]
       	   		public byte[] information { get; set; }
    }
}

