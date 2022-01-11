using BusinessModel.Abstraction;
using BusinessModel.Implementation;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Student_Management
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUsersBO, UsersBo>();
            container.RegisterType<IStudentDetailBO, StudentDetailBO>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}