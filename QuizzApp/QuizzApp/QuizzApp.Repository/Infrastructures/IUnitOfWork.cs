using QuizzApp.Repository.IRepository;

namespace QuizzApp.Repository.Infrastructures
{
    public interface IUnitOfWork 
    {
        ICourseRepository CourseRepository{ get; }
        IPostRepository PostRepository{ get; }
        ICommentRepository CommentRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ICategoryCourseRepository CategoryCourseRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IOptionRepository OptionRepository { get; }
        ITestAnswers TestAnswers { get; }
        //IResultRepository ResultRepository { get; }


        int SaveChanges();
    }
}