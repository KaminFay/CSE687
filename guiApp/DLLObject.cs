using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace guiApp
{
    public class DLLObject : INotifyPropertyChanged
    {
        
        
        private string name;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DLLObject()
        {

        }

        public DLLObject(string name)
        {
            this.name = name;
        }
        public string DLLObjectName
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

    }
}