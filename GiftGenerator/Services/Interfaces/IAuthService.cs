using Plugin.Firebase.Auth;

namespace GiftGenerator.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IFirebaseUser> SignInWithGoogle();
    }
}