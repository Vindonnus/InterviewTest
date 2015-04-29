using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class ToFundManagerModelMapper : IMapper<FundManager, FundManagerModel>
    {
        public FundManagerModel Map(FundManager obj)
        {
            return new FundManagerModel
            {
                Id = obj.Id,
                Biography = obj.Biography,
                Location = obj.Location,
                ManagedSince = obj.ManagedSince,
                Name = obj.Name
            };
        }

        public FundManager ReverseMap(FundManagerModel obj)
        {
            return new FundManager
            {
                Id = obj.Id,
                Biography = obj.Biography,
                Location = obj.Location,
                ManagedSince = obj.ManagedSince,
                Name = obj.Name
            };
        }
    }
}
