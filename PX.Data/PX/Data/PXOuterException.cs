// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOuterException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.DacDescriptorGeneration;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXOuterException : PXException, IExceptionWithDescriptor
{
  protected Dictionary<string, string> _InnerExceptions;
  protected System.Type _GraphType;
  protected object _Row;

  public virtual string[] InnerMessages
  {
    get
    {
      string[] array = new string[this._InnerExceptions.Count];
      this._InnerExceptions.Values.CopyTo(array, 0);
      return array;
    }
  }

  internal virtual string GetErrorOfField(string name)
  {
    string str;
    return !this._InnerExceptions.TryGetValue(name, out str) ? (string) null : str;
  }

  public virtual string[] InnerFields
  {
    get
    {
      string[] array = new string[this._InnerExceptions.Count];
      this._InnerExceptions.Keys.CopyTo(array, 0);
      return array;
    }
  }

  public virtual void InnerRemove(string fieldName) => this._InnerExceptions.Remove(fieldName);

  public virtual System.Type GraphType => this._GraphType;

  public virtual object Row => this._Row;

  public PXOuterException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor,
    Dictionary<string, string> innerExceptions,
    System.Type graphType,
    object row,
    string message)
    : this(innerExceptions, graphType, row, message)
  {
    this.DacDescriptor = descriptor;
  }

  public PXOuterException(
    Dictionary<string, string> innerExceptions,
    System.Type graphType,
    object row,
    string message)
    : base(message)
  {
    this._InnerExceptions = innerExceptions;
    this._GraphType = graphType;
    this._Row = row;
  }

  public PXOuterException(
    PX.Data.DacDescriptorGeneration.DacDescriptor? descriptor,
    Dictionary<string, string> innerExceptions,
    System.Type graphType,
    object row,
    string format,
    params object[] args)
    : this(innerExceptions, graphType, row, format, args)
  {
    this.DacDescriptor = descriptor;
  }

  public PXOuterException(
    Dictionary<string, string> innerExceptions,
    System.Type graphType,
    object row,
    string format,
    params object[] args)
    : base(format, args)
  {
    this._InnerExceptions = innerExceptions;
    this._GraphType = graphType;
    this._Row = row;
  }

  public PXOuterException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXOuterException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXOuterException>(this, info);
    base.GetObjectData(info, context);
  }
}
