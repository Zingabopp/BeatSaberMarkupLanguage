using System;
using System.Reflection;
using System.Windows.Input;

namespace BeatSaberMarkupLanguage.Parser
{
    public class BSMLCommand
    {
        private object host;
        private PropertyInfo command;
        private BSMLValue parameter;

        public BSMLCommand(object host, PropertyInfo commandProp, BSMLValue parameters = null)
        {
            this.host = host;
            if (!typeof(ICommand).IsAssignableFrom(commandProp.PropertyType))
                throw new InvalidOperationException($"{commandProp.Name} has the [UICommand] attribute, but is not of type ICommand in {host.GetType().FullName}");
            this.command = commandProp;
            this.parameter = parameters;
        }

        public ICommand GetCommand() => (ICommand)command.GetValue(host);

        public void SetCommand(ICommand command)
        {
            this.command.SetValue(host, command);
        }

        public void SetParameters(BSMLValue parameter)
        {
            this.parameter = parameter;
        }

        public bool CanExecute => GetCommand().CanExecute(parameter?.GetValue());

        public void Execute()
        {
            GetCommand().Execute(parameter?.GetValue());
        }
    }
}
