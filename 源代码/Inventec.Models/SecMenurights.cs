using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class SecMenurights
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// rightsname
      /// </summary>
      [Display(Name = "rightsname")]
       	   		public string rightsname { get; set; }
      /// <summary>
      /// remark
      /// </summary>
      [Display(Name = "remark")]
       	   		public string remark { get; set; }
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
    }
}

