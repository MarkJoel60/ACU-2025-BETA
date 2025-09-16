// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.CurrencyInfoAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CM.Extensions;

/// <summary>
/// Manages Foreign exchage data for the document or the document detail.
/// When used for the detail useally a reference to the parent document is passed through ParentCuryInfoID in a constructor.
/// </summary>
/// <example>
/// [CurrencyInfo]  - Document declaration
/// [CurrencyInfo(typeof(ARRegister.curyInfoID))] - Detail declaration
/// </example>
public class CurrencyInfoAttribute(Type ParentCuryInfoID) : 
  PXDBDefaultAttribute(ParentCuryInfoID),
  IPXReportRequiredField,
  IPXDependsOnFields
{
  protected Dictionary<long, string> _Matches;
  private object _KeyToAbort;
  private bool _Required;

  public virtual bool PopulateParentCuryInfoID { get; set; }

  public bool Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  public CurrencyInfoAttribute()
    : this(typeof (CurrencyInfo.curyInfoID))
  {
    this._KeyToAbort = (object) null;
  }

  protected virtual void EnsureIsRestriction(PXCache sender)
  {
    if (this._IsRestriction.Value.HasValue)
      return;
    this._IsRestriction.Value = new bool?(true);
  }

  public virtual bool IsTopLevel
  {
    get
    {
      return this._SourceType == (Type) null || typeof (CurrencyInfo).IsAssignableFrom(this._SourceType);
    }
  }

  protected virtual void CurrencyInfo_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    CurrencyInfo row = (CurrencyInfo) e.Row;
    long? nullable = row != null || !this._Required ? CurrencyCollection.MatchBaseCuryInfoId(row) : throw new PXSetPropertyException("The currency must be specified.");
    if (e.Operation == 2)
    {
      if (nullable.HasValue)
      {
        CurrencyInfo copy = (CurrencyInfo) cache.CreateCopy((object) row);
        base.EnsureIsRestriction(cache);
        this.StorePersisted(cache, (object) copy);
        copy.CuryInfoID = nullable;
        cache.SetStatus((object) copy, (PXEntryStatus) 0);
        ((CancelEventArgs) e).Cancel = true;
      }
      else
      {
        base.EnsureIsRestriction(cache);
        this.StorePersisted(cache, e.Row);
      }
    }
    long? curyInfoId1 = row.CuryInfoID;
    long? curyInfoId2 = (long?) CurrencyCollection.GetCurrency(row.BaseCuryID)?.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    long? key = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName);
    if (this.IsTopLevel && key.HasValue)
    {
      PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
      CurrencyInfo currencyInfo = (CurrencyInfo) cach.Locate((object) new CurrencyInfo()
      {
        CuryInfoID = key
      });
      PXEntryStatus pxEntryStatus = (PXEntryStatus) 0;
      base.EnsureIsRestriction(sender);
      if (currencyInfo != null && (pxEntryStatus = cach.GetStatus((object) currencyInfo)) == 2)
      {
        if (this._IsRestriction.Persisted == null || !this._IsRestriction.Persisted.ContainsKey((object) key))
          cach.PersistInserted((object) currencyInfo);
      }
      else if (pxEntryStatus == 1)
        cach.PersistUpdated((object) currencyInfo);
      else if (pxEntryStatus == 3)
        cach.PersistDeleted((object) currencyInfo);
    }
    object obj;
    if (!this.IsTopLevel && this._IsRestriction.Persisted != null && key.HasValue && this._IsRestriction.Persisted.TryGetValue((object) key, out obj))
    {
      this._KeyToAbort = (object) key;
      key = obj is CurrencyInfo currencyInfo ? currencyInfo.CuryInfoID : sender.Graph.Caches[obj.GetType()].GetValue(obj, this._SourceField) as long?;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) key);
    }
    if (key.HasValue)
    {
      long? nullable = key;
      long num = 0;
      if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
        return;
    }
    base.RowPersisting(sender, e);
  }

  public virtual void SourceRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object key = sender.GetValue(e.Row, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName);
    object obj;
    if (this._IsRestriction.Persisted != null && key != null && this._IsRestriction.Persisted.TryGetValue(key, out obj) && obj.GetType() == this._SourceType)
      return;
    base.SourceRowPersisting(sender, e);
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    long? key = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (e.TranStatus == null)
    {
      long? nullable = key;
      long num = 1;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue && EnumerableExtensions.IsNotIn<PXEntryStatus>(sender.GetStatus(e.Row), (PXEntryStatus) 3, (PXEntryStatus) 4))
        throw new PXException("An error occurred while saving Currency Info for the table '{0}'", new object[1]
        {
          (object) sender.GetItemType().Name
        });
    }
    if (this.IsTopLevel && e.TranStatus != null)
      sender.Graph.Caches[typeof (CurrencyInfo)].Persisted(e.TranStatus == 2);
    object obj;
    if (e.TranStatus == 2 && this._KeyToAbort == null && e.Operation == 1 && this._IsRestriction.Persisted != null && this._IsRestriction.Persisted.TryGetValue((object) key, out obj))
      this._KeyToAbort = (object) (obj is CurrencyInfo currencyInfo ? currencyInfo.CuryInfoID : new long?());
    if (e.TranStatus == 2 && this._KeyToAbort != null)
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._KeyToAbort);
    else
      base.RowPersisted(sender, e);
  }

  protected virtual void RowSelectingCollectMatches(PXCache sender, PXRowSelectingEventArgs e)
  {
    long? nullable = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (!nullable.HasValue)
      return;
    string str = (string) sender.GetValue(e.Row, "CuryID");
    if (string.IsNullOrEmpty(str))
      return;
    this._Matches[nullable.Value] = str;
  }

  public ISet<Type> GetDependencies(PXCache cache)
  {
    HashSet<Type> dependencies = new HashSet<Type>();
    Type bqlField = cache.GetBqlField("CuryID");
    if (bqlField != (Type) null)
      dependencies.Add(bqlField);
    return (ISet<Type>) dependencies;
  }

  private static Type GetPrimaryType(PXGraph graph)
  {
    foreach (DictionaryEntry action in (OrderedDictionary) graph.Actions)
    {
      try
      {
        Type rowType;
        if ((rowType = ((PXAction) action.Value).GetRowType()) != (Type) null)
          return rowType;
      }
      catch (Exception ex)
      {
      }
    }
    return (Type) null;
  }

  public static CurrencyInfo GetCurrencyInfo<Field>(PXCache sender, object row) where Field : IBqlField
  {
    return CurrencyInfoAttribute.GetCurrencyInfo(sender, typeof (Field), row);
  }

  public static CurrencyInfo GetCurrencyInfo(PXCache sender, Type field, object row)
  {
    foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly(row, field.Name))
    {
      if (subscriberAttribute is CurrencyInfoAttribute currencyInfoAttribute)
        return currencyInfoAttribute.GetCurrencyInfo(sender, row);
    }
    return (CurrencyInfo) null;
  }

  protected virtual CurrencyInfo GetCurrencyInfo(PXCache sender, object row)
  {
    long? key = (long?) sender.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
    CurrencyInfo currencyInfo1 = new CurrencyInfo()
    {
      CuryInfoID = key
    };
    object obj;
    if (this._SourceType != (Type) null && this._IsRestriction.Persisted != null && this._IsRestriction.Persisted.TryGetValue((object) key, out obj))
    {
      long? nullable = sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName) as long?;
      if (nullable.HasValue && nullable.Value > 0L)
        currencyInfo1 = new CurrencyInfo()
        {
          CuryInfoID = nullable
        };
    }
    CurrencyInfo currencyInfo2 = currencyInfo1;
    return (CurrencyInfo) cach.Locate((object) currencyInfo2);
  }

  /// <exclude />
  public static long? GetPersistedCuryInfoID(PXCache sender, long? curyInfoID)
  {
    if (!curyInfoID.HasValue || curyInfoID.Value > 0L)
      return curyInfoID;
    long[] array = sender.GetAttributesReadonly((string) null).OfType<CurrencyInfoAttribute>().Select<CurrencyInfoAttribute, long?>((Func<CurrencyInfoAttribute, long?>) (cattr => cattr?.TryGetPersistedID(sender, curyInfoID))).Where<long?>((Func<long?, bool>) (_ => _.HasValue)).Cast<long>().ToArray<long>();
    return ((IEnumerable<long>) array).Any<long>() ? new long?(((IEnumerable<long>) array).Distinct<long>().Single<long>()) : curyInfoID;
  }

  private long? TryGetPersistedID(PXCache sender, long? curyInfoID)
  {
    if (this._SourceType == (Type) null || this._IsRestriction.Persisted == null)
      return new long?();
    object obj;
    if (!this._IsRestriction.Persisted.TryGetValue((object) curyInfoID, out obj))
      return new long?();
    long? persistedId = obj is CurrencyInfo currencyInfo ? currencyInfo.CuryInfoID : new long?();
    if (persistedId.HasValue)
    {
      long? nullable = persistedId;
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        return persistedId;
    }
    long? nullable1 = sender.Graph.Caches[this._SourceType].GetValue(obj, this._SourceField ?? ((PXEventSubscriberAttribute) this)._FieldName) as long?;
    return nullable1.HasValue && nullable1.Value > 0L ? nullable1 : new long?();
  }

  /// <summary>
  /// Verifies if field is not is one of which changes on changing Cury/Base view state
  /// </summary>
  /// <param name="fieldName">field name to verify</param>
  /// <returns></returns>
  internal static bool IsDifferenceEssential(string fieldName)
  {
    switch (fieldName)
    {
      case "Base":
        return false;
      case "CuryViewState":
        return false;
      case "LastModifiedByScreenID":
        return false;
      case "RLastModifiedByScreenID":
        return false;
      default:
        return true;
    }
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass28_0 cDisplayClass280 = new CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass28_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.sender = sender;
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowPersistingEvents rowPersisting = cDisplayClass280.sender.Graph.RowPersisting;
    CurrencyInfoAttribute currencyInfoAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) currencyInfoAttribute1, __vmethodptr(currencyInfoAttribute1, CurrencyInfo_RowPersisting));
    rowPersisting.AddHandler<CurrencyInfo>(pxRowPersisting);
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(cDisplayClass280.sender);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass280.sender.GetBqlField("CuryID") != (Type) null && (this._Matches = CurrencyInfo.CuryIDStringAttribute.GetMatchesDictionary(cDisplayClass280.sender)) != null)
    {
      // ISSUE: reference to a compiler-generated field
      PXGraph.RowSelectingEvents rowSelecting = cDisplayClass280.sender.Graph.RowSelecting;
      // ISSUE: reference to a compiler-generated field
      Type itemType = cDisplayClass280.sender.GetItemType();
      CurrencyInfoAttribute currencyInfoAttribute2 = this;
      // ISSUE: virtual method pointer
      PXRowSelecting pxRowSelecting = new PXRowSelecting((object) currencyInfoAttribute2, __vmethodptr(currencyInfoAttribute2, RowSelectingCollectMatches));
      rowSelecting.AddHandler(itemType, pxRowSelecting);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.sourceType = this._SourceType == typeof (CurrencyInfo) ? CurrencyInfoAttribute.GetPrimaryType(cDisplayClass280.sender.Graph) : this._SourceType;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass280.sourceType != (Type) null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass280.cacheType = cDisplayClass280.sender.Graph.Caches[cDisplayClass280.sourceType].GetItemType();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      PXParentAttribute pxParentAttribute = cDisplayClass280.sender.GetAttributesReadonly((string) null).OfType<PXParentAttribute>().FirstOrDefault<PXParentAttribute>(new Func<PXParentAttribute, bool>(cDisplayClass280.\u003CCacheAttached\u003Eb__1));
      if (pxParentAttribute != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass28_1 cDisplayClass281 = new CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass28_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass281.CS\u0024\u003C\u003E8__locals1 = cDisplayClass280;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass281.parentType = pxParentAttribute.ParentType;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        cDisplayClass281.CS\u0024\u003C\u003E8__locals1.sender.Graph.FieldUpdated.AddHandler(cDisplayClass281.parentType, this._SourceField, new PXFieldUpdated((object) cDisplayClass281, __methodptr(\u003CCacheAttached\u003Eb__2)));
      }
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass280.sender.Graph.OnAfterPersist += (Action<PXGraph>) (graph =>
    {
      PXCache cach = graph.Caches[typeof (CurrencyInfo)];
      foreach (CurrencyInfo currencyInfo in cach.Inserted.ToArray<CurrencyInfo>())
      {
        if (currencyInfo.BaseCuryID == currencyInfo.CuryID)
        {
          long? curyInfoId = currencyInfo.CuryInfoID;
          long num = 0;
          if (curyInfoId.GetValueOrDefault() < num & curyInfoId.HasValue)
          {
            currencyInfo.CuryInfoID = CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).CuryInfoID;
            cach.SetStatus((object) currencyInfo, (PXEntryStatus) 0);
          }
        }
      }
    });
  }
}
