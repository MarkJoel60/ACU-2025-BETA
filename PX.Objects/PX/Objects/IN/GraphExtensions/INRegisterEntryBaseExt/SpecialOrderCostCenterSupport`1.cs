// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt.SpecialOrderCostCenterSupport`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;

public abstract class SpecialOrderCostCenterSupport<T> : 
  SpecialOrderCostCenterSupport<T, INTran>,
  IINTranCostCenterSupport,
  ICostCenterSupport<INTran>
  where T : INRegisterEntryBase
{
  public bool IsSupported(string inventorySource) => inventorySource == "S";

  public bool IsSupported(INTran tran) => tran.IsSpecialOrder.GetValueOrDefault();

  public string GetInventorySource(INTran tran) => !this.IsSpecificCostCenter(tran) ? "F" : "S";

  public string GetDestinationInventorySource(INTran tran)
  {
    return !this.IsDestinationSpecificCostCenter(tran) ? "F" : "S";
  }

  public override IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (INTran.isSpecialOrder);
    yield return typeof (INTran.siteID);
    yield return typeof (INTran.sOOrderType);
    yield return typeof (INTran.sOOrderNbr);
    yield return typeof (INTran.sOOrderLineNbr);
  }

  public override bool IsSpecificCostCenter(INTran tran)
  {
    return tran.IsSpecialOrder.GetValueOrDefault() && tran.SiteID.HasValue && !string.IsNullOrEmpty(tran.SOOrderType) && !string.IsNullOrEmpty(tran.SOOrderNbr) && tran.SOOrderLineNbr.HasValue;
  }

  protected override SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys GetCostCenterKeys(
    INTran line)
  {
    if (line.DocType == "I")
    {
      short? invtMult = line.InvtMult;
      if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == 1)
      {
        PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) (object) this.Base, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
        if (string.IsNullOrEmpty(soLine?.OrigOrderType) || string.IsNullOrEmpty(soLine.OrigOrderNbr) || !soLine.OrigLineNbr.HasValue)
          throw new PXInvalidOperationException();
        return new SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys()
        {
          SiteID = line.SiteID,
          OrderType = soLine.OrigOrderType,
          OrderNbr = soLine.OrigOrderNbr,
          LineNbr = soLine.OrigLineNbr
        };
      }
    }
    if (line.DocType == nameof (T) && line.OrigModule == "SO")
    {
      PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) (object) this.Base, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
      if (string.IsNullOrEmpty(soLine?.OrigOrderType) || string.IsNullOrEmpty(soLine.OrigOrderNbr) || !soLine.OrigLineNbr.HasValue)
        throw new PXInvalidOperationException();
      return new SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys()
      {
        SiteID = line.SiteID,
        OrderType = soLine.OrigOrderType,
        OrderNbr = soLine.OrigOrderNbr,
        LineNbr = soLine.OrigLineNbr
      };
    }
    return new SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys()
    {
      SiteID = line.SiteID,
      OrderType = line.SOOrderType,
      OrderNbr = line.SOOrderNbr,
      LineNbr = line.SOOrderLineNbr
    };
  }

  public virtual IEnumerable<Type> GetDestinationFieldsDependOn()
  {
    yield return typeof (INTran.isSpecialOrder);
    yield return typeof (INTran.toSiteID);
    yield return typeof (INTran.sOOrderType);
    yield return typeof (INTran.sOOrderNbr);
    yield return typeof (INTran.sOOrderLineNbr);
  }

  public virtual bool IsDestinationSpecificCostCenter(INTran tran)
  {
    return tran.IsSpecialOrder.GetValueOrDefault() && tran.ToSiteID.HasValue && !string.IsNullOrEmpty(tran.SOOrderType) && !string.IsNullOrEmpty(tran.SOOrderNbr) && tran.SOOrderLineNbr.HasValue;
  }

  protected virtual SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys GetDestinationCostCenterKeys(
    INTran line)
  {
    if (line.DocType == nameof (T) && line.OrigModule == "SO")
    {
      PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) (object) this.Base, line.SOOrderType, line.SOOrderNbr, line.SOOrderLineNbr);
      if (string.IsNullOrEmpty(soLine?.OrigOrderType) || string.IsNullOrEmpty(soLine.OrigOrderNbr) || !soLine.OrigLineNbr.HasValue)
        throw new PXInvalidOperationException();
      return new SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys()
      {
        SiteID = line.ToSiteID,
        OrderType = soLine.OrigOrderType,
        OrderNbr = soLine.OrigOrderNbr,
        LineNbr = soLine.OrigLineNbr
      };
    }
    return new SpecialOrderCostCenterSupport<T, INTran>.CostCenterKeys()
    {
      SiteID = line.ToSiteID,
      OrderType = line.SOOrderType,
      OrderNbr = line.SOOrderNbr,
      LineNbr = line.SOOrderLineNbr
    };
  }

  public virtual INCostCenter GetDestinationCostCenter(INTran tran)
  {
    return this.FindOrCreateCostCenter(this.GetDestinationCostCenterKeys(tran));
  }

  public virtual void OnInventorySourceChanged(
    INTran tran,
    string newInventorySource,
    bool isExternalCall)
  {
    PXCache<INTran> pxCache = GraphHelper.Caches<INTran>((PXGraph) (object) this.Base);
    ((PXCache) pxCache).SetValueExt<INTran.isSpecialOrder>((object) tran, (object) false);
    ((PXCache) pxCache).SetValueExt<INTran.specialOrderCostCenterID>((object) tran, (object) null);
    ((PXCache) pxCache).SetValueExt<INTran.sOOrderType>((object) tran, (object) null);
    ((PXCache) pxCache).SetValueExt<INTran.sOOrderNbr>((object) tran, (object) null);
    ((PXCache) pxCache).SetValueExt<INTran.sOOrderLineNbr>((object) tran, (object) null);
  }

  public virtual void OnDestinationInventorySourceChanged(
    INTran tran,
    string newInventorySource,
    bool isExternalCall)
  {
    ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.toSpecialOrderCostCenterID>((object) tran, (object) null);
  }

  public virtual void ValidateForPersisting(INTran tran)
  {
    if (!this.IsSpecificCostCenter(tran))
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.specialOrderCostCenterID>((object) tran, (object) null, (Exception) new PXSetPropertyException("The Special Order Nbr. column cannot be empty.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("specialOrderCostCenterID", (object) tran.SpecialOrderCostCenterID, "The Special Order Nbr. column cannot be empty.");
    }
  }

  public virtual void ValidateDestinationForPersisting(INTran tran)
  {
    if (!this.IsDestinationSpecificCostCenter(tran))
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.toSpecialOrderCostCenterID>((object) tran, (object) null, (Exception) new PXSetPropertyException("The To Special Order Nbr. column cannot be empty.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("toSpecialOrderCostCenterID", (object) tran.ToSpecialOrderCostCenterID, "The To Special Order Nbr. column cannot be empty.");
    }
  }

  [PXOverride]
  public virtual (bool? Project, bool? Task) IsProjectTaskEnabled(
    INTran row,
    Func<INTran, (bool? Project, bool? Task)> baseMethod)
  {
    (bool?, bool?) valueTuple = baseMethod(row);
    return (new bool?((valueTuple.Item1 ?? true) && row.CostLayerType != "S"), new bool?((valueTuple.Item2 ?? true) && row.CostLayerType != "S"));
  }

  protected virtual void _(PX.Data.Events.RowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INTran.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row, e.Row.CostLayerType != "S");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID> e)
  {
    if (object.Equals(((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>, INTran, object>) e).OldValue, (object) e.Row.SpecialOrderCostCenterID))
      return;
    PX.Objects.IN.INRegister current = this.Base.INRegisterDataMember.Current;
    if ((current != null ? (!EnumerableExtensions.IsIn<string>(current.OrigModule, "IN", "PI") ? 1 : 0) : 1) != 0)
      return;
    bool flag = this.Base.INRegisterDataMember.Current.OrigModule == "PI";
    if (!e.Row.SpecialOrderCostCenterID.HasValue)
    {
      if (!flag)
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache.SetDefaultExt<INTran.uOM>((object) e.Row);
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache.SetDefaultExt<INTran.unitCost>((object) e.Row);
    }
    else
    {
      INCostCenter inCostCenter = INCostCenter.PK.Find((PXGraph) (object) this.Base, e.Row.SpecialOrderCostCenterID);
      if (inCostCenter == null)
        throw new RowNotFoundException((PXCache) GraphHelper.Caches<INCostCenter>((PXGraph) (object) this.Base), new object[1]
        {
          (object) e.Row.SpecialOrderCostCenterID
        });
      PX.Objects.SO.SOLine soLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) (object) this.Base, inCostCenter.SOOrderType, inCostCenter.SOOrderNbr, inCostCenter.SOOrderLineNbr);
      if (soLine == null)
        throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) (object) this.Base), new object[3]
        {
          (object) inCostCenter?.SOOrderType,
          (object) inCostCenter?.SOOrderNbr,
          (object) (int?) inCostCenter?.SOOrderLineNbr
        });
      if (flag)
      {
        Decimal? nullable = new Decimal?(INUnitAttribute.ConvertFromTo<INTran.inventoryID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache, (object) e.Row, e.Row.UOM, soLine.UOM, soLine.UnitCost.Value, INPrecision.UNITCOST));
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache.SetValueExt<INTran.unitCost>((object) e.Row, (object) nullable);
      }
      else
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache.SetValueExt<INTran.uOM>((object) e.Row, (object) soLine.UOM);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INTran, INTran.specialOrderCostCenterID>>) e).Cache.SetValueExt<INTran.unitCost>((object) e.Row, (object) soLine.UnitCost);
      }
    }
  }
}
