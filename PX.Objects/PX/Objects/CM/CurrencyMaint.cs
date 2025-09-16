// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using PX.Objects.Common.EntityInUse;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CM;

public class CurrencyMaint : PXGraph<CurrencyMaint, CurrencyList>
{
  [PXCopyPasteHiddenView]
  public PXSelect<CurrencyList> CuryListRecords;
  public PXSelect<Currency, Where<Currency.curyID, Equal<Current<CurrencyList.curyID>>>> CuryRecords;
  public PXSetup<Company> company;

  public virtual IEnumerable curyRecords()
  {
    PXResultset<Currency> pxResultset = PXSelectBase<Currency, PXSelect<Currency, Where<Currency.curyID, Equal<Current<CurrencyList.curyID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    if (pxResultset.Count == 0 && ((PXSelectBase<CurrencyList>) this.CuryListRecords).Current != null)
      pxResultset.Add(new PXResult<Currency>(new Currency()
      {
        CuryID = ((PXSelectBase<CurrencyList>) this.CuryListRecords).Current.CuryID
      }));
    return (IEnumerable) pxResultset;
  }

  public CurrencyMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<Company>) this.company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
    PXUIFieldAttribute.SetVisible<CurrencyList.isFinancial>(((PXSelectBase) this.CuryListRecords).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
  }

  protected virtual void _(Events.FieldDefaulting<Currency.decimalPlaces> e)
  {
    ((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<Currency.decimalPlaces>>) e).Cache.SetValue(e.Row, typeof (Currency.decimalPlaces).Name, (object) (short) 2);
  }

  protected virtual void _(
    Events.FieldVerifying<CurrencyList.decimalPlaces> e)
  {
    if (!(e.Row is CurrencyList row))
      return;
    WebDialogResult webDialogResult = ((PXSelectBase<CurrencyList>) this.CuryListRecords).Ask("Warning", "Changing the precision of a currency in which transactions are recorded may lead to negative consequences, including the impossibility of processing documents in this currency. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 3);
    ((Events.FieldVerifyingBase<Events.FieldVerifying<CurrencyList.decimalPlaces>, object, object>) e).NewValue = webDialogResult == 6 ? ((Events.FieldVerifyingBase<Events.FieldVerifying<CurrencyList.decimalPlaces>, object, object>) e).NewValue : (object) row.DecimalPlaces;
  }

  [PXMergeAttributes]
  [PXSelectorReadDeletedFieldVerifyingLess(typeof (Search<CurrencyList.curyID>), CacheGlobal = true)]
  protected virtual void _(Events.CacheAttached<Currency.curyID> a)
  {
  }

  protected virtual void _(Events.RowUpdated<CurrencyList> e)
  {
    if (e.Row == null)
      return;
    bool? isFinancial;
    if (((PXSelectBase) this.CuryRecords).Cache.Current != null)
    {
      isFinancial = e.Row.IsFinancial;
      if (!isFinancial.GetValueOrDefault())
        ((PXSelectBase) this.CuryRecords).Cache.Delete(((PXSelectBase) this.CuryRecords).Cache.Current);
    }
    isFinancial = e.Row.IsFinancial;
    if (!isFinancial.GetValueOrDefault() || ((PXSelectBase) this.CuryRecords).Cache.Current == null || ((PXSelectBase<Currency>) this.CuryRecords).Current.tstamp != null)
      return;
    ((PXSelectBase) this.CuryRecords).Cache.Insert(((PXSelectBase) this.CuryRecords).Cache.Current);
  }

  protected void _(Events.RowSelected<CurrencyList> e)
  {
    CurrencyList row = e.Row;
    if (row == null)
      return;
    ((PXSelectBase) this.CuryListRecords).Cache.AllowDelete = true;
    ((PXSelectBase) this.CuryRecords).Cache.AllowDelete = true;
    if (EntityInUseHelper.IsEntityInUse<CurrencyInUse>((object) row.CuryID))
      PXUIFieldAttribute.SetEnabled<Currency.decimalPlaces>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CurrencyList>>) e).Cache, (object) row, false);
    if (!this.IsBaseCurrency(row.CuryID))
      return;
    ((PXSelectBase) this.CuryListRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.CuryRecords).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<CurrencyList.isActive>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CurrencyList>>) e).Cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CurrencyList.isFinancial>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CurrencyList>>) e).Cache, (object) row, false);
  }

  protected void _(Events.RowDeleting<CurrencyList> e)
  {
    CurrencyList row = e.Row;
    if (this.IsBaseCurrency(row.CuryID))
      throw new PXException("The {0} currency cannot be deleted because it is a base currency.", new object[1]
      {
        (object) row.CuryID
      });
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R2.")]
  protected virtual CurrencyMaint.SettingsByFeatures GetSettingsByFeatures(
    string curyID,
    string baseCuryID,
    bool? isFinancial)
  {
    return this.GetSettingsByFeatures(curyID, isFinancial);
  }

  protected virtual CurrencyMaint.SettingsByFeatures GetSettingsByFeatures(
    string curyID,
    bool? isFinancial)
  {
    bool flag1 = this.IsBaseCurrency(curyID);
    int num = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? 1 : 0;
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.invoiceRounding>();
    bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.finStatementCurTranslation>();
    bool flag5 = PXAccess.FeatureInstalled<FeaturesSet.financialStandard>();
    bool flag6 = false;
    bool flag7 = false;
    bool flag8 = true;
    bool flag9 = false;
    bool flag10 = false;
    bool flag11;
    bool flag12;
    bool flag13;
    bool flag14;
    bool flag15;
    bool flag16;
    bool flag17;
    bool flag18;
    bool flag19;
    if (num != 0)
    {
      flag11 = true;
      flag12 = true;
      flag13 = true;
      flag14 = true;
      flag15 = true;
      flag16 = false;
      flag17 = true;
      flag18 = true;
      flag6 = flag4;
      flag7 = flag4;
      flag19 = true;
      flag10 = isFinancial.GetValueOrDefault();
    }
    else
    {
      if (flag2)
      {
        flag11 = true;
        flag12 = true;
        flag13 = !flag1;
        flag14 = true;
        flag17 = !flag1;
        flag18 = true;
        if (flag4)
        {
          flag6 = true;
          flag7 = true;
        }
        flag15 = !flag1;
        flag16 = false;
        flag8 = true;
        flag19 = true;
      }
      else
      {
        flag11 = false;
        flag12 = false;
        flag13 = false;
        flag14 = false;
        flag15 = false;
        flag16 = false;
        flag17 = false;
        flag18 = false;
        flag6 = false;
        flag7 = false;
        if (!flag1)
          flag9 = false;
        flag19 = flag3 && flag1;
      }
      if ((flag5 | flag3) & flag1)
        flag10 = true;
    }
    return new CurrencyMaint.SettingsByFeatures()
    {
      showRealGL = flag11,
      reqRealGL = flag12 & flag11,
      showUnrealGL = flag13,
      reqUnrealGL = flag14 & flag13,
      showAPProvAcct = flag15,
      reqAPProvAcct = flag16 & flag15,
      showRevalGL = flag17,
      reqRevalGL = flag18 & flag17,
      showTransGL = flag6,
      reqTransGL = flag7 & flag6,
      showRoundingGL = flag8,
      reqRoundingGL = flag19 & flag8,
      showRoundingLimit = flag10
    };
  }

  protected virtual bool IsBaseCurrency(string currency)
  {
    return CurrencyCollection.IsBaseCurrency(currency);
  }

  protected void _(Events.RowSelected<Currency> e)
  {
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Currency>>) e).Cache;
    CurrencyList current = ((PXSelectBase<CurrencyList>) this.CuryListRecords).Current;
    Currency row = e.Row;
    PXUIFieldAttribute.SetEnabled(cache, (object) row, current != null && current.IsFinancial.GetValueOrDefault());
    string curyId = row.CuryID;
    bool? nullable = (bool?) current?.IsFinancial;
    bool? isFinancial = new bool?(nullable.GetValueOrDefault());
    CurrencyMaint.SettingsByFeatures settingsByFeatures = this.GetSettingsByFeatures(curyId, isFinancial);
    PXUIFieldAttribute.SetVisible<Currency.realGainAcctID>(cache, (object) row, settingsByFeatures.showRealGL);
    PXUIFieldAttribute.SetVisible<Currency.realGainSubID>(cache, (object) row, settingsByFeatures.showRealGL);
    PXUIFieldAttribute.SetVisible<Currency.realLossAcctID>(cache, (object) row, settingsByFeatures.showRealGL);
    PXUIFieldAttribute.SetVisible<Currency.realLossSubID>(cache, (object) row, settingsByFeatures.showRealGL);
    PXUIFieldAttribute.SetRequired<Currency.realGainAcctID>(cache, settingsByFeatures.reqRealGL);
    PXUIFieldAttribute.SetRequired<Currency.realGainSubID>(cache, settingsByFeatures.reqRealGL);
    PXUIFieldAttribute.SetRequired<Currency.realLossAcctID>(cache, settingsByFeatures.reqRealGL);
    PXUIFieldAttribute.SetRequired<Currency.realLossSubID>(cache, settingsByFeatures.reqRealGL);
    PXUIFieldAttribute.SetVisible<Currency.unrealizedGainAcctID>(cache, (object) row, settingsByFeatures.showUnrealGL);
    PXUIFieldAttribute.SetVisible<Currency.unrealizedGainSubID>(cache, (object) row, settingsByFeatures.showUnrealGL);
    PXUIFieldAttribute.SetVisible<Currency.unrealizedLossAcctID>(cache, (object) row, settingsByFeatures.showUnrealGL);
    PXUIFieldAttribute.SetVisible<Currency.unrealizedLossSubID>(cache, (object) row, settingsByFeatures.showUnrealGL);
    PXUIFieldAttribute.SetRequired<Currency.unrealizedGainAcctID>(cache, settingsByFeatures.reqUnrealGL);
    PXUIFieldAttribute.SetRequired<Currency.unrealizedGainSubID>(cache, settingsByFeatures.reqUnrealGL);
    PXUIFieldAttribute.SetRequired<Currency.unrealizedLossAcctID>(cache, settingsByFeatures.reqUnrealGL);
    PXUIFieldAttribute.SetRequired<Currency.unrealizedLossSubID>(cache, settingsByFeatures.reqUnrealGL);
    PXUIFieldAttribute.SetVisible<Currency.aPProvAcctID>(cache, (object) row, settingsByFeatures.showAPProvAcct);
    PXUIFieldAttribute.SetVisible<Currency.aPProvSubID>(cache, (object) row, settingsByFeatures.showAPProvAcct);
    PXUIFieldAttribute.SetVisible<Currency.aRProvAcctID>(cache, (object) row, settingsByFeatures.showAPProvAcct);
    PXUIFieldAttribute.SetVisible<Currency.aRProvSubID>(cache, (object) row, settingsByFeatures.showAPProvAcct);
    PXUIFieldAttribute.SetRequired<Currency.aPProvAcctID>(cache, settingsByFeatures.reqAPProvAcct);
    PXUIFieldAttribute.SetRequired<Currency.aPProvSubID>(cache, settingsByFeatures.reqAPProvAcct);
    PXUIFieldAttribute.SetRequired<Currency.aRProvAcctID>(cache, settingsByFeatures.reqAPProvAcct);
    PXUIFieldAttribute.SetRequired<Currency.aRProvSubID>(cache, settingsByFeatures.reqAPProvAcct);
    PXUIFieldAttribute.SetVisible<Currency.revalGainAcctID>(cache, (object) row, settingsByFeatures.showRevalGL);
    PXUIFieldAttribute.SetVisible<Currency.revalGainSubID>(cache, (object) row, settingsByFeatures.showRevalGL);
    PXUIFieldAttribute.SetVisible<Currency.revalLossAcctID>(cache, (object) row, settingsByFeatures.showRevalGL);
    PXUIFieldAttribute.SetVisible<Currency.revalLossSubID>(cache, (object) row, settingsByFeatures.showRevalGL);
    PXUIFieldAttribute.SetRequired<Currency.revalGainAcctID>(cache, settingsByFeatures.reqRevalGL);
    PXUIFieldAttribute.SetRequired<Currency.revalGainSubID>(cache, settingsByFeatures.reqRevalGL);
    PXUIFieldAttribute.SetRequired<Currency.revalLossAcctID>(cache, settingsByFeatures.reqRevalGL);
    PXUIFieldAttribute.SetRequired<Currency.revalLossSubID>(cache, settingsByFeatures.reqRevalGL);
    PXUIFieldAttribute.SetVisible<Currency.translationGainAcctID>(cache, (object) row, settingsByFeatures.showTransGL);
    PXUIFieldAttribute.SetVisible<Currency.translationGainSubID>(cache, (object) row, settingsByFeatures.showTransGL);
    PXUIFieldAttribute.SetVisible<Currency.translationLossAcctID>(cache, (object) row, settingsByFeatures.showTransGL);
    PXUIFieldAttribute.SetVisible<Currency.translationLossSubID>(cache, (object) row, settingsByFeatures.showTransGL);
    PXUIFieldAttribute.SetRequired<Currency.translationGainAcctID>(cache, settingsByFeatures.reqTransGL);
    PXUIFieldAttribute.SetRequired<Currency.translationGainSubID>(cache, settingsByFeatures.reqTransGL);
    PXUIFieldAttribute.SetRequired<Currency.translationLossAcctID>(cache, settingsByFeatures.reqTransGL);
    PXUIFieldAttribute.SetRequired<Currency.translationLossSubID>(cache, settingsByFeatures.reqTransGL);
    PXUIFieldAttribute.SetVisible<Currency.roundingGainAcctID>(cache, (object) row, settingsByFeatures.showRoundingGL);
    PXUIFieldAttribute.SetVisible<Currency.roundingGainSubID>(cache, (object) row, settingsByFeatures.showRoundingGL);
    PXUIFieldAttribute.SetVisible<Currency.roundingLossAcctID>(cache, (object) row, settingsByFeatures.showRoundingGL);
    PXUIFieldAttribute.SetVisible<Currency.roundingLossSubID>(cache, (object) row, settingsByFeatures.showRoundingGL);
    PXUIFieldAttribute.SetRequired<Currency.roundingGainAcctID>(cache, settingsByFeatures.reqRoundingGL);
    PXUIFieldAttribute.SetRequired<Currency.roundingGainSubID>(cache, settingsByFeatures.reqRoundingGL);
    PXUIFieldAttribute.SetRequired<Currency.roundingLossAcctID>(cache, settingsByFeatures.reqRoundingGL);
    PXUIFieldAttribute.SetRequired<Currency.roundingLossSubID>(cache, settingsByFeatures.reqRoundingGL);
    Currency currency1 = row;
    nullable = row.UseARPreferencesSettings;
    bool flag1 = false;
    int num1 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Currency.aRInvoiceRounding>(cache, (object) currency1, num1 != 0);
    Currency currency2 = row;
    nullable = row.UseARPreferencesSettings;
    bool flag2 = false;
    int num2 = !(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) ? 0 : (row.ARInvoiceRounding != "N" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Currency.aRInvoicePrecision>(cache, (object) currency2, num2 != 0);
    Currency currency3 = row;
    nullable = row.UseAPPreferencesSettings;
    bool flag3 = false;
    int num3 = nullable.GetValueOrDefault() == flag3 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Currency.aPInvoiceRounding>(cache, (object) currency3, num3 != 0);
    Currency currency4 = row;
    nullable = row.UseAPPreferencesSettings;
    bool flag4 = false;
    int num4 = !(nullable.GetValueOrDefault() == flag4 & nullable.HasValue) ? 0 : (row.APInvoiceRounding != "N" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Currency.aPInvoicePrecision>(cache, (object) currency4, num4 != 0);
    PXUIFieldAttribute.SetVisible<Currency.roundingLimit>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Currency>>) e).Cache, (object) row, settingsByFeatures.showRoundingLimit);
  }

  protected void _(Events.RowPersisting<Currency> e)
  {
    Currency row = e.Row;
    if (row == null)
      return;
    CurrencyList current = ((PXSelectBase<CurrencyList>) this.CuryListRecords).Current;
    bool? nullable1;
    if (current != null)
    {
      nullable1 = current.IsFinancial;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && e.Operation == 2)
      {
        e.Cancel = true;
        return;
      }
    }
    PXCache cache = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<Currency>>) e).Cache;
    string curyId = row.CuryID;
    bool? nullable2;
    if (current == null)
    {
      nullable1 = new bool?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current.IsFinancial;
    nullable1 = nullable2;
    bool? isFinancial = new bool?(nullable1.GetValueOrDefault());
    CurrencyMaint.SettingsByFeatures settingsByFeatures = this.GetSettingsByFeatures(curyId, isFinancial);
    PXDefaultAttribute.SetPersistingCheck<Currency.realGainAcctID>(cache, (object) row, settingsByFeatures.reqRealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.realGainSubID>(cache, (object) row, settingsByFeatures.reqRealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.realLossAcctID>(cache, (object) row, settingsByFeatures.reqRealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.realLossSubID>(cache, (object) row, settingsByFeatures.reqRealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.unrealizedGainAcctID>(cache, (object) row, settingsByFeatures.reqUnrealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.unrealizedGainSubID>(cache, (object) row, settingsByFeatures.reqUnrealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.unrealizedLossAcctID>(cache, (object) row, settingsByFeatures.reqUnrealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.unrealizedLossSubID>(cache, (object) row, settingsByFeatures.reqUnrealGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aPProvAcctID>(cache, (object) row, settingsByFeatures.reqAPProvAcct ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aPProvSubID>(cache, (object) row, settingsByFeatures.reqAPProvAcct ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aRProvAcctID>(cache, (object) row, settingsByFeatures.reqAPProvAcct ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aRProvSubID>(cache, (object) row, settingsByFeatures.reqAPProvAcct ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.revalGainAcctID>(cache, (object) row, settingsByFeatures.reqRevalGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.revalGainSubID>(cache, (object) row, settingsByFeatures.reqRevalGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.revalLossAcctID>(cache, (object) row, settingsByFeatures.reqRevalGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.revalLossSubID>(cache, (object) row, settingsByFeatures.reqRevalGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.translationGainAcctID>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.translationGainSubID>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.translationLossAcctID>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.translationLossSubID>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.roundingGainAcctID>(cache, (object) row, settingsByFeatures.reqRoundingGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.roundingGainSubID>(cache, (object) row, settingsByFeatures.reqRoundingGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.roundingLossAcctID>(cache, (object) row, settingsByFeatures.reqRoundingGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.roundingLossSubID>(cache, (object) row, settingsByFeatures.reqRoundingGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aRInvoiceRounding>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aRInvoicePrecision>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aPInvoiceRounding>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<Currency.aPInvoicePrecision>(cache, (object) row, settingsByFeatures.reqTransGL ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXFieldDescription]
  protected virtual void _(Events.CacheAttached<CurrencyList.curyID> a)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.realGainAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.realLossAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.revalGainAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.revalLossAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.aRProvAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(Events.CacheAttached<Currency.aPProvAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (False))]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.translationGainAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (False))]
  protected virtual void _(
    Events.CacheAttached<Currency.translationGainSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (False))]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.translationLossAcctID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (False))]
  protected virtual void _(
    Events.CacheAttached<Currency.translationLossSubID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.unrealizedGainAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.unrealizedLossAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.roundingGainAcctID> e)
  {
  }

  [PXMergeAttributes]
  [AvoidControlAccounts]
  protected virtual void _(
    Events.CacheAttached<Currency.roundingLossAcctID> e)
  {
  }

  public struct SettingsByFeatures
  {
    public bool showRealGL;
    public bool reqRealGL;
    public bool showUnrealGL;
    public bool reqUnrealGL;
    public bool showAPProvAcct;
    public bool reqAPProvAcct;
    public bool showRevalGL;
    public bool reqRevalGL;
    public bool showTransGL;
    public bool reqTransGL;
    public bool showRoundingGL;
    public bool reqRoundingGL;
    public bool showRoundingLimit;
  }
}
