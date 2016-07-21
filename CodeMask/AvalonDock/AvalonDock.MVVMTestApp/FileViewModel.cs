/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System.IO;
using System.Windows.Input;
using System.Windows.Media;

namespace AvalonDock.MVVMTestApp
{
    internal class FileViewModel : PaneViewModel
    {
        private static readonly ImageSourceConverter ISC = new ImageSourceConverter();

        public FileViewModel(string filePath)
        {
            FilePath = filePath;
            Title = FileName;

            //Set the icon only for open documents (just a test)
            IconSource = ISC.ConvertFromInvariantString(@"pack://application:,,/Images/document.png") as ImageSource;
        }

        public FileViewModel()
        {
            IsDirty = true;
            Title = FileName;
        }


        public string FileName
        {
            get
            {
                if (FilePath == null)
                    return "Noname" + (IsDirty ? "*" : "");

                return Path.GetFileName(FilePath) + (IsDirty ? "*" : "");
            }
        }

        #region FilePath

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    RaisePropertyChanged("FilePath");
                    RaisePropertyChanged("FileName");
                    RaisePropertyChanged("Title");

                    if (File.Exists(_filePath))
                    {
                        _textContent = File.ReadAllText(_filePath);
                        ContentId = _filePath;
                    }
                }
            }
        }

        #endregion

        #region TextContent

        private string _textContent = string.Empty;

        public string TextContent
        {
            get { return _textContent; }
            set
            {
                if (_textContent != value)
                {
                    _textContent = value;
                    RaisePropertyChanged("TextContent");
                    IsDirty = true;
                }
            }
        }

        #endregion

        #region IsDirty

        private bool _isDirty;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    RaisePropertyChanged("IsDirty");
                    RaisePropertyChanged("FileName");
                }
            }
        }

        #endregion

        #region SaveCommand

        private RelayCommand _saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(p => OnSave(p), p => CanSave(p));
                }

                return _saveCommand;
            }
        }

        private bool CanSave(object parameter)
        {
            return IsDirty;
        }

        private void OnSave(object parameter)
        {
            Workspace.This.Save(this, false);
        }

        #endregion

        #region SaveAsCommand

        private RelayCommand _saveAsCommand;

        public ICommand SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new RelayCommand(p => OnSaveAs(p), p => CanSaveAs(p));
                }

                return _saveAsCommand;
            }
        }

        private bool CanSaveAs(object parameter)
        {
            return IsDirty;
        }

        private void OnSaveAs(object parameter)
        {
            Workspace.This.Save(this, true);
        }

        #endregion

        #region CloseCommand

        private RelayCommand _closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(p => OnClose(), p => CanClose());
                }

                return _closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void OnClose()
        {
            Workspace.This.Close(this);
        }

        #endregion
    }
}