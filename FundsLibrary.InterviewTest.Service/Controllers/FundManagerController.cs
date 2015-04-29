using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;

namespace FundsLibrary.InterviewTest.Service.Controllers
{
    public class FundManagerController : ApiController
    {
        private readonly IFundManagerRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public FundManagerController()
            : this(new FundManagerMemoryDb())
        {}

        public FundManagerController(IFundManagerRepository injectedRepository)
        {
            _repository = injectedRepository;
        }

        public async Task<IEnumerable<FundManager>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/FundManager/79c74c79-f993-454e-a7d4-53791f17f179
        public async Task<FundManager> Get(Guid id)
        {
            return await _repository.GetBy(id);
        }

        /// <summary>
        /// Get Paged Fund Manager list
        /// </summary>
        [Route("api/FundManager/{currentOrder}/{sortDirection}")]
        public async Task<IEnumerable<FundManager>> GetPagedFundManager(string currentOrder, SortDirecton sortDirection)
        {
            return await _repository.GetPagedFundManager(currentOrder, sortDirection);
        }

        /// <summary>
        /// Update method added
        /// </summary>
        /// <param name="fundManager"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FundManager> Update(FundManager fundManager)
        {
            _repository.Update(fundManager.Id, fundManager);
            return await _repository.GetBy(fundManager.Id); // TODO: Change this into a TResult?
        }

        /// <summary>
        /// Handle the creation of a new fundmanager
        /// </summary>
        /// <param name="fundManager"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<FundManager> Create(FundManager fundManager)
        {
            _repository.Create(fundManager);
            return await _repository.GetBy(fundManager.Id); // TODO: Change this into a TResult?
        }
    }
}
