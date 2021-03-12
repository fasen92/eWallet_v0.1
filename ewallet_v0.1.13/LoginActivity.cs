using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using static Android.Views.View;

namespace ewallet_v0._1._13
{
    [Activity(Label = "eWallet", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity,IOnClickListener, IOnCompleteListener
    {
        Button btnLogin;
        EditText input_email, input_password;
        TextView btnSignUp, BtnForgotPassword;

        RelativeLayout activty_login;

        public static FirebaseApp app;
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity_layout);

            InitFirebaseAuth();

            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            BtnForgotPassword = FindViewById<TextView>(Resource.Id.login_btn_forgot_password);
            activty_login = FindViewById<RelativeLayout>(Resource.Id.activity_login);

            btnSignUp.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
            BtnForgotPassword.SetOnClickListener(this);
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
            .SetApplicationId("1:8503161812:android:d3cf85e02912385ebd5c41")
            .SetApiKey("AIzaSyC2bkLfUKFvD7ouxUc9gtD6oFwAlP6lLXg")
            .Build();

            if(app == null)
            {
                app = FirebaseApp.InitializeApp(this, options);
            }

            auth = FirebaseAuth.GetInstance(app);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.login_btn_forgot_password)
            {
                StartActivity(new Android.Content.Intent(this, typeof(ForgorPasswordActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.login_btn_sign_up)
            {
                StartActivity(new Android.Content.Intent(this, typeof(SignUpActivity)));
                Finish();
            }else if(v.Id == Resource.Id.login_btn_login)
            {
                LoginUser(input_email.Text, input_password.Text);
            }
        }

        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                MainActivity.startActivity(this);
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activty_login, "Prihlásenie nebolo úspešné", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}