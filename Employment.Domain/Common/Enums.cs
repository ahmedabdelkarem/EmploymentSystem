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
            DataFound,
            NoDataFound,
            BadRequest,
            InvalidMaxNumberOfApplication,
            InternalServerError,
            ApplicationExistTodayWithSameApplicant
        }
    }
}
