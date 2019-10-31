using BertScout2020.Models;
using BertScout2020.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BertScout2020.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FRCEventDetailPage : ContentPage
    {
        FRCEventDetailViewModel viewModel;

        public FRCEventDetailPage(FRCEventDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public FRCEventDetailPage()
        {
            InitializeComponent();

            var item = new FRCEvent
            {
                Name = "Event 1 Name",
                Location = "Event 1 Location"
            };

            viewModel = new FRCEventDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}