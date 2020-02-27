using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRules.Fluent.Dsl;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules3
{
    [Priority(1)]
    public class CheckRestRemainingRule:Rule
    {
        public override void Define()
        {
            IEnumerable<AskForRest> requests = null;
            Person person = null;
            When()
                .Match(() => person)
                .Query(() => requests, x => x.Match<AskForRest>(o => o.Name == person.Name,o=>o.CheckingStatus==RequestStatus.Checking)
                    .Collect()
                    .Where(c => c.Sum(y=> (y.EndTime - y.StartTime).Days)> person.RestRemaining));

            Then()
                .Do(_ => MessageService.SendMessageToPerson(person.Name, $"假單退回，補修天數不足"))
                .Do(_ => BackAllAskForRest(requests))
                .Do(ctx => ctx.UpdateAll(requests));
        }

        public static void BackAllAskForRest(IEnumerable<AskForRest> requests)
        {
            foreach (var askForRest in requests)
            {
                askForRest.CheckingStatus = RequestStatus.Init;
            }
        }
    }
}
