// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.OrganizationTreeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL.DAC;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBInt]
[PXDefault]
[PXUIField(DisplayName = "Company/Branch", FieldClass = "COMPANYBRANCH")]
public class OrganizationTreeAttribute : 
  BaseOrganizationTreeAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldDefaultingSubscriber
{
  [InjectDependencyOnTypeLevel]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public OrganizationTreeAttribute(Type treeDataMember = null, bool onlyActive = true)
    : this((Type) null, (Type) null, treeDataMember, onlyActive)
  {
  }

  public OrganizationTreeAttribute(
    Type sourceOrganizationID,
    Type sourceBranchID,
    Type treeDataMember = null,
    bool onlyActive = true)
    : base(treeDataMember, onlyActive)
  {
    this.SourceOrganizationID = sourceOrganizationID;
    this.SourceBranchID = sourceBranchID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (this.SourceOrganizationID != (Type) null)
      PXUIFieldAttribute.SetEnabled(sender, (object) null, this.SourceOrganizationID.Name, false);
    if (!(this.SourceBranchID != (Type) null))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) null, this.SourceBranchID.Name, false);
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this._attr.Nullable)
    {
      e.NewValue = (object) null;
    }
    else
    {
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(sender.Graph.Accessinfo.BranchID);
      if (branch == null || e.NewValue != null)
        return;
      PXFieldState stateExt = sender.GetStateExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) as PXFieldState;
      foreach (BranchItem branchItem in sender.Graph.Views[stateExt.ViewName].SelectMulti(Array.Empty<object>()))
      {
        if (branchItem.AcctCD.Trim() == branch.BranchCD.Trim())
        {
          e.NewValue = (object) branchItem.AcctCD;
          break;
        }
        if (branchItem.AcctCD.Trim() == ((PXAccess.Organization) branch.Organization).OrganizationCD.Trim())
          e.NewValue = (object) branchItem.AcctCD;
      }
    }
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.Row == null || this.SourceOrganizationID == (Type) null && this.SourceBranchID == (Type) null)
      return;
    int? newValue = (int?) e.NewValue;
    if (!newValue.HasValue)
    {
      this.SetValue(sender, e.Row, this.SourceOrganizationID, (object) null);
      this.SetValue(sender, e.Row, this.SourceBranchID, (object) null);
    }
    else
    {
      foreach (PXAccess.MasterCollection.Organization organization in this._currentUserInformationProvider.GetOrganizations(this._attr.OnlyActive, true).Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (_ =>
      {
        bool? deletedDatabaseRecord = ((PXAccess.Organization) _).DeletedDatabaseRecord;
        bool flag = false;
        return deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue;
      })))
      {
        int? baccountId1 = ((PXAccess.Organization) organization).BAccountID;
        int? nullable1 = newValue;
        if (baccountId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & baccountId1.HasValue == nullable1.HasValue)
        {
          this.SetValue(sender, e.Row, this.SourceOrganizationID, (object) ((PXAccess.Organization) organization).OrganizationID);
          this.SetValue(sender, e.Row, this.SourceBranchID, (object) null);
          return;
        }
        foreach (PXAccess.MasterCollection.Branch branch in organization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (_ => !_.DeletedDatabaseRecord)))
        {
          int baccountId2 = branch.BAccountID;
          int? nullable2 = newValue;
          int valueOrDefault = nullable2.GetValueOrDefault();
          if (baccountId2 == valueOrDefault & nullable2.HasValue)
          {
            this.SetValue(sender, e.Row, this.SourceOrganizationID, (object) ((PXAccess.Organization) organization).OrganizationID);
            this.SetValue(sender, e.Row, this.SourceBranchID, (object) branch.BranchID);
            return;
          }
        }
      }
      this.SetValue(sender, e.Row, this.SourceOrganizationID, (object) null);
      this.SetValue(sender, e.Row, this.SourceBranchID, (object) null);
    }
  }

  private void SetValue(PXCache cache, object row, Type fieldType, object value)
  {
    if (!(fieldType != (Type) null))
      return;
    cache.SetValue(row, fieldType.Name, value);
  }
}
