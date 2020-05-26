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

namespace ewallet_v0._1._13.Servis
{
    class ObchodServis
    {
        private static ObchodServis instance;

        private readonly Context context;
        private List<Obchod> ObchodList;

        public static ObchodServis getInstance()
        {
            if (instance == null)
            {
                instance = new ObchodServis();
            }

            return instance;
        }

        private ObchodServis()
        {
            this.context = Android.App.Application.Context;
            this.ObchodList = new List<Obchod>();
            nacitajObchody();
        }

        public void pridajObchod(Obchod obchod)
        {
            ObchodList.Add(obchod);
            ulozObchodList();
        }

        /*public void vymazNakup(int index)
        {
            ObchodList.RemoveAt(index);
            ulozObchodList();
        }*/

        /*public void editObchod(Obchod obchod, int index)
        {
            ObchodList[index] = obchod;
            ulozObchodList();
        }*/


        public List<Obchod> GetNakupList()
        {
            return ObchodList;
        }


        private void nacitajObchody()
        {
            ObchodList.Clear();

            ISharedPreferences pref = Application.Context.GetSharedPreferences("serializedObchod", FileCreationMode.Private);
            string sSerializedObchody = pref.GetString("jsonObchod", String.Empty);

            if (sSerializedObchody == string.Empty)
            {
                Obchod obchod1 = new Obchod("Kaufland");
                ObchodList.Add(obchod1);
                Obchod obchod2 = new Obchod("Tesco");
                ObchodList.Add(obchod2);
                Obchod obchod3 = new Obchod("Billa");
                ObchodList.Add(obchod3);
                Obchod obchod4 = new Obchod("Jednota");
                ObchodList.Add(obchod4);
                return;
            }


            string[] serializedObchody = sSerializedObchody.Split("GJ6MF");
            foreach (var obchodS in serializedObchody)
            {
                var obchod = Newtonsoft.Json.JsonConvert.DeserializeObject<Obchod>(obchodS);

                ObchodList.Add(obchod);
            }

        }

        private void ulozObchodList()
        {
            string serializedObchody = "";
            foreach (var obchod in ObchodList)
            {
                if (ObchodList.IndexOf(obchod) == ObchodList.Count - 1)
                {
                    serializedObchody += Newtonsoft.Json.JsonConvert.SerializeObject(obchod);
                }
                else
                {
                    serializedObchody += Newtonsoft.Json.JsonConvert.SerializeObject(obchod) + "GJ6MF";
                }
            }

            ISharedPreferences pref = Application.Context.GetSharedPreferences("serializedObchod", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.PutString("jsonObchod", serializedObchody);
            edit.Apply();
        }


    }
}