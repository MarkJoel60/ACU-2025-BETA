// Decompiled with JetBrains decompiler
// Type: PX.SM.PrintSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

public class PrintSettings : IEquatable<PrintSettings>, IPrintable
{
  public bool? PrintWithDeviceHub { get; set; }

  public bool? DefinePrinterManually { get; set; }

  public Guid? PrinterID { get; set; }

  public int? NumberOfCopies { get; set; }

  public override int GetHashCode() => !this.PrinterID.HasValue ? 0 : this.PrinterID.GetHashCode();

  public override bool Equals(object obj) => this.Equals(obj as PrintSettings);

  public bool Equals(PrintSettings obj)
  {
    if (obj == null)
      return false;
    Guid? printerId1 = obj.PrinterID;
    Guid? printerId2 = this.PrinterID;
    if (printerId1.HasValue != printerId2.HasValue)
      return false;
    return !printerId1.HasValue || printerId1.GetValueOrDefault() == printerId2.GetValueOrDefault();
  }
}
