// Decompiled with JetBrains decompiler
// Type: PX.SM.InstancesSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.SM;

internal class InstancesSelector : PXCustomSelectorAttribute
{
  public InstancesSelector()
    : base(typeof (PX.SM.FilterEntity.entity))
  {
  }

  internal IEnumerable GetRecords()
  {
    return (IEnumerable) ((RelationEntities) this._Graph).Instances.Select();
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
