// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVirtualSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Suppress GUI selector, used in formula.</summary>
public class PXVirtualSelectorAttribute : PXSelectorAttribute
{
  /// <summary>Creates an virtual selector</summary>
  /// <param name="type">Referenced table. Should be either IBqlField or IBqlSearch</param>
  public PXVirtualSelectorAttribute(System.Type type)
    : base(type)
  {
    this.ValidateValue = false;
  }

  public override bool ExcludeFromReferenceGeneratingProcess { get; set; } = true;

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXFieldState returnState) || !(returnState.ViewName != this._ViewName))
      return;
    returnState.ViewName = (string) null;
  }
}
