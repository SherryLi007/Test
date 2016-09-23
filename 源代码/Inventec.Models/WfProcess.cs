using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfProcess
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
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// displayname
      /// </summary>
      [Display(Name = "displayname")]
       	   		public string displayname { get; set; }
      /// <summary>
      /// processversion
      /// </summary>
      [Display(Name = "processversion")]
       	   		public Nullable<int> processversion { get; set; }
      /// <summary>
      /// defaultpcform
      /// </summary>
      [Display(Name = "defaultpcform")]
       	   		public string defaultpcform { get; set; }
      /// <summary>
      /// defaultmobileform
      /// </summary>
      [Display(Name = "defaultmobileform")]
       	   		public string defaultmobileform { get; set; }
      /// <summary>
      /// defaultemailform
      /// </summary>
      [Display(Name = "defaultemailform")]
       	   		public string defaultemailform { get; set; }
      /// <summary>
      /// categoryid
      /// </summary>
      [Display(Name = "categoryid")]
       	   		public string categoryid { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
      /// <summary>
      /// hasform
      /// </summary>
      [Display(Name = "hasform")]
       	   		public string hasform { get; set; }
      /// <summary>
      /// icon
      /// </summary>
      [Display(Name = "icon")]
       	   		public string icon { get; set; }
      /// <summary>
      /// hasamount
      /// </summary>
      [Display(Name = "hasamount")]
       	   		public Nullable<bool> hasamount { get; set; }
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

