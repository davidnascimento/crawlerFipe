namespace CrawlerFipe.Util
{
    public class TipoVeiculo
    {
        private TipoVeiculo(string codigo, string value) { Codigo = codigo; Value = value; }

        public string Codigo { get; set; }
        public string Value { get; set; }

        public static TipoVeiculo Carro { get { return new TipoVeiculo("1", "carro"); } }
        public static TipoVeiculo Moto { get { return new TipoVeiculo("2", "moto"); } }
        public static TipoVeiculo Caminhao { get { return new TipoVeiculo("3", "caminhao"); } }
        
    }
}