using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PresenterLayer
{
    public interface IRegisterPresenter
    {
        Task RegisterAsync(string phone, string password, string passwordConfirm, string verifyCode);
    }
}
