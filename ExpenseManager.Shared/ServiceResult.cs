using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared
{
    public class ServiceResult<T>
        where T : class 
    {
        public bool Success { get; set; }
        public IEnumerable<string> Messages { get; set; } = new List<string>();
        public T? Item { get; set; }

        public static ServiceResult<T> Ok(T item)
        {
            return new ServiceResult<T>()
            {
                Success = true,
                Item = item
            };
        }

        public static ServiceResult<T> Fail(string message)
        {
            return Fail(new List<string>() { message }, null);
        }

        public static ServiceResult<T> Fail(IEnumerable<string> messages)
        {
            return Fail(messages);
        }

        public static ServiceResult<T> Fail(string message, T item)
        {
            return Fail(new List<string>() { message }, item);
        }
        public static ServiceResult<T> Fail(IEnumerable<string> messages, T? item)
        {
            return new ServiceResult<T>()
            {
                Success = false,
                Messages = messages,
                Item = item
            };
        }
    }
}
