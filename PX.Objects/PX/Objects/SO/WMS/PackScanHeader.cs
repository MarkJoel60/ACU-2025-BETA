// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PackScanHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN.WMS;
using System;

#nullable enable
namespace PX.Objects.SO.WMS;

/// <exclude />
public sealed class PackScanHeader : PXCacheExtension<
#nullable disable
WMSScanHeader, QtyScanHeader, ScanHeader>
{
  [PXInt]
  [PXFormula(typeof (BqlOperand<Null, IBqlNull>.When<BqlOperand<WMSScanHeader.refNbr, IBqlString>.IsNull>.Else<PackScanHeader.packageLineNbr>))]
  public int? PackageLineNbr { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Package")]
  [PXSelector(typeof (SearchFor<SOPackageDetailEx.lineNbr>.Where<BqlOperand<SOPackageDetailEx.shipmentNbr, IBqlString>.IsEqual<BqlField<WMSScanHeader.refNbr, IBqlString>.FromCurrent>>), new Type[] {typeof (TypeArrayOf<IBqlField>.FilledWith<SOPackageDetail.confirmed, SOPackageDetailEx.lineNbr, SOPackageDetailEx.boxID, SOPackageDetailEx.boxDescription, SOPackageDetail.weight, SOPackageDetailEx.maxWeight, SOPackageDetail.weightUOM, SOPackageDetail.length, SOPackageDetail.width, SOPackageDetail.height>)}, DescriptionField = typeof (SOPackageDetailEx.boxID), DirtyRead = true, SuppressUnconditionalSelect = true)]
  [PXFormula(typeof (BqlOperand<PackScanHeader.packageLineNbr, IBqlInt>.When<BqlOperand<PackScanHeader.packageLineNbr, IBqlInt>.IsNotNull>.Else<PackScanHeader.packageLineNbrUI>))]
  public int? PackageLineNbrUI { get; set; }

  [PXDecimal(6)]
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  public Decimal? Weight { get; set; }

  [PXDate]
  public DateTime? LastWeighingTime { get; set; }

  [PXDecimal(6)]
  public Decimal? Length { get; set; }

  [PXDecimal(6)]
  public Decimal? Width { get; set; }

  [PXDecimal(6)]
  public Decimal? Height { get; set; }

  public abstract class packageLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PackScanHeader.packageLineNbr>
  {
  }

  public abstract class packageLineNbrUI : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PackScanHeader.packageLineNbrUI>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PackScanHeader.weight>
  {
  }

  public abstract class lastWeighingTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PackScanHeader.lastWeighingTime>
  {
  }

  public abstract class length : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PackScanHeader.length>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PackScanHeader.width>
  {
  }

  public abstract class height : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PackScanHeader.height>
  {
  }
}
