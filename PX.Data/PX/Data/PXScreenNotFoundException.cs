// Decompiled with JetBrains decompiler
// Type: PX.Data.PXScreenNotFoundException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

internal class PXScreenNotFoundException : PXException
{
  public PXScreenNotFoundException(string screenID)
    : base("The screen ID {0} is not found in the system.", (object) screenID)
  {
  }

  public PXScreenNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
