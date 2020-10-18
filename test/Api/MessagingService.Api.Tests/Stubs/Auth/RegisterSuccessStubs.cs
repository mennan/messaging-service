using System.Collections;
using System.Collections.Generic;
using MessagingService.Api.Models;
using MessagingService.Dto;

namespace MessagingService.Api.Tests
{
    public class RegisterSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RegisterModel
                {
                    Name = "mennan",
                    Email = "mennankose@gmail.com",
                    Password = "SamplePassword"
                }
            };

            yield return new object[]
            {
                new RegisterModel
                {
                    Name = "anil",
                    Email = "anilaatalay@gmail.com",
                    Password = "SampleUserPassword"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}