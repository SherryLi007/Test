using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgUser
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// loginname
      /// </summary>
      [Display(Name = "loginname")]
              [Required(ErrorMessage = "*")]
	   	   		public string loginname { get; set; }
      /// <summary>
      /// 用户名
      /// </summary>
      [Display(Name = "用户名")]
              [Required(ErrorMessage = "*")]
	   	   		public string username { get; set; }
      /// <summary>
      /// 中文名
      /// </summary>
      [Display(Name = "中文名")]
       	   		public string usernamecn { get; set; }
      /// <summary>
      /// 用户编号
      /// </summary>
      [Display(Name = "用户编号")]
       	   		public string usercode { get; set; }
      /// <summary>
      /// jobtitle
      /// </summary>
      [Display(Name = "jobtitle")]
       	   		public string jobtitle { get; set; }
      /// <summary>
      /// deparmentid
      /// </summary>
      [Display(Name = "deparmentid")]
       	   		public Nullable<int> deparmentid { get; set; }
      /// <summary>
      /// deparment
      /// </summary>
      [Display(Name = "deparment")]
       	   		public string deparment { get; set; }
      /// <summary>
      /// jobfunction
      /// </summary>
      [Display(Name = "jobfunction")]
       	   		public string jobfunction { get; set; }
      /// <summary>
      /// organization
      /// </summary>
      [Display(Name = "organization")]
       	   		public string organization { get; set; }
      /// <summary>
      /// 邮箱
      /// </summary>
      [Display(Name = "邮箱")]
       	   		public string email { get; set; }
      /// <summary>
      /// 手机
      /// </summary>
      [Display(Name = "手机")]
       	   		public string mobileno { get; set; }
      /// <summary>
      /// 电话
      /// </summary>
      [Display(Name = "电话")]
       	   		public string tel { get; set; }
      /// <summary>
      /// im
      /// </summary>
      [Display(Name = "im")]
       	   		public string im { get; set; }
      /// <summary>
      /// password
      /// </summary>
      [Display(Name = "password")]
       	   		public string password { get; set; }
      /// <summary>
      /// 语言
      /// </summary>
      [Display(Name = "语言")]
       	   		public string language { get; set; }
      /// <summary>
      /// picture
      /// </summary>
      [Display(Name = "picture")]
       	   		public string picture { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
      /// <summary>
      /// theme
      /// </summary>
      [Display(Name = "theme")]
       	   		public string theme { get; set; }
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
      /// <summary>
      /// isactive
      /// </summary>
      [Display(Name = "isactive")]
       	   		public string isactive { get; set; }
      /// <summary>
      /// isreceive
      /// </summary>
      [Display(Name = "isreceive")]
       	   		public Nullable<bool> isreceive { get; set; }
      /// <summary>
      /// ext01
      /// </summary>
      [Display(Name = "ext01")]
       	   		public string ext01 { get; set; }
      /// <summary>
      /// ext02
      /// </summary>
      [Display(Name = "ext02")]
       	   		public string ext02 { get; set; }
      /// <summary>
      /// ext03
      /// </summary>
      [Display(Name = "ext03")]
       	   		public string ext03 { get; set; }
      /// <summary>
      /// ext04
      /// </summary>
      [Display(Name = "ext04")]
       	   		public string ext04 { get; set; }
      /// <summary>
      /// ext05
      /// </summary>
      [Display(Name = "ext05")]
       	   		public string ext05 { get; set; }
      /// <summary>
      /// costcenter
      /// </summary>
      [Display(Name = "costcenter")]
       	   		public Nullable<int> costcenter { get; set; }
      /// <summary>
      /// sapaccount
      /// </summary>
      [Display(Name = "sapaccount")]
       	   		public string sapaccount { get; set; }
      /// <summary>
      /// istemp
      /// </summary>
      [Display(Name = "istemp")]
       	   		public Nullable<bool> istemp { get; set; }
    }
}

