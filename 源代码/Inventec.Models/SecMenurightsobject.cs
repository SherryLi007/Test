using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class SecMenurightsobject
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// rightsid
      /// </summary>
      [Display(Name = "rightsid")]
       	   		public Nullable<int> rightsid { get; set; }
      /// <summary>
      /// menuid
      /// </summary>
      [Display(Name = "menuid")]
       	   		public Nullable<int> menuid { get; set; }
      /// <summary>
      /// canedit
      /// </summary>
      [Display(Name = "canedit")]
       	   		public Nullable<int> canedit { get; set; }
      /// <summary>
      /// candelete
      /// </summary>
      [Display(Name = "candelete")]
       	   		public Nullable<int> candelete { get; set; }
    }
}

