using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.BusinessObject
{
    /// <summary>
    /// Base class for all Request classes
    /// </summary>
    public abstract class BaseRequest
    {
        public Point? RightCoOrdinates { get; set; }
        public abstract bool IsValid();        
    }
}
