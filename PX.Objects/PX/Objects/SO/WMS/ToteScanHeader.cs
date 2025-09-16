// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.ToteScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.WMS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.SO.WMS;

public sealed class ToteScanHeader : 
  PXCacheExtension<
  #nullable disable
  WorksheetScanHeader, WMSScanHeader, QtyScanHeader, ScanHeader>
{
  public static bool IsActive() => WorksheetPicking.IsActive();

  [PXBool]
  public bool? AddNewTote { get; set; }

  [PXInt]
  public int? ToteID { get; set; }

  public HashSet<int> PreparedForPackToteIDs { get; set; } = new HashSet<int>();

  public abstract class addNewTote : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ToteScanHeader.addNewTote>
  {
  }

  public abstract class toteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ToteScanHeader.toteID>
  {
  }

  public abstract class preparedForPackToteIDs : IBqlField, IBqlOperand
  {
  }
}
