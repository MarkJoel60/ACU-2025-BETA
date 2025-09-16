// Decompiled with JetBrains decompiler
// Type: PX.SM.PXZeroSizeFileException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.SM;

[Serializable]
public class PXZeroSizeFileException : PXException
{
  public PXZeroSizeFileException(string message)
    : base(message)
  {
  }

  public PXZeroSizeFileException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
