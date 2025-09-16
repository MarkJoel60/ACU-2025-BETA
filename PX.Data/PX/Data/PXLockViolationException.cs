// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLockViolationException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXLockViolationException : PXOverridableException
{
  private const string _keySeparator = ", ";
  private readonly bool _deletedDatabaseRecord;
  private PXDBOperation _Operation;
  private System.Type _Table;
  private bool _Retry;
  private object[] _Keys;
  private object _instanceRow;

  public PXDBOperation Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  public System.Type Table
  {
    get => this._Table;
    set => this._Table = value;
  }

  public bool Retry
  {
    get => this._Retry;
    set => this._Retry = value;
  }

  public object[] Keys
  {
    get => this._Keys;
    set => this._Keys = value;
  }

  public override string Message
  {
    get
    {
      switch (this._Operation)
      {
        case PXDBOperation.Update:
          string str1;
          if (!this._deletedDatabaseRecord)
            str1 = PXMessages.LocalizeFormat("Another process has updated the '{0}' record. Your changes will be lost.", out this._MessagePrefix, (object) this._Table.Name);
          else
            str1 = PXMessages.LocalizeFormatNoPrefixNLA("Cannot update the record. The record {0} ({1}) has been marked as deleted. Your changes will be lost.", (object) this._Table.Name, (object) this.GetKeysString());
          this._Message = str1;
          break;
        case PXDBOperation.Insert:
          if (this._deletedDatabaseRecord)
          {
            string str2 = PXMessages.LocalizeNoPrefix(this._Retry ? "Please try again." : "Your changes will be lost.");
            this._Message = PXMessages.LocalizeFormatNoPrefixNLA("Cannot insert the record. The record {0} ({1}) has been marked as deleted. {2}", (object) this._Table.Name, (object) this.GetKeysString(), (object) str2);
            break;
          }
          this._Message = PXMessages.LocalizeFormat("Another process has added the '{0}' record. {1}", out this._MessagePrefix, (object) this._Table.Name, this._Retry ? (object) "Please try again." : (object) "Your changes will be lost.");
          break;
        case PXDBOperation.Delete:
          string str3;
          if (!this._deletedDatabaseRecord)
            str3 = PXMessages.LocalizeFormat("Another process has deleted the '{0}' record. Your changes will be lost.", out this._MessagePrefix, (object) this._Table.Name);
          else
            str3 = PXMessages.LocalizeFormatNoPrefixNLA("Cannot delete the record. The record {0} ({1}) has been already deleted. Your changes will be lost.", (object) this._Table.Name, (object) this.GetKeysString());
          this._Message = str3;
          break;
      }
      return base.Message;
    }
  }

  public PXLockViolationException(
    System.Type table,
    PXDBOperation operation,
    object[] keys,
    bool deletedDatabaseRecord)
    : this(table, operation, keys, deletedDatabaseRecord, (Exception) null)
  {
  }

  public PXLockViolationException(
    System.Type table,
    PXDBOperation operation,
    object instanceRow,
    object[] keys,
    bool deletedDatabaseRecord)
    : this(table, operation, instanceRow, keys, deletedDatabaseRecord, (Exception) null)
  {
  }

  public PXLockViolationException(System.Type table, PXDBOperation operation, object[] keys)
    : this(table, operation, keys, false)
  {
  }

  public PXLockViolationException(
    System.Type table,
    PXDBOperation operation,
    object[] keys,
    Exception inner)
    : this(table, operation, keys, false, inner)
  {
  }

  public PXLockViolationException(
    System.Type table,
    PXDBOperation operation,
    object[] keys,
    bool deletedDatabaseRecord,
    Exception inner)
    : this(table, operation, (object) null, keys, deletedDatabaseRecord, inner)
  {
  }

  public PXLockViolationException(
    System.Type table,
    PXDBOperation operation,
    object instanceRow,
    object[] keys,
    bool deletedDatabaseRecord,
    Exception inner)
    : base("", inner)
  {
    this._Table = table;
    this._Operation = operation;
    this._instanceRow = instanceRow;
    this._Keys = keys;
    this._deletedDatabaseRecord = deletedDatabaseRecord;
  }

  public PXLockViolationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXLockViolationException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXLockViolationException>(this, info);
    base.GetObjectData(info, context);
  }

  private string GetKeysString()
  {
    if (this._Keys == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this._Keys.Length; ++index)
    {
      object key = this._Keys[index];
      if (key != null)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        string str = key.ToString().Trim();
        stringBuilder.Append(str);
      }
    }
    return stringBuilder.ToString();
  }

  public bool IsSameEntity(object anotherRow)
  {
    return this._instanceRow == null || this._instanceRow == anotherRow;
  }
}
