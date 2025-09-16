// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.RoundingManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.TX;

public class RoundingManager
{
  private readonly PX.Objects.AP.Vendor vendor;
  private readonly int _taxReportRevisionID;

  public RoundingManager(PXGraph graph, int? vendorId, int taxReportRevisionID)
  {
    this.vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) vendorId
    }));
    this._taxReportRevisionID = taxReportRevisionID;
  }

  public RoundingManager(PX.Objects.AP.Vendor vendor, int taxReportRevisionID)
  {
    this.vendor = vendor;
    this._taxReportRevisionID = taxReportRevisionID;
  }

  public bool IsRequireRounding
  {
    get
    {
      return this.vendor != null && !this.vendor.TaxUseVendorCurPrecision.GetValueOrDefault() && this.vendor.TaxReportRounding != null && this.vendor.TaxReportPrecision.HasValue;
    }
  }

  public PX.Objects.AP.Vendor CurrentVendor => this.vendor;

  public int CurrentTaxReportRevisionID => this._taxReportRevisionID;

  public Decimal? Round(Decimal? value)
  {
    if (!value.HasValue || this.vendor == null || !this.IsRequireRounding)
      return value;
    short decimals = this.vendor.TaxReportPrecision ?? (short) 2;
    Decimal num = value.Value;
    switch (this.vendor.TaxReportRounding)
    {
      case "R":
        return new Decimal?(PXRound.Math(num, (int) decimals));
      case "C":
        return new Decimal?(PXRound.Ceil(num, (int) decimals));
      case "F":
        return new Decimal?(PXRound.Floor(num, (int) decimals));
      default:
        return value;
    }
  }
}
