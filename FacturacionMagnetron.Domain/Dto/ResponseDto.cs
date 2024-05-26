using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionMagnetron.Domain.Dto
{
    public class ResponseDto<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string MessageError { get; set; }

        public ResponseDto() { }
        private ResponseDto(T value, bool isSuccess, string messageError)
        {
            Value = value;
            IsSuccess = isSuccess;
            MessageError = messageError;
        }

        public static ResponseDto<T> Success(T value) => new ResponseDto<T>(value, true, null);
        public static ResponseDto<T> Failure(string messageError)=> new ResponseDto<T> (default,false,messageError);

    }
}
