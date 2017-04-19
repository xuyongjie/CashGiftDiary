using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewLayer;
using Windows.UI.Xaml.Controls;

namespace CashGiftDiary.UWP.View
{
    public class BasePage : Page, IBaseView
    {
        public Task NavigateBack()
        {
            return Task.Run(() =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            });
        }

        public Task NavigateTo(Type viewType)
        {
            return Task.Run(() =>
            {
                Frame.Navigate(viewType);
            });
        }

        public virtual async Task ShowInfoDialog(string info)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Common Info",
                Content = info,
                PrimaryButtonText="Ok"
            };
            await dialog.ShowAsync();
        }
    }
}
