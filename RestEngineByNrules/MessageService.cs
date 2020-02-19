using System;
using System.Collections.Generic;

namespace RestEngineByNrules
{
    public class MessageService
    {
        private List<string> MessageInSql;

        public static void SendMessageToPerson(string personName,string message)
        {
            Console.WriteLine($"系統傳送了訊息給{personName}:{message}");
        }
    }
}
