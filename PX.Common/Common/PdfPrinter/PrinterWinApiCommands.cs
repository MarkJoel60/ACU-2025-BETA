// Decompiled with JetBrains decompiler
// Type: PX.Common.PdfPrinter.PrinterWinApiCommands
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

#nullable disable
namespace PX.Common.PdfPrinter;

public class PrinterWinApiCommands
{
  private static readonly string \u0002 = "Unable to allocate memory. Error number: {0}";
  private static readonly string \u000E = "Unable to open printer properties. Error number: {0}";
  private static readonly string \u0006 = "Unable get DEVMODE data. Error number: {0}";
  private static readonly string \u0008 = "Unable get printer properties. Error number: {0}";
  private IntPtr \u0003;
  private PrinterWinApiCommands.\u000E \u000F = new PrinterWinApiCommands.\u000E();
  private IntPtr \u0005;
  private IntPtr \u0002\u2009;

  /// <summary>
  /// based on this demo https://www.codeproject.com/KB/dotnet/NET_Printer_Library.aspx?display=PrintAll
  /// </summary>
  public void ChangePrinterLayout(string printName, bool IsPortrait)
  {
    Marshal.StructureToPtr<PrinterWinApiCommands.DEVMODE>(this.\u0002(printName) with
    {
      dmOrientation = IsPortrait ? (short) 1 : (short) 2
    }, this.\u0002\u2009, true);
    this.\u000F.\u0002 = this.\u0002\u2009;
    Marshal.StructureToPtr<PrinterWinApiCommands.\u000E>(this.\u000F, this.\u0005, false);
    if (Convert.ToInt16(PrinterWinApiCommands.\u0002(this.\u0003, 8, this.\u0005, 0)) == (short) 0)
      throw new Win32Exception("Unable to set shared printer settings. ERROR NBR: " + Marshal.GetLastWin32Error().ToString());
    if (!(this.\u0003 != IntPtr.Zero))
      return;
    PrinterWinApiCommands.\u0002(this.\u0003);
  }

  private PrinterWinApiCommands.DEVMODE \u0002(string _param1)
  {
    if (Convert.ToInt32(PrinterWinApiCommands.\u0002(_param1, out this.\u0003, ref new PrinterWinApiCommands.\u0002()
    {
      \u0002 = IntPtr.Zero,
      \u000E = IntPtr.Zero,
      \u0006 = 983052 /*0x0F000C*/
    })) == 0)
      throw new Win32Exception(string.Format(PrinterWinApiCommands.\u000E, (object) Marshal.GetLastWin32Error()));
    int cb;
    PrinterWinApiCommands.\u0002(this.\u0003, 8, IntPtr.Zero, 0, out cb);
    this.\u0005 = cb > 0 ? Marshal.AllocCoTaskMem(cb) : throw new Win32Exception(string.Format(PrinterWinApiCommands.\u0002, (object) Marshal.GetLastWin32Error()));
    this.\u0005 = Marshal.AllocHGlobal(cb);
    long int32 = (long) Convert.ToInt32(PrinterWinApiCommands.\u0002(this.\u0003, 8, this.\u0005, cb, out int _));
    if (int32 == 0L)
      throw new Win32Exception(string.Format(PrinterWinApiCommands.\u0002, (object) Marshal.GetLastWin32Error()));
    this.\u000F = (PrinterWinApiCommands.\u000E) Marshal.PtrToStructure(this.\u0005, typeof (PrinterWinApiCommands.\u000E));
    IntPtr num1 = new IntPtr();
    if (this.\u000F.\u0002 == IntPtr.Zero)
    {
      IntPtr zero = IntPtr.Zero;
      IntPtr num2 = Marshal.AllocCoTaskMem(PrinterWinApiCommands.\u0002(IntPtr.Zero, this.\u0003, _param1, zero, ref zero, 0));
      if (PrinterWinApiCommands.\u0002(IntPtr.Zero, this.\u0003, _param1, num2, ref zero, 2) < 0 || num2 == IntPtr.Zero)
        throw new Win32Exception(string.Format(PrinterWinApiCommands.\u0006, (object) Marshal.GetLastWin32Error()));
      this.\u000F.\u0002 = num2;
    }
    this.\u0002\u2009 = Marshal.AllocHGlobal(PrinterWinApiCommands.\u0002(IntPtr.Zero, this.\u0003, _param1, IntPtr.Zero, ref num1, 0));
    PrinterWinApiCommands.\u0002(IntPtr.Zero, this.\u0003, _param1, this.\u0002\u2009, ref num1, 2);
    PrinterWinApiCommands.DEVMODE structure = (PrinterWinApiCommands.DEVMODE) Marshal.PtrToStructure(this.\u0002\u2009, typeof (PrinterWinApiCommands.DEVMODE));
    if (int32 != 0L && !(this.\u0003 == IntPtr.Zero))
      return structure;
    throw new Win32Exception(string.Format(PrinterWinApiCommands.\u0008, (object) Marshal.GetLastWin32Error()));
  }

  [DllImport("winspool.drv", EntryPoint = "ClosePrinter")]
  private static extern int \u0002(IntPtr _param0);

  [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesA", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern int \u0002(
    IntPtr _param0,
    IntPtr _param1,
    [MarshalAs(UnmanagedType.LPStr)] string _param2,
    IntPtr _param3,
    ref IntPtr _param4,
    int _param5);

  [DllImport("winspool.Drv", EntryPoint = "GetPrinterA", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern bool \u0002(
    IntPtr _param0,
    int _param1,
    IntPtr _param2,
    int _param3,
    out int _param4);

  [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern bool \u0002(
    [MarshalAs(UnmanagedType.LPStr)] string _param0,
    out IntPtr _param1,
    ref PrinterWinApiCommands.\u0002 _param2);

  [DllImport("winspool.drv", EntryPoint = "SetPrinter", CharSet = CharSet.Ansi, SetLastError = true)]
  private static extern bool \u0002(IntPtr _param0, int _param1, IntPtr _param2, int _param3);

  private struct \u0002
  {
    public IntPtr \u0002;
    public IntPtr \u000E;
    public int \u0006;
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  private sealed class \u000E
  {
    public IntPtr \u0002;
  }

  public struct DEVMODE
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string dmDeviceName;
    public short dmSpecVersion;
    public short dmDriverVersion;
    public short dmSize;
    public short dmDriverExtra;
    public int dmFields;
    public short dmOrientation;
    public short dmPaperSize;
    public short dmPaperLength;
    public short dmPaperWidth;
    public short dmScale;
    public short dmCopies;
    public short dmDefaultSource;
    public short dmPrintQuality;
    public short dmColor;
    public short dmDuplex;
    public short dmYResolution;
    public short dmTTOption;
    public short dmCollate;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string dmFormName;
    public short dmUnusedPadding;
    public short dmBitsPerPel;
    public int dmPelsWidth;
    public int dmPelsHeight;
    public int dmDisplayFlags;
    public int dmDisplayFrequency;
  }
}
