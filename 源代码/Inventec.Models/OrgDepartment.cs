using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgDepartment
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// Cost Center 成本中心
      /// </summary>
      [Display(Name = "Cost Center 成本中心")]
              [Required(ErrorMessage = "*")]
	   	   		public string departmentname { get; set; }
      /// <summary>
      /// Cost Center Code 成本中心编号
      /// </summary>
      [Display(Name = "Cost Center Code 成本中心编号")]
       	   		public string departmentcode { get; set; }
      /// <summary>
      /// parentid
      /// </summary>
      [Display(Name = "parentid")]
       	   		public Nullable<int> parentid { get; set; }
      /// <summary>
      /// departmenttype
      /// </summary>
      [Display(Name = "departmenttype")]
       	   		public string departmenttype { get; set; }
      /// <summary>
      /// organization
      /// </summary>
      [Display(Name = "organization")]
       	   		public string organization { get; set; }
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
      /// <summary>
      /// isactive
      /// </summary>
      [Display(Name = "isactive")]
       	   		public Nullable<bool> isactive { get; set; }
    }
}

