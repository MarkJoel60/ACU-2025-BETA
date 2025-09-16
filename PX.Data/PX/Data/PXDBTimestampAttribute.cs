// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBTimestampAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>byte[]</tt> type to the database
/// column of <tt>timestamp</tt> type.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>The attribute binds the field to a timestamp column in the
/// database. The database timestamp is a counter that is incremented for
/// each insert or update operation performed on a table with a
/// <tt>timestamp</tt> column. The counter tracks a relative time within a
/// database (not an actual time that can be associated with a clock). You
/// can use the <tt>timestamp</tt> column of a data record to easily
/// determine whether any value in the data record has changed since the
/// last time it was read.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBTimestamp(VerifyTimestamp = VerifyTimestampOptions.BothFromGraphAndRecord)]
/// public virtual byte[] tstamp { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBTimestampAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  /// <exclude />
  [Obsolete("The property is obsolete. Please use VerifyTimestamp property instead.")]
  public virtual bool RecordComesFirst
  {
    get => this.VerifyTimestamp == VerifyTimestampOptions.FromRecord;
    set
    {
      this.VerifyTimestamp = value ? VerifyTimestampOptions.FromRecord : VerifyTimestampOptions.FromGraph;
    }
  }

  /// <summary>Timestamp verification settings</summary>
  public virtual VerifyTimestampOptions VerifyTimestamp { get; set; }

  /// <summary>
  /// Forbids the <see cref="M:PX.Data.PXCache.Update(System.Object)" /> and <see cref="M:PX.Data.PXCache.Delete(System.Object)" /> operations for records
  /// that have already been persisted via the <see cref="M:PX.Data.PXCache.Persist(System.Object,PX.Data.PXDBOperation)" /> operation in scope of a single <see cref="M:PX.Data.PXGraph.Persist" /> call.
  /// This option helps to track changes that would not go to the database, and thus would be lost and may lead to data inconsistency.
  /// The validation can be suppressed in a limited scope by the call of the <see cref="M:PX.Data.PXDBTimestampAttribute.AllowChangesOfPersistedRecordsScopeFor(PX.Data.PXCache)" /> method.
  /// </summary>
  public bool ForbidChangesOfPersistedRecords { get; set; }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    try
    {
      e.NewValue = (object) Convert.FromBase64String((string) e.NewValue);
    }
    catch
    {
    }
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue is byte[])
      e.ReturnValue = (object) Convert.ToBase64String((byte[]) e.ReturnValue);
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(false), this._FieldName, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    ((PXFieldState) e.ReturnState).Visible = false;
    ((PXFieldState) e.ReturnState).Enabled = false;
    ((PXFieldState) e.ReturnState).Visibility = PXUIVisibility.Invisible;
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Option) == PXDBOperation.Second)
      return;
    object obj = (object) null;
    bool flag = (e.Operation & PXDBOperation.Delete) == PXDBOperation.Select;
    if (!flag)
    {
      int num = EnumerableExtensions.IsIn<VerifyTimestampOptions>(this.VerifyTimestamp, VerifyTimestampOptions.FromRecord, VerifyTimestampOptions.BothFromGraphAndRecord) ? 1 : (PXTimeStampScope.GetRecordComesFirst(sender.GetItemType()) ? 1 : 0);
      byte[] second = sender.Graph.TimeStamp;
      if (sender.Graph._primaryRecordTimeStamp != null && sender.Graph.PrimaryItemType != (System.Type) null && sender.Graph.Caches[sender.Graph.PrimaryItemType] == sender)
        second = sender.Graph._primaryRecordTimeStamp;
      obj = (object) second;
      if (num != 0)
      {
        obj = !(e.Value is byte[] first1) || second == null || sender.Graph.UnattendedMode || this.VerifyTimestamp != VerifyTimestampOptions.BothFromGraphAndRecord ? (object) (first1 ?? second) : (e.SqlDialect.CompareTimestamps(first1, second) > 0 ? (object) second : (object) first1);
      }
      else
      {
        byte[] first2 = (byte[]) null;
        object[] persisted = PXTimeStampScope.GetPersisted(sender, e.Row);
        if (persisted != null && persisted.Length != 0)
          first2 = persisted[0] as byte[];
        if (first2 != null)
          obj = e.SqlDialect.CompareTimestamps(first2, second) > 0 ? (object) first2 : (object) second;
      }
    }
    if (obj == null)
    {
      if (e.Value is string s)
      {
        try
        {
          obj = (object) Convert.FromBase64String(s);
        }
        catch
        {
        }
      }
      else
        obj = e.Value;
    }
    if (!(obj != null | flag))
      throw new PXDBTimestampAttribute.PXTimeStampEmptyException(this._FieldName);
    this.PrepareFieldName(this._DatabaseFieldName, e);
    e.DataType = PXDbType.Timestamp;
    e.DataValue = obj;
    e.DataLength = new int?(8);
    e.IsRestriction = e.IsRestriction || !flag;
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      byte[] timeStamp = e.Record.GetTimeStamp(e.Position);
      sender.SetValue(e.Row, this._FieldOrdinal, (object) timeStamp);
    }
    ++e.Position;
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    byte[] timeStamp;
    if (e.TranStatus != PXTranStatus.Completed || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete || (timeStamp = sender.Graph.TimeStamp) == null)
      return;
    if (!(sender.GetValue(e.Row, this._FieldOrdinal) is byte[] second))
    {
      sender.SetValue(e.Row, this._FieldOrdinal, (object) timeStamp);
      PXTimeStampScope.PutPersisted(sender, e.Row, (object) timeStamp);
    }
    else
    {
      bool flag = false;
      if (sender.Graph.SqlDialect.CompareTimestamps(timeStamp, second) > 0)
      {
        sender.SetValue(e.Row, this._FieldOrdinal, (object) timeStamp);
        PXTimeStampScope.PutPersisted(sender, e.Row, (object) timeStamp);
        flag = true;
      }
      if (flag)
        return;
      PXTimeStampScope.PutPersisted(sender, e.Row, (object) second);
    }
  }

  /// <summary>
  /// If the <see cref="P:PX.Data.PXDBTimestampAttribute.ForbidChangesOfPersistedRecords" /> validation is turned on, it can be suppressed by this scope.
  /// </summary>
  public static IDisposable AllowChangesOfPersistedRecordsScopeFor(PXCache cache)
  {
    ExceptionExtensions.ThrowOnNull<PXCache>(cache, nameof (cache), (string) null);
    return (IDisposable) new PXDBTimestampAttribute.AllowChangesAfterPersistScope(cache);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender._Timestamp = this._FieldName;
    if (!this.ForbidChangesOfPersistedRecords)
      return;
    sender._ForbidChangesAfterPersist = true;
  }

  /// <exclude />
  public static string ToString(byte[] tstamp) => PXSqlDatabaseProvider.TimestampToString(tstamp);

  /// <exclude />
  public class PXTimeStampEmptyException(string field) : PXCommandPreparingException(field, (object) null, "'{0}' cannot be empty.", (object) field)
  {
  }

  private class AllowChangesAfterPersistScope : IDisposable
  {
    private readonly PXCache _cache;
    private readonly bool _prevValue;

    public AllowChangesAfterPersistScope(PXCache cache)
    {
      this._cache = cache;
      this._prevValue = this._cache._ForbidChangesAfterPersist;
      this._cache._ForbidChangesAfterPersist = false;
    }

    void IDisposable.Dispose() => this._cache._ForbidChangesAfterPersist = this._prevValue;
  }
}
