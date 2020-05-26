using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using ewallet_v0._1._13.Model;

namespace ewallet_v0._1._13.Servis
{
    class NakupServis
    {
        private static NakupServis instance;

        private readonly Context context;
        private List<Nakup> NakupList;

        public static NakupServis getInstance()
        {
            if(instance == null)
            {
                instance = new NakupServis();
            }

            return instance;
        }

        private NakupServis()
        {
            this.context = Android.App.Application.Context;
            this.NakupList = new List<Nakup>();
            nacitajNakupy();
        }

        public void pridajNakup(Nakup nakup)
        {
            NakupList.Add(nakup);
            ulozNakupList();
        }

        public void vymazNakup(int index)
        {
            NakupList.RemoveAt(index);
            ulozNakupList();
        }

        public void editNakup(Nakup nakup, int index)
        {
            NakupList[index] = nakup;
            ulozNakupList();
        }

        public bool emptyNakup()
        {
            if (NakupList.Count == 0)
                return true;
            else
                return false;
            
        }

        public List<Nakup> GetNakupList()
        {
            return NakupList;
        }


        private void ulozNakupList()
        {
            string serializedNakupy = "";
            foreach (var nakup in NakupList)
            {
                if(NakupList.IndexOf(nakup) == NakupList.Count - 1)
                {
                    serializedNakupy += Newtonsoft.Json.JsonConvert.SerializeObject(nakup);
                }
                else
                {
                    serializedNakupy += Newtonsoft.Json.JsonConvert.SerializeObject(nakup) + "GJ6MK";
                }
            }

            ISharedPreferences pref = Application.Context.GetSharedPreferences("serialized", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.PutString("jsonNakup", serializedNakupy);
            edit.Apply();
        }

        private void nacitajNakupy()
        {
            NakupList.Clear();

            ISharedPreferences pref = Application.Context.GetSharedPreferences("serialized", FileCreationMode.Private);
            string sSerializedNakupy = pref.GetString("jsonNakup", String.Empty);

            if (sSerializedNakupy == string.Empty)
                return;
        

            string[] serializedNakupy = sSerializedNakupy.Split("GJ6MK");
            foreach(var nakupS in serializedNakupy)
            {
                var nakup = Newtonsoft.Json.JsonConvert.DeserializeObject<Nakup>(nakupS);

                NakupList.Add(nakup);
            }

        }

    }
}