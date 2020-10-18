using System.Collections;
using System.Collections.Generic;
using MessagingService.Service;

namespace MessagingService.Api.Tests
{
    public class BlockUserSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = true,
                    Message = "User blocked successfully.",
                    Data = true
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}