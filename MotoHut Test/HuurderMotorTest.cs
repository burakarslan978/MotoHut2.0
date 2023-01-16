using Business;
using HuurderMotorCollectionTest.TestDal;
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