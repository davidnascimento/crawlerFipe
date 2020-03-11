using System.Collections.Generic;
using CrawlerFipe.Model;
using CrawlerFipe.Util;
using Newtonsoft.Json;
using RestSharp;

namespace CrawlerFipe
{
    public class FipeApi
    {
        const string _urlBase = "https://veiculos.fipe.org.br/api/veiculos/";

        private RestRequest CreateRequest()
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Referer", "https://veiculos.fipe.org.br/");
            request.AddHeader("Content-Type", "application/json");

            return request;
        }

        private T ExecuteRequest<T>(RestClient client, object json)
        {
            var request = CreateRequest();

            if (json != null)
                request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<T>(response.Content);

            return default(T);
        }

        public IEnumerable<ReferenceMonth> ConsultarTabelaReferencia()
        {
            var client = new RestClient(_urlBase + "ConsultarTabelaDeReferencia")
            {
                Timeout = -1
            };

            return ExecuteRequest<IEnumerable<ReferenceMonth>>(client, null);
        }

        public IEnumerable<Record> ConsultarMarcas(string codTabelaReferencia, TipoVeiculo tipoVeiculo)
        {
            var client = new RestClient(_urlBase + "ConsultarMarcas")
            {
                Timeout = -1
            };

            // var json = new {
            //     codigoTabelaReferencia = codTabelaReferencia
            //     ,
            //     codigoTipoVeiculo = tipoVeiculo.Codigo
            // };
            var json = "{ codigoTabelaReferencia: \"" + codTabelaReferencia + "\", codigoTipoVeiculo: \"" + tipoVeiculo.Codigo + "\" }";
            
            return ExecuteRequest<IEnumerable<Record>>(client, json);
        }

        public Model.Model ConsultarModelos(string codTabelaReferencia, TipoVeiculo tipoVeiculo, string codMarca, string codModelo,
                                                   int? ano, int? anoModelo, string codTipoCombustivel, string modeloCodExterno)
        {
            var client = new RestClient(_urlBase + "ConsultarModelos")
            {
                Timeout = -1
            };

            // var json = new { 
            //     codigoTabelaReferencia = codTabelaReferencia
            //     ,
            //     codigoTipoVeiculo = tipoVeiculo.Codigo
            //     ,
            //     codigoMarca = codMarca
            //     ,
            //     codigoModelo = codModelo
            //     ,
            //     ano = ano
            //     ,
            //     anoModelo = anoModelo
            //     ,
            //     codigoTipoCombustivel = codTipoCombustivel
            //     ,
            //     modeloCodigoExterno = modeloCodExterno
            // };
            var json = "{\n\tcodigoTipoVeiculo: " + tipoVeiculo.Codigo + ",\ncodigoTabelaReferencia: " + codTabelaReferencia + ",\ncodigoModelo: null,\n codigoMarca: " + codMarca + ",\nano: null,\n codigoTipoCombustivel: null,\n anoModelo: null,\n modeloCodigoExterno: null\n}";

            return ExecuteRequest<Model.Model>(client, json);
        }

        public IEnumerable<Record> ConsultarAnoModelo(string codTabelaReferencia, TipoVeiculo tipoVeiculo, string codMarca, string codModelo,
                                                   int? ano, int? anoModelo, string codTipoCombustivel, string modeloCodExterno)
        {
            var client = new RestClient(_urlBase + "ConsultarAnoModelo")
            {
                Timeout = -1
            };

            // var json = new { 
            //     codigoTabelaReferencia = codTabelaReferencia
            //     ,
            //     codigoTipoVeiculo = tipoVeiculo.Codigo
            //     ,
            //     codigoMarca = codMarca
            //     ,
            //     codigoModelo = codModelo
            //     ,
            //     ano = ano
            //     ,
            //     anoModelo = anoModelo
            //     ,
            //     codigoTipoCombustivel = codTipoCombustivel
            //     ,
            //     modeloCodigoExterno = modeloCodExterno
            // };
            var json = "{\n\tcodigoTipoVeiculo: " + tipoVeiculo.Codigo + ",\ncodigoTabelaReferencia: " + codTabelaReferencia + ",\ncodigoModelo: " + codModelo + ",\ncodigoMarca: " + codMarca + ",\nano: null,\ncodigoTipoCombustivel: null,\nanoModelo: null,\nmodeloCodigoExterno: null\n}";

            return ExecuteRequest<IEnumerable<Record>>(client, json);
        }

        public Fipe ConsultarPreco(string codTabelaReferencia, TipoVeiculo tipoVeiculo, TipoConsulta tipoConsulta, 
                                          string codMarca, string codModelo, string anoModelo, string codTipoCombustivel, 
                                          string modeloCodExterno)
        {
            var client = new RestClient(_urlBase + "ConsultarValorComTodosParametros")
            {
                Timeout = -1
            };

            // var json = new {
            //     codigoTabelaReferencia = codTabelaReferencia
            //     ,
            //     codigoMarca = codMarca
            //     ,
            //     codigoModelo = codModelo
            //     ,
            //     codigoTipoVeiculo = tipoVeiculo.Codigo
            //     ,
            //     anoModelo = anoModelo
            //     ,
            //     codigoTipoCombustivel = codTipoCombustivel
            //     ,
            //     tipoVeiculo = tipoVeiculo.Value
            //     ,
            //     modeloCodigoExterno = modeloCodExterno
            //     ,
            //     tipoConsulta = tipoConsulta.Value
            // };
            var json = "{\n\tcodigoTabelaReferencia: " + codTabelaReferencia + ",\ncodigoMarca: " + codMarca + ",\ncodigoModelo: " + codModelo + ",\ncodigoTipoVeiculo: " + tipoVeiculo.Codigo + ",\nanoModelo: " + anoModelo + ",\ncodigoTipoCombustivel: " + codTipoCombustivel + ",\ntipoVeiculo: '" + tipoVeiculo.Value + "',\nmodeloCodigoExterno: null, \ntipoConsulta: '" + tipoConsulta.Value + "'\n}";
            
            return ExecuteRequest<Fipe>(client, json);
        }
    }
}