using Application.Contracts.User.SignIn;
using Domain.Entities.User;
using Infrastructure.Repository;

namespace Application.Services.UserService;

public class UserService(
    IRepository<EUserSignIn> userSignInRepo)
    : IUserService
{
    public async Task<bool> LoginAsync(UserSignInRequest signInRequest)
    {
        await userSignInRepo.InsertAsync(new EUserSignIn
        {
            UserId = signInRequest.UserId
        });
        
        return true;
    }
}