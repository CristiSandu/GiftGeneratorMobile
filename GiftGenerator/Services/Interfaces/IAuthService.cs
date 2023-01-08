namespace GiftGenerator.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginBtnTappedAsync(string email, string password);
        Task<bool> RegisterUserTappedAsync(string email, string password, string displayName);
    }
}