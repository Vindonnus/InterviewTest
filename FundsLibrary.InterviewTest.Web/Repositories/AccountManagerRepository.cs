using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public interface IAccountManagerModelRepository
    {
        Task<LoginModel> GetByUsername(string username);
    }

    public class AccountManagerModelRepository : IAccountManagerModelRepository
    {
        private const string _serviceAppUrl = "http://localhost:50135/Service/";

        private readonly IHttpClientWrapper _client;
        private readonly IMapper<User, LoginModel> _toModelMapper;

        public AccountManagerModelRepository(
            IHttpClientWrapper client = null,
            IMapper<User, LoginModel> toModelMapper = null)
        {
            _client = client ?? new HttpClientWrapper(_serviceAppUrl);
            _toModelMapper = toModelMapper ?? new ToLoginModelMapper();
        }

        public async Task<LoginModel> GetByUsername(string username)
        {
            var user = await _client.GetAndReadFromContentGetAsync<User>("api/UserManager/"+username);
            return _toModelMapper.Map(user);
        }
    }
}