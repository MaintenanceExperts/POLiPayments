using System;
using System.Collections.Generic;
using System.Text;

namespace POLiPayments.Response
{
    public class GetTransactionResponse : IPOLiPaymentResponse
    {
        public string TransactionRefNo { get; set; }
        public string CurrencyCode { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        
        public decimal PaymentAmount { get; set; }
        public decimal AmountPaid { get; set; }

        public DateTime EstablishedDateTime { get; set; }
        public DateTime MerchantEstablishedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public string BankReceipt { get; set; }
        public string BankReceiptDateTime { get; set; }

        public string TransactionStatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public string FinancialInstitutionCode { get; set; }
        public string FinancialInstitutionCountryCode { get; set; }
        public string FinancialInstitutionName { get; set; }

        public string MerchantReference { get; set; }
        public string MerchantData { get; set; }
        public string MerchantAccountName { get; set; }
        public string MerchantAccountSortCode { get; set; }
        public string MerchantAccountSuffix { get; set; }
        public string MerchantAccountNumber { get; set; }

        public string PayerFirstName { get; set; }
        public string PayerFamilyName { get; set; }

        public string PayerAccountSortCode { get; set; }
        public string PayerAccountNumber { get; set; }
        public string PayerAccountSuffix { get; set; }
    }
}
