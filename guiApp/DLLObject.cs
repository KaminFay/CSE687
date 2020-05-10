using System;
using System.ComponentModel;

namespace guiApp
{
    internal class DLLObject : INotifyPropertyChanged
    {
        private string name;
        private string v;

        public DLLObject(string v)
        {
            this.v = v;
        }

        private string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }
    }
}