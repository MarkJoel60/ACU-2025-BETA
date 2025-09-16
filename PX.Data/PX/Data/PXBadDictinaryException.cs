// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBadDictinaryException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>
/// Derived from PXException. The exception that is thrown when one of the arguments provided to a method is not valid.
/// </summary>
[Serializable]
internal class PXBadDictinaryException : PXException
{
  public PXBadDictinaryException()
    : base("The key cannot be updated at this time. Try to save your previous changes first.")
  {
  }

  public PXBadDictinaryException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
