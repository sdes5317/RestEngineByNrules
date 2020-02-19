using NRules.Fluent.Dsl;
using RestEngineByNrules.Dto;

namespace RestEngineByNrules.MyRules
{
    public class IsFinishRule : Rule
    {
        /// <summary>
        /// 通知申請人假單完成
        /// </summary>
        public override void Define()
        {
            AskForRest request = null;
            RequestStatus status =RequestStatus.Init;

            When()
                .Match<AskForRest>(() => request, x => x.IsFinish, x => x.CheckingStatus == RequestStatus.Checking);

            Then()
                .Do(_ => MessageService.SendMessageToPerson(request.Name, $"{request.key} has finish"))
                .Do(_ => SetCheckingStatusToFinish(request))
                .Do(ctx => ctx.Update(request));
        }

        private static void SetCheckingStatusToFinish(AskForRest request)
        {
            request.CheckingStatus = RequestStatus.Finish;
        }
    }

}
