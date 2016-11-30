using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics; // modulo nescessario para contar o tempo
using System.Xml.Linq;
using MongoDB.Bson; // modulo nescessario para o mongo
using MongoDB.Driver; // modulo nescessario para o mongo
using Newtonsoft.Json; // modulo nescessario para listar a pesquisa em Json
using Newtonsoft.Json.Linq; // modulo nescessario para listar a pesquisa em Json

// ABRIR 2 CMD E ESCREVER NO PRIMEIRO mongod E NO SEGUNDO mongo PARA ABRIR A CONEXAO COM O MONGO


namespace MongoDBTempo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\teste\teste.txt"); //Abre o arquivo
            MongoServer mongo = MongoServer.Create(); // prepara para a conectar
            mongo.Connect();// conecta
            var db = mongo.GetDatabase("mongoTempo"); //seleciona o DB 
            Stopwatch iw = new Stopwatch();// prepara para contar o tempo
            Stopwatch qw = new Stopwatch();// prepara para contar o tempo
            iw.Start();// inicia a contagem de tempo de inserção
            using (mongo.RequestStart(db))
            {
                var collection = db.GetCollection<BsonDocument>("dados"); //seleciona a tabela 
                foreach (string line in lines) //loop para percorrer as linhas
                {
                    string[] exploded = line.Split(','); //função para separar os dados id, data, dados geograficos
                    if (exploded.Length == 4)
                    {
                        BsonDocument dados = new BsonDocument() //preparação das colunas
                        .Add("_id", exploded[0]) //prepara a coluna de id
                        .Add("data", exploded[1]) //prepara a coluna de data
                        .Add("local", exploded[2] + ';' + exploded[3]); //prepara a coluna de local

                        collection.Insert(dados); //insere os dados no BD
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                    

                }
                iw.Stop();//finaliza a contagem de tempo da incercao
                var query = new QueryDocument("data", "2008"); //variavel que guarda os dados de pesquisa (primeiro dado o nome da coluna, segundo dado o valor de pesquisa
                qw.Start();// inicia a contagem de tempo
                foreach (BsonDocument item in collection.Find(query)) //executa a pesquisa dentro de um foreach para caso ter mais de um resultado ser listado
                {
                    string json = item.ToJson(); //lista os resultados em formato Json;
                }
                qw.Stop();//finaliza a contagem de tempo da busca
            }







            Console.WriteLine("Insercao no MongoDB completada em: " + (iw.ElapsedTicks / Stopwatch.Frequency) + " (s) e busca feita em: " + (qw.ElapsedTicks / Stopwatch.Frequency) + " (segundos)");// mostra o tempo em segundos


            Console.WriteLine("Insercao no MongoDB completada em: " + (iw.ElapsedTicks *1000 / Stopwatch.Frequency ) + " (ms) e busca feita em: " + (qw.ElapsedTicks * 1000 / Stopwatch.Frequency ) + " (ms)");// mostra o tempo em milisegundos


            System.Console.ReadKey();//não finaliza o programa até precionar uma tecla
        }
    }
}
