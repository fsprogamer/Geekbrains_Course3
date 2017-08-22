using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для BorderForTransparentWindow.xaml
    /// </summary>
    public partial class BorderForTransparentWindow : UserControl
    {
        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource hwndSource;
        Window window;
        public BorderForTransparentWindow()
        {
            InitializeComponent();
            Loaded += (a1, b1) => Initialize();
        }
  

        IntPtr retInt = IntPtr.Zero;

        public HwndSource HwndSource { get => hwndSource; set => hwndSource = value; }

        public void Initialize()
        {
            Contract.Requires(window != null);
            window = Window.GetWindow(this);
            hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(window).Handle);
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            
        }
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            Debug.WriteLine("WndProc messages: " + msg.ToString());
            //
            // Check incoming window system messages

            if (msg == WM_SYSCOMMAND)
            {
                Debug.WriteLine("WndProc messages: " + msg.ToString());
            }

            return IntPtr.Zero;
        }

        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private void ResetCursor(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void Resize(object sender, MouseButtonEventArgs e)
        {
            Rectangle clickedRectangle = sender as Rectangle;

            switch (clickedRectangle.Name)
            {
                case "top":
                    this.Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    this.Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    this.Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    this.Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    this.Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    this.Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    this.Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    this.Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }

        private void DisplayResizeCursor(object sender, MouseEventArgs e)
        {
            Rectangle clickedRectangle = sender as Rectangle;

            switch (clickedRectangle.Name)
            {
                case "top":
                    this.Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    this.Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    this.Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    this.Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    this.Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    this.Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }

        private void Drag(object sender, MouseButtonEventArgs e)
        {
            try
            {
                window.DragMove();
            }
            catch { }
        }

        private void button_ClickClose(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        private void button_ClickHide(object sender, RoutedEventArgs e)
        {
            window.Hide();
        }

        private void CursorMouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
    }
}
