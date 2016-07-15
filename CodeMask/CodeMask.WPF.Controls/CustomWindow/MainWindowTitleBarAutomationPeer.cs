using System.Windows;
using System.Windows.Automation.Peers;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class MainWindowTitleBarAutomationPeer
    /// </summary>
    public sealed class MainWindowTitleBarAutomationPeer : FrameworkElementAutomationPeer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowTitleBarAutomationPeer"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public MainWindowTitleBarAutomationPeer(MainWindowTitleBar owner)
            : base(owner)
        {
        }

        /// <summary>
        /// 获取与此 <see cref="T:System.Windows.Automation.Peers.UIElementAutomationPeer" /> 关联的 <see cref="T:System.Windows.UIElement" /> 的控件类型。此方法由 <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationControlType" /> 调用。
        /// </summary>
        /// <returns><see cref="F:System.Windows.Automation.Peers.AutomationControlType.Custom" /> 枚举值。</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.TitleBar;
        }

        /// <summary>
        /// Gets the string that uniquely identifies the <see cref="T:System.Windows.FrameworkElement" /> that is associated with this <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer" />.由 <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetAutomationId" /> 调用。
        /// </summary>
        /// <returns>返回与 <see cref="T:System.Windows.Automation.Peers.FrameworkElementAutomationPeer" /> 关联的元素的自动标识符，如果没有自动标识符，则返回 <see cref="F:System.String.Empty" />。</returns>
        protected override string GetAutomationIdCore()
        {
            return "TitleBar";
        }

        /// <summary>
        /// 获取与此 <see cref="T:System.Windows.Automation.Peers.ContentElementAutomationPeer" /> 关联的 <see cref="T:System.Windows.ContentElement" /> 的文本标签。Called by <see cref="M:System.Windows.Automation.Peers.AutomationPeer.GetName" />.
        /// </summary>
        /// <returns>与此自动同级关联的元素的文本标签。</returns>
        protected override string GetNameCore()
        {
            PresentationSource source = PresentationSource.FromVisual(Owner);
            if (source != null)
            {
                System.Windows.Window rootVisual = source.RootVisual as System.Windows.Window;
                if (rootVisual != null)
                {
                    return rootVisual.Title;
                }
            }
            return "TitleBar";
        }
    }
}