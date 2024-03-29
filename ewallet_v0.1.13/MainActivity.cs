﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;
using Microcharts.Droid;
using System.Collections.Generic;
using Android.Views;
using Android.Util;
using ewallet_v0._1._13.Servis;
using Android.Content;
using ewallet_v0._1._13.Model;
using System;
using System.Linq;

namespace ewallet_v0._1._13
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        List<Entry> tyzdenList;
        List<Entry> tyzdenListKat;
        List<Entry> mesiacList;
        List<Entry> mesiacListKat;
        List<Nakup> nakupList;
        List<Entry> tyzdenDatumList;
        List<Entry> tyzdenDatumListKat;
        TextView txtOznam7dni;
        TextView txtOznamTyzden;
        TextView txtOznamMesiac;
        LinearLayout LnrLayTyzden;
        LinearLayout LnrLayTyzdenKat;
        LinearLayout LnrLayLast7;
        LinearLayout LnrLayLast7Kat;
        LinearLayout LnrLayMesiac;
        LinearLayout LnrLayMesiacKat;

        
        String[] farby;
        public static void startActivity(Context context)
        {
            Intent intent = new Intent(context, typeof(MainActivity));

            context.StartActivity(intent);
        }

        class sumaNakupov
        {
            public string obchodSumaNakup;
            public double vydajSumaNakup;

            public sumaNakupov(string obchodSumaNakup, double vydajSumaNakup)
            {
                this.obchodSumaNakup = obchodSumaNakup;
                this.vydajSumaNakup = vydajSumaNakup;
            }

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (NakupServis.getInstance().emptyNakup())
            {
                NakupActivity.startActivity(this);
                Finish();
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            nakupList = NakupServis.getInstance().GetNakupList();

            farby = new string[] { "#19152d", "#342a4c", "#424272", "#5c7677", "#7a9968", "#9abc51", "#b4cc49", "#e9ffbf" };

            txtOznam7dni = FindViewById<TextView>(Resource.Id.txtOznam7dni);
            txtOznam7dni.Visibility = Android.Views.ViewStates.Gone;
            txtOznamTyzden = FindViewById<TextView>(Resource.Id.txtOznamTyzden);
            txtOznamTyzden.Visibility = Android.Views.ViewStates.Gone;
            txtOznamMesiac = FindViewById<TextView>(Resource.Id.txtOznamMesiac);
            txtOznamMesiac.Visibility = Android.Views.ViewStates.Gone;

            LnrLayTyzden = FindViewById<LinearLayout>(Resource.Id.LnrLayTyzden);
            LnrLayTyzdenKat = FindViewById<LinearLayout>(Resource.Id.LnrLayTyzdenKat);
            LnrLayLast7 = FindViewById<LinearLayout>(Resource.Id.LnrLayLast7);
            LnrLayLast7Kat = FindViewById<LinearLayout>(Resource.Id.LnrLayLast7kat);
            LnrLayMesiac = FindViewById<LinearLayout>(Resource.Id.LnrLayMonth);
            LnrLayMesiacKat = FindViewById<LinearLayout>(Resource.Id.LnrLayMonthkat);

            tyzdenList = new List<Entry>();
            List<sumaNakupov> tyzdenSuma = new List<sumaNakupov>();
            tyzdenSuma = SumaTyzden(nakupList);
            for (int i = 0; i < tyzdenSuma.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(tyzdenSuma[i].vydajSumaNakup));
                vstup.Label = tyzdenSuma[i].obchodSumaNakup;
                vstup.ValueLabel = tyzdenSuma[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                tyzdenList.Add(vstup);
            }

            tyzdenListKat = new List<Entry>();
            List<sumaNakupov> tyzdenSumaKat = new List<sumaNakupov>();
            tyzdenSumaKat = SumaTyzdenKat(nakupList);
            for (int i = 0; i < tyzdenSumaKat.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(tyzdenSumaKat[i].vydajSumaNakup));
                vstup.Label = tyzdenSumaKat[i].obchodSumaNakup;
                vstup.ValueLabel = tyzdenSumaKat[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                tyzdenListKat.Add(vstup);
            }

            mesiacList = new List<Entry>();
            List<sumaNakupov> mesiacSuma = new List<sumaNakupov>();
            mesiacSuma = SumaMesiac(nakupList);
            for (int i = 0; i < mesiacSuma.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(mesiacSuma[i].vydajSumaNakup));
                vstup.Label = mesiacSuma[i].obchodSumaNakup;
                vstup.ValueLabel = mesiacSuma[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                mesiacList.Add(vstup);
            }

            mesiacListKat = new List<Entry>();
            List<sumaNakupov> mesiacSumaKat = new List<sumaNakupov>();
            mesiacSumaKat = SumaMesiacKat(nakupList);
            for (int i = 0; i < mesiacSumaKat.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(mesiacSumaKat[i].vydajSumaNakup));
                vstup.Label = mesiacSumaKat[i].obchodSumaNakup;
                vstup.ValueLabel = mesiacSumaKat[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                mesiacListKat.Add(vstup);
            }

            tyzdenDatumList = new List<Entry>();
            List<sumaNakupov> tyzdenDatumSuma = new List<sumaNakupov>();
            tyzdenDatumSuma = SumaDatumTyzden(nakupList);
            for (int i = 0; i < tyzdenDatumSuma.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(tyzdenDatumSuma[i].vydajSumaNakup));
                vstup.Label = tyzdenDatumSuma[i].obchodSumaNakup;
                vstup.ValueLabel = tyzdenDatumSuma[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                tyzdenDatumList.Add(vstup);
            }

            tyzdenDatumListKat = new List<Entry>();
            List<sumaNakupov> tyzdenDatumSumaKat = new List<sumaNakupov>();
            tyzdenDatumSumaKat = SumaDatumTyzdenKat(nakupList);
            for (int i = 0; i < tyzdenDatumSumaKat.Count; i++)
            {
                var vstup = new Entry(Convert.ToSingle(tyzdenDatumSumaKat[i].vydajSumaNakup));
                vstup.Label = tyzdenDatumSumaKat[i].obchodSumaNakup;
                vstup.ValueLabel = tyzdenDatumSumaKat[i].vydajSumaNakup.ToString();
                vstup.Color = SKColor.Parse(farby[i]);
                tyzdenDatumListKat.Add(vstup);
            }

            var chartlast7 = new DonutChart() { Entries = tyzdenList, LabelTextSize=30f };
            var chartlast7kat = new DonutChart() { Entries = tyzdenListKat, LabelTextSize = 30f };
            var chartWeek = new DonutChart() { Entries =  tyzdenDatumList, LabelTextSize = 30f };
            var chartWeekkat = new DonutChart() { Entries = tyzdenDatumListKat, LabelTextSize = 30f };
            var chartMonth = new DonutChart() { Entries =  mesiacList, LabelTextSize = 30f };
            var chartMonthkat = new DonutChart() { Entries = mesiacListKat, LabelTextSize = 30f };

            //ošetrenie starých nákupov
            if (tyzdenSuma.Count != 0)
            {
                var chartViewLast7 = FindViewById<ChartView>(Resource.Id.chartViewLast7);
                chartViewLast7.Chart = chartlast7;
                var chartViewLast7kat = FindViewById<ChartView>(Resource.Id.chartViewLast7kat);
                chartViewLast7kat.Chart = chartlast7kat;
            }
            else
            {
                txtOznam7dni.Visibility = Android.Views.ViewStates.Visible;
                LnrLayLast7.Visibility = Android.Views.ViewStates.Gone;
                LnrLayLast7Kat.Visibility = Android.Views.ViewStates.Gone;
            }

            if (tyzdenDatumSuma.Count != 0)
            {
                var chartViewWeek= FindViewById<ChartView>(Resource.Id.chartViewWeek);
                chartViewWeek.Chart = chartWeek;
                var chartViewWeekkat = FindViewById<ChartView>(Resource.Id.chartViewWeekkat);
                chartViewWeekkat.Chart = chartWeekkat;
            }
            else
            {
                txtOznamTyzden.Visibility = Android.Views.ViewStates.Visible;
                LnrLayTyzden.Visibility = Android.Views.ViewStates.Gone;
                LnrLayTyzdenKat.Visibility = Android.Views.ViewStates.Gone;
            }
               
            if(mesiacSuma.Count != 0)
            {
                var chartViewMonth = FindViewById<ChartView>(Resource.Id.chartViewMonth);
                chartViewMonth.Chart = chartMonth;
                var chartViewMonthkat = FindViewById<ChartView>(Resource.Id.chartViewMonthkat);
                chartViewMonthkat.Chart = chartMonthkat;
            }
            else
            {
                txtOznamMesiac.Visibility = Android.Views.ViewStates.Visible;
                LnrLayMesiac.Visibility = Android.Views.ViewStates.Gone;
                LnrLayMesiacKat.Visibility = Android.Views.ViewStates.Gone;
            }
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return true;
        }

        //menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.novyNakup:
                    NakupActivity.startActivity(this);
                    return true;
                case Resource.Id.grafy:
                    MainActivity.startActivity(this);
                    return true;
                case Resource.Id.zoznamNakupov:
                    NakupListActivity.StartActivity(this);
                    return true;
                case Resource.Id.ucet:
                    UcetActivity.startActivity(this);
                    return true;
                case Resource.Id.info:
                    InfoActivity.startActivity(this);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        
        //list nakupov za poslednych 7 dni
        private List<sumaNakupov> SumaTyzden(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            List<sumaNakupov> sumaTyzdenList = new List<sumaNakupov>();
            DateTime[] last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();

            sumaTyzdenList.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, last7Days[0]);
                vysledok2 = DateTime.Compare(datumNakupu, last7Days[6]);
                if (vysledok1 <= 0 && vysledok2 >= 0)
                {

                    if (sumaTyzdenList.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                        sumaTyzdenList.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaTyzdenList.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaTyzdenList[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].obchodNakup.ToUpper().TrimEnd()))
                            {
                                sumaTyzdenList[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                            sumaTyzdenList.Add(suma);
                        }
                    }
                }
            }
            return sumaTyzdenList;
        }

        //list nakupov za poslednych 7 dni podla kateg.
        private List<sumaNakupov> SumaTyzdenKat(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            List<sumaNakupov> sumaTyzdenListKat = new List<sumaNakupov>();
            DateTime[] last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();

            sumaTyzdenListKat.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, last7Days[0]);
                vysledok2 = DateTime.Compare(datumNakupu, last7Days[6]);
                if (vysledok1 <= 0 && vysledok2 >= 0)
                {

                    if (sumaTyzdenListKat.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                        sumaTyzdenListKat.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaTyzdenListKat.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaTyzdenListKat[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].kategoria.ToUpper().TrimEnd()))
                            {
                                sumaTyzdenListKat[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                            sumaTyzdenListKat.Add(suma);
                        }
                    }
                }
            }
            return sumaTyzdenListKat;
        }

        //list nakupov za mesiac
        private List<sumaNakupov> SumaMesiac(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            List<sumaNakupov> sumaMesiacList = new List<sumaNakupov>();
            DateTime date = DateTime.Now;
            DateTime prvyDenMesiaca = new DateTime(date.Year, date.Month, 1);
            DateTime poslednyDenMesiaca = prvyDenMesiaca.AddMonths(1).AddDays(-1);

            sumaMesiacList.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, poslednyDenMesiaca);
                vysledok2 = DateTime.Compare(datumNakupu, prvyDenMesiaca);
                if (vysledok1 <= 0 && vysledok2 >= 0)
                {

                    if (sumaMesiacList.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                        sumaMesiacList.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaMesiacList.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaMesiacList[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].obchodNakup.ToUpper().TrimEnd()))
                            {
                                sumaMesiacList[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                            sumaMesiacList.Add(suma);
                        }
                    }
                }
            }
            return sumaMesiacList;
        }

        //list nakupov za mesiac podla kateg.
        private List<sumaNakupov> SumaMesiacKat(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            List<sumaNakupov> sumaMesiacListKat = new List<sumaNakupov>();
            DateTime date = DateTime.Now;
            DateTime prvyDenMesiaca = new DateTime(date.Year, date.Month, 1);
            DateTime poslednyDenMesiaca = prvyDenMesiaca.AddMonths(1).AddDays(-1);

            sumaMesiacListKat.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, poslednyDenMesiaca);
                vysledok2 = DateTime.Compare(datumNakupu, prvyDenMesiaca);
                if (vysledok1 <= 0 && vysledok2 >= 0)
                {

                    if (sumaMesiacListKat.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                        sumaMesiacListKat.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaMesiacListKat.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaMesiacListKat[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].kategoria.ToUpper().TrimEnd()))
                            {
                                sumaMesiacListKat[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                            sumaMesiacListKat.Add(suma);
                        }
                    }
                }
            }
            return sumaMesiacListKat;
        }

        //list nakupov za posledny kalendarny tyzden
        private List<sumaNakupov> SumaDatumTyzden(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            DateTime nedela = DateTime.Now;
            List<sumaNakupov> sumaDatumTyzdenList = new List<sumaNakupov>();
            DateTime[] last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();

            for(int i = 0; i <= last7Days.Length; i++)
            {
                if(last7Days[i].DayOfWeek == DayOfWeek.Sunday)
                {
                    nedela = last7Days[i];
                    break;
                }
            }

            DateTime[] kalendarnyTyzden = Enumerable.Range(0, 7)
                .Select(i => nedela.Date.AddDays(+i))
                .ToArray();

            sumaDatumTyzdenList.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, kalendarnyTyzden[0]);
                vysledok2 = DateTime.Compare(datumNakupu, kalendarnyTyzden[6]); 
                if (vysledok1 >= 0 && vysledok2 <= 0)
                {

                    if (sumaDatumTyzdenList.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                        sumaDatumTyzdenList.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaDatumTyzdenList.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaDatumTyzdenList[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].obchodNakup.ToUpper().TrimEnd()))
                            {
                                sumaDatumTyzdenList[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].obchodNakup, nakupList[i].vydajNakup);
                            sumaDatumTyzdenList.Add(suma);
                        }
                    }
                }
            }
            return sumaDatumTyzdenList;
        }

        //list nakupov za posledny kalendarny tyzden podla kateg.
        private List<sumaNakupov> SumaDatumTyzdenKat(List<Nakup> nakupList)
        {
            this.nakupList = nakupList;
            DateTime nedela = DateTime.Now;
            List<sumaNakupov> sumaDatumTyzdenListKat = new List<sumaNakupov>();
            DateTime[] last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .ToArray();

            for (int i = 0; i <= last7Days.Length; i++)
            {
                if (last7Days[i].DayOfWeek == DayOfWeek.Sunday)
                {
                    nedela = last7Days[i];
                    break;
                }
            }

            DateTime[] kalendarnyTyzden = Enumerable.Range(0, 7)
                .Select(i => nedela.Date.AddDays(+i))
                .ToArray();

            sumaDatumTyzdenListKat.Clear();
            for (int i = 0; i < nakupList.Count; i++)
            {
                DateTime datumNakupu = new DateTime(nakupList[i].rok, nakupList[i].mesiac, nakupList[i].den);
                int vysledok1 = 0;
                int vysledok2 = 0;
                vysledok1 = DateTime.Compare(datumNakupu, kalendarnyTyzden[0]);
                vysledok2 = DateTime.Compare(datumNakupu, kalendarnyTyzden[6]);
                if (vysledok1 >= 0 && vysledok2 <= 0)
                {

                    if (sumaDatumTyzdenListKat.Count == 0)
                    {
                        sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                        sumaDatumTyzdenListKat.Add(suma);
                    }
                    else
                    {
                        int opakovania = sumaDatumTyzdenListKat.Count;
                        bool podmienka = true;
                        for (int j = 0; j < opakovania; j++)
                        {
                            if (String.Equals(sumaDatumTyzdenListKat[j].obchodSumaNakup.ToUpper().TrimEnd(), nakupList[i].kategoria.ToUpper().TrimEnd()))
                            {
                                sumaDatumTyzdenListKat[j].vydajSumaNakup += nakupList[i].vydajNakup;
                                podmienka = false;
                                break;
                            }
                        }

                        if (podmienka)
                        {
                            sumaNakupov suma = new sumaNakupov(nakupList[i].kategoria, nakupList[i].vydajNakup);
                            sumaDatumTyzdenListKat.Add(suma);
                        }
                    }
                }
            }
            return sumaDatumTyzdenListKat;
        }

    }

}
