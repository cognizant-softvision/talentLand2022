using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Features.Question.Common;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.QA.Commands
{
    public class CreateUserQACommandHandler : IRequestHandler<CreateUserQACommand, ApiResponse<CreateUserQAViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IQARepository _qaRepository;
        private readonly IQuestionRepository _questionRepository;

        private const int NUMBER_OF_QUESTIONS = 5;

        public CreateUserQACommandHandler(IMapper mapper,
            IQARepository qaRepository, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _qaRepository = qaRepository;
            _questionRepository = questionRepository;
        }

        public async Task<ApiResponse<CreateUserQAViewModel>> Handle(CreateUserQACommand createUserQACommand, CancellationToken cancellationToken)
        {
            //Mapping
            var qa = _mapper.Map<Domain.QA>(createUserQACommand);

            //create user
            await _qaRepository.AddAsync(qa, cancellationToken);

            var allQuestions = await _questionRepository.GetAllPagedQuestionsAsync(0, NUMBER_OF_QUESTIONS,
                cancellationToken);
            var questionAnswered = allQuestions.FirstOrDefault(q => q.Id == qa.QuestionId);

            //create response object
            var response = new CreateUserQAViewModel();
            response.IsLastQuestion = questionAnswered.Order == NUMBER_OF_QUESTIONS;

            if (!response.IsLastQuestion)
            {
                response.NextQuestion = _mapper.Map<QuestionViewModel>(
                    allQuestions
                    .Where(aq => aq.Order > questionAnswered.Order)
                    .OrderBy(aq => aq.Order).FirstOrDefault());
            }

            return new ApiResponse<CreateUserQAViewModel>() { Data = response };
        }
    }
}
