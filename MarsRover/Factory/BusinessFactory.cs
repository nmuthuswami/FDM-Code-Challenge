using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rover.BusinessObject;

namespace Rover.BusinessLogic
{
    /// <summary>
    /// This is a factory class which is exposed to other components to access this assembly classes.
    /// </summary>
    public class BusinessFactory
    {
        /// <summary>
        /// This static method instantiate MarsRover details.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <returns></returns>
        public static IRover<T, S> GetMarsRoverInstance<T, S>() where T : RequestMarsRover where S : ResponseMarsRover
        {
            return new MarsRover<T, S>();
        }
    }
}
