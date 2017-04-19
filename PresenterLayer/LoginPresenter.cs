using Account.Client;
using Client.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ViewLayer;
using System.Threading.Tasks;
using Constant;

namespace PresenterLayer
{
    public class LoginPresenter:ILoginPresenter
    {
        private ILoginView _loginView;
        private AccountClient _accountClient;
        public LoginPresenter(ILoginView loginView)
        {
            _loginView = loginView;
            _accountClient = ClientFactory.CreateAccountClient();
        }
        public async Task LoginAsync(string phone, string password)
        {
            var response=await _accountClient.LoginAsync(phone, password);
            if(response.Succeeded&&response.Content.StatusCode==SolutionConstant.STATUS_CODE_OK)
            {
                _loginView.SetLoginResult("登录成功\ntoken:"+response.Content?.ResponseData?.AccessToken);
            }
            else
            {
                _loginView.SetLoginResult("登录失败\n"+response.Errors[0]+response.Content?.Desc);
            }
        }
    }
}
