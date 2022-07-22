using MessageService.UI.Dtos;
using System.Threading.Tasks;

namespace MessageService.UI.Repos.IRepos
{
    public interface IMessageRepo
    {
        Task<SingleMessageResponse> SendMessage(string url, OutMessageDto outMessageDto);
        Task<ListMessageResponse> GetMessages(string url);
        Task<SingleMessageResponse> GetMessage(string url);
    }
}
