// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxZoneMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class TaxZoneMaint : PXGraph<TaxZoneMaint, TaxZone>
{
  public PXSelect<TaxZone> TxZone;
  public PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<TaxZone.taxZoneID>>>> TxZoneCurrent;
  public PXSelectJoin<TaxZoneDet, InnerJoin<Tax, On<TaxZoneDet.taxID, Equal<Tax.taxID>>>, Where<TaxZoneDet.taxZoneID, Equal<Current<TaxZone.taxZoneID>>>> Details;
  public PXSelect<TaxZoneDet> TxZoneDet;
  [PXImport(typeof (TaxZone))]
  [PXCopyPasteHiddenView]
  public PXSelect<TaxZoneAddressMapping, Where<TaxZoneAddressMapping.taxZoneID, Equal<Current<TaxZone.taxZoneID>>>> TaxZoneAddressMappings;
  public PXSetup<PX.Objects.GL.Branch> Company;
  [PXHidden]
  public PXSelect<PX.Objects.AP.VendorClass, Where<PX.Objects.AP.VendorClass.taxZoneID, Equal<Current<TaxZone.taxZoneID>>>> VendorClass;

  public TaxZoneMaint()
  {
    if (!((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<Tax.taxID, Where<Tax.isExternal, Equal<False>>>), new Type[] {typeof (Tax.taxID), typeof (Tax.descr), typeof (Tax.directTax)})]
  public virtual void _(PX.Data.Events.CacheAttached<TaxZoneDet.taxID> e)
  {
  }

  protected virtual void TaxZoneDet_TaxID_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    TaxZoneDet row = (TaxZoneDet) e.Row;
    string newValue = (string) e.NewValue;
    if (!(row.TaxID != newValue))
      return;
    List<string> stringList = new List<string>()
    {
      newValue
    };
    foreach (PXResult<TaxZoneDet> pxResult in ((PXSelectBase<TaxZoneDet>) this.Details).Select(Array.Empty<object>()))
    {
      TaxZoneDet taxZoneDet = PXResult<TaxZoneDet>.op_Implicit(pxResult);
      if (taxZoneDet.TaxID == newValue)
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("This tax is already included into the list.");
      }
      stringList.Add(taxZoneDet.TaxID);
    }
    PXResultset<TaxCategoryDet> invalidCategoryCombinations;
    if (!this.TryValidateTaxCategoryCombinationWithDirectTax(stringList.ToArray(), out invalidCategoryCombinations))
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("Multiple direct-entry taxes cannot be included in the same tax category ({0}) and tax zone ({1}).", new object[2]
      {
        (object) PXResultset<TaxCategoryDet>.op_Implicit(invalidCategoryCombinations)?.TaxCategoryID,
        (object) ((PXSelectBase<TaxZone>) this.TxZone).Current?.TaxZoneID
      });
    }
  }

  protected virtual void TaxZone_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxZone row1))
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>();
    PXUIFieldAttribute.SetVisible<TaxZone.isExternal>(cache, (object) null, flag1);
    PXCache pxCache1 = cache;
    object row2 = e.Row;
    bool? isExternal = row1.IsExternal;
    int num1 = isExternal.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<TaxZone.taxPluginID>(pxCache1, row2, num1 != 0);
    PXCache pxCache2 = cache;
    object row3 = e.Row;
    isExternal = row1.IsExternal;
    int num2 = isExternal.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<TaxZone.taxVendorID>(pxCache2, row3, num2 != 0);
    PXUIFieldAttribute.SetVisible<TaxZone.taxID>(cache, e.Row, false);
    PXDefaultAttribute.SetPersistingCheck<TaxZone.taxID>(cache, e.Row, (PXPersistingCheck) 2);
    PXCache pxCache3 = cache;
    object row4 = e.Row;
    isExternal = row1.IsExternal;
    int num3 = isExternal.GetValueOrDefault() ? 0 : 2;
    PXDefaultAttribute.SetPersistingCheck<TaxZone.taxVendorID>(pxCache3, row4, (PXPersistingCheck) num3);
    PXUIFieldAttribute.SetVisible<TaxZoneAddressMapping.countryID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "C");
    PXUIFieldAttribute.SetVisible<TaxZoneAddressMapping.stateID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "S");
    PXUIFieldAttribute.SetVisible<TaxZoneAddressMapping.fromPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "P");
    PXUIFieldAttribute.SetVisible<TaxZoneAddressMapping.toPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "P");
    PXUIFieldAttribute.SetEnabled<TaxZoneAddressMapping.countryID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "C");
    PXUIFieldAttribute.SetEnabled<TaxZoneAddressMapping.stateID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "S");
    PXUIFieldAttribute.SetEnabled<TaxZoneAddressMapping.fromPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "P");
    PXUIFieldAttribute.SetEnabled<TaxZoneAddressMapping.toPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) null, row1.MappingType == "P");
    bool flag2 = row1.CountryID == string.Empty && row1.MappingType == "P";
    PXUIFieldAttribute.SetEnabled<TaxZone.mappingType>(cache, (object) null, !flag2);
    PXUIFieldAttribute.SetVisible<TaxZone.countryID>(cache, (object) null, row1.MappingType != "C");
    bool flag3 = !flag2 && (row1.CountryID != null || row1.MappingType == "C");
    ((PXSelectBase) this.TaxZoneAddressMappings).Cache.AllowInsert = flag3;
    ((PXSelectBase) this.TaxZoneAddressMappings).Cache.AllowUpdate = flag3;
    ((PXSelectBase) this.TaxZoneAddressMappings).Cache.AllowDelete = flag3;
    PXCache cache1 = ((PXSelectBase) this.Details).Cache;
    isExternal = row1.IsExternal;
    int num4 = !isExternal.GetValueOrDefault() ? 1 : 0;
    cache1.AllowInsert = num4 != 0;
    PXCache cache2 = ((PXSelectBase) this.Details).Cache;
    isExternal = row1.IsExternal;
    int num5 = !isExternal.GetValueOrDefault() ? 1 : 0;
    cache2.AllowUpdate = num5 != 0;
    PXCache cache3 = ((PXSelectBase) this.Details).Cache;
    isExternal = row1.IsExternal;
    int num6 = !isExternal.GetValueOrDefault() ? 1 : 0;
    cache3.AllowDelete = num6 != 0;
    TaxPlugin taxPlugin = PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelect<TaxPlugin, Where<TaxPlugin.taxPluginID, Equal<Required<TaxPlugin.taxPluginID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row1.TaxPluginID
    }));
    bool flag4 = taxPlugin != null && taxPlugin.PluginTypeName != null && taxPlugin.PluginTypeName.Contains("PX.TaxProvider.AvalaraRest.AvalaraRestTaxProvider");
    PXCache pxCache4 = cache;
    object row5 = e.Row;
    isExternal = row1.IsExternal;
    int num7 = isExternal.GetValueOrDefault() & flag4 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<TaxZone.externalAPTaxType>(pxCache4, row5, num7 != 0);
  }

  protected virtual void _(PX.Data.Events.RowSelected<TaxZoneAddressMapping> e)
  {
    PXDefaultAttribute.SetPersistingCheck<TaxZoneAddressMapping.stateID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TaxZoneAddressMapping>>) e).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<TaxZoneAddressMapping.fromPostalCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TaxZoneAddressMapping>>) e).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "P" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<TaxZoneAddressMapping.toPostalCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<TaxZoneAddressMapping>>) e).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "P" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetVisibility<TaxZoneAddressMapping.countryID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "C" ? (PXUIVisibility) 3 : (PXUIVisibility) 33);
    PXUIFieldAttribute.SetVisibility<TaxZoneAddressMapping.stateID>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "S" ? (PXUIVisibility) 3 : (PXUIVisibility) 33);
    PXUIFieldAttribute.SetVisibility<TaxZoneAddressMapping.fromPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "P" ? (PXUIVisibility) 3 : (PXUIVisibility) 33);
    PXUIFieldAttribute.SetVisibility<TaxZoneAddressMapping.toPostalCode>(((PXSelectBase) this.TaxZoneAddressMappings).Cache, (object) e.Row, ((PXSelectBase<TaxZone>) this.TxZone).Current.MappingType == "P" ? (PXUIVisibility) 3 : (PXUIVisibility) 33);
  }

  protected virtual void TaxZone_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is TaxZone newRow) || !(e.Row is TaxZone))
      return;
    cache.SetValueExt<TaxZone.taxID>((object) newRow, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<TaxZoneAddressMapping, TaxZoneAddressMapping.fromPostalCode> e)
  {
    if (((PXSelectBase) this.TxZone).Cache.GetValueOriginal<TaxZone.countryID>((object) ((PXSelectBase<TaxZone>) this.TxZone).Current) as string == string.Empty)
      return;
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(e.Row.ToPostalCode) || ((PXSelectBase<TaxZone>) this.TxZone)?.Current?.MappingType != "P" || string.Compare(e.Row.ToPostalCode, newValue) >= 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TaxZoneAddressMapping, TaxZoneAddressMapping.fromPostalCode>>) e).Cache.RaiseExceptionHandling<TaxZoneAddressMapping.toPostalCode>((object) e.Row, (object) e.Row.ToPostalCode, (Exception) new PXSetPropertyException("The value must not be less than {0}.", (PXErrorLevel) 4, new object[1]
    {
      (object) newValue
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.fromPostalCode> e)
  {
    if (((PXSelectBase) this.TxZone).Cache.GetValueOriginal<TaxZone.countryID>((object) ((PXSelectBase<TaxZone>) this.TxZone).Current) as string == string.Empty)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.fromPostalCode>>) e).Cancel = true;
    }
    else
    {
      string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.fromPostalCode>, TaxZoneAddressMapping, object>) e).NewValue as string;
      if (string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(e.Row.ToPostalCode) || e.OldValue == null || ((PXSelectBase<TaxZone>) this.TxZone)?.Current?.MappingType != "P" && string.Compare(e.Row.ToPostalCode, newValue) < 0)
        return;
      TaxZoneAddressMapping recordByPostalCode = this.FindOverlapingRecordByPostalCode(newValue, e.Row.ToPostalCode);
      if (recordByPostalCode != null)
        throw new PXSetPropertyException("The {0} - {1} range of postal codes has already been associated with the {2} tax zone.", (PXErrorLevel) 4, new object[3]
        {
          (object) recordByPostalCode.FromPostalCode,
          (object) recordByPostalCode.ToPostalCode,
          (object) recordByPostalCode.TaxZoneID
        });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.toPostalCode> e)
  {
    if (((PXSelectBase) this.TxZone).Cache.GetValueOriginal<TaxZone.countryID>((object) ((PXSelectBase<TaxZone>) this.TxZone).Current) as string == string.Empty)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.toPostalCode>>) e).Cancel = true;
    }
    else
    {
      string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.toPostalCode>, TaxZoneAddressMapping, object>) e).NewValue as string;
      if (string.IsNullOrEmpty(newValue) || string.IsNullOrEmpty(e.Row.FromPostalCode) || e.OldValue == null || ((PXSelectBase<TaxZone>) this.TxZone)?.Current?.MappingType != "P")
        return;
      if (string.Compare(newValue, e.Row.FromPostalCode) < 0)
        throw new PXSetPropertyException("The value must not be less than {0}.", (PXErrorLevel) 4, new object[1]
        {
          (object) e.Row.FromPostalCode
        });
      TaxZoneAddressMapping recordByPostalCode = this.FindOverlapingRecordByPostalCode(e.Row.FromPostalCode, newValue);
      if (recordByPostalCode != null)
        throw new PXSetPropertyException("The {0} - {1} range of postal codes has already been associated with the {2} tax zone.", (PXErrorLevel) 4, new object[3]
        {
          (object) recordByPostalCode.FromPostalCode,
          (object) recordByPostalCode.ToPostalCode,
          (object) recordByPostalCode.TaxZoneID
        });
    }
  }

  protected virtual TaxZoneAddressMapping FindOverlapingRecordByPostalCode(
    string fromPostalCode,
    string toPostalCode)
  {
    TaxZone current = ((PXSelectBase<TaxZone>) this.TxZoneCurrent).Current;
    TaxZoneAddressMapping recordByPostalCode = PXResultset<TaxZoneAddressMapping>.op_Implicit(PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.taxZoneID, NotEqual<P.AsString>>>>, And<BqlOperand<TaxZoneAddressMapping.countryID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<TaxZoneAddressMapping.stateID, IBqlString>.IsEqual<P.AsString>>>>.And<Not<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.fromPostalCode, Greater<P.AsString>>>>>.Or<BqlOperand<Required<Parameter.ofString>, IBqlString>.IsGreater<TaxZoneAddressMapping.toPostalCodeSuffixed>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) current.TaxZoneID,
      (object) current.CountryID,
      (object) string.Empty,
      (object) (toPostalCode + "ZZZZZZZZZZZZZZZZZZZZ"),
      (object) fromPostalCode
    }));
    if (recordByPostalCode == null)
    {
      TaxZoneAddressMapping zoneAddressMapping = GraphHelper.RowCast<TaxZoneAddressMapping>((IEnumerable) ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Select(Array.Empty<object>())).Where<TaxZoneAddressMapping>((Func<TaxZoneAddressMapping, bool>) (a => !string.IsNullOrEmpty(a.FromPostalCode) && !string.IsNullOrEmpty(a.ToPostalCode) && a.FromPostalCode.CompareTo(toPostalCode + "ZZZZZZZZZZZZZZZZZZZZ") <= 0 && (a.ToPostalCode + "ZZZZZZZZZZZZZZZZZZZZ").CompareTo(fromPostalCode) >= 0)).FirstOrDefault<TaxZoneAddressMapping>();
      recordByPostalCode = zoneAddressMapping == null || ((object) ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Current).Equals((object) zoneAddressMapping) ? (TaxZoneAddressMapping) null : zoneAddressMapping;
    }
    return recordByPostalCode;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<TaxZone, TaxZone.mappingType> e)
  {
    TaxZone row = e.Row;
    if (row == null)
      return;
    PXResultset<TaxZoneAddressMapping> pxResultset = ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Select(Array.Empty<object>());
    if (pxResultset.Count == 0 || ((PXSelectBase<TaxZone>) this.TxZone).Ask("Warning", "Ship-to address mapping already exists for the current tax zone. Do you want to clear it and proceed?", (MessageButtons) 4) == 6)
    {
      foreach (PXResult<TaxZoneAddressMapping> pxResult in pxResultset)
        ((PXSelectBase) this.TaxZoneAddressMappings).Cache.Delete((object) PXResult<TaxZoneAddressMapping>.op_Implicit(pxResult));
      ((PXSelectBase) this.TaxZoneAddressMappings).View.Clear();
      ((PXSelectBase) this.TaxZoneAddressMappings).View.RequestRefresh();
      if (!(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<TaxZone, TaxZone.mappingType>>) e).NewValue as string == "C"))
        return;
      ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<TaxZone, TaxZone.mappingType>>) e).Cache.SetValueExt<TaxZone.countryID>((object) row, (object) null);
    }
    else
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<TaxZone, TaxZone.mappingType>>) e).NewValue = e.OldValue;
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<TaxZone, TaxZone.countryID> e)
  {
    if (e?.Row == null || ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<TaxZone, TaxZone.countryID>>) e).Cache.GetValueOriginal<TaxZone.countryID>((object) e.Row) as string == string.Empty)
      return;
    PXResultset<TaxZoneAddressMapping> pxResultset = ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Select(Array.Empty<object>());
    if (pxResultset.Count == 0)
      return;
    if (((PXSelectBase<TaxZone>) this.TxZone).Ask("Warning", "Ship-to address mapping already exists for the current tax zone. Do you want to clear it and proceed?", (MessageButtons) 4) == 6)
    {
      foreach (PXResult<TaxZoneAddressMapping> pxResult in pxResultset)
        ((PXSelectBase) this.TaxZoneAddressMappings).Cache.Delete((object) PXResult<TaxZoneAddressMapping>.op_Implicit(pxResult));
      ((PXSelectBase) this.TaxZoneAddressMappings).View.Clear();
      ((PXSelectBase) this.TaxZoneAddressMappings).View.RequestRefresh();
    }
    else
      ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<TaxZone, TaxZone.countryID>>) e).NewValue = e.OldValue;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<TaxZone, TaxZone.countryID> e)
  {
    if (e.Row != null && !(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<TaxZone, TaxZone.countryID>>) e).Cache.GetValueOriginal<TaxZone.countryID>((object) e.Row) as string == string.Empty))
      return;
    foreach (PXResult<TaxZoneAddressMapping> pxResult in ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Select(Array.Empty<object>()))
    {
      TaxZoneAddressMapping zoneAddressMapping1 = PXResult<TaxZoneAddressMapping>.op_Implicit(pxResult);
      TaxZoneAddressMapping instance = (TaxZoneAddressMapping) ((PXSelectBase) this.TaxZoneAddressMappings).Cache.CreateInstance();
      instance.CountryID = e.NewValue as string;
      instance.TaxZoneID = zoneAddressMapping1.TaxZoneID;
      instance.StateID = zoneAddressMapping1.StateID;
      instance.FromPostalCode = zoneAddressMapping1.FromPostalCode;
      instance.ToPostalCode = zoneAddressMapping1.ToPostalCode;
      TaxZoneAddressMapping zoneAddressMapping2 = (TaxZoneAddressMapping) null;
      try
      {
        zoneAddressMapping2 = ((PXSelectBase) this.TaxZoneAddressMappings).Cache.Insert((object) instance) as TaxZoneAddressMapping;
      }
      finally
      {
        if (zoneAddressMapping2 != null)
          ((PXSelectBase) this.TaxZoneAddressMappings).Cache.Delete((object) zoneAddressMapping1);
      }
    }
    ((PXSelectBase) this.TaxZoneAddressMappings).View.RequestRefresh();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID> e)
  {
    TaxZone row = e.Row;
    if (row == null || string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID>, TaxZone, object>) e).NewValue as string) || PXSelectorAttribute.Select<TaxZone.countryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID>>) e).Cache, (object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID>, TaxZone, object>) e).NewValue) is PX.Objects.CS.Country)
      return;
    if (row.MappingType != "C")
      throw new PXSetPropertyException("The '{0}' country cannot be found in the system.", new object[1]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID>, TaxZone, object>) e).NewValue
      });
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZone, TaxZone.countryID>, TaxZone, object>) e).NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID> e)
  {
    TaxZoneAddressMapping row = e.Row;
    if (row == null || string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>, TaxZoneAddressMapping, object>) e).NewValue as string))
      return;
    if (!(PXSelectorAttribute.Select<TaxZoneAddressMapping.countryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>>) e).Cache, (object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>, TaxZoneAddressMapping, object>) e).NewValue) is PX.Objects.CS.Country))
      throw new PXSetPropertyException("The '{0}' country cannot be found in the system.", (PXErrorLevel) 4, new object[1]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>, TaxZoneAddressMapping, object>) e).NewValue
      });
    if (row == null || string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>, TaxZoneAddressMapping, object>) e).NewValue as string) || !(((PXSelectBase<TaxZone>) this.TxZoneCurrent).Current?.MappingType == "C"))
      return;
    TaxZoneAddressMapping byCountryAndState = this.FindDuplicateRecordByCountryAndState((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.countryID>, TaxZoneAddressMapping, object>) e).NewValue, string.Empty);
    if (byCountryAndState != null)
      throw new PXSetPropertyException("The {0} tax zone has already been associated with the {1} country.", (PXErrorLevel) 4, new object[2]
      {
        (object) byCountryAndState.TaxZoneID,
        (object) byCountryAndState.CountryID
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.stateID> e)
  {
    if (e.Row == null || string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.stateID>, TaxZoneAddressMapping, object>) e).NewValue as string) || e.OldValue == null)
      return;
    TaxZone current = ((PXSelectBase<TaxZone>) this.TxZoneCurrent).Current;
    if (!(current?.MappingType == "S"))
      return;
    TaxZoneAddressMapping byCountryAndState = this.FindDuplicateRecordByCountryAndState(current.CountryID, (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TaxZoneAddressMapping, TaxZoneAddressMapping.stateID>, TaxZoneAddressMapping, object>) e).NewValue);
    if (byCountryAndState != null)
      throw new PXSetPropertyException("The {0} tax zone has already been associated with the {1} state.", (PXErrorLevel) 4, new object[2]
      {
        (object) byCountryAndState.TaxZoneID,
        (object) byCountryAndState.StateID
      });
  }

  protected virtual TaxZoneAddressMapping FindDuplicateRecordByCountryAndState(
    string countryId,
    string stateId)
  {
    return PXResultset<TaxZoneAddressMapping>.op_Implicit(PXSelectBase<TaxZoneAddressMapping, PXViewOf<TaxZoneAddressMapping>.BasedOn<SelectFromBase<TaxZoneAddressMapping, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneAddressMapping.taxZoneID, NotEqual<P.AsString>>>>, And<BqlOperand<TaxZoneAddressMapping.countryID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<TaxZoneAddressMapping.stateID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<TaxZoneAddressMapping.fromPostalCode, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<TaxZoneAddressMapping.toPostalCode, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) ((PXSelectBase<TaxZone>) this.TxZoneCurrent).Current.TaxZoneID,
      (object) countryId,
      (object) stateId,
      (object) string.Empty,
      (object) string.Empty
    })) ?? GraphHelper.RowCast<TaxZoneAddressMapping>((IEnumerable) ((PXSelectBase<TaxZoneAddressMapping>) this.TaxZoneAddressMappings).Select(Array.Empty<object>())).Where<TaxZoneAddressMapping>((Func<TaxZoneAddressMapping, bool>) (a => a.CountryID == countryId && a.StateID == stateId)).FirstOrDefault<TaxZoneAddressMapping>();
  }

  protected virtual bool TryValidateTaxCategoryCombinationWithDirectTax(
    string[] taxIds,
    out PXResultset<TaxCategoryDet> invalidCategoryCombinations)
  {
    if (taxIds.Length < 1)
    {
      invalidCategoryCombinations = new PXResultset<TaxCategoryDet>();
      return true;
    }
    invalidCategoryCombinations = PXSelectBase<TaxCategoryDet, PXViewOf<TaxCategoryDet>.BasedOn<SelectFromBase<TaxCategoryDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<TaxCategory>.On<BqlOperand<TaxCategory.taxCategoryID, IBqlString>.IsEqual<TaxCategoryDet.taxCategoryID>>>, FbqlJoins.Inner<Tax>.On<BqlOperand<Tax.taxID, IBqlString>.IsEqual<TaxCategoryDet.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxID, In<P.AsString>>>>, And<BqlOperand<TaxCategory.taxCatFlag, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<Tax.directTax, IBqlBool>.IsEqual<True>>>.Aggregate<To<GroupBy<TaxCategoryDet.taxCategoryID>, Count<TaxCategoryDet.taxID>>>.Having<BqlAggregatedOperand<Count<TaxCategoryDet.taxID>, IBqlInt>.IsGreater<decimal1>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taxIds
    });
    return invalidCategoryCombinations.Count == 0;
  }
}
