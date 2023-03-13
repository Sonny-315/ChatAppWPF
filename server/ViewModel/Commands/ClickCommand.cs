using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace server.ViewModel.Commands
{
    public class ClickCommand : ICommand /* In ClickCommand class, create interface ICommand, 
                                     to automatically create the method, ctrl+. Enter*/
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute; //생성자를 통해서 델리게이트_execute가 DisplayMessage와 연결 될 수 있도록 세팅합니다

        public ClickCommand (Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
