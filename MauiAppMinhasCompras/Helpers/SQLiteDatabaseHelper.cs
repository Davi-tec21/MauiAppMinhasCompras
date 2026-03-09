using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
    readonly SQLiteAsyncConnection _conn;

    public SQLiteDatabaseHelper(string path)
    { 
    _conn = new SQLiteAsyncConnection(path);
    _conn.CreateTableAsync<Produto>().Wait();
                
     }
    //Declaração e programação dos metodos
    //return mostra quantos valores foram retornados com a operação
    public Task<int> Insert(Produto p) 
    {
    return _conn.InsertAsync(p);  
    }

    public Task<List<Produto>> Update(Produto p)
    {
    String sql = "UPDATE Produto SET Descricao=?,Quantidade =?, Preco=? WHERE Id=?";

    return _conn.QueryAsync<Produto>(
     sql, p.Descricao, p.Quantidade, p.Preco, p.Id
        );
    }

    public Task<int> Delete(int id) 
    {
    return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
    //Seleciona a tabela, chama o metodo delete...
    }

    public Task<List<Produto>> GetAll() 
    { 
     return _conn.Table <Produto>().ToListAsync(); 
    }

    public Task<List<Produto>> Search(string q) 
    {
    String sql = "SELECT * Produto WHERE descricao LIKE '%"+ q + "%' ";

    return _conn.QueryAsync<Produto>(sql);

    }


   }
}
