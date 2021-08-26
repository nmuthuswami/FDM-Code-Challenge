using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rover.BusinessObject
{
    /// <summary>
    /// This class hold Mars Rover Details.
    /// </summary>
    public class MarsRoverDetails
    {
        public int RoverId { get; set; }
        public string StartingPosition { get; set; }
        public string Instruction { get; set; }

        #region Validation
        /// <summary>
        /// This method does validation against the MarsRoverDetails class properties.        
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool result = true;

            //verify the fields are empty.
            if (string.IsNullOrWhiteSpace(StartingPosition) || string.IsNullOrWhiteSpace(Instruction))
            {
                result = false;
            }
            else if (StartingPosition.Length != 3)//verify specified rover position is of length = 3 (For ex: "2 4 N")
            {
                return false;
            }

            return result;
        }
        #endregion
    }
}
