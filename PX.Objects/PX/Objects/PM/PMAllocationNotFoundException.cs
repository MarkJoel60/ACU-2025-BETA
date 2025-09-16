// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAllocationNotFoundException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.PM;

public class PMAllocationNotFoundException : PMAllocationException
{
  public PMAllocationNotFoundException(string refNbr, string message = "Auto-allocation of Project Transactions failed.")
    : base(message, "Auto-allocation of Project Transactions failed.")
  {
  }

  public PMAllocationNotFoundException(string format, params object[] args)
    : base(format, args)
  {
  }

  protected PMAllocationNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
