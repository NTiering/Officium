using System;
using System.Collections.Generic;
using System.Text;

namespace Officium.Core.Startup
{
    public class Builder
    {
        public Builder BeforeEveryRequest<T>(object o)
        {
            return this;
        }

        public Builder AfterEveryRequest<T>(object o)
        {
            return this;
        }

        public Builder OnError<T>(object o)
        {
            return this;
        }

        public Builder OnNotHandled<T>(object o)
        {
            return this;
        }

        public Builder AddHandler<T>(string path, object method)
        {
            return this;
        }

        public Builder AddValidator<T>(string path, object method)
        {
            return this;
        }

        public Builder AddOpenApi()
        {
            return this;
        }

        public object ProcessRequest(object o)
        {
            return null;
        }
    }
}
