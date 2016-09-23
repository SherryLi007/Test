using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecSocketDt
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// formid
      /// </summary>
      [Display(Name = "formid")]
              [Required(ErrorMessage = "*")]
	   	   		public int formid { get; set; }
      /// <summary>
      /// rowid
      /// </summary>
      [Display(Name = "rowid")]
       	   		public Nullable<int> rowid { get; set; }
      /// <summary>
      /// 插座编号
      /// </summary>
      [Display(Name = "插座编号")]
       	   		public string socketno { get; set; }
      /// <summary>
      /// 有无异常
      /// </summary>
      [Display(Name = "有无异常")]
       	   		public Nullable<int> unusualid { get; set; }
      /// <summary>
      /// 异常处理
      /// </summary>
      [Display(Name = "异常处理")]
       	   		public string exceptionhandling { get; set; }
      /// <summary>
      /// 异常处理人
      /// </summary>
      [Display(Name = "异常处理人")]
       	   		public string exceptionhandlingpersonnel { get; set; }
      /// <summary>
      /// 处理时间
      /// </summary>
      [Display(Name = "处理时间")]
       	   		public Nullable<System.DateTime> handdate { get; set; }
      /// <summary>
      /// 处理效果
      /// </summary>
      [Display(Name = "处理效果")]
       	   		public Nullable<int> resultcodeid { get; set; }
      /// <summary>
      /// 备注
      /// </summary>
      [Display(Name = "备注")]
       	   		public string remark { get; set; }
    }
}

