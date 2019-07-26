using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp2.CommandHandlers
{
    public interface ICommandHandler       
    {
        bool CanHandle(ICommand command);
        void Handle(ICommand command);
    }

    public abstract class HttpPostCommandHandler : ICommandHandler

    {
        public bool CanHandle(ICommand command)
        {
            return command.CommandRequestType == CommandRequestType.HttpPost;
        }

        public abstract void Handle(ICommand command);
    }

    public interface IHttpPutCommandHandler<T> : ICommandHandler
        where T : ICommand
    {

    }

    public interface IHttpDeleteCommandHandler<T> : ICommandHandler
        where T : ICommand
    {

    }

    public interface IHttpGetCommandHandler<T> : ICommandHandler
        where T : ICommand
    {

    }     

      


    public class WidgetCommand : ICommand
    {
        public ICommandResponse CommandResponse { get; set; }
        public CommandRequestType CommandRequestType { get ; set ; }
    }
    
    public interface ICommandHandlerFactory
    {
        ICommandHandler BuildForCommand<T>(Func<object,Type> typeResolver);
    }

    public class WidgetCommandHandler : HttpPostCommandHandler
    {        

        public override void Handle(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
