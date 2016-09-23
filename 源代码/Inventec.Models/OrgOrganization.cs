using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class OrgOrganization
    {
      /// <summary>
      /// organization
      /// </summary>
      [Display(Name = "organization")]
              [Required(ErrorMessage = "*")]
	   	   		public string organization { get; set; }
      /// <summary>
      /// companycode
      /// </summary>
      [Display(Name = "companycode")]
              [Required(ErrorMessage = "*")]
	   	   		public string companycode { get; set; }
      /// <summary>
      /// companyname
      /// </summary>
      [Display(Name = "companyname")]
       	   		public string companyname { get; set; }
      /// <summary>
      /// officialnameen
      /// </summary>
      [Display(Name = "officialnameen")]
       	   		public string officialnameen { get; set; }
      /// <summary>
      /// officialnamecn
      /// </summary>
      [Display(Name = "officialnamecn")]
       	   		public string officialnamecn { get; set; }
      /// <summary>
      /// legaladdressen
      /// </summary>
      [Display(Name = "legaladdressen")]
       	   		public string legaladdressen { get; set; }
      /// <summary>
      /// legaladdresscn
      /// </summary>
      [Display(Name = "legaladdresscn")]
       	   		public string legaladdresscn { get; set; }
      /// <summary>
      /// currencyid
      /// </summary>
      [Display(Name = "currencyid")]
       	   		public Nullable<int> currencyid { get; set; }
      /// <summary>
      /// effectfrom
      /// </summary>
      [Display(Name = "effectfrom")]
       	   		public Nullable<System.DateTime> effectfrom { get; set; }
      /// <summary>
      /// effectto
      /// </summary>
      [Display(Name = "effectto")]
       	   		public Nullable<System.DateTime> effectto { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
      /// <summary>
      /// company_tax_code
      /// </summary>
      [Display(Name = "company_tax_code")]
       	   		public string company_tax_code { get; set; }
      /// <summary>
      /// is_valid
      /// </summary>
      [Display(Name = "is_valid")]
       	   		public Nullable<int> is_valid { get; set; }
      /// <summary>
      /// last_modified
      /// </summary>
      [Display(Name = "last_modified")]
       	   		public Nullable<System.DateTime> last_modified { get; set; }
      /// <summary>
      /// isactive
      /// </summary>
      [Display(Name = "isactive")]
       	   		public Nullable<bool> isactive { get; set; }
    }
}

