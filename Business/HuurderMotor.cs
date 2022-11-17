using Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HuurderMotor : IHuurderMotor
    {
        public int HuurderMotorId { get; set; }
        public int MotorId { get; set; }
        public int HuurderId { get; set; }
        public DateTime OphaalDatum { get; set; }
        public DateTime InleverDatum { get; set; }

    }
}
