using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Util
{
    public class Response
    {
        public enum ResponseStatusEnum
        {
            Success,
            Warning,
            Error,
        }

        public ResponseStatusEnum StatusEnum { get; private init; }

        public string Message { get; private init; }

        public object? Data { get; private init; }

        [JsonIgnore] public int StatusCode { get; private set; }

        public static Response Return(ResponseStatusEnum statusEnum, string message, object data)
        {
            return new Response()
            {
                StatusEnum = statusEnum,
                Message = message,
                Data = data,
            };
        }
    }
}