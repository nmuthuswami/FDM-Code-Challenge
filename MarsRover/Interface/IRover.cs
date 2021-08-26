using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rover.BusinessObject;

namespace Rover.BusinessLogic
{
    /// <summary>
    /// This interface handles operation for all planet Rover.
    /// </summary>
    /// <typeparam name="T">request class</typeparam>
    /// <typeparam name="S">response class</typeparam>
    public interface IRover<T, S>
    {
        /// <summary>
        /// This method operates Rover.
        /// </summary>
        /// <param name="request">Rover Information</param>
        /// <returns>Rover's new position details</returns>
        S Operate(T request);
    }
}
