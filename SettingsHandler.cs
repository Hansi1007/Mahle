﻿using System.IO;
using Newtonsoft.Json;

namespace Mahle
{
    public class SettingsHandler
    {
        //  Cannot be changed via interface
        public const string settingsFileName = "settings.json";
        public const string valuesFileName = "values.json";
        public const string resourcesPath = "Resources";
        public const string logFolder = "Logs";

        //  Can changed via interface
        private const string defOutputFolder = "Output";
        private const string defInputFolder = "Input";
        private const string defOriginalFolder = "Original Files";

        public static Settings settings;    //  structs to keep locations and values
        public static Values values;

        static SettingsHandler()
        {
            if (!Directory.Exists(resourcesPath))    // Creates Resources folder
            {
                Directory.CreateDirectory(resourcesPath);
            }

            if (!File.Exists(resourcesPath + "/" + settingsFileName))   // Creates settings file with default values
            {
                settings = new Settings();
                settings.OutputFolder = defOutputFolder;
                settings.InputFolder = defInputFolder;
                settings.OriginalFilesFolder = defOriginalFolder;

                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(resourcesPath + "/" + settingsFileName, json);
            }
            else    // Reads settings from json file
            {
                string json = File.ReadAllText(resourcesPath + "/" + settingsFileName);
                settings = JsonConvert.DeserializeObject<Settings>(json);
            }

            if (!File.Exists(resourcesPath + "/" + valuesFileName)) // Creates default test values if file doesnt exist
            {
                values = new Values();
                values.RealValues = "Test 1,Test 2,Test 3,Test 4";
                values.TargetValues = "Test Target 1,Test Target 2,Test Target 3,Test Target 4";

                string json = JsonConvert.SerializeObject(values, Formatting.Indented);
                File.WriteAllText(resourcesPath + "/" + valuesFileName, json);
            }
            else    // Read default values
            {
                string json = File.ReadAllText(resourcesPath + "/" + valuesFileName);
                values = JsonConvert.DeserializeObject<Values>(json);
            }

            if (!Directory.Exists(settings.InputFolder))    // If input directory doesnt exist creates the folder
            {
                Directory.CreateDirectory(settings.InputFolder);
            }

            if (!Directory.Exists(settings.OutputFolder))   // If output directory doesnt exist creates the folder
            {
                Directory.CreateDirectory(settings.OutputFolder);
            }

            if (!Directory.Exists(settings.OriginalFilesFolder))    // If Original files directory doesnt exist creates the folder
            {
                Directory.CreateDirectory(settings.OriginalFilesFolder);
            }

            if (!Directory.Exists(logFolder))   // If log directory doesnt exist creates the folder
            {
                Directory.CreateDirectory(logFolder);
            }
        }

        public static void SaveSettings()   // Saves current settings
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(resourcesPath + "/" + settingsFileName, json);
        }

        public static void RefreshSettings()    // Refresh settings
        {
            string json = File.ReadAllText(resourcesPath + "/" + settingsFileName);
            settings = JsonConvert.DeserializeObject<Settings>(json);
        }

        public static void RefreshValues()  // Refresh values
        {
            string json = File.ReadAllText(resourcesPath + "/" + valuesFileName);
            values = JsonConvert.DeserializeObject<Values>(json);
        }

        public struct Settings
        {
            public string OutputFolder;
            public string InputFolder;
            public string OriginalFilesFolder;
        }

        public struct Values
        {
            public string RealValues;
            public string TargetValues;
        }
    }
}
