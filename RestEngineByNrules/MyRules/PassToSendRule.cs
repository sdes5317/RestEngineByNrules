using NRules.Fluent.Dsl;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules
{
    public class PassToSendRule : Rule
    {
        /// <summary>
        /// 通知申請人假單被通過
        /// </summary>
        public override void Define()
        {
            AskForRest request = null;
            When()
                .Match<AskForRest>(() => request, x => x.CheckingStatus == RequestStatus.Pass);

            Then()
                .Do(_ => MessageService.SendMessageToPerson(
                    request.Name, $"{request.key} pass by {request.CheckingPerson}"))
                .Do(_ => SetCheckingStatusToPass(request))
                .Do(_ => request.CheckList.Dequeue())
                .Do(ctx => ctx.Update(request));
        }
        private static void SetCheckingStatusToPass(AskForRest request)
        {
            request.CheckingStatus = RequestStatus.Checking;
        }
    }
}
