/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;

namespace CodeMask.WPF.AvalonDock.Controls
{
    internal class ReentrantFlag
    {
        private bool _flag;

        public bool CanEnter
        {
            get { return !_flag; }
        }

        public _ReentrantFlagHandler Enter()
        {
            if (_flag)
                throw new InvalidOperationException();
            return new _ReentrantFlagHandler(this);
        }

        public class _ReentrantFlagHandler : IDisposable
        {
            private readonly ReentrantFlag _owner;

            public _ReentrantFlagHandler(ReentrantFlag owner)
            {
                _owner = owner;
                _owner._flag = true;
            }

            public void Dispose()
            {
                _owner._flag = false;
            }
        }
    }
}