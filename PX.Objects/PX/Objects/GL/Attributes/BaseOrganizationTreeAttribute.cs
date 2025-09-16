// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.BaseOrganizationTreeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.DAC;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL.Attributes;

public class BaseOrganizationTreeAttribute : PXEntityAttribute
{
  public System.Type SourceOrganizationID;
  public System.Type SourceBranchID;
  protected BaseOrganizationTreeAttribute.TreeSelectorAttribute _attr;

  public BaseOrganizationTreeAttribute.SelectionModes SelectionMode
  {
    get
    {
      return (BaseOrganizationTreeAttribute.SelectionModes) Enum.Parse(typeof (BaseOrganizationTreeAttribute.SelectionModes), this._attr.SelectionMode, true);
    }
    set
    {
      this._attr.SelectionMode = Enum.GetName(typeof (BaseOrganizationTreeAttribute.SelectionModes), (object) value)?.ToLower();
    }
  }

  protected BaseOrganizationTreeAttribute(
    System.Type treeDataMember = null,
    bool onlyActive = true,
    bool nullable = true,
    bool branchMode = false)
  {
    this._attr = new BaseOrganizationTreeAttribute.TreeSelectorAttribute(treeDataMember)
    {
      OnlyActive = onlyActive,
      Nullable = nullable,
      BranchMode = branchMode
    };
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) this._attr);
  }

  public enum SelectionModes
  {
    All,
    Branches,
    Organizations,
  }

  protected class TreeSelectorAttribute : PXSelectorAttribute
  {
    public bool OnlyActive = true;
    public bool Nullable = true;
    public bool BranchMode;
    public string SelectionMode;
    protected readonly System.Type TreeDataMemberType;

    public TreeSelectorAttribute(System.Type treeDataMemberType = null)
      : this(typeof (Search3<BranchItem.bAccountID, OrderBy<Asc<BranchItem.acctCD>>>), treeDataMemberType, typeof (BranchItem.acctCD), typeof (BranchItem.acctName))
    {
    }

    public TreeSelectorAttribute(
      System.Type selectorSearch,
      System.Type treeDataMemberType,
      params System.Type[] fieldList)
      : base(selectorSearch, fieldList)
    {
      this.DescriptionField = typeof (BranchItem.acctName);
      this.SubstituteKey = typeof (BranchItem.acctCD);
      System.Type type = !(treeDataMemberType != (System.Type) null) || typeof (PXSelectOrganizationTree).IsAssignableFrom(treeDataMemberType) ? treeDataMemberType : throw new ArgumentException(treeDataMemberType.Name + " argument must be of PXSelectOrganizationTree type");
      if ((object) type == null)
        type = typeof (PXSelectOrganizationTree);
      this.TreeDataMemberType = type;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      base.CacheAttached(sender);
      this.TreeViewName = $"_OrganizationTree_{sender.GetItemType().FullName}{((PXEventSubscriberAttribute) this)._FieldName}";
      if (!((Dictionary<string, PXView>) sender.Graph.Views).ContainsKey(this.TreeViewName))
      {
        PXSelectOrganizationTree instance = (PXSelectOrganizationTree) Activator.CreateInstance(this.TreeDataMemberType, (object) sender.Graph);
        instance.OnlyActive = this.OnlyActive;
        sender.Graph.Views.Add(this.TreeViewName, ((PXSelectBase) instance).View);
      }
      if (!(sender.Graph.Caches[typeof (PX.Objects.CR.BAccount)].GetItemType() == typeof (BranchItem)))
        return;
      ((Dictionary<System.Type, PXCache>) sender.Graph.Caches).Remove(typeof (BranchItem));
    }

    public string TreeViewName { get; private set; }

    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
    }

    public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      base.FieldSelecting(sender, e);
      PXBranchSelectorState branchSelectorState = new PXBranchSelectorState((object) (e.ReturnState as PXFieldState));
      ((PXFieldState) branchSelectorState).ViewName = this.TreeViewName;
      branchSelectorState.DACName = sender.GetItemType().FullName;
      branchSelectorState.SelectionMode = this.SelectionMode;
      e.ReturnState = (object) branchSelectorState;
    }

    protected virtual void DescriptionFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      object key,
      ref bool deleted)
    {
      if (this.BranchMode)
        key = (object) PXAccess.GetBranch((int?) key)?.BAccountID;
      base.DescriptionFieldSelecting(sender, e, key, ref deleted);
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (e.ReturnValue != null && !this.Nullable)
      {
        int? returnValue = (int?) e.ReturnValue;
        int num = 0;
        if (returnValue.GetValueOrDefault() == num & returnValue.HasValue)
          e.ReturnValue = (object) null;
      }
      if (this.BranchMode && e.ReturnValue != null)
        e.ReturnValue = (object) PXAccess.GetBranch((int?) e.ReturnValue)?.BAccountID;
      base.SubstituteKeyFieldSelecting(sender, e);
    }

    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      object objA = e.NewValue;
      if (!this.Nullable && object.Equals(objA, (object) 0))
        e.NewValue = objA = (object) null;
      base.SubstituteKeyFieldUpdating(sender, e);
      if (this.BranchMode && e.NewValue != null && e.NewValue != objA)
        e.NewValue = (object) PXAccess.GetBranchByBAccountID((int?) e.NewValue)?.BranchID;
      if (e.NewValue != null || this.Nullable)
        return;
      e.NewValue = (object) 0;
    }
  }
}
