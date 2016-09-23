using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfProcessstep
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// processname
      /// </summary>
      [Display(Name = "processname")]
       	   		public string processname { get; set; }
      /// <summary>
      /// processversion
      /// </summary>
      [Display(Name = "processversion")]
       	   		public Nullable<int> processversion { get; set; }
      /// <summary>
      /// stepname
      /// </summary>
      [Display(Name = "stepname")]
       	   		public string stepname { get; set; }
      /// <summary>
      /// displayname
      /// </summary>
      [Display(Name = "displayname")]
       	   		public string displayname { get; set; }
      /// <summary>
      /// pcform
      /// </summary>
      [Display(Name = "pcform")]
       	   		public string pcform { get; set; }
      /// <summary>
      /// mobileform
      /// </summary>
      [Display(Name = "mobileform")]
       	   		public string mobileform { get; set; }
      /// <summary>
      /// emailform
      /// </summary>
      [Display(Name = "emailform")]
       	   		public string emailform { get; set; }
      /// <summary>
      /// approvervariable
      /// </summary>
      [Display(Name = "approvervariable")]
       	   		public string approvervariable { get; set; }
      /// <summary>
      /// isfinance
      /// </summary>
      [Display(Name = "isfinance")]
       	   		public Nullable<bool> isfinance { get; set; }
      /// <summary>
      /// functionalid
      /// </summary>
      [Display(Name = "functionalid")]
       	   		public string functionalid { get; set; }
      /// <summary>
      /// doaid
      /// </summary>
      [Display(Name = "doaid")]
       	   		public string doaid { get; set; }
      /// <summary>
      /// orderno
      /// </summary>
      [Display(Name = "orderno")]
       	   		public Nullable<int> orderno { get; set; }
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
      /// ext06
      /// </summary>
      [Display(Name = "ext06")]
       	   		public string ext06 { get; set; }
      /// <summary>
      /// ext07
      /// </summary>
      [Display(Name = "ext07")]
       	   		public string ext07 { get; set; }
      /// <summary>
      /// ext08
      /// </summary>
      [Display(Name = "ext08")]
       	   		public string ext08 { get; set; }
      /// <summary>
      /// ext09
      /// </summary>
      [Display(Name = "ext09")]
       	   		public string ext09 { get; set; }
      /// <summary>
      /// ext10
      /// </summary>
      [Display(Name = "ext10")]
       	   		public string ext10 { get; set; }
      /// <summary>
      /// ext11
      /// </summary>
      [Display(Name = "ext11")]
       	   		public string ext11 { get; set; }
      /// <summary>
      /// ext12
      /// </summary>
      [Display(Name = "ext12")]
       	   		public string ext12 { get; set; }
      /// <summary>
      /// ext13
      /// </summary>
      [Display(Name = "ext13")]
       	   		public string ext13 { get; set; }
      /// <summary>
      /// ext14
      /// </summary>
      [Display(Name = "ext14")]
       	   		public string ext14 { get; set; }
      /// <summary>
      /// ext15
      /// </summary>
      [Display(Name = "ext15")]
       	   		public string ext15 { get; set; }
      /// <summary>
      /// ext16
      /// </summary>
      [Display(Name = "ext16")]
       	   		public string ext16 { get; set; }
      /// <summary>
      /// ext17
      /// </summary>
      [Display(Name = "ext17")]
       	   		public string ext17 { get; set; }
      /// <summary>
      /// ext18
      /// </summary>
      [Display(Name = "ext18")]
       	   		public string ext18 { get; set; }
      /// <summary>
      /// ext19
      /// </summary>
      [Display(Name = "ext19")]
       	   		public string ext19 { get; set; }
      /// <summary>
      /// ext20
      /// </summary>
      [Display(Name = "ext20")]
       	   		public string ext20 { get; set; }
      /// <summary>
      /// ext21
      /// </summary>
      [Display(Name = "ext21")]
       	   		public string ext21 { get; set; }
      /// <summary>
      /// ext22
      /// </summary>
      [Display(Name = "ext22")]
       	   		public string ext22 { get; set; }
      /// <summary>
      /// ext23
      /// </summary>
      [Display(Name = "ext23")]
       	   		public string ext23 { get; set; }
      /// <summary>
      /// ext24
      /// </summary>
      [Display(Name = "ext24")]
       	   		public string ext24 { get; set; }
      /// <summary>
      /// ext25
      /// </summary>
      [Display(Name = "ext25")]
       	   		public string ext25 { get; set; }
      /// <summary>
      /// ext26
      /// </summary>
      [Display(Name = "ext26")]
       	   		public string ext26 { get; set; }
      /// <summary>
      /// ext27
      /// </summary>
      [Display(Name = "ext27")]
       	   		public string ext27 { get; set; }
      /// <summary>
      /// ext28
      /// </summary>
      [Display(Name = "ext28")]
       	   		public string ext28 { get; set; }
      /// <summary>
      /// ext29
      /// </summary>
      [Display(Name = "ext29")]
       	   		public string ext29 { get; set; }
      /// <summary>
      /// ext30
      /// </summary>
      [Display(Name = "ext30")]
       	   		public string ext30 { get; set; }
    }
}

