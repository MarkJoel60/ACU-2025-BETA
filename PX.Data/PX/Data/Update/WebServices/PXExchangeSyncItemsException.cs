// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeSyncItemsException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeSyncItemsException : PXException
{
  public Dictionary<string, List<string>> Errors;

  public PXExchangeSyncItemsException(Dictionary<string, List<string>> errors)
    : base("At least one item has not been processed.")
  {
    this.Errors = errors;
  }

  public PXExchangeSyncItemsException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXExchangeSyncItemsException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXExchangeSyncItemsException>(this, info);
    base.GetObjectData(info, context);
  }
}
