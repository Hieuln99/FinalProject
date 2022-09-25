using AutoMapper;
using Models.Account;
using QuizzApp.Data.Entities;
using QuizzApp.VModels.Answers;
using QuizzApp.VModels.Courses;
using QuizzApp.VModels.Options;
using QuizzApp.VModels.Questions;
using QuizzApp.VModels.Results;

namespace QuizzApp.Web.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Course, CourseVModel>().ReverseMap();
            CreateMap<Course, CourseModel>().ReverseMap();
            CreateMap<Question, QuestionVModel>().ReverseMap();
            CreateMap<Question, QuestionModel>().ReverseMap();
            CreateMap<TestQuestionAnswer, AnswerVModel>().ReverseMap();
            CreateMap<TestQuestionAnswer, AnswerModel>().ReverseMap();
            CreateMap<Option, OptionVModel>().ReverseMap();
            CreateMap<Option, OptionModel>().ReverseMap();
            //CreateMap<Result, ResultVModel>().ReverseMap();
            //CreateMap<Result, ResultModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
