using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MotorTest
    {
        [TestMethod]
        public void Rent_Dates_Dont_Overlap()
        {
            List<HuurderMotor> list = new List<HuurderMotor>();
            HuurderMotorCollection collection= new HuurderMotorCollection();
            list = collection.GetHuurderMotorList();


            foreach(var item in list)
            {
                foreach(var item2 in list)
                {
                    if(item.MotorId == item2.MotorId)
                    {
                        if (item.HuurderMotorId != item2.HuurderMotorId)
                        {
                            if (item.IsGeaccepteerd == true)
                            {
                                Assert.IsFalse(item.OphaalDatum <= item2.InleverDatum && item.InleverDatum >= item2.OphaalDatum);
                            }
                        }
                    }

                }
            }

            Assert.IsFalse(false);

        }

    }
}