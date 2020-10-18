using System;
using System.Collections;
using System.Collections.Generic;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Service;

namespace MessagingService.Api.Tests
{
    public class AllMessagesSuccessStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ServiceResponse<PagedUserMessageDto>
                {
                    Success = true,
                    Message = "Messages listed successfully.",
                    Data = new PagedUserMessageDto()
                    {
                        Messages = new List<UserMessageDto>
                        {
                            new UserMessageDto
                            {
                                From = "mennan",
                                To = "anil",
                                Content = "Hello!",
                                IsRead = false,
                                ReadDate = DateTime.MinValue,
                                SentDate = DateTime.UtcNow
                            }
                        },
                        CurrentPage = 1,
                        PerPage = 100,
                        TotalPage = 1
                    }
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}