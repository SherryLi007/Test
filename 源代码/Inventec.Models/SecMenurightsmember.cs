using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class SecMenurightsmember
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// rightsid
      /// </summary>
      [Display(Name = "rightsid")]
       	   		public Nullable<int> rightsid { get; set; }
      /// <summary>
      /// membertype
      /// </summary>
      [Display(Name = "membertype")]
       	   		public string membertype { get; set; }
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
      /// groupid
      /// </summary>
      [Display(Name = "groupid")]
       	   		public Nullable<int> groupid { get; set; }
      /// <summary>
      /// groupname
      /// </summary>
      [Display(Name = "groupname")]
       	   		public string groupname { get; set; }
    }
}

