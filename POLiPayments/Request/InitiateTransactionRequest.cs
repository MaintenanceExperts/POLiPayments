using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace POLiPayments.Request
{
    public class InitiateTransactionRequest : IPOLiPaymentRequest
    {
        decimal amount = 0;
        [JsonRequired]
        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (Math.Round(value, 2) != value)
                {
                    throw new Exception("Amount must be no more than 2 decimal places");
                }

                amount = value;
            }
        }

        [JsonRequired]
        public string CurrencyCode { get; set; }

        [JsonRequired]        
        public string MerchantReference { get; set; }
        public string MerchantReferenceFormat { get; set; }

        public string MerchantData { get; set; }

        [JsonRequired]
        public string MerchantHomepageURL { get; set; }

        [JsonRequired]
        public string SuccessURL { get; set; }
        public string FailureURL { get; set; }
        public string CancellationURL { get; set; }
        public string NotificationURL { get; set; }

        public int? Timeout { get; set; }

        public string SelectedFICode { get; set; }

        internal InitiateTransactionRequest()
        {
            this.CurrencyCode = "AUD";
        }

        public InitiateTransactionRequest(decimal amount, string merchantReference, string merchantHomepageUrl, string successUrl) : this() {
            this.Amount = amount;
            this.MerchantReference = merchantReference;
            this.MerchantHomepageURL = merchantHomepageUrl;
            this.SuccessURL = successUrl;
        }

        public void Validate()
        {
            List<Exception> exceptions = new List<Exception>();

            if(String.IsNullOrEmpty(this.CurrencyCode))
            {
                exceptions.Add(new Exception("Currency Code must have a value which matches the currency of your merchant account."));
            }

            if(String.IsNullOrEmpty(this.MerchantReference))
            {
                exceptions.Add(new Exception("Merchant Reference must have a value."));
            } else if(this.MerchantReference?.Length > 100)
            {
                exceptions.Add(new Exception("Merchant Reference must not be longer than 100 characters."));
            } else if(!Regex.IsMatch(this.MerchantReference, @"^[ A-Za-z0-9@\-_=:?./]*$"))
            {
                exceptions.Add(new Exception("Merchant Reference must only contain alphanumeric characters and the following special characters: @-_=:?./"));
            }

            if(this.MerchantReferenceFormat?.Length > 50)
            {
                exceptions.Add(new Exception("Merchant Reference Format must not be longer than 50 characters."));
            }

            if(this.MerchantData?.Length > 2000)
            {
                exceptions.Add(new Exception("Merchant Data must not be longer than 2000 characters."));
            }

            if(String.IsNullOrEmpty(this.MerchantHomepageURL))
            {
                exceptions.Add(new Exception("Merchant Homepage URL must have a value."));
            } else if(this.MerchantHomepageURL?.Length > 1000)
            {
                exceptions.Add(new Exception("Merchant Homepage URL must not be longer than 1000 characters."));
            }

            if(String.IsNullOrEmpty(this.SuccessURL))
            {
                exceptions.Add(new Exception("Success URL must have a value."));
            } else if(this.SuccessURL?.Length > 1000)
            {
                exceptions.Add(new Exception("Success URL must not be longer than 1000 characters."));
            } else
            {
                Uri successUrl;
                if(Uri.TryCreate(this.SuccessURL, UriKind.Absolute, out successUrl))
                {
                    this.SuccessURL = successUrl.ToString();
                } else
                {
                    exceptions.Add(new Exception("Success URL is malformed"));
                }
                
            }

            if (this.FailureURL?.Length > 1000)
            {
                exceptions.Add(new Exception("Failure URL must not be longer than 1000 characters."));
            } else if(!String.IsNullOrEmpty(this.FailureURL))
            {
                Uri failureUrl;
                if (Uri.TryCreate(this.FailureURL, UriKind.Absolute, out failureUrl))
                {
                    this.FailureURL = failureUrl.ToString();
                }
                else
                {
                    exceptions.Add(new Exception("Failure URL is malformed"));
                }
            }

            if (this.CancellationURL?.Length > 1000)
            {
                exceptions.Add(new Exception("Cancellation URL must not be longer than 1000 characters."));
            } else if (!String.IsNullOrEmpty(this.CancellationURL))
            {
                Uri cancellationUrl;
                if (Uri.TryCreate(this.CancellationURL, UriKind.Absolute, out cancellationUrl))
                {
                    this.CancellationURL = cancellationUrl.ToString();
                }
                else
                {
                    exceptions.Add(new Exception("Cancellation URL is malformed"));
                }
            }

            if (this.NotificationURL?.Length > 1000)
            {
                exceptions.Add(new Exception("Notification URL must not be longer than 1000 characters."));
            } else if (!String.IsNullOrEmpty(this.NotificationURL))
            {
                Uri notificationUrl;
                if (Uri.TryCreate(this.NotificationURL, UriKind.Absolute, out notificationUrl))
                {
                    this.NotificationURL = notificationUrl.ToString();
                }
                else
                {
                    exceptions.Add(new Exception("Notification URL is malformed"));
                }
            }


            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
