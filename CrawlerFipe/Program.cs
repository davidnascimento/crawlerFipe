using System;
using System.Collections.Generic;
using CrawlerFipe.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using CrawlerFipe.Util;

namespace CrawlerFipe
{
    class Program
    {
        static void Main(string[] args)
        {
            var fipeApi = new FipeApi();

            var tipoVeiculo = TipoVeiculo.Carro;
            var tabelaReferencia = fipeApi.ConsultarTabelaReferencia()?.FirstOrDefault();
            if (tabelaReferencia != null)
            {
                var marcaList = fipeApi.ConsultarMarcas(tabelaReferencia.Codigo, tipoVeiculo);
                if (marcaList != null && marcaList.Any())
                {
                    foreach(var marca in marcaList)
                    {
                        var modeloList = fipeApi.ConsultarModelos(tabelaReferencia.Codigo, tipoVeiculo, marca.Value, null, null, null, null, null);
                        if (modeloList?.Modelos != null && modeloList.Modelos.Any()) 
                        {
                            foreach(var modelo in modeloList.Modelos)
                            {
                                var anoModeloList = fipeApi.ConsultarAnoModelo(tabelaReferencia.Codigo, tipoVeiculo, marca.Value, modelo.Value, null, null, null, null);

                                if (anoModeloList != null && anoModeloList.Any())
                                {
                                    foreach(var anoModelo in anoModeloList)
                                    {
                                        var codCombustivel = anoModelo.Value.Split("-")[1];
                                        var ano = anoModelo.Value.Split("-")[0];
                                        var preco = fipeApi.ConsultarPreco(tabelaReferencia.Codigo, tipoVeiculo, TipoConsulta.Tradicional, marca.Value, modelo.Value, ano,
                                                                   codCombustivel, null);
                                        
                                        Console.WriteLine(preco.CodigoFipe + " - " + preco.Valor);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
