using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace StucturedConsoleApp.Core
{
    public interface ICommandReflector
    {
        IRequest ReflectCommand(GenericCommand req);
    }

    public class CommandReflector : ICommandReflector
    {
        public IRequest ReflectCommand(GenericCommand genericCommand)
        {
            var assembly = Assembly.GetEntryAssembly();

            var commandTypes = ReflectPossibleCommandTypes();
            foreach (var item in commandTypes)
            {
                Debug.WriteLine(item.Name);
                Debug.WriteLine(item.FullName);
            }
            var commandType = commandTypes.FirstOrDefault(x => x.Name.EndsWith(genericCommand.CommandName, StringComparison.InvariantCultureIgnoreCase));

            if (commandType == null)
            {
                throw new StructuredArgumentErrorException(genericCommand);
            }

            //var commandInstance = Reflect(genericCommand, commandType);
            var commandInstance = DeserializeCommand(genericCommand, commandType);
            //var commandInstance = ReflectFromCommand(genericCommand, commandType);

            return commandInstance;
        }

        private IEnumerable<Type> ReflectPossibleCommandTypes()
        {
            var assembly = Assembly.GetEntryAssembly();

            var type = typeof(IRequest); //types to look for

            var types = assembly.GetTypes()
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            return types;
        }
        private IRequest DeserializeCommand(GenericCommand genericCommand, Type commandType)
        {

            var noneOptArguments = genericCommand.NoneOptArguments.ToDictionary(x => x, x => string.Empty);
            List<Dictionary<string, string>> allArguments = new List<Dictionary<string, string>> { genericCommand.Arguments, noneOptArguments };
            var mergedArguments = allArguments.SelectMany(x => x)
                .ToLookup(pair => pair.Key, pair => pair.Value)
                .ToDictionary(group => group.Key, group => group.First());

            var json = JsonConvert.SerializeObject(mergedArguments);

            return (IRequest)JsonConvert.DeserializeObject(json, commandType);

        }
        private IRequest Reflect(GenericCommand genericCommand, Type commandType)
        {
            IRequest instance = (IRequest)Activator.CreateInstance(commandType);

            if (instance != null)
            {
                var properties = instance.GetType().GetProperties();
                foreach (var keyValue in genericCommand.Arguments)
                {
                    var prop = properties.FirstOrDefault(x => x.Name.ToLower().Equals(keyValue.Key));
                    if (prop != null)
                    {
                        var t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                        var value = (keyValue.Value == null) ? null : Convert.ChangeType(keyValue.Value, t);
                        prop.SetValue(instance, value);
                    }
                }
            }
            return instance;
        }
        private IRequest ReflectFromCommand(GenericCommand genericCommand, Type commandType)
        {
            IRequest instance = (IRequest)Activator.CreateInstance(commandType);


  

            return instance;
        }



    }
}

