// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNoUpdateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Property)]
public class PXNoUpdateAttribute : PXEventSubscriberAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.FieldVerifyingEvents[this._FieldName.ToLower()] += (PXFieldVerifying) ((cache, e) =>
    {
      if (e.Row == null || !e.ExternalCall)
        return;
      e.NewValue = cache.GetValue(e.Row, this._FieldOrdinal);
      e.Cancel = true;
    });
  }
}
