using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ewallet_v0._1._13.Model;

namespace ewallet_v0._1._13
{
    class NakupAdapter : BaseAdapter<Nakup>
    {
        private Activity activity;
        private IList<Nakup> nakupList;

        public NakupAdapter(Activity activity, IList<Nakup> nakupList)
        {
            this.activity = activity;
            this.nakupList = nakupList;
        }

        public override Nakup this[int position] => nakupList[position];

        public override int Count => nakupList.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            LayoutInflater inflater = activity.LayoutInflater;
            if (row == null)
            {
                row = inflater.Inflate(Resource.Layout.item_nakup, parent, false);
            }

            TextView tvObchod = row.FindViewById<TextView>(Resource.Id.txtObchodList);
            TextView tvCena = row.FindViewById<TextView>(Resource.Id.txtCenaList);
            TextView tvDatum = row.FindViewById<TextView>(Resource.Id.txtDatumList);

            Nakup nakup = nakupList[position];
            tvObchod.Text = nakup.obchodNakup;
            tvCena.Text = nakup.vydajNakup.ToString();
            tvDatum.Text = nakup.den + "." + nakup.mesiac + "." + nakup.rok;

            return row;
        }





    }
}