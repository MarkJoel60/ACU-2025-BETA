// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Attributes.MatchOrganizationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Attributes;

[PXDBInt]
[PXVirtualSelector(typeof (Branch.branchID))]
public class MatchOrganizationAttribute : PXAggregateAttribute
{
  [InjectDependencyOnTypeLevel]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    PXGraph.FieldSelectingEvents fieldSelecting = sender.Graph.FieldSelecting;
    Type itemType1 = sender.GetItemType();
    string fieldName1 = ((PXEventSubscriberAttribute) this)._FieldName;
    MatchOrganizationAttribute organizationAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) organizationAttribute1, __vmethodptr(organizationAttribute1, FieldSelecting));
    fieldSelecting.AddHandler(itemType1, fieldName1, pxFieldSelecting);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType2 = sender.GetItemType();
    string fieldName2 = ((PXEventSubscriberAttribute) this)._FieldName;
    MatchOrganizationAttribute organizationAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) organizationAttribute2, __vmethodptr(organizationAttribute2, FieldUpdating));
    fieldUpdating.AddHandler(itemType2, fieldName2, pxFieldUpdating);
  }

  protected virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string value = e.NewValue as string;
    if (value == null)
      return;
    value = value.Trim();
    foreach (PXAccess.MasterCollection.Organization organization in this._currentUserInformationProvider.GetOrganizations(true, true))
    {
      using (IEnumerator<PXAccess.MasterCollection.Branch> enumerator = organization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (branch => branch.BranchCD == value)).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXAccess.MasterCollection.Branch current = enumerator.Current;
          e.NewValue = (object) current.BranchID;
          break;
        }
      }
      if (((PXAccess.Organization) organization).OrganizationCD == value)
      {
        e.NewValue = (object) 0;
        break;
      }
    }
  }

  protected virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.ReturnValue is int returnValue))
      return;
    int value = returnValue;
    foreach (PXAccess.MasterCollection.Organization organization in this._currentUserInformationProvider.GetOrganizations(true, true))
    {
      using (IEnumerator<PXAccess.MasterCollection.Branch> enumerator = organization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (branch => branch.BAccountID == value)).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXAccess.MasterCollection.Branch current = enumerator.Current;
          e.ReturnValue = (object) current.BranchCD;
          break;
        }
      }
      int? baccountId = ((PXAccess.Organization) organization).BAccountID;
      int num = value;
      if (baccountId.GetValueOrDefault() == num & baccountId.HasValue)
      {
        e.ReturnValue = (object) ((PXAccess.Organization) organization).OrganizationCD;
        break;
      }
    }
  }
}
