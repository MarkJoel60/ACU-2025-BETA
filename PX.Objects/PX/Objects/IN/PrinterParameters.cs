// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PrinterParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;
using System;

#nullable disable
namespace PX.Objects.IN;

internal class PrinterParameters : IPrintable
{
  public bool? PrintWithDeviceHub { get; set; }

  public bool? DefinePrinterManually { get; set; }

  public Guid? PrinterID { get; set; }

  public int? NumberOfCopies { get; set; }
}
