// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Objects.Extensions.MultiCurrency;

/// <summary>The generic graph extension that defines the multi-currency functionality.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">A DAC (a <see cref="T:PX.Data.IBqlTable" /> type).</typeparam>
public abstract class MultiCurrencyGraph<TGraph, TPrimary> : 
  PXGraphExtension<TGraph>,
  IPXCurrencyHelper,
  ICurrencyHelperEx,
  ICurrencyHost
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Extensions.MultiCurrency.Document" /> data.</summary>
  public PXSelectExtension<Document> Documents;
  /// <summary>A mapping-based view of the <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" /> data.</summary>
  public PXSelectExtension<PX.Objects.Extensions.MultiCurrency.CurySource> CurySource;
  /// <summary>The current <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo" /> object of the document.</summary>
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo> currencyinfo;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> currencyinfobykey;
  /// <summary>The <strong>Currency Toggle</strong> action.</summary>
  public PXAction<TPrimary> currencyView;
  protected List<PXSelectBase> ChildViews;
  protected Dictionary<System.Type, List<CuryField>> TrackedItems;
  private Document _oldRow;
  protected bool? currencyInfoDirty;

  public PX.Objects.CM.Extensions.CurrencyInfo GetDefaultCurrencyInfo()
  {
    return this.Documents.Current == null && this.currencyinfo.Cache.Inserted.Count() == 1L ? this.currencyinfo.Cache.Inserted.Cast<PX.Objects.CM.Extensions.CurrencyInfo>().Single<PX.Objects.CM.Extensions.CurrencyInfo>() : this.GetCurrencyInfo((long?) this.Documents.Current?.CuryInfoID);
  }

  public virtual bool IsTrackedType(System.Type dacType)
  {
    return ((IEnumerable<PXSelectBase>) this.GetChildren()).Union<PXSelectBase>((IEnumerable<PXSelectBase>) this.GetTrackedExceptChildren()).Any<PXSelectBase>((Func<PXSelectBase, bool>) (s => s.View.CacheGetItemType() == dacType));
  }

  /// <summary>Returns the mapping of the <see cref="T:PX.Objects.Extensions.MultiCurrency.Document" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you  can either return the default mapping or override the default
  /// mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetDocumentMapping() method in the implementation class. The  method overrides the default mapping of the %Document% mapped cache extension to a DAC: For the CROpportunity DAC, the DocumentDate field of the mapped cache extension is mapped to the closeDate field of the DAC; other fields of the extension are mapped by default.</para>
  ///   <code title="Example" lang="CS">
  /// protected override DocumentMapping GetDocumentMapping()
  ///  {
  ///          return new DocumentMapping(typeof(CROpportunity)) {DocumentDate =  typeof(CROpportunity.closeDate)};
  ///  }</code>
  /// </example>
  protected abstract MultiCurrencyGraph<TGraph, TPrimary>.DocumentMapping GetDocumentMapping();

  /// <summary>Returns the mapping of the <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" /> mapped cache extension to a DAC. This method must be overridden in the implementation class of the base graph.</summary>
  /// <remarks>In the implementation graph for a particular graph, you can either return the default mapping or override the default mapping in this method.</remarks>
  /// <example><para>The following code shows the method that overrides the GetCurySourceMapping() method in the implementation class. The method returns the defaul mapping of the %CurySource% mapped cache extension to the Customer DAC.</para>
  ///   <code title="Example" lang="CS">
  /// protected override CurySourceMapping GetCurySourceMapping()
  ///  {
  ///      return new CurySourceMapping(typeof(Customer));
  ///  }</code>
  /// </example>
  protected abstract MultiCurrencyGraph<TGraph, TPrimary>.CurySourceMapping GetCurySourceMapping();

  protected abstract PXSelectBase[] GetChildren();

  protected virtual PXSelectBase[] GetTrackedExceptChildren() => Array.Empty<PXSelectBase>();

  /// <summary>Returns the current currency source.</summary>
  /// <returns>The default implementation returns the <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" /> data view.</returns>
  /// <example><para>The following code shows sample implementation of the method in the implementation class.</para>
  ///   <code title="Example" lang="CS">
  /// public PXSelect&lt;CRSetup&gt; crCurrency;
  /// protected PXSelectExtension&lt;CurySource&gt; SourceSetup =&gt; new PXSelectExtension&lt;CurySource&gt;(crCurrency);
  /// 
  /// protected virtual CurySourceMapping GetSourceSetupMapping()
  /// {
  ///       return new CurySourceMapping(typeof(CRSetup)) {CuryID = typeof(CRSetup.defaultCuryID), CuryRateTypeID = typeof(CRSetup.defaultRateTypeID)};
  ///  }
  /// 
  /// protected override CurySource CurrentSourceSelect()
  /// {
  ///        CurySource settings = base.CurrentSourceSelect();
  ///        if (settings == null)
  ///              return SourceSetup.Select();
  ///        if (settings.CuryID == null || settings.CuryRateTypeID == null)
  ///        {
  ///              CurySource setup = SourceSetup.Select();
  ///              settings = (CurySource)CurySource.Cache.CreateCopy(settings);
  ///              settings.CuryID = settings.CuryID ?? setup.CuryID;
  ///              settings.CuryRateTypeID = settings.CuryRateTypeID ?? setup.CuryRateTypeID;
  ///        }
  ///        return settings;
  /// }</code>
  /// </example>
  protected virtual PX.Objects.Extensions.MultiCurrency.CurySource CurrentSourceSelect()
  {
    return (PX.Objects.Extensions.MultiCurrency.CurySource) this.CurySource.Select();
  }

  protected IEnumerable currencyInfo()
  {
    MultiCurrencyGraph<TGraph, TPrimary> multiCurrencyGraph = this;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = multiCurrencyGraph.GetCurrencyInfo((long?) multiCurrencyGraph.Documents.Current?.CuryInfoID) ?? (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<Document.curyInfoID>>>>.Config>.Select((PXGraph) multiCurrencyGraph.Base);
    if (currencyInfo != null)
    {
      currencyInfo.IsReadOnly = new bool?(multiCurrencyGraph.ShouldMainCurrencyInfoBeReadonly());
      yield return (object) currencyInfo;
    }
  }

  protected virtual bool ShouldMainCurrencyInfoBeReadonly()
  {
    return !this.Base.UnattendedMode && !this.Documents.AllowUpdate;
  }

  [PXUIField(DisplayName = "Toggle Currency", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Money", Tooltip = "Toggle Currency View", DisplayOnMainToolbar = false, CommitChanges = false)]
  protected virtual IEnumerable CurrencyView(PXAdapter adapter)
  {
    MultiCurrencyGraph<TGraph, TPrimary> multiCurrencyGraph = this;
    multiCurrencyGraph.Base.Accessinfo.CuryViewState = !multiCurrencyGraph.Base.Accessinfo.CuryViewState;
    PXCache cache = adapter.View.Cache;
    bool anyDiff = !cache.IsDirty;
    foreach (object obj in adapter.Get())
    {
      if (!anyDiff)
      {
        TPrimary primary = !(obj is PXResult) ? (TPrimary) obj : (TPrimary) ((PXResult) obj)[0];
        anyDiff = multiCurrencyGraph.IsModified(cache, primary);
      }
      yield return obj;
    }
    if (!anyDiff)
      cache.IsDirty = false;
  }

  /// <summary>
  /// Verifyes if provided item differs from previously saved not-changed _oldRow for purpose should the change be ignored on changing
  /// </summary>
  /// <param name="cache">Cache</param>
  /// <param name="item">&gt;New (changed) row</param>
  /// <returns>True if any field have changed in New row in comparision with Old row, with exception of (by default) CuryViewState and LastModifiedByScreenID</returns>
  protected bool IsModified(PXCache cache, TPrimary item)
  {
    return (object) item == null || !(this._oldRow?.Base is TPrimary oldRow) || this.IsModified(cache, (IBqlTable) item, (IBqlTable) oldRow);
  }

  /// <summary>
  /// Verifies, if the difference between to objects includes values which actually should be processed
  /// </summary>
  /// <param name="cache">Cache</param>
  /// <param name="newRow">New (changed) row</param>
  /// <param name="oldRow">Old (original) row</param>
  /// <returns>True if any field have changed in New row in comparision with Old row, with exception of (by default) CuryViewState and LastModifiedByScreenID</returns>
  public virtual bool IsModified(PXCache cache, IBqlTable newRow, IBqlTable oldRow)
  {
    foreach (string fieldName in cache.Fields.Where<string>(new Func<string, bool>(PX.Objects.CM.Extensions.CurrencyInfoAttribute.IsDifferenceEssential)))
    {
      object objA = cache.GetValue((object) oldRow, fieldName);
      object objB = cache.GetValue((object) newRow, fieldName);
      if ((objA != null || objB != null) && !object.Equals(objA, objB))
      {
        if (objA is System.DateTime && objB is System.DateTime dateTime1)
        {
          System.DateTime dateTime = (System.DateTime) objA;
          System.DateTime date1 = dateTime.Date;
          dateTime = dateTime1;
          System.DateTime date2 = dateTime.Date;
          if (!(date1 != date2))
            continue;
        }
        return true;
      }
    }
    return false;
  }

  public override void Initialize()
  {
    base.Initialize();
    this.ChildViews = new List<PXSelectBase>();
    this.TrackedItems = new Dictionary<System.Type, List<CuryField>>();
    Dictionary<System.Type, string> topCuryInfoIDs = new Dictionary<System.Type, string>();
    foreach (PXSelectBase child in this.GetChildren())
    {
      this.ChildViews.Add(child);
      System.Type itemType = child.View.CacheGetItemType();
      if (!this.TrackedItems.ContainsKey(itemType))
      {
        List<CuryField> curyFieldList = new List<CuryField>();
        foreach (PXEventSubscriberAttribute subscriberAttribute in child.Cache.GetAttributesReadonly((string) null))
        {
          if (!(subscriberAttribute is ICurrencyAttribute PXCurrencyAttr))
          {
            if (subscriberAttribute is PX.Objects.CM.Extensions.CurrencyInfoAttribute currencyInfoAttribute && currencyInfoAttribute.IsTopLevel)
              topCuryInfoIDs[itemType] = subscriberAttribute.FieldName;
          }
          else
            curyFieldList.Add(new CuryField(PXCurrencyAttr));
        }
        this.TrackedItems.Add(itemType, curyFieldList);
      }
    }
    foreach (PXSelectBase trackedExceptChild in this.GetTrackedExceptChildren())
    {
      System.Type itemType = trackedExceptChild.View.CacheGetItemType();
      if (!this.TrackedItems.ContainsKey(itemType))
      {
        List<CuryField> list = trackedExceptChild.Cache.GetAttributesReadonly((string) null).OfType<ICurrencyAttribute>().Select<ICurrencyAttribute, CuryField>((Func<ICurrencyAttribute, CuryField>) (attr => new CuryField(attr))).ToList<CuryField>();
        this.TrackedItems.Add(itemType, list);
      }
    }
    foreach (KeyValuePair<System.Type, List<CuryField>> trackedItem in this.TrackedItems)
    {
      KeyValuePair<System.Type, List<CuryField>> table = trackedItem;
      this.Base.RowInserting.AddHandler(table.Key, (PXRowInserting) ((s, e) => this.CuryRowInserting(s, e, table.Value, topCuryInfoIDs)));
      this.Base.RowInserted.AddHandler(table.Key, (PXRowInserted) ((s, e) => this.CuryRowInserted(s, e, table.Value)));
      this.Base.RowPersisting.AddHandler(table.Key, (PXRowPersisting) ((s, e) => this.CuryRowPersisting(s, e, table.Value)));
      foreach (CuryField curyField in table.Value)
      {
        CuryField field = curyField;
        this.Base.FieldUpdating.AddHandler(table.Key, field.BaseName, (PXFieldUpdating) ((s, e) => this.BaseFieldUpdating(s, e, field)));
        this.Base.FieldUpdating.AddHandler(table.Key, field.CuryName, (PXFieldUpdating) ((s, e) => this.CuryFieldUpdating(s, e, field)));
        this.Base.FieldVerifying.AddHandler(table.Key, field.CuryName, (PXFieldVerifying) ((s, e) => this.CuryFieldVerifying(s, e, field)));
      }
    }
    if (this.Base.Views.Caches.Count == 0 || this.Base.Views.Caches[0] != typeof (PX.Objects.CM.Extensions.CurrencyInfo))
    {
      int index = this.Base.Views.Caches.IndexOf(typeof (PX.Objects.CM.Extensions.CurrencyInfo));
      if (index > 0)
        this.Base.Views.Caches.RemoveAt(index);
      this.Base.Views.Caches.Insert(0, typeof (PX.Objects.CM.Extensions.CurrencyInfo));
    }
    this.currencyView.SetCommitChanges(true);
  }

  public void StoreResult(PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (info == null)
      return;
    long? curyInfoId = info.CuryInfoID;
    long num = 0;
    if (!(curyInfoId.GetValueOrDefault() > num & curyInfoId.HasValue))
      return;
    this.currencyinfobykey.StoreResult((IBqlTable) info);
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo(long? key)
  {
    if (!key.HasValue)
      return (PX.Objects.CM.Extensions.CurrencyInfo) null;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo;
    if (!object.Equals((object) (long?) this.currencyinfo.Current?.CuryInfoID, (object) key.Value))
      currencyInfo = this.currencyinfo.Locate(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        CuryInfoID = new long?(key.Value)
      }) ?? this.ReadCurrencyInfo(key.Value);
    else
      currencyInfo = this.currencyinfo.Current;
    PX.Objects.CM.Extensions.CurrencyInfo info = currencyInfo;
    if (info == null)
      return (PX.Objects.CM.Extensions.CurrencyInfo) null;
    if (!info.BasePrecision.HasValue || !info.CuryPrecision.HasValue)
      ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).PopulatePrecision(this.currencyinfo.Cache, info);
    return info;
  }

  private PX.Objects.CM.Extensions.CurrencyInfo ReadCurrencyInfo(long key)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.currencyinfobykey.SelectSingle((object) key);
    if (key > 0L && PXTransactionScope.IsConnectionOutOfScope)
      this.currencyinfobykey.View.RemoveCached(new PXCommandKey(new object[1]
      {
        (object) key
      }, true));
    return currencyInfo;
  }

  private static bool FormatValue(PXFieldUpdatingEventArgs e, CultureInfo culture)
  {
    if (e.NewValue is string)
    {
      Decimal result;
      e.NewValue = !Decimal.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) culture, out result) ? (object) null : (object) result;
    }
    return e.NewValue != null;
  }

  protected virtual void recalculateRowBaseValues(
    PXCache sender,
    object row,
    IEnumerable<CuryField> fields)
  {
    foreach (CuryField field in fields)
    {
      Decimal? curyValue = (Decimal?) sender.GetValue(row, field.CuryName);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo(sender, row, field.CuryInfoIDName);
      field.RecalculateFieldBaseValue(sender, row, (object) curyValue, currencyInfo);
    }
  }

  protected virtual void CuryRowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e,
    List<CuryField> fields,
    Dictionary<System.Type, string> topCuryInfoIDs)
  {
    string fieldName;
    if (!(sender.GetItemType() != this.GetDocumentMapping().Table) || !topCuryInfoIDs.TryGetValue(sender.GetItemType(), out fieldName))
      return;
    PX.Objects.CM.Extensions.CurrencyInfo info = this.currencyinfo.Insert(new PX.Objects.CM.Extensions.CurrencyInfo());
    this.currencyinfo.Cache.IsDirty = false;
    if (info == null)
      return;
    long? id = (long?) sender.GetValue(e.Row, fieldName);
    long? nullable = id;
    long num = 0;
    PX.Objects.CM.Extensions.CurrencyInfo originalCurrencyInfo = nullable.GetValueOrDefault() > num & nullable.HasValue ? this.GetOriginalCurrencyInfo(id) : (PX.Objects.CM.Extensions.CurrencyInfo) null;
    sender.SetValue(e.Row, fieldName, (object) info.CuryInfoID);
    if (originalCurrencyInfo == null)
    {
      this.defaultCurrencyRate(this.currencyinfo.Cache, info, true, true);
    }
    else
    {
      long? curyInfoId = info.CuryInfoID;
      this.currencyinfo.Cache.RestoreCopy((object) info, (object) originalCurrencyInfo);
      info.CuryInfoID = curyInfoId;
      this.currencyinfo.Cache.Remove((object) originalCurrencyInfo);
    }
    sender.SetValue(e.Row, "curyID", (object) info.CuryID);
  }

  private PX.Objects.CM.Extensions.CurrencyInfo GetOriginalCurrencyInfo(long? id)
  {
    if (!id.HasValue)
      return (PX.Objects.CM.Extensions.CurrencyInfo) null;
    PX.Objects.CM.Extensions.CurrencyInfo data = (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfobykey.Select((object) id);
    return data == null ? (PX.Objects.CM.Extensions.CurrencyInfo) null : (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Cache.GetOriginal((object) data);
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo CloneCurrencyInfo(PX.Objects.CM.Extensions.CurrencyInfo currencyInfo)
  {
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
    copy.CuryInfoID = new long?();
    return (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Cache.Insert((object) copy);
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo CloneCurrencyInfo(
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    System.DateTime? currencyEffectiveDate)
  {
    return (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Cache.Insert((object) new PX.Objects.CM.Extensions.CurrencyInfo()
    {
      ModuleCode = currencyInfo.ModuleCode,
      CuryRateTypeID = currencyInfo.CuryRateTypeID,
      CuryID = currencyInfo.CuryID,
      BaseCuryID = currencyInfo.BaseCuryID,
      CuryEffDate = currencyEffectiveDate
    });
  }

  protected virtual void CuryRowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e,
    List<CuryField> fields)
  {
    this.recalculateRowBaseValues(sender, e.Row, (IEnumerable<CuryField>) fields);
  }

  protected virtual void CuryRowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e,
    List<CuryField> fields)
  {
    this.recalculateRowBaseValues(sender, e.Row, (IEnumerable<CuryField>) fields);
  }

  protected virtual void CuryFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    CuryField curyField)
  {
    if (!MultiCurrencyGraph<TGraph, TPrimary>.FormatValue(e, sender.Graph.Culture))
      return;
    Decimal newValue = (Decimal) e.NewValue;
    if (!(newValue != 0.0M))
      return;
    e.NewValue = (object) System.Math.Round(newValue, curyField.CustomPrecision ?? (int) ((short?) this.GetCurrencyInfo(sender, e.Row, curyField.CuryInfoIDName)?.CuryPrecision ?? (short) 2), MidpointRounding.AwayFromZero);
  }

  protected virtual void BaseFieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e,
    CuryField curyField)
  {
    if (!MultiCurrencyGraph<TGraph, TPrimary>.FormatValue(e, sender.Graph.Culture) || !((Decimal) e.NewValue != 0.0M))
      return;
    e.NewValue = (object) System.Math.Round((Decimal) e.NewValue, curyField.CustomPrecision ?? (int) ((short?) this.GetCurrencyInfo(sender, e.Row, curyField.CuryInfoIDName)?.BasePrecision ?? (short) 2), MidpointRounding.AwayFromZero);
  }

  protected virtual void CuryFieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    CuryField curyField)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo(sender, e.Row, curyField.CuryInfoIDName);
    curyField.RecalculateFieldBaseValue(sender, e.Row, e.NewValue, currencyInfo);
  }

  /// <summary>
  /// Try to obtain CurrencyInfo. This overload searches for CurrencyInfo which was already persisted
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="row"></param>
  /// <param name="curyInfoIDField"></param>
  /// <returns></returns>
  public PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo(
    PXCache sender,
    object row,
    string curyInfoIDField)
  {
    long? currencyInfoId = this.GetCurrencyInfoID(sender, row, curyInfoIDField);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo(currencyInfoId);
    if (currencyInfo == null && currencyInfoId.HasValue)
    {
      if (currencyInfoId.Value < 0L)
      {
        long? persistedCuryInfoId = PX.Objects.CM.Extensions.CurrencyInfoAttribute.GetPersistedCuryInfoID(sender, currencyInfoId);
        if (persistedCuryInfoId.HasValue && persistedCuryInfoId.Value > 0L)
          currencyInfo = this.GetCurrencyInfo(persistedCuryInfoId);
      }
      else if (row.GetType() != typeof (TPrimary))
        currencyInfo = this.GetDefaultCurrencyInfo();
    }
    return currencyInfo;
  }

  private long? GetCurrencyInfoID(PXCache sender, object row, string curyInfoIDField)
  {
    long? currencyInfoId = sender.GetValue(row, curyInfoIDField) as long?;
    if (currencyInfoId.HasValue)
      return currencyInfoId;
    return this.Documents.Current?.CuryInfoID;
  }

  /// <summary>The FieldUpdated2 event handler for the <see cref="P:PX.Objects.Extensions.MultiCurrency.Document.BAccountID" /> field. When the BAccountID field value is changed, <see cref="P:PX.Objects.Extensions.MultiCurrency.Document.CuryID" /> is assigned the default
  /// value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    Events.FieldUpdated<Document, Document.bAccountID> e)
  {
    if (!e.ExternalCall && e.Row?.CuryID != null)
      return;
    this.SourceFieldUpdated<Document.curyInfoID, Document.curyID, Document.documentDate>(e.Cache, (IBqlTable) e.Row);
  }

  protected virtual void _(Events.FieldUpdated<Document, Document.branchID> e)
  {
    bool resetCuryID = e.ExternalCall || e.Row?.CuryID == null;
    this.SourceFieldUpdated<Document.curyInfoID, Document.curyID, Document.documentDate>(e.Cache, (IBqlTable) e.Row, resetCuryID);
  }

  protected virtual void SourceFieldUpdated<CuryInfoID, CuryID, DocumentDate>(
    PXCache sender,
    IBqlTable row)
    where CuryInfoID : class, IBqlField
    where CuryID : class, IBqlField
    where DocumentDate : class, IBqlField
  {
    this.SourceFieldUpdated<CuryInfoID, CuryID, DocumentDate>(sender, row, true);
  }

  protected virtual void SourceFieldUpdated<CuryInfoID, CuryID, DocumentDate>(
    PXCache sender,
    IBqlTable row,
    bool resetCuryID)
    where CuryInfoID : class, IBqlField
    where CuryID : class, IBqlField
    where DocumentDate : class, IBqlField
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>() || row == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) sender.GetValue<CuryInfoID>((object) row));
    if (currencyInfo != null)
    {
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
      if (resetCuryID)
      {
        this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) currencyInfo);
      }
      else
      {
        object newValue;
        this.currencyinfo.Cache.RaiseFieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>((object) currencyInfo, out newValue);
        if (object.Equals((object) currencyInfo.BaseCuryID, newValue))
          return;
      }
      this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>((object) currencyInfo);
      if (currencyInfo.ModuleCode == null)
        this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.moduleCode>((object) currencyInfo);
      this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) currencyInfo);
      this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) currencyInfo);
      long? curyInfoId = currencyInfo.CuryInfoID;
      long num = 0;
      if (curyInfoId.GetValueOrDefault() > num & curyInfoId.HasValue && (CurrencyCollection.IsBaseCuryInfo(copy1) || CurrencyCollection.IsBaseCuryInfo(currencyInfo)))
      {
        PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
        copy2.CuryInfoID = new long?();
        currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) this.currencyinfo.Cache.Insert((object) copy2);
        sender.SetValueExt<CuryInfoID>((object) row, (object) currencyInfo.CuryInfoID);
        if (copy1.CuryID == copy1.BaseCuryID)
          this.currencyinfo.Cache.Remove((object) copy1);
        else
          this.currencyinfo.Cache.SetStatus((object) copy1, PXEntryStatus.Deleted);
      }
      else
        this.currencyinfo.Cache.MarkUpdated((object) currencyInfo);
      this.currencyinfo.Cache.RaiseRowUpdated((object) currencyInfo, (object) copy1);
    }
    if (currencyInfo != null)
      sender.SetValue<CuryID>((object) row, (object) currencyInfo.CuryID);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(this.Base.Caches[typeof (PX.Objects.CM.Extensions.CurrencyInfo)], (object) currencyInfo);
    if (string.IsNullOrEmpty(error))
      return;
    sender.RaiseExceptionHandling<DocumentDate>((object) row, sender.GetValue<DocumentDate>((object) row), (Exception) new PXSetPropertyException(error, PXErrorLevel.Warning));
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.Extensions.MultiCurrency.Document.DocumentDate" /> field. When the DocumentDate field value is changed, <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate" /> is changed to DocumentDate value.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    Events.FieldUpdated<Document, Document.documentDate> e)
  {
    this.DateFieldUpdated<Document.curyInfoID, Document.documentDate>(e.Cache, (IBqlTable) e.Row);
  }

  protected virtual void DateFieldUpdated<CuryInfoID, DocumentDate>(PXCache sender, IBqlTable row)
    where CuryInfoID : class, IBqlField
    where DocumentDate : class, IBqlField
  {
    if (row == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) sender.GetValue<CuryInfoID>((object) row));
    if (currencyInfo == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
    this.currencyinfo.SetValueExt<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(currencyInfo, sender.GetValue<DocumentDate>((object) row));
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(this.currencyinfo.Cache, (object) currencyInfo);
    if (!string.IsNullOrEmpty(error))
      sender.RaiseExceptionHandling<DocumentDate>((object) row, (object) null, (Exception) new PXSetPropertyException(error, PXErrorLevel.Warning));
    this.currencyinfo.Cache.RaiseRowUpdated((object) currencyInfo, (object) copy);
    if (this.currencyinfo.Cache.GetStatus((object) currencyInfo) == PXEntryStatus.Inserted)
      return;
    this.currencyinfo.Cache.SetStatus((object) currencyInfo, PXEntryStatus.Updated);
  }

  protected virtual void _(
    Events.FieldSelecting<Document, Document.curyViewState> e)
  {
    e.ReturnValue = (object) this.Base.Accessinfo.CuryViewState;
    Events.FieldSelecting<Document, Document.curyViewState> fieldSelecting = e;
    object returnState = e.ReturnState;
    System.Type dataType = typeof (bool);
    bool? isKey = new bool?(false);
    bool? nullable1 = new bool?(false);
    int? required = new int?(-1);
    bool? nullable2 = new bool?(false);
    bool? nullable3 = new bool?(false);
    bool? nullable4 = new bool?(true);
    int? precision = new int?();
    int? length = new int?();
    bool? enabled = nullable2;
    bool? visible = nullable3;
    bool? readOnly = nullable4;
    PXFieldState instance = PXFieldState.CreateInstance(returnState, dataType, isKey, nullable1, required, precision, length, fieldName: "CuryViewState", enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Visible);
    fieldSelecting.ReturnState = (object) instance;
  }

  protected virtual void _(Events.FieldSelecting<Document, Document.curyID> e)
  {
    e.ReturnValue = (object) this.CuryIDFieldSelecting<Document.curyInfoID>(e.Cache, (object) e.Row) ?? e.ReturnValue;
  }

  protected virtual string CuryIDFieldSelecting<CuryInfoID>(PXCache sender, object row) where CuryInfoID : class, IBqlField
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) sender.GetValue<CuryInfoID>(row));
    if (currencyInfo == null)
      return (string) null;
    return !this.Base.Accessinfo.CuryViewState ? currencyInfo.CuryID : currencyInfo.BaseCuryID;
  }

  protected virtual void _(Events.FieldVerifying<Document, Document.curyID> e)
  {
    if (this.Base.Accessinfo.CuryViewState)
    {
      e.NewValue = (object) e.Row?.CuryID;
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.GetCurrencyInfo((long?) e.Row?.CuryInfoID);
      if (currencyInfo1 == null || object.Equals((object) currencyInfo1.CuryID, e.NewValue))
        return;
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
      long? curyInfoId = copy1.CuryInfoID;
      long num = 0;
      if (curyInfoId.GetValueOrDefault() > num & curyInfoId.HasValue && (CurrencyCollection.IsBaseCuryInfo(copy1) || CurrencyCollection.IsBaseCuryInfo(currencyInfo1, (string) e.NewValue)))
      {
        PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
        this.DefaultRateType(copy2);
        copy2.CuryInfoID = new long?();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.currencyinfo.Cache.Insert((object) copy2) as PX.Objects.CM.Extensions.CurrencyInfo;
        e.Cache.SetValueExt<Document.curyInfoID>((object) e.Row, (object) currencyInfo2.CuryInfoID);
        if (copy1.CuryID == copy1.BaseCuryID)
          this.currencyinfo.Cache.Remove((object) copy1);
        else
          this.currencyinfo.Cache.SetStatus((object) copy1, PXEntryStatus.Deleted);
        this.ValidateCurrencyInfo(currencyInfo2, e);
        this.currencyinfo.Cache.RaiseRowUpdated((object) currencyInfo2, (object) copy1);
      }
      else
      {
        this.DefaultRateType(currencyInfo1);
        this.ValidateCurrencyInfo(currencyInfo1, e);
        this.currencyinfo.Cache.MarkUpdated((object) currencyInfo1);
        this.currencyinfo.Cache.RaiseRowUpdated((object) currencyInfo1, (object) copy1);
      }
    }
  }

  private void DefaultRateType(PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    if (info.CuryRateTypeID != null)
      return;
    this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) info);
  }

  private void ValidateCurrencyInfo(
    PX.Objects.CM.Extensions.CurrencyInfo info,
    Events.FieldVerifying<Document, Document.curyID> e)
  {
    this.currencyinfo.SetValueExt<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(info, e.ExternalCall ? (object) new PXCache.ExternalCallMarker(e.NewValue) : e.NewValue);
    this.currencyinfo.Cache.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) info);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(this.currencyinfo.Cache, (object) info);
    if (string.IsNullOrEmpty(error))
      return;
    e.Cache.RaiseExceptionHandling<Document.curyID>((object) e.Row, e.NewValue, (Exception) new PXSetPropertyException(error, PXErrorLevel.Warning));
  }

  protected virtual void _(
    Events.FieldSelecting<Document, Document.curyRate> e)
  {
    e.ReturnValue = (object) this.CuryRateFieldSelecting<Document.curyInfoID>(e.Cache, (object) e.Row);
  }

  protected virtual Decimal? CuryRateFieldSelecting<CuryInfoID>(PXCache sender, object row) where CuryInfoID : class, IBqlField
  {
    Decimal? nullable = new Decimal?();
    bool curyViewState = this.Base.Accessinfo.CuryViewState;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) sender.GetValue<CuryInfoID>(row));
    if (currencyInfo != null)
      nullable = curyViewState ? new Decimal?(1M) : currencyInfo.SampleCuryRate;
    return nullable;
  }

  /// <summary>The RowSelected event handler for the <see cref="T:PX.Objects.Extensions.MultiCurrency.Document" /> DAC. The handler sets the value of the Enabled property of <see cref="P:PX.Objects.Extensions.MultiCurrency.Document.CuryID" /> according to the value of this property of <see cref="P:PX.Objects.Extensions.MultiCurrency.CurySource.AllowOverrideCury" />.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(Events.RowSelected<Document> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<Document.curyID>(e.Cache, (object) e.Row, this.AllowOverrideCury());
  }

  protected virtual bool AllowOverrideCury()
  {
    PX.Objects.Extensions.MultiCurrency.CurySource curySource = this.CurrentSourceSelect();
    return (curySource == null || curySource.AllowOverrideCury.GetValueOrDefault()) && !this.Base.Accessinfo.CuryViewState;
  }

  protected virtual void _(Events.RowUpdating<Document> e)
  {
    if (!this.Base.Caches<TPrimary>().IsDirty)
      this._oldRow = e.Row;
    this.DocumentRowUpdating<Document.curyInfoID, Document.curyID>(e.Cache, (object) e.NewRow);
  }

  protected virtual void DocumentRowUpdating<CuryInfoID, CuryID>(PXCache sender, object row)
    where CuryInfoID : class, IBqlField
    where CuryID : class, IBqlField
  {
    long? objB = (long?) sender.GetValue<CuryInfoID>(row);
    if (objB.HasValue && objB.Value < 0L)
    {
      bool flag = false;
      foreach (PX.Objects.CM.Extensions.CurrencyInfo currencyInfo in this.currencyinfo.Cache.Inserted)
      {
        if (object.Equals((object) currencyInfo.CuryInfoID, (object) objB))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        sender.SetValue<CuryInfoID>(row, (object) null);
        objB = new long?();
      }
    }
    if (objB.HasValue)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo info = this.currencyinfo.Insert(new PX.Objects.CM.Extensions.CurrencyInfo());
    this.currencyinfo.Cache.IsDirty = false;
    if (info == null)
      return;
    sender.SetValue<CuryInfoID>(row, (object) info.CuryInfoID);
    sender.SetValue<CuryID>(row, (object) info.CuryID);
    this.defaultCurrencyRate(this.currencyinfo.Cache, info, true, true);
  }

  protected virtual void _(Events.RowInserting<Document> e)
  {
    this.DocumentRowInserting<Document.curyInfoID, Document.curyID>(e.Cache, (object) e.Row);
  }

  protected virtual void DocumentRowInserting<CuryInfoID, CuryID>(PXCache sender, object row)
    where CuryInfoID : class, IBqlField
    where CuryID : class, IBqlField
  {
    long? id = (long?) sender.GetValue<CuryInfoID>(row);
    long? nullable = id;
    long num = 0;
    if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo info = this.currencyinfo.Insert(new PX.Objects.CM.Extensions.CurrencyInfo());
    this.currencyinfo.Cache.IsDirty = false;
    if (info == null)
      return;
    sender.SetValue<CuryInfoID>(row, (object) info.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo originalCurrencyInfo = this.GetOriginalCurrencyInfo(id);
    if (originalCurrencyInfo == null)
    {
      this.defaultCurrencyRate(this.currencyinfo.Cache, info, true, true);
    }
    else
    {
      long? curyInfoId = info.CuryInfoID;
      this.currencyinfo.Cache.RestoreCopy((object) info, (object) originalCurrencyInfo);
      info.CuryInfoID = curyInfoId;
      this.currencyinfo.Cache.Remove((object) originalCurrencyInfo);
    }
    sender.SetValue<CuryID>(row, (object) info.CuryID);
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryID" /> field. The CuryID field takes the current value of <see cref="P:PX.Objects.Extensions.MultiCurrency.CurySource.CuryID" />.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
    {
      PX.Objects.Extensions.MultiCurrency.CurySource curySource = this.CurrentSourceSelect();
      if (!string.IsNullOrEmpty(curySource?.CuryID))
        e.NewValue = (object) curySource.CuryID;
      else
        e.NewValue = (object) this.GetBaseCurency();
    }
    else
      e.NewValue = (object) this.GetBaseCurency();
    e.Cancel = true;
  }

  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID> e)
  {
    e.NewValue = (object) this.GetBaseCurency();
    e.Cancel = true;
  }

  protected virtual string GetBaseCurency()
  {
    int? branchID = (int?) this.Documents.Current?.BranchID;
    if (!branchID.HasValue)
    {
      PXCache cach = this.Base.Caches[typeof (TPrimary)];
      object main = this.Documents.Cache.GetMain<Document>(this.Documents.Current);
      string name = this.GetDocumentMapping().BranchID.Name;
      object row = main;
      object obj;
      ref object local = ref obj;
      cach.RaiseFieldDefaulting(name, row, out local);
      branchID = (int?) obj ?? this.Base.Accessinfo.BranchID;
    }
    return ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).BaseCuryID(branchID);
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryRateTypeID" /> field. The CuryRateTypeID field takes the current value of <see cref="P:PX.Objects.Extensions.MultiCurrency.CurySource.CuryRateTypeID" />.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    PX.Objects.Extensions.MultiCurrency.CurySource curySource = this.CurrentSourceSelect();
    if (!string.IsNullOrEmpty(curySource?.CuryRateTypeID))
    {
      e.NewValue = (object) curySource.CuryRateTypeID;
      e.Cancel = true;
    }
    else
    {
      if (e.Row == null || string.IsNullOrEmpty(e.Row.ModuleCode))
        return;
      e.NewValue = (object) ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).DefaultRateTypeID(e.Row.ModuleCode);
      e.Cancel = true;
    }
  }

  /// <summary>Module, to set in CurrencyInfo</summary>
  protected abstract string Module { get; }

  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.moduleCode> e)
  {
    e.NewValue = (object) this.Module;
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
    this.defaultEffectiveDate(e.Cache, e.Row);
    try
    {
      this.defaultCurrencyRate(e.Cache, e.Row, true, false);
    }
    catch (PXSetPropertyException ex)
    {
      if (!e.ExternalCall)
        return;
      e.Cache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>((object) e.Row, (object) e.Row.CuryRateTypeID, (Exception) ex);
    }
  }

  protected virtual void _(
    Events.FieldVerifying<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.Cancel = true;
  }

  /// <summary>The FieldDefaulting2 event handler for the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryEffDate" /> field. The CuryEffDate field takes the current value of <see cref="P:PX.Objects.Extensions.MultiCurrency.Document.DocumentDate" />.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.NewValue = (object) (this.Documents.Cache.Current == null || !this.Documents.Current.DocumentDate.HasValue ? e.Cache.Graph.Accessinfo.BusinessDate : this.Documents.Current.DocumentDate);
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
  {
    try
    {
      this.defaultCurrencyRate(e.Cache, e.Row, true, false);
    }
    catch (PXSetPropertyException ex)
    {
      e.Cache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) e.Row, (object) e.Row.CuryEffDate, (Exception) ex);
    }
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
  {
    this.defaultEffectiveDate(e.Cache, e.Row);
    try
    {
      this.defaultCurrencyRate(e.Cache, e.Row, true, false);
    }
    catch (PXSetPropertyException ex)
    {
      e.Cache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyID>((object) e.Row, (object) e.Row.CuryID, (Exception) ex);
    }
    e.Row.CuryPrecision = new short?();
  }

  protected virtual void _(Events.RowUpdating<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row.IsReadOnly.GetValueOrDefault())
      e.Cancel = true;
    else
      this.currencyInfoDirty = new bool?(e.Cache.IsDirty);
  }

  protected virtual void _(Events.RowPersisting<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row == null)
      return;
    this.ValidateCurrencyRate(e.Row);
  }

  public virtual void ValidateCurrencyRate(PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    PXCache cach = this.Base.Caches[typeof (TPrimary)];
    if (!info.CuryRate.HasValue)
    {
      PXCache pxCache = cach;
      object current = cach.Current;
      string curyId = info.CuryID;
      object[] objArray = new object[2]
      {
        (object) info.CuryRateTypeID,
        null
      };
      System.DateTime? curyEffDate = info.CuryEffDate;
      ref System.DateTime? local = ref curyEffDate;
      objArray[1] = (object) (local.HasValue ? local.GetValueOrDefault().ToShortDateString() : (string) null);
      PXSetPropertyException propertyException = new PXSetPropertyException("The currency rate with the {0} rate type is not defined for the {1} date.", objArray);
      pxCache.RaiseExceptionHandling<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(current, (object) curyId, (Exception) propertyException);
    }
    else
    {
      string immutableMessagePart = ((IEnumerable<string>) "The currency rate with the {0} rate type is not defined for the {1} date.".Split('{')).First<string>();
      if (!cach.GetAttributes<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(cach.Current).OfType<IPXInterfaceField>().Any<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (field => !string.IsNullOrWhiteSpace(field.ErrorText) && field.ErrorText.StartsWith(immutableMessagePart))))
        return;
      cach.ClearFieldErrors<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(cach.Current);
    }
  }

  protected virtual void _(Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row == null)
      return;
    this.ValidateCurrencyRate(e.Row);
    if (string.IsNullOrEmpty(e.Row.CuryID) || string.IsNullOrEmpty(e.Row.BaseCuryID))
    {
      e.Row.BaseCuryID = e.OldRow.BaseCuryID;
      e.Row.CuryID = e.OldRow.CuryID;
    }
    bool? currencyInfoDirty = this.currencyInfoDirty;
    bool flag = false;
    if (currencyInfoDirty.GetValueOrDefault() == flag & currencyInfoDirty.HasValue && e.Row.CuryID == e.OldRow.CuryID && e.Row.CuryRateTypeID == e.OldRow.CuryRateTypeID)
    {
      System.DateTime? curyEffDate1 = e.Row.CuryEffDate;
      System.DateTime? curyEffDate2 = e.OldRow.CuryEffDate;
      if ((curyEffDate1.HasValue == curyEffDate2.HasValue ? (curyEffDate1.HasValue ? (curyEffDate1.GetValueOrDefault() == curyEffDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && e.Row.CuryMultDiv == e.OldRow.CuryMultDiv)
      {
        Decimal? curyRate1 = e.Row.CuryRate;
        Decimal? curyRate2 = e.OldRow.CuryRate;
        if (curyRate1.GetValueOrDefault() == curyRate2.GetValueOrDefault() & curyRate1.HasValue == curyRate2.HasValue)
        {
          e.Cache.IsDirty = false;
          this.currencyInfoDirty = new bool?();
        }
      }
    }
    foreach (PXSelectBase childView in this.ChildViews)
    {
      System.Type itemType = childView.View.CacheGetItemType();
      foreach (IGrouping<string, CuryField> fields in this.TrackedItems[itemType].GroupBy<CuryField, string>((Func<CuryField, string>) (f => f.CuryInfoIDName)))
      {
        if (itemType == this.GetDocumentMapping().Table)
        {
          long? nullable1 = (long?) this.Documents.Cache.GetValue(this.Documents.Current?.Base, fields.Key);
          long? curyInfoId = e.Row.CuryInfoID;
          long? nullable2 = nullable1;
          if (curyInfoId.GetValueOrDefault() == nullable2.GetValueOrDefault() & curyInfoId.HasValue == nullable2.HasValue)
            this.recalculateRowBaseValues(this.Documents.Cache, this.Documents.Current?.Base, (IEnumerable<CuryField>) fields);
        }
        else
        {
          foreach (object obj in childView.View.SelectMulti())
          {
            object row = obj is PXResult ? ((PXResult) obj)[0] : obj;
            long? currencyInfoId = this.GetCurrencyInfoID(childView.Cache, row, fields.Key);
            long? curyInfoId = e.Row.CuryInfoID;
            long? nullable = currencyInfoId;
            if (curyInfoId.GetValueOrDefault() == nullable.GetValueOrDefault() & curyInfoId.HasValue == nullable.HasValue)
            {
              this.recalculateRowBaseValues(childView.Cache, row, (IEnumerable<CuryField>) fields);
              childView.Cache.MarkUpdated(row);
            }
          }
        }
      }
    }
  }

  /// <summary>The RowSelected event handler for the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo" /> DAC. The handler sets the values of the Enabled property of the UI fields of <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo" /> according to the values of this property of the corresponding fields of <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" />.</summary>
  /// <param name="e">Parameters of the event.</param>
  protected virtual void _(Events.RowSelected<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row == null)
      return;
    e.Row.DisplayCuryID = e.Row.CuryID;
    PX.Objects.Extensions.MultiCurrency.CurySource source = this.CurrentSourceSelect();
    bool isEnabled = this.AllowOverrideRate(e.Cache, e.Row, source);
    long? nullable = CurrencyCollection.MatchBaseCuryInfoId(e.Row);
    PXUIFieldAttribute.SetVisible<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>(e.Cache, (object) e.Row, !nullable.HasValue);
    PXUIFieldAttribute.SetVisible<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(e.Cache, (object) e.Row, !nullable.HasValue);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>(e.Cache, (object) e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID>(e.Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.displayCuryID>(e.Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.curyID>(e.Cache, (object) e.Row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>(e.Cache, (object) e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>(e.Cache, (object) e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.sampleCuryRate>(e.Cache, (object) e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.Extensions.CurrencyInfo.sampleRecipRate>(e.Cache, (object) e.Row, isEnabled);
  }

  protected virtual bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, PX.Objects.Extensions.MultiCurrency.CurySource source)
  {
    bool flag = true;
    bool? nullable;
    if (source != null)
    {
      nullable = source.AllowOverrideRate;
      if (!nullable.GetValueOrDefault())
        goto label_3;
    }
    nullable = info.IsReadOnly;
    if (!nullable.GetValueOrDefault() && !(info.CuryID == info.BaseCuryID))
      goto label_4;
label_3:
    flag = false;
label_4:
    return flag;
  }

  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyPrecision> e)
  {
    e.NewValue = (object) Convert.ToInt16(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).CuryDecimalPlaces(e.Row.CuryID));
  }

  protected virtual void _(
    Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.basePrecision> e)
  {
    e.NewValue = (object) Convert.ToInt16(ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).CuryDecimalPlaces(e.Row.BaseCuryID));
  }

  protected virtual void defaultEffectiveDate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    object newValue;
    if (sender.RaiseFieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) info, out newValue))
      sender.RaiseFieldUpdating<PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>((object) info, ref newValue);
    info.CuryEffDate = (System.DateTime?) newValue;
  }

  protected virtual void defaultCurrencyRate(
    PXCache sender,
    PX.Objects.CM.Extensions.CurrencyInfo info,
    bool forceDefault,
    bool suppressErrors)
  {
    if (info.IsReadOnly.GetValueOrDefault())
      return;
    IPXCurrencyRate rate = info.SearchForNewRate((PXGraph) this.Base);
    if (rate != null)
    {
      System.DateTime? curyEffDate1 = info.CuryEffDate;
      rate.Populate(info);
      if (suppressErrors)
        return;
      System.DateTime? curyEffDate2 = rate.CuryEffDate;
      System.DateTime? nullable = curyEffDate1;
      if ((curyEffDate2.HasValue & nullable.HasValue ? (curyEffDate2.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
      int rateEffDays = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).GetRateEffDays(info.CuryRateTypeID);
      if (rateEffDays <= 0)
        return;
      nullable = curyEffDate1;
      curyEffDate2 = rate.CuryEffDate;
      if ((nullable.HasValue & curyEffDate2.HasValue ? new TimeSpan?(nullable.GetValueOrDefault() - curyEffDate2.GetValueOrDefault()) : new TimeSpan?()).Value.Days >= rateEffDays)
        throw new PXRateIsNotDefinedForThisDateException(info.CuryRateTypeID, rate.FromCuryID, rate.ToCuryID, curyEffDate1.Value);
    }
    else
    {
      if (!forceDefault)
        return;
      if (string.Equals(info.CuryID, info.BaseCuryID))
      {
        bool isDirty = sender.IsDirty;
        PX.Objects.CM.Extensions.CurrencyInfo data = new PX.Objects.CM.Extensions.CurrencyInfo();
        sender.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyRate>((object) data);
        sender.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>((object) data);
        sender.SetDefaultExt<PX.Objects.CM.Extensions.CurrencyInfo.recipRate>((object) data);
        info.CuryRate = new Decimal?(System.Math.Round(data.CuryRate.Value, 8));
        info.CuryMultDiv = data.CuryMultDiv;
        info.RecipRate = new Decimal?(System.Math.Round(data.RecipRate.Value, 8));
        sender.IsDirty = isDirty;
      }
      else
      {
        if (suppressErrors)
          return;
        info.CuryRate = new Decimal?();
        info.RecipRate = new Decimal?();
        info.CuryMultDiv = "M";
        if (info.CuryRateTypeID != null && info.CuryEffDate.HasValue)
          throw new PXSetPropertyException("Currency Rate is not defined.", PXErrorLevel.Warning);
      }
    }
  }

  protected virtual bool checkRateVariance(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info)
  {
    CMSetup cmSetup = this.GetCMSetup();
    if ((cmSetup != null ? (!cmSetup.RateVarianceWarn.GetValueOrDefault() ? 1 : 0) : 1) == 0)
    {
      Decimal? nullable;
      if (cmSetup != null)
      {
        nullable = cmSetup.RateVariance;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          goto label_3;
      }
      IPXCurrencyRate rate = info.SearchForNewRate((PXGraph) this.Base);
      int num1;
      if (rate == null)
      {
        num1 = 1;
      }
      else
      {
        nullable = rate.CuryRate;
        num1 = !nullable.HasValue ? 1 : 0;
      }
      if (num1 == 0)
      {
        if (rate != null)
        {
          nullable = rate.CuryRate;
          Decimal num2 = 0M;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
            goto label_10;
        }
        Decimal num3 = System.Math.Abs(MultiCurrencyGraph<TGraph, TPrimary>.CalculateRateVariance(info, rate));
        nullable = cmSetup.RateVariance;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        return num3 > valueOrDefault & nullable.HasValue;
      }
label_10:
      return false;
    }
label_3:
    return false;
  }

  private CMSetup GetCMSetup()
  {
    return (CMSetup) this.Base.Caches[typeof (CMSetup)].Current ?? (CMSetup) PXSelectBase<CMSetup, PXSelectReadonly<CMSetup>.Config>.Select((PXGraph) this.Base);
  }

  private static Decimal CalculateRateVariance(PX.Objects.CM.Extensions.CurrencyInfo info, IPXCurrencyRate rate)
  {
    Decimal? curyRate1 = info.CuryRate;
    Decimal num1 = curyRate1.Value;
    curyRate1 = rate.CuryRate;
    Decimal num2 = curyRate1.Value;
    Decimal num3 = num1 - num2;
    if (!(rate.CuryMultDiv == info.CuryMultDiv))
    {
      Decimal? curyRate2 = info.CuryRate;
      Decimal num4 = 0M;
      if (!(curyRate2.GetValueOrDefault() == num4 & curyRate2.HasValue))
        return 100M * (1M / num3) / rate.CuryRate.Value;
    }
    return 100M * num3 / rate.CuryRate.Value;
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.sampleRecipRate> e)
  {
    if (!e.ExternalCall)
      return;
    Decimal num = System.Math.Round(e.Row.SampleRecipRate.Value, 8);
    if (num == 0M)
      num = 1M;
    e.Row.CuryRate = new Decimal?(num);
    e.Row.RecipRate = new Decimal?(System.Math.Round(1M / num, 8));
    e.Row.CuryMultDiv = "D";
    if (!this.checkRateVariance(e.Cache, e.Row))
      return;
    PXUIFieldAttribute.SetWarning<PX.Objects.CM.Extensions.CurrencyInfo.sampleRecipRate>(e.Cache, (object) e.Row, "Rate variance exceeds the limit specified on the Currency Management Preferences form.");
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.baseCuryID> e)
  {
    e.Row.BasePrecision = new short?();
  }

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.sampleCuryRate> e)
  {
    if (!e.ExternalCall)
      return;
    Decimal num = System.Math.Round(e.Row.SampleCuryRate.Value, 8);
    bool flag = false;
    if (num == 0M)
    {
      try
      {
        this.defaultCurrencyRate(e.Cache, e.Row, true, false);
        flag = true;
      }
      catch (PXSetPropertyException ex)
      {
        num = 1M;
      }
    }
    if (!flag)
    {
      e.Row.CuryRate = new Decimal?(num);
      e.Row.RecipRate = new Decimal?(System.Math.Round(1M / num, 8));
      e.Row.CuryMultDiv = "M";
    }
    if (!this.checkRateVariance(e.Cache, e.Row))
      return;
    PXUIFieldAttribute.SetWarning<PX.Objects.CM.Extensions.CurrencyInfo.sampleCuryRate>(e.Cache, (object) e.Row, "Rate variance exceeds the limit specified on the Currency Management Preferences form.");
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  public virtual object GetRow(PXCache sender, object row) => row;

  /// <summary>A class that defines the default mapping of the <see cref="T:PX.Objects.Extensions.MultiCurrency.Document" /> class to a DAC.</summary>
  protected class DocumentMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type BAccountID = typeof (Document.bAccountID);
    /// <exclude />
    public System.Type BranchID = typeof (Document.branchID);
    /// <exclude />
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    /// <exclude />
    public System.Type CuryID = typeof (Document.curyID);
    /// <exclude />
    public System.Type DocumentDate = typeof (Document.documentDate);

    /// <exclude />
    public System.Type Extension => typeof (Document);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="T:PX.Objects.Extensions.MultiCurrency.Document" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public DocumentMapping(System.Type table) => this._table = table;
  }

  /// <summary>A class that defines the default mapping of the <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" /> mapped cache extension to a DAC.</summary>
  protected class CurySourceMapping : IBqlMapping
  {
    /// <exclude />
    protected System.Type _table;
    /// <exclude />
    public System.Type CuryID = typeof (PX.Objects.Extensions.MultiCurrency.CurySource.curyID);
    /// <exclude />
    public System.Type CuryRateTypeID = typeof (PX.Objects.Extensions.MultiCurrency.CurySource.curyRateTypeID);
    /// <exclude />
    public System.Type AllowOverrideCury = typeof (PX.Objects.Extensions.MultiCurrency.CurySource.allowOverrideCury);
    /// <exclude />
    public System.Type AllowOverrideRate = typeof (PX.Objects.Extensions.MultiCurrency.CurySource.allowOverrideRate);

    /// <exclude />
    public System.Type Extension => typeof (PX.Objects.Extensions.MultiCurrency.CurySource);

    /// <exclude />
    public System.Type Table => this._table;

    /// <summary>Creates the default mapping of the <see cref="F:PX.Objects.Extensions.MultiCurrency.MultiCurrencyGraph`2.CurySource" /> mapped cache extension to the specified table.</summary>
    /// <param name="table">A DAC.</param>
    public CurySourceMapping(System.Type table) => this._table = table;
  }
}
