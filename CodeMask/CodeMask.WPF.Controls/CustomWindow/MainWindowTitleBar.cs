using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace CodeMask.WPF.Controls.CustomWindow
{
    /// <summary>
    /// Class MainWindowTitleBar
    /// </summary>
    public sealed class MainWindowTitleBar : Border, INonClientArea
    {
        /// <summary>
        /// 实现 <see cref="M:System.Windows.Media.Visual.HitTestCore(System.Windows.Media.PointHitTestParameters)" /> 以提供基元素命中测试行为（返回 <see cref="T:System.Windows.Media.HitTestResult" />）。
        /// </summary>
        /// <param name="hitTestParameters">描述要执行的命中测试，包括初始命中点。</param>
        /// <returns>测试结果，包括计算点。</returns>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        /// <summary>
        /// Hits the test.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>System.Int32.</returns>
        public int HitTest(Point point)
        {
            return 2;
        }

        /// <summary>
        /// 每当未处理的 <see cref="E:System.Windows.FrameworkElement.ContextMenuOpening" /> 路由事件在其路由中到达此类时调用。实现此方法可为此事件添加类处理。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.RoutedEventArgs" />。</param>
        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            if (!e.Handled)
            {
                HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
                if (source != null)
                {
                    CustomChromeWindow.ShowWindowMenu(source, this, Mouse.GetPosition(this), RenderSize);
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 为 Windows Presentation Foundation (WPF) 基础结构返回特定于类的 <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> 实现。
        /// </summary>
        /// <returns>特定于类型的 <see cref="T:System.Windows.Automation.Peers.AutomationPeer" /> 实现。</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new MainWindowTitleBarAutomationPeer(this);
        }
    }
}