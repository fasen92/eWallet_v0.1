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

        string nakupySave;
        string nakupyLoad;

        private List<Nakup> nakupList;
        
        public static NakupServis getInstance()
        {
            Log.Debug("NakupServis", "getInstance");

            if(instance == null)
            {
                instance = new NakupServis();
            }

            return instance;
        }

        private NakupServis()
        {
            nakupList = new List<Nakup>();
            if (nacitajNakup())
            {
                nakupyLoad = nakupLoad();
                nakupList = JsonConvert.DeserializeObject<List<Nakup>>(nakupySave);
            }
            
        }

        public void ulozNakupList(List<Nakup> nakupList)
        {
            nakupySave = JsonConvert.SerializeObject(nakupList, Formatting.Indented);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("nakupListSave", FileCreationMode.Private);
            ISharedPreferencesEditor edit = pref.Edit();
            edit.PutString("jsonNakup", nakupySave);
            edit.Apply();
        }

        public bool nacitajNakup()
        {
            string nakupyLoad = nakupLoad();
            if(nakupyLoad == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }

        public string nakupLoad()
        {
            ISharedPreferences pref = Application.Context.GetSharedPreferences("nakupListSave", FileCreationMode.Private);
            nakupyLoad = pref.GetString("nakupListSave", String.Empty);
            return nakupyLoad;
        }

        public void pridajNakup(Nakup novyNakup)
        {
            nakupList.Add(novyNakup);
        }

        //developing 
        /*public void aktualizujNakup(int idNakupu, Nakup nakup)
        {
            nakupList[idNakupu] = nakup;
        }*/










        /**
         * Vráti zoznam otázok, ktorý je určený len na čítanie - nedá sa meniť, aby zodpovednosť na zmeny zostala len v tomto servise
         * */
         public IList<Nakup> getNakupy()
        {
            return nakupList.AsReadOnly();
        }


    }
}