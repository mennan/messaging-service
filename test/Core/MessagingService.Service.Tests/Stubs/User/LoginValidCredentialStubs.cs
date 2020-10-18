using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class LoginValidCredentialStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = "1",
                    Name = "mennan",
                    Email = "mennankose@gmail.com",
                    Password = "SamplePassword".Encrypt(),
                },
                new User
                {
                    Id = "2",
                    Name = "anil",
                    Email = "anilaatalay@gmail.com",
                    Password = "SamplePassword111".Encrypt(),
                }
            }.AsQueryable();
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "mennan",
                    Password = "SamplePassword"
                }
            };
            
            yield return new object[]
            {
                users,
                new LoginDto
                {
                    Name = "anil",
                    Password = "SamplePassword111"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}