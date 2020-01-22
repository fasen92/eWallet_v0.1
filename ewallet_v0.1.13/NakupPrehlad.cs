using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ewallet_v0._1._13
{
    class NakupPrehlad : AppCompatActivity
    {
        TextView txtObchod;
        TextView txtCena;
        TextView txtDatum;
        Button btnNovyNakup;
        Button btnGrafy;
        Button btnNakupList;

        public static void startActivity(Context context)
        {
            Intent intent = new Intent(context, typeof(NakupActivity));

            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle SavedInstanceState)
        {
            base.OnCreate(SavedInstanceState);
            SetContentView(Resource.Layout.nakup_activity_prehlad_layout);

           //txtObchod=FindViewById<TextView>(Resource.Id.)
        }
    }
}