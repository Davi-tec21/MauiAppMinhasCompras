using SQLite; //Importa o SQlite

namespace MauiAppMinhasCompras.Models //Onde a Classe está organizada
{
    public class Produto //Declaração da classe Produto
    {

        string _descricao;

        //Declaração das Propriedades da classe 
        [PrimaryKey,AutoIncrement] //Anotação que informa as funções da propriedade Id
        public int Id { get; set; }//Campo para o identificador único produto


        //Campo para a descrição do produto.
        public string Descricao {
            get=>_descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor Insira a descrição");
                }
               _descricao = value;
            }


        }
        public double Quantidade { get; set; } //Campo para a quantidade do produto.
        public double Preco { get; set; } //Campo para a preço do produto.
        public double Total { get => Quantidade * Preco; } //Campo para o total produto.

    }
}
