using BertScout2020.Models;

namespace BertScout2020.ViewModels
{
    public class FRCEventDetailViewModel : BaseViewModel
    {
        public FRCEvent FRCEvent { get; set; }
        public FRCEventDetailViewModel(FRCEvent item = null)
        {
            Title = item?.Name;
            FRCEvent = item;
        }
    }
}
