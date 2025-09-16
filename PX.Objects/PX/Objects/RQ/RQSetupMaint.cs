// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.RQ;

public class RQSetupMaint : PXGraph<RQSetupMaint>
{
  public PXSave<RQSetup> Save;
  public PXCancel<RQSetup> Cancel;
  public PXSelect<RQSetup> Setup;
  public PXSelect<RQSetupApproval> SetupApproval;
  public CRNotificationSetupList<RQNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<RQNotification.setupID>>>> Recipients;

  [PXDBString(10)]
  [PXDefault]
  [VendorContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void NotificationSetupRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  public virtual void NotificationSetupRecipient_ContactID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void RQSetup_RequestApproval_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (RQSetupApproval)];
    foreach (PXResult<RQSetupApproval> pxResult in PXSelectBase<RQSetupApproval, PXSelect<RQSetupApproval, Where<RQSetupApproval.type, Equal<RQType.requestItem>>>.Config>.Select(sender.Graph, (object[]) null))
    {
      RQSetupApproval rqSetupApproval = PXResult<RQSetupApproval>.op_Implicit(pxResult);
      rqSetupApproval.IsActive = (bool?) e.NewValue;
      cach.Update((object) rqSetupApproval);
    }
  }

  protected virtual void RQSetup_RequisitionApproval_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (RQSetupApproval)];
    foreach (PXResult<RQSetupApproval> pxResult in PXSelectBase<RQSetupApproval, PXSelect<RQSetupApproval, Where<RQSetupApproval.type, Equal<RQType.requisition>>>.Config>.Select(sender.Graph, (object[]) null))
    {
      RQSetupApproval rqSetupApproval = PXResult<RQSetupApproval>.op_Implicit(pxResult);
      rqSetupApproval.IsActive = (bool?) e.NewValue;
      cach.Update((object) rqSetupApproval);
    }
  }
}
