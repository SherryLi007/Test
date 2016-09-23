using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class ComLog
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// logdate
      /// </summary>
      [Display(Name = "logdate")]
       	   		public Nullable<System.DateTime> logdate { get; set; }
      /// <summary>
      /// thread
      /// </summary>
      [Display(Name = "thread")]
       	   		public string thread { get; set; }
      /// <summary>
      /// loglevel
      /// </summary>
      [Display(Name = "loglevel")]
       	   		public string loglevel { get; set; }
      /// <summary>
      /// logger
      /// </summary>
      [Display(Name = "logger")]
       	   		public string logger { get; set; }
      /// <summary>
      /// message
      /// </summary>
      [Display(Name = "message")]
       	   		public string message { get; set; }
      /// <summary>
      /// exception
      /// </summary>
      [Display(Name = "exception")]
       	   		public string exception { get; set; }
    }
}

