using Account.Client;
using CashGiftDiary.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Common
{
    public class ClientFactory
    {
        public const string BaseAddress = "http://xyjvd.cloudapp.net:8086/";
        public static HttpClient CreateHttpClient()
        {
            AppSettings settings = new AppSettings();
            AccessTokenProvider loginProvider = new AccessTokenProvider();
            OAuth2BearerTokenHandler oauth2Handler = new OAuth2BearerTokenHandler(settings, loginProvider);
            HttpClient httpClient = HttpClientFactory.Create(oauth2Handler);
            httpClient.BaseAddress = new Uri(BaseAddress);
            httpClient.Timeout = TimeSpan.FromMinutes(2);
            return httpClient;
        }

        public static AccountClient CreateAccountClient()
        {
            return new AccountClient(CreateHttpClient());
        }

        public static CashGiftDiaryClient CreateCashGiftDiaryClient()
        {
            return new CashGiftDiaryClient(CreateHttpClient());
        }
    }
}
