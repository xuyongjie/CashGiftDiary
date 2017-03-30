using Account.Client;
using Client.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewLayer;

namespace PresenterLayer
{
    public class RegisterPresenter : IRegisterPresenter
    {
        private AccountClient _accountClient;
        private IRegisterView _registerView;
        public RegisterPresenter(IRegisterView registerView)
        {
            _accountClient = ClientFactory.CreateAccountClient();
            _registerView = registerView;
        }
        public async Task RegisterAsync(string phone, string password, string confirmPassword, string verifyCode)
        {
            var registerResponse =await _accountClient.RegisterAsync(new RegisterUser
            {
                UserName=phone,
                Password=password,
                ConfirmPassword=confirmPassword,
                VerifyCode=verifyCode
            });
            if(registerResponse.Succeeded)
            {
                _registerView.setRegisterResult(registerResponse.Content?.StatusCode==Constant.SolutionConstant.STATUS_CODE_OK, registerResponse.Content?.Desc);
            }
            else
            {
                _registerView.setRegisterResult(false, registerResponse.Errors.ToString());
            }
        }
    }
}
