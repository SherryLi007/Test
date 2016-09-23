using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgJobgrade
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// jobgrade
      /// </summary>
      [Display(Name = "jobgrade")]
       	   		public string jobgrade { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
      /// <summary>
      /// organization
      /// </summary>
      [Display(Name = "organization")]
       	   		public string organization { get; set; }
    }
}

