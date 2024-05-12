using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Employment.Domain.Common.Enums;

namespace Employment.Domain.Common
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            this.MessageCodes = new MessageCodes();
            
        }
        public ResponseModel(T _result, MessageCodes _messageCodes ,  string _message )
        {
            Result = _result;
            MessageCodes = _messageCodes;
            Message = _message;
        }
        public T Result { get; set; }

        public MessageCodes MessageCodes { get; set; }
        public string Message { get; set; }
    }
}
