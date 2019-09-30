using System.Threading.Tasks;
using WeiXinBackEnd.Application.ImgSec.Dto;

namespace WeiXinBackEnd.Application.ImgSec
{
    public interface IImgSecApp
    {
        Task<ImgSecAuthResponse> AuthAsync(ImgSecAuthInput input);
    }
}