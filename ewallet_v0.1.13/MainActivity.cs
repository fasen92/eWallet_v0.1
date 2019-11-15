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

namespace ewallet_v0._1._13
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


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

            var chart = new BarChart() { Entries = entries };
            var chart1 = new PointChart() { Entries = entries };
            var chart2 = new DonutChart() { Entries = entries };

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
                    nakupActivity.startActivity(this);
                    return true;
                case Resource.Id.info:
                    
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}