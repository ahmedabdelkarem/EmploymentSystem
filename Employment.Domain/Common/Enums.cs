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
            DataFound=3,
            BadRequest = 4,
            InvalidMaxNumberOfApplication = 5,
            InternalServerError =6,
            ApplicationExistTodayWithSameApplicant = 7,
            

        }
    }
}
