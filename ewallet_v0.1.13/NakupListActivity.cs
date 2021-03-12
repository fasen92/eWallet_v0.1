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
using ewallet_v0._1._13.Servis;
using static Android.Widget.AdapterView;

namespace ewallet_v0._1._13
{
    [Activity(Label = "Zoznam nákupov")]
    class NakupListActivity : AppCompatActivity
    {
        ListView lvNakupList;
        NakupAdapter adapter;
        public static void StartActivity(Context context)
        {
            Intent intent = new Intent(context, typeof(NakupListActivity));
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle SavedInstanceState)
        {
            base.OnCreate(SavedInstanceState);
            SetContentView(Resource.Layout.nakup_list_activity_layout);

            SupportActionBar.Title = "Zoznam nákupov";

            lvNakupList = FindViewById<ListView>(Resource.Id.lvNakupList);

            NakupServis nakupServis = NakupServis.getInstance();

            adapter = new NakupAdapter(this, nakupServis.GetNakupList());
            lvNakupList.Adapter = adapter;

            lvNakupList.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                NakupDetailActivity.startActivity(this, e.Position);
            };
        }
        protected override void OnResume()
        {
            base.OnResume();
            // táto metóda vynúti prekreslenie celého listview
            // na tomto mieste je to pre prípad, že sa do aktivity vrátime z aktivity na editáciu
            adapter.NotifyDataSetChanged();
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

    }
}