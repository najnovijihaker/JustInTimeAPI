using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.FraudEngine
{
    public class FraudEngineConfig
    {
        public double Treshold { get; set; } = 50;
        public double HoursTolerance { get; set; } = 2.5;
        public double PercentageTolerance { get; set; } = 35;
        public double RequestTimeDifferance { get; set; } = 15;

        //public double Multiplier { get; set; } = 3.556;
        public double SoftLimitWorkingHours { get; set; } = 9;

        public double HardLimitWorkingHours { get; set; } = 12;
    }
}