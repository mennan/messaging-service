using System.Collections;
using System.Collections.Generic;
using MessagingService.Api.Models;

namespace MessagingService.Api.Tests
{
    public class LoginSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new LoginModel
                {
                    Name = "john",
                    Password = "SampleJohnPassword"
                }
            };

            yield return new object[]
            {
                new LoginModel
                {
                    Name = "mennan",
                    Password = "Pass@1234"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}