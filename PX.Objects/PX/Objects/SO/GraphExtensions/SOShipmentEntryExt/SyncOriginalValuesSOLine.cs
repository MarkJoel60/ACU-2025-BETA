// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SyncOriginalValuesSOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SyncOriginalValuesSOLine : SyncOriginalValues<SOLine2>
{
  /// Overrides <see cref="M:PX.Objects.SO.SOShipmentEntry.UpdateOrigValues(PX.Objects.SO.SOShipLine,PX.Objects.SO.SOLine,System.Nullable{System.Decimal})" />
  [PXOverride]
  public virtual void UpdateOrigValues(
    SOShipLine shipline,
    PX.Objects.SO.SOLine soline,
    Decimal? baseOrigQty,
    Action<SOShipLine, PX.Objects.SO.SOLine, Decimal?> baseMethod)
  {
    baseMethod(shipline, soline, baseOrigQty);
    shipline.ShipComplete = soline.ShipComplete;
    shipline.CompleteQtyMin = soline.CompleteQtyMin;
    shipline.OrderUOM = soline.UOM;
    SOShipLine soShipLine1 = shipline;
    short? lineSign = soline.LineSign;
    Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? orderQty1 = soline.OrderQty;
    Decimal? nullable2 = nullable1.HasValue & orderQty1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * orderQty1.GetValueOrDefault()) : new Decimal?();
    soShipLine1.FullOrderQty = nullable2;
    SOShipLine soShipLine2 = shipline;
    lineSign = soline.LineSign;
    Decimal? nullable3 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    nullable1 = soline.BaseOrderQty;
    Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
    soShipLine2.BaseFullOrderQty = nullable4;
    SOShipLine soShipLine3 = shipline;
    lineSign = soline.LineSign;
    nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? orderQty2 = soline.OrderQty;
    Decimal? shippedQty = soline.ShippedQty;
    nullable3 = orderQty2.HasValue & shippedQty.HasValue ? new Decimal?(orderQty2.GetValueOrDefault() - shippedQty.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
    soShipLine3.FullOpenQty = nullable5;
    SOShipLine soShipLine4 = shipline;
    lineSign = soline.LineSign;
    nullable3 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? baseOrderQty = soline.BaseOrderQty;
    Decimal? baseShippedQty = soline.BaseShippedQty;
    nullable1 = baseOrderQty.HasValue & baseShippedQty.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() - baseShippedQty.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable6 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
    soShipLine4.BaseFullOpenQty = nullable6;
    shipline.BaseOrigOrderQty = baseOrigQty;
    SOShipLine soShipLine5 = shipline;
    Decimal? nullable7;
    if (shipline.UOM == shipline.OrderUOM)
    {
      nullable1 = baseOrigQty;
      nullable3 = shipline.BaseFullOrderQty;
      if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
      {
        nullable7 = shipline.FullOrderQty;
        goto label_4;
      }
    }
    nullable7 = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.Transactions).Cache, shipline.InventoryID, shipline.UOM, baseOrigQty.GetValueOrDefault(), INPrecision.QUANTITY));
label_4:
    soShipLine5.OrigOrderQty = nullable7;
  }

  protected override Decimal? GetOpenQty(SOLine2 line)
  {
    short? lineSign = line.LineSign;
    Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? orderQty = line.OrderQty;
    Decimal? openQty = line.ShippedQty;
    Decimal? nullable2 = orderQty.HasValue & openQty.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - openQty.GetValueOrDefault()) : new Decimal?();
    if (nullable1.HasValue & nullable2.HasValue)
      return new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault());
    openQty = new Decimal?();
    return openQty;
  }

  protected override Decimal? GetBaseOpenQty(SOLine2 line)
  {
    short? lineSign = line.LineSign;
    Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
    Decimal? baseOrderQty = line.BaseOrderQty;
    Decimal? baseOpenQty = line.BaseShippedQty;
    Decimal? nullable2 = baseOrderQty.HasValue & baseOpenQty.HasValue ? new Decimal?(baseOrderQty.GetValueOrDefault() - baseOpenQty.GetValueOrDefault()) : new Decimal?();
    if (nullable1.HasValue & nullable2.HasValue)
      return new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault());
    baseOpenQty = new Decimal?();
    return baseOpenQty;
  }

  protected override Decimal? GetOriginalBaseShippedQty(SOLine2 line)
  {
    return line.OriginalBaseShippedQty;
  }

  protected override Decimal? GetBaseShippedQty(SOLine2 line) => line.BaseShippedQty;

  protected override PXResultset<SOShipLine> SelectAffectedSOShipLines(SOLine2 row)
  {
    return PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>>.WithTablesOf<PX.Objects.SO.SOShipment, SOShipLine>, PX.Objects.SO.SOShipment, SOShipLine>.SameAsCurrent>, And<BqlOperand<SOShipLine.origOrderType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<SOShipLine.origOrderNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<SOShipLine.origLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) row.OrderType,
      (object) row.OrderNbr,
      (object) row.LineNbr
    });
  }
}
