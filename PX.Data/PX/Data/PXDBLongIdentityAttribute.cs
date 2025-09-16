// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLongIdentityAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps an 8-byte auto-incremented integer DAC field of
/// <tt>int64?</tt> type to the <tt>bigint</tt> database column.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same name. The
/// field value is auto-incremented by the database.</para>
/// <para>A field of this type is typically declared a key field. To do
/// this, set the <tt>IsKey</tt> parameter to <tt>true</tt>.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBLongIdentity(IsKey = true)]
/// public virtual Int64? RecordID { ... }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBLongIdentityAttribute : 
  PXDBFieldAttribute,
  IPXFieldDefaultingSubscriber,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXRowPersistedSubscriber,
  IPXFieldVerifyingSubscriber,
  IPXIdentityColumn
{
  protected long? _KeyToAbort;
  protected PXDBLongIdentityAttribute.LastDefault _MaximumDefaultValue;

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null && this._MaximumDefaultValue.Value < 0L)
    {
      foreach (object row in this._MaximumDefaultValue.Rows)
      {
        if (sender.Locate(row) != null)
        {
          this._MaximumDefaultValue.Rows.Clear();
          ++this._MaximumDefaultValue.Value;
          break;
        }
      }
      e.NewValue = (object) this._MaximumDefaultValue.Value;
      this._MaximumDefaultValue.Rows.Add(e.Row);
    }
    else
    {
      long num1 = (long) int.MinValue;
      foreach (object data in sender.Cached)
      {
        object obj = sender.GetValue(data, this._FieldOrdinal);
        if (obj != null)
        {
          if ((long) obj > 0L)
          {
            foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(data, this._FieldName))
            {
              if (attribute is PXDBLongIdentityAttribute)
              {
                long? keyToAbort1 = ((PXDBLongIdentityAttribute) attribute)._KeyToAbort;
                long num2 = 0;
                if (keyToAbort1.GetValueOrDefault() < num2 & keyToAbort1.HasValue)
                {
                  long? keyToAbort2 = ((PXDBLongIdentityAttribute) attribute)._KeyToAbort;
                  long num3 = num1;
                  if (keyToAbort2.GetValueOrDefault() > num3 & keyToAbort2.HasValue)
                  {
                    num1 = ((PXDBLongIdentityAttribute) attribute)._KeyToAbort.Value;
                    break;
                  }
                  break;
                }
                break;
              }
            }
          }
          else if ((long) obj > num1)
            num1 = (long) obj;
        }
      }
      long num4 = num1 + 1L;
      e.NewValue = (object) num4;
      if (e.Row != null)
      {
        this._MaximumDefaultValue.Value = num4;
        this._MaximumDefaultValue.Rows.Add(e.Row);
      }
    }
    e.Cancel = true;
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Option) == PXDBOperation.Second && !this._IsKey && !e.IsRestriction)
      return;
    this.PrepareFieldName(this._DatabaseFieldName, e);
    e.DataType = PXDbType.BigInt;
    e.DataValue = e.Value;
    e.DataLength = new int?(8);
    e.IsRestriction = true;
  }

  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetInt64(e.Position));
    ++e.Position;
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    long result;
    if (long.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXLongState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new long?(), new long?(), typeof (long));
  }

  public virtual object GetLastInsertedIdentity(object valueFromCache)
  {
    return (object) this.getLastInsertedIdentity();
  }

  private long getLastInsertedIdentity()
  {
    return Convert.ToInt64((object) PXDatabase.SelectIdentity(this._BqlTable, this._FieldName));
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert)
    {
      if (e.TranStatus == PXTranStatus.Open)
      {
        this._KeyToAbort = (long?) sender.GetValue(e.Row, this._FieldOrdinal);
        long? keyToAbort = this._KeyToAbort;
        long num = 0;
        if (keyToAbort.GetValueOrDefault() < num & keyToAbort.HasValue)
        {
          long? nullable1 = new long?(this.getLastInsertedIdentity());
          long? nullable2 = nullable1;
          if ((nullable2.HasValue ? (Decimal) nullable2.GetValueOrDefault() : 0M) == 0M)
          {
            PXDataField[] pxDataFieldArray = new PXDataField[sender.Keys.Count + 1];
            pxDataFieldArray[0] = new PXDataField(this._DatabaseFieldName);
            for (int index = 0; index < sender.Keys.Count; ++index)
            {
              string key = sender.Keys[index];
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              sender.RaiseCommandPreparing(key, e.Row, sender.GetValue(e.Row, key), PXDBOperation.Select, this._BqlTable, out description);
              if (description != null && description.Expr != null && description.IsRestriction)
                pxDataFieldArray[index + 1] = (PXDataField) new PXDataFieldValue(description.Expr, description.DataType, description.DataLength, description.DataValue);
            }
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(this._BqlTable, pxDataFieldArray))
            {
              if (pxDataRecord != null)
                nullable1 = pxDataRecord.GetInt64(0);
            }
          }
          sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable1);
          PXTransactionScope.SendIdentity(this._DatabaseFieldName, BqlCommand.GetTableName(sender.BqlTable), (object) nullable1.GetValueOrDefault());
        }
        else
          this._KeyToAbort = new long?();
      }
      else if (e.TranStatus == PXTranStatus.Aborted && this._KeyToAbort.HasValue)
      {
        sender.SetValue(e.Row, this._FieldOrdinal, (object) this._KeyToAbort);
        this._KeyToAbort = new long?();
      }
    }
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.ClearMaximumDefaultValue();
  }

  public virtual void ClearMaximumDefaultValue()
  {
    this._MaximumDefaultValue.Rows.Clear();
    this._MaximumDefaultValue.Value = 0L;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    long? nullable = (long?) sender.GetValue(e.Row, this._FieldOrdinal);
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) nullable;
  }

  public override void CacheAttached(PXCache sender)
  {
    this._MaximumDefaultValue = new PXDBLongIdentityAttribute.LastDefault();
    sender._Identity = this._FieldName;
    sender._RowId = this._FieldName;
    sender.Graph.OnClear += (PXGraphClearDelegate) ((graph_, option) => this.ClearMaximumDefaultValue());
    base.CacheAttached(sender);
  }

  /// <exclude />
  protected class LastDefault
  {
    public long Value;
    public List<object> Rows = new List<object>();
  }
}
