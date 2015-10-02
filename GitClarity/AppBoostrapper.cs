using Autofac;
using Caliburn.Micro;
using GitClarity.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace GitClarity
{
    /// <summary>
    /// Entry point for the Caliburn.Micro framework
    /// </summary>
    public sealed class AppBootstrapper : BootstrapperBase
    {
        IContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterType<EventAggregator>()
                   .AsImplementedInterfaces()
                   .SingleInstance();

            // view models
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .InNamespaceOf<ShellViewModel>()
                   .AsImplementedInterfaces()
                   .InstancePerDependency();

            _container = builder.Build();
        }

        protected override object GetInstance(Type service, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return _container.Resolve(service);
            }

            return _container.ResolveNamed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShellViewModel>();
        }
    }
}
