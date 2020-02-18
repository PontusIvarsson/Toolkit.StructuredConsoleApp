

using System;
using System.Text;
using MediatR;


namespace StucturedConsoleApp.Core
{

    interface ICommandModelBinder
    {
        IRequest Bind(string[] args);
    } 

    public class CommandModelBinder : ICommandModelBinder
    {
        public CommandModelBinder(IStringCommandParser parser, ICommandReflector commandReflector)
        {
            _parser = parser;
            _commandReflector = commandReflector;
        }
        IStringCommandParser _parser;
        ICommandReflector _commandReflector;

        public IRequest Bind(string[] args)
        {
            GenericCommand genericCommand = _parser.ParseArgs(args);
            IRequest newRequest = _commandReflector.ReflectCommand(genericCommand);

            ValidateNewRequest(newRequest);

            return newRequest;
        }

        private void ValidateNewRequest(IRequest newRequest)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var propertyInfo in newRequest.GetType().GetProperties())
            {

                var prop = propertyInfo.PropertyType;
                if (IsNullable(prop))
                {
                    if(propertyInfo.GetValue(newRequest) == null)
                    {
                        sb.AppendFormat("Argument:{0} is mandatory.", propertyInfo.Name);
                        sb.AppendLine();
                    }
                }
            }

            bool IsNullable(Type type) => Nullable.GetUnderlyingType(type) != null;

            if (sb.Length > 0)
            {
                throw new StructuredArgumentErrorException(sb.ToString());
            }
        }
    }
}
