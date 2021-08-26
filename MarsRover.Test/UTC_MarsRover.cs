using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rover.BusinessLogic;
using Rover.BusinessObject;


namespace MarsRover.Test
{
    [TestClass]
    public class UTC_MarsRover
    {
        #region Validation Test Cases
        [TestMethod]
        public void UTC_MarsRover_Validate_InputParameters()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();

            RequestMarsRover request = new RequestMarsRover();
            ResponseMarsRover response;

            //Without passing any parameter
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing only rightcoordinates
            request = new RequestMarsRover()
            {
                RightCoOrdinates = new System.Drawing.Point(5, 5)
            };
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & empty roverdetails
            request = new RequestMarsRover()
            {
                RightCoOrdinates = new System.Drawing.Point(5, 5),
                RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>()
            };
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & with rover details without instructions
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & rover details with incorrect startingposition
            rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12";
            rover1.Instruction = "LMLMLM";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & rover details with incorrect startingposition
            rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLM";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & rover details with 1 rover by not passing the instruction
            rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLM";
            MarsRoverDetails rover2 = new MarsRoverDetails();
            rover2.RoverId = 2;
            rover2.StartingPosition = "12N";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            request.RoverDetails.Add(rover2);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & rover details with 1 rover by not passing the right position details
            rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLM";
            rover2 = new MarsRoverDetails();
            rover2.RoverId = 2;
            rover2.StartingPosition = "12";
            rover2.Instruction = "LMLMLM";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            request.RoverDetails.Add(rover2);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Fail, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual("Invalid Input Parameters", response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");

            //passing rightcooridnates & instruction to 2 rover details 
            rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLM";
            rover2 = new MarsRoverDetails();
            rover2.RoverId = 2;
            rover2.StartingPosition = "12N";
            rover2.Instruction = "LMLMLM";
            request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            request.RoverDetails.Add(rover1);
            request.RoverDetails.Add(rover2);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response.ResponseData), "Response ResponseData doesn't match with the expected result");
        }
        #endregion

        /// <summary>
        /// This unit test handle first case specified in the challenge 
        /// Input :::::::: boundary-limit = 5 5;rover-position: 1 2 N;instruction: LMLMLMLMM
        /// Output ::::::: 1 3 N 
        /// </summary>
        [TestMethod]        
        public void UTC_MarsRover_Scenario1()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLMLMM";
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("13N", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        /// <summary>
        /// This unit test handle second case specified in the challenge 
        /// Input :::::::: boundary-limit = 5 5;rover-position: 3 3 E;instruction: MMRMMRMRRM 
        /// Output ::::::: 5 1 E
        /// </summary>
        [TestMethod]
        public void UTC_MarsRover_Scenario2()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);

            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "33E";
            rover1.Instruction = "MMRMMRMRRM";
            request.RoverDetails.Add(rover1);

            response = _businessFactory.Operate(request);

            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("51E", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        /// <summary>
        /// This unit test method verifies both the above scenarios in a single run.
        /// </summary>
        [TestMethod]
        public void UTC_MarsRover_BothScenarios()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);

            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLMLMM";
            request.RoverDetails.Add(rover1);
            MarsRoverDetails rover2 = new MarsRoverDetails();
            rover2.RoverId = 2;
            rover2.StartingPosition = "33E";
            rover2.Instruction = "MMRMMRMRRM";
            request.RoverDetails.Add(rover2);

            response = _businessFactory.Operate(request);//call to businesslogic.

            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            //please note: since this method operates 2 rovers, so the output result is concatenated by '|'
            Assert.AreEqual("13N|51E", response.ResponseData, "Response ResponseData doesn't match with the expected result");            
        }

        #region Extra Test Cases

        [TestMethod]
        public void UTC_MarsRover_Scenario3()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "42S";
            rover1.Instruction = "RRMMLMMLMMM";
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("21S", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        [TestMethod]
        public void UTC_MarsRover_Scenario4()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "24W";
            rover1.Instruction = "RMMRMMMMMLMM";
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("55N", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        [TestMethod]
        public void UTC_MarsRover_Scenario5()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "51S";
            rover1.Instruction = "RMMMLMRMMMM";
            request.RoverDetails.Add(rover1);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("00W", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        [TestMethod]
        public void UTC_MarsRover_AllScenarios()
        {
            var _businessFactory = BusinessFactory.GetMarsRoverInstance<RequestMarsRover, ResponseMarsRover>();
            ResponseMarsRover response;

            RequestMarsRover request = new RequestMarsRover();
            request.RightCoOrdinates = new System.Drawing.Point(5, 5);
            request.RoverDetails = new System.Collections.Generic.List<MarsRoverDetails>();
            MarsRoverDetails rover1 = new MarsRoverDetails();
            rover1.RoverId = 1;
            rover1.StartingPosition = "12N";
            rover1.Instruction = "LMLMLMLMM";
            request.RoverDetails.Add(rover1);
            MarsRoverDetails rover2 = new MarsRoverDetails();
            rover2.RoverId = 2;
            rover2.StartingPosition = "33E";
            rover2.Instruction = "MMRMMRMRRM";
            request.RoverDetails.Add(rover2);
            MarsRoverDetails rover3 = new MarsRoverDetails();
            rover3.RoverId = 3;
            rover3.StartingPosition = "42S";
            rover3.Instruction = "RRMMLMMLMMM";
            request.RoverDetails.Add(rover3);
            MarsRoverDetails rover4 = new MarsRoverDetails();
            rover4.RoverId = 4;
            rover4.StartingPosition = "24W";
            rover4.Instruction = "RMMRMMMMMLMM";
            request.RoverDetails.Add(rover4);
            response = _businessFactory.Operate(request);
            MarsRoverDetails rover5 = new MarsRoverDetails();
            rover5.RoverId = 5;
            rover5.StartingPosition = "51S";
            rover5.Instruction = "RMMMLMRMMMM";
            request.RoverDetails.Add(rover5);
            response = _businessFactory.Operate(request);
            Assert.AreEqual(ResponseStatus.Success, response.MessageStatus, "Response MessageStatus doesn't match with the expected result");
            Assert.AreEqual(string.Empty, response.ResponseMessage, "Response ResponseMessage doesn't match with the expected result");
            Assert.AreEqual("13N|51E|21S|55N|00W", response.ResponseData, "Response ResponseData doesn't match with the expected result");
        }

        #endregion
    }
}
