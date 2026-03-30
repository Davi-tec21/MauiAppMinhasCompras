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
            lista.Clear();// Limpa a Observable Collection

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
        try
        {
            // Agrupa os produtos por categoria
            var relatorio = lista
                .GroupBy(p => p.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Total = g.Sum(p => p.Total)
                });

            string msg = "";

            foreach (var item in relatorio)
            {
                msg += item.Categoria + ": R$ " + item.Total + "\n";
            }

            DisplayAlert("Relatório por Categoria", msg, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }
    }



    // Método executado quando o usuário clica na opção de remover
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




    // Método executado quando o usuário seleciona um item da lista
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            // // Abre a tela de edição de produto

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
    private async void pickerFiltro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string categoria = pickerFiltro.SelectedItem.ToString();

            List<Produto> tmp = await App.Db.GetAll();

            // Limpa a lista atual
            lista.Clear();

            if (categoria == "Todos")
            {
                tmp.ForEach(i => lista.Add(i));
            }
            else
            {
                tmp.Where(p => p.Categoria == categoria)
                   .ToList()
                   .ForEach(i => lista.Add(i));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }





}


