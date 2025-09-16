// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUnboundKeyAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Marks the property as a key one.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class PXUnboundKeyAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  /// <exclude />
  public override void CacheAttached(PXCache sender) => sender.Keys.Add(this.FieldName);

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(true), fieldName: this._FieldName);
  }
}
