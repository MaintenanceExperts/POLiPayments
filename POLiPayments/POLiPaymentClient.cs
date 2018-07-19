using Newtonsoft.Json;
using POLiPayments.Request;
using POLiPayments.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POLiPayments
{
    public class POLiPaymentClient : POLiPaymentBase
    {
        public POLiPaymentClient(string merchantCode, string authenticationCode) : base(merchantCode, authenticationCode) { }

        public async Task<InitiateTransactionResponse> InitiateTransaction(InitiateTransactionRequest request)
        {
            InitiateTransactionResponse response = await this.ExecuteRequest<InitiateTransactionRequest, InitiateTransactionResponse>(request);
            return response;
        }

        public async Task<GetTransactionResponse> GetTransaction(string transactionToken)
        {
            return await this.ExecuteRequest<GetTransactionRequest, GetTransactionResponse>(new GetTransactionRequest(transactionToken));
        }

        async Task<Response> ExecuteRequest<Request, Response>(Request request) where Request: IPOLiPaymentRequest where Response: IPOLiPaymentResponse
        {
            request.Validate();

            string
                jsonPayload = JsonConvert.SerializeObject(request),
                auth = System.Convert.ToBase64String(Encoding.UTF8.GetBytes($"{this.MerchantCode}:{this.AuthenticationCode}"));

            string
                apiEndpoint,
                apiMethod = "POST";

            if(typeof(Request) == typeof(InitiateTransactionRequest))
            {
                apiEndpoint = "https://poliapi.apac.paywithpoli.com/api/v2/Transaction/Initiate";
            } else if(typeof(Request) == typeof(GetTransactionRequest))
            {
                apiEndpoint = $"https://poliapi.apac.paywithpoli.com/api/v2/Transaction/GetTransaction?token={(request as GetTransactionRequest).TransactionToken}";
                apiMethod = "GET";
                jsonPayload = null;
            } else
            {
                throw new Exception();
            }

            HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(apiEndpoint);
            httpRequest.Method = apiMethod;
            httpRequest.Headers.Add("Authorization", $"Basic {auth}");

            if(!String.IsNullOrEmpty(jsonPayload))
            {
                httpRequest.ContentType = "application/json";
                httpRequest.ContentLength = jsonPayload.Length;

                using (Stream httpRequestStream = httpRequest.GetRequestStream())
                using (StreamWriter sw = new StreamWriter(httpRequestStream))
                {
                    sw.Write(jsonPayload);
                }
            }

            HttpWebResponse httpResponse = await httpRequest.GetResponseAsync() as HttpWebResponse;
            string httpResponsePayload;

            using (Stream httpResponseStream = httpResponse.GetResponseStream())
            using (StreamReader sr = new StreamReader(httpResponseStream))
            {
                httpResponsePayload = await sr.ReadToEndAsync();
            }

            return JsonConvert.DeserializeObject<Response>(httpResponsePayload);
        }
    }
}
