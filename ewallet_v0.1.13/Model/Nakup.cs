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

namespace ewallet_v0._1._13.Model
{
    class Nakup
    {
        public string obchodNakup { get;  }
        public double vydajNakup { get; }
        public int den { get; }
        public int mesiac { get; }
        public int rok { get; }



        public Nakup(string obchodNakup, double vydajNakup, int den, int mesiac, int rok)
        {
            this.obchodNakup = obchodNakup;
            this.vydajNakup = vydajNakup;
            this.den = den;
            this.mesiac = mesiac;
            this.rok = rok;
        }
    }
}