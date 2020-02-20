using System;
using NRules;
using NRules.Fluent;
using RestEngineByNrules.MyRules;

namespace RestEngineByNrules
{
    public class Program
    {
        static void Main(string[] args)
        {
            //正常請假流程
            NoramlSample();

            //代理人衝突的測試
            //ProxyErrorSample();
        }

        private static void NoramlSample()
        {
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(NoticeNewRequestRule).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();
            //Fake Web Action
            var fakeAction = new FakeAction();
            var request = fakeAction.SendAskForRestRequest();

            session.Insert(request);
            session.Fire();

            request = fakeAction.SomeOnePassTheRequest(request);
            session.Update(request);
            session.Fire();
            request = fakeAction.SomeOneRejectTheRequest(request);
            session.Update(request);
            session.Fire();

            request = fakeAction.ReSendToContinueRequest(request);
            session.Update(request);
            session.Fire();

            request = fakeAction.SomeOnePassTheRequest(request);
            session.Update(request);
            session.Fire();

            request = fakeAction.SomeOnePassTheRequest(request);
            session.Update(request);
            session.Fire();
        }

        private static void PrintObject<T>(T obj)
        {
            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                Console.WriteLine($"{propertyInfo.Name}:{propertyInfo.GetValue(obj)}");
            }

            Console.WriteLine();
        }

        private static void ProxyErrorSample()
        {
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(NoticeNewRequestRule).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();
            //Fake Web Action
            var fakeAction = new FakeAction();
            var request = fakeAction.SendAskForRestRequest();

            session.Insert(request);
            session.Fire();

            //增加一張遠松已經請假的事實
            var fakeResult = fakeAction.GetPorxyAlreadyRestThatDay("遠松", "子喬");
            session.Insert(fakeResult);
            session.Fire();
            //增加一張子喬已經請假的事實
            fakeResult = fakeAction.GetPorxyAlreadyRestThatDay("子喬", "123");
            session.Insert(fakeResult);
            session.Fire();
            //嘗試把代理人改成子喬
            request.Proxy = "子喬";
            session.Update(request);
            session.Fire();
            //再改回遠松,看會不會重新再發一次警報
            request.Proxy = "遠松";
            session.Update(request);
            session.Fire();
        }
    }
}
