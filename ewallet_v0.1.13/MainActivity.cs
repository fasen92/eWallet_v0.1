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

namespace ewallet_v0._1._13
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        List<Entry> tyzdenList;
        public static void startActivity(Context context)
        {
            // intent je objekt, ktorý sa odovzdáva novej aktivite a systém podľa toho vie, čo má spustiť.
            Intent intent = new Intent(context, typeof(MainActivity));

            // toto volanie spôsobí otvorenie novej aktivity
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (!NakupServis.getInstance().nacitajNakup())
            {
                NakupActivity.startActivity(this);
            }

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            tyzdenList = new List<Entry>();
            
            var vstup = new Entry(111);
            vstup.Label = "Kaufland";
            vstup.ValueLabel = "16,50";
            vstup.Color = SKColor.Parse("#266489");
            tyzdenList.Add(vstup);

            vstup = new Entry(250);
            vstup.Label = "tesco";
            vstup.ValueLabel = "12";
            vstup.Color = SKColor.Parse("#68B9C0");
            tyzdenList.Add(vstup);


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
                case Resource.Id.info:

                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}