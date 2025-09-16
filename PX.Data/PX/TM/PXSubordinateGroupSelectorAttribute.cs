// Decompiled with JetBrains decompiler
// Type: PX.TM.PXSubordinateGroupSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows show work groups which are subordinated or include coworkers for current logined employee.
/// </summary>
/// <example>[PXSubordinateGroupSelector]</example>
public class PXSubordinateGroupSelectorAttribute : PXSelectorAttribute
{
  public PXSubordinateGroupSelectorAttribute()
    : base(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>))
  {
    this.SubstituteKey = typeof (EPCompanyTree.description);
    this.DescriptionField = typeof (EPCompanyTree.description);
  }

  public override void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (this.MacroVariablesManager != null)
      e.NewValue = this.MacroVariablesManager.TryResolveExt(e.NewValue, sender, this.FieldName, e.Row);
    EPCompanyTree epCompanyTree = (EPCompanyTree) PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, Where<EPCompanyTree.description, Equal<Required<EPCompanyTree.description>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, e.NewValue);
    if (epCompanyTree != null)
    {
      e.NewValue = (object) epCompanyTree.WorkGroupID;
      e.Cancel = true;
    }
    else
      base.SubstituteKeyFieldUpdating(sender, e);
  }

  public override void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    EPCompanyTree epCompanyTree = (EPCompanyTree) PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, Where<EPCompanyTree.workGroupID, Equal<Required<EPCompanyTree.workGroupID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, e.ReturnValue);
    if (epCompanyTree != null)
    {
      e.ReturnValue = (object) epCompanyTree.Description;
      string displayName = PXUIFieldAttribute.GetDisplayName(sender, this.FieldName);
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), fieldName: this._FieldName, descriptionName: this._DescriptionField != (System.Type) null ? this._DescriptionField.Name : (string) null, displayName: displayName);
      e.Cancel = true;
    }
    else
      base.SubstituteKeyFieldSelecting(sender, e);
  }
}
