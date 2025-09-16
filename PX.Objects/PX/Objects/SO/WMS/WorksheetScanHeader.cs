// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.WorksheetScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.WMS;

#nullable enable
namespace PX.Objects.SO.WMS;

public sealed class WorksheetScanHeader : PXCacheExtension<
#nullable disable
WMSScanHeader, QtyScanHeader, ScanHeader>
{
  public static bool IsActive() => WorksheetPicking.IsActive();

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Worksheet Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (SOPickingWorksheet.worksheetNbr))]
  public string WorksheetNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Picker Nbr.", Enabled = false, Visible = false)]
  public int? PickerNbr { get; set; }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WorksheetScanHeader.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WorksheetScanHeader.pickerNbr>
  {
  }
}
