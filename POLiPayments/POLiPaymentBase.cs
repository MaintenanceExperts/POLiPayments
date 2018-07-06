using System;
using System.Collections.Generic;
using System.Text;

namespace POLiPayments
{
    public abstract class POLiPaymentBase
    {
        internal string MerchantCode { get; set; }
        internal string AuthenticationCode { get; set; }

        public POLiPaymentBase(string merchantCode, string authenticationCode)
        {
            this.MerchantCode = merchantCode;
            this.AuthenticationCode = authenticationCode;
        }
    }
}
