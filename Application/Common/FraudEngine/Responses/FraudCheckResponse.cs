using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.FraudEngine.Responses
{
    internal class FraudCheckResponse
    {
        public bool IsFraud { get; set; }
        public string FraudMessage { get; set; } = string.Empty;

        public FraudCheckResponse(bool isFraud, string fraudMessage)
        {
            IsFraud = isFraud;
            FraudMessage = fraudMessage;
        }
    }
}