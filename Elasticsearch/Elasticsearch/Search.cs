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
    /// Makes a research in elastic search. We try different approaches.
    /// .index      let you know which index you want to research
    /// .size       let you difine a size for the result otherwise it will be by default 10
    /// .Query      you specify what you want to research
    /// </summary>
    class Search
    {
        public IReadOnlyCollection<Accounts> search_res(ElasticClient client, string field, string value)
        {
            ISearchResponse<Accounts> response = client.Search<Accounts>((s => s
               .Index("people")
               .From(0)
               .Size(10)
               .Query(q => q
                    .Match(z => z
                    .Field("firstname")
                    .Query("Amber")
                    )
                    )
                ));

			//Prendre les informations sur ES et modifier la proprieré qu'on veux puis tout repush sur ES

			var getresponse = client.Get<Person>("1");
			var surprise = getresponse.Source;

			surprise.Firstname = "check";
			var updateResponse = client.Update<Person>(1, u => u
			.Index("letest")
			.Doc(surprise));

			// Autre approche pour modifier du contenue dans ES

			var updateResponse1 = client.Update<Person>(2, u => u
				.Index("letest")
				.Doc(new Person
				{
					//Id = "2",
					Firstname = "teeeest"
				})
				.DetectNoop(false)
			);

			if (updateResponse1.IsValid == true)
				Console.WriteLine("is valid");

			/*
						var task = client.UpdateAsync<ElasticsearchDocument>(
							   new DocumentPath<ElasticsearchDocument>(doc), u =>
								   u.Index(indexName).Doc(doc));*/

			// await client.UpdateAsync<ElasticSearchDoc>(doc.Id, u => u.Index("movies").Doc(new ElasticSearchDoc { Title = "Updated title!" }));


			//var test = new UpdateRequest("people", "_doc", "1");

			var res = response.Documents;
            Console.WriteLine("count   " + res.Count);
     /*       foreach (var item in res)
            {
                Console.WriteLine("on test  " + item.firstname);
                item.firstname = "Nikita";
                Console.WriteLine(item.firstname);
            }
*/
            var test2 = client.Search<Accounts>(l => l
            .Index("people")
            .Query(z => z
                .MatchAll()));
            
            var quer = client.Search<Accounts>((l => l
            .Index("people")
            .Size(10000)
            .Query(p => p
                .Match(m => m
                    .Field(field)
                    .Query(value)))));

            IReadOnlyCollection<Accounts> querD = quer.Documents;
/*
            foreach (var item in querD)
            {
                Console.WriteLine(item.firstname + " " + item.lastname + " " + item.age + " " + item.gender);
            }*/
            Console.WriteLine("there is a Count of " + querD.Count);
            Console.WriteLine();
            return querD;
        }
    }
}
