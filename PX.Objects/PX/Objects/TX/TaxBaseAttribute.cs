// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.TX;

public abstract class TaxBaseAttribute : 
  PXAggregateAttribute,
  IPXRowInsertedSubscriber,
  IPXRowUpdatedSubscriber,
  IPXRowDeletedSubscriber,
  IPXRowPersistedSubscriber,
  IComparable
{
  protected const Decimal _negligibleDifference = 0.00005M;
  protected 
  #nullable disable
  string _LineNbr = "LineNbr";
  protected string _CuryOrigTaxableAmt = "CuryOrigTaxableAmt";
  protected string _CuryTaxAmt = "CuryTaxAmt";
  protected string _CuryTaxAmtSumm = "CuryTaxAmtSumm";
  protected string _CuryTaxDiscountAmt = "CuryTaxDiscountAmt";
  protected string _CuryTaxableAmt = "CuryTaxableAmt";
  protected string _CuryExemptedAmt = "CuryExemptedAmt";
  protected string _CuryTaxableDiscountAmt = "CuryTaxableDiscountAmt";
  protected string _CuryExpenseAmt = "CuryExpenseAmt";
  protected string _CuryRateTypeID = "CuryRateTypeID";
  protected string _CuryEffDate = "CuryEffDate";
  protected string _CuryRate = "SampleCuryRate";
  protected string _RecipRate = "SampleRecipRate";
  protected string _IsTaxSaved = "IsTaxSaved";
  protected string _RecordID = "RecordID";
  protected string _ExternalTaxesImportInProgress = "ExternalTaxesImportInProgress";
  protected string _IsDirectTaxLine = "IsDirectTaxLine";
  protected string _IsTaxInclusive = "IsTaxInclusive";
  protected Type _ParentType;
  protected Type _ChildType;
  protected Type _TaxType;
  protected Type _TaxSumType;
  protected Type _CuryKeyField;
  protected Dictionary<object, object> inserted;
  protected Dictionary<object, object> updated;
  protected bool _IncludeDirectTaxLine;
  protected string _TaxID = nameof (TaxID);
  protected string _TaxCategoryID = nameof (TaxCategoryID);
  protected string _TaxZoneID = nameof (TaxZoneID);
  protected string _DocDate = nameof (DocDate);
  protected string _FinPeriodID = nameof (FinPeriodID);
  protected Type CuryTranAmt = typeof (TaxBaseAttribute.curyTranAmt);
  protected Type OrigGroupDiscountRate = typeof (TaxBaseAttribute.origGroupDiscountRate);
  protected Type OrigDocumentDiscountRate = typeof (TaxBaseAttribute.origDocumentDiscountRate);
  protected Type GroupDiscountRate = typeof (TaxBaseAttribute.groupDiscountRate);
  protected Type DocumentDiscountRate = typeof (TaxBaseAttribute.documentDiscountRate);
  protected string _TermsID = nameof (TermsID);
  protected string _CuryID = nameof (CuryID);
  protected string _CuryDocBal = nameof (CuryDocBal);
  protected string _CuryTaxDiscountTotal = "CuryOrigTaxDiscAmt";
  protected string _CuryTaxTotal = nameof (CuryTaxTotal);
  protected string _CuryTaxInclTotal;
  protected string _CuryOrigDiscAmt = nameof (CuryOrigDiscAmt);
  protected string _CuryWhTaxTotal = "CuryOrigWhTaxAmt";
  public Type CuryLineTotal = typeof (TaxBaseAttribute.curyLineTotal);
  protected Type CuryDiscTot = typeof (TaxBaseAttribute.curyDiscTot);
  protected TaxCalc _TaxCalc = TaxCalc.Calc;
  protected TaxCalc _TaxFlags;
  protected string _TaxCalcMode;
  private bool? netGrossEntryModeEnable;
  protected bool _NoSumTaxable;
  protected Dictionary<object, bool> OrigDiscAmtExtCallDict = new Dictionary<object, bool>();
  protected Dictionary<object, Decimal?> DiscPercentsDict = new Dictionary<object, Decimal?>();
  protected object _ParentRow;
  private Type _uom;
  private Type _inventory;
  private Type _lineQty;

  public Type TaxID
  {
    set => this._TaxID = value.Name;
    get => (Type) null;
  }

  public Type TaxCategoryID
  {
    set => this._TaxCategoryID = value.Name;
    get => (Type) null;
  }

  public Type TaxZoneID
  {
    set => this._TaxZoneID = value.Name;
    get => (Type) null;
  }

  public Type DocDate
  {
    set => this._DocDate = value.Name;
    get => (Type) null;
  }

  public Type ParentBranchIDField { get; set; }

  public Type FinPeriodID
  {
    set => this._FinPeriodID = value.Name;
    get => (Type) null;
  }

  protected string _CuryTranAmt => this.CuryTranAmt.Name;

  protected string _OrigGroupDiscountRate => this.OrigGroupDiscountRate.Name;

  protected string _OrigDocumentDiscountRate => this.OrigDocumentDiscountRate.Name;

  protected string _GroupDiscountRate => this.GroupDiscountRate.Name;

  protected string _DocumentDiscountRate => this.DocumentDiscountRate.Name;

  public Type TermsID
  {
    set => this._TermsID = value.Name;
    get => (Type) null;
  }

  public Type CuryID
  {
    set => this._CuryID = value.Name;
    get => (Type) null;
  }

  public Type CuryDocBal
  {
    set => this._CuryDocBal = value != (Type) null ? value.Name : (string) null;
    get => (Type) null;
  }

  public Type CuryDocBalUndiscounted
  {
    set => this._CuryTaxDiscountTotal = value != (Type) null ? value.Name : (string) null;
    get => (Type) null;
  }

  public Type CuryTaxTotal
  {
    set => this._CuryTaxTotal = value.Name;
    get => (Type) null;
  }

  public Type CuryTaxInclTotal
  {
    set => this._CuryTaxInclTotal = value?.Name;
  }

  public Type CuryOrigDiscAmt
  {
    set => this._CuryOrigDiscAmt = value.Name;
    get => (Type) null;
  }

  public Type CuryWhTaxTotal
  {
    set => this._CuryWhTaxTotal = value.Name;
    get => (Type) null;
  }

  protected string _CuryLineTotal => this.CuryLineTotal.Name;

  protected string _CuryDiscTot => this.CuryDiscTot.Name;

  public TaxCalc TaxCalc
  {
    set => this._TaxCalc = value;
    get => this._TaxCalc;
  }

  public TaxCalc TaxFlags
  {
    set => this._TaxFlags = value;
    get => this._TaxFlags;
  }

  public Type TaxCalcMode
  {
    set => this._TaxCalcMode = value.Name;
    get => (Type) null;
  }

  protected virtual bool _isTaxCalcModeEnabled
  {
    get => !string.IsNullOrEmpty(this._TaxCalcMode) && this._NetGrossEntryModeEnabled;
  }

  protected virtual bool _NetGrossEntryModeEnabled
  {
    get
    {
      if (!this.netGrossEntryModeEnable.HasValue)
        this.netGrossEntryModeEnable = new bool?(PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>());
      return this.netGrossEntryModeEnable.GetValueOrDefault();
    }
  }

  public int? Precision { get; set; }

  protected virtual string RetainageApplyFieldName => "RetainageApply";

  protected virtual bool CalcGrossOnDocumentLevel { get; set; }

  protected virtual bool AskRecalculationOnCalcModeChange { get; set; }

  protected virtual string _PreviousTaxCalcMode { get; set; }

  public Type ChildBranchIDField { get; set; }

  public Type ChildFinPeriodIDField { get; set; }

  public static List<PXEventSubscriberAttribute> GetAttributes<Field, Target>(
    PXCache sender,
    object data)
    where Field : IBqlField
    where Target : TaxAttribute
  {
    bool exactfind = false;
    List<PXEventSubscriberAttribute> attributes = new List<PXEventSubscriberAttribute>();
    foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributes<Field>(data).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (attr =>
    {
      if (exactfind && data != null)
        return false;
      if (exactfind = attr.GetType() == typeof (Target))
        return true;
      return attr is TaxAttribute && typeof (Target) == typeof (TaxAttribute);
    })))
    {
      subscriberAttribute.IsDirty = true;
      attributes.Add(subscriberAttribute);
    }
    attributes.Sort((Comparison<PXEventSubscriberAttribute>) ((a, b) => ((IComparable) a).CompareTo((object) b)));
    return attributes;
  }

  public static void SetTaxCalc<Field>(PXCache cache, object data, TaxCalc isTaxCalc) where Field : IBqlField
  {
    TaxBaseAttribute.SetTaxCalc<Field, TaxAttribute>(cache, data, isTaxCalc);
  }

  public static void SetTaxCalc<Field, Target>(PXCache cache, object data, TaxCalc isTaxCalc)
    where Field : IBqlField
    where Target : TaxAttribute
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in TaxBaseAttribute.GetAttributes<Field, Target>(cache, data))
    {
      PXEventSubscriberAttribute subscriberAttribute;
      ((TaxBaseAttribute) (subscriberAttribute = attribute)).TaxCalc = (TaxCalc) ((int) (short) isTaxCalc & 3);
      ((TaxBaseAttribute) subscriberAttribute).TaxFlags = (TaxCalc) ((int) (short) isTaxCalc & 12);
    }
  }

  public static TaxCalc GetTaxCalc<Field>(PXCache cache, object data) where Field : IBqlField
  {
    return TaxBaseAttribute.GetTaxCalc<Field, TaxAttribute>(cache, data);
  }

  public static TaxCalc GetTaxCalc<Field, Target>(PXCache cache, object data)
    where Field : IBqlField
    where Target : TaxAttribute
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (TaxBaseAttribute attribute in TaxBaseAttribute.GetAttributes<Field, Target>(cache, data))
    {
      if (attribute.TaxCalc != TaxCalc.NoCalc)
        return TaxCalc.Calc;
    }
    return TaxCalc.NoCalc;
  }

  public static void IncludeDirectTaxLine<Field>(
    PXCache cache,
    object data,
    bool includeDirectTaxLine)
    where Field : IBqlField
  {
    if (data == null)
      cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is TaxBaseAttribute)
        ((TaxBaseAttribute) attribute)._IncludeDirectTaxLine = includeDirectTaxLine;
    }
  }

  public virtual object Insert(PXCache cache, object item) => cache.Insert(item);

  public virtual object Update(PXCache cache, object item) => cache.Update(item);

  public virtual object Delete(PXCache cache, object item) => cache.Delete(item);

  public static void Calculate<Field>(PXCache sender, PXRowInsertedEventArgs e) where Field : IBqlField
  {
    TaxBaseAttribute.Calculate<Field, TaxAttribute>(sender, e);
  }

  public static bool IsDirectTaxLine<Field>(PXCache cache, object data) where Field : IBqlField
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>(data))
    {
      if (attribute is TaxBaseAttribute && ((TaxBaseAttribute) attribute).IsDirectTaxLine(cache, data))
        return true;
    }
    return false;
  }

  public static void Calculate<Field, Target>(PXCache sender, PXRowInsertedEventArgs e)
    where Field : IBqlField
    where Target : TaxAttribute
  {
    bool flag = false;
    foreach (PXEventSubscriberAttribute attribute in TaxBaseAttribute.GetAttributes<Field, Target>(sender, e.Row))
    {
      flag = true;
      if (((TaxBaseAttribute) attribute).TaxCalc == TaxCalc.ManualLineCalc)
      {
        ((TaxBaseAttribute) attribute).TaxCalc = TaxCalc.Calc;
        try
        {
          ((IPXRowInsertedSubscriber) attribute).RowInserted(sender, e);
        }
        finally
        {
          ((TaxBaseAttribute) attribute).TaxCalc = TaxCalc.ManualLineCalc;
        }
      }
      object obj;
      if (((TaxBaseAttribute) attribute).TaxCalc == TaxCalc.ManualCalc && ((TaxBaseAttribute) attribute).inserted.TryGetValue(e.Row, out obj))
      {
        ((IPXRowUpdatedSubscriber) attribute).RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
        ((TaxBaseAttribute) attribute).inserted.Remove(e.Row);
        if (((TaxBaseAttribute) attribute).updated.TryGetValue(e.Row, out obj))
          ((TaxBaseAttribute) attribute).updated.Remove(e.Row);
      }
    }
    if (flag)
      return;
    TaxBaseAttribute.InvokeRecalcTaxes(sender);
  }

  public static void Calculate<Field>(PXCache sender, PXRowUpdatedEventArgs e) where Field : IBqlField
  {
    TaxBaseAttribute.Calculate<Field, TaxAttribute>(sender, e);
  }

  public static void Calculate<Field, Target>(PXCache sender, PXRowUpdatedEventArgs e)
    where Field : IBqlField
    where Target : TaxAttribute
  {
    foreach (PXEventSubscriberAttribute attribute in TaxBaseAttribute.GetAttributes<Field, Target>(sender, e.Row))
    {
      if (((TaxBaseAttribute) attribute).TaxCalc == TaxCalc.ManualLineCalc)
      {
        ((TaxBaseAttribute) attribute).TaxCalc = TaxCalc.Calc;
        try
        {
          ((IPXRowUpdatedSubscriber) attribute).RowUpdated(sender, e);
        }
        finally
        {
          ((TaxBaseAttribute) attribute).TaxCalc = TaxCalc.ManualLineCalc;
        }
      }
      object obj;
      if (((TaxBaseAttribute) attribute).TaxCalc == TaxCalc.ManualCalc && ((TaxBaseAttribute) attribute).updated.TryGetValue(e.Row, out obj))
      {
        ((IPXRowUpdatedSubscriber) attribute).RowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
        ((TaxBaseAttribute) attribute).updated.Remove(e.Row);
      }
    }
  }

  internal static void InvokeRecalcTaxes(PXCache sender)
  {
    sender.Graph.FindImplementation<ITaxRecalculator>()?.RecalcTaxes();
  }

  private static bool IsInstanceOfGenericType(Type genericType, object instance)
  {
    for (Type type = instance.GetType(); type != (Type) null; type = type.BaseType)
    {
      if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        return true;
    }
    return false;
  }

  protected virtual string GetTaxZone(PXCache sender, object row)
  {
    return (string) this.ParentGetValue(sender.Graph, this._TaxZoneID);
  }

  protected virtual DateTime? GetDocDate(PXCache sender, object row)
  {
    return (DateTime?) this.ParentGetValue(sender.Graph, this._DocDate);
  }

  protected virtual string GetTaxCategory(PXCache sender, object row)
  {
    return (string) sender.GetValue(row, this._TaxCategoryID);
  }

  protected virtual bool? GetIsDirectTaxLine(PXCache sender, object row)
  {
    return (bool?) sender.GetValue(row, this._IsDirectTaxLine);
  }

  protected virtual Decimal? GetCuryTranAmt(PXCache sender, object row, string TaxCalcType = "I")
  {
    return (Decimal?) sender.GetValue(row, this._CuryTranAmt);
  }

  protected virtual Decimal? GetDocLineFinalAmtNoRounding(
    PXCache sender,
    object row,
    string TaxCalcType = "I")
  {
    return (Decimal?) sender.GetValue(row, this._CuryTranAmt);
  }

  protected virtual string GetTaxID(PXCache sender, object row)
  {
    return (string) sender.GetValue(row, this._TaxID);
  }

  protected virtual object InitializeTaxDet(object data) => data;

  public virtual bool IsExternalTax(PXGraph graph, string taxZoneID)
  {
    TaxZone taxZone = PXResultset<TaxZone>.op_Implicit(PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    }));
    return taxZone != null && (taxZone.IsExternal ?? false) && !string.IsNullOrEmpty(taxZone.TaxPluginID);
  }

  protected virtual void AddOneTax(PXCache cache, object detrow, ITaxDetail taxitem)
  {
    if (taxitem == null)
      return;
    object child;
    TaxParentAttribute.NewChild(cache, detrow, this._ChildType, out child);
    ((ITaxDetail) child).TaxID = taxitem.TaxID;
    object obj1 = this.InitializeTaxDet(child);
    object obj2 = this.Insert(cache, obj1);
    if (obj2 == null)
      return;
    PXParentAttribute.SetParent(cache, obj2, this._ChildType, detrow);
  }

  public virtual ITaxDetail MatchesCategory(PXCache sender, object row, ITaxDetail zoneitem)
  {
    string taxCategory = this.GetTaxCategory(sender, row);
    string taxId = this.GetTaxID(sender, row);
    DateTime? docDate = this.GetDocDate(sender, row);
    if (PXResultset<TaxRev>.op_Implicit(PXSelectBase<TaxRev, PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Required<TaxRev.taxID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) zoneitem.TaxID,
      (object) docDate
    })) == null)
      return (ITaxDetail) null;
    if (string.Equals(taxId, zoneitem.TaxID))
      return zoneitem;
    if (PXResultset<TaxCategory>.op_Implicit(PXSelectBase<TaxCategory, PXSelect<TaxCategory, Where<TaxCategory.taxCategoryID, Equal<Required<TaxCategory.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) taxCategory
    })) == null)
      return (ITaxDetail) null;
    return this.MatchesCategory(sender, row, (IEnumerable<ITaxDetail>) new ITaxDetail[1]
    {
      zoneitem
    }).FirstOrDefault<ITaxDetail>();
  }

  public virtual IEnumerable<ITaxDetail> MatchesCategory(
    PXCache sender,
    object row,
    IEnumerable<ITaxDetail> zonetaxlist)
  {
    string taxCategory1 = this.GetTaxCategory(sender, row);
    List<ITaxDetail> taxDetailList = new List<ITaxDetail>();
    TaxCategory taxCategory2 = PXResultset<TaxCategory>.op_Implicit(PXSelectBase<TaxCategory, PXSelect<TaxCategory, Where<TaxCategory.taxCategoryID, Equal<Required<TaxCategory.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) taxCategory1
    }));
    if (taxCategory2 == null)
      return (IEnumerable<ITaxDetail>) taxDetailList;
    HashSet<string> stringSet = new HashSet<string>();
    foreach (PXResult<TaxCategoryDet> pxResult in PXSelectBase<TaxCategoryDet, PXSelect<TaxCategoryDet, Where<TaxCategoryDet.taxCategoryID, Equal<Required<TaxCategoryDet.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) taxCategory1
    }))
    {
      TaxCategoryDet taxCategoryDet = PXResult<TaxCategoryDet>.op_Implicit(pxResult);
      stringSet.Add(taxCategoryDet.TaxID);
    }
    foreach (ITaxDetail taxDetail in zonetaxlist)
    {
      bool flag1 = stringSet.Contains(taxDetail.TaxID);
      bool? taxCatFlag = taxCategory2.TaxCatFlag;
      bool flag2 = false;
      if (!(taxCatFlag.GetValueOrDefault() == flag2 & taxCatFlag.HasValue & flag1))
      {
        taxCatFlag = taxCategory2.TaxCatFlag;
        if (!taxCatFlag.GetValueOrDefault() || flag1)
          continue;
      }
      taxDetailList.Add(taxDetail);
    }
    return (IEnumerable<ITaxDetail>) taxDetailList;
  }

  protected abstract IEnumerable<ITaxDetail> ManualTaxes(PXCache sender, object row);

  protected virtual void DefaultTaxes(PXCache sender, object row, bool DefaultExisting)
  {
    PXCache cach = sender.Graph.Caches[this._TaxType];
    string taxZone = this.GetTaxZone(sender, row);
    string taxCategory = this.GetTaxCategory(sender, row);
    DateTime? docDate = this.GetDocDate(sender, row);
    HashSet<string> stringSet = new HashSet<string>();
    bool newValue = this.IsDirectTaxLine(sender, row);
    string str = (string) null;
    foreach (PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet, Tax> pxResult in PXSelectBase<TaxZoneDet, PXSelectJoin<TaxZoneDet, CrossJoin<TaxCategory, InnerJoin<TaxRev, On<TaxRev.taxID, Equal<TaxZoneDet.taxID>>, LeftJoin<TaxCategoryDet, On<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>, And<TaxCategoryDet.taxCategoryID, Equal<TaxCategory.taxCategoryID>>>, LeftJoin<Tax, On<Tax.taxID, Equal<TaxZoneDet.taxID>>>>>>, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, And<TaxCategory.taxCategoryID, Equal<Required<TaxCategory.taxCategoryID>>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>, And<TaxRev.outdated, Equal<False>, And<Where<TaxCategory.taxCatFlag, Equal<False>, And<TaxCategoryDet.taxCategoryID, IsNotNull, Or<TaxCategory.taxCatFlag, Equal<True>, And<TaxCategoryDet.taxCategoryID, IsNull>>>>>>>>>>.Config>.Select(sender.Graph, new object[3]
    {
      (object) taxZone,
      (object) taxCategory,
      (object) docDate
    }))
    {
      Tax tax = PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet, Tax>.op_Implicit(pxResult);
      TaxZoneDet taxitem = PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet, Tax>.op_Implicit(pxResult);
      if (!newValue && !tax.DirectTax.GetValueOrDefault())
      {
        this.AddOneTax(cach, row, (ITaxDetail) taxitem);
        stringSet.Add(taxitem.TaxID);
      }
      else if (newValue && tax.DirectTax.GetValueOrDefault())
      {
        this.AddOneTax(cach, row, (ITaxDetail) taxitem);
        stringSet.Add(taxitem.TaxID);
        str = taxitem.TaxID;
      }
    }
    string taxId;
    if ((taxId = this.GetTaxID(sender, row)) != null)
    {
      this.AddOneTax(cach, row, (ITaxDetail) new TaxZoneDet()
      {
        TaxID = taxId
      });
      stringSet.Add(taxId);
    }
    foreach (ITaxDetail manualTax in this.ManualTaxes(sender, row))
    {
      if (stringSet.Contains(manualTax.TaxID))
        stringSet.Remove(manualTax.TaxID);
    }
    foreach (string taxID in stringSet)
      this.AddTaxTotals(cach, taxID, row);
    if (DefaultExisting)
    {
      foreach (ITaxDetail taxitem in this.MatchesCategory(sender, row, this.ManualTaxes(sender, row)))
      {
        Tax tax = Tax.PK.Find(sender.Graph, taxitem.TaxID);
        if (!newValue && !tax.DirectTax.GetValueOrDefault())
          this.AddOneTax(cach, row, taxitem);
        else if (newValue && str == tax.TaxID)
          this.AddOneTax(cach, row, taxitem);
      }
    }
    if (!this._IncludeDirectTaxLine || !(this.GetFieldType(sender, this._IsDirectTaxLine) != (Type) null))
      return;
    this.UpdateIsDirectTaxLineFeildValue(sender, row, newValue, this.GetIsDirectTaxLine(sender, row).GetValueOrDefault());
  }

  protected virtual void UpdateIsDirectTaxLineFeildValue(
    PXCache cache,
    object row,
    bool newValue,
    bool oldValue)
  {
    if (newValue == oldValue)
      return;
    TaxBaseAttribute.SetValueOptional(cache, row, (object) newValue, this._IsDirectTaxLine);
  }

  protected virtual bool IsDirectTaxLine(PXCache sender, object row)
  {
    if (!this._IncludeDirectTaxLine)
      return false;
    string taxZone = this.GetTaxZone(sender, row);
    string taxCategory1 = this.GetTaxCategory(sender, row);
    DateTime? docDate = this.GetDocDate(sender, row);
    if (!string.IsNullOrEmpty(taxZone) && !string.IsNullOrEmpty(taxCategory1))
    {
      TaxCategory taxCategory2 = TaxCategory.PK.Find(sender.Graph, taxCategory1);
      bool? nullable;
      int num;
      if (taxCategory2 == null)
      {
        num = 0;
      }
      else
      {
        nullable = taxCategory2.TaxCatFlag;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num == 0)
      {
        HashSet<string> stringSet = new HashSet<string>();
        HashSet<string> applicableIndirectTaxes = new HashSet<string>();
        foreach (PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet, Tax> pxResult in PXSelectBase<TaxZoneDet, PXViewOf<TaxZoneDet>.BasedOn<SelectFromBase<TaxZoneDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<TaxCategory>>, FbqlJoins.Inner<TaxRev>.On<BqlOperand<TaxRev.taxID, IBqlString>.IsEqual<TaxZoneDet.taxID>>>, FbqlJoins.Left<TaxCategoryDet>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>>>>>.And<BqlOperand<TaxCategoryDet.taxCategoryID, IBqlString>.IsEqual<TaxCategory.taxCategoryID>>>>, FbqlJoins.Left<Tax>.On<BqlOperand<Tax.taxID, IBqlString>.IsEqual<TaxZoneDet.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneDet.taxZoneID, Equal<P.AsString>>>>, And<BqlOperand<TaxCategory.taxCategoryID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<Required<Parameter.ofDateTimeUTC>, IBqlDateTime>.IsBetween<TaxRev.startDate, TaxRev.endDate>>>, And<BqlOperand<TaxRev.outdated, IBqlBool>.IsEqual<False>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategory.taxCatFlag, Equal<False>>>>>.And<BqlOperand<TaxCategoryDet.taxCategoryID, IBqlString>.IsNotNull>>>>.Config>.Select(sender.Graph, new object[3]
        {
          (object) taxZone,
          (object) taxCategory1,
          (object) docDate
        }))
        {
          Tax tax = PXResult<TaxZoneDet, TaxCategory, TaxRev, TaxCategoryDet, Tax>.op_Implicit(pxResult);
          nullable = tax.DirectTax;
          if (nullable.GetValueOrDefault())
            stringSet.Add(tax.TaxID);
          else
            applicableIndirectTaxes.Add(tax.TaxID);
        }
        if (applicableIndirectTaxes.Count == 0 && stringSet.Count == 1 && !this.SkipDirectTax(sender, row, stringSet.First<string>()))
          return true;
        this.ShowInvalidDirectTaxCombinationWarnings(sender, row, taxCategory1, stringSet, applicableIndirectTaxes);
        return false;
      }
    }
    return false;
  }

  protected virtual void ShowInvalidDirectTaxCombinationWarnings(
    PXCache sender,
    object row,
    string taxcat,
    HashSet<string> applicableDirectTaxes,
    HashSet<string> applicableIndirectTaxes)
  {
    if (applicableIndirectTaxes.Count > 0 && applicableDirectTaxes.Count > 0)
    {
      sender.RaiseExceptionHandling(this._TaxCategoryID, row, (object) taxcat, (Exception) new PXSetPropertyException("The {0} direct-entry tax cannot be applied to the document line together with the {1} non-direct-entry tax.", (PXErrorLevel) 2, new object[2]
      {
        (object) applicableDirectTaxes.First<string>(),
        (object) applicableIndirectTaxes.First<string>()
      }));
    }
    else
    {
      if (applicableIndirectTaxes.Count != 0 || applicableDirectTaxes.Count <= 1)
        return;
      sender.RaiseExceptionHandling(this._TaxCategoryID, row, (object) taxcat, (Exception) new PXSetPropertyException("The {0} tax category contains multiple direct-entry taxes that cannot be applied to the same document line.", (PXErrorLevel) 2, new object[1]
      {
        (object) taxcat
      }));
    }
  }

  protected virtual void DefaultTaxes(PXCache sender, object row)
  {
    this.DefaultTaxes(sender, row, true);
  }

  private Type GetFieldType(PXCache cache, string FieldName)
  {
    List<Type> bqlFields = cache.BqlFields;
    for (int index = 0; index < bqlFields.Count; ++index)
    {
      if (string.Compare(bqlFields[index].Name, FieldName, StringComparison.OrdinalIgnoreCase) == 0)
        return bqlFields[index];
    }
    return (Type) null;
  }

  private Type GetTaxIDType(PXCache cache)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes((string) null))
    {
      if (attribute is PXSelectorAttribute && ((PXSelectorAttribute) attribute).Field == typeof (Tax.taxID))
        return this.GetFieldType(cache, attribute.FieldName);
    }
    return (Type) null;
  }

  private Type AddWhere(Type command, Type where)
  {
    if (!command.IsGenericType)
      return (Type) null;
    Type[] genericArguments = command.GetGenericArguments();
    Type[] typeArray = new Type[genericArguments.Length + 1];
    typeArray[0] = command.GetGenericTypeDefinition();
    for (int index = 0; index < genericArguments.Length; ++index)
    {
      if (genericArguments[index].IsGenericType && (genericArguments[index].GetGenericTypeDefinition() == typeof (Where<,>) || genericArguments[index].GetGenericTypeDefinition() == typeof (Where2<,>) || genericArguments[index].GetGenericTypeDefinition() == typeof (Where<,,>)))
        typeArray[index + 1] = typeof (Where2<,>).MakeGenericType(genericArguments[index], typeof (And<>).MakeGenericType(where));
      else
        typeArray[index + 1] = genericArguments[index];
    }
    return BqlCommand.Compose(typeArray);
  }

  protected List<object> SelectTaxes(PXCache sender, object row, PXTaxCheck taxchk)
  {
    return this.SelectTaxes<Where<True, Equal<True>>>(sender.Graph, row, taxchk);
  }

  protected abstract List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
    where Where : IBqlWhere, new();

  protected abstract List<object> SelectDocumentLines(PXGraph graph, object row);

  protected Tax AdjustTaxLevel(PXGraph graph, Tax taxToAdjust)
  {
    if (this._isTaxCalcModeEnabled && taxToAdjust.TaxCalcLevel != "2" && !taxToAdjust.DirectTax.GetValueOrDefault())
    {
      string taxCalcMode = this.GetTaxCalcMode(graph);
      if (!string.IsNullOrEmpty(taxCalcMode))
      {
        Tax copy = (Tax) graph.Caches[typeof (Tax)].CreateCopy((object) taxToAdjust);
        switch (taxCalcMode)
        {
          case "G":
            copy.TaxCalcLevel = "0";
            break;
          case "N":
            copy.TaxCalcLevel = "1";
            break;
        }
        return copy;
      }
    }
    return taxToAdjust;
  }

  protected virtual void ClearTaxes(PXCache sender, object row)
  {
    PXCache cach = sender.Graph.Caches[this._TaxType];
    foreach (object selectTax in this.SelectTaxes(sender, row, PXTaxCheck.Line))
      this.Delete(cach, ((PXResult) selectTax)[0]);
  }

  private Decimal Sum(PXGraph graph, List<object> list, Type field)
  {
    if (field == (Type) null)
      return 0M;
    Type itemType = BqlCommand.GetItemType(field);
    return list.Cast<PXResult>().Select<PXResult, Decimal>((Func<PXResult, Decimal>) (a => ((Decimal?) graph.Caches[itemType].GetValue(a[itemType], field.Name)).GetValueOrDefault())).Sum();
  }

  protected virtual void AddTaxTotals(PXCache sender, string taxID, object row)
  {
    PXCache cach = sender.Graph.Caches[this._TaxSumType];
    object instance = Activator.CreateInstance(this._TaxSumType);
    ((TaxDetail) instance).TaxID = taxID;
    object obj = this.InitializeTaxDet(instance);
    this.Insert(cach, obj);
  }

  protected PX.Objects.CS.Terms SelectTerms(PXGraph graph)
  {
    string TermsID = (string) this.ParentGetValue(graph, this._TermsID);
    return TermsAttribute.SelectTerms(graph, TermsID) ?? new PX.Objects.CS.Terms();
  }

  protected virtual void SetTaxableAmt(PXCache sender, object row, Decimal? value)
  {
  }

  protected virtual void SetTaxAmt(PXCache sender, object row, Decimal? value)
  {
  }

  protected virtual bool IsDeductibleVATTax(Tax tax)
  {
    return tax != null && tax.DeductibleVAT.GetValueOrDefault();
  }

  protected virtual bool IsExemptTaxCategory(PXGraph graph, object row)
  {
    return this.IsExemptTaxCategory(graph.Caches[this._ChildType], row);
  }

  protected virtual bool IsExemptTaxCategory(PXCache sender, object row)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.exemptedTaxReporting>())
      return false;
    bool flag = false;
    string taxCategory1 = this.GetTaxCategory(sender, row);
    if (!string.IsNullOrEmpty(taxCategory1))
    {
      TaxCategory taxCategory2 = PXResultset<TaxCategory>.op_Implicit(PXSelectBase<TaxCategory, PXSelect<TaxCategory, Where<TaxCategory.taxCategoryID, Equal<Required<TaxCategory.taxCategoryID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) taxCategory1
      }));
      flag = taxCategory2 != null && taxCategory2.Exempt.GetValueOrDefault();
    }
    return flag;
  }

  protected abstract Decimal? GetTaxableAmt(PXCache sender, object row);

  protected abstract Decimal? GetTaxAmt(PXCache sender, object row);

  protected virtual List<object> SelectInclusiveTaxes(PXGraph graph, object row)
  {
    List<object> objectList = new List<object>();
    if (this.IsExemptTaxCategory(graph, row))
      return objectList;
    string str = "T";
    if (this._isTaxCalcModeEnabled)
    {
      string taxCalcMode = this.GetTaxCalcMode(graph);
      if (!string.IsNullOrEmpty(taxCalcMode))
        str = taxCalcMode;
    }
    switch (str)
    {
      case "T":
        objectList = this.SelectTaxes<Where<Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>(graph, row, PXTaxCheck.Line);
        break;
      case "G":
        objectList = this.SelectTaxes<Where<Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>(graph, row, PXTaxCheck.Line);
        break;
    }
    return objectList;
  }

  protected List<object> SelectLvl1Taxes(PXGraph graph, object row)
  {
    return !this.IsExemptTaxCategory(graph, row) ? this.SelectTaxes<Where<Tax.taxCalcLevel2Exclude, Equal<False>, And<Where<Tax.taxCalcLevel, Equal<CSTaxCalcLevel.calcOnItemAmt>, Or<Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>>>>>>(graph, row, PXTaxCheck.Line) : new List<object>();
  }

  protected virtual void TaxSetLineDefault(PXCache sender, object taxrow, object row)
  {
    TaxDetail taxDetail1 = taxrow != null ? (TaxDetail) ((PXResult) taxrow)[0] : throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    Tax tax = PXResult.Unwrap<Tax>(taxrow);
    TaxRev taxRev = PXResult.Unwrap<TaxRev>(taxrow);
    if (taxRev.TaxID == null)
    {
      taxRev.TaxableMin = new Decimal?(0M);
      taxRev.TaxableMax = new Decimal?(0M);
      taxRev.TaxRate = new Decimal?(0M);
    }
    if (this.IsPerUnitTax(tax))
    {
      this.TaxSetLineDefaultForPerUnitTaxes(sender, row, tax, taxRev, taxDetail1);
    }
    else
    {
      PXCache cach = sender.Graph.Caches[this._TaxType];
      Decimal? nullable1 = this.GetCuryTranAmt(sender, row, tax.TaxCalcType);
      Decimal curyTranAmt = nullable1.Value;
      PX.Objects.CS.Terms terms = this.SelectTerms(sender.Graph);
      List<object> objectList = this.SelectInclusiveTaxes(sender.Graph, row);
      Decimal num1 = this.SumWithReverseAdjustment(sender.Graph, objectList, this.GetFieldType(cach, this._CuryTaxAmt));
      Decimal num2 = 0M;
      Type fieldType = this.GetFieldType(cach, this._CuryTaxDiscountAmt);
      if (fieldType != (Type) null)
        num2 = this.SumWithReverseAdjustment(sender.Graph, objectList, fieldType);
      Decimal curyTaxableAmt = 0.0M;
      Decimal curyTaxableDiscountAmt = 0.0M;
      Decimal taxableAmt = 0.0M;
      Decimal curyTaxAmt = 0.0M;
      Decimal curyTaxDiscountAmt = 0.0M;
      Decimal? nullable2;
      this.DiscPercentsDict.TryGetValue(this.ParentRow(sender.Graph), out nullable2);
      nullable1 = taxRev.TaxRate;
      Decimal calculatedTaxRate = nullable1.Value / 100M;
      nullable1 = nullable2;
      Decimal undiscountedPercent = 1M - (nullable1 ?? terms.DiscPercent.GetValueOrDefault()) / 100M;
      switch (tax.TaxCalcLevel)
      {
        case "0":
          (curyTaxableAmt, curyTaxAmt) = this.CalculateInclusiveTaxAmounts(sender, row, cach, taxDetail1, objectList, in calculatedTaxRate, in curyTranAmt);
          break;
        case "1":
          Decimal adjustmentCalculation1 = this.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(tax, taxDetail1, cach, row, sender);
          curyTaxableAmt = curyTranAmt - num1 - num2 + adjustmentCalculation1;
          break;
        case "2":
          Decimal adjustmentCalculation2 = this.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(tax, taxDetail1, cach, row, sender);
          List<object> list = this.SelectLvl1Taxes(sender.Graph, row);
          Decimal num3 = this.SumWithReverseAdjustment(sender.Graph, list, this.GetFieldType(cach, this._CuryTaxAmt));
          curyTaxableAmt = curyTranAmt - num1 + num3 - num2 + adjustmentCalculation2;
          break;
      }
      this.ApplyDiscounts(tax, sender, row, undiscountedPercent, calculatedTaxRate, ref curyTaxableAmt, ref curyTaxableDiscountAmt, ref curyTaxDiscountAmt, ref curyTaxAmt);
      if (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2")
      {
        if (cach.Fields.Contains(this._CuryOrigTaxableAmt))
          cach.SetValue((object) taxDetail1, this._CuryOrigTaxableAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxDetail1, curyTaxableAmt, this.Precision));
        this.AdjustMinMaxTaxableAmt(cach, taxDetail1, taxRev, ref curyTaxableAmt, ref taxableAmt);
        curyTaxAmt = curyTaxableAmt * calculatedTaxRate;
        curyTaxDiscountAmt = curyTaxableDiscountAmt * calculatedTaxRate;
        if (tax.TaxApplyTermsDisc == "T")
          curyTaxAmt *= undiscountedPercent;
      }
      taxDetail1.TaxRate = taxRev.TaxRate;
      taxDetail1.NonDeductibleTaxRate = taxRev.NonDeductibleTaxRate;
      TaxBaseAttribute.SetValueOptional(cach, (object) taxDetail1, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxDetail1, curyTaxableDiscountAmt), this._CuryTaxableDiscountAmt);
      TaxBaseAttribute.SetValueOptional(cach, (object) taxDetail1, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxDetail1, curyTaxDiscountAmt), this._CuryTaxDiscountAmt);
      Decimal num4 = MultiCurrencyCalculator.RoundCury(cach, (object) taxDetail1, curyTaxableAmt, this.Precision);
      int num5 = this.IsExemptTaxCategory(sender, row) ? 1 : 0;
      if (num5 != 0)
        this.SetTaxDetailExemptedAmount(cach, taxDetail1, new Decimal?(num4));
      else
        this.SetTaxDetailTaxableAmount(cach, taxDetail1, new Decimal?(num4));
      Decimal num6 = MultiCurrencyCalculator.RoundCury(cach, (object) taxDetail1, curyTaxAmt, this.Precision);
      Decimal num7;
      if (this.IsDeductibleVATTax(tax))
      {
        TaxDetail taxDetail2 = taxDetail1;
        PXCache sender1 = cach;
        TaxDetail row1 = taxDetail1;
        Decimal num8 = curyTaxAmt;
        nullable1 = taxRev.NonDeductibleTaxRate;
        Decimal num9 = 1M - nullable1.GetValueOrDefault() / 100M;
        Decimal val = num8 * num9;
        int? precision = this.Precision;
        Decimal? nullable3 = new Decimal?(MultiCurrencyCalculator.RoundCury(sender1, (object) row1, val, precision));
        taxDetail2.CuryExpenseAmt = nullable3;
        Decimal num10 = num6;
        nullable1 = taxDetail1.CuryExpenseAmt;
        Decimal num11 = nullable1.Value;
        num7 = num10 - num11;
        PXCache sender2 = cach;
        TaxDetail row2 = taxDetail1;
        nullable1 = taxDetail1.CuryExpenseAmt;
        Decimal curyval = nullable1.Value;
        Decimal num12;
        ref Decimal local = ref num12;
        MultiCurrencyCalculator.CuryConvBase(sender2, (object) row2, curyval, out local);
        taxDetail1.ExpenseAmt = new Decimal?(num12);
      }
      else
        num7 = num6;
      if (num5 == 0)
        this.SetTaxDetailTaxAmount(cach, taxDetail1, new Decimal?(num7));
      if (taxRev.TaxID != null && !tax.DirectTax.GetValueOrDefault())
      {
        this.Update(cach, (object) taxDetail1);
        if (!(tax.TaxCalcLevel == "0"))
          return;
        GraphHelper.MarkUpdated(sender, row);
      }
      else if (this._IncludeDirectTaxLine && taxRev.TaxID != null && tax.DirectTax.GetValueOrDefault() && !this.SkipDirectTax(sender, row, taxRev.TaxID))
      {
        this.SetTaxDetailTaxableAmount(cach, taxDetail1, new Decimal?(0.0M));
        this.SetTaxDetailTaxAmount(cach, taxDetail1, new Decimal?(curyTranAmt));
        this.Update(cach, (object) taxDetail1);
      }
      else
        this.Delete(cach, (object) taxDetail1);
    }
  }

  protected virtual bool SkipDirectTax(PXCache sender, object row, string applicableDirectTaxId)
  {
    return false;
  }

  private (Decimal InclTaxTaxable, Decimal InclTaxAmount) CalculateInclusiveTaxAmounts(
    PXCache sender,
    object row,
    PXCache cache,
    TaxDetail nonPerUnitTaxDetail,
    List<object> inclusiveTaxes,
    in Decimal calculatedTaxRate,
    in Decimal curyTranAmt)
  {
    (List<object> objectList1, List<object> objectList2, List<object> objectList3) = this.SegregateInclusiveTaxes(inclusiveTaxes);
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = this.SumWithReverseAdjustment(sender.Graph, objectList3, typeof (TaxRev.taxRate)) / 100M;
    Type bqlField = cache.GetBqlField(this._CuryTaxAmt);
    if (bqlField != (Type) null)
    {
      num1 = this.SumWithReverseAdjustment(sender.Graph, objectList1, bqlField);
      num2 = this.SumWithReverseAdjustment(sender.Graph, objectList2, bqlField);
    }
    Decimal num4 = (curyTranAmt - num2) / (1M + num3) - num1 + num1;
    Decimal val = num4 * calculatedTaxRate;
    Decimal num5 = MultiCurrencyCalculator.RoundCury(cache, (object) nonPerUnitTaxDetail, val, this.Precision);
    Decimal num6 = num1 + num2;
    PXCache cach = sender.Graph.Caches[typeof (TaxRev)];
    foreach (PXResult pxResult in objectList3)
    {
      object obj = pxResult[typeof (TaxRev)];
      Tax tax = (Tax) pxResult[typeof (Tax)];
      Decimal? nullable1 = cach.GetValue<TaxRev.taxRate>(obj) as Decimal?;
      Decimal num7 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
      Decimal num8 = num4;
      Decimal? nullable2 = nullable1;
      Decimal valueOrDefault = (nullable2.HasValue ? new Decimal?(num8 * nullable2.GetValueOrDefault() / 100M) : new Decimal?()).GetValueOrDefault();
      Decimal num9 = MultiCurrencyCalculator.RoundCury(cache, (object) nonPerUnitTaxDetail, valueOrDefault, this.Precision) * num7;
      num6 += num9;
    }
    Decimal num10 = curyTranAmt - num6;
    Decimal num11 = num10 + num1;
    this.SetTaxableAmt(sender, row, new Decimal?(num10));
    this.SetTaxAmt(sender, row, new Decimal?(num6));
    return (num11, num5);
  }

  private (List<object> PerUnitTaxesIncludedInTaxOnTaxCalc, List<object> PerUnitTaxesExcludedFromTaxOnTaxCalc, List<object> NonPerUnitTaxes) SegregateInclusiveTaxes(
    List<object> inclusiveTaxes)
  {
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    List<object> objectList3 = new List<object>(inclusiveTaxes.Count);
    foreach (PXResult inclusiveTax in inclusiveTaxes)
    {
      Tax tax = inclusiveTax.GetItem<Tax>();
      if (tax != null)
      {
        if (!this.IsPerUnitTax(tax))
          objectList3.Add((object) inclusiveTax);
        else if (tax.TaxCalcLevel2Exclude.GetValueOrDefault())
          objectList2.Add((object) inclusiveTax);
        else
          objectList1.Add((object) inclusiveTax);
      }
    }
    return (objectList1, objectList2, objectList3);
  }

  private void ApplyDiscounts(
    Tax tax,
    PXCache sender,
    object row,
    Decimal undiscountedPercent,
    Decimal calculatedTaxRate,
    ref Decimal curyTaxableAmt,
    ref Decimal curyTaxableDiscountAmt,
    ref Decimal curyTaxDiscountAmt,
    ref Decimal curyTaxAmt)
  {
    if (this.ConsiderDiscount(tax))
      curyTaxableAmt *= undiscountedPercent;
    else if (this.ConsiderEarlyPaymentDiscountDetail(sender, row, tax))
    {
      curyTaxableDiscountAmt = curyTaxableAmt * (1M - undiscountedPercent);
      curyTaxableAmt *= undiscountedPercent;
      curyTaxDiscountAmt = curyTaxableDiscountAmt * calculatedTaxRate;
    }
    else
    {
      if (!this.ConsiderInclusiveDiscountDetail(sender, row, tax))
        return;
      curyTaxableDiscountAmt = curyTaxableAmt * (1M - undiscountedPercent);
      curyTaxDiscountAmt = curyTaxableDiscountAmt * calculatedTaxRate;
      curyTaxableAmt *= undiscountedPercent;
      curyTaxAmt *= undiscountedPercent;
    }
  }

  protected virtual bool ConsiderDiscount(Tax tax)
  {
    return (tax.TaxCalcLevel == "1" || tax.TaxCalcLevel == "2") && tax.TaxApplyTermsDisc == "X";
  }

  private bool ConsiderEarlyPaymentDiscountDetail(PXCache sender, object detail, Tax tax)
  {
    object parent = PXParentAttribute.SelectParent(sender, detail, this._ParentType);
    return this.ConsiderEarlyPaymentDiscount(sender, parent, tax);
  }

  private bool ConsiderInclusiveDiscountDetail(PXCache sender, object detail, Tax tax)
  {
    object parent = PXParentAttribute.SelectParent(sender, detail, this._ParentType);
    return this.ConsiderInclusiveDiscount(sender, parent, tax);
  }

  protected virtual bool ConsiderEarlyPaymentDiscount(PXCache sender, object parent, Tax tax)
  {
    return false;
  }

  protected virtual bool ConsiderInclusiveDiscount(PXCache sender, object parent, Tax tax) => false;

  protected virtual void SetTaxDetailTaxableAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyTaxableAmt)
  {
    cache.SetValue((object) taxdet, this._CuryTaxableAmt, (object) curyTaxableAmt);
  }

  protected virtual void SetTaxDetailExemptedAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyExemptedAmt)
  {
    if (string.IsNullOrEmpty(this._CuryExemptedAmt))
      return;
    cache.SetValue((object) taxdet, this._CuryExemptedAmt, (object) curyExemptedAmt);
  }

  protected virtual void SetTaxDetailTaxAmount(
    PXCache cache,
    TaxDetail taxdet,
    Decimal? curyTaxAmt)
  {
    cache.SetValue((object) taxdet, this._CuryTaxAmt, (object) curyTaxAmt);
  }

  protected virtual void SetTaxDetailCuryExpenseAmt(
    PXCache cache,
    TaxDetail taxdet,
    Decimal CuryExpenseAmt)
  {
    taxdet.CuryExpenseAmt = new Decimal?(MultiCurrencyCalculator.RoundCury(cache, (object) taxdet, CuryExpenseAmt, this.Precision));
  }

  [Obsolete("This method is obsolete and will be removed in future versions of Acumatica. Use PX.Objects.Common.SquareEquationSolver instead")]
  public static Pair<double, double> SolveQuadraticEquation(double a, double b, double c)
  {
    (double X1, double X2)? nullable = SquareEquationSolver.SolveQuadraticEquation(a, b, c);
    return !nullable.HasValue ? (Pair<double, double>) null : new Pair<double, double>(nullable.Value.X1, nullable.Value.X2);
  }

  protected virtual void CuryOrigDiscAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.OrigDiscAmtExtCallDict[e.Row] = e.ExternalCall;
  }

  protected virtual bool ShouldUpdateFinPeriodID(PXCache sender, object oldRow, object newRow)
  {
    return (this._TaxCalc == TaxCalc.Calc || this._TaxCalc == TaxCalc.ManualLineCalc) && (string) sender.GetValue(oldRow, this._FinPeriodID) != (string) sender.GetValue(newRow, this._FinPeriodID);
  }

  protected virtual void ParentRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? nullable1 = new int?();
    int? branchID = new int?();
    string str = (string) null;
    if (this.ParentBranchIDField != (Type) null)
    {
      nullable1 = (int?) sender.GetValue(e.OldRow, this.ParentBranchIDField.Name);
      branchID = (int?) sender.GetValue(e.Row, this.ParentBranchIDField.Name);
      str = OrganizationMaint.GetCashDiscountBase(sender.Graph, branchID);
    }
    int? nullable2 = nullable1;
    int? nullable3 = branchID;
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) || this.ShouldUpdateFinPeriodID(sender, e.OldRow, e.Row))
    {
      PXCache cach = sender.Graph.Caches[this._TaxSumType];
      foreach (object obj in TaxParentAttribute.ChildSelect(cach, e.Row, this._ParentType))
      {
        int? nullable4 = nullable1;
        nullable2 = branchID;
        if (!(nullable4.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable4.HasValue == nullable2.HasValue))
          cach.SetDefaultExt(obj, this.ChildBranchIDField.Name, (object) null);
        if (this.ShouldUpdateFinPeriodID(sender, e.OldRow, e.Row))
          cach.SetDefaultExt(obj, this.ChildFinPeriodIDField.Name, (object) null);
        GraphHelper.MarkUpdated(cach, obj);
      }
    }
    bool flag1 = false;
    this.OrigDiscAmtExtCallDict.TryGetValue(e.Row, out flag1);
    if (!flag1)
      return;
    Decimal valueOrDefault1 = ((Decimal?) sender.GetValue(e.Row, this._CuryOrigDiscAmt)).GetValueOrDefault();
    Decimal valueOrDefault2 = ((Decimal?) sender.GetValue(e.OldRow, this._CuryOrigDiscAmt)).GetValueOrDefault();
    if (!(valueOrDefault1 != valueOrDefault2) || this.DiscPercentsDict.ContainsKey(e.Row))
      return;
    this.DiscPercentsDict.Add(e.Row, new Decimal?(0M));
    PXFieldUpdatedEventArgs e1 = new PXFieldUpdatedEventArgs(e.Row, (object) valueOrDefault2, false);
    using (new TermsAttribute.UnsubscribeCalcDiscScope(sender))
    {
      try
      {
        if (valueOrDefault1 == 0M)
          return;
        this.ParentFieldUpdated(sender, e1);
        this.DiscPercentsDict[e.Row] = new Decimal?();
        bool flag2 = false;
        Decimal reducableTaxAmountOld = 0M;
        PXCache cach = sender.Graph.Caches[this._TaxSumType];
        foreach (object selectTax in this.SelectTaxes(sender, e.Row, PXTaxCheck.RecalcTotals))
        {
          object obj1;
          object obj2 = ((PXResult) (obj1 = selectTax))[0];
          Tax tax = PXResult.Unwrap<Tax>(obj1);
          if (this.RecalcTaxableRequired(tax))
          {
            Decimal num1 = reducableTaxAmountOld;
            Decimal num2 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
            Decimal? nullable5 = (Decimal?) cach.GetValue(obj2, this._CuryTaxAmt);
            Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
            Decimal num3;
            if (!this.IsDeductibleVATTax(tax))
            {
              num3 = 0M;
            }
            else
            {
              nullable5 = (Decimal?) cach.GetValue(obj2, this._CuryExpenseAmt);
              num3 = nullable5.GetValueOrDefault();
            }
            Decimal num4 = valueOrDefault3 + num3;
            Decimal num5 = num2 * num4;
            reducableTaxAmountOld = num1 + num5;
          }
          else if (this.ConsiderEarlyPaymentDiscount(sender, e.Row, tax) || this.ConsiderInclusiveDiscount(sender, e.Row, tax))
          {
            flag2 = true;
            break;
          }
        }
        if (flag2)
        {
          Decimal valueOrDefault4 = ((Decimal?) sender.GetValue(e.Row, this._CuryDocBal)).GetValueOrDefault();
          this.DiscPercentsDict[e.Row] = new Decimal?(100M * valueOrDefault1 / valueOrDefault4);
        }
        else if (str == "TA" && reducableTaxAmountOld != 0M)
        {
          Decimal valueOrDefault5 = ((Decimal?) sender.GetValue(e.Row, this._CuryDocBal)).GetValueOrDefault();
          this.DiscPercentsDict[e.Row] = new Decimal?(100M * valueOrDefault1 / (valueOrDefault5 - reducableTaxAmountOld));
        }
        else
        {
          if (!(reducableTaxAmountOld != 0M))
            return;
          Decimal valueOrDefault6 = ((Decimal?) sender.GetValue(e.Row, this._CuryDocBal)).GetValueOrDefault();
          this.DiscPercentsDict[e.Row] = this.CalculateCashDiscountPercent(valueOrDefault6, reducableTaxAmountOld, valueOrDefault1);
        }
      }
      catch
      {
        this.DiscPercentsDict[e.Row] = new Decimal?();
      }
      finally
      {
        this.ParentFieldUpdated(sender, e1);
        sender.RaiseRowUpdated(e.Row, e.OldRow);
        this.OrigDiscAmtExtCallDict.Remove(e.Row);
        this.DiscPercentsDict.Remove(e.Row);
      }
    }
  }

  private Decimal? CalculateCashDiscountPercent(
    Decimal curyDocBalanceOld,
    Decimal reducableTaxAmountOld,
    Decimal newCashDiscountAmount)
  {
    (Decimal X1, Decimal X2)? nullable = SquareEquationSolver.SolveQuadraticEquation(reducableTaxAmountOld, -curyDocBalanceOld, newCashDiscountAmount);
    if (!nullable.HasValue)
      return new Decimal?();
    (Decimal X1, Decimal X2) = nullable.Value;
    if (X1 >= 0M && X1 <= 1M)
      return new Decimal?(X1 * 100M);
    return !(X2 >= 0M) || !(X2 <= 1M) ? new Decimal?() : new Decimal?(X2 * 100M);
  }

  protected virtual bool RecalcTaxableRequired(Tax tax)
  {
    return tax?.TaxCalcLevel != "0" && tax?.TaxApplyTermsDisc == "X";
  }

  protected virtual void AdjustTaxableAmount(
    PXCache cache,
    object row,
    List<object> taxitems,
    ref Decimal CuryTaxableAmt,
    string TaxCalcType)
  {
  }

  protected virtual Decimal GetPrecisionBasedNegligibleDifference(PXGraph graph, object row)
  {
    return (Decimal) Math.Pow(10.0, (double) -((short?) MultiCurrencyCalculator.GetCurrentCurrency(graph)?.DecimalPlaces ?? (short) 5)) + 0.00005M;
  }

  protected virtual void AdjustExemptedAmount(
    PXCache cache,
    object row,
    List<object> taxitems,
    ref Decimal CuryExemptedAmt,
    string TaxCalcType)
  {
  }

  protected virtual TaxDetail CalculateTaxSum(PXCache sender, object taxrow, object row)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cache = sender.Graph.Caches[this._TaxType];
    PXCache cach = sender.Graph.Caches[this._TaxSumType];
    TaxDetail taxdet = (TaxDetail) ((PXResult) taxrow)[0];
    Tax tax = PXResult.Unwrap<Tax>(taxrow);
    TaxRev taxRev = PXResult.Unwrap<TaxRev>(taxrow);
    if (taxRev.TaxID == null)
    {
      taxRev.TaxableMin = new Decimal?(0M);
      taxRev.TaxableMax = new Decimal?(0M);
      taxRev.TaxRate = new Decimal?(0M);
    }
    Decimal num1 = 0M;
    Decimal val1 = 0M;
    Decimal val2 = 0.0M;
    Decimal val3 = 0.0M;
    Decimal taxableAmt = 0.0M;
    Decimal val4 = 0.0M;
    Decimal curyExpenseAmt = 0.0M;
    List<object> calculateTaxSum = this.SelectTaxesToCalculateTaxSum(sender, row, taxdet);
    if (calculateTaxSum.Count == 0 || taxRev.TaxID == null)
      return (TaxDetail) null;
    bool? nullable1 = tax.DirectTax;
    if (nullable1.GetValueOrDefault() && this._IncludeDirectTaxLine)
    {
      taxdet.TaxRate = taxRev.TaxRate;
      taxdet.NonDeductibleTaxRate = taxRev.NonDeductibleTaxRate;
      Decimal val5 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxAmt));
      cach.SetValue((object) taxdet, this._CuryTaxableAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, 0.0M, this.Precision));
      cach.SetValue((object) taxdet, this._CuryTaxAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val5, this.Precision));
      cach.SetValue((object) taxdet, this._CuryTaxAmtSumm, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val5, this.Precision));
      return taxdet;
    }
    Decimal curyTaxAmt;
    if (tax.TaxCalcType == "I")
    {
      if (cache.Fields.Contains(this._CuryOrigTaxableAmt))
        num1 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryOrigTaxableAmt));
      val2 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxableAmt));
      Type fieldType1 = this.GetFieldType(cache, this._CuryTaxableDiscountAmt);
      if (fieldType1 != (Type) null)
        val3 = this.Sum(sender.Graph, calculateTaxSum, fieldType1);
      this.AdjustTaxableAmount(sender, row, calculateTaxSum, ref val2, tax.TaxCalcType);
      if (!(tax.TaxType == "Q"))
      {
        nullable1 = tax.ZeroTaxable;
        if (!nullable1.GetValueOrDefault())
        {
          curyTaxAmt = val1 = val2 == 0M ? 0M : this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxAmt));
          goto label_17;
        }
      }
      curyTaxAmt = val1 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxAmt));
label_17:
      Type fieldType2 = this.GetFieldType(cache, this._CuryTaxDiscountAmt);
      if (fieldType2 != (Type) null)
        val4 = this.Sum(sender.Graph, calculateTaxSum, fieldType2);
      curyExpenseAmt = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryExpenseAmt));
    }
    else if (tax.TaxType != "W" && (this.CalcGrossOnDocumentLevel && this._isTaxCalcModeEnabled && this.GetTaxCalcMode(sender.Graph) == "G" || tax.TaxCalcLevel == "0" && (!this._isTaxCalcModeEnabled || this.GetTaxCalcMode(sender.Graph) != "N")))
    {
      val2 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxableAmt));
      curyTaxAmt = val1 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxAmt));
      Type fieldType3 = this.GetFieldType(cache, this._CuryTaxableDiscountAmt);
      if (fieldType3 != (Type) null)
        val3 = this.Sum(sender.Graph, calculateTaxSum, fieldType3);
      Type fieldType4 = this.GetFieldType(cache, this._CuryTaxDiscountAmt);
      if (fieldType4 != (Type) null)
        val4 = this.Sum(sender.Graph, calculateTaxSum, fieldType4);
      List<object> source1 = this.SelectDocumentLines(sender.Graph, row);
      if (source1.Any<object>())
      {
        PXCache docLineCache = sender.Graph.Caches[source1[0].GetType()];
        Dictionary<int, Decimal> dictionary = source1.ToDictionary<object, int, Decimal>((Func<object, int>) (_ => (int) docLineCache.GetValue(_, this._LineNbr)), (Func<object, Decimal>) (_ => this.GetDocLineFinalAmtNoRounding(docLineCache, _, tax.TaxCalcType) ?? 0.0M));
        IEnumerable<\u003C\u003Ef__AnonymousType114<int, string, Decimal, Decimal, Decimal?, Decimal?>> source2 = this.SelectTaxes(sender, row, PXTaxCheck.RecalcLine).Where<object>((Func<object, bool>) (_ => PXResult.Unwrap<Tax>(_).TaxCalcLevel == "0")).ToList<object>().Select(_ => new
        {
          LineNbr = (int) cache.GetValue((object) (TaxDetail) ((PXResult) _)[0], this._LineNbr),
          TaxID = PXResult.Unwrap<Tax>(_).TaxID,
          TaxRate = PXResult.Unwrap<TaxRev>(_).TaxRate.GetValueOrDefault(),
          TaxRateMultiplier = PXResult.Unwrap<Tax>(_).TaxType == "W" ? 0M : (PXResult.Unwrap<Tax>(_).ReverseTax.GetValueOrDefault() ? -1.0M : 1.0M),
          CuryTaxableAmt = (Decimal?) cache.GetValue((object) (TaxDetail) ((PXResult) _)[0], this._CuryTaxableAmt),
          CuryTaxAmt = (Decimal?) cache.GetValue((object) (TaxDetail) ((PXResult) _)[0], this._CuryTaxAmt)
        });
        Decimal? nullable2 = taxRev.TaxRate;
        Decimal num2 = 0.0M;
        Decimal? nullable3;
        if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
        {
          var data = source2.FirstOrDefault(_ => _.TaxID == taxdet.TaxID);
          if (data == null)
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new Decimal?(data.TaxRate);
        }
        else
          nullable3 = taxRev.TaxRate;
        Decimal? nullable4 = nullable3;
        Decimal? currentTaxRate = nullable4 ?? new Decimal?(0.0M);
        List<int> list1 = source2.Where(_ => _.TaxID == taxdet.TaxID).Select(_ => _.LineNbr).ToList<int>();
        List<TaxBaseAttribute.InclusiveTaxGroup> source3 = new List<TaxBaseAttribute.InclusiveTaxGroup>();
        Decimal num3 = 0M;
        Decimal num4 = 0M;
        foreach (int num5 in list1)
        {
          int lineNbr = num5;
          List<\u003C\u003Ef__AnonymousType114<int, string, Decimal, Decimal, Decimal?, Decimal?>> list2 = source2.Where(_ => _.LineNbr == lineNbr).OrderBy(_ => _.TaxID).ToList();
          string groupKey = string.Join("::", list2.Select(_ => _.TaxID));
          Decimal num6 = list2.Sum(_ => _.TaxRate * _.TaxRateMultiplier);
          Decimal num7 = 0M;
          if (!dictionary.TryGetValue(lineNbr, out num7))
          {
            Decimal num8 = num3;
            nullable4 = list2.Sum(_ => _.CuryTaxAmt);
            Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
            num3 = num8 + valueOrDefault1;
            Decimal num9 = num4;
            nullable4 = list2.Sum(_ => _.CuryTaxableAmt);
            Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
            num4 = num9 + valueOrDefault2;
          }
          if (source3.Any<TaxBaseAttribute.InclusiveTaxGroup>((Func<TaxBaseAttribute.InclusiveTaxGroup, bool>) (g => g.Key == groupKey)))
            source3.Single<TaxBaseAttribute.InclusiveTaxGroup>((Func<TaxBaseAttribute.InclusiveTaxGroup, bool>) (g => g.Key == groupKey)).TotalAmount += num7;
          else
            source3.Add(new TaxBaseAttribute.InclusiveTaxGroup()
            {
              Key = groupKey,
              Rate = num6,
              TotalAmount = num7
            });
        }
        curyTaxAmt = source3.Sum<TaxBaseAttribute.InclusiveTaxGroup>((Func<TaxBaseAttribute.InclusiveTaxGroup, Decimal>) (g =>
        {
          PXCache sender1 = sender;
          TaxDetail row1 = taxdet;
          Decimal num10 = g.TotalAmount / (1M + g.Rate / 100.0M);
          Decimal? nullable5 = currentTaxRate;
          Decimal val6 = (nullable5.HasValue ? new Decimal?(num10 * nullable5.GetValueOrDefault() / 100.0M) : new Decimal?()) ?? 0.0M;
          int? precision = this.Precision;
          return MultiCurrencyCalculator.RoundCury(sender1, (object) row1, val6, precision);
        })) - val4 + num3;
        val2 = MultiCurrencyCalculator.RoundCury(sender, (object) taxdet, source3.Sum<TaxBaseAttribute.InclusiveTaxGroup>((Func<TaxBaseAttribute.InclusiveTaxGroup, Decimal>) (g => g.TotalAmount / (1M + g.Rate / 100.0M))), this.Precision) - val3 + num4;
      }
      nullable1 = tax.DeductibleVAT;
      if (nullable1.GetValueOrDefault())
        curyExpenseAmt = curyTaxAmt * (1.0M - (taxRev.NonDeductibleTaxRate ?? 0.0M) / 100.0M);
    }
    else
    {
      List<object> objectList = this.SelectLvl1Taxes(sender.Graph, row);
      if (this._NoSumTaxable && (tax.TaxCalcLevel == "1" || objectList.Count == 0))
      {
        val2 = (Decimal) cach.GetValue((object) taxdet, this._CuryTaxableAmt);
        val3 = this.GetOptionalDecimalValue(cach, (object) taxdet, this._CuryTaxableDiscountAmt);
      }
      else
      {
        val2 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxableAmt));
        Decimal val7 = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryTaxAmt));
        val1 = MultiCurrencyCalculator.RoundCury(sender, (object) taxdet, val7, this.Precision);
        Type fieldType = this.GetFieldType(cache, this._CuryTaxableDiscountAmt);
        if (fieldType != (Type) null)
          val3 = this.Sum(sender.Graph, calculateTaxSum, fieldType);
        this.AdjustTaxableAmount(sender, row, calculateTaxSum, ref val2, tax.TaxCalcType);
      }
      num1 = MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val2, this.Precision);
      this.AdjustMinMaxTaxableAmt(cach, taxdet, taxRev, ref val2, ref taxableAmt);
      Decimal val8 = val2 * taxRev.TaxRate.Value / 100M;
      curyTaxAmt = MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val8, this.Precision);
      val4 = val3 * taxRev.TaxRate.Value / 100M;
      this.AdjustExpenseAmt(tax, taxRev, curyTaxAmt, ref curyExpenseAmt);
      this.AdjustTaxAmtOnDiscount(sender, tax, ref curyTaxAmt);
    }
    taxdet = (TaxDetail) cach.CreateCopy((object) taxdet);
    if (cach.Fields.Contains(this._CuryOrigTaxableAmt))
      cach.SetValue((object) taxdet, this._CuryOrigTaxableAmt, (object) num1);
    Decimal CuryExemptedAmt = this.Sum(sender.Graph, calculateTaxSum, this.GetFieldType(cache, this._CuryExemptedAmt));
    this.AdjustExemptedAmount(sender, row, calculateTaxSum, ref CuryExemptedAmt, tax.TaxCalcType);
    taxdet.TaxRate = taxRev.TaxRate;
    taxdet.NonDeductibleTaxRate = taxRev.NonDeductibleTaxRate;
    nullable1 = tax.IsExternal;
    bool flag = false;
    if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      TaxBaseAttribute.SetValueOptional(cach, (object) taxdet, (object) (tax.TaxCalcLevel == "0"), this._IsTaxInclusive);
    cach.SetValue((object) taxdet, this._CuryTaxableAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val2, this.Precision));
    cach.SetValue((object) taxdet, this._CuryExemptedAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, CuryExemptedAmt, this.Precision));
    cach.SetValue((object) taxdet, this._CuryTaxAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, curyTaxAmt, this.Precision));
    cach.SetValue((object) taxdet, this._CuryTaxAmtSumm, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val1, this.Precision));
    TaxBaseAttribute.SetValueOptional(cach, (object) taxdet, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val3), this._CuryTaxableDiscountAmt);
    this.SetTaxDetailCuryExpenseAmt(cach, taxdet, curyExpenseAmt);
    TaxBaseAttribute.SetValueOptional(cach, (object) taxdet, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val4), this._CuryTaxDiscountAmt);
    if (this.IsDeductibleVATTax(tax) && tax.TaxCalcType != "I")
      cach.SetValue((object) taxdet, this._CuryTaxAmt, (object) ((Decimal) (cach.GetValue((object) taxdet, this._CuryTaxAmt) ?? (object) 0M) - (Decimal) (cach.GetValue((object) taxdet, this._CuryExpenseAmt) ?? (object) 0M)));
    if (this.IsPerUnitTax(tax))
      taxdet = this.FillAggregatedTaxDetailForPerUnitTax(sender, row, tax, taxRev, taxdet, calculateTaxSum);
    return taxdet;
  }

  protected virtual List<object> SelectTaxesToCalculateTaxSum(
    PXCache sender,
    object row,
    TaxDetail taxdet)
  {
    return this.SelectTaxes<Where<Tax.taxID, Equal<Required<Tax.taxID>>>>(sender.Graph, row, PXTaxCheck.RecalcLine, (object) taxdet.TaxID);
  }

  protected virtual void CalculateTaxSumTaxAmt(
    PXCache sender,
    TaxDetail taxdet,
    Tax tax,
    TaxRev taxrev)
  {
    if (tax.TaxType == "Q")
    {
      PXTrace.WriteError("This operation is not supported for per-unit taxes.");
      throw new PXException("This operation is not supported for per-unit taxes.");
    }
    PXCache cach = sender.Graph.Caches[this._TaxSumType];
    Decimal taxableAmt = 0.0M;
    Decimal curyExpenseAmt = 0.0M;
    Decimal optionalDecimalValue1 = this.GetOptionalDecimalValue(sender, (object) taxdet, this._CuryTaxableAmt);
    Decimal optionalDecimalValue2 = this.GetOptionalDecimalValue(sender, (object) taxdet, this._CuryTaxableDiscountAmt);
    Decimal num1 = MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, optionalDecimalValue1, this.Precision);
    Decimal num2 = taxrev.TaxRate ?? 0.0M;
    this.AdjustMinMaxTaxableAmt(sender, taxdet, taxrev, ref optionalDecimalValue1, ref taxableAmt);
    Decimal curyTaxAmt = optionalDecimalValue1 * num2 / 100M;
    Decimal val = optionalDecimalValue2 * num2 / 100M;
    this.AdjustExpenseAmt(tax, taxrev, curyTaxAmt, ref curyExpenseAmt);
    this.AdjustTaxAmtOnDiscount(sender, tax, ref curyTaxAmt);
    if (cach.Fields.Contains(this._CuryOrigTaxableAmt))
      cach.SetValue((object) taxdet, this._CuryOrigTaxableAmt, (object) num1);
    taxdet.TaxRate = new Decimal?(num2);
    taxdet.NonDeductibleTaxRate = new Decimal?(taxrev.NonDeductibleTaxRate ?? 0.0M);
    cach.SetValue((object) taxdet, this._CuryTaxableAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, optionalDecimalValue1, this.Precision));
    cach.SetValue((object) taxdet, this._CuryTaxAmt, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, curyTaxAmt, this.Precision));
    TaxBaseAttribute.SetValueOptional(cach, (object) taxdet, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, optionalDecimalValue2), this._CuryTaxableDiscountAmt);
    this.SetTaxDetailCuryExpenseAmt(cach, taxdet, curyExpenseAmt);
    TaxBaseAttribute.SetValueOptional(cach, (object) taxdet, (object) MultiCurrencyCalculator.RoundCury(cach, (object) taxdet, val), this._CuryTaxDiscountAmt);
    if (!this.IsDeductibleVATTax(tax) || !(tax.TaxCalcType != "I"))
      return;
    cach.SetValue((object) taxdet, this._CuryTaxAmt, (object) ((Decimal) (cach.GetValue((object) taxdet, this._CuryTaxAmt) ?? (object) 0M) - (Decimal) (cach.GetValue((object) taxdet, this._CuryExpenseAmt) ?? (object) 0M)));
  }

  private void AdjustExpenseAmt(
    Tax tax,
    TaxRev taxrev,
    Decimal curyTaxAmt,
    ref Decimal curyExpenseAmt)
  {
    if (!this.IsDeductibleVATTax(tax))
      return;
    curyExpenseAmt = curyTaxAmt * (1M - taxrev.NonDeductibleTaxRate.GetValueOrDefault() / 100M);
  }

  private void AdjustTaxAmtOnDiscount(PXCache sender, Tax tax, ref Decimal curyTaxAmt)
  {
    if (!(tax.TaxCalcLevel == "1") && !(tax.TaxCalcLevel == "2") || !(tax.TaxApplyTermsDisc == "T"))
      return;
    Decimal? nullable = new Decimal?();
    this.DiscPercentsDict.TryGetValue(this.ParentRow(sender.Graph), out nullable);
    PX.Objects.CS.Terms terms = this.SelectTerms(sender.Graph);
    curyTaxAmt *= 1M - (nullable ?? terms.DiscPercent.GetValueOrDefault()) / 100M;
  }

  private void AdjustMinMaxTaxableAmt(
    PXCache sumcache,
    TaxDetail taxdet,
    TaxRev taxrev,
    ref Decimal curyTaxableAmt,
    ref Decimal taxableAmt)
  {
    try
    {
      MultiCurrencyCalculator.CuryConvBase(sumcache, (object) taxdet, curyTaxableAmt, out taxableAmt);
    }
    catch (Exception ex)
    {
    }
    Decimal? nullable = taxrev.TaxableMin;
    Decimal num1 = 0.0M;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
    {
      Decimal num2 = taxableAmt;
      nullable = taxrev.TaxableMin;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      if (num2 < valueOrDefault & nullable.HasValue)
      {
        curyTaxableAmt = 0.0M;
        taxableAmt = 0.0M;
      }
    }
    nullable = taxrev.TaxableMax;
    num1 = 0.0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    Decimal num3 = taxableAmt;
    nullable = taxrev.TaxableMax;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    if (!(num3 > valueOrDefault1 & nullable.HasValue))
      return;
    PXCache sender = sumcache;
    TaxDetail row = taxdet;
    nullable = taxrev.TaxableMax;
    Decimal baseval = nullable.Value;
    ref Decimal local1 = ref curyTaxableAmt;
    MultiCurrencyCalculator.CuryConvCury(sender, (object) row, baseval, out local1);
    ref Decimal local2 = ref taxableAmt;
    nullable = taxrev.TaxableMax;
    Decimal num4 = nullable.Value;
    local2 = num4;
  }

  private static void SetValueOptional(PXCache cache, object data, object value, string field)
  {
    int fieldOrdinal = cache.GetFieldOrdinal(field);
    if (fieldOrdinal < 0)
      return;
    cache.SetValue(data, fieldOrdinal, value);
  }

  private TaxDetail TaxSummarize(PXCache sender, object taxrow, object row)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    PXCache cach = sender.Graph.Caches[this._TaxSumType];
    TaxDetail taxSum = this.CalculateTaxSum(sender, taxrow, row);
    if (taxSum != null)
      return (TaxDetail) cach.Update((object) taxSum);
    if (row != null && !this.IsTaxCalculationNeeded(sender, row))
      return (TaxDetail) null;
    TaxDetail taxDetail = (TaxDetail) ((PXResult) taxrow)[0];
    this.Delete(cach, (object) taxDetail);
    return (TaxDetail) null;
  }

  protected virtual void CalcTaxes(PXCache sender, object row)
  {
    this.CalcTaxes(sender, row, PXTaxCheck.RecalcLine);
  }

  /// <summary>
  /// This method is intended to select document line for given tax row.
  /// Do not use it to select parent document foir given line.
  /// </summary>
  /// <param name="cache">Cache of the tax row.</param>
  /// <param name="row">Tax row for which line will be returned.</param>
  /// <returns>Document line object.</returns>
  protected virtual object SelectParent(PXCache cache, object row)
  {
    return PXParentAttribute.SelectParent(cache, row, this._ChildType);
  }

  protected virtual void CalcTaxes(PXCache sender, object row, PXTaxCheck taxchk)
  {
    this.CalcTaxes(sender, row, taxchk, true);
  }

  protected virtual void CalcTaxes(PXCache sender, object row, PXTaxCheck taxchk, bool calcTaxes)
  {
    PXCache cach = sender.Graph.Caches[this._TaxType];
    object row1 = row;
    foreach (object selectTax in this.SelectTaxes(sender, row, taxchk))
    {
      if (row == null)
        row1 = this.SelectParent(cach, ((PXResult) selectTax)[0]);
      if (row1 != null)
        this.TaxSetLineDefault(sender, selectTax, row1);
    }
    this.CalcTotals(sender, row, calcTaxes && this.IsTaxCalculationNeeded(sender, row));
  }

  /// <summary>
  /// This method can be overridden to disable allow to disable automatic tax recalculation
  /// </summary>
  public virtual bool IsTaxCalculationNeeded(PXCache sender, object row) => true;

  public virtual IEnumerable<T> DistributeTaxDiscrepancy<T, CuryTaxField, BaseTaxField>(
    PXGraph graph,
    IEnumerable<T> taxDetList,
    Decimal CuryTaxAmt,
    bool updateCache)
    where T : TaxDetail, ITranTax
    where CuryTaxField : IBqlField
    where BaseTaxField : IBqlField
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    T row = default (T);
    PXCache cach = graph.Caches[this._TaxType];
    foreach (T taxDet in taxDetList)
    {
      Decimal num3 = (Decimal) cach.GetValue<CuryTaxField>((object) taxDet);
      Decimal num4 = (Decimal) (cach.GetValue((object) taxDet, this._CuryTaxableAmt) ?? (object) 0M);
      num1 += num3;
      num2 += num4;
      if ((object) row == null)
        row = taxDet;
      else if (Math.Abs((Decimal) (cach.GetValue((object) row, this._CuryTaxableAmt) ?? (object) 0M)) < Math.Abs(num4))
        row = taxDet;
    }
    Decimal num5 = CuryTaxAmt - num1;
    if (Math.Abs(num5) > 0M)
    {
      Decimal num6 = 0M;
      foreach (T taxDet in taxDetList)
      {
        Decimal num7 = MultiCurrencyCalculator.RoundCury(cach, (object) taxDet, num5 * (num2 != 0M ? (Decimal) (cach.GetValue((object) taxDet, this._CuryTaxableAmt) ?? (object) 0M) / num2 : 1M / (Decimal) taxDetList.Count<T>()));
        Decimal curyval = (Decimal) cach.GetValue<CuryTaxField>((object) taxDet) + num7;
        cach.SetValue<CuryTaxField>((object) taxDet, (object) curyval);
        num6 += num7;
        Decimal baseval;
        MultiCurrencyCalculator.CuryConvBase(cach, (object) taxDet, curyval, out baseval);
        cach.SetValue<BaseTaxField>((object) taxDet, (object) baseval);
        if (updateCache)
          this.Update(cach, (object) taxDet);
      }
      if (num6 != num5 && (object) row != null)
      {
        Decimal curyval = (Decimal) cach.GetValue<CuryTaxField>((object) row) + num5 - num6;
        cach.SetValue<CuryTaxField>((object) row, (object) curyval);
        Decimal baseval;
        MultiCurrencyCalculator.CuryConvBase(cach, (object) row, curyval, out baseval);
        cach.SetValue<BaseTaxField>((object) row, (object) baseval);
        if (updateCache)
          this.Update(cach, (object) row);
      }
    }
    return taxDetList;
  }

  protected virtual void CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    this._CalcDocTotals(sender, row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal, CuryTaxDiscountTotal);
  }

  protected virtual Decimal CalcLineTotal(PXCache sender, object row)
  {
    Decimal num = 0M;
    object[] objArray = PXParentAttribute.SelectSiblings(sender, (object) null);
    if (objArray != null)
    {
      foreach (object obj in objArray)
        num += this.GetCuryTranAmt(sender, sender.ObjectsEqual(obj, row) ? row : obj).GetValueOrDefault();
    }
    return num;
  }

  protected virtual void _CalcDocTotals(
    PXCache sender,
    object row,
    Decimal CuryTaxTotal,
    Decimal CuryInclTaxTotal,
    Decimal CuryWhTaxTotal,
    Decimal CuryTaxDiscountTotal)
  {
    Decimal objA1 = this.CalcLineTotal(sender, row);
    Decimal objA2 = objA1 + CuryTaxTotal - CuryInclTaxTotal;
    Decimal objB1 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryLineTotal) ?? (object) 0M);
    Decimal objB2 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryTaxTotal) ?? (object) 0M);
    if (!object.Equals((object) objA1, (object) objB1) || !object.Equals((object) CuryTaxTotal, (object) objB2))
    {
      this.ParentSetValue(sender.Graph, this._CuryLineTotal, (object) objA1);
      this.ParentSetValue(sender.Graph, this._CuryTaxTotal, (object) CuryTaxTotal);
      if (!string.IsNullOrEmpty(this._CuryTaxInclTotal))
        this.ParentSetValue(sender.Graph, this._CuryTaxInclTotal, (object) CuryInclTaxTotal);
      if (!string.IsNullOrEmpty(this._CuryDocBal))
      {
        this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
        return;
      }
    }
    if (string.IsNullOrEmpty(this._CuryDocBal))
      return;
    Decimal objB3 = (Decimal) (this.ParentGetValue(sender.Graph, this._CuryDocBal) ?? (object) 0M);
    if (object.Equals((object) objA2, (object) objB3))
      return;
    this.ParentSetValue(sender.Graph, this._CuryDocBal, (object) objA2);
  }

  protected virtual TaxDetail GetTaxDetail(
    PXCache sender,
    object taxrow,
    object row,
    out bool NeedUpdate)
  {
    if (taxrow == null)
      throw new PXArgumentException(nameof (taxrow), "The argument cannot be null.");
    NeedUpdate = false;
    return (TaxDetail) ((PXResult) taxrow)[0];
  }

  protected virtual void CalcTotals(PXCache sender, object row, bool CalcTaxes)
  {
    bool flag = false;
    Decimal CuryTaxTotal = 0M;
    Decimal CuryTaxDiscountTotal = 0M;
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal CuryWhTaxTotal = 0M;
    foreach (object selectTax in this.SelectTaxes(sender, row, PXTaxCheck.RecalcTotals))
    {
      TaxDetail data;
      if (CalcTaxes)
      {
        data = this.TaxSummarize(sender, selectTax, row);
      }
      else
      {
        bool NeedUpdate;
        data = this.GetTaxDetail(sender, selectTax, row, out NeedUpdate);
        if (NeedUpdate)
          data = (TaxDetail) sender.Graph.Caches[this._TaxSumType].Update((object) data);
      }
      if (data != null && PXResult.Unwrap<Tax>(selectTax).TaxType == "P")
        flag = true;
      else if (data != null)
      {
        PXCache cach = sender.Graph.Caches[((object) data).GetType()];
        Decimal num3 = (Decimal) cach.GetValue((object) data, this._CuryTaxAmt);
        Decimal optionalDecimalValue = this.GetOptionalDecimalValue(cach, (object) data, this._CuryTaxDiscountAmt);
        bool optionalBooleanValue = this.GetOptionalBooleanValue(cach, (object) data, this._IsTaxInclusive);
        Decimal num4 = PXResult.Unwrap<Tax>(selectTax).ReverseTax.GetValueOrDefault() ? -1M : 1M;
        if (PXResult.Unwrap<Tax>(selectTax).TaxType == "W")
          CuryWhTaxTotal += num4 * num3;
        if (PXResult.Unwrap<Tax>(selectTax).TaxCalcLevel == "0" | optionalBooleanValue)
        {
          num1 += num4 * num3;
          num2 += num4 * optionalDecimalValue;
        }
        CuryTaxTotal += num4 * num3;
        CuryTaxDiscountTotal += num4 * optionalDecimalValue;
        if (this.IsDeductibleVATTax(PXResult.Unwrap<Tax>(selectTax)))
        {
          Decimal num5 = CuryTaxTotal;
          Decimal num6 = num4;
          Decimal? curyExpenseAmt = data.CuryExpenseAmt;
          Decimal num7 = curyExpenseAmt.Value;
          Decimal num8 = num6 * num7;
          CuryTaxTotal = num5 + num8;
          if (PXResult.Unwrap<Tax>(selectTax).TaxCalcLevel == "0")
          {
            Decimal num9 = num1;
            Decimal num10 = num4;
            curyExpenseAmt = data.CuryExpenseAmt;
            Decimal num11 = curyExpenseAmt.Value;
            Decimal num12 = num10 * num11;
            num1 = num9 + num12;
          }
        }
      }
    }
    if (this.ParentGetStatus(sender.Graph) != 3 && this.ParentGetStatus(sender.Graph) != 4)
      this.CalcDocTotals(sender, row, CuryTaxTotal, num1 + num2, CuryWhTaxTotal, CuryTaxDiscountTotal);
    if (!flag || sender.Graph.UnattendedMode)
      return;
    this.ParentCache(sender.Graph).RaiseExceptionHandling(this._CuryTaxTotal, this.ParentRow(sender.Graph), (object) CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", (PXErrorLevel) 2));
  }

  private Decimal GetOptionalDecimalValue(PXCache cache, object data, string field)
  {
    Decimal optionalDecimalValue = 0M;
    int fieldOrdinal = cache.GetFieldOrdinal(field);
    if (fieldOrdinal >= 0)
      optionalDecimalValue = (Decimal) (cache.GetValue(data, fieldOrdinal) ?? (object) 0M);
    return optionalDecimalValue;
  }

  private bool GetOptionalBooleanValue(PXCache cache, object data, string field)
  {
    bool optionalBooleanValue = false;
    int fieldOrdinal = cache.GetFieldOrdinal(field);
    if (fieldOrdinal >= 0)
      optionalBooleanValue = (bool) (cache.GetValue(data, fieldOrdinal) ?? (object) false);
    return optionalBooleanValue;
  }

  protected virtual PXCache ParentCache(PXGraph graph) => graph.Caches[this._ParentType];

  protected virtual object ParentRow(PXGraph graph)
  {
    return this._ParentRow == null ? this.ParentCache(graph).Current : this._ParentRow;
  }

  protected virtual PXEntryStatus ParentGetStatus(PXGraph graph)
  {
    PXCache pxCache = this.ParentCache(graph);
    return this._ParentRow == null ? pxCache.GetStatus(pxCache.Current) : pxCache.GetStatus(this._ParentRow);
  }

  protected virtual void ParentSetValue(PXGraph graph, string fieldname, object value)
  {
    PXCache pxCache = this.ParentCache(graph);
    if (this._ParentRow == null)
    {
      object copy = pxCache.CreateCopy(pxCache.Current);
      pxCache.SetValueExt(pxCache.Current, fieldname, value);
      GraphHelper.MarkUpdated(pxCache, pxCache.Current);
      pxCache.RaiseRowUpdated(pxCache.Current, copy);
    }
    else
      pxCache.SetValueExt(this._ParentRow, fieldname, value);
  }

  protected virtual object ParentGetValue(PXGraph graph, string fieldname)
  {
    PXCache pxCache = this.ParentCache(graph);
    return this._ParentRow == null ? pxCache.GetValue(pxCache.Current, fieldname) : pxCache.GetValue(this._ParentRow, fieldname);
  }

  protected object ParentGetValue<Field>(PXGraph graph) where Field : IBqlField
  {
    return this.ParentGetValue(graph, typeof (Field).Name.ToLower());
  }

  protected void ParentSetValue<Field>(PXGraph graph, object value) where Field : IBqlField
  {
    this.ParentSetValue(graph, typeof (Field).Name.ToLower(), value);
  }

  protected virtual bool CompareZone(PXGraph graph, string zoneA, string zoneB)
  {
    if (!string.Equals(zoneA, zoneB, StringComparison.OrdinalIgnoreCase))
    {
      if (this.IsExternalTax(graph, zoneA) != this.IsExternalTax(graph, zoneB))
        return false;
      foreach (PXResult pxResult in PXSelectBase<TaxZoneDet, PXSelectGroupBy<TaxZoneDet, Where<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>, Or<TaxZoneDet.taxZoneID, Equal<Required<TaxZoneDet.taxZoneID>>>>, Aggregate<GroupBy<TaxZoneDet.taxID, Count>>>.Config>.Select(graph, new object[2]
      {
        (object) zoneA,
        (object) zoneB
      }))
      {
        if (pxResult.RowCount.GetValueOrDefault() == 1)
          return false;
      }
    }
    return true;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXRowInsertedSubscriber) || typeof (ISubscriber) == typeof (IPXRowUpdatedSubscriber) || typeof (ISubscriber) == typeof (IPXRowDeletedSubscriber))
      subscribers.Add(this as ISubscriber);
    else
      base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    object obj = (object) null;
    bool flag = false;
    if (this._TaxCalc != TaxCalc.NoCalc && this._TaxCalc != TaxCalc.ManualLineCalc)
    {
      this.RaiseAttributesRowInserted(sender, e);
      obj = sender.CreateCopy(e.Row);
      if (!this.inserted.ContainsKey(e.Row))
      {
        this.inserted[e.Row] = obj;
        flag = true;
      }
    }
    if (this.GetTaxCategory(sender, e.Row) == null)
    {
      Decimal? curyTranAmt;
      if (!(curyTranAmt = this.GetCuryTranAmt(sender, e.Row)).HasValue)
        return;
      Decimal? nullable = curyTranAmt;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        return;
    }
    if (this._TaxCalc == TaxCalc.Calc)
    {
      this.Preload(sender);
      this.DefaultTaxes(sender, e.Row);
      this.CalcTaxes(sender, e.Row, PXTaxCheck.Line);
    }
    else if (this._TaxCalc == TaxCalc.ManualCalc)
      this.CalcTotals(sender, e.Row, false);
    if (this._TaxCalc == TaxCalc.NoCalc || this._TaxCalc == TaxCalc.ManualLineCalc)
      return;
    this.RaiseAttributesRowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
    if (!flag)
      return;
    object copy = sender.CreateCopy(e.Row);
    this.inserted[e.Row] = copy;
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    object obj = (object) null;
    bool flag1 = false;
    if (this._TaxCalc != TaxCalc.NoCalc && this._TaxCalc != TaxCalc.ManualLineCalc)
    {
      this.RaiseAttributesRowUpdated(sender, e);
      obj = sender.CreateCopy(e.Row);
      if (!this.updated.ContainsKey(e.Row))
      {
        this.updated[e.Row] = obj;
        flag1 = true;
      }
    }
    if (this._TaxCalc == TaxCalc.Calc)
    {
      string taxZone1 = this.GetTaxZone(sender, e.OldRow);
      string taxZone2 = this.GetTaxZone(sender, e.Row);
      if (!this.CompareZone(sender.Graph, taxZone1, taxZone2))
      {
        this.Preload(sender);
        this.ReDefaultTaxes(sender, e.OldRow, e.Row, false);
      }
      else if (!object.Equals((object) this.GetTaxCategory(sender, e.OldRow), (object) this.GetTaxCategory(sender, e.Row)))
      {
        this.Preload(sender);
        this.ReDefaultTaxes(sender, e.OldRow, e.Row);
      }
      else if (!object.Equals((object) this.GetTaxID(sender, e.OldRow), (object) this.GetTaxID(sender, e.Row)))
      {
        PXCache cach = sender.Graph.Caches[this._TaxType];
        TaxDetail instance = (TaxDetail) cach.CreateInstance();
        instance.TaxID = this.GetTaxID(sender, e.OldRow);
        this.DelOneTax(cach, e.Row, (object) instance);
        this.AddOneTax(cach, e.Row, (ITaxDetail) new TaxZoneDet()
        {
          TaxID = this.GetTaxID(sender, e.Row)
        });
      }
      bool flag2 = false;
      if (this.ShouldRecalculateTaxesOnRowUpdate(sender, e.Row, e.OldRow))
      {
        this.CalcTaxes(sender, e.Row, PXTaxCheck.Line);
        flag2 = true;
      }
      if (!flag2)
        this.CalcTotals(sender, e.Row, false);
    }
    else if (this._TaxCalc == TaxCalc.ManualCalc)
      this.CalcTotals(sender, e.Row, false);
    if (this._TaxCalc == TaxCalc.NoCalc || this._TaxCalc == TaxCalc.ManualLineCalc)
      return;
    this.RaiseAttributesRowUpdated(sender, new PXRowUpdatedEventArgs(e.Row, obj, false));
    if (!flag1)
      return;
    object copy = sender.CreateCopy(e.Row);
    this.updated[e.Row] = copy;
  }

  protected virtual bool ShouldRecalculateTaxesOnRowUpdate(
    PXCache rowCache,
    object newRow,
    object oldRow)
  {
    if (this.GetTaxZone(rowCache, oldRow) != this.GetTaxZone(rowCache, newRow) || this.GetTaxCategory(rowCache, oldRow) != this.GetTaxCategory(rowCache, newRow))
      return true;
    Decimal? curyTranAmt1 = this.GetCuryTranAmt(rowCache, oldRow);
    Decimal? curyTranAmt2 = this.GetCuryTranAmt(rowCache, newRow);
    Decimal? nullable1 = curyTranAmt1;
    Decimal? nullable2 = curyTranAmt2;
    return !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || this.GetTaxID(rowCache, oldRow) != this.GetTaxID(rowCache, newRow) || PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>() && (this.GetLineQty(rowCache, oldRow).GetValueOrDefault() != this.GetLineQty(rowCache, newRow).GetValueOrDefault() || this.GetUOM(rowCache, oldRow) != this.GetUOM(rowCache, newRow));
  }

  public virtual void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.NoCalc)
      this.RaiseAttributesRowDeleted(sender, e);
    PXEntryStatus status = this.ParentGetStatus(sender.Graph);
    if (status == 3 || status == 4)
      return;
    if (this.GetTaxCategory(sender, e.Row) == null)
    {
      Decimal? curyTranAmt;
      if (!(curyTranAmt = this.GetCuryTranAmt(sender, e.Row)).HasValue)
        return;
      Decimal? nullable = curyTranAmt;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        return;
    }
    if (this._TaxCalc == TaxCalc.Calc || this._TaxCalc == TaxCalc.ManualLineCalc)
    {
      this.ClearTaxes(sender, e.Row);
      this.CalcTaxes(sender, (object) null, PXTaxCheck.Line, this.IsTaxCalculationNeeded(sender, e.Row));
    }
    else
    {
      if (this._TaxCalc != TaxCalc.ManualCalc)
        return;
      this.CalcTotals(sender, e.Row, false);
    }
  }

  protected virtual void RaiseAttributesRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    foreach (IPXRowInsertedSubscriber insertedSubscriber in ((IEnumerable) this._Attributes).OfType<IPXRowInsertedSubscriber>().ToArray<IPXRowInsertedSubscriber>())
      insertedSubscriber.RowInserted(sender, e);
  }

  protected virtual void RaiseAttributesRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    foreach (IPXRowUpdatedSubscriber updatedSubscriber in ((IEnumerable) this._Attributes).OfType<IPXRowUpdatedSubscriber>().ToArray<IPXRowUpdatedSubscriber>())
      updatedSubscriber.RowUpdated(sender, e);
  }

  protected virtual void RaiseAttributesRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    foreach (IPXRowDeletedSubscriber deletedSubscriber in ((IEnumerable) this._Attributes).OfType<IPXRowDeletedSubscriber>().ToArray<IPXRowDeletedSubscriber>())
      deletedSubscriber.RowDeleted(sender, e);
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1)
      return;
    if (this.inserted != null)
      this.inserted.Clear();
    if (this.updated == null)
      return;
    this.updated.Clear();
  }

  protected virtual void CurrencyInfo_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc || e.Row == null || !((PX.Objects.CM.CurrencyInfo) e.Row).CuryRate.HasValue || e.OldRow != null && sender.ObjectsEqual<PX.Objects.CM.CurrencyInfo.curyRate, PX.Objects.CM.CurrencyInfo.curyMultDiv>(e.Row, e.OldRow))
      return;
    PXView view = CurrencyInfoAttribute.GetView(sender.Graph, this._ChildType, this._CuryKeyField);
    if (view == null || view.SelectSingle(Array.Empty<object>()) == null)
      return;
    this.CalcTaxes(view.Cache, (object) null);
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc)
      return;
    if (e.Row.GetType() == this._ParentType)
      this._ParentRow = e.Row;
    this.CalcTaxes(sender.Graph.Caches[this._ChildType], (object) null);
    this._ParentRow = (object) null;
  }

  protected virtual void IsTaxSavedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, this._CuryWhTaxTotal);
    if (!(((Decimal?) sender.GetValue(e.Row, this._CuryLineTotal)).GetValueOrDefault() == 0M))
      return;
    this.CalcDocTotals(sender, e.Row, nullable1.GetValueOrDefault(), 0M, nullable2.GetValueOrDefault(), 0M);
  }

  protected virtual List<object> ChildSelect(PXCache cache, object data)
  {
    return TaxParentAttribute.ChildSelect(cache, data, this._ParentType);
  }

  protected virtual void ZoneUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    TaxCalc taxCalc = this.TaxCalc;
    try
    {
      if (this.IsExternalTax(sender.Graph, (string) e.OldValue))
        this.TaxCalc = TaxCalc.Calc;
      if (this.IsExternalTax(sender.Graph, (string) sender.GetValue(e.Row, this._TaxZoneID)) || ((bool?) sender.GetValue(e.Row, this._ExternalTaxesImportInProgress)).GetValueOrDefault() || !this.IsTaxCalculationNeeded(sender, e.Row))
        this.TaxCalc = TaxCalc.ManualCalc;
      if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc)
        return;
      PXCache cach = sender.Graph.Caches[this._ChildType];
      if (this.CompareZone(sender.Graph, (string) e.OldValue, (string) sender.GetValue(e.Row, this._TaxZoneID)) && sender.GetValue(e.Row, this._TaxZoneID) != null)
        return;
      this.Preload(sender);
      List<object> objectList = this.ChildSelect(cach, e.Row);
      this.OnZoneUpdated(new TaxBaseAttribute.ZoneUpdatedArgs()
      {
        Cache = cach,
        Details = objectList,
        OldValue = (string) e.OldValue,
        NewValue = (string) sender.GetValue(e.Row, this._TaxZoneID)
      });
      this._ParentRow = e.Row;
      this.CalcTaxes(cach, (object) null);
      this._ParentRow = (object) null;
    }
    finally
    {
      this.TaxCalc = taxCalc;
    }
  }

  protected virtual void OnZoneUpdated(TaxBaseAttribute.ZoneUpdatedArgs e)
  {
    this.ReDefaultTaxes(e.Cache, e.Details);
  }

  protected virtual void ReDefaultTaxes(PXCache cache, List<object> details)
  {
    foreach (object detail in details)
    {
      this.ClearTaxes(cache, detail);
      this.ClearChildTaxAmts(cache, detail);
    }
    foreach (object detail in details)
      this.DefaultTaxes(cache, detail, false);
  }

  protected virtual void ClearChildTaxAmts(PXCache cache, object childRow)
  {
    PXCache cach = cache.Graph.Caches[this._ChildType];
    this.SetTaxableAmt(cach, childRow, new Decimal?(0M));
    this.SetTaxAmt(cach, childRow, new Decimal?(0M));
    if (cach.Locate(childRow) == null)
      return;
    GraphHelper.MarkUpdated(cach, childRow, true);
  }

  protected virtual void ReDefaultTaxes(
    PXCache cache,
    object clearDet,
    object defaultDet,
    bool defaultExisting = true)
  {
    this.ClearTaxes(cache, clearDet);
    this.ClearChildTaxAmts(cache, defaultDet);
    this.DefaultTaxes(cache, defaultDet, defaultExisting);
  }

  protected virtual void DateUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this._TaxCalc != TaxCalc.Calc && this._TaxCalc != TaxCalc.ManualLineCalc)
      return;
    this.Preload(sender);
    PXCache cach = sender.Graph.Caches[this._ChildType];
    foreach (object obj in this.ChildSelect(cach, e.Row))
      this.ReDefaultTaxes(cach, obj, obj);
    this._ParentRow = e.Row;
    try
    {
      this.CalcTaxes(cach, (object) null);
    }
    finally
    {
      this._ParentRow = (object) null;
    }
  }

  protected abstract void SetExtCostExt(PXCache sender, object child, Decimal? value);

  protected abstract string GetExtCostLabel(PXCache sender, object row);

  protected string GetTaxCalcMode(PXGraph graph)
  {
    if (!this._isTaxCalcModeEnabled)
      throw new PXException("Document Tax Calculation mode is not enabled!");
    string taxCalcMode = (string) this.ParentGetValue(graph, this._TaxCalcMode);
    if (string.IsNullOrWhiteSpace(taxCalcMode))
      taxCalcMode = "T";
    return taxCalcMode;
  }

  protected string GetOriginalTaxCalcMode(PXGraph graph)
  {
    if (!this._isTaxCalcModeEnabled)
      throw new PXException("Document Tax Calculation mode is not enabled!");
    return !string.IsNullOrEmpty(this._PreviousTaxCalcMode) ? this._PreviousTaxCalcMode : (string) this.ParentGetValue(graph, this._TaxCalcMode);
  }

  protected virtual bool AskRecalculate(PXCache sender, PXCache detailCache, object detail)
  {
    return sender.Graph.Views[sender.Graph.PrimaryView].Ask(PXLocalizer.LocalizeFormat("Do you want the system to recalculate the amount(s) in the '{0}' column?", new object[1]
    {
      (object) this.GetExtCostLabel(detailCache, detail)
    }), (MessageButtons) 4) == 6;
  }

  protected virtual void TaxCalcModeUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    string str = sender.GetValue(e.Row, this._TaxCalcMode) as string;
    this._PreviousTaxCalcMode = e.OldValue as string;
    if (!(str != (string) e.OldValue))
      return;
    PXCache cach1 = sender.Graph.Caches[this._ChildType];
    List<object> objectList = this.ChildSelect(cach1, e.Row);
    Decimal? nullable1 = (Decimal?) sender.GetValue(e.Row, this._CuryTaxTotal);
    if (objectList != null && objectList.Count != 0 && this.AskRecalculationOnCalcModeChange && nullable1.HasValue && nullable1.Value != 0M && this.AskRecalculate(sender, cach1, objectList[0]))
    {
      PXCache cach2 = cach1.Graph.Caches[this._TaxType];
      foreach (object obj in objectList)
      {
        TaxDetail taxDetail1 = this.TaxSummarizeOneLine(cach1, obj, TaxBaseAttribute.SummType.All);
        Decimal? nullable2;
        if (taxDetail1 != null)
        {
          switch (str)
          {
            case "N":
              nullable2 = (Decimal?) cach2.GetValue((object) taxDetail1, this._CuryTaxableAmt);
              this.SetExtCostExt(cach1, obj, new Decimal?(MultiCurrencyCalculator.RoundCury(cach1, obj, nullable2.Value, this.Precision)));
              continue;
            case "G":
              nullable2 = (Decimal?) cach2.GetValue((object) taxDetail1, this._CuryTaxableAmt);
              Decimal? nullable3 = (Decimal?) cach2.GetValue((object) taxDetail1, this._CuryTaxAmt);
              this.SetExtCostExt(cach1, obj, new Decimal?(MultiCurrencyCalculator.RoundCury(cach1, obj, nullable2.Value + nullable3.Value, this.Precision)));
              continue;
            case "T":
              TaxDetail taxDetail2 = this.TaxSummarizeOneLine(cach1, obj, TaxBaseAttribute.SummType.Inclusive);
              Decimal? nullable4;
              if (taxDetail2 != null)
              {
                Decimal? nullable5 = (Decimal?) cach2.GetValue((object) taxDetail2, this._CuryTaxableAmt);
                Decimal? nullable6 = (Decimal?) cach2.GetValue((object) taxDetail2, this._CuryTaxAmt);
                nullable4 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
              }
              else
                nullable4 = (Decimal?) cach2.GetValue((object) taxDetail1, this._CuryTaxableAmt);
              this.SetExtCostExt(cach1, obj, new Decimal?(MultiCurrencyCalculator.RoundCury(cach1, obj, nullable4.Value, this.Precision)));
              continue;
            default:
              continue;
          }
        }
      }
    }
    this.Preload(sender);
    if (objectList != null)
    {
      foreach (object obj in objectList)
        this.ReDefaultTaxes(cach1, obj, obj, false);
    }
    this._ParentRow = e.Row;
    this.CalcTaxes(cach1, (object) null);
    this._ParentRow = (object) null;
  }

  private TaxDetail TaxSummarizeOneLine(
    PXCache cache,
    object row,
    TaxBaseAttribute.SummType summType)
  {
    List<object> list = new List<object>();
    switch (summType)
    {
      case TaxBaseAttribute.SummType.Inclusive:
        list = !this.CalcGrossOnDocumentLevel || !this._isTaxCalcModeEnabled ? this.SelectTaxes<Where<Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<Tax.taxCalcType, Equal<CSTaxCalcType.item>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>>(cache.Graph, row, PXTaxCheck.Line) : this.SelectTaxes<Where<Tax.taxCalcLevel, Equal<CSTaxCalcLevel.inclusive>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>(cache.Graph, row, PXTaxCheck.Line);
        break;
      case TaxBaseAttribute.SummType.All:
        list = !this.CalcGrossOnDocumentLevel || !this._isTaxCalcModeEnabled ? this.SelectTaxes<Where<Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<Tax.taxCalcType, Equal<CSTaxCalcType.item>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>>(cache.Graph, row, PXTaxCheck.Line) : this.SelectTaxes<Where<Tax.taxCalcLevel, NotEqual<CSTaxCalcLevel.calcOnItemAmtPlusTaxAmt>, And<Tax.taxType, NotEqual<CSTaxType.withholding>, And<Tax.directTax, Equal<False>>>>>(cache.Graph, row, PXTaxCheck.Line);
        break;
    }
    if (list.Count == 0)
      return (TaxDetail) null;
    PXCache cach = cache.Graph.Caches[this._TaxType];
    TaxDetail instance = (TaxDetail) cach.CreateInstance();
    Decimal? nullable1 = (Decimal?) cach.GetValue(((PXResult) list[0])[0], this._CuryTaxableAmt);
    Decimal? nullable2 = new Decimal?(this.SumWithReverseAdjustment(cache.Graph, list, this.GetFieldType(cach, this._CuryTaxAmt)));
    if (this.CalcGrossOnDocumentLevel && this._isTaxCalcModeEnabled)
    {
      string originalTaxCalcMode = this.GetOriginalTaxCalcMode(cache.Graph);
      if (this.GetTaxCalcMode(cache.Graph) == "G" && originalTaxCalcMode != "G")
      {
        foreach (object obj in list)
        {
          Tax tax = (Tax) ((PXResult) obj)[typeof (Tax)];
          TaxRev taxRev = (TaxRev) ((PXResult) obj)[typeof (TaxRev)];
          if (tax?.TaxCalcType == "D")
          {
            Decimal? nullable3 = (Decimal?) cach.GetValue(((PXResult) obj)[0], this._CuryTaxAmt);
            Decimal? nullable4 = nullable1;
            Decimal? nullable5 = taxRev.TaxRate;
            Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault() / 100.0M) : new Decimal?();
            nullable5 = nullable2;
            Decimal? nullable7 = nullable6;
            Decimal? nullable8 = nullable3;
            nullable4 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable9;
            if (!(nullable5.HasValue & nullable4.HasValue))
            {
              nullable8 = new Decimal?();
              nullable9 = nullable8;
            }
            else
              nullable9 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
            nullable2 = nullable9;
          }
        }
      }
    }
    Decimal? nullable10 = new Decimal?(this.SumWithReverseAdjustment(cache.Graph, list, this.GetFieldType(cach, this._CuryExpenseAmt)));
    cach.SetValue((object) instance, this._CuryTaxableAmt, (object) nullable1);
    PXCache pxCache = cach;
    TaxDetail taxDetail = instance;
    string curyTaxAmt = this._CuryTaxAmt;
    Decimal? nullable11 = nullable2;
    Decimal? nullable12 = nullable10;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?());
    pxCache.SetValue((object) taxDetail, curyTaxAmt, (object) local);
    return instance;
  }

  private Decimal SumWithReverseAdjustment(PXGraph graph, List<object> list, Type field)
  {
    Decimal ret = 0.0M;
    list.ForEach((Action<object>) (a =>
    {
      Decimal? nullable = (Decimal?) graph.Caches[BqlCommand.GetItemType(field)].GetValue(((PXResult) a)[BqlCommand.GetItemType(field)], field.Name);
      Decimal num = ((Tax) ((PXResult) a)[typeof (Tax)]).ReverseTax.GetValueOrDefault() ? -1M : 1M;
      ret += nullable.GetValueOrDefault() * num;
    }));
    return ret;
  }

  protected virtual void TaxSum_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    object row1 = e.Row;
    if (row1 == null)
      return;
    Dictionary<string, object> keyFieldValues1 = this.GetKeyFieldValues(cache, row1);
    bool flag1 = true;
    if (ExternalTaxBase<PXGraph>.IsExternalTax(cache.Graph, (string) cache.GetValue(row1, this._TaxZoneID)))
      return;
    if (e.ExternalCall && e.Row is TaxDetail row2 && this.CheckIfTaxDetailHasPerUnitTaxType(cache.Graph, row2.TaxID))
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("Per-unit taxes cannot be inserted manually.");
    }
    if (this._IncludeDirectTaxLine && e.ExternalCall)
    {
      Tax tax = this.GetTax(cache.Graph, ((TaxDetail) row1).TaxID);
      if ((tax != null ? (tax.DirectTax.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("Direct-entry taxes cannot be inserted manually.");
      }
    }
    foreach (object row3 in cache.Cached)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      Dictionary<string, object> keyFieldValues2 = this.GetKeyFieldValues(cache, row3);
      bool flag2 = true;
      PXEntryStatus status = cache.GetStatus(row3);
      if (status != 3 && status != 4)
      {
        foreach (KeyValuePair<string, object> keyValuePair in keyFieldValues1)
        {
          if (keyFieldValues2.ContainsKey(keyValuePair.Key) && !object.Equals(keyFieldValues2[keyValuePair.Key], keyValuePair.Value))
          {
            flag2 = false;
            break;
          }
        }
        if (flag2)
        {
          if (cache.Graph.IsMobile)
          {
            cache.Delete(row3);
          }
          else
          {
            flag1 = false;
            break;
          }
        }
      }
    }
    if (flag1)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void TaxSum_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!e.ExternalCall || !this._IncludeDirectTaxLine || !(e.Row is TaxDetail row1))
      return;
    Tax tax = this.GetTax(cache.Graph, row1.TaxID);
    if ((tax != null ? (tax.DirectTax.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      string taxZone = this.GetTaxZone(cache, e.Row);
      string str = (string) null;
      PXCache cach = cache.Graph.Caches[this._ChildType];
      List<object> objectList = this.ChildSelect(cach, e.Row);
      HashSet<string> stringSet = new HashSet<string>();
      foreach (object row2 in objectList)
      {
        string taxCategory = this.GetTaxCategory(cach, row2);
        if (!string.IsNullOrWhiteSpace(taxCategory))
          stringSet.Add(taxCategory);
      }
      foreach (PXResult<TaxZoneDet, TaxCategory, TaxCategoryDet, Tax> pxResult in PXSelectBase<TaxZoneDet, PXViewOf<TaxZoneDet>.BasedOn<SelectFromBase<TaxZoneDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<TaxCategory>>, FbqlJoins.Left<TaxCategoryDet>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategoryDet.taxID, Equal<TaxZoneDet.taxID>>>>>.And<BqlOperand<TaxCategoryDet.taxCategoryID, IBqlString>.IsEqual<TaxCategory.taxCategoryID>>>>, FbqlJoins.Left<Tax>.On<BqlOperand<Tax.taxID, IBqlString>.IsEqual<TaxZoneDet.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxZoneDet.taxZoneID, Equal<P.AsString>>>>, And<BqlOperand<Tax.taxID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxCategory.taxCatFlag, Equal<False>>>>>.And<BqlOperand<TaxCategoryDet.taxCategoryID, IBqlString>.IsNotNull>>>>.Config>.Select(cache.Graph, new object[2]
      {
        (object) taxZone,
        (object) row1?.TaxID
      }))
      {
        str = PXResult<TaxZoneDet, TaxCategory, TaxCategoryDet, Tax>.op_Implicit(pxResult)?.TaxCategoryID;
        if (!string.IsNullOrWhiteSpace(str))
        {
          if (stringSet.Contains(str))
            break;
        }
      }
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("Direct-entry taxes cannot be deleted manually. To delete a direct-entry tax, change the {0} tax category in the document line or delete the line.", new object[1]
      {
        (object) str
      });
    }
  }

  private Dictionary<string, object> GetKeyFieldValues(PXCache cache, object row)
  {
    Dictionary<string, object> keyFieldValues = new Dictionary<string, object>();
    foreach (string key in (IEnumerable<string>) cache.Keys)
    {
      if (key != this._RecordID)
        keyFieldValues.Add(key, cache.GetValue(row, key));
    }
    return keyFieldValues;
  }

  protected virtual void DelOneTax(PXCache sender, object detrow, object taxrow)
  {
    PXCache cach = sender.Graph.Caches[this._ChildType];
    bool flag = false;
    foreach (object selectTax in this.SelectTaxes(cach, detrow, PXTaxCheck.Line))
    {
      if (object.Equals((object) ((TaxDetail) ((PXResult) selectTax)[0]).TaxID, (object) ((TaxDetail) taxrow).TaxID))
      {
        sender.Delete(((PXResult) selectTax)[0]);
        if (((Tax) ((PXResult) selectTax)[1]).TaxCalcLevel == "0")
          flag = true;
      }
    }
    if (!flag)
      return;
    this.SetTaxableAmt(cach, detrow, new Decimal?(0M));
    this.SetTaxAmt(cach, detrow, new Decimal?(0M));
    GraphHelper.MarkUpdated(cach, detrow);
  }

  protected virtual void Preload(PXCache sender)
  {
    this.SelectTaxes(sender, (object) null, PXTaxCheck.RecalcTotals);
  }

  /// <summary>
  /// During the import process, some fields may not have a default value.
  /// </summary>
  private static void InvokeExceptForExcelImport(PXCache cache, Action action)
  {
    if (cache.Graph.IsImportFromExcel || cache.Graph.IsCopyPasteContext)
      return;
    action();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TaxBaseAttribute.\u003C\u003Ec__DisplayClass299_0 displayClass2990 = new TaxBaseAttribute.\u003C\u003Ec__DisplayClass299_0();
    // ISSUE: reference to a compiler-generated field
    displayClass2990.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass2990.sender = sender;
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(displayClass2990.sender);
    // ISSUE: reference to a compiler-generated field
    this._ChildType = displayClass2990.sender.GetItemType();
    this.inserted = new Dictionary<object, object>();
    this.updated = new Dictionary<object, object>();
    // ISSUE: reference to a compiler-generated field
    PXCache cach = displayClass2990.sender.Graph.Caches[this._TaxType];
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    displayClass2990.sender.Graph.FieldUpdated.AddHandler(this._ParentType, this._DocDate, new PXFieldUpdated((object) displayClass2990, __methodptr(\u003CCacheAttached\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    displayClass2990.sender.Graph.FieldUpdated.AddHandler(this._ParentType, this._TaxZoneID, new PXFieldUpdated((object) displayClass2990, __methodptr(\u003CCacheAttached\u003Eb__1)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    displayClass2990.sender.Graph.FieldUpdated.AddHandler(this._ParentType, this._IsTaxSaved, new PXFieldUpdated((object) displayClass2990, __methodptr(\u003CCacheAttached\u003Eb__2)));
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldUpdatedEvents fieldUpdated1 = displayClass2990.sender.Graph.FieldUpdated;
    Type parentType1 = this._ParentType;
    string termsId = this._TermsID;
    TaxBaseAttribute taxBaseAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) taxBaseAttribute1, __vmethodptr(taxBaseAttribute1, ParentFieldUpdated));
    fieldUpdated1.AddHandler(parentType1, termsId, pxFieldUpdated1);
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldUpdatedEvents fieldUpdated2 = displayClass2990.sender.Graph.FieldUpdated;
    Type parentType2 = this._ParentType;
    string curyId = this._CuryID;
    TaxBaseAttribute taxBaseAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) taxBaseAttribute2, __vmethodptr(taxBaseAttribute2, ParentFieldUpdated));
    fieldUpdated2.AddHandler(parentType2, curyId, pxFieldUpdated2);
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldUpdatedEvents fieldUpdated3 = displayClass2990.sender.Graph.FieldUpdated;
    Type parentType3 = this._ParentType;
    string curyOrigDiscAmt = this._CuryOrigDiscAmt;
    TaxBaseAttribute taxBaseAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) taxBaseAttribute3, __vmethodptr(taxBaseAttribute3, CuryOrigDiscAmt_FieldUpdated));
    fieldUpdated3.AddHandler(parentType3, curyOrigDiscAmt, pxFieldUpdated3);
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowUpdatedEvents rowUpdated1 = displayClass2990.sender.Graph.RowUpdated;
    Type parentType4 = this._ParentType;
    TaxBaseAttribute taxBaseAttribute4 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated1 = new PXRowUpdated((object) taxBaseAttribute4, __vmethodptr(taxBaseAttribute4, ParentRowUpdated));
    rowUpdated1.AddHandler(parentType4, pxRowUpdated1);
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowInsertingEvents rowInserting = displayClass2990.sender.Graph.RowInserting;
    Type taxSumType1 = this._TaxSumType;
    TaxBaseAttribute taxBaseAttribute5 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) taxBaseAttribute5, __vmethodptr(taxBaseAttribute5, TaxSum_RowInserting));
    rowInserting.AddHandler(taxSumType1, pxRowInserting);
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowDeletingEvents rowDeleting1 = displayClass2990.sender.Graph.RowDeleting;
    Type taxSumType2 = this._TaxSumType;
    TaxBaseAttribute taxBaseAttribute6 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting1 = new PXRowDeleting((object) taxBaseAttribute6, __vmethodptr(taxBaseAttribute6, TaxSum_RowDeleting));
    rowDeleting1.AddHandler(taxSumType2, pxRowDeleting1);
    if (PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      displayClass2990.sender.Graph.RowPersisting.AddHandler(this._ChildType, new PXRowPersisting((object) this, __methodptr(DocumentLineCheckPerUnitTaxesOnRowPersisting)));
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowPersistingEvents rowPersisting = displayClass2990.sender.Graph.RowPersisting;
      Type parentType5 = this._ParentType;
      TaxBaseAttribute taxBaseAttribute7 = this;
      // ISSUE: virtual method pointer
      PXRowPersisting pxRowPersisting = new PXRowPersisting((object) taxBaseAttribute7, __vmethodptr(taxBaseAttribute7, DocumentCheckPerUnitTaxesOnRowPersisting));
      rowPersisting.AddHandler(parentType5, pxRowPersisting);
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowSelectedEvents rowSelected1 = displayClass2990.sender.Graph.RowSelected;
      Type parentType6 = this._ParentType;
      TaxBaseAttribute taxBaseAttribute8 = this;
      // ISSUE: virtual method pointer
      PXRowSelected pxRowSelected1 = new PXRowSelected((object) taxBaseAttribute8, __vmethodptr(taxBaseAttribute8, CheckCurrencyAndRetainageOnDocumentRowSelected));
      rowSelected1.AddHandler(parentType6, pxRowSelected1);
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowDeletingEvents rowDeleting2 = displayClass2990.sender.Graph.RowDeleting;
      Type taxSumType3 = this._TaxSumType;
      TaxBaseAttribute taxBaseAttribute9 = this;
      // ISSUE: virtual method pointer
      PXRowDeleting pxRowDeleting2 = new PXRowDeleting((object) taxBaseAttribute9, __vmethodptr(taxBaseAttribute9, CheckForPerUnitTaxesOnAggregatedTaxRowDeleting));
      rowDeleting2.AddHandler(taxSumType3, pxRowDeleting2);
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowSelectedEvents rowSelected2 = displayClass2990.sender.Graph.RowSelected;
      Type taxSumType4 = this._TaxSumType;
      TaxBaseAttribute taxBaseAttribute10 = this;
      // ISSUE: virtual method pointer
      PXRowSelected pxRowSelected2 = new PXRowSelected((object) taxBaseAttribute10, __vmethodptr(taxBaseAttribute10, DisablePerUnitTaxesOnAggregatedTaxDetailRowSelected));
      rowSelected2.AddHandler(taxSumType4, pxRowSelected2);
    }
    // ISSUE: reference to a compiler-generated field
    foreach (PXEventSubscriberAttribute subscriberAttribute in displayClass2990.sender.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is CurrencyInfoAttribute)
      {
        // ISSUE: reference to a compiler-generated field
        this._CuryKeyField = displayClass2990.sender.GetBqlField(subscriberAttribute.FieldName);
        break;
      }
    }
    if (this._CuryKeyField != (Type) null)
    {
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowUpdatedEvents rowUpdated2 = displayClass2990.sender.Graph.RowUpdated;
      TaxBaseAttribute taxBaseAttribute11 = this;
      // ISSUE: virtual method pointer
      PXRowUpdated pxRowUpdated2 = new PXRowUpdated((object) taxBaseAttribute11, __vmethodptr(taxBaseAttribute11, CurrencyInfo_RowUpdated));
      rowUpdated2.AddHandler<PX.Objects.CM.CurrencyInfo>(pxRowUpdated2);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    displayClass2990.sender.Graph.Caches.SubscribeCacheCreated<Tax>(new Action(displayClass2990.\u003CCacheAttached\u003Eb__3));
    if (!this._isTaxCalcModeEnabled)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    displayClass2990.sender.Graph.FieldUpdated.AddHandler(this._ParentType, this._TaxCalcMode, new PXFieldUpdated((object) displayClass2990, __methodptr(\u003CCacheAttached\u003Eb__4)));
  }

  public TaxBaseAttribute(
    Type ParentType,
    Type TaxType,
    Type TaxSumType,
    Type CalcMode = null,
    Type parentBranchIDField = null)
  {
    this.ParentBranchIDField = parentBranchIDField;
    this.ChildFinPeriodIDField = typeof (TaxTran.finPeriodID);
    this.ChildBranchIDField = typeof (TaxTran.branchID);
    this._ParentType = ParentType;
    this._TaxType = TaxType;
    this._TaxSumType = TaxSumType;
    if (!(CalcMode != (Type) null))
      return;
    this.TaxCalcMode = typeof (IBqlField).IsAssignableFrom(CalcMode) ? CalcMode : throw new PXArgumentException(nameof (CalcMode), "An invalid argument has been specified.");
  }

  public virtual int CompareTo(object other) => 0;

  protected virtual IComparer<Tax> GetTaxByCalculationLevelComparer()
  {
    return (IComparer<Tax>) TaxByCalculationLevelComparer.Instance;
  }

  protected virtual IDictionary<string, PXResult<Tax, TaxRev>> CollectInvolvedTaxes<TWhere>(
    PXGraph graph,
    IEnumerable<ITaxDetail> details,
    BqlCommand select,
    object[] currents,
    object[] whereParameters,
    object[] selectParameters = null)
    where TWhere : IBqlWhere, new()
  {
    IDictionary<string, PXResult<Tax, TaxRev>> dictionary = (IDictionary<string, PXResult<Tax, TaxRev>>) new Dictionary<string, PXResult<Tax, TaxRev>>();
    if (select == null)
      return dictionary;
    HashSet<string> source = new HashSet<string>(details.Select<ITaxDetail, string>((Func<ITaxDetail, string>) (d => d.TaxID)));
    object[] objArray = EnumerableExtensions.Prepend<object>(whereParameters, (object) source.ToArray<string>());
    if (selectParameters != null && ((IEnumerable<object>) selectParameters).Any<object>())
      objArray = EnumerableExtensions.Prepend<object>(objArray, selectParameters);
    select = select.WhereAnd<Where2<Where<Tax.taxID, In<Required<Tax.taxID>>>, And<TWhere>>>();
    foreach (PXResult<Tax, TaxRev> pxResult in select.CreateView(graph).SelectMultiBound(currents, objArray))
    {
      Tax tax = this.AdjustTaxLevel(graph, PXResult<Tax, TaxRev>.op_Implicit(pxResult));
      dictionary[PXResult<Tax, TaxRev>.op_Implicit(pxResult).TaxID] = new PXResult<Tax, TaxRev>(tax, PXResult<Tax, TaxRev>.op_Implicit(pxResult));
    }
    return dictionary;
  }

  protected virtual bool InnerLineNbrCondition<TTaxDetail>(
    PXTaxCheck taxchk,
    TTaxDetail record,
    int index,
    List<object> taxList)
    where TTaxDetail : class, ITaxDetail, IBqlTable, new()
  {
    if (taxchk != PXTaxCheck.RecalcLine || !(record is ITaxDetailWithLineNbr detailWithLineNbr1) || !(PXResult<TTaxDetail, Tax, TaxRev>.op_Implicit((PXResult<TTaxDetail, Tax, TaxRev>) taxList[index]) is ITaxDetailWithLineNbr detailWithLineNbr2))
      return true;
    int? lineNbr1 = detailWithLineNbr2.LineNbr;
    int? lineNbr2 = detailWithLineNbr1.LineNbr;
    return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
  }

  protected virtual void InsertTax<TTaxDetail>(
    PXGraph graph,
    PXTaxCheck taxchk,
    TTaxDetail record,
    IDictionary<string, PXResult<Tax, TaxRev>> tails,
    List<object> taxList)
    where TTaxDetail : class, ITaxDetail, IBqlTable, new()
  {
    IComparer<Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<Tax>>(calculationLevelComparer, "taxByCalculationLevelComparer", (string) null);
    PXResult<Tax, TaxRev> pxResult;
    if (record.TaxID == null || !tails.TryGetValue(record.TaxID, out pxResult))
      return;
    int count = taxList.Count;
    while (count > 0 && this.InnerLineNbrCondition<TTaxDetail>(taxchk, record, count - 1, taxList) && calculationLevelComparer.Compare(PXResult<TTaxDetail, Tax, TaxRev>.op_Implicit((PXResult<TTaxDetail, Tax, TaxRev>) taxList[count - 1]), PXResult<Tax, TaxRev>.op_Implicit(pxResult)) > 0)
      --count;
    taxList.Insert(count, (object) new PXResult<TTaxDetail, Tax, TaxRev>(record, this.AdjustTaxLevel(graph, PXResult<Tax, TaxRev>.op_Implicit(pxResult)), PXResult<Tax, TaxRev>.op_Implicit(pxResult)));
  }

  protected virtual string TaxUomFieldNameForTaxDetail => "TaxUOM";

  protected virtual string TaxableQtyFieldNameForTaxDetail => "TaxableQty";

  /// <summary>
  /// The document line's UOM field for taxes per unit and specific taxes.
  /// </summary>
  public Type UOM
  {
    get => this._uom;
    set
    {
      this.CheckDocLineFieldTypes(value, nameof (UOM));
      this._uom = value;
    }
  }

  /// <summary>
  /// The document line's inventory for conversions to tax UOM.
  /// </summary>
  public Type Inventory
  {
    get => this._inventory;
    set
    {
      this.CheckDocLineFieldTypes(value, nameof (Inventory));
      this._inventory = value;
    }
  }

  /// <summary>
  /// The document line quantity field in line UOM that will be used for taxes per unit and specific taxes calculation.
  /// </summary>
  public Type LineQty
  {
    get => this._lineQty;
    set
    {
      this.CheckDocLineFieldTypes(value, nameof (LineQty));
      this._lineQty = value;
    }
  }

  private void CheckDocLineFieldTypes(Type fieldTypeNewValue, string docLineFieldName)
  {
    ExceptionExtensions.ThrowOnNull<Type>(fieldTypeNewValue, nameof (fieldTypeNewValue), (string) null);
    if (!typeof (IBqlField).IsAssignableFrom(fieldTypeNewValue))
      throw new ArgumentException("The docLineFieldName should be a type implementing IBqlField", nameof (fieldTypeNewValue));
  }

  protected virtual bool IsPerUnitTax(Tax tax) => tax?.TaxType == "Q";

  protected virtual void SetTaxUomForTaxDetail(
    PXCache taxDetailCache,
    TaxDetail taxDetail,
    string taxUOM)
  {
    taxDetailCache.SetValue((object) taxDetail, this.TaxUomFieldNameForTaxDetail, (object) taxUOM);
  }

  protected virtual Decimal? GetTaxableQuantityForTaxDetail(
    PXCache taxDetailCache,
    TaxDetail taxDetail)
  {
    return taxDetailCache.GetValue((object) taxDetail, this.TaxableQtyFieldNameForTaxDetail) as Decimal?;
  }

  protected virtual void SetTaxableQuantityForTaxDetail(
    PXCache taxDetailCache,
    TaxDetail taxDetail,
    Decimal? taxableQty)
  {
    taxDetailCache.SetValue((object) taxDetail, this.TaxableQtyFieldNameForTaxDetail, (object) taxableQty);
  }

  protected virtual Decimal? GetLineQty(PXCache rowCache, object row)
  {
    return !(this.LineQty != (Type) null) ? new Decimal?() : rowCache.GetValue(row, this.LineQty.Name) as Decimal?;
  }

  protected virtual string GetUOM(PXCache rowCache, object row)
  {
    return !(this.UOM != (Type) null) ? (string) null : rowCache.GetValue(row, this.UOM.Name) as string;
  }

  protected virtual int? GetInventory(PXCache rowCache, object row)
  {
    return !(this.Inventory != (Type) null) ? new int?() : rowCache.GetValue(row, this.Inventory.Name) as int?;
  }

  /// <summary>Fill tax details for line for per unit taxes.</summary>
  /// <exception cref="T:PX.Data.PXArgumentException">Thrown when a PX Argument error condition occurs.</exception>
  /// <param name="rowCache">The row cache.</param>
  /// <param name="row">The row.</param>
  /// <param name="tax">The tax.</param>
  /// <param name="taxRevision">The tax revision.</param>
  /// <param name="taxDetail">The tax detail.</param>
  protected virtual void TaxSetLineDefaultForPerUnitTaxes(
    PXCache rowCache,
    object row,
    Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail)
  {
    PXCache cach = rowCache.Graph.Caches[this._TaxType];
    PX.Objects.CM.CurrencyInfo dacCurrencyInfo = this.GetDacCurrencyInfo(rowCache, row);
    bool flag = dacCurrencyInfo == null || dacCurrencyInfo.CuryID == dacCurrencyInfo.BaseCuryID;
    string taxCalcLevel = tax.TaxCalcLevel;
    if (taxCalcLevel == "1" || taxCalcLevel == "0")
    {
      Decimal quantityForPerUnitTaxes = this.GetTaxableQuantityForPerUnitTaxes(rowCache, row, tax, taxRevision);
      Decimal curyTaxAmount = !flag ? 0M : this.GetTaxAmountForPerUnitTaxWithCorrectSign(rowCache, row, tax, taxRevision, cach, taxDetail, quantityForPerUnitTaxes).CuryTaxAmount;
      this.FillTaxDetailValuesForPerUnitTax(cach, tax, taxRevision, taxDetail, rowCache, row, quantityForPerUnitTaxes, curyTaxAmount);
    }
    else
    {
      PXTrace.WriteError("The calculation level {0} for the per-unit tax is not supported.");
      throw new PXArgumentException("The calculation level {0} for the per-unit tax is not supported.");
    }
  }

  private Decimal GetTaxableQuantityForPerUnitTaxes(
    PXCache rowCache,
    object row,
    Tax tax,
    TaxRev taxRevison)
  {
    Decimal? nullable1 = this.GetLineQty(rowCache, row);
    string uom = this.GetUOM(rowCache, row);
    if (nullable1.HasValue)
    {
      Decimal? nullable2 = nullable1;
      Decimal num = 0M;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue) && tax.TaxUOM != null)
      {
        nullable1 = this.ConvertLineQtyToTaxUOM(rowCache, row, tax, uom, nullable1.Value);
        return !nullable1.HasValue ? 0M : this.GetAdjustedTaxableQuantity(nullable1.Value, taxRevison);
      }
    }
    return 0M;
  }

  protected virtual Decimal? ConvertLineQtyToTaxUOM(
    PXCache rowCache,
    object row,
    Tax tax,
    string lineUOM,
    Decimal lineQuantity)
  {
    if (tax.TaxUOM == lineUOM)
      return new Decimal?(lineQuantity);
    int? inventory = this.GetInventory(rowCache, row);
    if (!inventory.HasValue && string.IsNullOrEmpty(lineUOM))
    {
      string taxZone = this.GetTaxZone(rowCache, row);
      string taxCategory = this.GetTaxCategory(rowCache, row);
      string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA("The {0} per-unit tax cannot be calculated for the document with the {1} tax zone if inventory item and UOM are not specified for the line with the {2} tax category.", new object[3]
      {
        (object) tax.TaxID,
        (object) taxZone,
        (object) taxCategory
      });
      this.SetPerUnitTaxUomConversionError(rowCache, row, tax, errorMessage);
      return new Decimal?();
    }
    Decimal? taxUom = new Decimal?();
    if (inventory.HasValue)
    {
      try
      {
        Decimal quantityInBaseUom = this.GetNotRoundedLineQuantityInBaseUOM(rowCache, inventory, lineUOM, lineQuantity);
        taxUom = new Decimal?(INUnitAttribute.ConvertFromBase(rowCache, inventory, tax.TaxUOM, quantityInBaseUom, INPrecision.QUANTITY));
      }
      catch (PXUnitConversionException ex)
      {
        taxUom = new Decimal?();
      }
    }
    if (!taxUom.HasValue)
    {
      taxUom = this.ConvertLineQtyToTaxUOMWithGlobalConversions(rowCache.Graph, lineUOM, tax.TaxUOM, lineQuantity);
      if (!taxUom.HasValue)
      {
        string taxZone = this.GetTaxZone(rowCache, row);
        string taxCategory = this.GetTaxCategory(rowCache, row);
        string errorMessage = PXMessages.LocalizeFormatNoPrefixNLA("The {0} per-unit tax cannot be calculated for the document with the {1} tax zone and the line with the {2} tax category. Conversion rule to the {3} tax UOM is missing.", new object[4]
        {
          (object) tax.TaxID,
          (object) taxZone,
          (object) taxCategory,
          (object) tax.TaxUOM
        });
        this.SetPerUnitTaxUomConversionError(rowCache, row, tax, errorMessage);
      }
    }
    return taxUom;
  }

  protected virtual Decimal GetNotRoundedLineQuantityInBaseUOM(
    PXCache rowCache,
    int? lineInventoryID,
    string lineUOM,
    Decimal lineQuantityInLineUOM)
  {
    return INUnitAttribute.ConvertToBase(rowCache, lineInventoryID, lineUOM, lineQuantityInLineUOM, INPrecision.NOROUND);
  }

  protected Decimal? ConvertLineQtyToTaxUOMWithGlobalConversions(
    PXGraph graph,
    string lineUOM,
    string taxUOM,
    Decimal lineQuantity)
  {
    Decimal result;
    return INUnitAttribute.TryConvertGlobalUnits(graph, lineUOM, taxUOM, lineQuantity, INPrecision.QUANTITY, out result) ? new Decimal?(result) : new Decimal?();
  }

  protected virtual void SetPerUnitTaxUomConversionError(
    PXCache rowCache,
    object row,
    Tax tax,
    string errorMessage)
  {
    PXException pxException = (PXException) new PXSetPropertyException(errorMessage, (PXErrorLevel) 4);
    if (!(this.UOM != (Type) null))
      throw pxException;
    string uom = this.GetUOM(rowCache, row);
    rowCache.RaiseExceptionHandling(this.UOM.Name, row, (object) uom, (Exception) pxException);
  }

  private Decimal GetAdjustedTaxableQuantity(Decimal lineQty, TaxRev taxRevison)
  {
    return !taxRevison.TaxableMaxQty.HasValue || lineQty <= taxRevison.TaxableMaxQty.Value ? lineQty : taxRevison.TaxableMaxQty.Value;
  }

  protected virtual (Decimal TaxAmount, Decimal CuryTaxAmount) GetTaxAmountForPerUnitTaxWithCorrectSign(
    PXCache rowCache,
    object row,
    Tax tax,
    TaxRev taxRevison,
    PXCache taxDetailCache,
    TaxDetail taxDetail,
    Decimal taxableQty)
  {
    (Decimal TaxAmount, Decimal CuryTaxAmount) = this.GetTaxAmountForPerUnitTax(taxDetailCache, taxRevison, taxDetail, taxableQty);
    if (TaxAmount == 0M && CuryTaxAmount == 0M)
      return (TaxAmount, CuryTaxAmount);
    return this.InvertPerUnitTaxAmountSign(rowCache, row, tax, taxRevison, taxDetailCache, taxDetail) ? (-TaxAmount, -CuryTaxAmount) : (TaxAmount, CuryTaxAmount);
  }

  protected virtual bool InvertPerUnitTaxAmountSign(
    PXCache rowCache,
    object row,
    Tax tax,
    TaxRev taxRevison,
    PXCache taxDetailCache,
    TaxDetail taxDetail)
  {
    return false;
  }

  protected virtual (Decimal TaxAmount, Decimal CuryTaxAmount) GetTaxAmountForPerUnitTax(
    PXCache taxDetailCache,
    TaxRev taxRevison,
    TaxDetail taxDetail,
    Decimal taxableQty)
  {
    Decimal rateForPerUnitTaxes = this.GetTaxRateForPerUnitTaxes(taxRevison);
    Decimal baseval = taxableQty * rateForPerUnitTaxes;
    Decimal curyval;
    PXDBCurrencyAttribute.CuryConvCury(taxDetailCache, (object) taxDetail, baseval, out curyval);
    Decimal num = PXDBCurrencyAttribute.RoundCury(taxDetailCache, (object) taxDetail, curyval, this.Precision);
    return (baseval, num);
  }

  protected virtual Decimal GetTaxRateForPerUnitTaxes(TaxRev taxRevison)
  {
    Decimal? taxRate = taxRevison.TaxRate;
    Decimal num = 0M;
    return !(taxRate.GetValueOrDefault() > num & taxRate.HasValue) ? 0M : taxRevison.TaxRate.Value;
  }

  protected virtual void FillTaxDetailValuesForPerUnitTax(
    PXCache taxDetailCache,
    Tax tax,
    TaxRev taxRevision,
    TaxDetail taxDetail,
    PXCache rowCache,
    object row,
    Decimal taxableQty,
    Decimal curyTaxAmount)
  {
    this.SetTaxUomForTaxDetail(taxDetailCache, taxDetail, tax.TaxUOM);
    this.SetTaxableQuantityForTaxDetail(taxDetailCache, taxDetail, new Decimal?(taxableQty));
    taxDetail.TaxRate = taxRevision.TaxRate;
    taxDetail.NonDeductibleTaxRate = taxRevision.NonDeductibleTaxRate;
    bool? nullable;
    switch (tax.TaxCalcLevel)
    {
      case "0":
        this.FillLineTaxableAndTaxAmountsForInclusivePerUnitTax(taxDetailCache, taxDetail, rowCache, row, tax);
        break;
      case "1":
        nullable = tax.TaxCalcLevel2Exclude;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          this.CheckThatExclusivePerUnitTaxIsNotUsedWithInclusiveNonPerUnitTax(rowCache, row);
          break;
        }
        break;
    }
    if (!this.IsExemptTaxCategory(rowCache, row))
      this.SetTaxDetailTaxAmount(taxDetailCache, taxDetail, new Decimal?(curyTaxAmount));
    this.FillDiscountAmountsForPerUnitTax(taxDetailCache, taxDetail);
    if (taxRevision.TaxID != null)
    {
      nullable = tax.DirectTax;
      if (!nullable.GetValueOrDefault())
      {
        taxDetailCache.Update((object) taxDetail);
        return;
      }
    }
    this.Delete(taxDetailCache, (object) taxDetail);
  }

  private void FillDiscountAmountsForPerUnitTax(PXCache taxDetailCache, TaxDetail taxDetail)
  {
    TaxBaseAttribute.SetValueOptional(taxDetailCache, (object) taxDetail, (object) 0M, this._CuryTaxableDiscountAmt);
    TaxBaseAttribute.SetValueOptional(taxDetailCache, (object) taxDetail, (object) 0M, this._CuryTaxDiscountAmt);
  }

  private void CheckThatExclusivePerUnitTaxIsNotUsedWithInclusiveNonPerUnitTax(
    PXCache rowCache,
    object row)
  {
    if (this.SelectInclusiveTaxes(rowCache.Graph, row).Select<object, Tax>((Func<object, Tax>) (taxRow => PXResult.Unwrap<Tax>(taxRow))).Any<Tax>((Func<Tax, bool>) (inclusiveTax => inclusiveTax != null && !this.IsPerUnitTax(inclusiveTax))))
    {
      PXTrace.WriteInformation("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.");
      throw new PXSetPropertyException("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.", (PXErrorLevel) 4);
    }
  }

  private void FillLineTaxableAndTaxAmountsForInclusivePerUnitTax(
    PXCache taxDetailCache,
    TaxDetail taxDetail,
    PXCache rowCache,
    object row,
    Tax tax)
  {
    Decimal valueOrDefault = this.GetCuryTranAmt(rowCache, row, tax.TaxCalcType).GetValueOrDefault();
    Decimal taxesTotalAmount = this.GetInclusivePerUnitTaxesTotalAmount(taxDetailCache, taxDetail, rowCache, row);
    Decimal num1 = taxesTotalAmount;
    Decimal num2 = valueOrDefault - num1;
    this.SetTaxableAmt(rowCache, row, new Decimal?(num2));
    this.SetTaxAmt(rowCache, row, new Decimal?(taxesTotalAmount));
  }

  private Decimal GetInclusivePerUnitTaxesTotalAmount(
    PXCache taxDetailCache,
    TaxDetail taxDetail,
    PXCache rowCache,
    object row)
  {
    if (taxDetailCache.GetBqlField(this._CuryTaxAmt) == (Type) null)
      return 0M;
    IEnumerable<object> inclusivePerUnitTaxRows = this.GetInclusivePerUnitTaxRows(rowCache, row);
    List<object> list = inclusivePerUnitTaxRows != null ? inclusivePerUnitTaxRows.ToList<object>() : (List<object>) null;
    if (list == null || list.Count == 0)
      return 0M;
    Decimal taxesTotalAmount = 0M;
    foreach (PXResult pxResult in list)
    {
      TaxRev taxRevison = pxResult.GetItem<TaxRev>();
      Tax tax = pxResult.GetItem<Tax>();
      Decimal quantityForPerUnitTaxes = this.GetTaxableQuantityForPerUnitTaxes(rowCache, row, tax, taxRevison);
      Decimal? taxRate = taxRevison.TaxRate;
      Decimal num = this.GetTaxAmountForPerUnitTaxWithCorrectSign(rowCache, row, tax, taxRevison, taxDetailCache, taxDetail, quantityForPerUnitTaxes).CuryTaxAmount;
      if (tax.ReverseTax.GetValueOrDefault())
        num = -num;
      taxesTotalAmount += num;
    }
    return taxesTotalAmount;
  }

  private IEnumerable<object> GetInclusivePerUnitTaxRows(PXCache rowCache, object row)
  {
    List<object> objectList = this.SelectInclusiveTaxes(rowCache.Graph, row);
    if (objectList != null && objectList.Count != 0)
    {
      foreach (PXResult inclusivePerUnitTaxRow in objectList)
      {
        Tax tax = inclusivePerUnitTaxRow.GetItem<Tax>();
        if (tax != null && this.IsPerUnitTax(tax))
          yield return (object) inclusivePerUnitTaxRow;
      }
    }
  }

  protected virtual Decimal GetPerUnitTaxAmountForTaxableAdjustmentCalculation(
    Tax taxForTaxableAdustment,
    TaxDetail taxDetail,
    PXCache taxDetailCache,
    object row,
    PXCache rowCache)
  {
    if (taxForTaxableAdustment.TaxType == "Q")
      return 0M;
    PerUnitTaxesAdjustmentToTaxableCalculator adjustmentCalculator = this.GetPerUnitTaxAdjustmentCalculator();
    return adjustmentCalculator == null ? 0M : adjustmentCalculator?.GetPerUnitTaxAmountForTaxableAdjustmentCalculation(taxForTaxableAdustment, taxDetailCache, row, rowCache, this._CuryTaxAmt, (Func<List<object>>) (() => this.SelectPerUnitTaxesForTaxableAdjustmentCalculation(taxDetailCache.Graph, row))).GetValueOrDefault();
  }

  protected virtual PerUnitTaxesAdjustmentToTaxableCalculator GetPerUnitTaxAdjustmentCalculator()
  {
    return PerUnitTaxesAdjustmentToTaxableCalculator.Instance;
  }

  protected virtual List<object> SelectPerUnitTaxes(PXGraph graph, object row)
  {
    return !this.IsExemptTaxCategory(graph, row) ? this.SelectTaxes<Where<Tax.taxType, Equal<CSTaxType.perUnit>>>(graph, row, PXTaxCheck.Line) : new List<object>();
  }

  protected virtual List<object> SelectDocumentPerUnitTaxes(PXGraph graph, object document)
  {
    return this.SelectTaxes<Where<Tax.taxType, Equal<CSTaxType.perUnit>>>(graph, document, PXTaxCheck.RecalcTotals);
  }

  protected virtual List<object> SelectPerUnitTaxesForTaxableAdjustmentCalculation(
    PXGraph graph,
    object row)
  {
    return !this.IsExemptTaxCategory(graph, row) ? this.SelectTaxes<Where<Tax.taxType, Equal<CSTaxType.perUnit>, And<Tax.taxCalcLevel2Exclude, Equal<False>>>>(graph, row, PXTaxCheck.Line) : new List<object>();
  }

  /// <summary>Fill aggregated tax detail for per unit tax.</summary>
  /// <param name="rowCache">The row cache.</param>
  /// <param name="row">The row.</param>
  /// <param name="tax">The tax.</param>
  /// <param name="taxRevision">The tax revision.</param>
  /// <param name="aggrTaxDetail">The aggregated tax detail.</param>
  /// <param name="taxItems">The tax items.</param>
  /// <returns />
  protected virtual TaxDetail FillAggregatedTaxDetailForPerUnitTax(
    PXCache rowCache,
    object row,
    Tax tax,
    TaxRev taxRevision,
    TaxDetail aggrTaxDetail,
    List<object> taxItems)
  {
    PXCache taxDetailCache = rowCache.Graph.Caches[this._TaxType];
    PXCache cach = rowCache.Graph.Caches[this._TaxSumType];
    Decimal num = taxItems.OfType<PXResult>().Select<PXResult, object>((Func<PXResult, object>) (item => item[this._TaxType])).OfType<TaxDetail>().Sum<TaxDetail>((Func<TaxDetail, Decimal>) (taxDetail => this.GetTaxableQuantityForTaxDetail(taxDetailCache, taxDetail).GetValueOrDefault()));
    this.SetTaxableQuantityForTaxDetail(cach, aggrTaxDetail, new Decimal?(num));
    this.SetTaxUomForTaxDetail(cach, aggrTaxDetail, tax.TaxUOM);
    return aggrTaxDetail;
  }

  protected virtual void CheckCurrencyAndRetainageOnDocumentRowSelected(
    PXCache documentCache,
    PXRowSelectedEventArgs e)
  {
    IBqlTable document = e.Row as IBqlTable;
    if (document == null || !PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>())
      return;
    List<object> objectList = this.SelectDocumentPerUnitTaxes(documentCache.Graph, (object) document);
    if (objectList == null || objectList.Count == 0)
      return;
    CheckIfDocumentTaxesAreRetained();
    CheckDocumentCurrency();

    void CheckIfDocumentTaxesAreRetained()
    {
      PXSetPropertyException propertyException = this.IsRetainedTaxes(documentCache.Graph) ? new PXSetPropertyException("Calculation of per-unit taxes is not supported for retained documents.", (PXErrorLevel) 4) : (PXSetPropertyException) null;
      string retainageApplyFieldName = this.RetainageApplyFieldName;
      if (string.IsNullOrWhiteSpace(retainageApplyFieldName))
      {
        if (propertyException != null)
          throw propertyException;
      }
      else
      {
        object obj = documentCache.GetValue((object) document, retainageApplyFieldName);
        documentCache.RaiseExceptionHandling(retainageApplyFieldName, (object) document, obj, (Exception) propertyException);
      }
    }

    void CheckDocumentCurrency()
    {
      IEnumerable<PXEventSubscriberAttribute> attributesReadonly = documentCache.GetAttributesReadonly((object) document, (string) null);
      CurrencyInfoAttribute currencyInfoAttribute = attributesReadonly != null ? attributesReadonly.OfType<CurrencyInfoAttribute>().FirstOrDefault<CurrencyInfoAttribute>() : (CurrencyInfoAttribute) null;
      if (currencyInfoAttribute == null || currencyInfoAttribute.CuryIDField == null)
        return;
      PX.Objects.CM.CurrencyInfo dacCurrencyInfo = this.GetDacCurrencyInfo(documentCache, (object) document);
      PXSetPropertyException propertyException = dacCurrencyInfo == null || !(dacCurrencyInfo.BaseCuryID != dacCurrencyInfo.CuryID) ? (PXSetPropertyException) null : new PXSetPropertyException("Calculation of per-unit taxes is not supported for documents in a non-base currency.", (PXErrorLevel) 4);
      documentCache.RaiseExceptionHandling(currencyInfoAttribute.CuryIDField, (object) document, (object) dacCurrencyInfo.CuryID, (Exception) propertyException);
    }
  }

  protected virtual void DisablePerUnitTaxesOnAggregatedTaxDetailRowSelected(
    PXCache aggrTaxDetCache,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxDetail row) || !PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>() || !this.CheckIfTaxDetailHasPerUnitTaxType(aggrTaxDetCache.Graph, row.TaxID))
      return;
    PXUIFieldAttribute.SetEnabled(aggrTaxDetCache, (object) row, false);
  }

  protected virtual void CheckForPerUnitTaxesOnAggregatedTaxRowDeleting(
    PXCache aggrTaxDetCache,
    PXRowDeletingEventArgs e)
  {
    if (e.Row is TaxDetail row && PXAccess.FeatureInstalled<FeaturesSet.perUnitTaxSupport>() && e.ExternalCall && this.CheckIfTaxDetailHasPerUnitTaxType(aggrTaxDetCache.Graph, row.TaxID))
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("Per-unit taxes cannot be deleted manually.");
    }
  }

  public void DocumentLineCheckPerUnitTaxesOnRowPersisting(
    PXCache rowCache,
    PXRowPersistingEventArgs e)
  {
    object row = e.Row;
    if (row == null)
      return;
    List<object> documentTaxRows = this.SelectTaxes(rowCache, row, PXTaxCheck.Line);
    if (documentTaxRows == null || documentTaxRows.Count == 0)
      return;
    (bool HasInclusiveNonPerUnitTax, bool HasLevel1NonExcludedPerUnitTax, List<Tax> PerUnitTaxes) documentTaxesInfo = GetDocumentTaxesInfo(documentTaxRows);
    int num1 = documentTaxesInfo.HasInclusiveNonPerUnitTax ? 1 : 0;
    bool excludedPerUnitTax = documentTaxesInfo.HasLevel1NonExcludedPerUnitTax;
    List<Tax> perUnitTaxes = documentTaxesInfo.PerUnitTaxes;
    int num2 = excludedPerUnitTax ? 1 : 0;
    if ((num1 & num2) != 0)
    {
      PXTrace.WriteInformation("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.");
      throw new PXSetPropertyException("This combination of inclusive taxes and exclusive per-unit taxes is forbidden. Please review and modify your tax settings on the Taxes (TX205000) form.", (PXErrorLevel) 4);
    }
    CheckPerUnitTaxesForConversionToTaxUomPossibility();

    static (bool HasInclusiveNonPerUnitTax, bool HasLevel1NonExcludedPerUnitTax, List<Tax> PerUnitTaxes) GetDocumentTaxesInfo(
      List<object> documentTaxRows)
    {
      bool flag1 = false;
      bool flag2 = false;
      List<Tax> taxList = new List<Tax>(documentTaxRows.Count);
      foreach (PXResult documentTaxRow in documentTaxRows)
      {
        Tax tax = PXResult.Unwrap<Tax>((object) documentTaxRow);
        if (tax.TaxType != "Q")
        {
          flag1 = flag1 || tax.TaxCalcLevel == "0";
        }
        else
        {
          taxList.Add(tax);
          bool? calcLevel2Exclude = tax.TaxCalcLevel2Exclude;
          bool flag3 = false;
          if (calcLevel2Exclude.GetValueOrDefault() == flag3 & calcLevel2Exclude.HasValue)
            flag2 = flag2 || tax.TaxCalcLevel == "1";
        }
      }
      return (flag1, flag2, taxList);
    }

    void CheckPerUnitTaxesForConversionToTaxUomPossibility()
    {
      Decimal valueOrDefault = this.GetLineQty(rowCache, row).GetValueOrDefault();
      if (valueOrDefault == 0M)
        return;
      string uom = this.GetUOM(rowCache, row);
      foreach (Tax perUnitTax in perUnitTaxes)
      {
        if (!this.ConvertLineQtyToTaxUOM(rowCache, row, perUnitTax, uom, valueOrDefault).HasValue)
        {
          ((CancelEventArgs) e).Cancel = true;
          throw new PXException(PXUIFieldAttribute.GetError(rowCache, row, this.UOM.Name));
        }
      }
    }
  }

  protected virtual void DocumentCheckPerUnitTaxesOnRowPersisting(
    PXCache documentCache,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is IBqlTable row))
      return;
    List<object> objectList = this.SelectDocumentPerUnitTaxes(documentCache.Graph, (object) row);
    // ISSUE: explicit non-virtual call
    if ((objectList != null ? (__nonvirtual (objectList.Count) > 0 ? 1 : 0) : 0) == 0)
      return;
    if (this.IsRetainedTaxes(documentCache.Graph))
    {
      PXTrace.WriteInformation("Calculation of per-unit taxes is not supported for retained documents.");
      throw new PXSetPropertyException("Calculation of per-unit taxes is not supported for retained documents.", (PXErrorLevel) 4);
    }
    PX.Objects.CM.CurrencyInfo dacCurrencyInfo = this.GetDacCurrencyInfo(documentCache, (object) row);
    if (dacCurrencyInfo != null && dacCurrencyInfo.CuryID != dacCurrencyInfo.BaseCuryID)
    {
      PXTrace.WriteInformation("Calculation of per-unit taxes is not supported for documents in a non-base currency.");
      throw new PXSetPropertyException("Calculation of per-unit taxes is not supported for documents in a non-base currency.", (PXErrorLevel) 4);
    }
  }

  private PX.Objects.CM.CurrencyInfo GetDacCurrencyInfo(PXCache dacCache, object dac)
  {
    long? curyInfoIdFromDac = this.GetCuryInfoIDFromDac(dacCache, dac);
    if (!curyInfoIdFromDac.HasValue)
      return (PX.Objects.CM.CurrencyInfo) null;
    return PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.SelectSingleBound(dacCache.Graph, (object[]) null, new object[1]
    {
      (object) curyInfoIdFromDac
    }));
  }

  private long? GetCuryInfoIDFromDac(PXCache dacCache, object dac)
  {
    if (dacCache == null || dac == null)
      return new long?();
    IEnumerable<PXEventSubscriberAttribute> attributesReadonly = dacCache.GetAttributesReadonly(dac, (string) null);
    CurrencyInfoAttribute currencyInfoAttribute = attributesReadonly != null ? attributesReadonly.OfType<CurrencyInfoAttribute>().FirstOrDefault<CurrencyInfoAttribute>() : (CurrencyInfoAttribute) null;
    if (currencyInfoAttribute == null || ((PXEventSubscriberAttribute) currencyInfoAttribute).FieldName == null)
      return new long?();
    int fieldOrdinal = dacCache.GetFieldOrdinal(((PXEventSubscriberAttribute) currencyInfoAttribute).FieldName);
    return fieldOrdinal < 0 ? new long?() : dacCache.GetValue(dac, fieldOrdinal) as long?;
  }

  private bool CheckIfTaxDetailHasPerUnitTaxType(PXGraph graph, string taxID)
  {
    return !string.IsNullOrWhiteSpace(taxID) && this.GetTax(graph, taxID)?.TaxType == "Q";
  }

  private Tax GetTax(PXGraph graph, string taxID)
  {
    return PXResultset<Tax>.op_Implicit(PXSelectBase<Tax, PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) taxID
    }));
  }

  protected virtual bool IsRetainedTaxes(PXGraph graph) => false;

  protected abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.curyTranAmt>
  {
  }

  protected abstract class origGroupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.origGroupDiscountRate>
  {
  }

  protected abstract class origDocumentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.origDocumentDiscountRate>
  {
  }

  protected abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.groupDiscountRate>
  {
  }

  protected abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.documentDiscountRate>
  {
  }

  protected abstract class curyLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.curyLineTotal>
  {
  }

  protected abstract class curyDiscTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxBaseAttribute.curyDiscTot>
  {
  }

  protected class InclusiveTaxGroup
  {
    public string Key { get; set; }

    public Decimal Rate { get; set; }

    public Decimal TotalAmount { get; set; }
  }

  public class ZoneUpdatedArgs
  {
    public PXCache Cache;
    public List<object> Details;
    public string OldValue;
    public string NewValue;
  }

  private enum SummType
  {
    Inclusive,
    All,
  }
}
