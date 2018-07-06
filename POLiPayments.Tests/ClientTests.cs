using Microsoft.VisualStudio.TestTools.UnitTesting;
using POLiPayments.Request;
using POLiPayments.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLiPayments.Tests
{
    [TestClass]
    public class ClientTests
    {
        const string
            MerchantCode = "xx",
            AuthenticationCode = "xx";

        POLiPaymentClient Client = new POLiPaymentClient(MerchantCode, AuthenticationCode);

        [TestMethod]
        public async Task InitiateTransactionTest()
        {
            InitiateTransactionRequest request = new InitiateTransactionRequest(100, Guid.NewGuid().ToString(), "https://www.mex.com.au", "https://www.mex.com.au/success");
            InitiateTransactionResponse response = await this.Client.InitiateTransaction(request);

            Assert.IsTrue(response.Success);
        }
    }
}
