using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Nest;
using System.Text;
using System.Linq;

namespace Elasticsearch
{
    /// <summary>
    /// Creation d'endex et Indexage de données + on remplis également les indexes
    /// </summary>
    class Indexing
    {
        public Indexing(ElasticClient client)
        {
			var person = new Person
			{
				Id = "1",
				Firstname = "Jerome",
				Lastname = "Fortias"
			};
			var indexResponse = client.IndexDocument(person);

			// Si on fait un indexof d'un fichier avec un ID existant ca vas ecrasé le fichier qu'il y avais

			var person1 = new Person
			{
				Id = "2",
				Firstname = "Yoda",
				Lastname = "Fortias"
			};
			var indexResponse1 = client.IndexDocument(person1);
			if (!indexResponse1.IsValid)
			{
				Console.WriteLine("Request is not valid");
				return;
			}

			// Je rajoute un nouvelle index ici du nom de "people", tant qu'elle reste vide elle auras le contenue de letest

			var fluentIndexResponse = client.Index(person, i => i.Index("people"));

            // var initializerIndexResponse = client.Index(new IndexRequest<Person>(person, "people"));

            // Creation d'une list d'objet de person

            var list = new[]
            {
                                            new Person
                                            {
                                                Id = "1",
                                                Firstname = "Martijn",
                                                Lastname = "Laarman"
                                            },
                                            new Person
                                            {
                                                Id = "2",
                                                Firstname = "Stuart",
                                                Lastname = "Cam"
                                            },
                                            new Person
                                            {
                                                Id = "3",
                                                Firstname = "Russ",
                                                Lastname = "Cam"
                                            }
                                        };

            // j'envoie la liste d'objet dans l'index "people" sinon par default non l'index people

          /*  var indexManyResponse = client.IndexMany(list, "people");

            if (indexManyResponse.Errors)
            {
                foreach (var i in indexManyResponse.ItemsWithErrors)
                {
                    Console.WriteLine("Ca deconne pour l'index et le document {0} : {1}", i.Id, i.Error);
                }
            }*/

        }
    }
}
