// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ReassignDelegatedActivitiesProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.TM;
using System;
using System.Collections;
using System.Threading;

#nullable enable
namespace PX.Objects.EP;

public class ReassignDelegatedActivitiesProcess : PXGraph<
#nullable disable
ReassignDelegatedActivitiesProcess>
{
  [PXHidden]
  public PXSelect<EPApproval> dummyEPApproval;
  [PXFilterable(new System.Type[] {})]
  public PXProcessing<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter> Records;
  public PXAction<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter> EditDetail;

  public ReassignDelegatedActivitiesProcess()
  {
    this.Records.SetSelected<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.selected>();
    this.Records.SetProcessDelegate<ReassignDelegatedActivitiesProcess>(new Action<ReassignDelegatedActivitiesProcess, ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter, CancellationToken>(ReassignDelegatedActivitiesProcess.StartProcessing));
    PXStringListAttribute.SetList<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.delegationOf>(this.Records.Cache, (object) null, new string[1]
    {
      "A"
    }, new string[1]{ "Approvals" });
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    if (this.Records.Current != null)
    {
      Guid? refNoteId = this.Records.Current.RefNoteID;
      if (refNoteId.HasValue)
      {
        EntityHelper entityHelper = new EntityHelper((PXGraph) this);
        refNoteId = this.Records.Current.RefNoteID;
        Guid? noteID = new Guid?(refNoteId.Value);
        entityHelper.NavigateToRow(noteID, PXRedirectHelper.WindowMode.InlineWindow);
      }
    }
    return adapter.Get();
  }

  public static void StartProcessing(
    ReassignDelegatedActivitiesProcess graph,
    ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter item,
    CancellationToken cancellationToken)
  {
    graph.ProcessApprovalDelegate(item, cancellationToken);
  }

  public virtual void ProcessApprovalDelegate(
    ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter item,
    CancellationToken cancellationToken)
  {
    if (cancellationToken.IsCancellationRequested || item == null)
      return;
    PXProcessing<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter>.SetCurrentItem((object) item);
    try
    {
      if ((item != null ? (!item.DelegatedToContactID.HasValue ? 1 : 0) : 1) != 0)
      {
        item.DelegationRecordID = new int?();
        item.IgnoreDelegations = new bool?(false);
        EPApprovalHelper.ReassignToDelegate((PXGraph) this, (EPApproval) item, item.OrigOwnerID);
      }
      else
        EPApprovalHelper.ReassignToDelegate((PXGraph) this, (EPApproval) item, item.OwnerID);
    }
    catch (EPApprovalHelper.PXReassignmentApproverNotAvailableException ex)
    {
      object[] objArray = Array.Empty<object>();
      throw new PXSetPropertyException((Exception) ex, PXErrorLevel.RowError, "The delegate or their delegates are not available. The approval was not delegated.", objArray);
    }
    this.Caches[typeof (EPApproval)].Update((object) item);
    this.Persist();
    PXProcessing<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter>.SetInfo("The record has been processed successfully.");
  }

  /// <exclude />
  [PXBreakInheritance]
  [PXProjection(typeof (SelectFromBase<EPWingmanForApprovals, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccount>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPWingman.employeeID, Equal<BAccount.bAccountID>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.employeeType>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPWingman.startsOn, LessEqual<EPApprovalHelper.PXTimeZoneInfoToday.dayEnd>>>>, And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPWingman.expiresOn, IsNull>>>>.Or<BqlOperand<EPWingman.expiresOn, IBqlDateTime>.IsGreaterEqual<EPApprovalHelper.PXTimeZoneInfoToday.dayBegin>>>>>>.Or<BqlOperand<EPWingman.startsOn, IBqlDateTime>.IsGreater<EPApprovalHelper.PXTimeZoneInfoToday.dayEnd>>>))]
  [PXHidden]
  [Serializable]
  public class TodayDelegates : EPWingmanForApprovals
  {
    [PXDBInt(BqlTable = typeof (BAccount), BqlField = typeof (BAccount.defContactID))]
    public int? ContactID { get; set; }

    public abstract class contactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.TodayDelegates.contactID>
    {
    }

    public new abstract class recordID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.TodayDelegates.recordID>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [PXProjection(typeof (SelectFromBase<EPApproval, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Note>.On<BqlOperand<Note.noteID, IBqlGuid>.IsEqual<EPApproval.refNoteID>>>, FbqlJoins.Inner<EPRule>.On<EPApproval.FK.Rule>>, FbqlJoins.Left<ReassignDelegatedActivitiesProcess.TodayDelegates>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ReassignDelegatedActivitiesProcess.TodayDelegates.contactID, Equal<EPApproval.ownerID>>>>>.Or<BqlOperand<ReassignDelegatedActivitiesProcess.TodayDelegates.recordID, IBqlInt>.IsEqual<EPApproval.delegationRecordID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPApproval.status, Equal<EPApprovalStatus.pending>>>>, And<BqlOperand<EPRule.ruleID, IBqlGuid>.IsNotNull>>>.And<Where2<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ReassignDelegatedActivitiesProcess.TodayDelegates.contactID, IsNotNull>>>, And<BqlOperand<EPApproval.ignoreDelegations, IBqlBool>.IsEqual<False>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPApproval.delegationRecordID, IsNull>>>>.Or<BqlOperand<EPApproval.delegationRecordID, IBqlInt>.IsNotEqual<ReassignDelegatedActivitiesProcess.TodayDelegates.recordID>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPApproval.delegationRecordID, IsNotNull>>>>.And<BqlOperand<ReassignDelegatedActivitiesProcess.TodayDelegates.recordID, IBqlInt>.IsNull>>>>>))]
  [Serializable]
  public class EPApprovalWingmanFilter : EPApproval
  {
    [PXBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXString(1, IsFixed = true)]
    [EPDelegationOf.List]
    [PXUIField(DisplayName = "Delegation Of")]
    [PXDefault(typeof (EPDelegationOf.approvals), PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual string DelegationOf => "A";

    [PXDBString(BqlField = typeof (Note.entityType))]
    public string EntityType { get; set; }

    [Owner(DisplayName = "Original Assignee", Visibility = PXUIVisibility.SelectorVisible)]
    public override int? OrigOwnerID { get; set; }

    [Owner(DisplayName = "Current Assignee", Visibility = PXUIVisibility.SelectorVisible)]
    public override int? OwnerID { get; set; }

    [PXDBDate(PreserveTime = true, DisplayMask = "g")]
    [PXUIField(DisplayName = "Requested Time")]
    public override DateTime? CreatedDateTime { get; set; }

    [PXString]
    [PXFormula(typeof (ApprovalDocType<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.entityType, EPApproval.sourceItemType>))]
    [PXUIField(DisplayName = "Document Type")]
    public override string DocType { get; set; }

    [Owner(DisplayName = "Delegated To", BqlField = typeof (ReassignDelegatedActivitiesProcess.TodayDelegates.contactID), Visibility = PXUIVisibility.SelectorVisible)]
    public int? DelegatedToContactID { get; set; }

    [PXDBDate(BqlField = typeof (EPWingman.startsOn))]
    [PXUIField(DisplayName = "Starts On", Visibility = PXUIVisibility.SelectorVisible)]
    [PXUIEnabled(typeof (Where<BqlOperand<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.delegationOf, IBqlString>.IsEqual<EPDelegationOf.approvals>>))]
    public virtual DateTime? StartsOn { get; set; }

    [PXDBDate(BqlField = typeof (EPWingman.expiresOn))]
    [PXUIField(DisplayName = "Expires On", Visibility = PXUIVisibility.SelectorVisible)]
    [PXUIEnabled(typeof (Where<BqlOperand<ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.delegationOf, IBqlString>.IsEqual<EPDelegationOf.approvals>>))]
    public virtual DateTime? ExpiresOn { get; set; }

    [PXDBBool(BqlField = typeof (EPWingman.isActive))]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Active")]
    public virtual bool? IsActive { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.selected>
    {
    }

    public abstract class delegationOf : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.delegationOf>
    {
    }

    public abstract class entityType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.entityType>
    {
    }

    public new abstract class origOwnerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.origOwnerID>
    {
    }

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.ownerID>
    {
    }

    public new abstract class createdDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.createdDateTime>
    {
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.docType>
    {
    }

    public abstract class delegatedToContactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.delegatedToContactID>
    {
    }

    public abstract class startsOn : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.startsOn>
    {
    }

    public abstract class expiresOn : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.expiresOn>
    {
    }

    public abstract class isActive : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ReassignDelegatedActivitiesProcess.EPApprovalWingmanFilter.isActive>
    {
    }
  }
}
