using EMS.Services.Models.DTO;

namespace EMS.Service.Interface
{
    public interface IResponse
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
