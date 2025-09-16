// Decompiled with JetBrains decompiler
// Type: PX.SM.PXNotSupportedFileTypeException
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
public class PXNotSupportedFileTypeException : PXException
{
  public PXNotSupportedFileTypeException(string message)
    : base(message)
  {
  }

  public PXNotSupportedFileTypeException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
