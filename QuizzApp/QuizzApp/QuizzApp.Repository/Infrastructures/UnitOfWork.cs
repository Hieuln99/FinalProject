using QuizzApp.Data.DbContext;
using QuizzApp.Repository.IRepository;
using QuizzApp.Repository.Repository;

namespace QuizzApp.Repository.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ICategoryRepository _categoryRepository;

        private ICategoryCourseRepository _categoryCourseRepository;
        private ICommentRepository _commentRepository;
        private IPostRepository _postRepository;
        private IAnswerRepository _answerRepository;
        private IQuestionRepository _questionRepository;
        //private IResultRepository _resulRepository;
        private IOptionRepository _optionRepository;
        private ICourseRepository _courseRepository;
        private ITestAnswers _testRepository;

 

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAnswerRepository AnswerRepository => _answerRepository ??= new AnswerRepository(_context);

        public IQuestionRepository QuestionRepository => _questionRepository ??= new QuestionRepository(_context);

        public IOptionRepository OptionRepository => _optionRepository ??= new OptionRepository(_context);

        //public IResultRepository ResultRepository => _resulRepository ??= new ResultRepository(_context);

        public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);

        public ICommentRepository CommentRepository => _commentRepository ??= new CommentRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);


        public ICourseRepository CourseRepository => _courseRepository ??= new CourseRepository(_context);

        public ICategoryCourseRepository CategoryCourseRepository => _categoryCourseRepository ??= new CategoryCourseRepository(_context);

        public ITestAnswers TestAnswers => _testRepository ??= new TestAnswers(_context);

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }
}