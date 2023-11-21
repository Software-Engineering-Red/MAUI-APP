using System;
using UndacApp.ViewModels;
using Microsoft.Maui.Controls;

namespace UndacApp.Views
{
    public partial class ResourceRequestPage : ContentPage
    {
        public ResourceRequestPage()
        {
            InitializeComponent();
            BindingContext = new ResourceRequestViewModel();
        }
    }
}