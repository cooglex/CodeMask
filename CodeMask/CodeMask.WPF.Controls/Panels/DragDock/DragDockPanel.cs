using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CodeMask.WPF.Controls.Panels.DragDock
{
    /// <summary>
    /// A draggable, dockable, expandable panel class.
    /// </summary>
    public class DragDockPanel : DraggablePanel
    {
        /// <summary>
        /// The template part name for the maxmize toggle button.
        /// </summary>
        private const string ElementMaximizeToggleButton = "MaximizeToggleButton";

        #region Private members
        /// <summary>
        /// Stores the panel index.
        /// </summary>
        private int panelIndex = 0;
        #endregion

        /// <summary>
        /// Drag dock panel constructor.
        /// </summary>
        public DragDockPanel()
        {
            this.DefaultStyleKey = typeof(DragDockPanel);
        }

        #region Events
        /// <summary>
        /// The maxmised event.
        /// </summary>
        public event EventHandler Maximized;

        /// <summary>
        /// The restored event.
        /// </summary>
        public event EventHandler Restored;

        /// <summary>
        /// The minimized event.
        /// </summary>
        public event EventHandler Minimized;
        #endregion

        #region Public members
        /// <summary>
        /// Gets or sets the calculated panel index.
        /// </summary>
        public int PanelIndex
        {
            get { return this.panelIndex; }
            set { this.panelIndex = value; }
        }

        #region IsMaximized
        /// <summary>
        /// Gets a value indicating whether the panel is maximised.
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            private set { SetValue(IsMaximizedProperty, value); }
        }

        /// <summary>
        /// The IsMaximised Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register("IsMaximized", typeof(bool), typeof(DragDockPanel), new PropertyMetadata(false));
        #endregion

        #region PanelState
        public PanelState PanelState
        {
            get { return (PanelState)GetValue(PanelStateProperty); }
            set { SetValue(PanelStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PanelState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PanelStateProperty =
            DependencyProperty.Register("PanelState", typeof(PanelState), typeof(DragDockPanel), new PropertyMetadata(PanelState.Restored, OnPanelStatePropertyChanged));

        private static void OnPanelStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DragDockPanel panel = d as DragDockPanel;
            if (panel != null)
            {
                switch ((PanelState)Enum.Parse(typeof(PanelState), e.NewValue.ToString()))
                {
                    case PanelState.Restored:
                        panel.Restore();
                        break;
                    case PanelState.Maximized:
                        panel.Maximize();
                        break;
                    case PanelState.Minimized:
                        panel.Minimize();
                        break;
                }
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Gets called once the template is applied so we can fish out the bits
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToggleButton maximizeToggle =
                this.GetTemplateChild(DragDockPanel.ElementMaximizeToggleButton) as ToggleButton;

            if (maximizeToggle != null)
            {
                maximizeToggle.Click += new RoutedEventHandler(this.MaximizeToggle_Click);
            }
        }

        /// <summary>
        /// Override for updating the panel position.
        /// </summary>
        /// <param name="pos">The new position.</param>
        public override void UpdatePosition(Point pos)
        {
            Canvas.SetLeft(this, pos.X);
            Canvas.SetTop(this, pos.Y);
        }

        /// <summary>
        /// Override for when a panel is maximized.
        /// </summary>
        public virtual void Maximize()
        {
            // Bring the panel to the front
            Canvas.SetZIndex(this, CurrentZIndex++);

            bool raiseEvent = this.PanelState != PanelState.Maximized;
            this.PanelState = PanelState.Maximized;

            this.IsMaximized = true;

            // Fire the panel maximized event
            if (raiseEvent && this.Maximized != null)
            {
                this.Maximized(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Override for when the panel minimizes.
        /// </summary>
        public virtual void Minimize()
        {
            bool raiseEvent = this.PanelState != PanelState.Minimized;
            this.PanelState = PanelState.Minimized;

            this.IsMaximized = false;

            // Fire the panel minimized event
            if (raiseEvent && this.Minimized != null)
            {
                this.Minimized(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Override for when the panel restores.
        /// </summary>
        public virtual void Restore()
        {
            bool raiseEvent = this.PanelState != PanelState.Restored;
            this.PanelState = PanelState.Restored;

            this.IsMaximized = false;

            // Fire the panel minimized event
            if (raiseEvent && this.Restored != null)
            {
                this.Restored(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Toggles the maximixed state of the panel.
        /// </summary>
        /// <param name="sender">The maximize toggle.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MaximizeToggle_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsMaximized)
            {
                this.Maximize();
            }
            else
            {
                this.Restore();
            }
        }
    }
}
