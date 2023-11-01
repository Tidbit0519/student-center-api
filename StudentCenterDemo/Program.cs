using StudentCenterDemo.Helper;
using StudentCenterDemo.Models.Persistance;
using StudentCenterDemo.Models.Persistence;

namespace StudentCenterDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NHibernateHelper nhHelper = new NHibernateHelper();

            IStudentRepository studentRepository = new StudentRepository(nhHelper.OpenSession());
            IProfessorRepository professorRepository = new ProfessorRepository(nhHelper.OpenSession());
            ICourseRepository courseRepository = new CourseRepository(nhHelper.OpenSession());
            IGradeRepository gradeRepository = new GradeRepository(nhHelper.OpenSession());

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<NHibernateHelper>();

            builder.Services.AddScoped<NHibernate.ISession>(provider => {
                var nhHelper = provider.GetRequiredService<NHibernateHelper>();
                return nhHelper.OpenSession();
            });

            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IGradeRepository, GradeRepository>();
            builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}