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
    public class nakupActivity : AppCompatActivity
    {
        public static void startActivity(Context context)
        {
            // intent je objekt, ktorý sa odovzdáva novej aktivite a systém podľa toho vie, čo má spustiť.
            Intent intent = new Intent(context, typeof(nakupActivity));

            // toto volanie spôsobí otvorenie novej aktivity
            context.StartActivity(intent);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // týmto zapnem v ActionBare šípku späť
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
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

    }
}