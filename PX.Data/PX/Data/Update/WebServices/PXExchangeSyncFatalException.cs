// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.PXExchangeSyncFatalException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

public class PXExchangeSyncFatalException : PXException
{
  public string Mailbox;
  public string InnerMessage;

  public PXExchangeSyncFatalException(string mailbox, string message)
    : base("An error occurred during the processing of mailbox {0}. Synchronization cannot be completed.", (object) mailbox)
  {
    this.Mailbox = mailbox;
    this.InnerMessage = message;
  }

  public PXExchangeSyncFatalException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXExchangeSyncFatalException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXExchangeSyncFatalException>(this, info);
    base.GetObjectData(info, context);
  }
}
