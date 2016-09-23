using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfProcessfield
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// processid
      /// </summary>
      [Display(Name = "processid")]
       	   		public Nullable<int> processid { get; set; }
      /// <summary>
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// fieldname
      /// </summary>
      [Display(Name = "fieldname")]
       	   		public string fieldname { get; set; }
      /// <summary>
      /// isqueryshow
      /// </summary>
      [Display(Name = "isqueryshow")]
       	   		public Nullable<bool> isqueryshow { get; set; }
    }
}

