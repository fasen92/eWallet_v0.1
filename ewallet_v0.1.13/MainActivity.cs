using Android.App;
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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        List<Entry> tyzdenList;
        List<Nakup> nakupList;
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

            /*
            vstup = new Entry(250);
            vstup.Label = "tesco";
            vstup.ValueLabel = "12";
            vstup.Color = SKColor.Parse("#68B9C0");
            tyzdenList.Add(vstup);*/


            //test grafov
            var entries = new[]
                {
                new Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                Color = SKColor.Parse("#266489")
                },
                new Entry(400)
                {
                Label = "February",
                ValueLabel = "400",
                Color = SKColor.Parse("#68B9C0")
                },
                new Entry(-100)
                {
                Label = "March",
                ValueLabel = "-100",
                Color = SKColor.Parse("#90D585")
                }
            };
            // var entriesObchod = new[];
            // for(int i = 1; i < )



            var chart = new BarChart() { Entries = entries };
            var chart1 = new PointChart() { Entries = entries };
            var chart2 = new DonutChart() { Entries = tyzdenList };

            var chartView = FindViewById<ChartView>(Resource.Id.chartView);
            chartView.Chart = chart;
            var chartView1 = FindViewById<ChartView>(Resource.Id.chartView1);
            chartView1.Chart = chart1;
            var chartView2 = FindViewById<ChartView>(Resource.Id.chartView2);
            chartView2.Chart = chart2;


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return true;
        }


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
                case Resource.Id.info:
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

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
                        for (int j = 0; j <opakovania ; j++)
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
    }
}