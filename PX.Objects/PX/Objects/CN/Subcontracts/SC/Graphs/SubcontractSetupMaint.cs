// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.SubcontractSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Common.Helpers;
using PX.Objects.CN.Subcontracts.PO.CacheExtensions;
using PX.Objects.CN.Subcontracts.SC.DAC;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.PO;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class SubcontractSetupMaint : POSetupMaint
{
  public 
  #nullable disable
  PXSelectJoin<CSAnswers, RightJoin<POOrder, On<CSAnswers.refNoteID, Equal<POOrder.noteID>>>> Answers;
  public FbqlSelect<SelectFromBase<CSAttributeGroup, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAttribute>.On<BqlOperand<
  #nullable enable
  CSAttribute.attributeID, IBqlString>.IsEqual<
  #nullable disable
  CSAttributeGroup.attributeID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CSAttributeGroup.entityClassID, 
  #nullable disable
  Equal<PoOrderExt.subcontractClass>>>>>.And<BqlOperand<
  #nullable enable
  CSAttributeGroup.entityType, IBqlString>.IsEqual<
  #nullable disable
  PoOrderExt.pOOrderExtTypeName>>>, CSAttributeGroup>.View Attributes;
  public CRNotificationSetupList<SubcontractNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<SubcontractNotification.setupID>>>> Recipients;
  public PXSetup<POSetup> PurchaseOrderSetup;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypePurchaseOrder>, And<EPAssignmentMap.mapType, NotEqual<EPMapType.assignment>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POSetupApproval.assignmentMapID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Notification.notificationID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POSetupApproval.assignmentNotificationID> e)
  {
  }

  public SubcontractSetupMaint()
  {
    FeaturesSetHelper.CheckConstructionFeature();
    POSetup current = ((PXSelectBase<POSetup>) this.PurchaseOrderSetup).Current;
  }

  public IEnumerable setup()
  {
    return (IEnumerable) PXSelectBase<POSetup, PXSelect<POSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()).FirstTableItems.Select<POSetup, POSetup>(new Func<POSetup, POSetup>(this.UpdateSubcontractSetupStatusIfRequired));
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<POSetupApproval>) this.SetupApproval).Current != null)
    {
      int? nullable = ((PXSelectBase<POSetupApproval>) this.SetupApproval).Current.AssignmentMapID;
      if (!nullable.HasValue)
      {
        nullable = ((PXSelectBase<POSetupApproval>) this.SetupApproval).Current.AssignmentNotificationID;
        if (!nullable.HasValue)
          ((PXSelectBase<POSetupApproval>) this.SetupApproval).Delete(((PXSelectBase<POSetupApproval>) this.SetupApproval).Current);
      }
    }
    ((PXGraph) this).Persist();
  }

  public virtual void _(PX.Data.Events.RowInserting<POSetupApproval> args)
  {
    args.Row.OrderType = "RS";
  }

  public virtual void _(PX.Data.Events.RowInserting<CSAttributeGroup> args)
  {
    args.Row.EntityClassID = "SUBCONTRACTS";
    args.Row.EntityType = typeof (PoOrderExt).FullName;
  }

  public virtual void _(PX.Data.Events.RowDeleting<CSAttributeGroup> args)
  {
    CSAttributeGroup row = args.Row;
    if (row == null)
      return;
    if (row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (this.IsDeleteConfirmed())
      this.DeleteAnswers(row.AttributeID);
    else
      args.Cancel = true;
  }

  private void DeleteAnswers(string attributeId)
  {
    foreach (object obj in ((IEnumerable<CSAnswers>) ((PXSelectBase<CSAnswers>) this.Answers).SelectMain(Array.Empty<object>())).Where<CSAnswers>((Func<CSAnswers, bool>) (a => a.AttributeID == attributeId)))
      ((PXSelectBase) this.Answers).Cache.Delete(obj);
  }

  private bool IsDeleteConfirmed()
  {
    return ((PXSelectBase<CSAttributeGroup>) this.Attributes).Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) == 1;
  }

  private POSetup UpdateSubcontractSetupStatusIfRequired(POSetup setup)
  {
    PoSetupExt extension = ((PXSelectBase) this.Setup).Cache.GetExtension<PoSetupExt>((object) setup);
    if (!extension.IsSubcontractSetupSaved.GetValueOrDefault())
    {
      extension.IsSubcontractSetupSaved = new bool?(true);
      ((PXSelectBase) this.Setup).Cache.SetDefaultExt<PoSetupExt.requireSubcontractControlTotal>((object) setup);
      ((PXSelectBase) this.Setup).Cache.SetDefaultExt<PoSetupExt.subcontractNumberingID>((object) setup);
      ((PXSelectBase) this.Setup).Cache.Update((object) setup);
    }
    return setup;
  }
}
