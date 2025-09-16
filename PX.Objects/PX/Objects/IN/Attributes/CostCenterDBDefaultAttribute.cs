// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Attributes.CostCenterDBDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN.Attributes;

/// <summary>
/// Special attribute for synchronization of the links to <see cref="T:PX.Objects.IN.INCostCenter" /> in dependent tables.
/// Derived from <see cref="T:PX.Data.PXDBDefaultAttribute" /> but suppresses the FieldDefaulting logic,
/// takes <see cref="F:PX.Objects.IN.CostCenter.FreeStock" /> as a default value.
/// </summary>
public class CostCenterDBDefaultAttribute : PXDBDefaultAttribute
{
  public CostCenterDBDefaultAttribute()
    : base(typeof (INCostCenter.costCenterID))
  {
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) 0;
    ((CancelEventArgs) e).Cancel = true;
  }
}
