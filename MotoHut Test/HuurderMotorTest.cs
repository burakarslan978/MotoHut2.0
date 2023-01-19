using Business;
using HuurderMotorCollectionTest.TestDal;
using System;
using System.ComponentModel;
using System.Net.Mail;

namespace MotoHut_Test
{
    [TestClass]
    public class HuurderMotorTest
    {
        [TestMethod]
        public void TestCheckAvailability()
        {
            // Arrange
            int motorId = 2;
            DateTime ophaalDatum = new DateTime(2022, 2, 6); //true bij null en/of false
            DateTime inleverDatum = new DateTime(2022, 2, 9);

            DateTime ophaalDatum2 = new DateTime(2022, 2, 1); //false inleverdatum voor ophaaldatum
            DateTime inleverDatum2 = new DateTime(2022, 2, 3);

            DateTime ophaalDatum3 = new DateTime(2022, 2, 4); //false ophaaldatum voor inleverdatum
            DateTime inleverDatum3 = new DateTime(2022, 2, 8);


            var testObject = new HuurderMotor(new HuurderMotorTestDal(), new HuurderMotorCollectionTestDal());

            // Act
            bool IsAvailable = testObject.CheckAvailability(motorId, ophaalDatum, inleverDatum);
            bool IsAvailable2 = testObject.CheckAvailability(motorId, ophaalDatum2, inleverDatum2);
            bool IsAvailable3 = testObject.CheckAvailability(motorId, ophaalDatum3, inleverDatum3);

            // Assert
            Assert.IsTrue(IsAvailable);
            Assert.IsFalse(IsAvailable2);
            Assert.IsFalse(IsAvailable3);
        }

        [TestMethod]
        [DataRow("2022-2-6", "2022-2-9", true, DisplayName = "Geen overlappende")]
        [DataRow("2022-2-1", "2022-2-3", false, DisplayName = "Inleverdatum voor ophaaldatum")]
        [DataRow("2022-2-4", "2022-2-8", false, DisplayName = "Ophaaldatum voor inleverdatum")]
        public void _TestCheckAvailability(string ophaalDatum, string inleverDatum, bool expected)
        {
            //Arrange
            int motorId = 2;
           
            var testObject = new HuurderMotor(new HuurderMotorTestDal(), new HuurderMotorCollectionTestDal());

            // Act
            bool IsAvailable = testObject.CheckAvailability(motorId, DateTime.Parse(ophaalDatum), DateTime.Parse(inleverDatum));

            //Assert
            Assert.AreEqual(expected, IsAvailable);
        }

        [TestMethod]
        public void GetMotorId5ResultYamaha()
        {
            //Arrange
            Motor motor = new Motor(new MotorTestDal());
            //Act
            Motor motorResult = motor.GetMotor(5);
            //Assert
            Assert.IsTrue(motorResult.Model == "Yamaha");
        }
    }
}