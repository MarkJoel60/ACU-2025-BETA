// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOShipmentEntryExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.SOShipmentEntryExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<SOShipmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<Current2<PX.Objects.SO.SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<PX.Objects.IN.INSite.baseCuryID, Equal<Current2<PX.Objects.AR.Customer.baseCuryID>>, Or<Current2<PX.Objects.AR.Customer.baseCuryID>, IsNull>>>), "The {0} branch specified for the {1} warehouse is restricted in the {2} account.", new Type[] {typeof (PX.Objects.IN.INSite.branchID), typeof (PX.Objects.IN.INSite.siteCD), typeof (Current<PX.Objects.AR.Customer.acctCD>)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOShipment.siteID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.baseCuryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.baseCuryID>>) e).Cancel = true;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.baseCuryID>, PX.Objects.CM.CurrencyInfo, object>) e).NewValue = (object) this.GetDefaultCuryID(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>>) e).Cancel = true;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo, PX.Objects.CM.CurrencyInfo.curyID>, PX.Objects.CM.CurrencyInfo, object>) e).NewValue = (object) this.GetDefaultCuryID(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
  }

  protected virtual string GetDefaultCuryID(PX.Objects.SO.SOShipment shipment)
  {
    return PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, (int?) shipment?.SiteID)?.BaseCuryID ?? ((PXGraph) this.Base).Accessinfo.BaseCuryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment.siteID> e)
  {
    this.Base.RedefaultCurrencyInfo(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment.siteID>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment.siteID>>) e).Args);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOShipment> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOShipment>>) e).Cache.VerifyFieldAndRaiseException<PX.Objects.SO.SOShipment.siteID>((object) e.Row);
  }
}
