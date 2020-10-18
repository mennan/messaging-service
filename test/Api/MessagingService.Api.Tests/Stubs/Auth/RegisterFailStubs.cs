using System.Collections;
using System.Collections.Generic;
using MessagingService.Api.Models;

namespace MessagingService.Api.Tests
{
    public class RegisterFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new RegisterModel
                {
                    Name = "john",
                    Email = "johndoe@gmail.com",
                    Password = "SampleJohnPassword"
                }
            };

            yield return new object[]
            {
                new RegisterModel
                {
                    Name = "jane",
                    Email = "janedoe@gmail.com",
                    Password = "FakePassword"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}