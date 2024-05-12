using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Domain.Common
{
    public class Enums
    {
        public enum MessageCodes
        {
            Success,
            NoDataFound,
            BadRequest,
            InvalidMaxNumberOfApplication,
            InternalServerError
        }
    }
}
