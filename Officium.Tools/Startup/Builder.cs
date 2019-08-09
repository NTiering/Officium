using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Tools.Startup
{
    public class Builder
    {
        private readonly IServiceCollection services;

        public Builder(IServiceCollection services)
        {
            this.services = services;
        }
    }
}
