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
    [Activity(Label = "Účet")]
    public class UcetActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener 
    {
        TextView txtWelcome;
        EditText input_new_password;
        Button btnChangePass, btnLogout;
        RelativeLayout activity_dashboard;

        FirebaseAuth auth;

        public static void startActivity(Context context)
        {
            Intent intent = new Intent(context, typeof(UcetActivity));

            context.StartActivity(intent);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.dashboard_btn_change_pass)
            {
                ChangePassword(input_new_password.Text);
            }
            else if(v.Id == Resource.Id.dashboard_btn_logout)
            {
                LogoutUser();
            }
        }

        private void LogoutUser()
        {
            auth.SignOut();
            if(auth.CurrentUser == null)
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
            }
        }

        private void ChangePassword(string newPassword)
        {
            FirebaseUser user = auth.CurrentUser;
            user.UpdatePassword(newPassword)
                .AddOnCompleteListener(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ucet_activity_layout);

            auth = FirebaseAuth.GetInstance(LoginActivity.app);

            txtWelcome = FindViewById<TextView>(Resource.Id.dashboard_welcome);
            input_new_password = FindViewById<EditText>(Resource.Id.dashboard_newpassword);
            btnChangePass = FindViewById<Button>(Resource.Id.dashboard_btn_change_pass);
            btnLogout = FindViewById<Button>(Resource.Id.dashboard_btn_logout);
            activity_dashboard = FindViewById<RelativeLayout>(Resource.Id.activity_dashboard);

            btnChangePass.SetOnClickListener(this);
            btnLogout.SetOnClickListener(this);

            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            if (auth.CurrentUser != null)
            {
                txtWelcome.Text = auth.CurrentUser.Email;
            }

        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                Snackbar snackbar = Snackbar.Make(activity_dashboard, "Heslo bolo zmenené", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}