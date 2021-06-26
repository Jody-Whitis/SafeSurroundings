using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Ajax.Utilities;
using SafeSurroundings.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SafeSurroundings.App_Start
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<InMemoryPersonTable>().SingleInstance();
            builder.RegisterType<InMemoryProfileTable>().SingleInstance();
            builder.RegisterType<InMemoryPlaceTable>().SingleInstance();
            builder.RegisterType<InMemoryMeetUpTable>().SingleInstance();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}