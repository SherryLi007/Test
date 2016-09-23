using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventec.Models
{
    public partial class WfAgentProcess
    {
      /// <summary>
      /// id
      /// </summary>
      [Display(Name = "id")]
              [Required(ErrorMessage = "*")]
	   	   		public int id { get; set; }
      /// <summary>
      /// agentid
      /// </summary>
      [Display(Name = "agentid")]
       	   		public Nullable<int> agentid { get; set; }
      /// <summary>
      /// processid
      /// </summary>
      [Display(Name = "processid")]
       	   		public Nullable<int> processid { get; set; }
      /// <summary>
      /// canapply
      /// </summary>
      [Display(Name = "canapply")]
       	   		public Nullable<bool> canapply { get; set; }
      /// <summary>
      /// canapprove
      /// </summary>
      [Display(Name = "canapprove")]
       	   		public Nullable<bool> canapprove { get; set; }
      /// <summary>
      /// approveamount
      /// </summary>
      [Display(Name = "approveamount")]
       	   		public Nullable<decimal> approveamount { get; set; }
    }
}

