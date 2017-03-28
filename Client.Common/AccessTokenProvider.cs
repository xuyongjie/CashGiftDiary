using Account.Client;
using Entity.ResultModel;
using System;
using System.Threading.Tasks;
using WebApi.Client;

namespace Client.Common
{
    class AccessTokenProvider : IAccessTokenProvider
    {
        AppSettings settings = AppSettings.GetInstance();

        public async Task<string> GetTokenAsync()
        {
            string cacheToken = settings.AccessToken;
            if(!string.IsNullOrEmpty(cacheToken))
            {
                return cacheToken;
            }
            string accessToken = await PasswordVaultLoginAsync();
            if (accessToken != null)
                return accessToken;

            TaskCompletionSource<string> accessTokenSource = new TaskCompletionSource<string>();
            //Frame.Navigate(typeof(LoginPage), accessTokenSource);
            return await accessTokenSource.Task;
        }

        public async Task<string> PasswordVaultLoginAsync()
        {
            var passwordCredential = settings.GetPasswordCredentials();
            if (passwordCredential != null)
            {
                HttpResult<TokenResult> result;
                using (AccountClient accountClient = ClientFactory.CreateAccountClient())
                {
                    result = await accountClient.LoginAsync(passwordCredential.Item1, passwordCredential.Item2);
                }
                if (result.Succeeded)
                {
                    return result.Content.ResponseData?.AccessToken;
                }
                settings.ClearPasswordCredentials();
            }
            return null;
        }
    }
}
