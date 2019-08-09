//using System;
//using System.Collections.Generic;
//using fnTools.Core.Handlers;
//using Moq;
//using Officium.Core.Handlers;
//using Officium.Core.ReqRes;
//using Officium.Core.Startup;
//using Xunit;

//namespace fnTool.Test.Startup
//{
//    public class FunctionHandlerTests
//    {
//        [Fact]
//        public void BeforeRequestIsCalled()
//        {
//            var handler = new Mock<IBeforeEveryRequestHandler>();
//            var request = new RequestContext();
//            var response = new ResponseContent();

//            new Builder(null,null).Add(handler.Object).GetHandler().HandleRequest(request, response);

//            handler.Verify(x => x.Handle(request, response), Times.Once);
//        }

//        [Fact]
//        public void AfterRequestIsCalled()
//        {
//            var handler = new Mock<IAfterEveryRequestHandler>();
//            var request = new RequestContext();
//            var response = new ResponseContent();

//            new Builder(null, null).Add(handler.Object).GetHandler().HandleRequest(request, response);

//            handler.Verify(x => x.Handle(request, response), Times.Once);
//        }

//        [Fact]
//        public void OnErrorIsCalled()
//        {
//            var errorHandler = new Mock<IOnErrorHandler>();
//            var exception = new Exception();
//            var handler = new Mock<IAfterEveryRequestHandler>();
//            handler.Setup(x => x.Handle(It.IsAny<RequestContext>(), It.IsAny<ResponseContent>())).Throws(exception);
//            var request = new RequestContext();
//            var response = new ResponseContent();

//            new Builder(null, null).Add(handler.Object)
//                .Add(errorHandler.Object)
//                .GetHandler()
//                .HandleRequest(request, response);

//            errorHandler.Verify(x => x.Handle(request, response,exception), Times.Once);
//        }

//        [Fact]
//        public void RequestHandlerIsCalled()
//        {
//            //var request = new RequestContext { Path = "/v1/" };
//            //var response = new ResponseContent();
//            //var handler = new Mock<IRequestHandler>();

//            //new Builder()
//            //    .Add(Method.GET, "/v1/", handler.Object)
//            //    .GetHandler()
//            //    .HandleRequest(request, response);


//            //handler.Verify(x => x.Handle(request, response), Times.Once);
//        }

//        [Fact]
//        public void ValidationHandlerIsCalled()
//        {
//            //var request = new RequestContext { Path = "/v1/" };
//            //var response = new ResponseContent();
//            //var handler = new Mock<IValidationHandler>();

//            //new Builder()
//            //    .Add(Method.GET, "/v1/", handler.Object)
//            //    .GetHandler()
//            //    .HandleRequest(request, response);


//            //handler.Verify(x => x.Handle(request, response), Times.Once);
//        }
//    }
//}
