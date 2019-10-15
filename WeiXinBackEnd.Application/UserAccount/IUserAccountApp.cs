using System.Threading.Tasks;
using WeiXinBackEnd.Application.UserAccount.Dto;

namespace WeiXinBackEnd.Application.UserAccount
{
    public interface IUserAccountApp
    {
        Task<UserAccountResponse> Login(UserAccountInput input);

    }
}