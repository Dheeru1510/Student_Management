using BusinessModel.Abstraction;
using BusinessModel.Implementation;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

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
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}