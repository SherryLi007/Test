using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventec.Ultimus.Entity
{
    //formno,applicant, requestdate, STATUS, LOGINUSERACCOUNT, DEPTID, TECOSTCENTER AS costcenter, PRIMARYEXPENSETYPE, CURRENCY, AMOUNT,CNYAMOUNT,paymentDate
    public class ExpenseReportEntity
    {
        string formNo;

        public string FormNo
        {
            get { return formNo; }
            set { formNo = value; }
        }
        string applicant;

        public string Applicant
        {
            get { return applicant; }
            set { applicant = value; }
        }
        DateTime? requestDate;

        public DateTime? RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
        string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        string loginUseraccount;

        public string LoginUseraccount
        {
            get { return loginUseraccount; }
            set { loginUseraccount = value; }
        }
        int depId;

        public int DepId
        {
            get { return depId; }
            set { depId = value; }
        }
        string costcenter;

        public string Costcenter
        {
            get { return costcenter; }
            set { costcenter = value; }
        }
        string primaryExpenseType;

        public string PrimaryExpenseType
        {
            get { return primaryExpenseType; }
            set { primaryExpenseType = value; }
        }
        string currency;

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        decimal cnyAmount;

        public decimal CnyAmount
        {
            get { return cnyAmount; }
            set { cnyAmount = value; }
        }
        DateTime? paymentDate;

        public DateTime? PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        DateTime? endTime;

        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        Decimal? totalAmount;
        public Decimal? TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        string chartofAccount;
        public string ChartofAccount
        {
            get { return chartofAccount; }
            set { chartofAccount = value; }
        }

        string dept;
        public string Dept
        {
            get { return dept; }
            set { dept = value; }
        }

        string vendorCode;
        public string VendorCode
        {
            get { return vendorCode; }
            set { vendorCode = value; }
        }

        string intOrderNo;
        public string IntOrderNo
        {
            get { return intOrderNo; }
            set { intOrderNo = value; }
        }

        string expenseType;
        public string ExpenseType
        {
            get { return expenseType; }
            set { expenseType = value; }
        }

        string poNo;
        public string PoNo
        {
            get { return poNo; }
            set { poNo = value; }
        }

        string companyCode;
        public string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        }

    }
}
