using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfProcessstepsstatus
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// 状态ID
      /// </summary>
      [Display(Name = "状态ID")]
       	   		public Nullable<int> status { get; set; }
      /// <summary>
      /// 状态(中文)
      /// </summary>
      [Display(Name = "状态(中文)")]
       	   		public string statuscn { get; set; }
      /// <summary>
      /// 状态(英文)
      /// </summary>
      [Display(Name = "状态(英文)")]
       	   		public string statusen { get; set; }
    }
}

