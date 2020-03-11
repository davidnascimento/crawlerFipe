using System;
using System.Collections.Generic;
using CrawlerFipe.Model;
using Newtonsoft.Json;
using RestSharp;

namespace CrawlerFipe
{
    class Program
    {
        static string _urlBase = "https://veiculos.fipe.org.br/api/veiculos/";

        static void Main(string[] args)
        {
            var tabelaReferencia = ConsultarTabelaReferencia();
            var marcas = ConsultarMarcas();
            var modelos = ConsultarModelos();
            var anoModelo = ConsultarAnoModelo();
            var preco = ConsultarPreco();
        }

        private static RestRequest CreateRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Referer", "https://veiculos.fipe.org.br/");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        public static IEnumerable<ReferenceMonth> ConsultarTabelaReferencia()
        {
            var client = new RestClient(_urlBase + "ConsultarTabelaDeReferencia")
            {
                Timeout = -1
            };

            var request = CreateRequest();
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<IEnumerable<ReferenceMonth>>(response.Content);

            return null;
        }

        public static IEnumerable<Record> ConsultarMarcas()
        {
            var client = new RestClient(_urlBase + "ConsultarMarcas")
            {
                Timeout = -1
            };

            var request = CreateRequest();
            request.AddParameter("application/json", "{ codigoTabelaReferencia: \"252\", codigoTipoVeiculo: \"1\" }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<IEnumerable<Record>>(response.Content);

            return null;
        }

        public static Model.Model ConsultarModelos()
        {
            var client = new RestClient(_urlBase + "ConsultarModelos")
            {
                Timeout = -1
            };

            var request = CreateRequest();
            request.AddParameter("application/json", "{\n\tcodigoTipoVeiculo: 1,\ncodigoTabelaReferencia: 252,\ncodigoModelo: null,\n        codigoMarca: 1,\nano: null,\n        codigoTipoCombustivel: null,\n        anoModelo: null,\n        modeloCodigoExterno: null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<Model.Model>(response.Content);

            return null;
        }

        public static IEnumerable<Record> ConsultarAnoModelo()
        {
            var client = new RestClient(_urlBase + "ConsultarAnoModelo")
            {
                Timeout = -1
            };

            var request = CreateRequest();
            request.AddParameter("application/json", "{\n\tcodigoTipoVeiculo: 1,\ncodigoTabelaReferencia: 252,\ncodigoModelo: 1,\ncodigoMarca: 1,\nano: null,\ncodigoTipoCombustivel: null,\nanoModelo: null,\nmodeloCodigoExterno: null\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<IEnumerable<Record>>(response.Content);

            return null;
        }

        public static Fipe ConsultarPreco()
        {
            var client = new RestClient(_urlBase + "ConsultarValorComTodosParametros")
            {
                Timeout = -1
            };

            var request = CreateRequest();
            request.AddParameter("application/json", "{\n\tcodigoTabelaReferencia: 252,\ncodigoMarca: 1,\ncodigoModelo: 1,\ncodigoTipoVeiculo: 1,\nanoModelo: 1991,\ncodigoTipoCombustivel: 1,\ntipoVeiculo: 'carro',\nmodeloCodigoExterno: null, \ntipoConsulta: 'tradicional'\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<Fipe>(response.Content);

            return null;
        }
    }
}
