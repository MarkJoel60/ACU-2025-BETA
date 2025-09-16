// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSApptLotSerialNbrAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class FSApptLotSerialNbrAttribute : SOShipLotSerialNbrAttribute
{
  public FSApptLotSerialNbrAttribute(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
    : base(SiteID, InventoryType, SubItemType, LocationType, CostCenterType)
  {
    this.CreateCustomSelector(SiteID, InventoryType, SubItemType, LocationType, CostCenterType);
  }

  public FSApptLotSerialNbrAttribute(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type ParentLotSerialNbrType,
    Type CostCenterType)
    : base(SiteID, InventoryType, SubItemType, LocationType, ParentLotSerialNbrType, CostCenterType)
  {
    this.CreateCustomSelector(SiteID, InventoryType, SubItemType, LocationType, CostCenterType);
  }

  protected virtual void CreateCustomSelector(
    Type SiteID,
    Type InventoryType,
    Type SubItemType,
    Type LocationType,
    Type CostCenterType)
  {
    PXSelectorAttribute attribute = (PXSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex];
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) new FSINLotSerialNbrAttribute(SiteID, InventoryType, SubItemType, LocationType, CostCenterType, (Type) null);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    FSApptLotSerialNbrAttribute serialNbrAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) serialNbrAttribute, __vmethodptr(serialNbrAttribute, LotSerialNumberUpdated));
    fieldUpdated.AddHandler<FSApptLineSplit.lotSerialNbr>(pxFieldUpdated);
    // ISSUE: method pointer
    sender.Graph.FieldVerifying.AddHandler<FSApptLineSplit.lotSerialNbr>(new PXFieldVerifying((object) this, __methodptr(LotSerialNumberFieldVerifying)));
  }

  protected override void LotSerialNumberUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
  }

  protected void LotSerialNumberFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID))?.LotSerAssign == "U")
      return;
    FSApptLineSplit row = (FSApptLineSplit) e.Row;
    FSAppointmentDet apptLine = (FSAppointmentDet) PXParentAttribute.SelectParent(sender, (object) row, typeof (FSAppointmentDet));
    Decimal lotSerialAvailQty;
    Decimal lotSerialUsedQty;
    bool foundServiceOrderAllocation;
    this.GetLotSerialAvailability(sender.Graph, apptLine, (string) e.NewValue, row.OrigSplitLineNbr, true, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
    Decimal num1 = lotSerialAvailQty - lotSerialUsedQty;
    Decimal num2 = num1;
    Decimal? qty = row.Qty;
    Decimal valueOrDefault = qty.GetValueOrDefault();
    if (!(num2 < valueOrDefault & qty.HasValue))
    {
      if (!(num1 == 0.0M))
        return;
      qty = row.Qty;
      Decimal num3 = 0.0M;
      if (!(qty.GetValueOrDefault() == num3 & qty.HasValue))
        return;
    }
    if (!foundServiceOrderAllocation)
      throw new PXSetPropertyException("The lot or serial number is not available at the specified warehouse.");
    throw new PXSetPropertyException("The lot or serial number is already used in another appointment.");
  }

  public override void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.RowSelected(sender, e);
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
  }

  public virtual void GetLotSerialAvailability(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityInt(graphToQuery, apptLine, lotSerialNbr, ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  public static void GetLotSerialAvailabilityInt(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityStatic(graphToQuery, apptLine, lotSerialNbr, new int?(), ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  public virtual void GetLotSerialAvailability(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    int? splitLineNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityInt(graphToQuery, apptLine, lotSerialNbr, splitLineNbr, ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  public static void GetLotSerialAvailabilityInt(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    int? splitLineNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityStatic(graphToQuery, apptLine, lotSerialNbr, splitLineNbr, ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  public static void GetLotSerialAvailabilityStatic(
    PXGraph graphToQuery,
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    int? splitLineNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    lotSerialAvailQty = 0M;
    lotSerialUsedQty = 0M;
    foundServiceOrderAllocation = false;
    if (string.IsNullOrEmpty(lotSerialNbr) && !splitLineNbr.HasValue)
      return;
    if (!string.IsNullOrEmpty(lotSerialNbr))
      splitLineNbr = new int?();
    bool flag = true;
    FSSODetSplit fssoDetSplit = (FSSODetSplit) null;
    if (apptLine.SODetID.HasValue)
    {
      int? soDetId = apptLine.SODetID;
      int num = 0;
      if (soDetId.GetValueOrDefault() > num & soDetId.HasValue)
      {
        FSSODet fssoDet = FSSODet.UK.Find(graphToQuery, apptLine.SODetID);
        if (fssoDet != null)
        {
          BqlCommand bqlCommand1 = (BqlCommand) new Select<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.lineNbr, Equal<Required<FSSODetSplit.lineNbr>>>>>>();
          List<object> objectList = new List<object>();
          objectList.Add((object) fssoDet.SrvOrdType);
          objectList.Add((object) fssoDet.RefNbr);
          objectList.Add((object) fssoDet.LineNbr);
          BqlCommand bqlCommand2;
          if (!splitLineNbr.HasValue)
          {
            bqlCommand2 = bqlCommand1.WhereAnd(typeof (Where<FSSODetSplit.lotSerialNbr, Equal<Required<FSSODetSplit.lotSerialNbr>>>));
            objectList.Add((object) lotSerialNbr);
          }
          else
          {
            bqlCommand2 = bqlCommand1.WhereAnd(typeof (Where<FSSODetSplit.splitLineNbr, Equal<Required<FSSODetSplit.splitLineNbr>>>));
            objectList.Add((object) splitLineNbr);
          }
          fssoDetSplit = (FSSODetSplit) new PXView(graphToQuery, false, bqlCommand2).SelectSingle(objectList.ToArray());
          if (fssoDetSplit != null)
            flag = false;
        }
      }
    }
    if (flag && !string.IsNullOrEmpty(lotSerialNbr))
    {
      INLotSerialStatusByCostCenter statusByCostCenter = INLotSerialStatusByCostCenter.PK.Find(graphToQuery, apptLine.InventoryID, apptLine.SubItemID, apptLine.SiteID, apptLine.LocationID, lotSerialNbr, apptLine.CostCenterID);
      if (statusByCostCenter == null)
        return;
      lotSerialAvailQty = statusByCostCenter.QtyAvail.Value;
    }
    else
    {
      if (fssoDetSplit == null)
        return;
      lotSerialAvailQty = fssoDetSplit.Qty.Value;
      BqlCommand bqlCommand3 = (BqlCommand) new Select4<FSApptLineSplit, Where<FSApptLineSplit.origSrvOrdType, Equal<Required<FSApptLineSplit.origSrvOrdType>>, And<FSApptLineSplit.origSrvOrdNbr, Equal<Required<FSApptLineSplit.origSrvOrdNbr>>, And<FSApptLineSplit.origLineNbr, Equal<Required<FSApptLineSplit.origLineNbr>>>>>, Aggregate<Sum<FSApptLineSplit.qty>>>();
      List<object> objectList = new List<object>();
      objectList.Add((object) fssoDetSplit.SrvOrdType);
      objectList.Add((object) fssoDetSplit.RefNbr);
      objectList.Add((object) fssoDetSplit.LineNbr);
      BqlCommand bqlCommand4;
      if (!splitLineNbr.HasValue)
      {
        bqlCommand4 = bqlCommand3.WhereAnd(typeof (Where<FSApptLineSplit.lotSerialNbr, Equal<Required<FSApptLineSplit.lotSerialNbr>>>));
        objectList.Add((object) fssoDetSplit.LotSerialNbr);
      }
      else
      {
        bqlCommand4 = bqlCommand3.WhereAnd(typeof (Where<FSApptLineSplit.origSplitLineNbr, Equal<Required<FSApptLineSplit.origSplitLineNbr>>>));
        objectList.Add((object) fssoDetSplit.SplitLineNbr);
      }
      if (ignoreUseByApptLine)
      {
        bqlCommand4 = bqlCommand4.WhereAnd(typeof (Where<FSApptLineSplit.srvOrdType, NotEqual<Required<FSApptLineSplit.srvOrdType>>, Or<FSApptLineSplit.apptNbr, NotEqual<Required<FSApptLineSplit.apptNbr>>, Or<FSApptLineSplit.lineNbr, NotEqual<Required<FSApptLineSplit.lineNbr>>>>>));
        objectList.Add((object) apptLine.SrvOrdType);
        objectList.Add((object) apptLine.RefNbr);
        objectList.Add((object) apptLine.LineNbr);
      }
      FSApptLineSplit fsApptLineSplit = (FSApptLineSplit) new PXView(graphToQuery, false, bqlCommand4).SelectSingle(objectList.ToArray());
      Decimal? nullable = fsApptLineSplit != null ? fsApptLineSplit.Qty : new Decimal?(0M);
      lotSerialUsedQty = nullable.HasValue ? nullable.Value : 0M;
      foundServiceOrderAllocation = true;
    }
  }
}
