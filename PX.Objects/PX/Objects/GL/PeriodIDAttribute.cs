// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PeriodIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Abstractions.Periods;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

public class PeriodIDAttribute : 
  PXAggregateAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowSelectingSubscriber
{
  protected Type _sourceType;
  protected Type _sourceFieldType;
  protected Type _searchType;
  protected string _sourceField;
  protected DateTime _sourceDate;
  protected Type _defaultType;
  protected bool _IsDBField = true;

  public Type SourceFieldType
  {
    get => this._sourceFieldType;
    set
    {
      this._sourceFieldType = value;
      this._sourceType = BqlCommand.GetItemType(this._sourceFieldType);
      this._sourceField = this._sourceFieldType.Name;
    }
  }

  public Type SourceType
  {
    get => this._sourceType;
    set => this._sourceType = value;
  }

  public virtual Type SearchType
  {
    get => this._searchType;
    set => this._searchType = value;
  }

  public virtual Type DefaultType
  {
    get => this._defaultType;
    set => this._defaultType = value;
  }

  public bool IsDBField
  {
    get => this._IsDBField;
    set => this._IsDBField = value;
  }

  public bool RedefaultOnDateChanged { get; set; }

  public bool IsKey
  {
    get => ((PXDBFieldAttribute) this._Attributes[0]).IsKey;
    set => ((PXDBFieldAttribute) this._Attributes[0]).IsKey = value;
  }

  public string FieldName
  {
    get => this._Attributes[0].FieldName;
    set => this._Attributes[0].FieldName = value;
  }

  public Type BqlField
  {
    get => ((PXDBFieldAttribute) this._Attributes[0]).BqlField;
    set
    {
      ((PXDBFieldAttribute) this._Attributes[0]).BqlField = value;
      ((PXEventSubscriberAttribute) this).BqlTable = this._Attributes[0].BqlTable;
    }
  }

  public string DisplayMask
  {
    get
    {
      return ((IEnumerable) this._Attributes).OfType<FinPeriodIDFormattingAttribute>().FirstOrDefault<FinPeriodIDFormattingAttribute>()?.DisplayMask;
    }
    set
    {
      FinPeriodIDFormattingAttribute formattingAttribute = ((IEnumerable) this._Attributes).OfType<FinPeriodIDFormattingAttribute>().FirstOrDefault<FinPeriodIDFormattingAttribute>();
      if (formattingAttribute == null)
        return;
      formattingAttribute.DisplayMask = value;
    }
  }

  public PeriodIDAttribute(
    Type sourceType = null,
    Type searchType = null,
    Type defaultType = null,
    bool redefaultOnDateChanged = true)
  {
    this.RedefaultOnDateChanged = redefaultOnDateChanged;
    if (sourceType != (Type) null)
    {
      this._sourceType = BqlCommand.GetItemType(sourceType);
      this.SearchType = searchType;
      this.SourceFieldType = sourceType;
    }
    Type type = defaultType;
    if ((object) type == null)
      type = searchType;
    this.DefaultType = type;
    PXAggregateAttribute.AggregatedAttributesCollection attributesCollection = new PXAggregateAttribute.AggregatedAttributesCollection();
    attributesCollection.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(6)
    {
      IsFixed = true
    });
    attributesCollection.Add((PXEventSubscriberAttribute) new FinPeriodIDFormattingAttribute());
    this._Attributes = attributesCollection;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscibers) where ISubscriber : class
  {
    if (typeof (ISubscriber) == typeof (IPXCommandPreparingSubscriber) || typeof (ISubscriber) == typeof (IPXRowSelectingSubscriber))
    {
      if (!this._IsDBField)
      {
        subscibers.Add(this as ISubscriber);
      }
      else
      {
        base.GetSubscriber<ISubscriber>(subscibers);
        subscibers.Remove(this as ISubscriber);
      }
    }
    else
      base.GetSubscriber<ISubscriber>(subscibers);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (!(this.SourceType != (Type) null))
      return;
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this.SourceFieldType)];
    if (!cach.GetItemType().IsAssignableFrom(sender.GetItemType()) && !sender.GetItemType().IsAssignableFrom(cach.GetItemType()))
      return;
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(this._sourceType, this._sourceField, new PXFieldUpdated((object) this, __methodptr(DateSourceFieldUpdated)));
  }

  protected virtual Type GetExecutableDefaultType() => this.DefaultType;

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    e.Expr = (SQLExpression) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
  }

  protected void DateSourceFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!this.RedefaultOnDateChanged)
      return;
    this.RedefaultPeriodID(cache, e.Row);
  }

  protected virtual DateTime? GetSourceDate(PXCache cache, object row)
  {
    if (this.SourceFieldType == (Type) null)
      return new DateTime?();
    PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(this.SourceFieldType)];
    return !cach.GetItemType().IsAssignableFrom(row.GetType()) ? (DateTime?) cach.GetValue(cach.Current, this._sourceField) : (DateTime?) cach.GetValue(row, this._sourceField);
  }

  protected virtual bool IsSourcesValuesDefined(PXCache cache, object row)
  {
    return this.SourceFieldType == (Type) null || this.GetSourceDate(cache, row).HasValue;
  }

  protected virtual OrganizationDependedPeriodKey GetPeriodKey(PXCache cache, object row)
  {
    return (OrganizationDependedPeriodKey) null;
  }

  protected virtual void SetPeriodIDBySources(PXCache cache, object row)
  {
    if (this.IsSourcesValuesDefined(cache, row))
    {
      DateTime? sourceDate = this.GetSourceDate(cache, row);
      PeriodIDAttribute.PeriodResult period = this.GetPeriod(cache.Graph, this.GetExecutableDefaultType(), sourceDate, this.GetPeriodKey(cache, row), row.SingleToListOrNull<object>());
      bool flag = false;
      try
      {
        object valuePending = cache.GetValuePending(row, ((PXEventSubscriberAttribute) this)._FieldName);
        if (valuePending == null || valuePending == PXCache.NotSetValue)
          return;
        object obj1 = (object) PeriodIDAttribute.UnFormatPeriod((string) valuePending);
        if (!cache.Graph.UnattendedMode)
        {
          object copy = cache.CreateCopy(row);
          cache.RaiseFieldVerifying(((PXEventSubscriberAttribute) this)._FieldName, row, ref obj1);
          object obj2 = (object) PeriodIDAttribute.UnFormatPeriod((string) valuePending);
          cache.SetValue(copy, ((PXEventSubscriberAttribute) this)._FieldName, obj2);
          cache.RaiseRowUpdating(row, copy);
          if (cache.GetAttributesReadonly(copy, ((PXEventSubscriberAttribute) this)._FieldName).OfType<IPXInterfaceField>().Any<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (a => a.ErrorLevel == 4 || a.ErrorLevel == 5)))
            cache.SetValuePending(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) period.PeriodIDForDisplay);
        }
        flag = true;
      }
      catch (PXSetPropertyException ex)
      {
        cache.SetValuePending(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) period.PeriodIDForDisplay);
      }
      finally
      {
        if (cache.HasAttributes(row))
          cache.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, row, (object) null, (Exception) null);
        if (!flag)
        {
          DateTime? startDate = period.StartDate;
          DateTime? nullable = sourceDate;
          if ((startDate.HasValue & nullable.HasValue ? (startDate.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            cache.SetValueExt(row, this._sourceField, (object) period.StartDate);
            goto label_14;
          }
        }
        cache.SetDefaultExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) period.PeriodIDForDisplay);
label_14:;
      }
    }
    else
      cache.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }

  protected virtual void RedefaultPeriodID(PXCache cache, object row)
  {
    this.SetPeriodIDBySources(cache, row);
  }

  protected virtual PeriodIDAttribute.PeriodResult GetPeriod(
    PXGraph graph,
    Type searchType,
    DateTime? date,
    OrganizationDependedPeriodKey periodKey,
    List<object> currents = null)
  {
    return new PeriodIDAttribute.PeriodResult((IPeriod) graph.GetService<IFinPeriodRepository>().FindFinPeriodByDate(date, (int?) periodKey?.OrganizationID));
  }

  public virtual void GetFields(PXCache sender, object row)
  {
    this._sourceDate = DateTime.MinValue;
    if (!(this._sourceType != (Type) null))
      return;
    this._sourceDate = (DateTime) (PXView.FieldGetValue(sender, row, this._sourceType, this._sourceField) ?? (object) DateTime.MinValue);
  }

  public virtual DateTime GetDate(PXCache sender, object row)
  {
    this.GetFields(sender, row);
    return this._sourceDate;
  }

  public static string FormatPeriod(string period) => PeriodIDAttribute.FormatForDisplay(period);

  /// <summary>
  /// Format Period to string that can be used in an error message.
  /// </summary>
  public static string FormatForError(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForError(period);
  }

  /// <summary>
  /// Format Period to string that can be displayed in the control.
  /// </summary>
  /// <param name="period">Period in database format</param>
  public static string FormatForDisplay(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForDisplay(period);
  }

  /// <summary>Format period to database format</summary>
  /// <param name="period">Period in display format</param>
  /// <returns></returns>
  public static string UnFormatPeriod(string period)
  {
    return FinPeriodIDFormattingAttribute.FormatForStoring(period);
  }

  public class PeriodResult
  {
    public string PeriodID { get; }

    public DateTime? StartDate { get; }

    public DateTime? EndDate { get; }

    public string PeriodIDForDisplay
    {
      get
      {
        string periodId = this.PeriodID;
        return (periodId != null ? (periodId.Length == 6 ? 1 : 0) : 0) == 0 ? this.PeriodID : FinPeriodIDFormattingAttribute.FormatForDisplay(this.PeriodID);
      }
    }

    public PeriodResult(IPeriod period)
    {
      this.PeriodID = period?.FinPeriodID;
      this.StartDate = (DateTime?) period?.StartDate;
      this.EndDate = (DateTime?) period?.EndDate;
    }
  }

  public class QueryParams : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate]
    public DateTime? SourceDate { get; set; }

    public abstract class sourceDate : IBqlField, IBqlOperand
    {
    }
  }
}
