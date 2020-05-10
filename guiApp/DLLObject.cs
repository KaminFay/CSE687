using System;
using System.ComponentModel;

namespace guiApp
{
    internal class DLLObject : INotifyPropertyChanged
    {
        private string Title { get; }
        private string Subtitle { get; }
        private string Description { get; }

        private String PathLocation { get; }
        

        public DLLObject(string title)
        {
            this.Title = title;
        }
        public DLLObject(string title, string subtitle)
        {
            this.Title = title;
            this.Subtitle = subtitle;
        }
        public DLLObject(string title, string subtitle, string description)
        {
            this.Title = title;
            this.Subtitle = subtitle;
            this.Description = description;
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
    }
}