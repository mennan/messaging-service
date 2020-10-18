using System.Collections;
using System.Collections.Generic;
using MessagingService.Dto;

namespace MessagingService.Service.Tests
{
    public class GetUnreadMessagesFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Name = "mennan"
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Page = 1
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    PerPage = 100
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Name = "mennan",
                    Page = 1
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Name = "mennan",
                    Page = 0
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Name = "mennan",
                    PerPage = 100
                }
            };
            
            yield return new object[]
            {
                new ReadUserMessageDto
                {
                    Name = "mennan",
                    Page = 0,
                    PerPage = 0
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}