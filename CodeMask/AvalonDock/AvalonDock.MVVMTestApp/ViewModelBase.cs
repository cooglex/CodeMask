/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.ComponentModel;

namespace AvalonDock.MVVMTestApp
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}