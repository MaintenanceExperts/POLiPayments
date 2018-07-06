using POLiPayments.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLiPayments
{
    public class POLiPaymentServer : POLiPaymentBase
    {
        public POLiPaymentServer(string merchantCode, string authenticationCode) : base(merchantCode, authenticationCode) { }

        public async Task<GetTransactionResponse> Nudge(Stream requestStream)
        {
            string nudgePost;
            using (StreamReader sr = new StreamReader(requestStream))
            {
                nudgePost = await sr.ReadToEndAsync();
            }

            if(!nudgePost.StartsWith("Token="))
            {
                throw new Exception("Request was not a POLi Nudge request");
            }

            string token = nudgePost.Split('=').LastOrDefault();

            return await new POLiPaymentClient(this.MerchantCode, this.AuthenticationCode).GetTransaction(token);
        }
    }
}
