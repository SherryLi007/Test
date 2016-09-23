using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfAttachment
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
       	   		public string formid { get; set; }
      /// <summary>
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// incident
      /// </summary>
      [Display(Name = "incident")]
       	   		public Nullable<int> incident { get; set; }
      /// <summary>
      /// stepname
      /// </summary>
      [Display(Name = "stepname")]
       	   		public string stepname { get; set; }
      /// <summary>
      /// filename
      /// </summary>
      [Display(Name = "filename")]
       	   		public string filename { get; set; }
      /// <summary>
      /// newname
      /// </summary>
      [Display(Name = "newname")]
       	   		public string newname { get; set; }
      /// <summary>
      /// filetype
      /// </summary>
      [Display(Name = "filetype")]
       	   		public string filetype { get; set; }
      /// <summary>
      /// filesize
      /// </summary>
      [Display(Name = "filesize")]
       	   		public Nullable<int> filesize { get; set; }
      /// <summary>
      /// status
      /// </summary>
      [Display(Name = "status")]
       	   		public string status { get; set; }
      /// <summary>
      /// taskid
      /// </summary>
      [Display(Name = "taskid")]
       	   		public string taskid { get; set; }
      /// <summary>
      /// type
      /// </summary>
      [Display(Name = "type")]
       	   		public string type { get; set; }
      /// <summary>
      /// comments
      /// </summary>
      [Display(Name = "comments")]
       	   		public string comments { get; set; }
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

