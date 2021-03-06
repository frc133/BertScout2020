﻿using BertScout2020Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BertScout2020.Services
{
    public class SqlDataStoreEventTeamMatches : IDataStore<EventTeamMatch>
    {
        private bool _paramsFlag = false;
        private string _eventKey = "";
        private int? _teamNumber = 0;

        private List<EventTeamMatch> items;

        public SqlDataStoreEventTeamMatches()
        {
            _paramsFlag = false;
        }

        public SqlDataStoreEventTeamMatches(string eventKey)
        {
            _paramsFlag = true;
            _eventKey = eventKey;
            _teamNumber = null;
        }

        public SqlDataStoreEventTeamMatches(string eventKey, int teamNumber)
        {
            _paramsFlag = true;
            _eventKey = eventKey;
            _teamNumber = teamNumber;
        }

        private void FillList()
        {
            if (items == null)
            {
                // must complete, so don't async/await
                if (_paramsFlag)
                {
                    if (_teamNumber == null)
                    {
                        items = App.Database.GetEventTeamMatchesAsync(_eventKey).Result;
                    }
                    else
                    {
                        items = App.Database.GetEventTeamMatchesAsync(_eventKey, _teamNumber.Value).Result;
                    }
                }
                else
                {
                    items = App.Database.GetEventTeamMatchesAsync().Result;
                }
            }
        }

        public async Task<bool> AddItemAsync(EventTeamMatch item)
        {
            FillList();
            if (item.Uuid == null)
            {
                item.Uuid = Guid.NewGuid().ToString();
            }
            await App.database.SaveEventTeamMatchAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            FillList();
            var oldItem = items.Where((EventTeamMatch arg) => arg.Id == id).FirstOrDefault();
            await App.database.DeleteEventTeamMatchAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string uuid)
        {
            FillList();
            var oldItem = items.Where((EventTeamMatch arg) => arg.Uuid == uuid).FirstOrDefault();
            await App.database.DeleteEventTeamMatchAsync(oldItem.Id.Value);
            items.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<EventTeamMatch> GetItemAsync(int id)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<EventTeamMatch> GetItemAsync(string uuid)
        {
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.Uuid == uuid));
        }

        public async Task<EventTeamMatch> GetItemByKeyAsync(string key)
        {
            // key = EventKey|TeamNumber|MatchNumber
            string[] keys = key.Split('|');
            FillList();
            return await Task.FromResult(items.FirstOrDefault(s => s.EventKey == keys[0]
                                                              && s.TeamNumber == int.Parse(keys[1])
                                                              && s.MatchNumber == int.Parse(keys[2])));
        }

        public async Task<IEnumerable<EventTeamMatch>> GetItemsAsync(bool forceRefresh = false)
        {
            FillList();
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(EventTeamMatch item)
        {
            FillList();
            var oldItem = items.Where((EventTeamMatch arg) => arg.Uuid == item.Uuid).FirstOrDefault();
            item.Id = oldItem.Id;
            items.Remove(oldItem);
            await App.database.SaveEventTeamMatchAsync(item);
            items = null;
            FillList();
            return await Task.FromResult(true);
        }
    }
}
