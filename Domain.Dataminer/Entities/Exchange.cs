﻿using System.Collections.Generic;

namespace Domain.Dataminer.Entities
{
    public class Exchange
    {
        public int ExchangeId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public ICollection<ApiExchange> ApiExchanges { get; set; }
        public ICollection<Market> Markets { get; set; }
    }
}