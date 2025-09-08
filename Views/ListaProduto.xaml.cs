using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();

        Lst_produtos.ItemsSource = Lista;
    }

    protected async override void OnAppearing()
    {
        try
        {

            List<Produto> tmp = await App.Db.getAll();

            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            Lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
        
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);

        string msg = $"O totalé {soma:C}";

        DisplayAlert("Total dos produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {

        try
        {
            MenuItem selecionado = sender as MenuItem; // pegar item do tipo correto
            if (selecionado == null) return;

            Produto p = selecionado.BindingContext as Produto;
            if (p == null)
            {
                await DisplayAlert("Erro", "Produto inválido ou não encontrado.", "OK");
                return;
            }

            bool confirm = await DisplayAlert(
                "Tem certeza? ", $"Confirma a exclusão do produto {p.Descricao}?", "Sim", "Não");
            if (confirm)
            {
                await App.Db.Delete(p.Id);
                Lista.Remove(p);
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void Lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e) // Por tras do Editar produtos
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