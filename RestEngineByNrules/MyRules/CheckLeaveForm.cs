using NRules.Fluent.Dsl;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules
{
    public class NoticeNewRequestRule : Rule
    {
        /// <summary>
        /// 通知對象審核假單
        /// </summary>
        public override void Define()
        {
            AskForRest request = null;

            When()
                .Match<AskForRest>(() => request,
                    x => x.CheckingStatus == RequestStatus.Checking);

            Then()
                .Do(_ => MessageService.SendMessageToPerson(request.CheckingPerson, $"有一張{request.Name}的假單待批"));
        }
    }
}
