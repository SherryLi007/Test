using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgGroupmember
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// groupid
      /// </summary>
      [Display(Name = "groupid")]
       	   		public Nullable<int> groupid { get; set; }
      /// <summary>
      /// memberid
      /// </summary>
      [Display(Name = "memberid")]
       	   		public Nullable<int> memberid { get; set; }
      /// <summary>
      /// membername
      /// </summary>
      [Display(Name = "membername")]
       	   		public string membername { get; set; }
      /// <summary>
      /// membertype
      /// </summary>
      [Display(Name = "membertype")]
       	   		public string membertype { get; set; }
      /// <summary>
      /// weight
      /// </summary>
      [Display(Name = "weight")]
       	   		public Nullable<int> weight { get; set; }
      /// <summary>
      /// seq
      /// </summary>
      [Display(Name = "seq")]
       	   		public Nullable<int> seq { get; set; }
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

