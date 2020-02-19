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
            //Load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(NoticeNewRequestRule).Assembly));

            //Compile rules
            var factory = repository.Compile();

            //Create a working session
            var session = factory.CreateSession();
            //Fake Web Action
            var fakeAction=new FakeAction();
            var request= fakeAction.SendAskForRestRequest();

            session.Insert(request);
            session.Fire();

            request= fakeAction.SomeOnePassTheRequest(request);
            session.Update(request);
            session.Fire();
             request= fakeAction.SomeOneRejectTheRequest(request);
            session.Update(request);
            session.Fire();
            
             request= fakeAction.ReSendToContinueRequest(request);
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
    }
}
