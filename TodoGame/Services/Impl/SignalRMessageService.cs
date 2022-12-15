﻿using System;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using TodoGame.Events;
using TodoGame.Models;

namespace TodoGame.Services.Impl
{
	public class SignalRMessageService : ISignalRMessageService
    {
		private readonly IHubContext<SignalRGameEventHub> _hub;

        public SignalRMessageService(IHubContext<SignalRGameEventHub> hub)
		{
			_hub = hub;
		}

        public Task sendGameStatusNotificationAsync(SignalRMessage signalRMessage)
        {
            if (signalRMessage is not null)
            {
                var users = new string[] { signalRMessage.startuserid, signalRMessage.opuserid };
                var jsonString = JsonConvert.SerializeObject(signalRMessage);
                return _hub.Clients.All.SendAsync(signalRMessage.messageType, jsonString);
                // return _hub.Clients.Users(users).SendAsync(signalRMessage.messageType, signalRMessage);
            }

            return Task.CompletedTask;
        }
    }
}

