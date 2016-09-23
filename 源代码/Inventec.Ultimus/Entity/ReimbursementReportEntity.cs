using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventec.Ultimus.Entity
{
    public class ReimbursementReportEntity
    {
        string payMonth;

        public string PayMonth
        {
            get { return payMonth; }
            set { payMonth = value; }
        }
        decimal payment;

        public decimal Payment
        {
            get { return payment; }
            set { payment = value; }
        }
    }
}
