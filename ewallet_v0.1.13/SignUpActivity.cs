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
using Firebase.Auth;
using static Android.Views.View;

namespace ewallet_v0._1._13
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : AppCompatActivity,IOnClickListener, IOnCompleteListener
    {
        Button btnSignup;
        TextView btnLogin, btnForgotPass;
        EditText input_email, input_pass;
        RelativeLayout activity_sign_up;

        FirebaseAuth auth;

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.signup_btn_login)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
                Finish();
            }else if (v.Id == Resource.Id.signup_btn_forgot_password)
            {
                StartActivity(new Intent(this, typeof(ForgorPasswordActivity)));
                Finish();
            }else if (v.Id == Resource.Id.signup_btn_register)
            {
                SignUpUser(input_email.Text, input_pass.Text);
            }
        }

        private void SignUpUser(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this, this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sign_up_activity_layout);

            auth = FirebaseAuth.GetInstance(LoginActivity.app);

            btnSignup = FindViewById<Button>(Resource.Id.signup_btn_register);
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            btnForgotPass = FindViewById<TextView>(Resource.Id.signup_btn_forgot_password);
            input_email = FindViewById<EditText>(Resource.Id.signup_email);
            input_pass = FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);

            btnLogin.SetOnClickListener(this);
            btnForgotPass.SetOnClickListener(this);
            btnSignup.SetOnClickListener(this);

        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                Snackbar snackbar = Snackbar.Make(activity_sign_up, "Registrácia bola úspešná", Snackbar.LengthShort);
                snackbar.Show();
                StartActivity(new Intent(this, typeof(LoginActivity)));
                Finish();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activity_sign_up, "Registrácia nebola úspešná", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}