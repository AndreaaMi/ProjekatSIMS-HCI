using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekatSIMS.CompositeComon
{
    public class RelayCommand : ICommand
    {
        readonly Action<object> _execute;  //akcija
        readonly Predicate<object> _canExecute; //uslov da se akcija izvrsi

        public RelayCommand(Action<object> execute) : this(execute, null) { } //konstruktor za komandu kojoj ne treba uslov neki, odnosno nema predikat

        public RelayCommand(Action<object> execute, Predicate<object> canExecute) //konstruktor za komandu kojoj treba neki uslov 
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");         //??
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

    }
}
