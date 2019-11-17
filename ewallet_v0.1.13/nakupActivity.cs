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
    [Activity(Label = "Nový nákup")]
    public class NakupActivity : AppCompatActivity, DatePickerDialog.IOnDateSetListener
    {
        private const int DATE_DIALOG = 1;
        private int rok, mesiac, den;
        Button btnDatum;
        TextView txtDatum;
        

        public static void startActivity(Context context)
        {
            // intent je objekt, ktorý sa odovzdáva novej aktivite a systém podľa toho vie, čo má spustiť.
            Intent intent = new Intent(context, typeof(NakupActivity));

            // toto volanie spôsobí otvorenie novej aktivity
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AktualnyDatum();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.nakup_activity_layout);

            // týmto zapnem v ActionBare šípku späť
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            btnDatum = FindViewById<Button>(Resource.Id.btnDatum);
            txtDatum = FindViewById<TextView>(Resource.Id.txtDatum);

            //button cez ktorý otvoríme dialog na dátum
            btnDatum.Click += delegate
            {
                ShowDialog(DATE_DIALOG);
            };
        }

        //nastavi aktualny datum
        public void AktualnyDatum()
        {
            den = int.Parse(DateTime.Now.ToString("dd"));
            mesiac = int.Parse(DateTime.Now.ToString("MM"));
            mesiac = mesiac - 1;
            rok = int.Parse(DateTime.Now.ToString("yyyy"));
           
        }

        protected override Dialog OnCreateDialog(int id)
        {
            //dialog na zadanie datumu
            switch (id)
            {
                case DATE_DIALOG:
                    {
                        return new DatePickerDialog(this, this, rok, mesiac, den);
                    }
                default:
                    break;
            }
            return null;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // toto znamená, že som stlačil šípku späť v action bare
            if (item.ItemId == Android.Resource.Id.Home)
            {
                // toto je to isté, akoby som stlačil "späť" na telefóne
                OnBackPressed();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            //tu nastav čo sa ma stať po zadani datumu
            rok = year;
            mesiac = month;
            den = dayOfMonth;
            txtDatum.Text = "Dátum: " + den + "." + (mesiac+1) + "." + rok;
        }
    }
}