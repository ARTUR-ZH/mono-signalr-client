using System;
using Microsoft.AspNet.SignalR.Client;
using NLog;

namespace signalr
{
	public class SocketConnector
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private HubConnection _hubConnection;
		private IHubProxy _messageProxy;
		private IHubProxy MessageProxy
		{
			get
			{
				if (_messageProxy != null && _hubConnection.State==ConnectionState.Connected)
				{
					return _messageProxy;
				}
				_hubConnection = new HubConnection("SIGNALR_URL");
				_messageProxy = _hubConnection.CreateHubProxy("HUB_NAME");
				_hubConnection.Start().Wait();
				return _messageProxy;
			}
		}

		private void Invoke(string method, params object[] args)
		{
			try
			{
				Logger.Trace("Invoking method {0}", method);
				MessageProxy.Invoke(method, args);
			}
			catch (Exception ex)
			{
				Logger.ErrorException(string.Format("Invoke {0} error.", method), ex);
			}
		}

		public void UpdateNotificationsCount(long memberKey)
		{
			Invoke("UpdateNotificationsCount", memberKey, 10);
		}
	}
}

