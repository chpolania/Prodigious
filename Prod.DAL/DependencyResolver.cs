namespace Prod.DAL
{
    using System.ComponentModel.Composition;
    using System.Data.Entity;
    using Prod.DAL.UnitOfWork;
    using Prod.Resolver;

    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
