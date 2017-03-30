using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ViewLayer;
using PresenterLayer;

namespace CashGiftDiary.Android
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity,IRegisterView
    {
        private IRegisterPresenter _registerPresenter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);
            // Create your application here
            _registerPresenter = new RegisterPresenter(this);
            FindViewById<Button>(Resource.Id.btRegister).Click += RegisterClick;
        }

        private async void RegisterClick(object sender, EventArgs e)
        {
            await _registerPresenter.RegisterAsync(FindViewById<EditText>(Resource.Id.etPhone).Text, FindViewById<EditText>(Resource.Id.etPassword).Text,
                FindViewById<EditText>(Resource.Id.etConfirmPassword).Text, FindViewById<EditText>(Resource.Id.etVerifyCode).Text);
        }

        public void setRegisterResult(bool result, string error)
        {
            if(result)
            {
                Finish();
            }
            else
            {
                FindViewById<TextView>(Resource.Id.tvRegisterResult).SetText(error, TextView.BufferType.Normal);
            }
        }

    }
}