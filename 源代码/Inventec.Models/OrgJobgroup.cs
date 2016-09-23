using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgJobgroup
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// jobgroupid
      /// </summary>
      [Display(Name = "jobgroupid")]
       	   		public Nullable<int> jobgroupid { get; set; }
      /// <summary>
      /// jobgroup
      /// </summary>
      [Display(Name = "jobgroup")]
       	   		public string jobgroup { get; set; }
      /// <summary>
      /// organization
      /// </summary>
      [Display(Name = "organization")]
       	   		public string organization { get; set; }
    }
}

