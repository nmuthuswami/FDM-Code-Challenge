using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.BusinessObject
{
    /// <summary>
    /// Base class for all response classes
    /// </summary>
    public abstract class BaseResponse
    {
        public string ResponseMessage { get; set; }

        public ResponseStatus MessageStatus { get; set; }

    }

    /// <summary>
    /// This enum holds the Response Status information.
    /// </summary>
    public enum ResponseStatus
    {
        Success = 0,
        Fail = 1
    }
}
