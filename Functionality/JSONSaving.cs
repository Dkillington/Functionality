using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Functionality
{
    public class JSONSaving
    {
        // Save a given object to a given JSON file
        public void Save(string fullDirectory, Object objectToSave)
        {
            CheckJSONSaveExists(fullDirectory);

            try
            {
                // Serialize an object into a string of valid JSON text
                string jsonString = JsonConvert.SerializeObject(objectToSave);

                // Write the JSON string to the given
                File.WriteAllText(fullDirectory, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serializing the JSON data: " + ex.Message);
            }
        }

        // Checks to see if a specified Json file exists, if not it creates one
        public void CheckJSONSaveExists(string fullDirectory, bool showText = false)
        {
            // Check if the file exists
            if (!File.Exists(fullDirectory))
            {
                using (FileStream fs = File.Create(fullDirectory))
                {
                    // FileStream is used to create the file, since previously saying "File.Create()" caused errors where the file was being accessed before it could be closed.
                }

                if (showText)
                {
                    Console.WriteLine($"JSON file did not exist...\nFile created at: {fullDirectory}");
                }
            }
            else
            {
                if (showText)
                {
                    Console.WriteLine($"JSON file found @{fullDirectory}");
                }
            }
        }

        // Returns if valid Json string or not
        public bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || // For object
                (strInput.StartsWith("[") && strInput.EndsWith("]")))   // For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



        // Loads JSON data into a given object
        public void Load<T>(string fullDirectory, out T objectToLoadInto, bool showText = true) where T : new()
        {
            // Await user input to proceed
            Functionality.Text text = new Functionality.Text();

            Console.Clear();
            CheckJSONSaveExists(fullDirectory);

            // Read JSON file text as a string
            string jsonData = File.ReadAllText(fullDirectory);
            if (!string.IsNullOrEmpty(jsonData))
            {
                // Convert the string text into an object of type T
                try
                {
                    objectToLoadInto = JsonConvert.DeserializeObject<T>(jsonData);
                    if(objectToLoadInto != null)
                    {
                        if(showText)
                        {
                            Console.WriteLine($"JSON file deserialized!");
                            text.PressAnyKey();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deserializing the JSON data: " + ex.Message);
                    objectToLoadInto = new T();
                    text.PressAnyKey();
                }
            }
            else
            {
                if (showText)
                {
                    Console.WriteLine("JSON file was empty, creating default object...");
                    text.PressAnyKey();
                }
                objectToLoadInto = new T();
            }
        }
    }
}
