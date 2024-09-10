
using Autofac;
using LENA.WebAppCase.Core.Repository;
using LENA.WebAppCase.Core.Service;
using LENA.WebAppCase.Core.UnitOfWork;
using LENA.WebAppCase.Repository.Repository;
using LENA.WebAppCase.Repository.UnitOfWork;
using LENA.WebAppCase.Service.Service;
using Microsoft.AspNetCore.Components.Forms;

namespace LENA.WebAppCase.Modules
{
    public class DIContainerModule : Module
    {
        private readonly IConfiguration _configuration;

        public DIContainerModule()
        {

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericDBRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            base.Load(builder);
        }
    }
}
