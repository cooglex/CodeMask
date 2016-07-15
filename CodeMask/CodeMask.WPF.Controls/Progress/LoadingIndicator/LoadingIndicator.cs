using System.Windows;
using System.Windows.Controls;

namespace CodeMask.WPF.Controls.Progress
{
    /// <summary>
    /// </summary>
    [TemplatePart(Name = "Border", Type = typeof (Border))]
    public class LoadingIndicator : Control
    {
        // Variables
        protected Border PART_Border;

        static LoadingIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LoadingIndicator),
                new FrameworkPropertyMetadata(typeof (LoadingIndicator)));
        }

        #region override methods

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code
        ///     or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Border = (Border) GetTemplateChild("PART_Border");

            if (PART_Border != null)
            {
                VisualStateManager.GoToElementState(PART_Border, (IsActive ? "Active" : "Inactive"), false);
                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(PART_Border))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(PART_Border, SpeedRatio);
                            }
                        }
                    }
                }

                PART_Border.Visibility = (IsActive ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        #endregion

        #region properties

        #region SpeedRatio

        /// <summary>
        ///     Get/set the speed ratio of the animation.
        /// </summary>
        public double SpeedRatio
        {
            get { return (double) GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        /// <summary>
        /// </summary>
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof (double), typeof (LoadingIndicator),
                new PropertyMetadata(1d, (o, e) =>
                {
                    var li = (LoadingIndicator) o;

                    if (li.PART_Border == null || li.IsActive == false)
                    {
                        return;
                    }

                    foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PART_Border))
                    {
                        if (group.Name == "ActiveStates")
                        {
                            foreach (VisualState state in group.States)
                            {
                                if (state.Name == "Active")
                                {
                                    state.Storyboard.SetSpeedRatio(li.PART_Border, (double) e.NewValue);
                                }
                            }
                        }
                    }
                }));

        #endregion

        #region IsActive

        /// <summary>
        ///     Get/set whether the loading indicator is active.
        /// </summary>
        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof (bool), typeof (LoadingIndicator),
                new PropertyMetadata(true, (o, e) =>
                {
                    var li = (LoadingIndicator) o;

                    if (li.PART_Border == null)
                    {
                        return;
                    }

                    if ((bool) e.NewValue == false)
                    {
                        VisualStateManager.GoToElementState(li.PART_Border, "Inactive", false);
                        li.PART_Border.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        VisualStateManager.GoToElementState(li.PART_Border, "Active", false);
                        li.PART_Border.Visibility = Visibility.Visible;

                        foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.PART_Border))
                        {
                            if (group.Name == "ActiveStates")
                            {
                                foreach (VisualState state in group.States)
                                {
                                    if (state.Name == "Active")
                                    {
                                        state.Storyboard.SetSpeedRatio(li.PART_Border, li.SpeedRatio);
                                    }
                                }
                            }
                        }
                    }
                }));

        #endregion

        #region LoadingStyles

        /// <summary>
        ///     Get/set whether the loading indicator is active.
        /// </summary>
        public LoadingStyles LoadingStyle
        {
            get { return (LoadingStyles) GetValue(LoadingStyleProperty); }
            set { SetValue(LoadingStyleProperty, value); }
        }

        /// <summary>
        /// </summary>
        public static readonly DependencyProperty LoadingStyleProperty =
            DependencyProperty.Register("LoadingStyle", typeof (LoadingStyles), typeof (LoadingIndicator),
                new PropertyMetadata(LoadingStyles.Arcs));

        #endregion

        #endregion
    }

    /// <summary>
    /// </summary>
    public enum LoadingStyles
    {
        Arcs,
        ArcsRing,
        DoubleBounce,
        FlipPlane,
        Pulse,
        Ring,
        ThreeDots,
        Wave,
        Apple,
        Cogs,
        Swirl,
        DotCircle
    }
}