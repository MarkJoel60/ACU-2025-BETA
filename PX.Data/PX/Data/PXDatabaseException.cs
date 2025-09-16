// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDatabaseException : PXOverridableException
{
  protected string _Table;
  private bool _Retry;
  protected object[] _Keys;
  protected PXDbExceptions _ErrorCode;
  protected bool _IsFriendlyMessage;

  public string Table => this._Table;

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

  public PXDbExceptions ErrorCode => this._ErrorCode;

  public bool IsFriendlyMessage
  {
    get => this._IsFriendlyMessage;
    set => this._IsFriendlyMessage = value;
  }

  public PXDatabaseException(
    string table,
    object[] keys,
    PXDbExceptions errCode,
    string message,
    Exception inner)
    : base(message, inner)
  {
    this._Table = table;
    this._Keys = keys;
    this._ErrorCode = errCode;
    if (errCode != PXDbExceptions.Deadlock)
      return;
    this.Retry = true;
  }

  public PXDatabaseException(string table, object[] keys, string message, Exception inner)
    : base(message, inner)
  {
    this._Table = table;
    this._Keys = keys;
    this._ErrorCode = PXDbExceptions.Unknown;
  }

  public PXDatabaseException(string table, object[] keys, PXDbExceptions errCode, string message)
    : base(message)
  {
    this._Table = table;
    this._Keys = keys;
    this._ErrorCode = errCode;
  }

  public PXDatabaseException(string table, object[] keys, string message)
    : base(message)
  {
    this._Table = table;
    this._Keys = keys;
    this._ErrorCode = PXDbExceptions.Unknown;
  }

  public PXDatabaseException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXDatabaseException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXDatabaseException>(this, info);
    base.GetObjectData(info, context);
  }

  internal static PXDatabaseException GenericCriticalException(string message)
  {
    return new PXDatabaseException((string) null, (object[]) null, message)
    {
      Retry = false
    };
  }
}
