namespace DocumentServices.Models.DTO
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        //kết quả trả veeff thành công hay không
        public bool IsSuccess { get; set; } = true;
        //trả về lỗi message (thành công hay thất bại)
        public string Message { get; set; } = "";
    }
}
