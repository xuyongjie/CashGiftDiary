using Android.App;
using Android.Widget;
using Android.OS;
using ViewLayer;
using System;
using PresenterLayer;
using Android.Content;

namespace CashGiftDiary.Android
{
    [Activity(Label = "CashGiftDiary.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity,ILoginView
    {
        private ILoginPresenter _loginPresenter;
        public void SetLoginResult(string result)
        {
            FindViewById<TextView>(Resource.Id.tvLoginResult).SetText(result,TextView.BufferType.Normal);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _loginPresenter = new LoginPresenter(this);
            FindViewById<Button>(Resource.Id.btLogin).Click += LoginClick;
            FindViewById<Button>(Resource.Id.btRegister).Click += NavigateToRegisterClick;
            // Set our view from the "main" layout resource
        }

        private void NavigateToRegisterClick(object sender, EventArgs e)
        {
            var intent = new Intent(this,typeof(RegisterActivity));
            StartActivity(intent);
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            await _loginPresenter.LoginAsync(FindViewById<EditText>(Resource.Id.etPhone).Text, FindViewById<EditText>(Resource.Id.etPassword).Text);
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}

