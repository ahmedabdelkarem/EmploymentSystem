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
            Success = 1,
            NoDataFound = 2,
            BadRequest = 3,
            InvalidMaxNumberOfApplication = 4,
            InternalServerError =5
        }
    }
}
