// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBIdentityAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps an auto-incremented integer DAC field of <tt>int?</tt>
/// type to the <tt>int</tt> database column.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</para>
/// <para>The field value is auto-incremented by the attribute.</para>
/// <para>A field with this attribute typically is a key field. To declare a key field,
/// set the <tt>IsKey</tt> parameter to <tt>true</tt>.</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBIdentity(IsKey = true)]
/// [PXUIField(DisplayName = "Contact ID", Visible = false)]
/// public virtual int? ContactID { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBIdentityAttribute : 
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
  protected int? _KeyToAbort;
  protected PXDBIdentityAttribute.LastDefault _MaximumDefaultValue;

  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null && this._MaximumDefaultValue.Value < 0)
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
      int num1 = int.MinValue;
      foreach (object data in sender.Cached)
      {
        object obj = sender.GetValue(data, this._FieldOrdinal);
        if (obj != null)
        {
          if ((int) obj > 0)
          {
            foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(data, this._FieldName))
            {
              if (attribute is PXDBIdentityAttribute)
              {
                int? keyToAbort1 = ((PXDBIdentityAttribute) attribute)._KeyToAbort;
                int num2 = 0;
                if (keyToAbort1.GetValueOrDefault() < num2 & keyToAbort1.HasValue)
                {
                  int? keyToAbort2 = ((PXDBIdentityAttribute) attribute)._KeyToAbort;
                  int num3 = num1;
                  if (keyToAbort2.GetValueOrDefault() > num3 & keyToAbort2.HasValue)
                  {
                    num1 = ((PXDBIdentityAttribute) attribute)._KeyToAbort.Value;
                    break;
                  }
                  break;
                }
                break;
              }
            }
          }
          else if ((int) obj > num1)
            num1 = (int) obj;
        }
      }
      int num4 = num1 + 1;
      e.NewValue = (object) num4;
      if (e.Row != null)
      {
        this._MaximumDefaultValue.Value = num4;
        this._MaximumDefaultValue.Rows.Add(e.Row);
      }
    }
    e.Cancel = true;
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Option) == PXDBOperation.Second && !this._IsKey && !e.IsRestriction || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Update && (!(e.Value is int) || (int) e.Value < 0) && (e.Operation & PXDBOperation.Option) != PXDBOperation.Second)
      return;
    this.PrepareFieldName(this._DatabaseFieldName, e);
    e.DataType = PXDbType.Int;
    e.DataValue = e.Value;
    e.DataLength = new int?(4);
    e.IsRestriction = true;
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetInt32(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    int result;
    if (int.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new int?(), new int?(), (int[]) null, (string[]) null, typeof (int), new int?());
  }

  /// <exclude />
  public virtual object GetLastInsertedIdentity(object valueFromCache)
  {
    return (object) this.getLastInsertedIdentity();
  }

  private int getLastInsertedIdentity()
  {
    return Convert.ToInt32((object) PXDatabase.SelectIdentity(this._BqlTable, this._FieldName));
  }

  /// <exclude />
  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Insert)
    {
      if (e.TranStatus == PXTranStatus.Open)
      {
        if (!this._KeyToAbort.HasValue)
          this._KeyToAbort = (int?) sender.GetValue(e.Row, this._FieldOrdinal);
        int? keyToAbort = this._KeyToAbort;
        int num = 0;
        if (keyToAbort.GetValueOrDefault() < num & keyToAbort.HasValue)
          this.assignIdentityValue(sender, e);
        else
          this._KeyToAbort = new int?();
      }
      else if (e.TranStatus == PXTranStatus.Aborted && this._KeyToAbort.HasValue)
      {
        sender.SetValue(e.Row, this._FieldOrdinal, (object) this._KeyToAbort);
        this._KeyToAbort = new int?();
      }
    }
    if (e.TranStatus != PXTranStatus.Completed)
      return;
    this.ClearMaximumDefaultValue();
  }

  public virtual void ClearMaximumDefaultValue()
  {
    this._MaximumDefaultValue.Rows.Clear();
    this._MaximumDefaultValue.Value = 0;
  }

  protected virtual void assignIdentityValue(PXCache sender, PXRowPersistedEventArgs e)
  {
    int? nullable = new int?(this.getLastInsertedIdentity());
    if ((Decimal) nullable.Value == 0M)
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
          nullable = pxDataRecord.GetInt32(0);
      }
    }
    sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable);
    PXTransactionScope.SendIdentity(this._DatabaseFieldName, BqlCommand.GetTableName(sender.BqlTable), (object) nullable.GetValueOrDefault());
  }

  /// <exclude />
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    int? nullable = (int?) sender.GetValue(e.Row, this._FieldOrdinal);
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) nullable;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this._MaximumDefaultValue = new PXDBIdentityAttribute.LastDefault();
    sender._Identity = this._FieldName;
    sender._RowId = this._FieldName;
    sender.Graph.OnClear += (PXGraphClearDelegate) ((graph_, option) => this.ClearMaximumDefaultValue());
    base.CacheAttached(sender);
  }

  /// <exclude />
  protected class LastDefault
  {
    public int Value;
    public List<object> Rows = new List<object>();
  }
}
