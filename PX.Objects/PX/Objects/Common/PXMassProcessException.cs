// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXMassProcessException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.Common;

public class PXMassProcessException : PXException
{
  protected Exception _InnerException;
  protected int _ListIndex;

  public int ListIndex => this._ListIndex;

  public PXMassProcessException(int ListIndex, Exception InnerException)
    : base(InnerException is PXOuterException ? $"{InnerException.Message}\r\n{string.Join("\r\n", ((PXOuterException) InnerException).InnerMessages)}" : InnerException.Message, InnerException)
  {
    this._ListIndex = ListIndex;
  }

  public PXMassProcessException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXMassProcessException>(this, info);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXMassProcessException>(this, info);
    base.GetObjectData(info, context);
  }
}
