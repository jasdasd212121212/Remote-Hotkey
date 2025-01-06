using System.Windows;

namespace Remote_Hub.ViewModels._Base
{
    public abstract class ViewModelBase : DependencyObject
    {
        private bool _initialized;

        public void Init()
        {
            if (_initialized)
            {
                return;
            }

            OnInit();

            _initialized = true;
        }

        protected abstract void OnInit();
    }
}