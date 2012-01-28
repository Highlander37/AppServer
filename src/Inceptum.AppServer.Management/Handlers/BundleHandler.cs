﻿using System;
using System.Runtime.InteropServices;
using Inceptum.AppServer.Configuration;
using OpenRasta.Web;

namespace Inceptum.AppServer.Management.Handlers
{
    public class BundleHandler
    {
        private readonly IConfigurationProvider m_Provider;

        public BundleHandler(IConfigurationProvider provider)
        {
            m_Provider = provider;
        }

        public OperationResult Get(string configuration, string bundle, [Optional, DefaultParameterValue(null)]string overrides)
        {
            try
            {
                return new OperationResult.OK {ResponseResource = m_Provider.GetBundle(configuration, bundle, overrides == null ? new string[0] : overrides.Split(new[] {':'}))};
            }
            catch (BundleNotFoundException e)
            {
                return new OperationResult.NotFound{Description = e.Message,ResponseResource = e.Message,Title = "Error"};
            }            
            catch (Exception e)
            {
                return new OperationResult.InternalServerError{Description = e.Message,ResponseResource = e.ToString(),Title = "Error"};
            }
            
        }
    }
}