using System;
using System.Collections.Generic;
using System.Text;
using Nest;
using System.IO;
using Newtonsoft.Json;

namespace Elasticsearch
{
    /// <summary>
    /// Load a Jsonfile from a specific place on your computer and Deserialize 
    /// the file so it can be converted in a list of objects
    /// </summary>
    class LoadJson
    {
        public LoadJson(ElasticClient client)
        {
            
                     string path = @"c:\Users\sofiane\Desktop\data.json";
                     string path2 = @"c:\Users\sofiane\Desktop\accounts1.json";

                     Console.WriteLine("le chemin au dossier" + path);
                     Console.WriteLine();

                     List<Test> datasO = new List<Test>();
                     List<Accounts> datas1 = new List<Accounts>();

                     using (StreamReader r = new StreamReader(path))
                     {
                         string json = r.ReadToEnd();
                         datasO = JsonConvert.DeserializeObject<List<Test>>(json);
                     }
                     foreach (Test data in datasO)
                     {
                         Console.WriteLine(data.url);
                     }
                     using (StreamReader r = new StreamReader(path2))
                     {
                         try
                         {
                             string json = r.ReadToEnd();
                             datas1 = JsonConvert.DeserializeObject<List<Accounts>>(json);
                         }
                         catch (Exception ex)
                         {
                             Console.WriteLine("ici que ca deconne");
                         }
                     }
                     var indexManyResponse2 = client.IndexMany(datas1, "people");
        }
    }
}
