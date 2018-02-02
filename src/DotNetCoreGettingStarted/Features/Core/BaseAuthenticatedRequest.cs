namespace DotNetCoreGettingStarted.Features.Core
{
    public class BaseAuthenticatedRequest: BaseRequest
    {
        public string Username { get; set; }
    }
}
