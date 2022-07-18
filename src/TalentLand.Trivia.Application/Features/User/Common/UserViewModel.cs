namespace TalentLand.Trivia.Application.Features.User.Common
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            QA = new QAViewModel();
        }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string University { get; set; } = null!;

        public string Company { get; set; } = null!;

        public QAViewModel QA { get; set; }
    }
}
