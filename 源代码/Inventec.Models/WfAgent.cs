using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfAgent
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// 用户
      /// </summary>
      [Display(Name = "用户")]
       	   		public Nullable<int> userid { get; set; }
      /// <summary>
      /// 代理人
      /// </summary>
      [Display(Name = "代理人")]
       	   		public Nullable<int> agentuserid { get; set; }
      /// <summary>
      /// agenttype
      /// </summary>
      [Display(Name = "agenttype")]
       	   		public Nullable<int> agenttype { get; set; }
      /// <summary>
      /// 代理起始日期
      /// </summary>
      [Display(Name = "代理起始日期")]
       	   		public Nullable<System.DateTime> startdate { get; set; }
      /// <summary>
      /// 代理结束日期
      /// </summary>
      [Display(Name = "代理结束日期")]
       	   		public Nullable<System.DateTime> enddate { get; set; }
      /// <summary>
      /// 设定人
      /// </summary>
      [Display(Name = "设定人")]
       	   	   [ReadOnly(true)]
	   		public Nullable<int> createby { get; set; }
      /// <summary>
      /// 设定时间
      /// </summary>
      [Display(Name = "设定时间")]
       	   	   [ReadOnly(true)]
	   		public Nullable<System.DateTime> createdate { get; set; }
    }
}

