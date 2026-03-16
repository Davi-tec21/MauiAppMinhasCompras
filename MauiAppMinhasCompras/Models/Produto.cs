using SQLite; //Importa o SQlite

namespace MauiAppMinhasCompras.Models //Onde a Classe está organizada
{
    public class Produto //Declaração da classe Produto
    {
        //Declaração das Propriedades da classe 
        [PrimaryKey,AutoIncrement] //Anotação que informa as funções da propriedade Id
        public int Id { get; set; }//Campo para o identificador único produto

        public string Descricao { get; set; } //Campo para a descrição do produto.

        public double Quantidade { get; set; } //Campo para a quantidade do produto.
        public double Preco { get; set; } //Campo para a preço do produto.
        public double Total { get => Quantidade * Preco; } //Campo para o total produto.

    }
}
