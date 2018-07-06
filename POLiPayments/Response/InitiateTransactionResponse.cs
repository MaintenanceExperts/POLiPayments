using System;
using System.Collections.Generic;
using System.Text;

namespace POLiPayments.Response
{
    public class InitiateTransactionResponse : IPOLiPaymentResponse
    {
        public bool Success { get; set; }
        public string TransactionRefNo { get; set; }
        public string NavigateURL { get; set; }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
