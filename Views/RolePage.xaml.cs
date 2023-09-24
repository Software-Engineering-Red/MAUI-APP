
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class RotaPage : ContentPage
{
    Rota selectedRota = null;
    IRotaService rotaService;
    ObservableCollection<Rota> rotas = new ObservableCollection<Rota>();

    public RotaPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.rotaService = new RotaService();

        Task.Run(async () => await LoadRotas());
        txe_rota.Text = "";
    }

    private async Task LoadRotas()
    {
        rotas = new ObservableCollection<Rota>(await rotaService.GetRotaList());
        ltv_rotas.ItemsSource = rotas;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_rota.Text)) return;

        if (selectedRota == null)
        {
            var rota = new Rota() { Name = txe_rota.Text };
            rotaService.AddRota(rota);
            rotas.Add(rota);
        }
        else
        {
            selectedRota.Name = txe_rota.Text;
            rotaService.UpdateRota(selectedRota);
            var rota = rotas.FirstOrDefault(x => x.ID == selectedRota.ID);
            rota.Name = txe_rota.Text;
        }


        selectedRota = null;
        ltv_rotas.SelectedItem = null;
        txe_rota.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_rotas.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Rota Selected", "Select the rota you want to delete from the list", "OK");
            return;
        }

        await rotaService.DeleteRota(selectedRota);
        rotas.Remove(selectedRota);

        ltv_rotas.SelectedItem = null;
        txe_rota.Text = "";
    }

    private void ltv_rotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedRota = e.SelectedItem as Rota;
        if (selectedRota == null) return;

        txe_rota.Text = selectedRota.Name;
    }
}