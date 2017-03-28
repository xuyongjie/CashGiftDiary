using PresenterLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewLayer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CashGiftDiary.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page,ILoginView
    {
        private ILoginPresenter _loginPresenter;

        public MainPage()
        {
            this.InitializeComponent();
            _loginPresenter = new LoginPresenter(this);
        }

        public void SetLoginResult(string result)
        {
            LoginResultTextBlock.Text = result;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await _loginPresenter.LoginAsync(PhoneTextBox.Text, PasswordTextBox.Password);
        }
    }
}
