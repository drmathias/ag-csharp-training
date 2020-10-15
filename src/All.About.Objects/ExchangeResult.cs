using System;

namespace All.About.Objects
{
    public class ExchangeResult
    {
        public string Base { get; set; }

        public DateTime Date { get; set; }

        public Rates Rates { get; set; }
    }

    public class Rates
    {
        public decimal GBP { get; set; }

        public decimal USD { get; set; }

        public decimal EUR { get; set; }

        public decimal CAD { get; set; }

        public decimal BRL { get; set; }

        public decimal RUB { get; set; }

        public decimal JPY { get; set; }
    }
}