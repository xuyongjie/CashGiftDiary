using System;
using System.Threading.Tasks;

namespace PresenterLayer
{
    public interface ILoginPresenter
    {
        Task LoginAsync(string phone, string password);
    }
}
