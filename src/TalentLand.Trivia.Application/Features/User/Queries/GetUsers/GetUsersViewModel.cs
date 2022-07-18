using System.Collections.Generic;
using TalentLand.Trivia.Application.Features.User.Common;

namespace TalentLand.Trivia.Application.Features.User.GetUsers
{
    public class GetUsersViewModel
    {
        public GetUsersViewModel()
        {
            Users = new List<UserViewModel>();
        }

        public List<UserViewModel> Users { get; set; } 
    }
}
