using System.Collections;
using System.Collections.Generic;
using MessagingService.Dto;

namespace MessagingService.Service.Tests
{
    public class SendMessageFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new SendMessageDto
                {
                    
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    FromUser = "mennan"
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    ToUser = "anil"
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    Content = "Hello"
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    FromUser = "mennan",
                    ToUser = "anil"
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    FromUser = "mennan",
                    Content = "Hello!"
                }
            };
            
            yield return new object[]
            {
                new SendMessageDto
                {
                    ToUser = "anil",
                    Content = "Hello!"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}