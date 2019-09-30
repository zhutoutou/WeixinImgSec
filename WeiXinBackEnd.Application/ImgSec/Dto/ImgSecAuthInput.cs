namespace WeiXinBackEnd.Application.ImgSec.Dto
{
    public class ImgSecAuthInput
    {
        public ImgSecAuthInput(string fileName,byte[] file)
        {
            File = file;
            FileName = fileName;
        }

        public string FileName { get; set; }

        public byte[] File { get; set; }

        public string FileUrl { get; set; }
    }
}