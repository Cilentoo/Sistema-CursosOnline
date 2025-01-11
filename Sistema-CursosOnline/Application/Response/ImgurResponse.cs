using Sistema_CursosOnline.Application.Request;

namespace Sistema_CursosOnline.Application.Response
{
    public class ImgurResponse
    {
        public ImgurData Data { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
    }
}
