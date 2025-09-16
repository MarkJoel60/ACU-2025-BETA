// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderItemAvailabilityAllocatedExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXProtectedAccess(typeof (FSServiceOrderItemAvailabilityExtension))]
public abstract class FSServiceOrderItemAvailabilityAllocatedExtension : 
  ItemAvailabilityAllocatedExtension<ServiceOrderEntry, FSServiceOrderItemAvailabilityExtension, FSSODet, FSSODetSplit>
{
  protected FSSODet DetailLine { get; private set; }

  protected override string GetStatusWithAllocated(FSSODet line)
  {
    string statusWithAllocated = string.Empty;
    this.DetailLine = line;
    IStatus availability = this.Base1.FetchWithLineUOM(line, line == null || !line.Completed.GetValueOrDefault(), (int?) line?.CostCenterID);
    if (availability != null)
    {
      Decimal allocatedQty = this.GetAllocatedQty(line);
      statusWithAllocated = this.FormatStatusAllocated(availability, new Decimal?(allocatedQty), line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return statusWithAllocated;
  }

  protected virtual Decimal GetAllocatedQty(FSSODet line)
  {
    return PXDBQuantityAttribute.Round(new Decimal?(line.LineQtyHardAvail.GetValueOrDefault() * this.GetUnitRate(line)));
  }

  private string FormatStatusAllocated(IStatus availability, Decimal? allocated, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}, Allocated {4} {0}", new object[5]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail),
      (object) this.FormatQty(allocated)
    });
  }

  protected override Type LineQtyAvail => typeof (FSSODet.lineQtyAvail);

  protected override Type LineQtyHardAvail => typeof (FSSODet.lineQtyHardAvail);

  protected override FSSODetSplit[] GetSplits(FSSODet line)
  {
    return ((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base).FindImplementation<FSServiceOrderLineSplittingExtension>().GetSplits(line);
  }

  protected override FSSODetSplit EnsurePlanType(FSSODetSplit split)
  {
    if (split.PlanID.HasValue)
    {
      INItemPlan itemPlan = this.GetItemPlan(split.PlanID);
      if (itemPlan != null)
      {
        split = PXCache<FSSODetSplit>.CreateCopy(split);
        split.PlanType = itemPlan.PlanType;
      }
    }
    return split;
  }

  protected override Guid? DocumentNoteID
  {
    get
    {
      return ((PXSelectBase<FSServiceOrder>) ((PXGraphExtension<ServiceOrderEntry>) this).Base.ServiceOrderRecords).Current?.NoteID;
    }
  }

  protected override IStatus CalculateAllocatedQuantity(
    IStatus availability,
    Decimal? lineQtyAvail,
    Decimal? lineQtyHardAvail)
  {
    IStatus status1 = availability;
    Decimal? nullable1 = status1.QtyAvail;
    FSSODet detailLine1 = this.DetailLine;
    int? soDetId;
    int num1;
    if (detailLine1 == null)
    {
      num1 = 0;
    }
    else
    {
      soDetId = detailLine1.SODetID;
      int num2 = 0;
      num1 = soDetId.GetValueOrDefault() > num2 & soDetId.HasValue ? 1 : 0;
    }
    Decimal? nullable2;
    if (num1 != 0)
    {
      FSSODet detailLine2 = this.DetailLine;
      if ((detailLine2 != null ? (!detailLine2.Completed.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        nullable2 = new Decimal?(0M);
        goto label_7;
      }
    }
    nullable2 = lineQtyAvail;
label_7:
    Decimal? nullable3 = nullable2;
    status1.QtyAvail = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    IStatus status2 = availability;
    nullable3 = status2.QtyHardAvail;
    FSSODet detailLine3 = this.DetailLine;
    int num3;
    if (detailLine3 == null)
    {
      num3 = 0;
    }
    else
    {
      soDetId = detailLine3.SODetID;
      int num4 = 0;
      num3 = soDetId.GetValueOrDefault() > num4 & soDetId.HasValue ? 1 : 0;
    }
    Decimal? nullable4;
    if (num3 != 0)
    {
      FSSODet detailLine4 = this.DetailLine;
      if ((detailLine4 != null ? (!detailLine4.Completed.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        nullable4 = new Decimal?(0M);
        goto label_14;
      }
    }
    nullable4 = lineQtyHardAvail;
label_14:
    nullable1 = nullable4;
    status2.QtyHardAvail = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    IStatus status3 = availability;
    FSSODet detailLine5 = this.DetailLine;
    int num5;
    if (detailLine5 == null)
    {
      num5 = 0;
    }
    else
    {
      soDetId = detailLine5.SODetID;
      int num6 = 0;
      num5 = soDetId.GetValueOrDefault() > num6 & soDetId.HasValue ? 1 : 0;
    }
    Decimal? nullable5;
    if (num5 != 0)
    {
      FSSODet detailLine6 = this.DetailLine;
      if ((detailLine6 != null ? (!detailLine6.Completed.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        nullable5 = new Decimal?(0M);
        goto label_23;
      }
    }
    nullable1 = lineQtyAvail;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = new Decimal?(-nullable1.GetValueOrDefault());
label_23:
    status3.QtyNotAvail = nullable5;
    return availability;
  }

  [PXProtectedAccess(null)]
  protected abstract string FormatQty(Decimal? value);

  [PXProtectedAccess(null)]
  protected abstract Decimal GetUnitRate(FSSODet line);

  [PXProtectedAccess(null)]
  protected abstract void Check(ILSMaster row, IStatus availability);
}
