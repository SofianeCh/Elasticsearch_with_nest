using System;
using System.Collections.Generic;
using System.Text;
using Nest;


namespace Elasticsearch
{
	[ElasticsearchType(IdProperty = nameof(SourceId))]
	class Accounts
    {
        public int account_nbr { get; set; }
        public int balance { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string state { get; set; }
		public string SourceId { get; set; }
	}
}
