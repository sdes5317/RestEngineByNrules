using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRules.Fluent.Dsl;
using NRules.RuleModel;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules2
{
    [Repeatability(RuleRepeatability.NonRepeatable)]
    public class CheckProxyRule : Rule
    {
        public override void Define()
        {
            AskForRest request = null;
            AskForRest requestFinish = null;
            string person = null;
            When()
                .Match<AskForRest>(() => request, x => !x.IsFinish)
                .Exists<AskForRest>(
                    x => x.Name == request.Proxy && x.IsFinish,
                    x => CheckRestTime(x, request));

            Then()
                .Do(_ => SetCheckingStatusToInit(request))
                .Do(_ => MessageService.SendMessageToPerson(request.Name, $"假單:{request.key} 代理人{request.Proxy}在時間內有假，無法代理"))
                .Do(ctx => ctx.Update(request));
        }

        public static bool CheckRestTime(AskForRest x, AskForRest y)
        {
            return true;
        }
        private static void SetCheckingStatusToInit(AskForRest request)
        {
            request.CheckingStatus = RequestStatus.Init;
        }
    }
}
