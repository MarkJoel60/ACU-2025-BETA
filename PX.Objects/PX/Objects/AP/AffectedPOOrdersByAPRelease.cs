// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AffectedPOOrdersByAPRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class AffectedPOOrdersByAPRelease : 
  AffectedPOOrdersByPOLine<AffectedPOOrdersByAPRelease, APReleaseProcess>
{
  private PX.Objects.PO.POOrder[] _affectedOrders;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.distributionModule>();

  protected override bool EntityIsAffected(PX.Objects.PO.POOrder entity)
  {
    return !(entity.OrderType == "RS") && base.EntityIsAffected(entity);
  }

  [PXOverride]
  public override void Persist(System.Action basePersist)
  {
    this._affectedOrders = this.GetAffectedEntities().ToArray<PX.Objects.PO.POOrder>();
    basePersist();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AP.APReleaseProcess.PerformPersist(PX.Data.PXGraph.IPersistPerformer)" />
  /// and <see cref="M:PX.Objects.PO.GraphExtensions.APReleaseProcessExt.UpdatePOOnRelease.PerformPersist(PX.Data.PXGraph.IPersistPerformer)" />
  /// </summary>
  [PXOverride]
  public void PerformPersist(
    PXGraph.IPersistPerformer persister,
    System.Action<PXGraph.IPersistPerformer> base_PerformPersist)
  {
    base_PerformPersist(persister);
    if (this._affectedOrders == null)
      return;
    this.ProcessAffectedEntities((IEnumerable<PX.Objects.PO.POOrder>) this._affectedOrders);
    this._affectedOrders = (PX.Objects.PO.POOrder[]) null;
  }

  protected override PX.Objects.PO.POLine UpdateBlanketRow(
    PXCache cache,
    PX.Objects.PO.POLine normalRow,
    PX.Objects.PO.POLine normalOldRow,
    bool hardCheck)
  {
    PX.Objects.PO.POLine poLine = (PX.Objects.PO.POLine) null;
    if (!cache.ObjectsEqual<PX.Objects.PO.POLine.billedQty, PX.Objects.PO.POLine.curyBilledAmt>((object) normalRow, (object) normalOldRow))
    {
      poLine = this.UpdateBilledQty(cache, this.FindBlanketRow(cache, normalRow) ?? throw new PXArgumentException("blanketRow"), normalRow, normalOldRow);
      if (poLine != null)
      {
        poLine = (PX.Objects.PO.POLine) cache.Update((object) poLine);
        int num1 = hardCheck ? 1 : 0;
        int num2;
        if (!POLineType.IsService(poLine.LineType) || !(poLine.CompletePOLine == "Q"))
        {
          if (poLine.CompletePOLine == "A")
          {
            bool? closed = normalOldRow.Closed;
            bool flag = false;
            if (closed.GetValueOrDefault() == flag & closed.HasValue)
            {
              num2 = normalRow.Closed.GetValueOrDefault() ? 1 : 0;
              goto label_10;
            }
          }
          num2 = 0;
        }
        else
          num2 = 1;
label_10:
        hardCheck = (num1 | num2) != 0;
      }
    }
    return base.UpdateBlanketRow(cache, normalRow, normalOldRow, hardCheck) ?? poLine;
  }

  protected virtual PX.Objects.PO.POLine UpdateBilledQty(
    PXCache cache,
    PX.Objects.PO.POLine blanketRow,
    PX.Objects.PO.POLine normalRow,
    PX.Objects.PO.POLine normalOldRow)
  {
    PX.Objects.PO.POOrder poOrder1 = PXParentAttribute.SelectParent<PX.Objects.PO.POOrder>(cache, (object) normalRow);
    PX.Objects.PO.POOrder poOrder2 = PXParentAttribute.SelectParent<PX.Objects.PO.POOrder>(cache, (object) blanketRow);
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    Decimal? nullable5;
    if (!(normalRow.UOM == blanketRow.UOM))
    {
      Decimal num;
      if (blanketRow.InventoryID.HasValue)
      {
        PXCache sender = cache;
        int? inventoryId = blanketRow.InventoryID;
        string uom = blanketRow.UOM;
        nullable1 = normalRow.BaseBilledQty;
        nullable2 = normalOldRow.BaseBilledQty;
        Decimal valueOrDefault = (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        num = INUnitAttribute.ConvertFromBase(sender, inventoryId, uom, valueOrDefault, INPrecision.QUANTITY);
      }
      else
      {
        APReleaseProcess graph = this.Base;
        string uom1 = normalRow.UOM;
        string uom2 = blanketRow.UOM;
        nullable3 = normalRow.BaseBilledQty;
        nullable4 = normalOldRow.BaseBilledQty;
        Decimal valueOrDefault = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        num = INUnitAttribute.ConvertGlobal((PXGraph) graph, uom1, uom2, valueOrDefault, INPrecision.QUANTITY);
      }
      nullable5 = new Decimal?(num);
    }
    else
    {
      Decimal? billedQty1 = normalRow.BilledQty;
      Decimal? billedQty2 = normalOldRow.BilledQty;
      nullable5 = billedQty1.HasValue & billedQty2.HasValue ? new Decimal?(billedQty1.GetValueOrDefault() - billedQty2.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable6 = nullable5;
    PX.Objects.PO.POLine poLine1 = blanketRow;
    nullable1 = poLine1.BilledQty;
    nullable2 = nullable6;
    Decimal? nullable7;
    if (!(nullable1.HasValue & nullable2.HasValue))
    {
      nullable3 = new Decimal?();
      nullable7 = nullable3;
    }
    else
      nullable7 = new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault());
    poLine1.BilledQty = nullable7;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.Base.FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(blanketRow.CuryInfoID);
    if (poOrder1.CuryID == poOrder2.CuryID)
    {
      PX.Objects.PO.POLine poLine2 = blanketRow;
      nullable2 = poLine2.CuryBilledAmt;
      nullable1 = normalRow.CuryBilledAmt;
      nullable3 = normalOldRow.CuryBilledAmt;
      Decimal? nullable8;
      if (!(nullable1.HasValue & nullable3.HasValue))
      {
        nullable4 = new Decimal?();
        nullable8 = nullable4;
      }
      else
        nullable8 = new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault());
      nullable4 = nullable8;
      Decimal valueOrDefault = nullable4.GetValueOrDefault();
      Decimal? nullable9;
      if (!nullable2.HasValue)
      {
        nullable4 = new Decimal?();
        nullable9 = nullable4;
      }
      else
        nullable9 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault);
      poLine2.CuryBilledAmt = nullable9;
    }
    else
    {
      PX.Objects.PO.POLine poLine3 = blanketRow;
      nullable1 = poLine3.CuryBilledAmt;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
      nullable3 = normalRow.BilledAmt;
      nullable2 = normalOldRow.BilledAmt;
      Decimal? nullable10;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable4 = new Decimal?();
        nullable10 = nullable4;
      }
      else
        nullable10 = new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault());
      nullable4 = nullable10;
      Decimal valueOrDefault = nullable4.GetValueOrDefault();
      Decimal num = currencyInfo2.CuryConvCury(valueOrDefault);
      Decimal? nullable11;
      if (!nullable1.HasValue)
      {
        nullable4 = new Decimal?();
        nullable11 = nullable4;
      }
      else
        nullable11 = new Decimal?(nullable1.GetValueOrDefault() + num);
      poLine3.CuryBilledAmt = nullable11;
    }
    return blanketRow;
  }
}
