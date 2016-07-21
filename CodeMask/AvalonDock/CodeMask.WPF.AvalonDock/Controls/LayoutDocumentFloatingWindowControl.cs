﻿/************************************************************************

   AvalonDock

   




 


   

  **********************************************************************/

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using CodeMask.WPF.AvalonDock.Layout;
using Microsoft.Windows.Shell;

namespace CodeMask.WPF.AvalonDock.Controls
{
    public class LayoutDocumentFloatingWindowControl : LayoutFloatingWindowControl
    {
        private readonly LayoutDocumentFloatingWindow _model;

        static LayoutDocumentFloatingWindowControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LayoutDocumentFloatingWindowControl),
                new FrameworkPropertyMetadata(typeof (LayoutDocumentFloatingWindowControl)));
        }

        internal LayoutDocumentFloatingWindowControl(LayoutDocumentFloatingWindow model)
            : base(model)
        {
            _model = model;
        }

        public override ILayoutElement Model
        {
            get { return _model; }
        }

        public LayoutItem RootDocumentLayoutItem
        {
            get { return _model.Root.Manager.GetLayoutItemFromModel(_model.RootDocument); }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (_model.RootDocument == null)
            {
                InternalClose();
            }
            else
            {
                var manager = _model.Root.Manager;

                Content = manager.CreateUIElementForModel(_model.RootDocument);

                _model.RootDocumentChanged += _model_RootDocumentChanged;
            }
        }

        private void _model_RootDocumentChanged(object sender, EventArgs e)
        {
            if (_model.RootDocument == null)
            {
                InternalClose();
            }
        }

        protected override IntPtr FilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32Helper.WM_NCLBUTTONDOWN: //Left button down on title -> start dragging over docking manager
                    if (wParam.ToInt32() == Win32Helper.HT_CAPTION)
                    {
                        if (_model.RootDocument != null)
                            _model.RootDocument.IsActive = true;
                    }
                    break;
                case Win32Helper.WM_NCRBUTTONUP:
                    if (wParam.ToInt32() == Win32Helper.HT_CAPTION)
                    {
                        if (OpenContextMenu())
                            handled = true;
                        if (_model.Root.Manager.ShowSystemMenu)
                            WindowChrome.GetWindowChrome(this).ShowSystemMenu = !handled;
                        else
                            WindowChrome.GetWindowChrome(this).ShowSystemMenu = false;
                    }
                    break;
            }

            return base.FilterMessage(hwnd, msg, wParam, lParam, ref handled);
        }

        private bool OpenContextMenu()
        {
            var ctxMenu = _model.Root.Manager.DocumentContextMenu;
            if (ctxMenu != null && RootDocumentLayoutItem != null)
            {
                ctxMenu.PlacementTarget = null;
                ctxMenu.Placement = PlacementMode.MousePoint;
                ctxMenu.DataContext = RootDocumentLayoutItem;
                ctxMenu.IsOpen = true;
                return true;
            }

            return false;
        }


        protected override void OnClosed(EventArgs e)
        {
            var root = Model.Root;
            root.Manager.RemoveFloatingWindow(this);
            root.CollectGarbage();

            base.OnClosed(e);

            if (!CloseInitiatedByUser)
            {
                root.FloatingWindows.Remove(_model);
            }

            _model.RootDocumentChanged -= _model_RootDocumentChanged;
        }
    }
}