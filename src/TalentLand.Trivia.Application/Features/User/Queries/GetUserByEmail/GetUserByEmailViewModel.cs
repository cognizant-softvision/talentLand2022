using TalentLand.Trivia.Application.Features.User.Common;

namespace TalentLand.Trivia.Application.Features.User.GetUserByEmail
{
    public class GetUserByEmailViewModel
    {
        public GetUserByEmailViewModel()
        {
            User = new UserViewModel();
        }

        public UserViewModel User { get; set; }       
    }
}
