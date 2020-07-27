using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExplosiveMemes.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExplosiveMemes
{
    public class CommandStore
    {
        private Dictionary<string, Type> _commands = new Dictionary<string, Type>();
        private IServiceProvider _serviceProvider;

        public CommandStore(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            LoadCommandTypes();
        }

        /// <summary>
        /// Try to get command instance, if command is supported
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public bool TryToGet(string commandName, out ICommand command)
        {
            var result = _commands.TryGetValue(commandName, out Type commandType);

            var services = new List<object>();

            if (commandType != null)
            {
                var ctrParams = commandType.GetConstructors().FirstOrDefault()?.GetParameters();
                if (ctrParams != null && ctrParams?.Length != 0)
                {
                    // so it is services we need to get them
                    foreach (var param in ctrParams)
                    {
                        var paramType = param.ParameterType;
                        var service = _serviceProvider.GetService(paramType);
                        services.Add(service);
                    }

                }

                // create instance of command
                command = (ICommand) Activator.CreateInstance(commandType, services.ToArray());
            }
            else
            {
                // nothing
                command = null;
            }

            return result;
        }

        /// <summary>
        /// Loads the command types.
        /// </summary>
        private void LoadCommandTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();

            // load all command types
            var commandTypes = assembly.GetTypes().Where(t => t.GetInterface("ICommand") != null);

            // make dictionary
            _commands = commandTypes.ToDictionary(t => t.GetProperty("Name").GetValue(null).ToString(), t => t);
        }
    }
}
