﻿using System;
using System.Reflection;
using Inceptum.AppServer.Management2.Handlers;
using Inceptum.AppServer.Management2.Resources;
using Inceptum.AppServer.Model;
using OpenRasta.Codecs;
using OpenRasta.Codecs.Razor;
using OpenRasta.Configuration;
using OpenRasta.IO;

namespace Inceptum.AppServer.Management2.OpenRasta
{
    public class Configurator : IConfigurationSource
    {
        #region IConfigurationSource Members

        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
/*                ResourceSpace.Uses.PipelineContributor<CrossDomainPipelineContributor>();
                ResourceSpace.Uses.PipelineContributor<StaticContentPipelineContributor>();*/
                ResourceSpace.Uses.ViewsEmbeddedInTheAssembly(Assembly.GetExecutingAssembly(), "Inceptum.AppServer.Management2.Views");
                ResourceSpace.Has.ResourcesOfType<string>().WithoutUri.TranscodedBy<UtfTextPlainCodec>();

                ResourceSpace.Has
                    .ResourcesOfType<Application[]>()
                    .AtUri("api/applications")
                    .HandledBy<ApplicationsHandler>()
                    .TranscodedBy<NewtonsoftJsonCodec>();       
                
                ResourceSpace.Has
                    .ResourcesOfType<Application>()
                    .AtUri("api/applications/{application}")
                    .HandledBy<ApplicationsHandler>()
                    .TranscodedBy<NewtonsoftJsonCodec>();

                ResourceSpace.Has
                    .ResourcesOfType<ApplicationInstanceInfo[]>()
                    .AtUri("api/applications/{application}/instances")
                    .And.AtUri("api/instances")
                    .HandledBy<InstancesHandler>()
                    .TranscodedBy<NewtonsoftJsonCodec>();

                ResourceSpace.
                    Has.ResourcesOfType<ApplicationInstanceInfo>()
                    .AtUri("api/instance")//TODO: looks like it should be removed
                    .And.AtUri("api/instance/{instance}")
                    .And.AtUri("api/instance/{instance}/start").Named("start")
                    .And.AtUri("api/instance/{instance}/stop").Named("stop")
                    .HandledBy<InstancesHandler>()
                    .TranscodedBy<NewtonsoftJsonCodec>();

                ResourceSpace.Has
                    .ResourcesOfType<HostInfo>()
                    .AtUri("api/host")
                    .HandledBy<HostHandler>()
                    .TranscodedBy<NewtonsoftJsonCodec>();
            }
        }

        #endregion
    }
}