using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MessagingService.Dto;
using MessagingService.Entity;

namespace MessagingService.Service.Tests
{
    public class GetUnreadMessagesSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new List<Message>
                {
                    new Message
                    {
                        FromUser = "mennan",
                        ToUser = "anil",
                        Content = "Hello!",
                        IsRead = false,
                        ReadDate = DateTime.MinValue,
                        SentDate = DateTime.UtcNow
                    },
                    new Message
                    {
                        FromUser = "ekrem",
                        ToUser = "mennan",
                        Content = "Hi!",
                        IsRead = false,
                        ReadDate = DateTime.MinValue,
                        SentDate = DateTime.UtcNow
                    },
                }.AsQueryable(),
                new ReadUserMessageDto
                {
                    Name = "mennan",
                    Page = 1,
                    PerPage = 100
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}