// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyInfoAttribute
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
using System.Reflection;

#nullable disable
namespace PX.Objects.CM;

/// <summary>
/// Manages Foreign exchage data for the document or the document detail.
/// When used for the detail useally a reference to the parent document is passed through ParentCuryInfoID in a constructor.
/// </summary>
/// <example>
/// [CurrencyInfo(ModuleCode = "AR")]  - Document declaration
/// [CurrencyInfo(typeof(ARRegister.curyInfoID))] - Detail declaration
/// </example>
public class CurrencyInfoAttribute : 
  PXAggregateAttribute,
  IPXRowInsertingSubscriber,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber,
  IPXRowUpdatingSubscriber,
  IPXReportRequiredField,
  IPXDependsOnFields
{
  /// <summary>
  /// The negative value of the record's currency info ID field
  /// that will be assigned back to it upon transaction abort.
  /// This field handles DB transaction abort from the record side
  /// (<see cref="M:PX.Objects.CM.CurrencyInfoAttribute.RowPersisted(PX.Data.PXCache,PX.Data.PXRowPersistedEventArgs)" />). Compare with <see cref="!:_SelfKeyToAbort" />.
  /// </summary>
  protected object _KeyToAbort;
  public const string _CuryViewField = "CuryViewState";
  protected Type _ChildType;
  protected object _oldRow;
  protected bool _NeedSync;
  protected string _ModuleCode;
  private bool _Required;
  public const string DefaultCuryIDFieldName = "CuryID";
  protected string _CuryIDField = "CuryID";
  public const string DefaultCuryRateFieldName = "CuryRate";
  protected string _CuryRateField = "CuryRate";
  public const string DefaultCuryIDDisplayName = "Currency";
  protected string _CuryDisplayName = "Currency";
  protected bool _Enabled = true;
  private Type _ParentType;
  public const string DefaultBranchIDFieldName = "BranchID";
  protected Type _baseCurySourceBranchIdField;
  private bool _InternalCall;
  /// <summary>
  /// The negative value of the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryInfoID" />
  /// that will be assigned back to the record's currency info field
  /// upon transaction abort. This field handles DB transaction abort
  /// from the CurrencyInfo side (<see cref="M:PX.Objects.CM.CurrencyInfoAttribute.CurrencyInfo_RowPersisted(PX.Data.PXCache,PX.Data.PXRowPersistedEventArgs)" />).
  /// Compare with <see cref="F:PX.Objects.CM.CurrencyInfoAttribute._KeyToAbort" />.
  /// </summary>
  private Dictionary<long?, CurrencyInfo> _persisted;

  protected bool _ParentChildMode
  {
    get => ((List<PXEventSubscriberAttribute>) this._Attributes).Count == 0;
  }

  public string ModuleCode
  {
    get => this._ModuleCode;
    set => this._ModuleCode = value;
  }

  public bool Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  public string CuryIDField
  {
    get => this._CuryIDField;
    set => this._CuryIDField = value;
  }

  public string CuryRateField
  {
    get => this._CuryRateField;
    set => this._CuryRateField = value;
  }

  public string CuryDisplayName
  {
    get => PXMessages.LocalizeNoPrefix(this._CuryDisplayName);
    set => this._CuryDisplayName = value;
  }

  public bool Enabled
  {
    get => this._Enabled;
    set => this._Enabled = value;
  }

  public bool SuppressDefaultBaseCury { get; set; }

  public CurrencyInfoAttribute()
  {
  }

  public CurrencyInfoAttribute(Type ParentCuryInfoID)
  {
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    CurrencyInfoAttribute.CurrencyInfoDefaultAttribute defaultAttribute = new CurrencyInfoAttribute.CurrencyInfoDefaultAttribute(ParentCuryInfoID);
    defaultAttribute.PersistingCheck = (PXPersistingCheck) 2;
    attributes.Add((PXEventSubscriberAttribute) defaultAttribute);
    this._ParentType = BqlCommand.GetItemType(ParentCuryInfoID);
  }

  public CurrencyInfoAttribute(Type ParentCuryInfoID, Type BaseCurySourceBranchIdField)
  {
    this._baseCurySourceBranchIdField = BaseCurySourceBranchIdField;
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

  public static PXView GetView(PXGraph graph, Type ClassType, Type KeyField)
  {
    if (!graph.Views.Caches.Contains(ClassType))
      return (PXView) null;
    string key = $"_{ClassType.Name}.{KeyField.Name}_CurrencyInfo.CuryInfoID_";
    PXView siblingsView;
    if (!((Dictionary<string, PXView>) graph.Views).TryGetValue(key, out siblingsView))
    {
      siblingsView = CurrencyInfoAttribute.CreateSiblingsView(graph, ClassType, KeyField);
      graph.Views[key] = siblingsView;
    }
    return siblingsView;
  }

  public static PXView GetViewInversed(PXGraph graph, Type ClassType, Type KeyField)
  {
    string key = $"_CurrencyInfo.CuryInfoID_{ClassType.Name}.{KeyField.Name}_";
    PXView viewInversed;
    if (!((Dictionary<string, PXView>) graph.Views).TryGetValue(key, out viewInversed))
    {
      BqlCommand instance = BqlCommand.CreateInstance(new Type[7]
      {
        typeof (Select<,>),
        typeof (CurrencyInfo),
        typeof (Where<,>),
        typeof (CurrencyInfo.curyInfoID),
        typeof (Equal<>),
        typeof (Optional<>),
        KeyField
      });
      viewInversed = new PXView(graph, false, instance);
      graph.Views[key] = viewInversed;
    }
    return viewInversed;
  }

  public static void SetEffectiveDate<Field, CuryKeyField>(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
    where Field : IBqlField
    where CuryKeyField : IBqlField
  {
    CurrencyInfoAttribute.SetEffectiveDate<Field>(sender, e, typeof (CuryKeyField));
  }

  public static void SetEffectiveDate<Field>(PXCache sender, PXFieldUpdatedEventArgs e) where Field : IBqlField
  {
    Type curyKeyField = (Type) null;
    foreach (PXEventSubscriberAttribute subscriberAttribute in sender.GetAttributesReadonly((string) null))
    {
      if (subscriberAttribute is CurrencyInfoAttribute)
      {
        curyKeyField = sender.GetBqlField(subscriberAttribute.FieldName);
        break;
      }
    }
    CurrencyInfoAttribute.SetEffectiveDate<Field>(sender, e, curyKeyField);
  }

  protected static void SetEffectiveDate<Field>(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    Type curyKeyField)
    where Field : IBqlField
  {
    CurrencyInfoAttribute.SetEffectiveDate<Field>(sender, e.Row, curyKeyField);
  }

  public static void SetEffectiveDate<Field>(PXCache sender, object row, Type curyKeyField) where Field : IBqlField
  {
    if (!(curyKeyField != (Type) null))
      return;
    PXView viewInversed = CurrencyInfoAttribute.GetViewInversed(sender.Graph, BqlCommand.GetItemType(curyKeyField), curyKeyField);
    foreach (CurrencyInfo currencyInfo in viewInversed.SelectMulti(Array.Empty<object>()))
    {
      PXCache cache = viewInversed.Cache;
      object obj = sender.GetValue<Field>(row);
      CurrencyInfo copy = PXCache<CurrencyInfo>.CreateCopy(currencyInfo);
      cache.SetValueExt<CurrencyInfo.curyEffDate>((object) currencyInfo, obj);
      string error = PXUIFieldAttribute.GetError<CurrencyInfo.curyEffDate>(cache, (object) currencyInfo);
      if (!string.IsNullOrEmpty(error))
        sender.RaiseExceptionHandling<Field>(row, (object) null, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
      cache.RaiseRowUpdated((object) currencyInfo, (object) copy);
      if (cache.GetStatus((object) currencyInfo) != 2)
        cache.SetStatus((object) currencyInfo, (PXEntryStatus) 1);
    }
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
    if (this._persisted != null && key.HasValue && this._persisted.TryGetValue(key, out currencyInfo1))
      return currencyInfo1;
    CurrencyInfo currencyInfo2 = (CurrencyInfo) cach.Locate((object) currencyInfo1);
    if (currencyInfo2 != null)
      return currencyInfo2;
    Type bqlField = sender.GetBqlField(((PXEventSubscriberAttribute) this)._FieldName);
    PXView viewInversed = CurrencyInfoAttribute.GetViewInversed(sender.Graph, BqlCommand.GetItemType(bqlField), bqlField);
    if (!key.HasValue)
      return (CurrencyInfo) null;
    return (CurrencyInfo) viewInversed.SelectSingle(new object[1]
    {
      (object) key
    });
  }

  public virtual void CurrencyInfo_CuryRate_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Exception is PXSetPropertyException) || this._InternalCall)
      return;
    PXCache cach = sender.Graph.Caches[this._ChildType];
    foreach (object obj in cach.Inserted)
    {
      long? nullable = (long?) cach.GetValue(obj, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      long? keyToAbort = (long?) this._KeyToAbort;
      if (nullable.GetValueOrDefault() == keyToAbort.GetValueOrDefault() & nullable.HasValue == keyToAbort.HasValue)
      {
        this._InternalCall = true;
        try
        {
          sender.RaiseExceptionHandling<CurrencyInfo.sampleCuryRate>(e.Row, e.NewValue, e.Exception);
        }
        finally
        {
          this._InternalCall = false;
        }
        cach.RaiseExceptionHandling(this._CuryIDField, obj, cach.GetValue(obj, this._CuryIDField), e.Exception);
      }
    }
    foreach (object obj in cach.Updated)
    {
      long? nullable = (long?) cach.GetValue(obj, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      long? keyToAbort = (long?) this._KeyToAbort;
      if (nullable.GetValueOrDefault() == keyToAbort.GetValueOrDefault() & nullable.HasValue == keyToAbort.HasValue)
      {
        this._InternalCall = true;
        try
        {
          sender.RaiseExceptionHandling<CurrencyInfo.sampleCuryRate>(e.Row, e.NewValue, e.Exception);
        }
        finally
        {
          this._InternalCall = false;
        }
        cach.RaiseExceptionHandling(this._CuryIDField, obj, cach.GetValue(obj, this._CuryIDField), e.Exception);
      }
    }
  }

  public virtual void CurrencyInfo_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CurrencyInfo row = (CurrencyInfo) e.Row;
    long? nullable = row != null || !this._Required ? CurrencyCollection.MatchBaseCuryInfoId(row) : throw new PXSetPropertyException("The currency must be specified.");
    if (e.Operation == 2)
    {
      long? curyInfoId = row.CuryInfoID;
      long num = 0;
      if (curyInfoId.GetValueOrDefault() < num & curyInfoId.HasValue)
        this._persisted[row.CuryInfoID] = row;
      if (!nullable.HasValue)
        return;
      CurrencyInfo copy = (CurrencyInfo) sender.CreateCopy((object) row);
      copy.CuryInfoID = nullable;
      sender.SetStatus((object) copy, (PXEntryStatus) 0);
      this._persisted[row.CuryInfoID] = copy;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      long? curyInfoId1 = row.CuryInfoID;
      long? curyInfoId2 = (long?) CurrencyCollection.GetCurrency(row.BaseCuryID)?.CuryInfoID;
      if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  public virtual void CurrencyInfo_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation != 2 || e.TranStatus != 2)
      return;
    CurrencyInfo info = (CurrencyInfo) e.Row;
    long? curyInfoId1 = info.CuryInfoID;
    long num = 0;
    if (!(curyInfoId1.GetValueOrDefault() > num & curyInfoId1.HasValue))
      return;
    CurrencyInfo currencyInfo = this._persisted.FirstOrDefault<KeyValuePair<long?, CurrencyInfo>>((Func<KeyValuePair<long?, CurrencyInfo>, bool>) (p =>
    {
      long? curyInfoId2 = p.Value.CuryInfoID;
      long? curyInfoId3 = info.CuryInfoID;
      return curyInfoId2.GetValueOrDefault() == curyInfoId3.GetValueOrDefault() & curyInfoId2.HasValue == curyInfoId3.HasValue;
    })).Value;
    if (currencyInfo == null)
      return;
    sender.SetValue<CurrencyInfo.curyInfoID>(e.Row, (object) currencyInfo.CuryInfoID);
  }

  public virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    long? nullable = CurrencyCollection.MatchBaseCuryInfoId((CurrencyInfo) e.Row);
    PXUIFieldAttribute.SetVisible<CurrencyInfo.curyRateTypeID>(sender, e.Row, !nullable.HasValue);
    PXUIFieldAttribute.SetVisible<CurrencyInfo.curyEffDate>(sender, e.Row, !nullable.HasValue);
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
    object objB;
    if ((objB = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal)) == null)
    {
      CurrencyInfo currencyInfo1 = new CurrencyInfo();
      if (!string.IsNullOrEmpty(this._ModuleCode))
        currencyInfo1.ModuleCode = this._ModuleCode;
      CurrencyInfo currencyInfo2 = (CurrencyInfo) cach.Insert((object) currencyInfo1);
      cach.IsDirty = false;
      if (currencyInfo2 == null)
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) currencyInfo2.CuryInfoID);
      if (!this._NeedSync)
        return;
      sender.SetValue(e.Row, this._CuryIDField, (object) currencyInfo2.CuryID);
    }
    else
    {
      if (!this._NeedSync)
        return;
      CurrencyInfo currencyInfo3 = (CurrencyInfo) null;
      foreach (CurrencyInfo currencyInfo4 in cach.Inserted)
      {
        if (object.Equals((object) currencyInfo4.CuryInfoID, objB))
        {
          currencyInfo3 = currencyInfo4;
          break;
        }
      }
      if (currencyInfo3 == null)
        currencyInfo3 = PXResultset<CurrencyInfo>.op_Implicit(PXSelectBase<CurrencyInfo, PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<PX.Data.Required<CurrencyInfo.curyInfoID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          objB
        }));
      if (currencyInfo3 == null)
        return;
      sender.SetValue(e.Row, this._CuryIDField, (object) currencyInfo3.CuryID);
    }
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) is long objA))
      return;
    PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
    if (Convert.ToInt64((object) objA) < 0L)
    {
      CurrencyInfo currencyInfo1;
      if (this._persisted.TryGetValue(new long?(objA), out currencyInfo1) && cach.Locate((object) currencyInfo1) != null)
      {
        this._KeyToAbort = (object) objA;
        sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) currencyInfo1.CuryInfoID);
      }
      else
      {
        if (!this._ParentChildMode)
          return;
        foreach (CurrencyInfo currencyInfo2 in cach.Inserted)
        {
          if (object.Equals((object) objA, (object) currencyInfo2.CuryInfoID))
          {
            cach.PersistInserted((object) currencyInfo2);
            CurrencyInfo currencyInfo3;
            if (this._persisted.TryGetValue(new long?(objA), out currencyInfo3))
            {
              this._KeyToAbort = (object) objA;
              sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) currencyInfo3.CuryInfoID);
            }
          }
        }
      }
    }
    else
    {
      if (!this._ParentChildMode)
        return;
      foreach (CurrencyInfo currencyInfo in cach.Updated)
      {
        if (object.Equals((object) objA, (object) currencyInfo.CuryInfoID))
        {
          cach.PersistUpdated((object) currencyInfo);
          break;
        }
      }
    }
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus == null && e.Operation == 2)
    {
      long? nullable = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      long num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
        throw new PXException("An error occurred while saving Currency Info for the table '{0}'", new object[1]
        {
          (object) sender.GetItemType().Name
        });
    }
    if (e.TranStatus == null)
      return;
    PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
    if (e.TranStatus == 2)
    {
      if (this._KeyToAbort != null)
      {
        object objA = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, this._KeyToAbort);
        foreach (CurrencyInfo currencyInfo in cach.Inserted)
        {
          if (object.Equals(objA, (object) currencyInfo.CuryInfoID))
          {
            cach.RaiseRowPersisted((object) currencyInfo, (PXDBOperation) 2, (PXTranStatus) 2, e.Exception);
            currencyInfo.CuryInfoID = new long?(Convert.ToInt64(this._KeyToAbort));
            cach.ResetPersisted((object) currencyInfo);
          }
        }
      }
      else
      {
        object objA = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
        foreach (CurrencyInfo currencyInfo in cach.Updated)
        {
          if (object.Equals(objA, (object) currencyInfo.CuryInfoID))
            cach.ResetPersisted((object) currencyInfo);
        }
      }
    }
    else
    {
      object objA = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      foreach (CurrencyInfo currencyInfo in cach.Inserted)
      {
        if (object.Equals(objA, (object) currencyInfo.CuryInfoID))
        {
          cach.SetStatus((object) currencyInfo, (PXEntryStatus) 0);
          cach.RaiseRowPersisted((object) currencyInfo, (PXDBOperation) 2, e.TranStatus, e.Exception);
          PXTimeStampScope.PutPersisted(cach, (object) currencyInfo, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted((object) currencyInfo);
        }
      }
      foreach (CurrencyInfo currencyInfo in cach.Updated)
      {
        if (object.Equals(objA, (object) currencyInfo.CuryInfoID))
        {
          cach.SetStatus((object) currencyInfo, (PXEntryStatus) 0);
          cach.RaiseRowPersisted((object) currencyInfo, (PXDBOperation) 1, e.TranStatus, e.Exception);
          PXTimeStampScope.PutPersisted(cach, (object) currencyInfo, new object[1]
          {
            (object) sender.Graph.TimeStamp
          });
          cach.ResetPersisted((object) currencyInfo);
        }
      }
      cach.IsDirty = false;
    }
    this._KeyToAbort = (object) null;
    cach.Normalize();
  }

  protected virtual void curyViewFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = (object) sender.Graph.Accessinfo.CuryViewState;
    if (((PXEventSubscriberAttribute) this)._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(), (object) null, "CuryViewState", (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(true), new bool?(true), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
  }

  public virtual void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!sender.IsDirty)
      this._oldRow = e.Row;
    PXCache cach = sender.Graph.Caches[typeof (CurrencyInfo)];
    object objB;
    if ((objB = sender.GetValue(e.NewRow, ((PXEventSubscriberAttribute) this)._FieldOrdinal)) != null && Convert.ToInt64(objB) < 0L)
    {
      bool flag = false;
      foreach (CurrencyInfo currencyInfo in cach.Inserted)
      {
        if (object.Equals((object) currencyInfo.CuryInfoID, objB))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
        objB = (object) null;
      }
    }
    if (objB != null)
      return;
    sender.SetDefaultExt(e.NewRow, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    if (sender.GetValue(e.NewRow, ((PXEventSubscriberAttribute) this)._FieldOrdinal) != null)
      return;
    CurrencyInfo currencyInfo1 = new CurrencyInfo();
    if (!string.IsNullOrEmpty(this._ModuleCode))
      currencyInfo1.ModuleCode = this._ModuleCode;
    CurrencyInfo currencyInfo2 = (CurrencyInfo) cach.Insert((object) currencyInfo1);
    cach.IsDirty = false;
    if (currencyInfo2 == null)
      return;
    sender.SetValue(e.NewRow, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) currencyInfo2.CuryInfoID);
    if (!this._NeedSync)
      return;
    sender.SetValue(e.NewRow, this._CuryIDField, (object) currencyInfo2.CuryID);
  }

  protected virtual void curyRateFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    bool curyViewState = sender.Graph.Accessinfo.CuryViewState;
    PXView view = sender.Graph.Views[$"_{sender.GetItemType().Name}_CurrencyInfo_{(((PXEventSubscriberAttribute) this)._FieldName == "CuryInfoID" ? "" : ((PXEventSubscriberAttribute) this)._FieldName + "_")}"];
    long? objB = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (objB.HasValue)
    {
      if (this._ParentType != (Type) null)
      {
        long? nullable = objB;
        long num = 0;
        if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
          goto label_5;
      }
      else
        goto label_5;
    }
    object obj;
    sender.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
    if (obj != null)
      objB = (long?) obj;
label_5:
    if (objB.HasValue)
    {
      if (view.Cache.Current is CurrencyInfo currencyInfo3)
      {
        if (!object.Equals((object) currencyInfo3.CuryInfoID, (object) objB))
        {
          if (!(view.Cache.Locate((object) new CurrencyInfo()
          {
            CuryInfoID = objB
          }) is CurrencyInfo currencyInfo3))
            currencyInfo3 = view.SelectSingle(new object[1]
            {
              (object) objB
            }) as CurrencyInfo;
        }
      }
      else if (!(view.Cache.Locate((object) new CurrencyInfo()
      {
        CuryInfoID = objB
      }) is CurrencyInfo currencyInfo3))
        currencyInfo3 = view.SelectSingle(new object[1]
        {
          (object) objB
        }) as CurrencyInfo;
      if (currencyInfo3 != null)
        e.ReturnValue = curyViewState ? (object) 1M : (object) currencyInfo3.SampleCuryRate;
    }
    if (((((PXEventSubscriberAttribute) this)._AttributeLevel == 2 ? 1 : (e.IsAltered ? 1 : 0)) | (curyViewState ? 1 : 0)) == 0)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (Decimal), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(5), (object) null, this._CuryRateField, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(this._Enabled), new bool?(true), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
    if (!curyViewState)
      return;
    ((PXFieldState) e.ReturnState).Enabled = false;
  }

  protected virtual void curyIdFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (sender.Graph.GetType() == typeof (PXGenericInqGrph) || sender.Graph.GetType() == typeof (PXGraph))
      return;
    bool curyViewState = sender.Graph.Accessinfo.CuryViewState;
    PXView view = sender.Graph.Views[$"_{sender.GetItemType().Name}_CurrencyInfo_{(((PXEventSubscriberAttribute) this)._FieldName == "CuryInfoID" ? "" : ((PXEventSubscriberAttribute) this)._FieldName + "_")}"];
    long? nullable = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (!nullable.HasValue && this._ParentType != (Type) null)
    {
      object obj;
      sender.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this)._FieldName, e.Row, ref obj);
      if (obj != null)
        nullable = (long?) obj;
    }
    if (nullable.HasValue)
    {
      if (view.Cache.Current is CurrencyInfo currencyInfo3)
      {
        if (!object.Equals((object) currencyInfo3.CuryInfoID, (object) nullable))
        {
          if (!(view.Cache.Locate((object) new CurrencyInfo()
          {
            CuryInfoID = nullable
          }) is CurrencyInfo currencyInfo3) && (this._persisted == null || !this._persisted.TryGetValue(nullable, out currencyInfo3)))
            currencyInfo3 = view.SelectSingle(new object[1]
            {
              (object) nullable
            }) as CurrencyInfo;
        }
      }
      else if (!(view.Cache.Locate((object) new CurrencyInfo()
      {
        CuryInfoID = nullable
      }) is CurrencyInfo currencyInfo3) && (this._persisted == null || !this._persisted.TryGetValue(nullable, out currencyInfo3)))
        currencyInfo3 = view.SelectSingle(new object[1]
        {
          (object) nullable
        }) as CurrencyInfo;
      if (currencyInfo3 != null)
        e.ReturnValue = curyViewState ? (object) currencyInfo3.BaseCuryID : (object) currencyInfo3.CuryID;
    }
    if (((((PXEventSubscriberAttribute) this)._AttributeLevel == 2 ? 1 : (e.IsAltered ? 1 : 0)) | (curyViewState ? 1 : 0)) == 0)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(-1), new int?(), new int?(5), (object) null, this._CuryIDField, (string) null, this.CuryDisplayName, (string) null, (PXErrorLevel) 0, new bool?(this._Enabled), new bool?(true), new bool?(), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
    if (!curyViewState)
      return;
    ((PXFieldState) e.ReturnState).Enabled = false;
  }

  protected virtual void curyIdFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (sender.Graph.Accessinfo.CuryViewState)
    {
      e.NewValue = sender.GetValue(e.Row, this._CuryIDField);
    }
    else
    {
      PXView view = sender.Graph.Views[$"_{sender.GetItemType().Name}_CurrencyInfo_{(((PXEventSubscriberAttribute) this)._FieldName == "CuryInfoID" ? "" : ((PXEventSubscriberAttribute) this)._FieldName + "_")}"];
      CurrencyInfo info1 = (CurrencyInfo) null;
      long? nullable1 = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      if (nullable1.HasValue)
        info1 = view.SelectSingle(new object[1]
        {
          (object) nullable1
        }) as CurrencyInfo;
      if (info1 == null || object.Equals((object) info1.CuryID, e.NewValue))
        return;
      CurrencyInfo copy1 = PXCache<CurrencyInfo>.CreateCopy(info1);
      long? nullable2 = copy1.CuryInfoID;
      long num = 0;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue && (CurrencyCollection.IsBaseCuryInfo(info1) || CurrencyCollection.IsBaseCuryInfo(info1, (string) e.NewValue)))
      {
        CurrencyInfo copy2 = PXCache<CurrencyInfo>.CreateCopy(info1);
        if (copy2.CuryRateTypeID == null)
          view.Cache.SetDefaultExt<CurrencyInfo.curyRateTypeID>((object) copy2);
        CurrencyInfo currencyInfo = copy2;
        nullable2 = new long?();
        long? nullable3 = nullable2;
        currencyInfo.CuryInfoID = nullable3;
        CurrencyInfo info2 = view.Cache.Insert((object) copy2) as CurrencyInfo;
        sender.SetValueExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) info2.CuryInfoID);
        if (copy1.CuryID == copy1.BaseCuryID)
          view.Cache.Remove((object) copy1);
        else
          view.Cache.SetStatus((object) copy1, (PXEntryStatus) 3);
        this.ValidateCurrencyInfo(view, info2, sender, e);
        view.Cache.RaiseRowUpdated((object) info2, (object) copy1);
      }
      else
      {
        this.ValidateCurrencyInfo(view, info1, sender, e);
        GraphHelper.MarkUpdated(view.Cache, (object) info1);
        view.Cache.RaiseRowUpdated((object) info1, (object) copy1);
      }
    }
  }

  protected virtual void branchIDFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? oldValue = (int?) e.OldValue;
    int? nullable1 = (int?) sender.GetValue(e.Row, this._baseCurySourceBranchIdField.Name);
    int? nullable2 = nullable1;
    int? nullable3 = oldValue;
    if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue || PXAccess.GetBranch(oldValue)?.BaseCuryID == PXAccess.GetBranch(nullable1)?.BaseCuryID)
      return;
    CurrencyInfoAttribute.SetDefaults<CurrencyInfo.curyInfoID>(sender, e.Row);
  }

  protected virtual void baseCuryIdFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.SuppressDefaultBaseCury || e.Row == null || this._baseCurySourceBranchIdField == (Type) null || ((CancelEventArgs) e).Cancel)
      return;
    Type itemType = BqlCommand.GetItemType(this._baseCurySourceBranchIdField);
    PXCache cach = sender.Graph.Caches[itemType];
    if (cach == null || cach.Current == null)
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch((int?) cach.GetValue(cach.Current, this._baseCurySourceBranchIdField.Name));
    if (branch == null || branch.BaseCuryID == null)
      return;
    e.NewValue = (object) branch.BaseCuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  private void ValidateCurrencyInfo(
    PXView view,
    CurrencyInfo info,
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (info.CuryRateTypeID == null)
      view.Cache.SetDefaultExt<CurrencyInfo.curyRateTypeID>((object) info);
    view.Cache.SetValueExt<CurrencyInfo.curyID>((object) info, e.ExternalCall ? (object) new PXCache.ExternalCallMarker(e.NewValue) : e.NewValue);
    view.Cache.SetDefaultExt<CurrencyInfo.curyEffDate>((object) info);
    string error = PXUIFieldAttribute.GetError<CurrencyInfo.curyID>(view.Cache, (object) info);
    if (string.IsNullOrEmpty(error))
      return;
    sender.RaiseExceptionHandling(this._CuryIDField, e.Row, e.NewValue, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
  }

  public static string GetCuryID(PXCache cache, object row, string keyField)
  {
    using (IEnumerator<CurrencyInfoAttribute> enumerator = cache.GetAttributesReadonly(row, keyField).OfType<CurrencyInfoAttribute>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        CurrencyInfoAttribute current = enumerator.Current;
        return current._NeedSync ? (string) cache.GetValue(row, current._CuryIDField) : (string) null;
      }
    }
    return (string) null;
  }

  public static object GetOldRow(PXCache cache, object item)
  {
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(item, (string) null))
    {
      if (attribute is CurrencyInfoAttribute)
        return ((CurrencyInfoAttribute) attribute)._oldRow;
    }
    return (object) null;
  }

  public static CurrencyInfo SetDefaults<Field>(PXCache cache, object item) where Field : IBqlField
  {
    return CurrencyInfoAttribute.SetDefaults<Field>(cache, item, true);
  }

  public static CurrencyInfo SetDefaults<Field>(PXCache cache, object item, bool resetCuryID) where Field : IBqlField
  {
    return CurrencyInfoAttribute.SetDefaults<Field>(cache, item, resetCuryID, cache.GetItemType());
  }

  public static CurrencyInfo SetDefaults<Field>(PXCache cache, object item, Type viewType) where Field : IBqlField
  {
    return CurrencyInfoAttribute.SetDefaults<Field>(cache, item, true, cache.GetItemType());
  }

  public static CurrencyInfo SetDefaults<Field>(
    PXCache cache,
    object item,
    bool resetCuryID,
    Type viewType)
    where Field : IBqlField
  {
    string key = $"_{viewType.Name}_CurrencyInfo_";
    PXView pxView = (PXView) null;
    ((Dictionary<string, PXView>) cache.Graph.Views).TryGetValue(key, out pxView);
    if (pxView == null)
    {
      string str = $"{key}{typeof (Field).Name}_";
      pxView = cache.Graph.Views[str];
    }
    if (pxView.SelectSingle(new object[1]
    {
      cache.GetValue(item, typeof (Field).Name)
    }) is CurrencyInfo info)
    {
      if (!resetCuryID)
      {
        object objB;
        pxView.Cache.RaiseFieldDefaulting<CurrencyInfo.baseCuryID>((object) info, ref objB);
        if (object.Equals((object) info.BaseCuryID, objB))
          return info;
      }
      CurrencyInfo copy = PXCache<CurrencyInfo>.CreateCopy(info);
      long? curyInfoId = info.CuryInfoID;
      long num = 0;
      if (curyInfoId.GetValueOrDefault() > num & curyInfoId.HasValue && CurrencyCollection.IsBaseCuryInfo(info))
      {
        info = PXCache<CurrencyInfo>.CreateCopy(info);
        info.CuryInfoID = new long?();
      }
      pxView.Cache.SetDefaultExt<CurrencyInfo.baseCuryID>((object) info);
      if (resetCuryID)
        pxView.Cache.SetDefaultExt<CurrencyInfo.curyID>((object) info);
      pxView.Cache.SetDefaultExt<CurrencyInfo.curyRateTypeID>((object) info);
      pxView.Cache.SetDefaultExt<CurrencyInfo.curyEffDate>((object) info);
      if (!info.CuryInfoID.HasValue)
      {
        info = (CurrencyInfo) pxView.Cache.Insert((object) info);
        cache.SetValueExt<Field>(item, (object) info.CuryInfoID);
        if (copy.CuryID == copy.BaseCuryID)
          pxView.Cache.Remove((object) copy);
        else
          pxView.Cache.SetStatus((object) copy, (PXEntryStatus) 3);
        pxView.Cache.RaiseRowUpdated((object) info, (object) copy);
      }
      else
      {
        GraphHelper.MarkUpdated(pxView.Cache, (object) info);
        pxView.Cache.RaiseRowUpdated((object) info, (object) copy);
      }
    }
    return info;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (((List<PXEventSubscriberAttribute>) this._Attributes).Count > 0 && (typeof (ISubscriber) == typeof (IPXRowPersistingSubscriber) || typeof (ISubscriber) == typeof (IPXRowPersistedSubscriber)))
      subscribers.Add(this._Attributes[0] as ISubscriber);
    base.GetSubscriber<ISubscriber>(subscribers);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass79_0 cDisplayClass790 = new CurrencyInfoAttribute.\u003C\u003Ec__DisplayClass79_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass790.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass790.sender = sender;
    // ISSUE: reference to a compiler-generated field
    this._ChildType = cDisplayClass790.sender.GetItemType();
    if (this._baseCurySourceBranchIdField == (Type) null)
    {
      // ISSUE: reference to a compiler-generated field
      this._baseCurySourceBranchIdField = cDisplayClass790.sender.GetBqlField("BranchID");
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass790.sender.Graph.Views[$"_{cDisplayClass790.sender.GetItemType().Name}_CurrencyInfo_{(((PXEventSubscriberAttribute) this)._FieldName == "CuryInfoID" ? "" : ((PXEventSubscriberAttribute) this)._FieldName + "_")}"] = (PXView) new CurrencyInfoAttribute.CurrencyInfoView(cDisplayClass790.sender.Graph, this);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass790.sender.Graph.Views.Caches.Count == 0 || cDisplayClass790.sender.Graph.Views.Caches[0] != typeof (CurrencyInfo))
    {
      // ISSUE: reference to a compiler-generated field
      int index = cDisplayClass790.sender.Graph.Views.Caches.IndexOf(typeof (CurrencyInfo));
      if (index > 0)
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass790.sender.Graph.Views.Caches.RemoveAt(index);
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass790.sender.Graph.Views.Caches.Insert(0, typeof (CurrencyInfo));
    }
    // ISSUE: reference to a compiler-generated field
    if (!CompareIgnoreCase.IsInList((List<string>) cDisplayClass790.sender.Fields, this._CuryIDField))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass790.sender.Fields.Add(this._CuryIDField);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass790.sender.GetFieldOrdinal(this._CuryIDField) < 0)
        throw new PXArgumentException("CuryIDField", "Invalid field: '{0}'", new object[1]
        {
          (object) this._CuryIDField
        });
      this._NeedSync = true;
    }
    // ISSUE: reference to a compiler-generated field
    if (!CompareIgnoreCase.IsInList((List<string>) cDisplayClass790.sender.Fields, this._CuryRateField))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass790.sender.Fields.Add(this._CuryRateField);
    }
    // ISSUE: reference to a compiler-generated field
    if (!CompareIgnoreCase.IsInList((List<string>) cDisplayClass790.sender.Fields, "CuryViewState"))
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass790.sender.Fields.Add("CuryViewState");
    }
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldSelectingEvents fieldSelecting1 = cDisplayClass790.sender.Graph.FieldSelecting;
    Type childType1 = this._ChildType;
    string curyRateField = this._CuryRateField;
    CurrencyInfoAttribute currencyInfoAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) currencyInfoAttribute1, __vmethodptr(currencyInfoAttribute1, curyRateFieldSelecting));
    fieldSelecting1.AddHandler(childType1, curyRateField, pxFieldSelecting1);
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldSelectingEvents fieldSelecting2 = cDisplayClass790.sender.Graph.FieldSelecting;
    Type childType2 = this._ChildType;
    string curyIdField1 = this._CuryIDField;
    CurrencyInfoAttribute currencyInfoAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) currencyInfoAttribute2, __vmethodptr(currencyInfoAttribute2, curyIdFieldSelecting));
    fieldSelecting2.AddHandler(childType2, curyIdField1, pxFieldSelecting2);
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldVerifyingEvents fieldVerifying = cDisplayClass790.sender.Graph.FieldVerifying;
    Type childType3 = this._ChildType;
    string curyIdField2 = this._CuryIDField;
    CurrencyInfoAttribute currencyInfoAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) currencyInfoAttribute3, __vmethodptr(currencyInfoAttribute3, curyIdFieldVerifying));
    fieldVerifying.AddHandler(childType3, curyIdField2, pxFieldVerifying);
    // ISSUE: reference to a compiler-generated field
    PXGraph.FieldSelectingEvents fieldSelecting3 = cDisplayClass790.sender.Graph.FieldSelecting;
    Type childType4 = this._ChildType;
    CurrencyInfoAttribute currencyInfoAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting3 = new PXFieldSelecting((object) currencyInfoAttribute4, __vmethodptr(currencyInfoAttribute4, curyViewFieldSelecting));
    fieldSelecting3.AddHandler(childType4, "CuryViewState", pxFieldSelecting3);
    if (this._ParentType == (Type) null && this._baseCurySourceBranchIdField != (Type) null)
    {
      // ISSUE: reference to a compiler-generated field
      PXGraph.FieldDefaultingEvents fieldDefaulting = cDisplayClass790.sender.Graph.FieldDefaulting;
      CurrencyInfoAttribute currencyInfoAttribute5 = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) currencyInfoAttribute5, __vmethodptr(currencyInfoAttribute5, baseCuryIdFieldDefaulting));
      fieldDefaulting.AddHandler<CurrencyInfo.baseCuryID>(pxFieldDefaulting);
    }
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowPersistingEvents rowPersisting = cDisplayClass790.sender.Graph.RowPersisting;
    CurrencyInfoAttribute currencyInfoAttribute6 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) currencyInfoAttribute6, __vmethodptr(currencyInfoAttribute6, CurrencyInfo_RowPersisting));
    rowPersisting.AddHandler<CurrencyInfo>(pxRowPersisting);
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowPersistedEvents rowPersisted = cDisplayClass790.sender.Graph.RowPersisted;
    CurrencyInfoAttribute currencyInfoAttribute7 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) currencyInfoAttribute7, __vmethodptr(currencyInfoAttribute7, CurrencyInfo_RowPersisted));
    rowPersisted.AddHandler<CurrencyInfo>(pxRowPersisted);
    // ISSUE: reference to a compiler-generated field
    PXGraph.RowSelectedEvents rowSelected = cDisplayClass790.sender.Graph.RowSelected;
    CurrencyInfoAttribute currencyInfoAttribute8 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) currencyInfoAttribute8, __vmethodptr(currencyInfoAttribute8, CurrencyInfo_RowSelected));
    rowSelected.AddHandler<CurrencyInfo>(pxRowSelected);
    if (this._ParentChildMode)
    {
      // ISSUE: reference to a compiler-generated field
      PXGraph.ExceptionHandlingEvents exceptionHandling1 = cDisplayClass790.sender.Graph.ExceptionHandling;
      CurrencyInfoAttribute currencyInfoAttribute9 = this;
      // ISSUE: virtual method pointer
      PXExceptionHandling exceptionHandling2 = new PXExceptionHandling((object) currencyInfoAttribute9, __vmethodptr(currencyInfoAttribute9, CurrencyInfo_CuryRate_ExceptionHandling));
      exceptionHandling1.AddHandler<CurrencyInfo.sampleCuryRate>(exceptionHandling2);
    }
    else if (this._NeedSync)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass790.sender.Graph.RowUpdated.AddHandler(this._ParentType, new PXRowUpdated((object) cDisplayClass790, __methodptr(\u003CCacheAttached\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass790.sender.Graph.OnAfterPersist += (Action<PXGraph>) (graph =>
    {
      PXCache cach = graph.Caches[typeof (CurrencyInfo)];
      foreach (CurrencyInfo info in cach.Inserted.ToArray<CurrencyInfo>())
      {
        long? nullable = CurrencyCollection.MatchBaseCuryInfoId(info);
        long? curyInfoId = info.CuryInfoID;
        long num = 0;
        if (curyInfoId.GetValueOrDefault() < num & curyInfoId.HasValue && nullable.HasValue)
        {
          cach.SetStatus((object) info, (PXEntryStatus) 0);
          info.CuryInfoID = nullable;
        }
      }
    });
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass790.sender.Graph.RowInserting.AddHandler<CurrencyInfo>(CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9__79_2 ?? (CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9__79_2 = new PXRowInserting((object) CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCacheAttached\u003Eb__79_2))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass790.sender.Graph.GetType() == typeof (PXGenericInqGrph) || cDisplayClass790.sender.Graph.GetType() == typeof (PXGraph))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass790.sender.Graph.RowSelected.AddHandler<CurrencyInfo>(CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9__79_3 ?? (CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9__79_3 = new PXRowSelected((object) CurrencyInfoAttribute.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCacheAttached\u003Eb__79_3))));
    }
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(cDisplayClass790.sender);
    this._persisted = new Dictionary<long?, CurrencyInfo>();
  }

  private static PXView CreateSiblingsView(PXGraph graph, Type classType, Type keyField)
  {
    BqlCommand bqlCommand = (BqlCommand) null;
    Type primaryType = CurrencyInfoAttribute.GetPrimaryType(graph);
    if (primaryType != (Type) null)
    {
      foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) graph.Views)
      {
        if (view.Value.GetItemType() == classType && !view.Value.IsReadOnly)
        {
          bool flag = false;
          IBqlParameter[] parameters = view.Value.BqlSelect.GetParameters();
          for (int index = 0; index < parameters.Length; ++index)
          {
            if (!parameters[index].IsVisible && parameters[index].HasDefault)
            {
              Type referencedType = parameters[index].GetReferencedType();
              if (referencedType.IsNested && BqlCommand.GetItemType(referencedType) == primaryType)
                flag = true;
            }
            else
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            bqlCommand = view.Value.BqlSelect.WhereAnd(typeof (Where<,>).MakeGenericType(keyField, typeof (Equal<Current<CurrencyInfo.curyInfoID>>)));
            break;
          }
        }
      }
    }
    if (bqlCommand == null)
      bqlCommand = BqlCommand.CreateInstance(new Type[5]
      {
        typeof (Select<,>),
        classType,
        typeof (Where<,>),
        keyField,
        typeof (Equal<Current<CurrencyInfo.curyInfoID>>)
      });
    return new PXView(graph, false, bqlCommand);
  }

  public ISet<Type> GetDependencies(PXCache cache)
  {
    HashSet<Type> dependencies = new HashSet<Type>();
    Type bqlField = cache.GetBqlField(this._CuryIDField);
    if (bqlField != (Type) null)
      dependencies.Add(bqlField);
    return (ISet<Type>) dependencies;
  }

  protected class CurrencyInfoDefaultAttribute(Type sourceType) : CurrencyInfoDBDefaultAttribute(sourceType)
  {
    private long? valueBeforePersisting;

    public virtual void CacheAttached(PXCache sender)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CurrencyInfoAttribute.CurrencyInfoDefaultAttribute.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new CurrencyInfoAttribute.CurrencyInfoDefaultAttribute.\u003C\u003Ec__DisplayClass1_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.sender = sender;
      // ISSUE: reference to a compiler-generated field
      base.CacheAttached(cDisplayClass10.sender);
      this._DoubleDefaultAttribute = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.sourceType = this._SourceType == typeof (CurrencyInfo) ? CurrencyInfoAttribute.GetPrimaryType(cDisplayClass10.sender.Graph) : this._SourceType;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass10.sourceType == (Type) null)
        return;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.cacheType = cDisplayClass10.sender.Graph.Caches[cDisplayClass10.sourceType].GetItemType();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      PXParentAttribute pxParentAttribute = cDisplayClass10.sender.GetAttributesReadonly((string) null).OfType<PXParentAttribute>().FirstOrDefault<PXParentAttribute>(new Func<PXParentAttribute, bool>(cDisplayClass10.\u003CCacheAttached\u003Eb__0));
      if (pxParentAttribute == null)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CurrencyInfoAttribute.CurrencyInfoDefaultAttribute.\u003C\u003Ec__DisplayClass1_1 cDisplayClass11 = new CurrencyInfoAttribute.CurrencyInfoDefaultAttribute.\u003C\u003Ec__DisplayClass1_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass11.CS\u0024\u003C\u003E8__locals1 = cDisplayClass10;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass11.parentType = pxParentAttribute.ParentType;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass11.CS\u0024\u003C\u003E8__locals1.sender.Graph.FieldUpdated.AddHandler(cDisplayClass11.parentType, this._SourceField, new PXFieldUpdated((object) cDisplayClass11, __methodptr(\u003CCacheAttached\u003Eb__1)));
    }

    public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      if (!this.valueBeforePersisting.HasValue)
        this.valueBeforePersisting = (long?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      base.RowPersisting(sender, e);
    }

    public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
    {
      base.RowPersisted(sender, e);
      if (e.TranStatus != 2 || !this.valueBeforePersisting.HasValue)
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) this.valueBeforePersisting);
    }
  }

  /// <summary>
  /// Skip setting <see cref="P:PX.Objects.CM.CurrencyInfo.IsReadOnly" /> in <see cref="M:PX.Objects.CM.CurrencyInfoAttribute.CurrencyInfoView.Select(System.Object[],System.Object[],System.Object[],System.String[],System.Boolean[],PX.Data.PXFilterRow[],System.Int32@,System.Int32,System.Int32@)" />
  /// </summary>
  public class SkipReadOnly : IDisposable
  {
    private CurrencyInfoAttribute.CurrencyInfoView _view;

    public SkipReadOnly(PXView view)
    {
      this._view = view as CurrencyInfoAttribute.CurrencyInfoView;
      if (this._view == null)
        return;
      this._view.skipReadOnly = true;
    }

    void IDisposable.Dispose()
    {
      if (this._view == null)
        return;
      this._view.skipReadOnly = false;
    }
  }

  private sealed class CurrencyInfoView : PXView
  {
    private CurrencyInfoAttribute _Owner;
    private PXView _innerView;
    public bool skipReadOnly;

    public CurrencyInfoView(PXGraph graph, CurrencyInfoAttribute owner)
      : base(graph, false, (BqlCommand) new Select<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Optional<CurrencyInfo.curyInfoID>>>>())
    {
      this._Owner = owner;
      this._innerView = new PXView(graph, false, (BqlCommand) new Select<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Optional<CurrencyInfo.curyInfoID>>>>());
    }

    public virtual List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      searches = (object[]) null;
      PXCache cach = this._Graph.Caches[this._Owner._ChildType];
      if (parameters == null || parameters.Length == 0 || parameters[0] == null)
      {
        object obj = cach.InternalCurrent;
        if (currents != null)
        {
          for (int index = 0; index < currents.Length; ++index)
          {
            if (currents[index] != null && (currents[index].GetType() == this._Owner._ChildType || currents[index].GetType().IsSubclassOf(this._Owner._ChildType)))
            {
              obj = currents[index];
              break;
            }
          }
        }
        parameters = new object[1];
        if (obj != null)
          parameters[0] = cach.GetValue(obj, ((PXEventSubscriberAttribute) this._Owner)._FieldOrdinal);
        if (parameters[0] == null && cach.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this._Owner)._FieldName, (object) null, ref parameters[0]))
          cach.RaiseFieldUpdating(((PXEventSubscriberAttribute) this._Owner)._FieldName, (object) null, ref parameters[0]);
      }
      List<object> objectList = (List<object>) null;
      foreach (CurrencyInfo currencyInfo in this.Cache.Cached)
      {
        if (object.Equals((object) currencyInfo.CuryInfoID, parameters[0]))
        {
          objectList = new List<object>();
          objectList.Add((object) currencyInfo);
          break;
        }
      }
      if (objectList == null)
      {
        if (parameters[0] != null && parameters[0] is long && (long) parameters[0] < 0L && this._Owner._ParentType != (Type) null)
        {
          parameters[0] = (object) null;
          currents = (object[]) null;
          if (cach.RaiseFieldDefaulting(((PXEventSubscriberAttribute) this._Owner)._FieldName, (object) null, ref parameters[0]))
            cach.RaiseFieldUpdating(((PXEventSubscriberAttribute) this._Owner)._FieldName, (object) null, ref parameters[0]);
        }
        objectList = this._innerView.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
        if (parameters[0] != null && objectList.Count == 0)
        {
          long num = long.MinValue;
          if (parameters.Length == 1 && parameters[0] is long)
            num = (long) parameters[0];
          if (num > 0L && PXTransactionScope.IsScoped)
            this._innerView.RemoveCached(new PXCommandKey(new object[1]
            {
              (object) num
            }, true, new bool?()));
        }
      }
      if (objectList.Count > 0)
      {
        CurrencyInfo currencyInfo = (CurrencyInfo) objectList[0];
        currencyInfo.ModuleCode = this._Owner._ModuleCode;
        if (!this.skipReadOnly)
          currencyInfo.IsReadOnly = new bool?(!this.Graph.UnattendedMode && !this._Graph.Caches[this._Owner._ChildType].AllowUpdate);
      }
      return objectList;
    }
  }

  private sealed class SiblingsView(PXGraph graph, Type ClassType, Type KeyField) : PXView(graph, false, CurrencyInfoAttribute.SiblingsView.GetCommand(graph, ClassType, KeyField))
  {
    private static BqlCommand GetCommand(PXGraph graph, Type ClassType, Type KeyField)
    {
      BqlCommand command = (BqlCommand) null;
      Type primaryType = CurrencyInfoAttribute.GetPrimaryType(graph);
      if (KeyField.DeclaringType != ClassType)
      {
        Type type = ClassType.GetNestedType(KeyField.Name, BindingFlags.Public);
        if ((object) type == null)
          type = KeyField;
        KeyField = type;
      }
      if (primaryType != (Type) null)
      {
        foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) graph.Views)
        {
          if (view.Value.GetItemType() == ClassType && !view.Value.IsReadOnly)
          {
            bool flag = false;
            IBqlParameter[] parameters = view.Value.BqlSelect.GetParameters();
            for (int index = 0; index < parameters.Length; ++index)
            {
              if (!parameters[index].IsVisible && parameters[index].HasDefault)
              {
                Type referencedType = parameters[index].GetReferencedType();
                if (referencedType.IsNested && BqlCommand.GetItemType(referencedType) == primaryType)
                  flag = true;
              }
              else
              {
                flag = false;
                break;
              }
            }
            if (flag)
            {
              command = view.Value.BqlSelect.WhereAnd(typeof (Where<,>).MakeGenericType(KeyField, typeof (Equal<Current<CurrencyInfo.curyInfoID>>)));
              break;
            }
          }
        }
      }
      if (command == null)
        command = BqlCommand.CreateInstance(new Type[5]
        {
          typeof (Select<,>),
          ClassType,
          typeof (Where<,>),
          KeyField,
          typeof (Equal<Current<CurrencyInfo.curyInfoID>>)
        });
      return command;
    }
  }
}
