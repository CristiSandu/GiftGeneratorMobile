using Plugin.Firebase.Auth;

namespace GiftGenerator.Services
{
    public interface IAuthService
    {
        Task<IFirebaseUser> SignInWithGoogle();
    }
}