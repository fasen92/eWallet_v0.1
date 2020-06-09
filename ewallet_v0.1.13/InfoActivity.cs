using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace ewallet_v0._1._13
{
    [Activity(Label = "InfoActivity")]
    public class InfoActivity : AppCompatActivity
    {
        public static void startActivity(Context context)
        {
            Intent intent = new Intent(context, typeof(NakupActivity));
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.info_activity_layout);

            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }
    }
}