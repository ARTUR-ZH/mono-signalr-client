using System;

namespace signalr
{
	public class Program
	{
		static void Main(string[] args)
		{
			var connector = new SocketConnector ();
			connector.UpdateNotificationsCount (9);
		}
	}
}

