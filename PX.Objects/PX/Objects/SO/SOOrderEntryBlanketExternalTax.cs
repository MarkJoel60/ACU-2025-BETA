// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntryBlanketExternalTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderEntryBlanketExternalTax : PXGraphExtension<SOOrderEntryExternalTax, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();

  [PXOverride]
  public virtual IAddressLocation GetToAddress(
    SOOrder order,
    SOLine line,
    Func<SOOrder, SOLine, IAddressLocation> baseFunc)
  {
    if (line?.Behavior == "BL")
    {
      PXResult<PX.Objects.CR.Standalone.Location> pxResult = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXViewOf<PX.Objects.CR.Standalone.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Standalone.Location, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Address>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>>>>.And<BqlOperand<PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.defAddressID>>>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<BqlOperand<PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<PX.Objects.CR.Standalone.Location.cCarrierID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Standalone.Location.bAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
      {
        (object) line.CustomerID,
        (object) line.CustomerLocationID
      }));
      if (pxResult != null)
      {
        PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
        PX.Objects.CR.Address address = ((PXResult) pxResult).GetItem<PX.Objects.CR.Address>();
        PX.Objects.CS.Carrier carrier = ((PXResult) pxResult).GetItem<PX.Objects.CS.Carrier>();
        if (line.ShipVia != null && line.ShipVia != carrier.CarrierID)
          carrier = PXResultset<PX.Objects.CS.Carrier>.op_Implicit(PXSelectBase<PX.Objects.CS.Carrier, PXViewOf<PX.Objects.CS.Carrier>.BasedOn<SelectFromBase<PX.Objects.CS.Carrier, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CS.Carrier.carrierID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[1]
          {
            (object) line.ShipVia
          }));
        bool flag = true;
        bool? nullable;
        if (carrier != null && carrier.CarrierID != null)
        {
          nullable = carrier.IsCommonCarrier;
          if (nullable.GetValueOrDefault())
            flag = false;
        }
        if (flag && line.SiteID.HasValue)
        {
          nullable = line.POCreate;
          if (!nullable.GetValueOrDefault() || !(line.POSource == "D"))
            return this.Base1.GetFromAddress(order, line);
        }
        return !ExternalTaxBase<SOOrderEntry>.IsEmptyAddress((IAddressLocation) address) ? (IAddressLocation) address : throw new PXException("The external tax provider cannot calculate taxes for the lines with the {0} customer location in the Ship To Location column because the location address is not specified for this customer location on the General tab of the Customer Locations (AR303020) form.", new object[1]
        {
          (object) location.LocationCD
        });
      }
    }
    return baseFunc(order, line);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOOrder> e, PXRowSelected baseMethod)
  {
    baseMethod.Invoke(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOOrder>>) e).Args);
    if (e.Row == null || !(e.Row.Behavior == "BL"))
      return;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowInsert = false;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowUpdate = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOLine> e)
  {
    if (e.Row == null || !(e.Row.Behavior == "BL") || ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current == null || !this.Base1.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.TaxZoneID))
      return;
    PXUIFieldAttribute.SetEnabled<SOLine.taxZoneID>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOOrder> e)
  {
    if (e.Row == null || !(e.Row.Behavior == "BL") || !this.Base1.CalculateTaxesUsingExternalProvider(e.Row.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOOrder>>) e).Cache.ObjectsEqual<SOOrder.taxZoneID>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<SOLine.taxZoneID>((object) soLine, (object) e.Row.TaxZoneID);
      GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) soLine, true);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOLine> e)
  {
    if (((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current == null || !this.Base1.CalculateTaxesUsingExternalProvider(((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.TaxZoneID) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine>>) e).Cache.ObjectsEqual<SOLine.taxZoneID, SOLine.customerLocationID, SOLine.shipVia>((object) e.Row, (object) e.OldRow))
      return;
    this.Base1.InvalidateExternalTax(((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current);
  }
}
