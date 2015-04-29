using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
   public interface IUserManagerRepository
    {
       Task<User> GetByUsername(string username);
    }
}
