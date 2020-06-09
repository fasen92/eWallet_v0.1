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
using Newtonsoft.Json;
using ewallet_v0._1._13.Model;

namespace ewallet_v0._1._13
{
    [Activity(Label = "Nakup prehľad")]
    class NakupPrehlad : AppCompatActivity
    {
        TextView txtObchod;
        TextView txtCena;
        TextView txtDatum;
        TextView txtKategoria;
        Button btnNovyNakup;
        Button btnGrafy;
        Button btnNakupList;

        //string nakupPrehladJson;

        
        public static void startActivity(Context context)
        {     
            Intent intent = new Intent(context, typeof(NakupPrehlad));

            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle SavedInstanceState)
        {
            base.OnCreate(SavedInstanceState);
            SetContentView(Resource.Layout.nakup_activity_prehlad_layout);

            

            txtObchod = FindViewById<TextView>(Resource.Id.txtObchodPrehlad);
            txtCena = FindViewById<TextView>(Resource.Id.txtCenaPrehlad);
            txtDatum = FindViewById<TextView>(Resource.Id.txtDatumPrehlad);
            txtKategoria = FindViewById<TextView>(Resource.Id.txtKatPrehlad);
            btnGrafy = FindViewById<Button>(Resource.Id.btnGrafy);
            btnNakupList = FindViewById<Button>(Resource.Id.btnNakupList);
            btnNovyNakup = FindViewById<Button>(Resource.Id.btnNovyNakup);


            //nakupPrehladJson = Intent.GetStringExtra("Nakup");
            Nakup nakup = JsonConvert.DeserializeObject<Nakup>(Intent.GetStringExtra("Nakup"));
            txtObchod.Text = nakup.obchodNakup;
            txtCena.Text = nakup.vydajNakup.ToString();
            txtKategoria.Text = nakup.kategoria;
            txtDatum.Text = nakup.den + "." + nakup.mesiac + "." + nakup.rok;



            btnGrafy.Click += delegate
            {
                MainActivity.startActivity(this);
                Finish();
            };

            btnNakupList.Click += delegate
            {
                NakupListActivity.StartActivity(this);
                Finish();
            };

            btnNovyNakup.Click += delegate
            {
                NakupActivity.startActivity(this);
                Finish();
            };
        }
    }
}