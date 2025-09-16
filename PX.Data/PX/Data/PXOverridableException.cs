// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOverridableException
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
public class PXOverridableException : PXException
{
  protected string _MapErrorTo;

  /// <summary>The field name to map exception to.</summary>
  public string MapErrorTo
  {
    get => this._MapErrorTo;
    set => this._MapErrorTo = value;
  }

  public PXOverridableException(string message, Exception inner)
    : base(message, inner)
  {
    this._Message = this.MessageNoPrefix;
  }

  public PXOverridableException(Exception inner, string format, params object[] args)
    : base(inner, format, args)
  {
    this._Message = this.MessageNoPrefix;
  }

  public PXOverridableException(string message)
    : base(message)
  {
    this._Message = this.MessageNoPrefix;
  }

  public PXOverridableException(string format, params object[] args)
    : base(format, args)
  {
    this._Message = this.MessageNoPrefix;
  }

  public override string Message
  {
    get => this.MessagePrefix == null ? this._Message : $"{this.MessagePrefix}: {this._Message}";
  }

  public override string MessageNoNumber
  {
    get => this.MessagePrefix != null ? $"{this.MessagePrefix}: {this._Message}" : this._Message;
  }

  public virtual void SetMessage(string message)
  {
    if (this.MessagePrefix != null)
    {
      string str = this.MessagePrefix + ":";
      if (message.IndexOf(str) != -1)
        message = message.Substring(message.IndexOf(str) + str.Length).TrimStart(' ');
    }
    this._Message = PXMessages.Localize(message);
  }

  public PXOverridableException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXOverridableException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXOverridableException>(this, info);
    base.GetObjectData(info, context);
  }
}
