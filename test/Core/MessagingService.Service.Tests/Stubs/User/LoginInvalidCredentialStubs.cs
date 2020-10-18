using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class LoginInvalidCredentialStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var users = new List<User>
            {
                new User
                {
                    Name = "mennan",
                    Email = "mennankose@gmail.com",
                    Password = "SamplePassword".Encrypt(),
                }
            }.AsQueryable();
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "mennan",
                    Password = "Pass1234"
                }
            };
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "mennan",
                    Password = "SamplePassword@Pass"
                }
            };
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "anil",
                    Password = "11SamplePassword@Pass"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}