using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class BdLog
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// tablename
      /// </summary>
      [Display(Name = "tablename")]
       	   		public string tablename { get; set; }
      /// <summary>
      /// originaldata
      /// </summary>
      [Display(Name = "originaldata")]
       	   		public string originaldata { get; set; }
      /// <summary>
      /// newdata
      /// </summary>
      [Display(Name = "newdata")]
       	   		public string newdata { get; set; }
      /// <summary>
      /// modifyby
      /// </summary>
      [Display(Name = "modifyby")]
       	   	   [ReadOnly(true)]
	   		public string modifyby { get; set; }
      /// <summary>
      /// modifytime
      /// </summary>
      [Display(Name = "modifytime")]
       	   		public Nullable<System.DateTime> modifytime { get; set; }
      /// <summary>
      /// modifytype
      /// </summary>
      [Display(Name = "modifytype")]
       	   		public Nullable<int> modifytype { get; set; }
      /// <summary>
      /// dataid
      /// </summary>
      [Display(Name = "dataid")]
       	   		public Nullable<int> dataid { get; set; }
      /// <summary>
      /// formid
      /// </summary>
      [Display(Name = "formid")]
       	   		public Nullable<int> formid { get; set; }
    }
}

