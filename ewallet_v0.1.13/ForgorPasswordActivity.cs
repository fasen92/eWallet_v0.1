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
    [Activity(Label = "ForgorPasswordActivity")]
    public class ForgorPasswordActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private EditText input_email;
        private Button btnResetPass;
        private TextView btnBack;
        private RelativeLayout activity_forgot;

        FirebaseAuth auth;

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.forgot_btn_back)
            {
                MainActivity.startActivity(this);
                Finish();
            }else if (v.Id == Resource.Id.forgot_btn_reset)
            {
                ResetPasswprd(input_email.Text);
            }
        }

        private void ResetPasswprd(string email)
        {
            auth.SendPasswordResetEmail(email)
                .AddOnCompleteListener(this, this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.forgot_password_activity_layout);

            auth = FirebaseAuth.GetInstance(LoginActivity.app);

            input_email = FindViewById<EditText>(Resource.Id.forgot_email);
            btnResetPass = FindViewById<Button>(Resource.Id.forgot_btn_reset);
            btnBack = FindViewById<TextView>(Resource.Id.forgot_btn_back);
            activity_forgot = FindViewById<RelativeLayout>(Resource.Id.activity_forgot);

            btnResetPass.SetOnClickListener(this);
            btnBack.SetOnClickListener(this);
        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == false)
            {
                Snackbar snackbar = Snackbar.Make(activity_forgot, "Resetovanie hesla nebolo úspešné", Snackbar.LengthShort);
                snackbar.Show();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activity_forgot, "Link na resetovanie hesla bol zaslaný na email: "+input_email.Text, Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}