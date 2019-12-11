using System;

namespace Elasticsearch
{
    internal class Auditor
    {
        private Func<object> p;

        public Auditor(Func<object> p)
        {
            this.p = p;
        }
    }
}