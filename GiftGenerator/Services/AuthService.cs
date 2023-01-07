using GiftGenerator.Services.Interfaces;
using Plugin.Firebase.Auth;

namespace GiftGenerator.Services;

public class AuthService : IAuthService
{
    private readonly IFirebaseAuth _firebaseAuth;
    public AuthService(IFirebaseAuth firebaseAuth)
    {
        _firebaseAuth = firebaseAuth;
    }

    public Task<IFirebaseUser> SignInWithGoogle()
    {
        return _firebaseAuth.SignInWithGoogleAsync();
    }
}
