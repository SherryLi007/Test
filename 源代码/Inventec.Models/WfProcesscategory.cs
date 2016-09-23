using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfProcesscategory
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// categoryid
      /// </summary>
      [Display(Name = "categoryid")]
              [Required(ErrorMessage = "*")]
	   	   		public string categoryid { get; set; }
      /// <summary>
      /// categoryname
      /// </summary>
      [Display(Name = "categoryname")]
       	   		public string categoryname { get; set; }
      /// <summary>
      /// displayname
      /// </summary>
      [Display(Name = "displayname")]
       	   		public string displayname { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
      /// <summary>
      /// icon
      /// </summary>
      [Display(Name = "icon")]
       	   		public string icon { get; set; }
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

