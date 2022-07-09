using BusinessLogic.Services;
using BusinessLogic.Services.Contracts;
using DataAccsess.Services;
using DataAccsess.Services.Contracts;
using EfRepsitory;

namespace DAISUni2
{
    internal static class BaseStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITeacherBL, TeacherBL>();
            services.AddScoped<ITeachersDA, TeachersDA>();
            services.AddScoped<IStudentBL, StudentBL>();
            services.AddScoped<IStudentsDA, StudentsDA>();
            services.AddScoped<IStudentsWithADO, StudentsWithADO>();
           
            services.AddScoped<ICoursesDA , CoursesDA>();
            services.AddScoped<ICourseBL, CourseBL>();
            services.AddScoped<IModuleBL, ModuleBL>();
            services.AddScoped<IModuleDA, ModuleDA>();

            services.AddScoped<IMaterialBL, MaterialBL>();
            services.AddScoped<IMaterialsDA, MaterialsDA>();
            services.AddScoped<IMaterialsWithADO, MaterialsWithADO>();


            services.AddScoped<IEntityRepository, EntityRepository>();
        }
    }
}
