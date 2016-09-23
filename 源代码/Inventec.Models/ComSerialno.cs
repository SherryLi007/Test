using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ComSerialno
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// module
      /// </summary>
      [Display(Name = "module")]
       	   		public string module { get; set; }
      /// <summary>
      /// serialtype
      /// </summary>
      [Display(Name = "serialtype")]
              [Required(ErrorMessage = "*")]
	   	   		public string serialtype { get; set; }
      /// <summary>
      /// serialyear
      /// </summary>
      [Display(Name = "serialyear")]
       	   		public Nullable<int> serialyear { get; set; }
      /// <summary>
      /// serialmonth
      /// </summary>
      [Display(Name = "serialmonth")]
       	   		public Nullable<int> serialmonth { get; set; }
      /// <summary>
      /// serialday
      /// </summary>
      [Display(Name = "serialday")]
       	   		public Nullable<int> serialday { get; set; }
      /// <summary>
      /// serialno
      /// </summary>
      [Display(Name = "serialno")]
       	   		public Nullable<int> serialno { get; set; }
      /// <summary>
      /// updatedate
      /// </summary>
      [Display(Name = "updatedate")]
       	   		public Nullable<System.DateTime> updatedate { get; set; }
    }
}

