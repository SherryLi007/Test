using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ProcInventecMfgDt
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
      /// 确认项目
      /// </summary>
      [Display(Name = "确认项目")]
       	   		public string confirmproject { get; set; }
      /// <summary>
      /// 检查单位
      /// </summary>
      [Display(Name = "检查单位")]
       	   		public string checkunit { get; set; }
      /// <summary>
      /// 执行状况
      /// </summary>
      [Display(Name = "执行状况")]
       	   		public string performsituation { get; set; }
      /// <summary>
      /// 确认者
      /// </summary>
      [Display(Name = "确认者")]
       	   		public string confirmpeople { get; set; }
      /// <summary>
      /// 异常情形
      /// </summary>
      [Display(Name = "异常情形")]
       	   		public string abnormalities { get; set; }
      /// <summary>
      /// 异常处理
      /// </summary>
      [Display(Name = "异常处理")]
       	   		public string exceptionhandling { get; set; }
      /// <summary>
      /// 处理者
      /// </summary>
      [Display(Name = "处理者")]
       	   		public string handler { get; set; }
        public virtual ProcInventecMfg proc_inventec_mfg { get; set; }
    }
}

