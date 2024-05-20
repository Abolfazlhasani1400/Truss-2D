using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truss_2D
{
    public class Units
    {
        public Units()
        {

        }
        public enum AreaUnit
        {
            m2,
            mm2
        };
        public enum LengthUnit
        {
            m,
            mm
        };
        public enum ForceUnit
        {
            N,
            KN,
            MN
        }
        public enum LinkElasticityUnit
        {
            pa,
            Kpa,
            Mpa,
            Gpa
        }
        public enum LinkStressUnit
        {
            pa,
            Kpa,
            Mpa,
            Gpa
        }
    }
}
