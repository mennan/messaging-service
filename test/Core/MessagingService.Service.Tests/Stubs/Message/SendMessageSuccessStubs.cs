using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class SendMessageSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new List<User>
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
                }.AsQueryable(),
                new SendMessageDto
                {
                    FromUser = "anil",
                    ToUser = "mennan",
                    Content = "Hello World!"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}