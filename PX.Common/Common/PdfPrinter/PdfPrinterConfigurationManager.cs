// Decompiled with JetBrains decompiler
// Type: PX.Common.PdfPrinter.PdfPrinterConfigurationManager
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.Win32;
using System.Configuration;

#nullable disable
namespace PX.Common.PdfPrinter;

/// <summary>
/// Configure registry for AppPool user to use pdf printer
/// </summary>
public class PdfPrinterConfigurationManager
{
  /// <summary>
  /// Setting default printer for printer and disable windows printer managment
  /// </summary>
  /// <param name="printerRegistryName">Printe name and params. You can get this information from regkey: Software\Microsoft\Windows NT\CurrentVersion\Devices. Example (Acumatica_PrinterSite_Landscape, winspool,Ne08:)</param>
  /// <param name="sid">User Security Identifier</param>
  /// <returns>False if can't edit registry</returns>
  public static bool SetDefaultPrinter(string printerRegistryName, string sid)
  {
    using (RegistryKey registryKey = Registry.Users.OpenSubKey(sid + "\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\", true))
    {
      if (registryKey == null)
        return false;
      registryKey.SetValue("LegacyDefaultPrinterMode", (object) 1);
      registryKey.SetValue("Device", (object) printerRegistryName);
      registryKey.Close();
    }
    return true;
  }

  /// <summary>
  /// Setting default IE configuration for printing. Remove header, footer and allow print background
  /// </summary>
  /// <param name="sid">User Security Identifier</param>
  /// <returns>False if can't edit registry</returns>
  public static bool SetDefaultIEConfiguration(string sid)
  {
    using (RegistryKey registryKey = Registry.Users.OpenSubKey(sid + "\\Software\\Microsoft\\Internet Explorer\\", true))
    {
      if (registryKey == null)
        return false;
      RegistryKey subKey = registryKey.CreateSubKey("PageSetup", true);
      subKey.SetValue("header", (object) "");
      subKey.SetValue("footer", (object) "");
      subKey.SetValue("Print_Background", (object) "yes");
      subKey.SetValue("margin_top", (object) "0.5");
      subKey.SetValue("margin_bottom", (object) "0.5");
    }
    return true;
  }

  /// <summary>Setting default page orientation for printing.</summary>
  /// <param name="printerName">Printer name from web.config</param>
  public static void SetIEPageOrientation(string printerName, bool isPortrait)
  {
    new PrinterWinApiCommands().ChangePrinterLayout(printerName.Substring(0, printerName.IndexOf(",")), isPortrait);
  }

  public static bool IsMicrosoftPdfEnabled
  {
    get
    {
      return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PdfPrinter"]) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PdfPrinterPort"]);
    }
  }
}
