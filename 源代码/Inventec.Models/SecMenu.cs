using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class SecMenu
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
      /// menuname
      /// </summary>
      [Display(Name = "menuname")]
       	   		public string menuname { get; set; }
      /// <summary>
      /// displayname
      /// </summary>
      [Display(Name = "displayname")]
       	   		public string displayname { get; set; }
      /// <summary>
      /// menutype
      /// </summary>
      [Display(Name = "menutype")]
       	   		public string menutype { get; set; }
      /// <summary>
      /// parentid
      /// </summary>
      [Display(Name = "parentid")]
       	   		public Nullable<int> parentid { get; set; }
      /// <summary>
      /// formid
      /// </summary>
      [Display(Name = "formid")]
       	   		public string formid { get; set; }
      /// <summary>
      /// url
      /// </summary>
      [Display(Name = "url")]
       	   		public string url { get; set; }
      /// <summary>
      /// icon
      /// </summary>
      [Display(Name = "icon")]
       	   		public string icon { get; set; }
      /// <summary>
      /// target
      /// </summary>
      [Display(Name = "target")]
       	   		public string target { get; set; }
      /// <summary>
      /// accesslevel
      /// </summary>
      [Display(Name = "accesslevel")]
       	   		public string accesslevel { get; set; }
      /// <summary>
      /// isactive
      /// </summary>
      [Display(Name = "isactive")]
       	   		public string isactive { get; set; }
      /// <summary>
      /// ishomepage
      /// </summary>
      [Display(Name = "ishomepage")]
       	   		public string ishomepage { get; set; }
      /// <summary>
      /// isvisible
      /// </summary>
      [Display(Name = "isvisible")]
       	   		public string isvisible { get; set; }
      /// <summary>
      /// relatedfolder
      /// </summary>
      [Display(Name = "relatedfolder")]
       	   		public string relatedfolder { get; set; }
      /// <summary>
      /// relatedform
      /// </summary>
      [Display(Name = "relatedform")]
       	   		public string relatedform { get; set; }
      /// <summary>
      /// remark
      /// </summary>
      [Display(Name = "remark")]
       	   		public string remark { get; set; }
      /// <summary>
      /// height
      /// </summary>
      [Display(Name = "height")]
       	   		public Nullable<int> height { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
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
    }
}

