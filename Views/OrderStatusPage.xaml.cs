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
            this.BindingContext = this;
            this.orderStatusService = new OrderStatusService();

            Task.Run(async () => await LoadOrderStatus());
            txe_order_status.Text = "";
        }

        private async Task LoadOrderStatus()
        {
            orderStatuses = new ObservableCollection<OrderStatus>(await orderStatusService.GetOrderStatusList());
            ltv_order_statuses.ItemsSource = orderStatuses;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txe_order_status.Text)) return;

            if (selectedOrderStatus == null)
            {
                var orderStatus = new OrderStatus() { Name = txe_order_status.Text };
                orderStatusService.AddStatus(orderStatus);
                orderStatuses.Add(orderStatus);
            }
            else
            {
                selectedOrderStatus.Name = txe_order_status.Text;
                orderStatusService.UpdateStatus(selectedOrderStatus);
                var orderStatus = orderStatuses.FirstOrDefault(x => x.ID == selectedOrderStatus.ID);
                orderStatus.Name = txe_order_status.Text;
            }

            selectedOrderStatus = null;
            ltv_order_statuses.SelectedItem = null;
            txe_order_status.Text = "";

        }

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

        private void ltv_orderStatus_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedOrderStatus = e.SelectedItem as OrderStatus;
            if (selectedOrderStatus == null) return;

            txe_order_status.Text = selectedOrderStatus.Name;
        }
    }
}
