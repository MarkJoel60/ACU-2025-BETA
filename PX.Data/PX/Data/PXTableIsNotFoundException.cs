// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTableIsNotFoundException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

[Serializable]
internal class PXTableIsNotFoundException : PXException
{
  public PXTableIsNotFoundException(string alias)
    : base("A table with the alias {0} cannot be found.", (object) alias)
  {
  }

  public PXTableIsNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
