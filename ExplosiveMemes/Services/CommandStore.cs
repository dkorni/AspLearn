using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExplosiveMemes.Commands;

namespace ExplosiveMemes
{
    public class CommandStore
    {
        private Dictionary<string, Type> _commands = new Dictionary<string, Type>();

        public CommandStore()
        {
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

            if (commandType != null)
                // create instance of command
                command = (ICommand)Activator.CreateInstance(commandType);
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
