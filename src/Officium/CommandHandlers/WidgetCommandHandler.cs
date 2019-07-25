using Officium.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp2.CommandHandlers
{
    public interface ICommandHandler<T>
       where T : ICommand
    {
        void Handle(ICommand command);
    }

    public interface IHttpPostCommandHandler<T> : ICommandHandler<T>
        where T : ICommand
    {

    }

    public interface IHttpPutCommandHandler<T> : ICommandHandler<T>
        where T : ICommand
    {

    }

    public interface IHttpDeleteCommandHandler<T> : ICommandHandler<T>
        where T : ICommand
    {

    }

    public interface IHttpGetCommandHandler<T> : ICommandHandler<T>
        where T : ICommand
    {

    }     

      


    public class WidgetCommand : ICommand
    {
        public ICommandRequest CommandRequest { get; set; }
        public ICommandResponse CommandResponse { get; set; }
    }
    
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> BuildForCommand<T>(Func<object,Type> typeResolver) where T : ICommand;
    }

    public class WidgetCommandHandler : IHttpPostCommandHandler<WidgetCommand>
    {
        public void Handle(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}
