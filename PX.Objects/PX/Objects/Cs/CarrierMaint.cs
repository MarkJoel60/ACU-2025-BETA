// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class CarrierMaint : PXGraph<CarrierMaint, PX.Objects.CS.Carrier>
{
  public PXSelect<PX.Objects.CS.Carrier> Carrier;
  public PXSelect<PX.Objects.CS.Carrier, Where<PX.Objects.CS.Carrier.carrierID, Equal<Current<PX.Objects.CS.Carrier.carrierID>>>> CarrierCurrent;
  public PXSelect<FreightRate, Where<FreightRate.carrierID, Equal<Current<PX.Objects.CS.Carrier.carrierID>>>> FreightRates;
  public PXSelectJoin<CarrierPackage, InnerJoin<CSBox, On<CSBox.boxID, Equal<CarrierPackage.boxID>>, CrossJoin<CommonSetup>>, Where<CarrierPackage.carrierID, Equal<Current<PX.Objects.CS.Carrier.carrierID>>>> CarrierPackages;
  public PXSelect<CSBox, Where<CSBox.activeByDefault, Equal<boolTrue>>> DefaultBoxes;

  protected virtual void FreightRate_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FreightRate row = (FreightRate) e.Row;
    Decimal? weight = row.Weight;
    Decimal num1 = 0M;
    if (weight.GetValueOrDefault() < num1 & weight.HasValue)
    {
      if (sender.RaiseExceptionHandling<FreightRate.weight>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) "[weight]"
      })))
        throw new PXRowPersistingException(typeof (FreightRate.weight).Name, (object) null, "'{0}' should not be negative.", new object[1]
        {
          (object) "weight"
        });
      ((CancelEventArgs) e).Cancel = true;
    }
    Decimal? nullable = row.Volume;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
    {
      if (sender.RaiseExceptionHandling<FreightRate.volume>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) "[volume]"
      })))
        throw new PXRowPersistingException(typeof (FreightRate.volume).Name, (object) null, "'{0}' should not be negative.", new object[1]
        {
          (object) "volume"
        });
      ((CancelEventArgs) e).Cancel = true;
    }
    nullable = row.Rate;
    Decimal num3 = 0M;
    if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
      return;
    if (sender.RaiseExceptionHandling<FreightRate.rate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
    {
      (object) "[rate]"
    })))
      throw new PXRowPersistingException(typeof (FreightRate.rate).Name, (object) null, "'{0}' should not be negative.", new object[1]
      {
        (object) "rate"
      });
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Carrier_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CS.Carrier row))
      return;
    foreach (PXResult<CSBox> pxResult in ((PXSelectBase<CSBox>) this.DefaultBoxes).Select(Array.Empty<object>()))
    {
      CSBox csBox = PXResult<CSBox>.op_Implicit(pxResult);
      ((PXSelectBase<CarrierPackage>) this.CarrierPackages).Insert(new CarrierPackage()
      {
        CarrierID = row.CarrierID,
        BoxID = csBox.BoxID
      });
    }
    ((PXSelectBase) this.CarrierPackages).Cache.IsDirty = false;
  }

  protected virtual void Carrier_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PX.Objects.CS.Carrier row = (PX.Objects.CS.Carrier) e.Row;
    Decimal? baseRate = row.BaseRate;
    Decimal num = 0M;
    if (baseRate.GetValueOrDefault() < num & baseRate.HasValue)
    {
      if (sender.RaiseExceptionHandling<PX.Objects.CS.Carrier.baseRate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) "[baseRate]"
      })))
        throw new PXRowPersistingException(typeof (PX.Objects.CS.Carrier.baseRate).Name, (object) null, "'{0}' should not be negative.", new object[1]
        {
          (object) "baseRate"
        });
      ((CancelEventArgs) e).Cancel = true;
    }
    bool? nullable = row.IsExternal;
    if (nullable.GetValueOrDefault())
    {
      if (string.IsNullOrEmpty(row.CarrierPluginID))
      {
        if (sender.RaiseExceptionHandling<PX.Objects.CS.Carrier.carrierPluginID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[carrierPluginID]"
        })))
          throw new PXRowPersistingException(typeof (PX.Objects.CS.Carrier.carrierPluginID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "carrierPluginID"
          });
        ((CancelEventArgs) e).Cancel = true;
      }
      if (string.IsNullOrEmpty(row.PluginMethod))
      {
        if (sender.RaiseExceptionHandling<PX.Objects.CS.Carrier.pluginMethod>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[pluginMethod]"
        })))
          throw new PXRowPersistingException(typeof (PX.Objects.CS.Carrier.pluginMethod).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "pluginMethod"
          });
        ((CancelEventArgs) e).Cancel = true;
      }
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.advancedFulfillment>())
      return;
    nullable = row.IsExternalShippingApplication;
    if (!nullable.GetValueOrDefault() || !string.IsNullOrEmpty(row.ShippingApplicationType))
      return;
    if (sender.RaiseExceptionHandling<PX.Objects.CS.Carrier.shippingApplicationType>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[shippingApplicationType]"
    })))
      throw new PXRowPersistingException(typeof (PX.Objects.CS.Carrier.shippingApplicationType).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) "shippingApplicationType"
      });
  }

  protected virtual void Carrier_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CS.Carrier row))
      return;
    bool? nullable;
    if (row.CarrierPluginID != null)
    {
      CarrierPlugin carrierPlugin = (CarrierPlugin) PXSelectorAttribute.Select<CarrierPlugin.carrierPluginID>(sender, (object) row);
      string str = (string) null;
      if ((!string.IsNullOrEmpty(carrierPlugin?.PluginTypeName) ? (CarrierPluginMaint.IsValidType(carrierPlugin.PluginTypeName) ? 1 : 0) : 0) == 0)
        str = PXMessages.LocalizeFormatNoPrefixNLA("The {0} carrier references a missing plug-in.", new object[1]
        {
          (object) row?.CarrierPluginID
        });
      else if (carrierPlugin != null)
      {
        nullable = carrierPlugin.IsActive;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          str = "The carrier is not active.";
      }
      PXUIFieldAttribute.SetWarning<PX.Objects.CS.Carrier.carrierPluginID>(sender, (object) row, str);
      PXCache pxCache = sender;
      PX.Objects.CS.Carrier carrier = row;
      nullable = row.IsExternal;
      int num;
      if (!nullable.HasValue || nullable.GetValueOrDefault())
      {
        nullable = row.IsExternal;
        if (nullable.HasValue && nullable.GetValueOrDefault())
        {
          nullable = (bool?) carrierPlugin?.IsActive;
          num = !nullable.HasValue ? 0 : (nullable.GetValueOrDefault() ? 1 : 0);
        }
        else
          num = 0;
      }
      else
        num = 1;
      PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.isActive>(pxCache, (object) carrier, num != 0);
    }
    PXCache pxCache1 = sender;
    PX.Objects.CS.Carrier carrier1 = row;
    nullable = row.IsExternal;
    int num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.calcMethod>(pxCache1, (object) carrier1, num1 != 0);
    PXCache pxCache2 = sender;
    PX.Objects.CS.Carrier carrier2 = row;
    nullable = row.IsExternal;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.baseRate>(pxCache2, (object) carrier2, num2 != 0);
    PXCache pxCache3 = sender;
    PX.Objects.CS.Carrier carrier3 = row;
    nullable = row.IsExternal;
    int num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.calcFreightOnReturn>(pxCache3, (object) carrier3, num3 != 0);
    PXCache pxCache4 = sender;
    PX.Objects.CS.Carrier carrier4 = row;
    nullable = row.IsExternal;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.carrierPluginID>(pxCache4, (object) carrier4, num4 != 0);
    PXCache pxCache5 = sender;
    PX.Objects.CS.Carrier carrier5 = row;
    nullable = row.IsExternal;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.pluginMethod>(pxCache5, (object) carrier5, num5 != 0);
    PXCache pxCache6 = sender;
    PX.Objects.CS.Carrier carrier6 = row;
    nullable = row.IsExternal;
    int num6 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.confirmationRequired>(pxCache6, (object) carrier6, num6 != 0);
    PXCache pxCache7 = sender;
    PX.Objects.CS.Carrier carrier7 = row;
    nullable = row.IsExternal;
    int num7 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.packageRequired>(pxCache7, (object) carrier7, num7 != 0);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.baseRate>(sender, (object) row, row.CalcMethod != "M");
    if (!PXAccess.FeatureInstalled<FeaturesSet.advancedFulfillment>())
      return;
    PXCache pxCache8 = sender;
    PX.Objects.CS.Carrier carrier8 = row;
    nullable = row.IsExternalShippingApplication;
    bool flag1 = false;
    int num8 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.isExternal>(pxCache8, (object) carrier8, num8 != 0);
    PXCache pxCache9 = sender;
    PX.Objects.CS.Carrier carrier9 = row;
    nullable = row.IsExternal;
    int num9 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<PX.Objects.CS.Carrier.returnLabel>(pxCache9, (object) carrier9, num9 != 0);
    PXCache pxCache10 = sender;
    PX.Objects.CS.Carrier carrier10 = row;
    nullable = row.IsExternal;
    bool flag2 = false;
    int num10 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.isExternalShippingApplication>(pxCache10, (object) carrier10, num10 != 0);
    PXCache pxCache11 = sender;
    PX.Objects.CS.Carrier carrier11 = row;
    nullable = row.IsExternal;
    bool flag3 = false;
    int num11 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.shippingApplicationType>(pxCache11, (object) carrier11, num11 != 0);
    PXCache pxCache12 = sender;
    PX.Objects.CS.Carrier carrier12 = row;
    nullable = row.IsExternalShippingApplication;
    int num12 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CS.Carrier.shippingApplicationType>(pxCache12, (object) carrier12, num12 != 0);
  }

  protected virtual void Carrier_CarrierPluginID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CS.Carrier row))
      return;
    row.PluginMethod = (string) null;
  }

  protected virtual void Carrier_CalcMethod_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CS.Carrier row = e.Row as PX.Objects.CS.Carrier;
    if (!(row.CalcMethod == "M"))
      return;
    row.BaseRate = new Decimal?(0M);
  }

  protected virtual void Carrier_IsExternal_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CS.Carrier row))
      return;
    sender.SetDefaultExt<PX.Objects.CS.Carrier.calcFreightOnReturn>((object) row);
    if (!row.IsExternal.GetValueOrDefault())
      return;
    sender.SetDefaultExt<PX.Objects.CS.Carrier.calcMethod>((object) row);
  }

  public static ICarrierService CreateCarrierService(PXGraph graph, string carrierID)
  {
    if (string.IsNullOrEmpty(carrierID))
      throw new ArgumentNullException();
    ICarrierService carrierService = (ICarrierService) null;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find(graph, carrierID);
    if (carrier != null && carrier.IsExternal.GetValueOrDefault() && !string.IsNullOrEmpty(carrier.CarrierPluginID))
    {
      carrierService = CarrierPluginMaint.CreateCarrierService(graph, carrier.CarrierPluginID, true).Result;
      carrierService.Method = carrier.PluginMethod;
    }
    return carrierService;
  }
}
