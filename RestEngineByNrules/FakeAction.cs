using System;
using System.Collections.Generic;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules
{
    public class FakeAction
    {
        public AskForRest SendAskForRestRequest()
        {
            var request= new AskForRest(
                "皓之",
                "遠松",
                new DateTime(2020, 2, 10),
                new DateTime(2020, 2, 13));
            //SendRequest
            request.CheckingStatus = RequestStatus.Checking;

            Console.WriteLine($"{request.Name}新增了一張假單 代理人{request.Proxy}" +
                              $" 時間{request.StartTime}到{request.EndTime}");

            return request;
        }

        public AskForRest ReSendToContinueRequest(AskForRest request)
        {
            request.CheckingStatus = RequestStatus.Checking;
            Console.WriteLine($"{request.Name}重新送出未通過的假單:{request.key}");
            return request;
        }

        public AskForRest SomeOnePassTheRequest(AskForRest request)
        {
            var name= request.CheckingPerson;
            request.CheckingStatus = RequestStatus.Pass;
            Console.WriteLine($"{name} pass request:{request.key}");

            return request;
        }
        public AskForRest SomeOneRejectTheRequest(AskForRest request)
        {
            var name= request.CheckingPerson;
            request.CheckingStatus = RequestStatus.Reject;
            Console.WriteLine($"{name} reject request:{request.key}");

            return request;
        }

        public AskForRest GetPorxyAlreadyRestThatDay(string name, string proxy)
        {
            var request = new AskForRest(
                name,
                proxy,
                new DateTime(2020, 2, 10),
                new DateTime(2020, 2, 13));
            //SendRequest
            request.CheckingStatus = RequestStatus.Finish;
            request.CheckList=new Queue<string>();

            Console.WriteLine($"{request.Name}新增了一張假單 代理人{request.Proxy}" +
                              $" 時間{request.StartTime}到{request.EndTime}");

            return request;
        }
    }
}
