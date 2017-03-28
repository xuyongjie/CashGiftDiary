using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewLayer
{
    public interface IBaseView
    {
        Task ShowInfoDialog(string info);
        Task NavigateBack();
        Task NavigateTo(Type viewType);
    }
}
