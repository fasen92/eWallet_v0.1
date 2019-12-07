﻿using System;
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
    [Activity(Label = "Nový nákup")]
    public class NakupActivity : AppCompatActivity, DatePickerDialog.IOnDateSetListener
    {
        private const int DATE_DIALOG = 1;
        private int rok, mesiac, den;
        Button btnDatum;
        Button btnSave;
        EditText txtVydaj;
        EditText txtObchod;
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
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            txtObchod = FindViewById<EditText>(Resource.Id.txtObchod);
            txtVydaj = FindViewById<EditText>(Resource.Id.txtCena);
            txtDatum = FindViewById<TextView>(Resource.Id.txtDatum);


            //button cez ktorý otvoríme dialog na dátum
            btnDatum.Click += delegate
            {
#pragma warning disable CS0618 // Type or member is obsolete
                ShowDialog(DATE_DIALOG);
#pragma warning restore CS0618 // Type or member is obsolete
            };

            btnSave.Click += delegate
            {
                if (txtObchod.Text == "")
                {
                    Toast.MakeText(this, "Názov obchodu nebol zadaný, prosím zadajte názov obchodu.", ToastLength.Long).Show();

                } else if(txtVydaj.Text == "")
                {
                    Toast.MakeText(this, "Cena nákupu nebola zadaná, prosím zadajte cenu nákupu.", ToastLength.Long).Show();
                }
                else
                {
                    ulozit();
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    DialogNakup dialogNakup = new DialogNakup();
                    dialogNakup.Show(transaction, "dialog fragment");
                    
                }

               
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

        //ulozenie do objektu
        private void ulozit()
        {
            string obchodNakupu = txtObchod.Text;
            double vydajNakupu = double.Parse(txtVydaj.Text, CultureInfo.InvariantCulture);
            int denNakupu = den;
            int mesiacNakupu = mesiac;
            int rokNakupu = rok;

            Nakup nakup = new Nakup(obchodNakupu, vydajNakupu, den, mesiac, rok);
            NakupServis.getInstance().pridajNakup(nakup);
        }
    }
}