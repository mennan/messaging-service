using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class RegisterFailStubs : IEnumerable<object[]>
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
                    
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "mennan"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Email = "mennankose@gmail.com"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Password = "samplePassword"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "ekrem",
                    Email = "ekremkose@gmail.com"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "ekrem",
                    Password = "SamplePassword"
                }
            };
            
            yield return new object[]
            {
                users,
                new RegisterDto
                {
                    Name = "mennan",
                    Email = "mennankose@gmail.com",
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