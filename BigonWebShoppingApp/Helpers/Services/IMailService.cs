namespace BigonWebShoppingApp.Helpers.Services
{
    public interface IMailService
    {
        Task<bool> SendAsync(string to,string subject , string body,bool isHtml = true);
    }
}
