using System.Collections;
using System.Collections.Generic;
using MessagingService.Service;

namespace MessagingService.Api.Tests
{
    public class SendMessageSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = true,
                    Message = "Message sent successfully.",
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