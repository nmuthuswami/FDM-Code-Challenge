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
    /// This class holds Mars Rover related operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    internal class MarsRover<T, S> : IRover<T,S> where T : RequestMarsRover where S : ResponseMarsRover
    {
        #region Private Variable
        private Point roverBoundaryLimit;
        #endregion

        #region Public Method

        #region Operate
        /// <summary>
        /// This method operates Mars Rover.
        /// </summary>
        /// <param name="request">object of RequestMarsRover</param>
        /// <returns>object of ResponseMarsRover</returns>
        public S Operate(T request)
        {
            ResponseMarsRover response = new ResponseMarsRover();
            if (!request.IsValid())//validate request information
            {
                response.MessageStatus = ResponseStatus.Fail;
                response.ResponseMessage = "Invalid Input Parameters";
            }
            else
            {
                int roverX = -1, roverY = -1;
                char roverDirection;
                Tuple<int, int, char> currentPosition;

                roverBoundaryLimit = request.RightCoOrdinates.Value;
                //Iterate over RoverDetails to action against one rover at a time.
                foreach (MarsRoverDetails roverDetails in request.RoverDetails)
                {
                    /*Start: Below snippet is to collect rover's x-axis, y-axis & direction. Collected 
                     * information is stored in local variable.
                    */
                    if((!int.TryParse(Convert.ToString(roverDetails.StartingPosition[0]),out roverX)) ||
                        (!int.TryParse(Convert.ToString(roverDetails.StartingPosition[1]), out roverY)) || 
                        (!char.IsLetter(roverDetails.StartingPosition[2])))
                    {
                        response.MessageStatus = ResponseStatus.Fail;
                        response.ResponseMessage = $"Incorrect Startingposition has been supplied to RoverID:{roverDetails.RoverId}";
                    }

                    roverDirection = roverDetails.StartingPosition[2];
                    //end

                    //Iterate through one by one instruction to make the rover move.
                    foreach (char inst in roverDetails.Instruction)
                    {
                        currentPosition = InstructRover(roverX, roverY, inst, roverDirection);
                        roverX = currentPosition.Item1;
                        roverY = currentPosition.Item2;
                        roverDirection = currentPosition.Item3;
                    }

                    //concatenate each rover final position by seperating them by '|'
                    if (!string.IsNullOrWhiteSpace(response.ResponseData))
                    {
                        response.ResponseData += "|";
                    }

                    //gather rover information as "{x-axis}{y-axis}{direction}" and store the result in response object.
                    response.ResponseData += $"{roverX}{roverY}{roverDirection}";
                }

                //validate the response information has been collected successfully.
                if (!string.IsNullOrWhiteSpace(response.ResponseData))
                {
                    response.MessageStatus = ResponseStatus.Success;
                    response.ResponseMessage = string.Empty;
                }
                else//set the message status as fail and log the reason for failure.
                {
                    response.MessageStatus = ResponseStatus.Fail;
                    response.ResponseMessage = "Something went wrong while instructing the rover";
                }                
            }
            
            return (S)response;
        }
        #endregion

        #endregion

        #region Private Methods

        #region InstructRover
        /// <summary>
        /// This method redirects the rover with the specified instruction & direction.
        /// </summary>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <param name="instruction"></param>
        /// <param name="direction"></param>
        /// <returns>rover's current x-axis, y-axis & direction</returns>
        private Tuple<int,int,char> InstructRover(int xAxis, int yAxis, char instruction, char direction)
        {
            int outXAxis = xAxis, outYAxis = yAxis;
            char outDirection = direction;
            switch (instruction)
            {
                case 'L':
                    outDirection = MoveLeft(direction);
                    break;
                case 'R':
                    outDirection = MoveRight(direction);
                    break;
                case 'M':
                    var mfOutput = MoveForward(xAxis, yAxis,direction);
                    outXAxis = mfOutput.Item1;
                    outYAxis = mfOutput.Item2;
                    break;
            }

            return new Tuple<int, int, char>(outXAxis, outYAxis, outDirection);
        }
        #endregion

        #region MoveLeft
        /// <summary>
        /// This methods changes the rover's direction to left and returns the new direction details.
        /// North -> West; East -> North; South -> East; West -> South
        /// </summary>
        /// <param name="direction">Current Rover's Direction</param>
        /// <returns>new direction details</returns>
        private char MoveLeft(char direction)
        {            
            char result = direction;

            switch (direction)
            {
                case 'N':
                    result = 'W';
                    break;
                case 'E':
                    result = 'N';
                    break;
                case 'S':
                    result = 'E';
                    break;
                case 'W':
                    result = 'S';
                    break;
            }

            return result;
        }
        #endregion

        #region MoveRight
        /// <summary>
        /// This method changes the rover's direction to right and returns the new direction details.
        /// North -> East; East -> South; South -> West; West -> North
        /// </summary>
        /// <param name="direction">Current Rover's Direction</param>
        /// <returns>new direction details</returns>
        private char MoveRight(char direction)
        {         
            char result = direction;
            switch (direction)
            {
                case 'N':
                    result = 'E';
                    break;
                case 'E':
                    result = 'S';
                    break;
                case 'S':
                    result = 'W';
                    break;
                case 'W':
                    result = 'N';
                    break;
            }

            return result;
        }
        #endregion

        #region MoveForward
        /// <summary>
        /// This method moves rover to one step forward in the specified direction.
        /// </summary>
        /// <param name="xAxis">rover's xaxis position</param>
        /// <param name="yAxis">rover's yaxis position</param>
        /// <param name="direction">rover's direction</param>
        /// <returns>rover's new position details (xaxis, yaxis, direction)</returns>
        private Tuple<int,int> MoveForward(int xAxis, int yAxis, char direction)
        {
            int outXAxis = xAxis, outYAxis = yAxis;

            switch (direction)
            {
                case 'N':
                    if (outYAxis < roverBoundaryLimit.Y)//verify yaxis is within upper boundary limit
                    {
                        outYAxis += 1;
                    }
                    break;
                case 'E':
                    if (outXAxis < roverBoundaryLimit.X)//verify xaxis is within upper boundary limit
                    {
                        outXAxis += 1;
                    }
                    break;
                case 'S':
                    if (outYAxis > 0)//verify yaxis is within lower boundary limit(0,0)
                    {
                        outYAxis -= 1;
                    }
                    break;
                case 'W':
                    if (outXAxis > 0)//verify xaxis is within lower boundary limit(0,0)
                    {
                        outXAxis -= 1;
                    }
                    break;
            }

            return new Tuple<int, int>(outXAxis, outYAxis);
        }
        #endregion

        #endregion
    }
}
