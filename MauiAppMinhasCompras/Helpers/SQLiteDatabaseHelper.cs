//Criação da Classe que faz o  crud no sqlite

//Importação as bibliotecas
using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
    readonly SQLiteAsyncConnection _conn;// conexão Assincrona com o SQLite, somente leitura
 
    public SQLiteDatabaseHelper(string path)
        { 
    _conn = new SQLiteAsyncConnection(path);
            //Construtor para conexão com o banco de dados,
            //utiliza o caminho do arquivo

            _conn.CreateTableAsync<Produto>().Wait(); //Cria a tabela Produto caso não exista
        }



        //Declaração e programação dos metodos
        //return mostra quantos valores foram retornados com a operação


        public Task<int> Insert(Produto p) //método para inserir um novo produto
        {
    return _conn.InsertAsync(p); //Utiliza a a conexão para inserir o
                                 //produto no banco de dados de forma assíncrona.
        }


    public Task<List<Produto>> Update(Produto p)//método para atualizar um produto existente
        {
    String sql = "UPDATE Produto SET Descricao=?,Quantidade =?, Preco=? WHERE Id=?";
    // Instrução para atualizar os dados no banco de dados usando os valores do objeto p

            return _conn.QueryAsync<Produto>(
     sql, p.Descricao, p.Quantidade, p.Preco, p.Id
        ); // Utiliza a conexão para executar o comando SQL de atualização
           // do produto no banco de dados de forma assíncrona
        }


        public Task<int> Delete(int id) //método para remover um produto
        {
    return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
            //Acessa a tabela Produto e remove o registro correspondente
            // ao Id informado de maneira assíncrona

        }


        public Task<List<Produto>> GetAll() //método para listar todos os produtos
        { 
     return _conn.Table <Produto>().ToListAsync();
            // Acessa a tabela Produto e retorna todos os registros
             // em lista de maneira assíncrona
        }

        public Task<List<Produto>> Search(string q) //método para fazer uma busca por produtos
    {
    String sql = "SELECT * FROM Produto WHERE descricao LIKE '%"+ q + "%' ";
            // Instrução SQL para buscar produtos onde a descrição
            // Tenha o texto informado na variável q (query)

            return _conn.QueryAsync<Produto>(sql);
            // Utiliza a conexão para executar a consulta SQL de busca
            // de produtos no banco de dados de forma assíncrona

        }


    }
}
