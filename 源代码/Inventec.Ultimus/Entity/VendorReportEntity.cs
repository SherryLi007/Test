using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventec.Ultimus.Entity
{
    public class VendorReportEntity
    {
        string vendorCode;

        public string VendorCode
        {
            get { return vendorCode; }
            set { vendorCode = value; }
        }
        string vendorName;

        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }
        string formNo;

        public string FormNo
        {
            get { return formNo; }
            set { formNo = value; }
        }
        string paymentDocNo;

        public string PaymentDocNo
        {
            get { return paymentDocNo; }
            set { paymentDocNo = value; }
        }
        DateTime? paymentDate;

        public DateTime? PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        DateTime? scanDate;

        public DateTime? ScanDate
        {
            get { return scanDate; }
            set { scanDate = value; }
        }
        string dept;

        public string Dept
        {
            get { return dept; }
            set { dept = value; }
        }
        decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        string invoiceNo;

        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }
        DateTime? invoiceDate;

        public DateTime? InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; }
        }
        DateTime? requestDate;

        public DateTime? RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }
        string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        string applicant;
        public string Applicant
        {
            get { return applicant; }
            set { applicant = value; }
        }

        string invoiceType;
        public string InvoiceType
        {
            get { return invoiceType; }
            set { invoiceType = value; }
        }

        decimal? taxAmount;
        public decimal? TaxAmount
        {
            get { return taxAmount; }
            set { taxAmount = value; }
        }

        decimal? invoiceAmount;
        public decimal? InvoiceAmount
        {
            get { return invoiceAmount; }
            set { invoiceAmount = value; }
        }

        string glAccount;
        public string GlAccount
        {
            get { return glAccount; }
            set { glAccount = value; }
        }

        string paymentTerm;
        public string PaymentTerm
        {
            get { return paymentTerm; }
            set { paymentTerm = value; }
        }

        DateTime? dueDate;
        public DateTime? DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        String batchNo;
        public String BatchNo
        {
            get { return batchNo; }
            set { batchNo = value; }
        }

        int? funDays;
        public int? FunDays
        {
            get { return funDays; }
            set { funDays = value; }
        }

        int? finanDays;
        public int? FinanDays
        {
            get { return finanDays; }
            set { finanDays = value; }
        }

        int? payDays;
        public int? PayDays
        {
            get { return payDays; }
            set { payDays = value; }
        }

        string companyCode;
        public string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        }

        //k.ArticleNo,k.GoodsName,k.TaxRate,k.Currency,k.Quantifiers,k.RowID
        string articleNo;
        public string ArticleNo
        {
            get { return articleNo; }
            set { articleNo = value; }
        }

        string goodsName;
        public string GoodsName
        {
            get { return goodsName; }
            set { goodsName = value; }
        }

        string taxRate;
        public string TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        string currency;
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        string quantifiers;
        public string Quantifiers
        {
            get { return quantifiers; }
            set { quantifiers = value; }
        }

        Nullable<int> rowID;
        public Nullable<int> RowID
        {
            get { return rowID; }
            set { rowID = value; }
        }

        Nullable<DateTime> emailSendDate;
        public Nullable<DateTime> EmailSendDate
        {
            get { return emailSendDate; }
            set { emailSendDate = value; }
        }

        Nullable<DateTime> startTime;
        public Nullable<DateTime> StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        Nullable<DateTime> endTime;
        public Nullable<DateTime> EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

    }
}
