// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPRequireApprovalAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.EP;

[AttributeUsage(AttributeTargets.Property)]
public sealed class EPRequireApprovalAttribute : PXDBBoolAttribute
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) false);
  }
}
