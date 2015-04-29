using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FundsLibrary.InterviewTest.Service.Repositories
{
    public class UserManagerMemoryDb : IUserManagerRepository
    {
        
        private static readonly ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>();

        static UserManagerMemoryDb()
        {
            //fake data

            var users = new[]
            {
              new User { Id = Guid.Parse("13eed26a-d1e3-4e78-8d3d-c4c9b63eb548"),Name="User1", Username = "User1", Password = "Password1", Roles =new List<Role>(){new Role(){Id=1, Name="Admin" } }},
              new User { Id = Guid.Parse("23eed26a-d1e3-4e78-8d3d-c4c9b63eb548"),Name="User2", Username = "User2", Password = "Password1", Roles =new List<Role>(){new Role(){Id=2, Name="Client" } }},
              new User { Id = Guid.Parse("33eed26a-d1e3-4e78-8d3d-c4c9b63eb548"),Name="User3", Username = "User3", Password = "Password1", Roles = new List<Role>(){ new Role(){Id=1, Name="Admin" },new Role(){Id=2, Name="Client" }} },
            };

            var roles = new[]
            {
              new Role { Id = 1,Name="Admin" },
              new Role { Id = 2,Name="Client"} 
              };

            foreach (var user in users)
                _users.TryAdd(user.Username.ToUpper(), user);
        }

        public Task<User> GetByUsername(string userName)
        {
            User value;
            _users.TryGetValue(userName.ToUpper(), out value);

            return Task.FromResult(value);
        }
    
    }
}