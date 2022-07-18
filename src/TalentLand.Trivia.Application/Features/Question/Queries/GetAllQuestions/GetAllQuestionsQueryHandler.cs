using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TalentLand.Trivia.Application.Interfaces.Repositories;
using TalentLand.Trivia.Application.Wrappers;

namespace TalentLand.Trivia.Application.Features.Question.Queries.GetAllQuestions
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, ApiResponse<GetAllQuestionsViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;

        public GetAllQuestionsQueryHandler(IMapper mapper, IQuestionRepository questionRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public async Task<ApiResponse<GetAllQuestionsViewModel>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionRepository.GetAllPagedQuestionsAsync(request.Offset!.Value, request.Limit!.Value, cancellationToken);

            //no users found
            if (null == questions || questions.Count == 0)
            {
                return new ApiResponse<GetAllQuestionsViewModel>(new GetAllQuestionsViewModel());
            }

            var response = _mapper.Map<GetAllQuestionsViewModel>(questions);
            return new ApiResponse<GetAllQuestionsViewModel>(response);
        }
    }
}
