using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rover.BusinessObject
{
    /// <summary>
    /// This class hold request details for Mars Rover.
    /// </summary>
    public class RequestMarsRover: BaseRequest
    {
        public List<MarsRoverDetails> RoverDetails { get; set; }

        #region Validation
        /// <summary>
        /// This method validates the properties of this class & its child.
        /// </summary>
        /// <returns></returns>
        public override bool IsValid()
        {
            bool result = true;

            //verify the fields are empty & checks the child collection has records
            if ((!RightCoOrdinates.HasValue)|| (RoverDetails == null || RoverDetails.Count == 0))
            {
                result = false;
            }
            else if (RoverDetails.Count(c => c.IsValid() == false) > 0) //verify the child object records.
            {
                result = false;
            }

            return result;            
        }
        #endregion
    }
}
