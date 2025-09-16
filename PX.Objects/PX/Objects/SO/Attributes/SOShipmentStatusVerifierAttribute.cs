// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.SOShipmentStatusVerifierAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class SOShipmentStatusVerifierAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber
{
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    SOShipment row = (SOShipment) e.Row;
    if (this.VerifyStatus(sender, row))
      return;
    this.LogError(sender, row);
    this.ThrowInvalidStatusException(sender, e, row);
  }

  protected virtual bool VerifyStatus(PXCache cache, SOShipment shipment)
  {
    switch (shipment.Status)
    {
      case "H":
        return this.VerifyHold(cache, shipment);
      case "N":
        return this.VerifyOpen(cache, shipment);
      case "F":
        return this.VerifyConfirmed(cache, shipment);
      case "Y":
        return this.VerifyPartiallyInvoiced(cache, shipment);
      case "I":
        return this.VerifyInvoiced(cache, shipment);
      case "C":
        return this.VerifyCompleted(cache, shipment);
      default:
        return this.VerifyUnknown(cache, shipment);
    }
  }

  protected virtual bool VerifyHold(PXCache cache, SOShipment shipment)
  {
    if (shipment.Hold.GetValueOrDefault() && !shipment.Confirmed.GetValueOrDefault())
    {
      int? nullable = shipment.BilledOrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = shipment.ReleasedOrderCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          return !shipment.Released.GetValueOrDefault();
      }
    }
    return false;
  }

  protected virtual bool VerifyOpen(PXCache cache, SOShipment shipment)
  {
    if (!shipment.Hold.GetValueOrDefault() && !shipment.Confirmed.GetValueOrDefault())
    {
      int? nullable = shipment.BilledOrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = shipment.ReleasedOrderCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          return !shipment.Released.GetValueOrDefault();
      }
    }
    return false;
  }

  protected virtual bool VerifyConfirmed(PXCache cache, SOShipment shipment)
  {
    if (!shipment.Hold.GetValueOrDefault() && shipment.Confirmed.GetValueOrDefault())
    {
      int? nullable = shipment.BilledOrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = shipment.ReleasedOrderCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        {
          nullable = shipment.UnbilledOrderCntr;
          int num3 = 0;
          if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
            return true;
          nullable = shipment.UnbilledOrderCntr;
          int num4 = 0;
          return nullable.GetValueOrDefault() == num4 & nullable.HasValue && !shipment.Released.GetValueOrDefault();
        }
      }
    }
    return false;
  }

  protected virtual bool VerifyPartiallyInvoiced(PXCache cache, SOShipment shipment)
  {
    if (!shipment.Hold.GetValueOrDefault() && shipment.Confirmed.GetValueOrDefault())
    {
      int? nullable = shipment.UnbilledOrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      {
        nullable = shipment.BilledOrderCntr;
        int num2 = 0;
        if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
          return true;
        nullable = shipment.ReleasedOrderCntr;
        int num3 = 0;
        return nullable.GetValueOrDefault() > num3 & nullable.HasValue;
      }
    }
    return false;
  }

  protected virtual bool VerifyInvoiced(PXCache cache, SOShipment shipment)
  {
    if (!shipment.Hold.GetValueOrDefault() && shipment.Confirmed.GetValueOrDefault())
    {
      int? nullable = shipment.UnbilledOrderCntr;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = shipment.BilledOrderCntr;
        int num2 = 0;
        return nullable.GetValueOrDefault() > num2 & nullable.HasValue;
      }
    }
    return false;
  }

  protected virtual bool VerifyCompleted(PXCache cache, SOShipment shipment)
  {
    int? nullable;
    int num1;
    if (!shipment.Hold.GetValueOrDefault() && shipment.Confirmed.GetValueOrDefault())
    {
      nullable = shipment.UnbilledOrderCntr;
      int num2 = 0;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      {
        nullable = shipment.BilledOrderCntr;
        int num3 = 0;
        num1 = nullable.GetValueOrDefault() == num3 & nullable.HasValue ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    if (num1 == 0)
      return false;
    nullable = shipment.ReleasedOrderCntr;
    int num4 = 0;
    return nullable.GetValueOrDefault() > num4 & nullable.HasValue || shipment.Released.GetValueOrDefault() || this.IsNonStockTransfer(cache, shipment);
  }

  protected virtual bool IsNonStockTransfer(PXCache cache, SOShipment shipment)
  {
    return PXParentAttribute.SelectChildren(cache.Graph.Caches[typeof (SOOrderShipment)], (object) shipment, typeof (SOShipment)).Cast<SOOrderShipment>().All<SOOrderShipment>((Func<SOOrderShipment, bool>) (s => !s.CreateINDoc.GetValueOrDefault()));
  }

  protected virtual bool VerifyUnknown(PXCache cache, SOShipment shipment) => true;

  protected virtual void LogError(PXCache sender, SOShipment shipment)
  {
    string fieldValues = this.GetFieldValues(sender, shipment, (IEnumerable<Type>) this.GetLogFieldList());
    PXTrace.WriteError("The {0} {1} has the following field values: {2}.", new object[3]
    {
      (object) shipment.ShipmentNbr,
      (object) sender.DisplayName,
      (object) fieldValues
    });
  }

  protected virtual List<Type> GetLogFieldList()
  {
    return new List<Type>()
    {
      typeof (SOShipment.status),
      typeof (SOShipment.hold),
      typeof (SOShipment.confirmed),
      typeof (SOShipment.billedOrderCntr),
      typeof (SOShipment.releasedOrderCntr),
      typeof (SOShipment.unbilledOrderCntr),
      typeof (SOShipment.released)
    };
  }

  protected virtual string GetFieldValues(
    PXCache cache,
    SOShipment shipment,
    IEnumerable<Type> field)
  {
    return string.Join(", ", field.Select<Type, string>((Func<Type, string>) (f => $"{f.Name}={cache.GetValue((object) shipment, f.Name)}")));
  }

  protected virtual void ThrowInvalidStatusException(
    PXCache sender,
    PXRowPersistingEventArgs e,
    SOShipment shipment)
  {
    throw new PXException("The {0} for the {1} {2} cannot be updated. Please contact support service.", new object[3]
    {
      (object) ((sender.GetStateExt(e.Row, this.FieldName) is PXFieldState stateExt ? stateExt.DisplayName : (string) null) ?? this.FieldName),
      (object) shipment.ShipmentNbr,
      (object) sender.DisplayName
    });
  }

  [PXLocalizable]
  public static class AttributeMessages
  {
    public const string InvalidShipmentStatus = "The {0} for the {1} {2} cannot be updated. Please contact support service.";
    public const string EntityFieldValues = "The {0} {1} has the following field values: {2}.";
  }
}
