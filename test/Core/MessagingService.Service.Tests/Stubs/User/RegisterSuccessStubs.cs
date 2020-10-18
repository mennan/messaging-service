using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class RegisterSuccessStubs : IEnumerable<object[]>
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
                },
                new User
                {
                    Name = "anil",
                    Email = "anilaatalay@gmail.com",
                    Password = "SamplePassword"
                }
            }.AsQueryable();
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "ekrem",
                    Email = "ekremkose@gmail.com",
                    Password = "SamplePassword"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "mahmut",
                    Email = "mahmutakpinar@gmail.com",
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