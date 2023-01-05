using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Project.Dtos
{
    public class ResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public ResponseDto(string message)
        {
            Message = message;
        }
    }
}