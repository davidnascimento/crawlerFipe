namespace CrawlerFipe.Util
{
    public class TipoConsulta
    {
        private TipoConsulta(string value) { Value = value; }

        public string Value { get; set; }

        public static TipoConsulta Tradicional   { get { return new TipoConsulta("tradicional"); } }
    }
}