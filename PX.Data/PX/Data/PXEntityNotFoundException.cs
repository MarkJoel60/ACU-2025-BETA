// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEntityNotFoundException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

public class PXEntityNotFoundException : PXException
{
  public PXEntityNotFoundException(string entityId)
    : base("The system cannot find the '{0}' record.", (object) entityId)
  {
  }

  public PXEntityNotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
