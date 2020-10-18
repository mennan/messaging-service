using System;
using System.Collections;
using System.Collections.Generic;
using MessagingService.Dto;
using MessagingService.Service;

namespace MessagingService.Api.Tests
{
    public class SendMessageFailStubs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Model validation errors!",
                    Errors = new List<string> {"From user not found!"}
                },
                "From user not found!"
            };

            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Model validation errors!",
                    Errors = new List<string> {"To user not found!"}
                },
                "To user not found!"
            };

            yield return new object[]
            {
                new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Model validation errors!",
                    Errors = new List<string> {"Content is required."}
                },
                "Content is required."
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}