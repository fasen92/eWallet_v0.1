using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ewallet_v0._1._13.Model;
using ewallet_v0._1._13.Servis;

namespace ewallet_v0._1._13
{
    [Activity(Label = "NakupDetailActivity")]
    public class NakupDetailActivity : AppCompatActivity, DatePickerDialog.IOnDateSetListener
    {
        private static readonly string ID_NAKUP = "ID_NAKUP";
        private const int DATE_DIALOG = 1;
        Button btnDatum;
        Button btnSave;
        Button btnDelete;
        EditText txtVydaj;
        EditText txtObchod;
        TextView txtDatum;
        int den, mesiac, rok;
        int idNakup;

        public static void startActivity(Context context, int idNakup)
        {
            Intent intent = new Intent(context, typeof(NakupDetailActivity));
            intent.PutExtra(ID_NAKUP, idNakup);
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.nakup_activity_detail_layout);

            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            btnDatum = FindViewById<Button>(Resource.Id.btnDatumDetail);
            btnSave = FindViewById<Button>(Resource.Id.btnSaveDetail);
            btnDelete = FindViewById<Button>(Resource.Id.btnDeleteDetail);
            txtObchod = FindViewById<EditText>(Resource.Id.txtObchodDetail);
            txtVydaj = FindViewById<EditText>(Resource.Id.txtCenaDetail);
            txtDatum = FindViewById<TextView>(Resource.Id.txtDatumDetail);

            btnSave.Click += delegate
            {
                ulozit();
            };

           

            idNakup = Intent.GetIntExtra(ID_NAKUP, -1);
            if(idNakup >= 0)
            {
                IList<Nakup> nakupList = NakupServis.getInstance().GetNakupList();
                Nakup nakup = nakupList[idNakup];

                txtObchod.Text = nakup.obchodNakup;
                txtVydaj.Text = nakup.vydajNakup.ToString();
                txtDatum.Text = nakup.den + "." + nakup.mesiac + "." + nakup.rok;
                den = nakup.den;
                mesiac = nakup.mesiac;
                rok = nakup.rok;
            }

            btnDelete.Click += delegate
            {
                vymazat();
            };

            //button cez ktorý otvoríme dialog na dátum
            btnDatum.Click += delegate
            {
#pragma warning disable CS0618 // Type or member is obsolete
                ShowDialog(DATE_DIALOG);
#pragma warning restore CS0618 // Type or member is obsolete
            };

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

#pragma warning disable CS0672 // Member overrides obsolete member
        protected override Dialog OnCreateDialog(int id)
#pragma warning restore CS0672 // Member overrides obsolete member
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


        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            //tu nastav čo sa ma stať po zadani datumu
            rok = year;
            mesiac = month+1;
            den = dayOfMonth;
            txtDatum.Text =den + "." + mesiac + "." + rok;
        }


        private void ulozit()
        {
            string obchodNakupu = txtObchod.Text;
            double vydajNakupu = double.Parse(txtVydaj.Text, CultureInfo.InvariantCulture);

            Nakup nakup = new Nakup(obchodNakupu, vydajNakupu, den, mesiac, rok);
            NakupServis.getInstance().editNakup(nakup, idNakup);
            Finish();
        }

        private void vymazat()
        {
            NakupServis.getInstance().vymazNakup(idNakup);
            Finish();
        }
    }
}