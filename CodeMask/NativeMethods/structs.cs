using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace NativeMethodsPack
{
    #region Struct

    /// <summary>
    /// Struct SIZE
    /// </summary>
    public struct SIZE
    {
        /// <summary>
        /// The cx
        /// </summary>
        [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LONG")]
        public int cx;

        /// <summary>
        /// The cy
        /// </summary>
        [ComAliasName("Microsoft.VisualStudio.OLE.Interop.LONG")]
        public int cy;
    }

    //public enum VARTYPE : ushort
    //{
    //    VT_EMPTY = 0,
    //    VT_NULL = 1,
    //    VT_I2 = 2,
    //    VT_I4 = 3,
    //    VT_R4 = 4,
    //    VT_R8 = 5,
    //    VT_CY = 6,
    //    VT_DATE = 7,
    //    VT_BSTR = 8,
    //    VT_DISPATCH = 9,
    //    VT_ERROR = 10,
    //    VT_BOOL = 11,
    //    VT_VARIANT = 12,
    //    VT_UNKNOWN = 13,
    //    VT_DECIMAL = 14,
    //    VT_I1 = 16,
    //    VT_UI1 = 17,
    //    VT_UI2 = 18,
    //    VT_UI4 = 19,
    //    VT_I8 = 20,
    //    VT_UI8 = 21,
    //    VT_INT = 22,
    //    VT_UINT = 23,
    //    VT_VOID = 24,
    //    VT_HRESULT = 25,
    //    VT_PTR = 26,
    //    VT_SAFEARRAY = 27,
    //    VT_CARRAY = 28,
    //    VT_USERDEFINED = 29,
    //    VT_LPSTR = 30,
    //    VT_LPWSTR = 31,
    //    VT_RECORD = 36,
    //    VT_INT_PTR = 37,
    //    VT_UINT_PTR = 38,
    //    VT_FILETIME = 64,
    //    VT_BLOB = 65,
    //    VT_STREAM = 66,
    //    VT_STORAGE = 67,
    //    VT_STREAMED_OBJECT = 68,
    //    VT_STORED_OBJECT = 69,
    //    VT_BLOB_OBJECT = 70,
    //    VT_CF = 71,
    //    VT_CLSID = 72,
    //    VT_VERSIONED_STREAM = 73,
    //    VT_ILLEGALMASKED = 4095,
    //    VT_BSTR_BLOB = 4095,
    //    VT_TYPEMASK = 4095,
    //    VT_VECTOR = 4096,
    //    VT_ARRAY = 8192,
    //    VT_BYREF = 16384,
    //    VT_RESERVED = 32768,
    //    VT_ILLEGAL = 65535
    //}

    //public enum VARIANTFLAGS : ushort
    //{
    //    NONE = 0,
    //    VARIANT_NOVALUEPROP = 1,
    //    VARIANT_ALPHABOOL = 2,
    //    VARIANT_NOUSEROVERRIDE = 4,
    //    VARIANT_LOCALBOOL = 16
    //}

    //public struct IMAGEINFO
    //{
    //    public IntPtr hbmImage;

    //    public IntPtr hbmMask;

    //    public int Unused1;

    //    public int Unused2;

    //    public RECT rcImage;
    //}

    /// <summary>
    /// Struct MONITORINFO
    /// </summary>
    public struct MONITORINFO
    {
        /// <summary>
        /// The cb size
        /// </summary>
        public uint cbSize;

        /// <summary>
        /// The rc monitor
        /// </summary>
        public RECT rcMonitor;

        /// <summary>
        /// The rc work
        /// </summary>
        public RECT rcWork;

        /// <summary>
        /// The dw flags
        /// </summary>
        public uint dwFlags;
    }

    //[StructLayout(LayoutKind.Explicit)]
    //public struct VARIANT
    //{
    //    [FieldOffset(0)]
    //    public VARTYPE vt;

    //    [FieldOffset(8)]
    //    public IntPtr pdispVal;

    //    [FieldOffset(8)]
    //    public byte ui1;

    //    [FieldOffset(8)]
    //    public ushort ui2;

    //    [FieldOffset(8)]
    //    public uint ui4;

    //    [FieldOffset(8)]
    //    public ulong ui8;

    //    [FieldOffset(8)]
    //    public float r4;

    //    [FieldOffset(8)]
    //    public double r8;
    //}

    /// <summary>
    /// Enum SHGFI
    /// </summary>
    [Flags]
    public enum SHGFI : uint
    {
        /// <summary>
        /// The large icon
        /// </summary>
        LargeIcon = 0,
        /// <summary>
        /// The small icon
        /// </summary>
        SmallIcon = 1,
        /// <summary>
        /// The open icon
        /// </summary>
        OpenIcon = 2,
        /// <summary>
        /// The shell icon size
        /// </summary>
        ShellIconSize = 4,
        /// <summary>
        /// The PIDL
        /// </summary>
        PIDL = 8,
        /// <summary>
        /// The use file attributes
        /// </summary>
        UseFileAttributes = 16,
        /// <summary>
        /// The add overlays
        /// </summary>
        AddOverlays = 32,
        /// <summary>
        /// The overlay index
        /// </summary>
        OverlayIndex = 64,
        /// <summary>
        /// The icon
        /// </summary>
        Icon = 256,
        /// <summary>
        /// The display name
        /// </summary>
        DisplayName = 512,
        /// <summary>
        /// The type name
        /// </summary>
        TypeName = 1024,
        /// <summary>
        /// The attributes
        /// </summary>
        Attributes = 2048,
        /// <summary>
        /// The icon location
        /// </summary>
        IconLocation = 4096,
        /// <summary>
        /// The exe type
        /// </summary>
        ExeType = 8192,
        /// <summary>
        /// The sys icon index
        /// </summary>
        SysIconIndex = 16384,
        /// <summary>
        /// The link overlay
        /// </summary>
        LinkOverlay = 32768,
        /// <summary>
        /// The selected
        /// </summary>
        Selected = 65536,
        /// <summary>
        /// The attr_ specified
        /// </summary>
        Attr_Specified = 131072
    }

    //public struct ACCEL
    //{
    //    private byte fVirt;

    //    private ushort key;

    //    private ushort cmd;
    //}

    /// <summary>
    /// Struct SHFILEINFO
    /// </summary>
    public struct SHFILEINFO
    {
        /// <summary>
        /// The h icon
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// The i icon
        /// </summary>
        public int iIcon;

        /// <summary>
        /// The dw attributes
        /// </summary>
        public uint dwAttributes;

        /// <summary>
        /// The sz display name
        /// </summary>
        public string szDisplayName;

        /// <summary>
        /// The sz type name
        /// </summary>
        public string szTypeName;
    }

    /// <summary>
    /// Struct WNDCLASSEX
    /// </summary>
    public struct WNDCLASSEX
    {
        /// <summary>
        /// WNDCLASSEX 的大小。我们可以用sizeof（WNDCLASSEX）来获得准确的值。
        /// </summary>
        public uint cbSize;

        /// <summary>
        /// 从这个窗口类派生的窗口具有的风格。您可以用“or”操作符来把几个风格或到一起。
        /// </summary>
        public uint style;

        /// <summary>
        /// 窗口处理函数的指针。
        /// </summary>
        public Delegate lpfnWndProc;

        /// <summary>
        /// 指定紧跟在窗口类结构后的附加字节数。
        /// </summary>
        public int cbClsExtra;

        /// <summary>
        /// 指定紧跟在窗口事例后的附加字节数。如果一个应用程序在资源中用CLASS伪指令注册一个对话框类时，则必须把这个成员设成DLGWINDOWEXTRA。
        /// </summary>
        public int cbWndExtra;

        /// <summary>
        /// 本模块的事例句柄。
        /// </summary>
        public IntPtr hInstance;

        /// <summary>
        /// 图标的句柄。
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// 光标的句柄。
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// 背景画刷的句柄。
        /// </summary>
        public IntPtr hbrBackground;

        /// <summary>
        /// 指向菜单的指针。
        /// </summary>
        public string lpszMenuName;

        /// <summary>
        /// 指向类名称的指针。
        /// </summary>
        public string lpszClassName;

        /// <summary>
        /// 和窗口类关联的小图标。如果该值为NULL。则把hIcon中的图标转换成大小合适的小图标。
        /// </summary>
        public IntPtr hIconSm;
    }

    /// <summary>
    /// WNDCLASS结构包含了RegisterClass函数注册窗口类时的窗口类属性。
    /// </summary>
    public struct WNDCLASS
    {
        /// <summary>
        /// 描述类风格。
        /// </summary>
        public uint style;

        /// <summary>
        /// 指向窗口过程的指针。必须使用CallWindowProc函数调用窗口过程。
        /// </summary>
        public Delegate lpfnWndProc;

        /// <summary>
        /// 表示窗口类结构之后分配的额外的字节数。系统将该值初始化为0.
        /// </summary>
        public int cbClsExtra;

        /// <summary>
        /// 表示窗口实例之后分配的额外的字节数。系统将该值初始化为0.如果使用资源文件里的CLASS指令创建对话框，并用WNDCLASS注册该对话框时，cbWndExtra必须设置成DLGWNDOWEXTRA。
        /// </summary>
        public int cbWndExtra;

        /// <summary>
        /// 包含该类实例的句柄，该实例包含了窗口过程。
        /// </summary>
        public IntPtr hInstance;

        /// <summary>
        /// 类图标的句柄。该成员必须为一个图标资源的句柄。如果hIcon为NULL，系统将提供默认图标。
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// 鼠标指针的句柄。改成员必须为一个指针资源的句柄。如果hCursor为NULL，应用程序必须在指针移入应用程序窗口时显式设置指针类型。
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// 背景画刷的句柄。
        /// </summary>
        public IntPtr hbrBackground;

        /// <summary>
        /// 指向NULL结束的字符串，该字符串描述菜单的资源名，如同在资源文件里显示的名字一样。若使用一个整数标识菜单，可以使用MAKEINTRESOURCE宏。如果lpszMenuName为NULL，那么该窗口类的窗口将没有默认菜单。
        /// </summary>
        public string lpszMenuName;

        /// <summary>
        /// 指向NULL结束的字符串，或者是一个原型(atom)。
        /// </summary>
        public string lpszClassName;
    }

    /// <summary>
    /// WINDOWPOS结构包含了有关窗口的大小和位置的信息。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class WINDOWPOS
    {
        /// <summary>
        /// 标识窗口。
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// 标识了一个窗口，本窗口将被放在这个窗口的后面。
        /// </summary>
        public IntPtr hwndInsertAfter;

        /// <summary>
        /// 指定了窗口的左边界的位置。
        /// </summary>
        public int x;

        /// <summary>
        /// 指定了窗口的右边界的位置。
        /// </summary>
        public int y;

        /// <summary>
        /// 指定了窗口的宽度，以象素为单位。
        /// </summary>
        public int cx;

        /// <summary>
        /// 指定了窗口的高度，以象素为单位。
        /// </summary>
        public int cy;

        /// <summary>
        /// 指定了窗口位置的选项。参照SWP_FLAGS。
        /// </summary>
        public uint flags;
    }

    /// <summary>
    /// Struct POINT
    /// </summary>
    public struct POINT
    {
        /// <summary>
        /// The x
        /// </summary>
        public int x;

        /// <summary>
        /// The y
        /// </summary>
        public int y;
    }

    /// <summary>
    /// Struct RECT
    /// </summary>
    [Serializable]
    public struct RECT
    {
        /// <summary>
        /// The left
        /// </summary>
        public int Left;

        /// <summary>
        /// The top
        /// </summary>
        public int Top;

        /// <summary>
        /// The right
        /// </summary>
        public int Right;

        /// <summary>
        /// The bottom
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get
            {
                return this.Bottom - this.Top;
            }
            set
            {
                this.Bottom = this.Top + value;
            }
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public Point Position
        {
            get
            {
                return new Point((double)this.Left, (double)this.Top);
            }
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public Size Size
        {
            get
            {
                return new Size((double)this.Width, (double)this.Height);
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get
            {
                return this.Right - this.Left;
            }
            set
            {
                this.Right = this.Left + value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RECT"/> struct.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        public RECT(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RECT"/> struct.
        /// </summary>
        /// <param name="rect">The rect.</param>
        public RECT(Rect rect)
        {
            this.Left = (int)rect.Left;
            this.Top = (int)rect.Top;
            this.Right = (int)rect.Right;
            this.Bottom = (int)rect.Bottom;
        }

        /// <summary>
        /// Offsets the specified dx.
        /// </summary>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        public void Offset(int dx, int dy)
        {
            this.Left = this.Left + dx;
            this.Right = this.Right + dx;
            this.Top = this.Top + dy;
            this.Bottom = this.Bottom + dy;
        }

        /// <summary>
        /// To the int32 rect.
        /// </summary>
        /// <returns>Int32Rect.</returns>
        public Int32Rect ToInt32Rect()
        {
            return new Int32Rect(this.Left, this.Top, this.Width, this.Height);
        }
    }

    /// <summary>
    /// Struct LOGFONT
    /// </summary>
    public struct LOGFONT
    {
        /// <summary>
        /// The lf height
        /// </summary>
        public int lfHeight;

        /// <summary>
        /// The lf width
        /// </summary>
        public int lfWidth;

        /// <summary>
        /// The lf escapement
        /// </summary>
        public int lfEscapement;

        /// <summary>
        /// The lf orientation
        /// </summary>
        public int lfOrientation;

        /// <summary>
        /// The lf weight
        /// </summary>
        public int lfWeight;

        /// <summary>
        /// The lf italic
        /// </summary>
        public byte lfItalic;

        /// <summary>
        /// The lf underline
        /// </summary>
        public byte lfUnderline;

        /// <summary>
        /// The lf strike out
        /// </summary>
        public byte lfStrikeOut;

        /// <summary>
        /// The lf char set
        /// </summary>
        public byte lfCharSet;

        /// <summary>
        /// The lf out precision
        /// </summary>
        public byte lfOutPrecision;

        /// <summary>
        /// The lf clip precision
        /// </summary>
        public byte lfClipPrecision;

        /// <summary>
        /// The lf quality
        /// </summary>
        public byte lfQuality;

        /// <summary>
        /// The lf pitch and family
        /// </summary>
        public byte lfPitchAndFamily;

        /// <summary>
        /// The lf face name
        /// </summary>
        public string lfFaceName;
    }

    /// <summary>
    /// Struct NONCLIENTMETRICS
    /// </summary>
    public struct NONCLIENTMETRICS
    {
        /// <summary>
        /// The cb size
        /// </summary>
        public int cbSize;

        /// <summary>
        /// The i border width
        /// </summary>
        public int iBorderWidth;

        /// <summary>
        /// The i scroll width
        /// </summary>
        public int iScrollWidth;

        /// <summary>
        /// The i scroll height
        /// </summary>
        public int iScrollHeight;

        /// <summary>
        /// The i caption width
        /// </summary>
        public int iCaptionWidth;

        /// <summary>
        /// The i caption height
        /// </summary>
        public int iCaptionHeight;

        /// <summary>
        /// The lf caption font
        /// </summary>
        public LOGFONT lfCaptionFont;

        /// <summary>
        /// The i sm caption width
        /// </summary>
        public int iSmCaptionWidth;

        /// <summary>
        /// The i sm caption height
        /// </summary>
        public int iSmCaptionHeight;

        /// <summary>
        /// The lf sm caption font
        /// </summary>
        public LOGFONT lfSmCaptionFont;

        /// <summary>
        /// The i menu width
        /// </summary>
        public int iMenuWidth;

        /// <summary>
        /// The i menu height
        /// </summary>
        public int iMenuHeight;

        /// <summary>
        /// The lf menu font
        /// </summary>
        public LOGFONT lfMenuFont;

        /// <summary>
        /// The lf status font
        /// </summary>
        public LOGFONT lfStatusFont;

        /// <summary>
        /// The lf message font
        /// </summary>
        public LOGFONT lfMessageFont;

        /// <summary>
        /// The i padded border width
        /// </summary>
        public int iPaddedBorderWidth;
    }

    /// <summary>
    /// Struct COPYDATASTRUCT
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        /// <summary>
        /// The dw data
        /// </summary>
        public IntPtr dwData;
        /// <summary>
        /// The cb data
        /// </summary>
        public int cbData;

        /// <summary>
        /// The lp data
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    /// <summary>
    /// Struct WINDOWINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        /// <summary>
        /// The cb size
        /// </summary>
        public uint cbSize;
        /// <summary>
        /// The rc window
        /// </summary>
        public RECT rcWindow;
        /// <summary>
        /// The rc client
        /// </summary>
        public RECT rcClient;
        /// <summary>
        /// The dw style
        /// </summary>
        public uint dwStyle;
        /// <summary>
        /// The dw ex style
        /// </summary>
        public uint dwExStyle;
        /// <summary>
        /// The dw window status
        /// </summary>
        public uint dwWindowStatus;
        /// <summary>
        /// The cx window borders
        /// </summary>
        public uint cxWindowBorders;
        /// <summary>
        /// The cy window borders
        /// </summary>
        public uint cyWindowBorders;
        /// <summary>
        /// The atom window type
        /// </summary>
        public ushort atomWindowType;
        /// <summary>
        /// The w creator version
        /// </summary>
        public ushort wCreatorVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="WINDOWINFO"/> struct.
        /// </summary>
        /// <param name="filler">The filler.</param>
        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }
    }

    /// <summary>
    /// WINDOWPLACEMENT 结构中包含了有关窗口在屏幕上位置的信息。
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        /// <summary>
        /// 结构的长度，以字节为单位。
        /// </summary>
        public int Length;

        /// <summary>
        /// 指定了控制最小化窗口的位置的标志以及复原窗口的方法。
        /// </summary>
        public int Flags;

        /// <summary>
        /// 指定了窗口的当前显示状态。参考SW_。
        /// </summary>
        public int ShowCmd;

        /// <summary>
        /// 指定了窗口被最小化时左上角的位置。
        /// </summary>
        public Point MinPosition;

        /// <summary>
        /// 指定了窗口被最大化时左上角的位置。
        /// </summary>
        public Point MaxPosition;

        /// <summary>
        /// 指定了窗口处于正常状态（复原）时的坐标。
        /// </summary>
        public RECT NormalPosition;

        /// <summary>
        /// Gets the default (empty) value.
        /// </summary>
        /// <value>The default.</value>
        public static WINDOWPLACEMENT Default
        {
            get
            {
                WINDOWPLACEMENT result = new WINDOWPLACEMENT();
                result.Length = Marshal.SizeOf(result);
                return result;
            }
        }
    }

    /// <summary>
    /// Struct BITMAPINFO
    /// </summary>
    public struct BITMAPINFO
    {
        /// <summary>
        /// The bi size
        /// </summary>
        public int biSize;

        /// <summary>
        /// The bi width
        /// </summary>
        public int biWidth;

        /// <summary>
        /// The bi height
        /// </summary>
        public int biHeight;

        /// <summary>
        /// The bi planes
        /// </summary>
        public short biPlanes;

        /// <summary>
        /// The bi bit count
        /// </summary>
        public short biBitCount;

        /// <summary>
        /// The bi compression
        /// </summary>
        public int biCompression;

        /// <summary>
        /// The bi size image
        /// </summary>
        public int biSizeImage;

        /// <summary>
        /// The bi X pels per meter
        /// </summary>
        public int biXPelsPerMeter;

        /// <summary>
        /// The bi Y pels per meter
        /// </summary>
        public int biYPelsPerMeter;

        /// <summary>
        /// The bi CLR used
        /// </summary>
        public int biClrUsed;

        /// <summary>
        /// The bi CLR important
        /// </summary>
        public int biClrImportant;

        /// <summary>
        /// The bmi colors
        /// </summary>
        public byte[] bmiColors;

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static BITMAPINFO Default
        {
            get
            {
                BITMAPINFO bITMAPINFO = new BITMAPINFO();
                bITMAPINFO.biSize = 40;
                bITMAPINFO.biPlanes = 1;
                return bITMAPINFO;
            }
        }
    }

    /// <summary>
    /// Struct BLENDFUNCTION
    /// </summary>
    public struct BLENDFUNCTION
    {
        /// <summary>
        /// 指定源混合操作。目前，唯一的源和目标的混合方式已定义为AC_SRC_OVER。
        /// </summary>
        public byte BlendOp;

        /// <summary>
        /// 必须是0。
        /// </summary>
        public byte BlendFlags;

        /// <summary>
        /// 指定一个alpha透明度值，这个值将用于整个源位图;该SourceConstantAlpha值与源位图的每个像素的alpha值组合;如果设置为0，就会假定你的图片是透明的;如果需要使用每像素本身的alpha值，设置SourceConstantAlpha值255（不透明）。
        /// </summary>
        public byte SourceConstantAlpha;

        /// <summary>
        /// 这个参数控制源和目标的解析方式，AlphaFormat参数有以下值：
        /// AC_SRC_ALPHA： 这个值在源或者目标本身有Alpha通道时（也就是操作的图本身带有透明通道信息时），提醒系统API调用函数前必须预先乘以alpha值，也就是说位图上某个像素位置的red、green、blue通道值必须先与alpha相乘。例如，如果alpha透明值是x，那么red、green、blue三个通道的值必须乘以x并且再除以255（因为alpha的值的范围是0～255），之后才能被调用。
        /// </summary>
        public byte AlphaFormat;
    }

    /// <summary>
    /// Struct BITMAPINFOHEADER
    /// </summary>
    public struct BITMAPINFOHEADER
    {
        /// <summary>
        /// 指定这个结构的长度，为40。
        /// </summary>
        public uint biSize;

        /// <summary>
        /// 指定图象的宽度，单位是象素。
        /// </summary>
        public int biWidth;

        /// <summary>
        /// 指定图象的高度，单位是象素。
        /// </summary>
        public int biHeight;

        /// <summary>
        /// 必须是1，不用考虑。
        /// </summary>
        public ushort biPlanes;

        /// <summary>
        /// 指定表示颜色时要用到的位数，常用的值为1(黑白二色图), 4(16色图), 8(256色), 24(真彩色图)(新的.bmp格式支持32位色)。
        /// </summary>
        public ushort biBitCount;

        /// <summary>
        /// 指定位图是否压缩，有效的值为BI_RGB，BI_RLE8，BI_RLE4，BI_BITFIELDS(都是一些Windows定义好的常量)。
        /// </summary>
        public uint biCompression;

        /// <summary>
        /// 指定实际的位图数据占用的字节数。
        /// </summary>
        public uint biSizeImage;

        /// <summary>
        /// 指定目标设备的水平分辨率。
        /// </summary>
        public int biXPelsPerMeter;

        /// <summary>
        /// 指定目标设备的垂直分辨率。
        /// </summary>
        public int biYPelsPerMeter;

        /// <summary>
        /// 指定本图象实际用到的颜色数，如果该值为零，则用到的颜色数为2biBitCount。
        /// </summary>
        public uint biClrUsed;

        /// <summary>
        /// The bi CLR important
        /// </summary>
        public uint biClrImportant;

        /// <summary>
        /// 指定本图象中重要的颜色数，如果该值为零，则认为所有的颜色都是重要的。
        /// </summary>
        /// <value>The default.</value>
        public static BITMAPINFOHEADER Default
        {
            get
            {
                BITMAPINFOHEADER bITMAPINFOHEADER = new BITMAPINFOHEADER();
                bITMAPINFOHEADER.biSize = 40;
                bITMAPINFOHEADER.biPlanes = 1;
                return bITMAPINFOHEADER;
            }
        }
    }

    #endregion Struct
}