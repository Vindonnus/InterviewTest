using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FundsLibrary.InterviewTest.Service.Controllers
{
    public class UserManagerController : ApiController
    {
         private readonly IUserManagerRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public UserManagerController()
             : this(new UserManagerMemoryDb())
        {}

        public UserManagerController(IUserManagerRepository injectedRepository)
        {
            _repository = injectedRepository;
        }

          [Route("api/UserManager/{username}")]
        public async Task<User> GetByUsername(string username)
        {
            return await _repository.GetByUsername(username);
        }
    }
}