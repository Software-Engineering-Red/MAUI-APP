using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace MauiApp1.Views
{
/*! <summary>
        OrderStatusPage class extending ContentPage - responsible for OrderStatusPage view
    </summary> */
    public partial class OrderStatusPage : ContentPage
    {
         /*! <summary>
            A reference for storing currently selected order status
        </summary> */
        OrderStatus selectedOrderStatus = null;
        
        /*! <summary>
            Service for interacting with order statuses
        </summary> */
        IOrderStatusService orderStatusService;
         /*! <summary>
        Collection of order statuses
        </summary> */
        ObservableCollection<OrderStatus> orderStatuses = new ObservableCollection<OrderStatus>();

         /*! <summary>
        Constructor class, setting the context and initiating class variables.
        </summary> */
        public OrderStatusPage()
        {
            InitializeComponent();
            BindingContext = new OrderStatus();
            orderStatusService = new OrderStatusService();

            Task.Run(async () => await LoadOrderStatus());
            txe_order_status.Text = "";
        }

         /*! <summary>
            Methods loads order status from db into the orderStatuses class variable 
        </summary> */
        private async Task LoadOrderStatus()
        {
            orderStatuses = new ObservableCollection<OrderStatus>(await orderStatusService.GetOrderStatusList());
            ltv_order_statuses.ItemsSource = orderStatuses;
        }

        /* <summary>
         Event handler for the Save Button Clicked event.
         Saves or updates an order status based on user input.
         </summary> */
        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string newStatusName = txe_order_status.Text.Trim();

            if (string.IsNullOrEmpty(newStatusName))
                return;

            if (selectedOrderStatus == null)
            {
                var orderStatus = new OrderStatus()
                {
                    Name = newStatusName
                };
                orderStatusService.AddStatus(orderStatus);
                orderStatuses.Add(orderStatus);
            }
            else
            {
                selectedOrderStatus.Name = newStatusName;
                orderStatusService.UpdateStatus(selectedOrderStatus);
                var updatedOrderStatus = orderStatuses.FirstOrDefault(x => x.ID == selectedOrderStatus.ID);
                if (updatedOrderStatus != null)
                    updatedOrderStatus.Name = newStatusName;
            }

            selectedOrderStatus = null;
            ltv_order_statuses.SelectedItem = null;
            txe_order_status.Text = "";
        }

        /* <summary>
        Event handler for the Delete Button Clicked event.
        Deletes the selected order status.
        </summary> */
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (ltv_order_statuses.SelectedItem == null)
            {
                await DisplayAlert("No Order Status Selected", "Please select value to delete", "OK");
                return;
            }

            await orderStatusService.DeleteStatus(selectedOrderStatus);
            orderStatuses.Remove(selectedOrderStatus);

            ltv_order_statuses.SelectedItem = null;
            txe_order_status.Text = "";
        }

        /* <summary>
        Event handler for the ItemSelected event of the order status list view.
        Updates the selectedOrderStatus when an item is selected.
        </summary> */
        private void ltv_orderStatus_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedOrderStatus = e.SelectedItem as OrderStatus;
            if (selectedOrderStatus == null) return;

            txe_order_status.Text = selectedOrderStatus.Name;
        }
    }
}
