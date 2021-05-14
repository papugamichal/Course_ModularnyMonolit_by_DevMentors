using System.Threading.Tasks;

namespace Confab.Shared.Abstraction.Modules
{
    public interface IModuleClient
    {
        Task<TResult> SendAsync<TResult>(string path, object request) where TResult : class;
        Task PublishAsync(object message);
    }
}
