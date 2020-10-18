using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class LoginFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var users = new List<User>
            {
                new User
                {
                    Name = "mennan",
                    Email = "mennankose@gmail.com",
                    Password = "SamplePassword",
                }
            }.AsQueryable();
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    
                }
            };
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "mennan"
                }
            };
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Password = "SamplePassword"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}