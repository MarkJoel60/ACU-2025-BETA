// Decompiled with JetBrains decompiler
// Type: PX.SM.IPrintable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.SM;

public interface IPrintable
{
  bool? PrintWithDeviceHub { get; set; }

  bool? DefinePrinterManually { get; set; }

  Guid? PrinterID { get; set; }

  int? NumberOfCopies { get; set; }
}
