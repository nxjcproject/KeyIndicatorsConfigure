﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace KeyIndicatorsConfigure.Infrastructure.Configuration
{
    public static class ConnectionStringFactory
    {
        public static string NXJCConnectionString { get { return ConfigurationManager.ConnectionStrings["ConnNXJC"].ToString(); } }
    }
}