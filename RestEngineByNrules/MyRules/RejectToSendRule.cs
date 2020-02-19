using NRules.Fluent.Dsl;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules
{
    public class RejectToSendRule : Rule
    {
        /// <summary>
        /// 通知申請人假單被拒絕
        /// </summary>
        public override void Define()
        {
            AskForRest request = null;
            When()
                .Match<AskForRest>(() => request, x => x.CheckingStatus == RequestStatus.Reject);

            Then()
                .Do(_ => MessageService.SendMessageToPerson(
                    request.Name, $"{request.key} reject by {request.CheckingPerson}"));
        }
    }
}