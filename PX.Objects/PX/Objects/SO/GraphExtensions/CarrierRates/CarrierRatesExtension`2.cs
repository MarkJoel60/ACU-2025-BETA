// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.CarrierRates.CarrierRatesExtension`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SM;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.CarrierRates;

public abstract class CarrierRatesExtension<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public PXSelectExtension<Document> Documents;
  public PXSelectExtension<DocumentPackage> DocumentPackages;
  public PXSelect<PX.Objects.CS.Carrier, Where<PX.Objects.CS.Carrier.isExternal, Equal<True>, And<PX.Objects.CS.Carrier.carrierPluginID, IsNotNull, And<PX.Objects.CS.Carrier.pluginMethod, IsNotNull>>>> PlugIns;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<SOCarrierRate, Where<SOCarrierRate.method, IsNotNull>, OrderBy<Asc<SOCarrierRate.deliveryDate, Asc<SOCarrierRate.amount>>>> CarrierRates;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<ARSetup> arsetup;
  public PXSelect<PX.Objects.CM.CurrencyInfo> CarrierRatesDummyCuryInfo;
  public PXAction<TPrimary> shopRates;
  public PXAction<TPrimary> refreshRates;
  public PXAction<TPrimary> recalculatePackages;
  public PXAction<TPrimary> captureWeightFromScale;

  public virtual IEnumerable carrierRates()
  {
    return ((PXSelectBase<Document>) this.Documents).Current == null ? (IEnumerable) Array.Empty<SOCarrierRate>() : ((PXSelectBase) this.CarrierRates).Cache.Cached;
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable ShopRates(PXAdapter adapter)
  {
    if (((PXSelectBase<Document>) this.Documents).Current != null)
    {
      if (this.AskForRateSelection() == 1)
      {
        foreach (SOCarrierRate cr in ((PXSelectBase) this.CarrierRates).Cache.Cached)
        {
          if (cr.Selected.GetValueOrDefault())
          {
            if (cr.Method != null)
            {
              try
              {
                ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipViaUpdateFromShopForRate>(((PXSelectBase<Document>) this.Documents).Current, (object) true);
                ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipVia>(((PXSelectBase<Document>) this.Documents).Current, (object) cr.Method);
              }
              finally
              {
                ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipViaUpdateFromShopForRate>(((PXSelectBase<Document>) this.Documents).Current, (object) false);
              }
              if (!((PXSelectBase<Document>) this.Documents).Current.FreightCostIsValid.GetValueOrDefault())
                this.CalculateFreightCost(((PXSelectBase<Document>) this.Documents).Current);
              else
                this.RateHasBeenSelected(cr);
            }
          }
        }
      }
      ((PXSelectBase) this.CarrierRates).Cache.Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable RefreshRates(PXAdapter adapter)
  {
    this.UpdateRates();
    return adapter.Get();
  }

  public virtual void UpdateRates()
  {
    if (((PXSelectBase<Document>) this.Documents).Current == null)
      return;
    ((PXSelectBase) this.CarrierRates).Cache.Clear();
    this.ValidatePackages();
    List<CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo> source = new List<CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo>();
    foreach (CarrierPlugin applicableCarrierPlugin in this.GetApplicableCarrierPlugins())
    {
      CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService((PXGraph) this.Base, applicableCarrierPlugin);
      if (carrierService.IsSuccess)
      {
        List<string> messages = new List<string>();
        CarrierRequest carrierRequest = (CarrierRequest) null;
        try
        {
          carrierRequest = this.BuildQuoteRequest(((PXSelectBase<Document>) this.Documents).Current, applicableCarrierPlugin);
          if (carrierRequest != null)
          {
            if (carrierRequest.PackagesEx.Count == 0)
            {
              string str = string.Format(PXLocalizer.Localize("Autopackaging for {0} resulted in zero packages. Please check your settings. Make sure that boxes used for packing are configured for a carrier."), (object) applicableCarrierPlugin.CarrierPluginID);
              PXTrace.WriteWarning(str);
              messages.Add(str);
            }
          }
          else
            continue;
        }
        catch (Exception ex)
        {
          CarrierRatesExtension<TGraph, TPrimary>.LogException(messages, ex);
        }
        source.Add(new CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo()
        {
          Plugin = applicableCarrierPlugin,
          Service = carrierService.Result,
          Request = carrierRequest,
          Messages = messages
        });
      }
      else
        source.Add(new CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo()
        {
          Plugin = applicableCarrierPlugin,
          Service = carrierService.Result,
          Request = (CarrierRequest) null,
          Messages = new List<string>()
          {
            carrierService.Messages.FirstOrDefault<Message>().Description
          }
        });
    }
    Parallel.ForEach<CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo>((IEnumerable<CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo>) source, (Action<CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo>) (info =>
    {
      try
      {
        if (info.Request == null || info.Service == null)
          return;
        info.Result = info.Service.GetRateList(info.Request);
      }
      catch (Exception ex)
      {
        CarrierRatesExtension<TGraph, TPrimary>.LogException(info.Messages, ex);
      }
    }));
    int num = 0;
    foreach (CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo carrierRequestInfo in source)
    {
      CarrierRatesExtension<TGraph, TPrimary>.CarrierRequestInfo info = carrierRequestInfo;
      CarrierResult<IList<RateQuote>> result = info.Result;
      if (result != null && result.IsSuccess)
      {
        foreach (RateQuote rateQuote in (IEnumerable<RateQuote>) result.Result)
        {
          if (rateQuote.IsSuccess && rateQuote.Currency != ((PXSelectBase<Document>) this.Documents).Current.CuryID && string.IsNullOrEmpty(((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID))
            throw new PXException("Carrier returned rates in currency that differs from the Order currency. RateType that is used to convert from one currency to another is not specified in AR Preferences.");
          foreach (PXResult<PX.Objects.CS.Carrier> pxResult in ((PXSelectBase<PX.Objects.CS.Carrier>) new PXSelectReadonly<PX.Objects.CS.Carrier, Where<PX.Objects.CS.Carrier.carrierPluginID, Equal<Required<PX.Objects.CS.Carrier.carrierPluginID>>, And<PX.Objects.CS.Carrier.pluginMethod, Equal<Required<PX.Objects.CS.Carrier.pluginMethod>>, And<PX.Objects.CS.Carrier.isExternal, Equal<True>>>>>((PXGraph) this.Base)).Select(new object[2]
          {
            (object) info.Plugin.CarrierPluginID,
            (object) rateQuote.Method.Code
          }))
          {
            PX.Objects.CS.Carrier carrier = PXResult<PX.Objects.CS.Carrier>.op_Implicit(pxResult);
            SOCarrierRate soCarrierRate1 = new SOCarrierRate()
            {
              LineNbr = new int?(num++),
              Method = carrier.CarrierID,
              Description = rateQuote.Method.Description,
              Amount = new Decimal?(this.ConvertAmt(rateQuote.Currency, ((PXSelectBase<Document>) this.Documents).Current.CuryID, ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID, ((PXSelectBase<Document>) this.Documents).Current.DocumentDate.Value, rateQuote.Amount)),
              DeliveryDate = rateQuote.DeliveryDate,
              CanBeSelected = new bool?(true)
            };
            soCarrierRate1.Selected = new bool?(soCarrierRate1.Method == ((PXSelectBase<Document>) this.Documents).Current.ShipVia);
            if (rateQuote.DaysInTransit > 0)
              soCarrierRate1.DaysInTransit = new int?(rateQuote.DaysInTransit);
            SOCarrierRate soCarrierRate2 = ((PXSelectBase<SOCarrierRate>) this.CarrierRates).Insert(soCarrierRate1);
            PXErrorLevel pxErrorLevel = rateQuote.IsSuccess ? (PXErrorLevel) 3 : (PXErrorLevel) 5;
            if (rateQuote.Messages.Any<Message>())
            {
              string str = rateQuote.Messages.Select<Message, string>((Func<Message, string>) (x => !string.IsNullOrEmpty(x.Code) ? $"{x.Code}: {x.Description}" : x.Description)).Aggregate<string>((Func<string, string, string>) ((i, j) => i + Environment.NewLine + j + Environment.NewLine));
              ((PXSelectBase) this.CarrierRates).Cache.RaiseExceptionHandling(typeof (SOCarrierRate.method).Name, (object) soCarrierRate2, (object) null, (Exception) new PXSetPropertyException("{0}", pxErrorLevel, new object[1]
              {
                (object) str
              }));
              if (soCarrierRate2.Selected.GetValueOrDefault())
                ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.freightCostIsValid>(((PXSelectBase<Document>) this.Documents).Current, (object) false);
            }
            else if (soCarrierRate2.Selected.GetValueOrDefault())
              ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.freightCostIsValid>(((PXSelectBase<Document>) this.Documents).Current, (object) true);
          }
        }
      }
      else
      {
        if (result != null)
        {
          IList<Message> messages = result.Messages;
          if ((messages != null ? new bool?(messages.Any<Message>()) : new bool?()).GetValueOrDefault())
            goto label_42;
        }
        List<string> messages1 = info.Messages;
        if ((messages1 != null ? (messages1.Any<string>() ? 1 : 0) : 0) == 0)
          goto label_49;
label_42:
        SOCarrierRate soCarrierRate = ((PXSelectBase<SOCarrierRate>) this.CarrierRates).Insert(new SOCarrierRate()
        {
          LineNbr = new int?(num++),
          Method = info.Plugin.CarrierPluginID,
          Description = info.Plugin.Description,
          CanBeSelected = new bool?(false)
        });
        string str = string.Empty;
        List<string> messages2 = info.Messages;
        if ((messages2 != null ? (messages2.Any<string>() ? 1 : 0) : 0) != 0)
          str = info.Messages.Aggregate<string>((Func<string, string, string>) ((i, j) => i + Environment.NewLine + j + Environment.NewLine));
        if (result != null)
        {
          IList<Message> messages3 = result.Messages;
          if ((messages3 != null ? new bool?(messages3.Any<Message>()) : new bool?()).GetValueOrDefault())
            str += result.Messages.Select<Message, string>((Func<Message, string>) (x => x.ToString())).ToList<string>().Aggregate<string>((Func<string, string, string>) ((i, j) => (PXMessages.LocalizeNoPrefix("{0} Returned error:{1}"), info.Plugin.CarrierPluginID, i).ToString() + Environment.NewLine + j + Environment.NewLine));
        }
        if (str.Length > 0)
          ((PXSelectBase) this.CarrierRates).Cache.RaiseExceptionHandling(typeof (SOCarrierRate.method).Name, (object) soCarrierRate, (object) null, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 5));
label_49:
        if (result != null && !string.IsNullOrEmpty(result.RequestData))
          PXTrace.WriteInformation(result.RequestData);
      }
    }
    ((PXSelectBase) this.CarrierRates).View.RequestRefresh();
  }

  private static void LogException(List<string> messages, Exception ex)
  {
    PXTrace.WriteError(ex);
    messages.Add(ex.Message);
    if (ex.InnerException == null)
      return;
    PXTrace.WriteError(ex.InnerException);
    messages.Add(ex.InnerException.Message);
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable RecalculatePackages(PXAdapter adapter)
  {
    if (((PXSelectBase<Document>) this.Documents).Current != null)
      this.RecalculatePackagesForOrder(((PXSelectBase<Document>) this.Documents).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CaptureWeightFromScale(PXAdapter adapter)
  {
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (userPreferences != null && !userPreferences.DefaultScalesID.HasValue)
      throw new PXException("The default scale is not found in the system.");
    ((PXCache) GraphHelper.Caches<SMScale>((PXGraph) this.Base)).ClearQueryCache();
    SMScale scale = PXResultset<SMScale>.op_Implicit(PXSelectBase<SMScale, PXSelectReadonly<SMScale, Where<SMScale.scaleDeviceID, Equal<Required<SMScale.scaleDeviceID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) (Guid?) userPreferences?.DefaultScalesID
    }));
    SMScaleWeightConversion extension = PXCacheEx.GetExtension<SMScaleWeightConversion>((IBqlTable) scale);
    this.VerifyScaleWeight(scale);
    if (extension != null)
    {
      Decimal? companyLastWeight = extension.CompanyLastWeight;
      Decimal num = 0M;
      if (!(companyLastWeight.GetValueOrDefault() == num & companyLastWeight.HasValue))
        this.UpdatePackageWeightFromScale(extension.CompanyLastWeight);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<Document> eventArgs)
  {
    string str = (string) null;
    if (eventArgs.Row?.ShipVia != null)
    {
      PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, eventArgs.Row.ShipVia);
      if (carrier != null)
      {
        CarrierPlugin row = CarrierPlugin.PK.Find((PXGraph) this.Base, carrier.CarrierPluginID);
        if (row != null && string.IsNullOrEmpty(row.PluginTypeName))
          str = FieldIsEmptyException.GetErrorText((PXCache) GraphHelper.Caches<CarrierPlugin>((PXGraph) this.Base), (object) row, typeof (CarrierPlugin.pluginTypeName), (object) row.CarrierPluginID);
      }
    }
    PXUIFieldAttribute.SetError(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<Document>>) eventArgs).Cache, (object) eventArgs.Row, "shipVia", str, eventArgs.Row?.ShipVia, false, (PXErrorLevel) 2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<DocumentPackage> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<DocumentPackage>>) e).Cache.ObjectsEqual<DocumentPackage.boxID, DocumentPackage.siteID, DocumentPackage.length, DocumentPackage.width, DocumentPackage.height, DocumentPackage.weight, DocumentPackage.grossWeight>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<DocumentPackage>>) e).Cache.ObjectsEqual<DocumentPackage.declaredValue, DocumentPackage.cOD, DocumentPackage.stampsAddOns>((object) e.Row, (object) e.OldRow))
      return;
    this.ResetFreightCostIsValid(((PXSelectBase<Document>) this.Documents).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserted<DocumentPackage> e)
  {
    this.ResetFreightCostIsValid(((PXSelectBase<Document>) this.Documents).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<DocumentPackage> e)
  {
    this.ResetFreightCostIsValid(((PXSelectBase<Document>) this.Documents).Current);
  }

  protected void ResetFreightCostIsValid(Document currentDocument)
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, currentDocument?.ShipVia);
    if ((carrier != null ? (carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.freightCostIsValid>(((PXSelectBase<Document>) this.Documents).Current, (object) false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOCarrierRate> e)
  {
    if (e.Row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.CarrierRates).Cache;
    SOCarrierRate row1 = e.Row;
    bool? canBeSelected = e.Row.CanBeSelected;
    bool flag1 = false;
    int num1 = !(canBeSelected.GetValueOrDefault() == flag1 & canBeSelected.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOCarrierRate.selected>(cache1, (object) row1, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.CarrierRates).Cache;
    SOCarrierRate row2 = e.Row;
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    int num2;
    if (current == null)
    {
      num2 = 0;
    }
    else
    {
      bool? freightCostIsValid = current.FreightCostIsValid;
      bool flag2 = false;
      num2 = freightCostIsValid.GetValueOrDefault() == flag2 & freightCostIsValid.HasValue ? 1 : 0;
    }
    string str = num2 != 0 ? "The amount is not up to date." : (string) null;
    PXUIFieldAttribute.SetWarning<SOCarrierRate.amount>(cache2, (object) row2, str);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOCarrierRate> e)
  {
    if (e.Row == null || e.OldRow == null)
      return;
    bool? selected1 = e.Row.Selected;
    bool? selected2 = e.OldRow.Selected;
    if (selected1.GetValueOrDefault() == selected2.GetValueOrDefault() & selected1.HasValue == selected2.HasValue)
      return;
    selected2 = e.Row.Selected;
    if (selected2.GetValueOrDefault())
    {
      try
      {
        ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipViaUpdateFromShopForRate>(((PXSelectBase<Document>) this.Documents).Current, (object) true);
        ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipVia>(((PXSelectBase<Document>) this.Documents).Current, (object) e.Row.Method);
        ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.freightCostIsValid>(((PXSelectBase<Document>) this.Documents).Current, (object) false);
      }
      finally
      {
        ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipViaUpdateFromShopForRate>(((PXSelectBase<Document>) this.Documents).Current, (object) false);
      }
      foreach (SOCarrierRate soCarrierRate in ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOCarrierRate>>) e).Cache.Cached)
      {
        int? lineNbr1 = soCarrierRate.LineNbr;
        int? lineNbr2 = e.Row.LineNbr;
        if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
          soCarrierRate.Selected = new bool?(false);
      }
      ((PXSelectBase) this.CarrierRates).View.RequestRefresh();
    }
    else
      ((PXSelectBase<Document>) this.Documents).SetValueExt<Document.shipVia>(((PXSelectBase<Document>) this.Documents).Current, (object) null);
    ((PXSelectBase) this.Documents).Cache.Update((object) ((PXSelectBase<Document>) this.Documents).Current);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<SOCarrierRate> e) => e.Cancel = true;

  protected virtual IEnumerable<CarrierPlugin> GetApplicableCarrierPlugins()
  {
    return GraphHelper.RowCast<CarrierPlugin>((IEnumerable) PXSelectBase<CarrierPlugin, PXSelectReadonly<CarrierPlugin, Where<CarrierPlugin.isActive, Equal<True>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  protected virtual DateTime GetServerTime()
  {
    DateTime dateTime1;
    DateTime dateTime2;
    PXDatabase.SelectDate(ref dateTime1, ref dateTime2);
    return PXTimeZoneInfo.ConvertTimeFromUtc(dateTime2, LocaleInfo.GetTimeZone());
  }

  protected virtual UnitsType GetUnitsType(CarrierPlugin plugin)
  {
    return !(plugin.UnitType == "U") ? (UnitsType) 0 : (UnitsType) 1;
  }

  public virtual Decimal ConvertWeightValue(Decimal value, CarrierPlugin plugin)
  {
    return this.ConvertValue(value, ((PXSelectBase<CommonSetup>) this.commonsetup).Current.WeightUOM, plugin.UOM);
  }

  public virtual Decimal ConvertLinearValue(Decimal value, CarrierPlugin plugin)
  {
    string linearUom = ((PXSelectBase<CommonSetup>) this.commonsetup).Current.LinearUOM;
    if (string.IsNullOrEmpty(linearUom))
      throw new PXSetPropertyException("Cannot request information from the carrier because the linear UOM for the company is not specified. Specify the linear UOM on the Companies (CS101500) form.");
    return this.ConvertValue(value, linearUom, plugin.LinearUOM);
  }

  protected virtual Decimal ConvertValue(Decimal value, string from, string to)
  {
    if (from == to)
      return value;
    bool viceVersa = false;
    INUnit unit = INUnit.UK.ByGlobal.Find((PXGraph) this.Base, from, to);
    if (unit == null)
    {
      unit = INUnit.UK.ByGlobal.Find((PXGraph) this.Base, to, from);
      viceVersa = true;
    }
    if (unit == null)
      throw new PXException("Unit Conversion is not setup on 'Units Of Measure' screen. Please setup Unit Conversion FROM {0} TO {1}.", new object[2]
      {
        (object) from,
        (object) to
      });
    return Decimal.Round(INUnitAttribute.ConvertValue(value, unit, INPrecision.NOROUND, viceVersa), 6, MidpointRounding.AwayFromZero);
  }

  protected virtual CarrierRequest BuildRateRequest(Document doc)
  {
    if (string.IsNullOrEmpty(doc.ShipVia))
      return (CarrierRequest) null;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, doc.ShipVia);
    if (carrier == null)
      throw new PXException("No carrier with the given ID was found in the system.");
    if (!carrier.IsExternal.GetValueOrDefault())
      return (CarrierRequest) null;
    CarrierPlugin plugin = CarrierPlugin.PK.Find((PXGraph) this.Base, carrier.CarrierPluginID);
    this.ValidatePlugin(plugin);
    List<string> list = this.GetCarriers(plugin.CarrierPluginID).Select<PX.Objects.CS.Carrier, string>((Func<PX.Objects.CS.Carrier, string>) (x => x.PluginMethod)).ToList<string>();
    IList<SOPackageEngine.PackSet> packages = this.GetPackages(doc, true);
    if (packages.Count == 0)
      throw new PXException("When using 'Manual Packaging' option at least one package must be defined before a Rate Quote can be requested from the Carriers.");
    List<CarrierBoxEx> boxes = new List<CarrierBoxEx>();
    foreach (SOPackageEngine.PackSet packSet in (IEnumerable<SOPackageEngine.PackSet>) packages)
    {
      PX.Objects.CR.Address origin = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, new int?(packSet.SiteID)).AddressID
      }));
      List<SOCarrierPackageInfoEx> carrierPackageInfo = this.GetCarrierPackageInfo(packSet.Packages, carrier.CarrierID);
      boxes.Add(this.BuildCarrierBoxes(carrierPackageInfo, (IAddressBase) origin, carrier.PluginMethod, plugin));
    }
    return this.GetCarrierRequest(doc, this.GetUnitsType(plugin), list, boxes);
  }

  protected virtual CarrierRequest BuildQuoteRequest(Document doc, CarrierPlugin plugin)
  {
    this.ValidatePlugin(plugin);
    List<PX.Objects.CS.Carrier> carriers = this.GetCarriers(plugin.CarrierPluginID);
    List<CarrierBoxEx> boxes = new List<CarrierBoxEx>();
    foreach (PX.Objects.CS.Carrier carrier in carriers)
    {
      foreach (SOPackageEngine.PackSet package in (IEnumerable<SOPackageEngine.PackSet>) this.GetPackages(doc))
      {
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, new int?(package.SiteID));
        PX.Objects.CR.Address origin = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) inSite.AddressID
        }));
        if (origin == null)
        {
          PXTrace.WriteWarning("The address information was not found. Make sure that {0} warehouse has the address information filled in on the Warehouses (IN204000) form.", new object[1]
          {
            (object) inSite.SiteCD
          });
        }
        else
        {
          List<SOCarrierPackageInfoEx> carrierPackageInfo = this.GetCarrierPackageInfo(package.Packages, carrier.CarrierID);
          boxes.Add(this.BuildCarrierBoxes(carrierPackageInfo, (IAddressBase) origin, carrier.PluginMethod, plugin));
        }
      }
    }
    return carriers.Count == 0 ? (CarrierRequest) null : this.GetCarrierRequest(doc, this.GetUnitsType(plugin), carriers.Select<PX.Objects.CS.Carrier, string>((Func<PX.Objects.CS.Carrier, string>) (x => x.PluginMethod)).ToList<string>(), boxes);
  }

  protected abstract CarrierRequest GetCarrierRequest(
    Document doc,
    UnitsType unit,
    List<string> methods,
    List<CarrierBoxEx> boxes);

  protected abstract IList<SOPackageEngine.PackSet> GetPackages(Document doc, bool suppressRecalc = false);

  protected virtual CarrierBoxEx BuildCarrierBoxes(
    List<SOCarrierPackageInfoEx> list,
    IAddressBase origin,
    string method,
    CarrierPlugin plugin)
  {
    List<CarrierBox> carrierBoxList = new List<CarrierBox>();
    foreach (SOCarrierPackageInfoEx package in list)
      carrierBoxList.Add(this.BuildCarrierPackage(package, plugin));
    return new CarrierBoxEx(0, 0M)
    {
      Packages = (IList<CarrierBox>) carrierBoxList,
      Method = method,
      Origin = origin
    };
  }

  protected virtual CarrierBox BuildCarrierPackage(
    SOCarrierPackageInfoEx package,
    CarrierPlugin plugin)
  {
    SOPackageInfoEx package1 = package.Package;
    Decimal num1 = this.ConvertWeightValue(package1.GrossWeight.GetValueOrDefault(), plugin);
    CarrierBox carrierBox1 = new CarrierBox(((int?) package1?.LineNbr).GetValueOrDefault(), num1);
    carrierBox1.DeclaredValue = package1.DeclaredValue.GetValueOrDefault();
    carrierBox1.CarrierPackage = package.CarrierBoxName;
    carrierBox1.Length = this.ConvertLinearValue(package1.Length.GetValueOrDefault(), plugin);
    CarrierBox carrierBox2 = carrierBox1;
    Decimal? nullable = package1.Width;
    Decimal num2 = this.ConvertLinearValue(nullable.GetValueOrDefault(), plugin);
    carrierBox2.Width = num2;
    CarrierBox carrierBox3 = carrierBox1;
    nullable = package1.Height;
    Decimal num3 = this.ConvertLinearValue(nullable.GetValueOrDefault(), plugin);
    carrierBox3.Height = num3;
    if (package1.COD.GetValueOrDefault())
    {
      CarrierBox carrierBox4 = carrierBox1;
      nullable = package1.DeclaredValue;
      Decimal num4 = nullable ?? 1M;
      carrierBox4.COD = num4;
    }
    return carrierBox1;
  }

  private List<PX.Objects.CS.Carrier> GetCarriers(string carrierPluginID)
  {
    List<PX.Objects.CS.Carrier> source = new List<PX.Objects.CS.Carrier>();
    foreach (PXResult<PX.Objects.CS.Carrier> pxResult in ((PXSelectBase<PX.Objects.CS.Carrier>) new PXSelectReadonly<PX.Objects.CS.Carrier, Where<PX.Objects.CS.Carrier.carrierPluginID, Equal<Required<PX.Objects.CS.Carrier.carrierPluginID>>, And<PX.Objects.CS.Carrier.isExternal, Equal<True>, And<PX.Objects.CS.Carrier.isActive, Equal<True>>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) carrierPluginID
    }))
    {
      PX.Objects.CS.Carrier carrier = PXResult<PX.Objects.CS.Carrier>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(carrier.PluginMethod) && !source.Any<PX.Objects.CS.Carrier>((Func<PX.Objects.CS.Carrier, bool>) (x => x.PluginMethod.Contains(carrier.PluginMethod))))
        source.Add(carrier);
    }
    return source;
  }

  private List<SOCarrierPackageInfoEx> GetCarrierPackageInfo(
    List<SOPackageInfoEx> packages,
    string carrierID)
  {
    List<SOCarrierPackageInfoEx> carrierPackageInfo = new List<SOCarrierPackageInfoEx>();
    IEnumerable<CarrierPackage> source = GraphHelper.RowCast<CarrierPackage>((IEnumerable) PXSelectBase<CarrierPackage, PXSelect<CarrierPackage, Where<CarrierPackage.carrierID, Equal<Required<CarrierPackage.carrierID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) carrierID
    })).AsEnumerable<CarrierPackage>();
    foreach (SOPackageInfoEx package1 in packages)
    {
      SOPackageInfoEx package = package1;
      carrierPackageInfo.Add(new SOCarrierPackageInfoEx()
      {
        CarrierID = carrierID,
        CarrierBoxName = source.Where<CarrierPackage>((Func<CarrierPackage, bool>) (x => x.BoxID.Equals(package.BoxID))).Select<CarrierPackage, string>((Func<CarrierPackage, string>) (y => y.CarrierBox)).FirstOrDefault<string>(),
        Package = package
      });
    }
    return carrierPackageInfo;
  }

  protected virtual void ValidatePackages()
  {
  }

  protected virtual void UpdatePackageWeightFromScale(Decimal? weight)
  {
  }

  protected virtual void VerifyScaleWeight(SMScale scale)
  {
    if (scale == null)
      throw new PXException("The scale is not found in the system.");
    DateTime serverTime = this.GetServerTime();
    if (scale.LastModifiedDateTime.Value.AddHours(1.0) < serverTime)
      throw new PXException("No information from the {0} scale. Check the connection of the scale.", new object[1]
      {
        (object) scale.ScaleID
      });
    if (scale.LastWeight.GetValueOrDefault() == 0M)
      throw new PXException("No information from the {0} scale. Make sure that the items are on the scale.", new object[1]
      {
        (object) scale.ScaleID
      });
  }

  protected virtual void ValidatePlugin(CarrierPlugin plugin)
  {
    if (plugin == null)
      throw new PXException("No carrier plug-in with the given ID was found in the system.");
    if (string.IsNullOrEmpty(plugin.UOM))
      throw new PXException("Cannot request information from the carrier because the weight UOM for the carrier is not specified. Specify the weight UOM for the {0} carrier on the Carriers (CS207700) form.", new object[1]
      {
        (object) plugin.CarrierPluginID
      });
    if (string.IsNullOrEmpty(plugin.LinearUOM))
      throw new PXException("Cannot request information from the carrier because the linear UOM for the carrier is not specified. Specify the linear UOM for the {0} carrier on the Carriers (CS207700) form.", new object[1]
      {
        (object) plugin.CarrierPluginID
      });
  }

  protected virtual void RateHasBeenSelected(SOCarrierRate cr)
  {
  }

  protected virtual WebDialogResult AskForRateSelection() => (WebDialogResult) 1;

  protected virtual Decimal ConvertAmt(
    string from,
    string to,
    string rateType,
    DateTime effectiveDate,
    Decimal amount)
  {
    if (from == to)
      return amount;
    Decimal num = amount;
    PXCache cache = ((PXSelectBase) this.CarrierRatesDummyCuryInfo).Cache;
    using (new ReadOnlyScope(new PXCache[1]{ cache }))
    {
      if (from == this.Base.Accessinfo.BaseCuryID)
      {
        PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
        {
          CuryRateTypeID = rateType,
          CuryID = to
        });
        info.SetCuryEffDate(cache, (object) effectiveDate);
        cache.Update((object) info);
        PXCurrencyAttribute.CuryConvCury(cache, info, amount, out num);
        cache.Delete((object) info);
      }
      else if (to == this.Base.Accessinfo.BaseCuryID)
      {
        PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
        {
          CuryRateTypeID = rateType,
          CuryID = from
        });
        info.SetCuryEffDate(cache, (object) effectiveDate);
        cache.Update((object) info);
        PXCurrencyAttribute.CuryConvBase(cache, info, amount, out num);
        cache.Delete((object) info);
      }
      else
      {
        PX.Objects.CM.CurrencyInfo info1 = (PX.Objects.CM.CurrencyInfo) cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
        {
          CuryRateTypeID = rateType,
          CuryID = from
        });
        info1.SetCuryEffDate(cache, (object) effectiveDate);
        cache.Update((object) info1);
        Decimal baseval;
        PXCurrencyAttribute.CuryConvBase(cache, info1, amount, out baseval, true);
        PX.Objects.CM.CurrencyInfo info2 = (PX.Objects.CM.CurrencyInfo) cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
        {
          CuryRateTypeID = rateType,
          CuryID = to
        });
        info2.SetCuryEffDate(cache, (object) effectiveDate);
        cache.Update((object) info1);
        PXCurrencyAttribute.CuryConvCury(cache, info2, baseval, out num, true);
        cache.Delete((object) info1);
        cache.Delete((object) info2);
      }
    }
    return num;
  }

  protected virtual IList<SOPackageEngine.PackSet> CalculatePackages(Document doc, string carrierID)
  {
    Dictionary<string, SOPackageEngine.ItemStats> dictionary = new Dictionary<string, SOPackageEngine.ItemStats>();
    SOPackageEngine.OrderInfo orderInfo = new SOPackageEngine.OrderInfo(carrierID);
    foreach (Tuple<CarrierRatesExtension<TGraph, TPrimary>.ILineInfo, PX.Objects.IN.InventoryItem> line in this.GetLines(doc))
    {
      CarrierRatesExtension<TGraph, TPrimary>.ILineInfo lineInfo = line.Item1;
      PX.Objects.IN.InventoryItem inventoryItem = line.Item2;
      if (!(inventoryItem.PackageOption == "N"))
      {
        orderInfo.AddLine(inventoryItem, lineInfo.BaseQty);
        int num1 = inventoryItem.PackSeparately.GetValueOrDefault() ? inventoryItem.InventoryID.Value : 0;
        string key = $"{lineInfo.SiteID}.{num1}.{inventoryItem.PackageOption}.{lineInfo.Operation}";
        Decimal? nullable = lineInfo.BaseQty;
        Decimal num2 = Math.Abs(nullable.GetValueOrDefault());
        nullable = lineInfo.CuryLineAmt;
        Decimal num3 = Math.Abs(nullable.GetValueOrDefault());
        if (dictionary.ContainsKey(key))
        {
          SOPackageEngine.ItemStats itemStats1 = dictionary[key];
          itemStats1.BaseQty += num2;
          SOPackageEngine.ItemStats itemStats2 = itemStats1;
          Decimal baseWeight = itemStats2.BaseWeight;
          nullable = lineInfo.ExtWeight;
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          itemStats2.BaseWeight = baseWeight + valueOrDefault;
          itemStats1.DeclaredValue += num3;
          itemStats1.AddLine(inventoryItem, new Decimal?(num2));
        }
        else
        {
          SOPackageEngine.ItemStats itemStats3 = new SOPackageEngine.ItemStats();
          itemStats3.SiteID = lineInfo.SiteID;
          itemStats3.InventoryID = new int?(num1);
          itemStats3.Operation = lineInfo.Operation;
          itemStats3.PackOption = inventoryItem.PackageOption;
          itemStats3.BaseQty += num2;
          SOPackageEngine.ItemStats itemStats4 = itemStats3;
          Decimal baseWeight = itemStats4.BaseWeight;
          nullable = lineInfo.ExtWeight;
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          itemStats4.BaseWeight = baseWeight + valueOrDefault;
          itemStats3.DeclaredValue += num3;
          itemStats3.AddLine(inventoryItem, new Decimal?(num2));
          dictionary.Add(key, itemStats3);
        }
      }
    }
    orderInfo.Stats.AddRange((IEnumerable<SOPackageEngine.ItemStats>) dictionary.Values);
    return this.CreatePackageEngine().Pack(orderInfo);
  }

  protected virtual SOPackageEngine CreatePackageEngine()
  {
    return new SOPackageEngine((PXGraph) this.Base);
  }

  protected virtual void RecalculatePackagesForOrder(Document doc)
  {
    if (doc == null)
      throw new ArgumentNullException(nameof (doc));
    if (string.IsNullOrEmpty(doc.ShipVia))
      throw new PXException("Ship Via must be specified before auto packaging.");
    this.ClearPackages(doc);
    IList<SOPackageEngine.PackSet> packages = this.CalculatePackages(doc, doc.ShipVia);
    foreach (SOPackageEngine.PackSet packSet in (IEnumerable<SOPackageEngine.PackSet>) packages)
    {
      if (packSet.Packages.Count > 1000)
        PXTrace.WriteWarning("During autopackaging more than 1000 packages were generated. Please check your configuration.");
      this.InsertPackages((IEnumerable<SOPackageInfoEx>) packSet.Packages);
    }
    if (packages.Count <= 0)
      return;
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.isPackageValid>((object) doc, (object) true);
  }

  protected abstract void ClearPackages(Document doc);

  protected abstract void InsertPackages(IEnumerable<SOPackageInfoEx> packages);

  protected abstract IEnumerable<Tuple<CarrierRatesExtension<TGraph, TPrimary>.ILineInfo, PX.Objects.IN.InventoryItem>> GetLines(
    Document doc);

  protected abstract void CalculateFreightCost(Document doc);

  protected abstract CarrierRatesExtension<TGraph, TPrimary>.DocumentMapping GetDocumentMapping();

  protected abstract CarrierRatesExtension<TGraph, TPrimary>.DocumentPackageMapping GetDocumentPackageMapping();

  private class CarrierRequestInfo
  {
    public CarrierPlugin Plugin;
    public ICarrierService Service;
    public CarrierRequest Request;
    public CarrierResult<IList<RateQuote>> Result;
    public List<string> Messages;
  }

  protected interface ILineInfo
  {
    Decimal? BaseQty { get; }

    Decimal? CuryLineAmt { get; }

    Decimal? ExtWeight { get; }

    int? SiteID { get; }

    string Operation { get; }
  }

  protected class DocumentMapping : IBqlMapping
  {
    public System.Type ShipVia = typeof (Document.shipVia);
    public System.Type CuryID = typeof (Document.curyID);
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    public System.Type DocumentDate = typeof (Document.documentDate);
    public System.Type IsPackageValid = typeof (Document.isPackageValid);
    public System.Type ShipViaUpdateFromShopForRate = typeof (Document.shipViaUpdateFromShopForRate);
    public System.Type FreightCostIsValid = typeof (Document.freightCostIsValid);

    public System.Type Table { get; }

    public System.Type Extension => typeof (Document);

    public DocumentMapping(System.Type table) => this.Table = table;
  }

  protected class DocumentPackageMapping : IBqlMapping
  {
    public System.Type BoxID = typeof (DocumentPackage.boxID);
    public System.Type SiteID = typeof (DocumentPackage.siteID);
    public System.Type Length = typeof (DocumentPackage.length);
    public System.Type Width = typeof (DocumentPackage.width);
    public System.Type Height = typeof (DocumentPackage.height);
    public System.Type Weight = typeof (DocumentPackage.weight);
    public System.Type GrossWeight = typeof (DocumentPackage.grossWeight);
    public System.Type DeclaredValue = typeof (DocumentPackage.declaredValue);
    public System.Type COD = typeof (DocumentPackage.cOD);
    public System.Type StampsAddOns = typeof (DocumentPackage.stampsAddOns);

    public System.Type Table { get; }

    public System.Type Extension => typeof (DocumentPackage);

    public DocumentPackageMapping(System.Type table) => this.Table = table;
  }
}
