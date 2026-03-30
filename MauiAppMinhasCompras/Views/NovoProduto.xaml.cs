using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
        InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém a categoria selecionada no Picker
            // retorna o item escolhido pelo usuário

            string categoriaSelecionada = pickerCategoria.SelectedItem.ToString();
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),

                // Atribui a categoria ao produto
                Categoria = categoriaSelecionada
            };

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso", "Registro Inserido","OK");

        }
        catch(Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}