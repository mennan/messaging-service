using System.Collections;
using System.Collections.Generic;
using MessagingService.Service;

namespace MessagingService.Api.Tests
{
    public class BlockUserFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Model validation errors!",
                    Errors = new List<string> {"Blocked by user not found!"}
                },
                "Blocked by user not found!"
            };

            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Model validation errors!",
                    Errors = new List<string> {"Blocked user not found!"}
                },
                "Blocked user not found!"
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}