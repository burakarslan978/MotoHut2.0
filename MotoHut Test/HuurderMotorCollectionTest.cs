using Business;
using HuurderMotorCollectionTest.TestDal;

namespace MotoHut_Test
{
    [TestClass]
    public class HuurderMotorCollectionTest
    {
        [TestMethod]
        public void TestDeclineOverlappingRents()
        {
            // Arrange
            int huurderMotorId = 0;
            int motorId = 2;
            DateTime ophaalDatum = new DateTime(2022, 1, 1);
            DateTime inleverDatum = new DateTime(2022, 1, 15);

            var testObject = new HuurderMotorCollection(new HuurderMotorCollectionTestDal(), new HuurderMotorTestDal());

            // Act
            List<HuurderMotor> resultList = testObject.DeclineOverlappingRents(huurderMotorId, motorId, ophaalDatum, inleverDatum);

            // Assert
            Assert.IsFalse(resultList[0].IsGeaccepteerd); // false ophaaldatum overlapt
            Assert.IsFalse(resultList[1].IsGeaccepteerd); // false inleverdatum overlapt
            Assert.IsNull(resultList[2].IsGeaccepteerd); // blijft null 
        }

        [TestMethod]
        [DataRow(0, false, DisplayName = "Ophaaldatum overlapt")]
        [DataRow(1, false, DisplayName = "Inleverdatum overlapt")]
        [DataRow(2, null, DisplayName = "Geen overlapping, blijft null")]
        public void _TestDeclineOverlappingRents(int index, bool? expected)
        {
            //Arrange
            int huurderMotorId = 0;
            int motorId = 2;
            DateTime ophaalDatum = new DateTime(2022, 1, 1);
            DateTime inleverDatum = new DateTime(2022, 1, 15);

            var testObject = new HuurderMotorCollection(new HuurderMotorCollectionTestDal(), new HuurderMotorTestDal());

            // Act
            List<HuurderMotor> resultList = testObject.DeclineOverlappingRents(huurderMotorId, motorId, ophaalDatum, inleverDatum);

            // Assert
            Assert.AreEqual(resultList[index].IsGeaccepteerd, expected);
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