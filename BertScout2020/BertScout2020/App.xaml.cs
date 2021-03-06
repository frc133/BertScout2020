﻿using BertScout2020.Views;
using BertScout2020Data.Data;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BertScout2020
{
    public partial class App : Application
    {
        static public string BertColor = "#22BE1C";
        static public string HighlightColor = "#23DAFF";
        static public Color SelectedButtonColor = Color.FromHex(BertColor);
        static public Color UnselectedButtonColor = Color.LightGray;
        static public double dbVersion = 1.3;
        static public string OptionPassword = "letmein";
        static public string AppVersionDate = "2020.01.13.1909";
        static public string AppYear = "2020";

        // app properties for easy page communication
        static public string currFRCEventKey { get; set; }
        static public string currFRCEventName { get; set; }
        static public int currTeamNumber { get; set; }
        static public string currTeamName { get; set; }
        static public int currMatchNumber { get; set; }
        static public int highestMatchNumber { get; set; } = 0;
        static public string syncIpAddress { get; set; } = "";
        static public string kindleName { get; set; } = "00";

        // app database
        static public BertScout2020Database database;

        // http client for syncing
        static public HttpClient client; // = new HttpClient();

        // app properties saved by OnSleep()
        private const string propNameVersionNumber = "currentVersionNumber";
        private const string propNameFRCEventKey = "currentFRCEventKey";
        private const string propNameFRCEventName = "currentFRCEventName";
        private const string propNameHighestMatchNumber = "highestMatchNumber";
        private const string propNameIpAddress = "syncIpAddress";
        private const string propNameKindleName = "currKindleName";

        public App()
        {
            try
            {
                if (Properties.ContainsKey(propNameVersionNumber)
                    && (double)Properties[propNameVersionNumber] == dbVersion)
                {
                    if (Properties.ContainsKey(propNameFRCEventKey))
                    {
                        currFRCEventKey = (string)Properties[propNameFRCEventKey];
                    }
                    if (Properties.ContainsKey(propNameFRCEventName))
                    {
                        currFRCEventName = (string)Properties[propNameFRCEventName];
                    }
                    if (Properties.ContainsKey(propNameHighestMatchNumber))
                    {
                        highestMatchNumber = (int)Properties[propNameHighestMatchNumber];
                    }
                    if (Properties.ContainsKey(propNameIpAddress))
                    {
                        syncIpAddress = (string)Properties[propNameIpAddress];
                    }
                    if (Properties.ContainsKey(propNameKindleName))
                    {
                        kindleName = (string)Properties[propNameKindleName];
                    }
                }
            }
            catch (Exception)
            {
                Properties[propNameVersionNumber] = dbVersion;
                Properties[propNameFRCEventKey] = "";
                Properties[propNameFRCEventName] = "";
                Properties[propNameHighestMatchNumber] = 0;
                Properties[propNameIpAddress] = "";
                Properties[propNameKindleName] = "";
            }
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex(BertColor)
            };
        }

        public static BertScout2020Database Database
        {
            get
            {
                if (database == null)
                {
                    string myDocumentsPath = GetMyDocumentsPath();
                    database = new BertScout2020Database(
                        Path.Combine(myDocumentsPath, BertScout2020Database.dbFilename)
                        );
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            SaveProperties();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SaveProperties()
        {
            Properties[propNameVersionNumber] = dbVersion;
            Properties[propNameFRCEventKey] = currFRCEventKey;
            Properties[propNameFRCEventName] = currFRCEventName;
            Properties[propNameHighestMatchNumber] = highestMatchNumber;
            Properties[propNameIpAddress] = syncIpAddress;
            Properties[propNameKindleName] = kindleName;
        }

        public static string GetMyDocumentsPath()
        {
            string baseDocumentsPath = "";
            string myDocumentsPath = "";

            baseDocumentsPath = "/storage/sdcard0"; // android kindle
            if (!Directory.Exists(baseDocumentsPath))
            {
                baseDocumentsPath = "/storage/sdcard"; // android emulator
            }
            if (Directory.Exists(baseDocumentsPath))
            {
                myDocumentsPath = $"{baseDocumentsPath}/Documents";
                if (!Directory.Exists(myDocumentsPath))
                {
                    try
                    {
                        Directory.CreateDirectory(myDocumentsPath);
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }
            if (!Directory.Exists(myDocumentsPath))
            {
                myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // windows
            }
            if (!Directory.Exists(myDocumentsPath))
            {
                myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); // local data
            }
            return myDocumentsPath;
        }
    }
}
