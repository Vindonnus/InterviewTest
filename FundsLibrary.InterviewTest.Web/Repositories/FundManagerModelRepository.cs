using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IFundManagerModelRepository
    {
        Task<IEnumerable<FundManagerModel>> GetAll();
        Task<FundManagerModel> Get(Guid id);
        Task<IEnumerable<FundManagerModel>> GetPagedFundManager(string currentOrder, SortDirecton sortDirection);

        Task<FundManagerModel> Update(FundManagerModel model); // New function to update the fund managers details
        Task<FundManagerModel> Create(FundManagerModel model); // New function to create the new fund manager details

    }

    public class FundManagerModelRepository : IFundManagerModelRepository
    {
        private const string _serviceAppUrl = "http://localhost:50135/Service/";

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<FundManager, FundManagerModel> _toModelMapper;

        public FundManagerModelRepository(
            IHttpClientWrapper client = null,
            IMapper<FundManager, FundManagerModel> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new ToFundManagerModelMapper();
        }

        public async Task<IEnumerable<FundManagerModel>> GetAll()
        {
            var managers = await _client.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/");
            return managers.Select(s => _toModelMapper.Map(s));
        }

        public async Task<IEnumerable<FundManagerModel>> GetPagedFundManager(string currentOrder, SortDirecton sortDirection)
        {
            var managers = await _client.GetAndReadFromContentGetAsync<IEnumerable<FundManager>>("api/FundManager/" + currentOrder + "/" + sortDirection);
            return managers.Select(s => _toModelMapper.Map(s));
        }

        public async Task<FundManagerModel> Get(Guid id)
        {
            var manager = await _client.GetAndReadFromContentGetAsync<FundManager>("api/FundManager/" + id);
            return _toModelMapper.Map(manager);
        }

        /// <summary>
        /// Takes either an updated fund manager and updates its record
        /// </summary>
        public async Task<FundManagerModel> Update(FundManagerModel fundManagerModel)
        {

            var fundManager = _toModelMapper.ReverseMap(fundManagerModel);
            await _client.PostFromContentPostAsync<FundManager>("api/FundManager/", fundManager);

            return fundManagerModel;
        }

        /// <summary>
        /// Taskes a neewly created fund manager and sends it to be added
        /// </summary>
        public async Task<FundManagerModel> Create(FundManagerModel model)
        {
            var fundManager = _toModelMapper.ReverseMap(model);
            await _client.PutFromContentPutAsync("api/FundManager/", fundManager);

            return model; 
        }
    }
}
