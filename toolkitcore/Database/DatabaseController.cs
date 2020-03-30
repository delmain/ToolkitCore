﻿using System;
using System.IO;
using UnityEngine;
using Verse;

namespace ToolkitCore.Database
{
    [StaticConstructorOnStartup]
    public static class DatabaseController
    {
        static readonly string modFolder = "ToolkitCore";
        public static readonly string dataPath = Application.persistentDataPath + $"/{modFolder}/";

        static DatabaseController()
        {
            Main();
        }

        static void Main()
        {
            if (!Directory.Exists(dataPath)) 
                Directory.CreateDirectory(dataPath);
        }

        public static void SaveToolkit()
        {

        }

        public static void LoadToolkit()
        {

        }

        public static bool SaveFile(string json, string fileName)
        {
            Log.Message(json);
            try
            {
                using (StreamWriter streamWriter = File.CreateText(Path.Combine(dataPath, fileName)))
                {
                    streamWriter.Write(json.ToString());
                }
            }
            catch (IOException e)
            {
                Log.Error(e.Message);
                return false;
            }

            return true;
        }

        public static bool LoadFile(string fileName, out string json)
        {
            json = null;

            var file = Path.Combine(dataPath, fileName);
            if (!File.Exists(file)) return false;

            try
            {
                using (StreamReader streamReader = File.OpenText(file))
                {
                    json = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Log.Warning(e.Message);
                return false;
            }

            return true;
        }
    }
}
