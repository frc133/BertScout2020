using BertScout2020.Services;
using BertScout2020Data.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace BertScout2020.ViewModels
{
    public class SelectTeamsByEventViewModel : BaseViewModel
    {
        public IDataStore<Team> DataStoreTeam => new SqlDataStoreTeams(App.currFRCEventKey);

        public ObservableCollection<Team> Teams { get; set; }
        public Command LoadEventTeamsCommand { get; set; }

        public SelectTeamsByEventViewModel()
        {
            Title = App.currFRCEventName;
            Teams = new ObservableCollection<Team>();
            ExecuteLoadEventTeamsCommand();
        }

        public void ExecuteLoadEventTeamsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Teams.Clear();
                var teams = DataStoreTeam.GetItemsAsync(true).Result;
                foreach (var team in teams)
                {
                    Teams.Add(team);
                }
            }
            catch (Exception ex)
            {
                Title = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
