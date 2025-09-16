// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOShipmentEntry
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOShipmentEntry : PXGraphExtension<CreateShipmentExtension, SOShipmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension.CreateShipment(PX.Objects.SO.CreateShipmentArgs)" />.
  /// </summary>
  [PXOverride]
  public virtual void CreateShipment(
    CreateShipmentArgs args,
    SM_SOShipmentEntry.CreateShipmentDelegate del)
  {
    this.ValidatePostBatchStatus((PXDBOperation) 1, "SO", args.Order.OrderType, args.Order.OrderNbr);
    del(args);
  }

  [PXOverride]
  public virtual void ResortStockForShipment(
    SOShipLine newline,
    List<PXResult> resultset,
    SM_SOShipmentEntry.ResortStockForShipmentDelegate del)
  {
    del(newline, resultset);
    int? locationID = this.GetLocationFromServiceDocument(newline, out string _);
    if (!locationID.HasValue)
      return;
    List<PXResult> list = resultset.OrderByDescending<PXResult, bool>((Func<PXResult, bool>) (r =>
    {
      int? locationId = PXResult.Unwrap<INLocation>((object) r).LocationID;
      int? nullable = locationID;
      return locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue;
    })).ToList<PXResult>();
    resultset.Clear();
    resultset.AddRange((IEnumerable<PXResult>) list);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<PX.Objects.SO.SOShipment> e)
  {
    if (e.Row == null)
      return;
    FSxSOShipment extension = PXCacheEx.GetExtension<FSxSOShipment>((IBqlTable) e.Row);
    using (new PXConnectionScope())
    {
      FSxSOShipment fsxSoShipment = extension;
      PXResultset<PX.Objects.SO.SOOrderType> pxResultset = PXSelectBase<PX.Objects.SO.SOOrderType, PXSelectJoin<PX.Objects.SO.SOOrderType, InnerJoin<PX.Objects.SO.SOOrderShipment, On<PX.Objects.SO.SOOrderType.orderType, Equal<PX.Objects.SO.SOOrderShipment.orderType>>>, Where<PX.Objects.SO.SOOrderShipment.shipmentType, Equal<Required<PX.Objects.SO.SOShipment.shipmentType>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<Required<PX.Objects.SO.SOShipment.shipmentNbr>>, And<FSxSOOrderType.enableFSIntegration, Equal<True>>>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, 0, 1, new object[2]
      {
        (object) e.Row.ShipmentType,
        (object) e.Row.ShipmentNbr
      });
      bool? nullable = pxResultset != null ? new bool?(GraphHelper.RowCast<PX.Objects.SO.SOOrderType>((IEnumerable) pxResultset).Any<PX.Objects.SO.SOOrderType>()) : new bool?();
      fsxSoShipment.IsFSRelated = nullable;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOShipLine> e)
  {
    if (e.Row == null)
      return;
    this.ShowOrHideWarningForLocation(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOShipLine>>) e).Cache, e.Row);
  }

  private void ShowOrHideWarningForLocation(PXCache cache, SOShipLine line)
  {
    FSxSOShipLine extension = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.GetExtension<FSxSOShipLine>((object) line);
    if (extension == null)
      return;
    bool? locationDifferent = extension.IsLocationDifferent;
    if (!locationDifferent.HasValue)
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, line.InventoryID);
      string serviceDocRefNbr;
      int? fromServiceDocument = this.GetLocationFromServiceDocument(line, out serviceDocRefNbr);
      FSxSOShipLine fsxSoShipLine = extension;
      int num;
      if (fromServiceDocument.HasValue)
      {
        int? nullable = fromServiceDocument;
        int? locationId = line.LocationID;
        num = !(nullable.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable.HasValue == locationId.HasValue) ? 1 : 0;
      }
      else
        num = 0;
      bool? nullable1 = new bool?(num != 0);
      fsxSoShipLine.IsLocationDifferent = nullable1;
      extension.ServiceDocRefNbr = serviceDocRefNbr;
      extension.InventoryCD = inventoryItem.InventoryCD;
    }
    string str1;
    if (extension.ServiceDocRefNbr != null)
      str1 = PXLocalizer.LocalizeFormat("The location of the {0} item has been modified and no longer matches the location in the originating service document. These changes will not affect the originating {1} service document.", new object[2]
      {
        (object) extension.InventoryCD.Trim(),
        (object) extension.ServiceDocRefNbr
      });
    else
      str1 = (string) null;
    string str2 = str1;
    string warning = PXUIFieldAttribute.GetWarning<SOShipLine.locationID>(cache, (object) line);
    locationDifferent = extension.IsLocationDifferent;
    if (locationDifferent.GetValueOrDefault())
    {
      if (!string.IsNullOrEmpty(warning))
        return;
      ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.RaiseExceptionHandling<SOShipLine.locationID>((object) line, (object) line.LocationID, (Exception) new PXSetPropertyException((IBqlTable) line, str2, (PXErrorLevel) 3));
    }
    else
    {
      if (string.IsNullOrEmpty(warning) || !(warning == str2))
        return;
      ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.RaiseExceptionHandling<SOShipLine.locationID>((object) line, (object) line.LocationID, (Exception) null);
    }
  }

  private int? GetLocationFromServiceDocument(SOShipLine shipLine, out string serviceDocRefNbr)
  {
    int? fromServiceDocument = new int?();
    serviceDocRefNbr = (string) null;
    FSxSOLine extension = ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Caches[typeof (PX.Objects.SO.SOLine)].GetExtension<FSxSOLine>((object) PX.Objects.SO.SOLine.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shipLine.OrigOrderType, shipLine.OrigOrderNbr, shipLine.OrigLineNbr));
    if (extension != null && extension.AppointmentRefNbr != null)
    {
      fromServiceDocument = (int?) FSAppointmentDet.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, extension.SrvOrdType, extension.AppointmentRefNbr, extension.AppointmentLineNbr)?.LocationID;
      serviceDocRefNbr = extension.AppointmentRefNbr;
    }
    else if (extension != null && extension.ServiceOrderRefNbr != null)
    {
      fromServiceDocument = (int?) FSSODet.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, extension.SrvOrdType, extension.ServiceOrderRefNbr, extension.ServiceOrderLineNbr)?.LocationID;
      serviceDocRefNbr = extension.ServiceOrderRefNbr;
    }
    return fromServiceDocument;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<SOShipLine.locationID> e)
  {
    if (e.Row == null)
      return;
    this.ClearLocationFlag(e.Row);
  }

  private void ClearLocationFlag(object line)
  {
    FSxSOShipLine extension = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.GetExtension<FSxSOShipLine>(line);
    if (extension == null)
      return;
    extension.IsLocationDifferent = new bool?();
  }

  public virtual void ValidatePostBatchStatus(
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
  {
    DocGenerationHelper.ValidatePostBatchStatus<PX.Objects.SO.SOShipment>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, dbOperation, postTo, createdDocType, createdRefNbr);
  }

  public delegate void CreateShipmentDelegate(CreateShipmentArgs args);

  public delegate void ResortStockForShipmentDelegate(SOShipLine newline, List<PXResult> resultset);
}
