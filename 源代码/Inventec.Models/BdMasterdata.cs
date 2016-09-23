using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class BdMasterdata
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// 键
      /// </summary>
      [Display(Name = "键")]
       	   		public string typename { get; set; }
      /// <summary>
      /// 值
      /// </summary>
      [Display(Name = "值")]
       	   		public string datadec { get; set; }
      /// <summary>
      /// datavalue
      /// </summary>
      [Display(Name = "datavalue")]
       	   		public Nullable<int> datavalue { get; set; }
      /// <summary>
      /// createdate
      /// </summary>
      [Display(Name = "createdate")]
       	   		public Nullable<System.DateTime> createdate { get; set; }
      /// <summary>
      /// createby
      /// </summary>
      [Display(Name = "createby")]
       	   		public string createby { get; set; }
      /// <summary>
      /// updatedate
      /// </summary>
      [Display(Name = "updatedate")]
       	   		public Nullable<System.DateTime> updatedate { get; set; }
      /// <summary>
      /// updateby
      /// </summary>
      [Display(Name = "updateby")]
       	   		public string updateby { get; set; }
      /// <summary>
      /// isdelete
      /// </summary>
      [Display(Name = "isdelete")]
       	   		public Nullable<int> isdelete { get; set; }
    }
}

