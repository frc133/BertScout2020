using BertScout2020.Models;
using System.Collections.ObjectModel;

namespace BertScout2020.ViewModels
{
    public class MatchResultsViewModel : BaseViewModel
    {
        public ObservableCollection<MatchResult> MatchResults { get; set; }
    }
}
