// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActionInterruptException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// The exception that interrupts the execution of a workflow action.
/// </summary>
[Serializable]
public class PXActionInterruptException : PXException
{
  public string InterruptReason { get; }

  public PXActionInterruptException(string interruptReason)
    : base("The action has been interrupted due to the following reason: {0}.", (object) interruptReason)
  {
    this.InterruptReason = interruptReason;
  }

  public PXActionInterruptException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
