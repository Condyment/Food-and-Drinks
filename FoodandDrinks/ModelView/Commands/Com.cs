using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FoodandDrinks.ModelView.Commands
{
    public class Com : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<string> executee;
        public Com(Action<string> executee)
        {
            
            this.executee = executee;


        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executee.Invoke(parameter as string);
        }
    }
}
