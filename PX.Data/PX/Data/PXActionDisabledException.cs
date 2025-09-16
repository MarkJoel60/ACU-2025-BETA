// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActionDisabledException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>Derived from PXException</summary>
[Serializable]
public class PXActionDisabledException : PXException
{
  public PXActionDisabledException(string actionName)
    : base("The {0} button is disabled.", (object) actionName)
  {
  }

  public PXActionDisabledException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXActionDisabledException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
