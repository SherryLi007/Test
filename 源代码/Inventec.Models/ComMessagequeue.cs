using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ComMessagequeue
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// messageid
      /// </summary>
      [Display(Name = "messageid")]
       	   		public string messageid { get; set; }
      /// <summary>
      /// source
      /// </summary>
      [Display(Name = "source")]
       	   		public string source { get; set; }
      /// <summary>
      /// formid
      /// </summary>
      [Display(Name = "formid")]
       	   		public string formid { get; set; }
      /// <summary>
      /// fromuser
      /// </summary>
      [Display(Name = "fromuser")]
       	   		public string fromuser { get; set; }
      /// <summary>
      /// touser
      /// </summary>
      [Display(Name = "touser")]
       	   		public string touser { get; set; }
      /// <summary>
      /// subject
      /// </summary>
      [Display(Name = "subject")]
       	   		public string subject { get; set; }
      /// <summary>
      /// body
      /// </summary>
      [Display(Name = "body")]
       	   		public string body { get; set; }
      /// <summary>
      /// bodytype
      /// </summary>
      [Display(Name = "bodytype")]
       	   		public string bodytype { get; set; }
      /// <summary>
      /// sendtype
      /// </summary>
      [Display(Name = "sendtype")]
       	   		public string sendtype { get; set; }
      /// <summary>
      /// status
      /// </summary>
      [Display(Name = "status")]
       	   		public Nullable<int> status { get; set; }
      /// <summary>
      /// retrytimes
      /// </summary>
      [Display(Name = "retrytimes")]
       	   		public Nullable<int> retrytimes { get; set; }
      /// <summary>
      /// createdate
      /// </summary>
      [Display(Name = "createdate")]
       	   		public Nullable<System.DateTime> createdate { get; set; }
      /// <summary>
      /// senddate
      /// </summary>
      [Display(Name = "senddate")]
       	   		public Nullable<System.DateTime> senddate { get; set; }
      /// <summary>
      /// isread
      /// </summary>
      [Display(Name = "isread")]
       	   		public Nullable<int> isread { get; set; }
    }
}

