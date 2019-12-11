using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Nest;
using System.Text;
using System.Linq;
using Elasticsearch.Net;

namespace Elasticsearch
{

    class Program
    {
        /// <summary>
        /// Basic connection to server. If we connect with a simple node we could skip the SinglenodeconnectionPool, it will be added by default.
        /// But it's not advised to do that !!
        /// clientP = Connection to the pool of nodes.
        /// Client = simple connection. If single node the proces is exactly the same as a clientP a little bit shorter
        /// clientU = connection to range of nodes
        /// </summary>
        /// <param name="args"></param>
        
        public static CatResponse<CatIndicesRecord> lecat { get; private set; }

        static void Main(string[] args)
        {
            try {
                Uri node = new Uri("http://localhost:9200");
                SingleNodeConnectionPool pool = new SingleNodeConnectionPool(node);
                var config = new ConnectionConfiguration(pool);
                var settings = new ConnectionSettings(node)
                    .DefaultIndex("letest")
                    .PrettyJson();
                var clientP = new ElasticClient(new ConnectionSettings(pool));
                ElasticClient client = new ElasticClient(settings);


                // Connecting different nodes to pool and afterward get the clients. It allows sniffing options and more maneuvrability to switch/add nodes
                // Expand sniffing KL !!!!

                var uris = Enumerable.Range(9200, 1)
                    .Select(port => new Uri($"http://localhost:{port}"));
                var poolU = new SniffingConnectionPool(uris);
                var clientU = new ElasticClient(new ConnectionSettings(poolU));
                var check = clientU.ConnectionSettings;
                var configU = new ConnectionConfiguration(poolU);
                configU.SniffLifeSpan(TimeSpan.FromMinutes(1));

                // check.SniffInformationLifeSpan(TimeSpan.FromMinutes(1));


                config.SniffLifeSpan(default);
                var monping = client.Ping();
                var s = pool.SniffedOnStartup;

                Console.WriteLine(s);
                if (monping.IsValid)
                    Console.WriteLine("c'est connecté");
                else
                    Console.WriteLine("c'est pas connecté");

                // !!! index sans caractere speciaux ni de majuscule !!!
               new Indexing(client);

                // --------------------------- Load Json file in server ---------------------------------------

               // new LoadJson(client);

                // --------------------------- Take information from Server -----------------------------------

                Search result = new Search();
                var data = result.search_res(client, "gender", "M");
                //var data1 = result.search_res(client, "gender", "F");
                //var data = result.search_res(clientU, "gender", "M");

                // --------------------------- Linq with data -------------------------------------------------

                /*new Linq(data, data1);*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
