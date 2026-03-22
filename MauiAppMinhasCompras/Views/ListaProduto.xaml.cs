using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
    // ObservableCollection para armazenar os produtos
    // e atualizar lista na interface quando os dados mudam
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // Define que o ListView da tela irá mostrar os dados da ObservableCollection
        lst_produtos.ItemsSource = lista;
    }




    // Método executado automaticamente quando a tela aparece
    protected async override void OnAppearing()
    {
        try
        {

            // Busca todos os produtos armazenados no banco de dados
            List<Produto> tmp = await App.Db.GetAll();

            // Adiciona cada produto encontrado na ObservableCollection
            // Isso faz com que a lista na tela seja atualizada automaticamente
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Caso ocorra algum erro,exibe uma menssagem 
            await DisplayAlert("ERRO", ex.Message, "OK");
        }
    }



    // Método executado quando o usuário clica no botão adicionar
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Tela para cadastrar um novo produto
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Caso ocorra algum erro,exibe uma menssagem 
            DisplayAlert("ERRO", ex.Message, "OK");
        }
    }



    // Executa sempre que o usuário digita  no SearchBar
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Captura o texto que foi digitado
            String q = e.NewTextValue;

            // Limpa a lista atual exibida na tela
            lista.Clear();

            // Realiza a busca no banco de dados usando o texto digitado
            List<Produto> tmp = await App.Db.Search(q);


            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Caso ocorra algum erro,exibe uma menssagem 
            await DisplayAlert("ERRO", ex.Message, "OK");
        }
    }



    // Método executado quando o usuário clica no botão "Somar"
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Soma o valor total de todos os produtos da lista
        double soma = lista.Sum(i => i.Total);

        //  mensagem mostrando o total formatado como moeda
        string msg = $"O Total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

  


    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecinado = sender as MenuItem;

            Produto p = selecinado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }










}
