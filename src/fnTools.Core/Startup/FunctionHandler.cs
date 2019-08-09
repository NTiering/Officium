using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fnTools.Core.HandlerCollection;
using fnTools.Core.Handlers;
using Officium.Core.Handlers;
using Officium.Core.ReqRes;

namespace fnTools.Core.Startup
{
    public class FunctionHandler 
    {
        private readonly IBeforeFunctionHandlerCollection beforeFunctions;
        private readonly IAfterFunctionHandlerCollection afterFunctions;
        private readonly IOnErrorHandlerCollection onErrorFunctions;
        private readonly IRequestFunctionHandlerCollection requestFunctions;
        private readonly IValidationFunctionHandler validationFunctions;

        public FunctionHandler(
            IBeforeFunctionHandlerCollection beforeFunctions,
            IAfterFunctionHandlerCollection afterFunctions,
            IOnErrorHandlerCollection onErrorFunctions,
            IRequestFunctionHandlerCollection requestFunctions,
            IValidationFunctionHandler validationFunctions
            )
        {
            this.beforeFunctions = beforeFunctions;
            this.afterFunctions = afterFunctions;
            this.onErrorFunctions = onErrorFunctions;
            this.requestFunctions = requestFunctions;
            this.validationFunctions = validationFunctions;
        }

        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            try
            {
                beforeFunctions.Handle(request, response);
                validationFunctions.Handle(request, response);
                requestFunctions.Handle(request, response);
                afterFunctions.Handle(request, response);
            }
            catch (Exception ex)
            {
                onErrorFunctions.Handle(request, response, ex);
            }
        }
        
       // => beforeFunctions.Add(req);
       // public void Add(IAfterEveryRequestHandler req)
      //      => afterFunctions.Add(req);
     //   public void Add(IOnErrorHandler req)
     //      => onErrorFunctions.Add(req);
      //  public void Add(Method method, string pathSelector, IRequestHandlerFunction handler)
     //       => requestFunctions.Add(method, pathSelector, handler);
        //public void Add(Method method, string pathSelector, IValidationHandler handler)
        //   => validationFunctions.Add(method, pathSelector, handler);
    }
}
