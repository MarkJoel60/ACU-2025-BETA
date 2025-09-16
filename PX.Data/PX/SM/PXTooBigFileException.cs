// Decompiled with JetBrains decompiler
// Type: PX.SM.PXTooBigFileException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.SM;

/// <exclude />
[Serializable]
public class PXTooBigFileException : PXSetPropertyException
{
  public PXTooBigFileException(string message)
    : base(message)
  {
  }

  public PXTooBigFileException(string format, params object[] args)
    : base(format, args)
  {
  }

  public PXTooBigFileException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
