// Decompiled with JetBrains decompiler
// Type: PX.TM.PXCompanyMemberSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.TM;

/// <summary>Shows members of a company tree.</summary>
/// <example>
/// <code title="Example" lang="CS">
/// //The general attribute definition
/// [PXCompanyMemberSelector(typeof(WikiPage.approvalGroupID))]</code>
/// </example>
public class PXCompanyMemberSelectorAttribute : PXSelectorAttribute
{
  protected string _workGroupField;

  public PXCompanyMemberSelectorAttribute()
    : this((System.Type) null)
  {
  }

  public PXCompanyMemberSelectorAttribute(System.Type workgroupType)
    : base(PXCompanyMemberSelectorAttribute.CreateSearch(workgroupType))
  {
    this.DescriptionField = typeof (Users.displayName);
    if (!(workgroupType != (System.Type) null))
      return;
    this._workGroupField = workgroupType.Name;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this._workGroupField == null)
      return;
    sender.Graph.FieldUpdated.AddHandler(sender.GetItemType(), this._workGroupField, new PXFieldUpdated(this.WorkGroup_FieldUpdated));
  }

  private static System.Type CreateSearch(System.Type workgroupType)
  {
    if (workgroupType == (System.Type) null)
      return typeof (Search<Users.pKID, Where<Users.guest, NotEqual<PX.Data.True>>>);
    return BqlCommand.Compose(typeof (Search5<,,,>), typeof (Users.pKID), typeof (LeftJoin<,,>), typeof (PXAccess.EPEmployee), typeof (On<,>), typeof (PXAccess.EPEmployee.userID), typeof (Equal<Users.pKID>), typeof (LeftJoin<,,>), typeof (PXAccess.BAccount), typeof (On<,>), typeof (PXAccess.BAccount.bAccountID), typeof (Equal<PXAccess.EPEmployee.bAccountID>), typeof (LeftJoin<,>), typeof (EPCompanyTreeMember), typeof (On<,,>), typeof (EPCompanyTreeMember.contactID), typeof (Equal<PXAccess.BAccount.defContactID>), typeof (And<,>), typeof (EPCompanyTreeMember.workGroupID), typeof (Equal<>), typeof (Optional<>), workgroupType, typeof (Where<,,>), typeof (Optional<>), workgroupType, typeof (IsNull), typeof (Or<EPCompanyTreeMember.contactID, IsNotNull>), typeof (Aggregate<GroupBy<Users.pKID>>));
  }

  public static bool BelongsToWorkGroup(PXGraph graph, int? WorkGroupID, Guid? OwnerID)
  {
    return PXSelectBase<EPCompanyTreeMember, PXSelectJoin<EPCompanyTreeMember, InnerJoin<PXAccess.BAccount, On<PXAccess.BAccount.defContactID, Equal<EPCompanyTreeMember.contactID>>, InnerJoin<PXAccess.EPEmployee, On<PXAccess.EPEmployee.bAccountID, Equal<PXAccess.BAccount.bAccountID>>>>, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<PXAccess.EPEmployee.userID, Equal<Required<PXAccess.EPEmployee.userID>>>>>.Config>.Select(graph, (object) WorkGroupID, (object) OwnerID).Count > 0;
  }

  public static Guid? OwnerWorkGroup(PXGraph graph, int? WorkGroupID)
  {
    EPCompanyTreeMember companyTreeMember = (EPCompanyTreeMember) PXSelectBase<EPCompanyTreeMember, PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.isOwner, Equal<Required<EPCompanyTreeMember.isOwner>>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) WorkGroupID, (object) 1);
    if (companyTreeMember == null)
      return new Guid?();
    return ((PXAccess.EPEmployee) PXSelectBase<PXAccess.EPEmployee, PXSelectJoin<PXAccess.EPEmployee, InnerJoin<PXAccess.BAccount, On<PXAccess.BAccount.bAccountID, Equal<PXAccess.EPEmployee.bAccountID>>>, Where<PXAccess.BAccount.defContactID, Equal<Required<PXAccess.BAccount.defContactID>>>>.Config>.SelectSingleBound(graph, (object[]) null, (object) companyTreeMember.ContactID))?.UserID;
  }

  protected void WorkGroup_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    int? WorkGroupID = (int?) sender.GetValue(e.Row, this._workGroupField);
    Guid? OwnerID = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
    if (PXCompanyMemberSelectorAttribute.BelongsToWorkGroup(sender.Graph, WorkGroupID, OwnerID))
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) PXCompanyMemberSelectorAttribute.OwnerWorkGroup(sender.Graph, WorkGroupID));
  }
}
