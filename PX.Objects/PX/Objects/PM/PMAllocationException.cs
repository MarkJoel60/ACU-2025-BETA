// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocationException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.PM;

public class PMAllocationException : PXException
{
  public string RefNbr { get; private set; }

  public PMAllocationException(string refNbr, string message = "Auto-allocation of Project Transactions failed.")
    : base(message)
  {
    this.RefNbr = refNbr;
  }

  public PMAllocationException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PMAllocationException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PMAllocationException>(this, info);
    base.GetObjectData(info, context);
  }
}
