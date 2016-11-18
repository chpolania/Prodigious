namespace Prod.Services
{
    using System.ComponentModel.Composition;
    using Prod.DAL;
    using Prod.DAL.UnitOfWork;
    using Prod.Resolver;

    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductServices, ProductServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterType<ITokenServices, TokenServices>();

        }
    }
}
