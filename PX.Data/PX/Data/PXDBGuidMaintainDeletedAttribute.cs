// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBGuidMaintainDeletedAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// This attribute is equivalent to the PXDBGuidAttribute but doesn't update Guid when restoring deleted record.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBGuidMaintainDeletedAttribute : PXDBGuidAttribute
{
  public PXDBGuidMaintainDeletedAttribute()
    : base()
  {
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Option) == PXDBOperation.Second)
      return;
    base.CommandPreparing(sender, e);
  }
}
