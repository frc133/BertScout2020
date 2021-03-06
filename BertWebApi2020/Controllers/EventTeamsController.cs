﻿using BertScout2020Data.Models;
using BertWebApi2020.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventTeamStore.Controllers
{
    public class EventTeamsController : ApiController
    {
        static readonly IRepository<EventTeam> repository = new EventTeamRepository();

        [AcceptVerbs("GET")]
        [HttpGet]
        public IEnumerable<EventTeam> GetAllEventTeams()
        {
            return repository.GetAll();
        }

        [AcceptVerbs("GET")]
        [HttpGet]
        public EventTeam GetEventTeam(string uuid)
        {
            EventTeam item = repository.GetByUuid(uuid);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public EventTeam GetEventTeamByEventTeamNumber(string key)
        {
            return repository.GetByKey(key);
        }

        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage PostEventTeam(EventTeam item)
        {
            item.Id = null; // clear for autoincrement
            item = repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { uuid = item.Uuid });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [AcceptVerbs("PUT")]
        [HttpPut]
        public void PutEventTeam(string uuid, EventTeam item)
        {
            EventTeam oldItem = repository.GetByUuid(uuid);
            // only update if new .Changed is greater
            if (oldItem != null && oldItem.Changed < item.Changed)
            {
                item.Uuid = uuid;
                if (!repository.Update(item))
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        [AcceptVerbs("DELETE")]
        [HttpDelete]
        public void DeleteEventTeam(string uuid)
        {
            repository.RemoveByUuid(uuid);
        }
    }
}
