using EMS.Service.Interface;

namespace EMS.Models
{
    public class Response:IResponse
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
