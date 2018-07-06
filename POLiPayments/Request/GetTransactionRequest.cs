using System;
using System.Collections.Generic;
using System.Text;

namespace POLiPayments.Request
{
    internal class GetTransactionRequest : IPOLiPaymentRequest
    {
        public string TransactionToken { get; set; }

        internal GetTransactionRequest(string transactionToken)
        {
            this.TransactionToken = transactionToken;
        }

        public void Validate() {
            if(String.IsNullOrEmpty(this.TransactionToken))
            {
                throw new Exception("Transaction Token should not be empty");
            }
        }
    }
}
