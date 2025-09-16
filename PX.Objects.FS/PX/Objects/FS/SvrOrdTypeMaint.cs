// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SvrOrdTypeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.FS;

public class SvrOrdTypeMaint : PXGraph<SvrOrdTypeMaint, FSSrvOrdType>
{
  [PXImport(typeof (FSSrvOrdType))]
  public PXSelect<FSSrvOrdType> SvrOrdTypeRecords;
  [PXHidden]
  public PXSelectJoin<FSAppointmentDet, InnerJoin<FSAppointment, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>>>, Where<FSAppointment.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>, And<FSAppointmentDet.postID, IsNotNull>>> SrvOrdAppointmentsPosted;
  [PXViewName("Attributes")]
  public FSAttributeGroupList<FSSrvOrdType, FSAppointment, FSServiceOrder, FSSchedule> Mapping;
  public PXSelect<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>>> CurrentSrvOrdTypeRecord;
  public PXSelectJoin<FSSrvOrdTypeProblem, InnerJoin<FSProblem, On<FSSrvOrdTypeProblem.problemID, Equal<FSProblem.problemID>>>, Where<FSSrvOrdTypeProblem.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>>> SrvOrdTypeProblemRecords;
  public CRClassNotificationSourceList<FSSrvOrdType.srvOrdType, FSNotificationSource.appointment> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>>> NotificationRecipients;
  public PXSelect<FSQuickProcessParameters, Where<FSQuickProcessParameters.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>>> QuickProcessSettings;
  public PXSetup<FSSetup> SetupRecord;

  public SvrOrdTypeMaint()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(SvrOrdTypeMaint.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (SvrOrdTypeMaint.\u003C\u003Ec.\u003C\u003E9__0_0 = new PXFieldDefaulting((object) SvrOrdTypeMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__0_0))));
  }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<FSNotificationSource.appointment>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (FSSrvOrdType.srvOrdType))]
  [PXUIField]
  [PXParent(typeof (Select2<FSSrvOrdType, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Current<NotificationSource.classID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ClassID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<SiteMap.screenID, Where2<Where<SiteMap.url, Like<urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>, And<Where<SiteMap.screenID, Like<FSModule.fs_>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [ApptContactType.ClassList]
  [PXCheckUnique(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.refNoteID, IsNull, And<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsUnicode = true, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  protected virtual void CSAttributeGroup_EntityClassID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// Enables/Disables the Employee Time Card Integration fields.
  /// </summary>
  public virtual void EnableDisableEmployeeTimeCardIntegrationFields(
    PXCache cache,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool? nullable;
    int num1;
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current != null)
    {
      nullable = ((PXSelectBase<FSSetup>) this.SetupRecord).Current.EnableEmpTimeCardIntegration;
      if (nullable.GetValueOrDefault())
      {
        num1 = fsSrvOrdTypeRow.Behavior != "QT" ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    bool flag1 = num1 != 0;
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireTimeApprovalToInvoice>(cache, (object) fsSrvOrdTypeRow, flag1);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.createTimeActivitiesFromAppointment>(cache, (object) fsSrvOrdTypeRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.requireTimeApprovalToInvoice>(cache, (object) fsSrvOrdTypeRow, flag1);
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.createTimeActivitiesFromAppointment>(cache, (object) fsSrvOrdTypeRow, flag1);
    int num2;
    if (flag1)
    {
      nullable = fsSrvOrdTypeRow.CreateTimeActivitiesFromAppointment;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag2 = num2 != 0;
    bool flag3 = flag2 && fsSrvOrdTypeRow.Behavior != "QT";
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.dfltEarningType>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.dfltEarningType>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetRequired<FSSrvOrdType.dfltEarningType>(cache, flag3);
    PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltEarningType>(cache, (object) fsSrvOrdTypeRow, flag3 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual void CheckAppointmenAddressContactOptions(
    PXCache cache,
    FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsSrvOrdTypeRow.AppAddressSource.Equals("CC") || fsSrvOrdTypeRow.AppContactInfoSource.Equals("CC"))
    {
      fsSrvOrdTypeRow.RequireContact = new bool?(true);
      PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireContact>(cache, (object) fsSrvOrdTypeRow, false);
    }
    else
      PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireContact>(cache, (object) fsSrvOrdTypeRow, true);
  }

  public virtual void SetQuickProcessSettingsVisibility(
    PXCache cache,
    FSSrvOrdType fsSrvOrdTypeRow,
    FSQuickProcessParameters fsQuickProcessParametersRow)
  {
    if (fsSrvOrdTypeRow == null)
      return;
    bool flag1 = false;
    bool? nullable;
    int num1;
    if (!fsQuickProcessParametersRow.GenerateInvoiceFromServiceOrder.Value)
    {
      nullable = fsQuickProcessParametersRow.GenerateInvoiceFromAppointment;
      num1 = nullable.Value ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag2 = num1 != 0;
    bool flag3 = fsSrvOrdTypeRow.PostTo == "SO";
    bool flag4 = fsSrvOrdTypeRow.PostTo == "SI";
    bool flag5 = flag4;
    if (flag3)
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderType, PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Required<PX.Objects.SO.SOOrderType.orderType>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) fsSrvOrdTypeRow.PostOrderType
      }));
      if (soOrderType != null)
      {
        flag5 = soOrderType.Behavior == "IN";
        nullable = soOrderType.AllowQuickProcess;
        flag1 = nullable.Value;
      }
    }
    int num2;
    if (flag5)
    {
      int num3;
      if (flag3)
      {
        nullable = fsQuickProcessParametersRow.SOQuickProcess;
        bool flag6 = false;
        if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue)
        {
          nullable = fsQuickProcessParametersRow.PrepareInvoice;
          num3 = nullable.GetValueOrDefault() ? 1 : 0;
          goto label_12;
        }
      }
      num3 = 0;
label_12:
      int num4 = flag4 ? 1 : 0;
      num2 = num3 | num4;
    }
    else
      num2 = 0;
    int num5 = flag2 ? 1 : 0;
    bool flag7 = (num2 & num5) != 0;
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.sOQuickProcess>(cache, (object) fsQuickProcessParametersRow, flag3 & flag1);
    PXCache pxCache1 = cache;
    FSQuickProcessParameters processParameters1 = fsQuickProcessParametersRow;
    int num6;
    if (flag3 & flag5)
    {
      nullable = fsQuickProcessParametersRow.SOQuickProcess;
      bool flag8 = false;
      num6 = nullable.GetValueOrDefault() == flag8 & nullable.HasValue ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.prepareInvoice>(pxCache1, (object) processParameters1, num6 != 0);
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.emailSalesOrder>(cache, (object) fsQuickProcessParametersRow, flag3);
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.releaseInvoice>(cache, (object) fsQuickProcessParametersRow, flag7);
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.emailInvoice>(cache, (object) fsQuickProcessParametersRow, flag7);
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.releaseBill>(cache, (object) fsQuickProcessParametersRow, false);
    PXUIFieldAttribute.SetVisible<FSQuickProcessParameters.payBill>(cache, (object) fsQuickProcessParametersRow, false);
    PXCache pxCache2 = cache;
    FSQuickProcessParameters processParameters2 = fsQuickProcessParametersRow;
    nullable = fsQuickProcessParametersRow.PrepareInvoice;
    int num7 = !nullable.Value & flag2 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSQuickProcessParameters.sOQuickProcess>(pxCache2, (object) processParameters2, num7 != 0);
    PXUIFieldAttribute.SetEnabled<FSQuickProcessParameters.releaseInvoice>(cache, (object) fsQuickProcessParametersRow, flag7);
    PXUIFieldAttribute.SetEnabled<FSQuickProcessParameters.emailInvoice>(cache, (object) fsQuickProcessParametersRow, flag7);
  }

  /// <summary>
  /// Hides/Shows invoicing related fields, depending on the <c>fsSrvOrdTypeRow.Behavior</c>.
  /// </summary>
  public virtual void SetPostingSettingVisibility(
    PXCache cache,
    FSSrvOrdType fsSrvOrdTypeRow,
    bool isVisible)
  {
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.postTo>(cache, (object) fsSrvOrdTypeRow, isVisible);
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();
    isVisible = isVisible && fsSrvOrdTypeRow.PostTo != "NA";
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.dfltTermIDARSO>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.copyNotesToInvoice>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.copyAttachmentsToInvoice>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.dfltTermIDAP>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo == "AR");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.salesAcctSource>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.combineSubFrom>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.subID>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo != "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.allowInvoiceOnlyClosedAppointment>(cache, (object) fsSrvOrdTypeRow, isVisible || fsSrvOrdTypeRow.PostTo == "PM");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.postNegBalanceToAP>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.PostTo == "AR");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.enableINPosting>(cache, (object) fsSrvOrdTypeRow, isVisible && fsSrvOrdTypeRow.Behavior == "RO");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.postOrderType>(cache, (object) fsSrvOrdTypeRow, isVisible & flag1 && fsSrvOrdTypeRow.PostTo == "SO");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.postOrderTypeNegativeBalance>(cache, (object) fsSrvOrdTypeRow, isVisible & flag1 && fsSrvOrdTypeRow.PostTo == "SO");
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.allowQuickProcess>(cache, (object) fsSrvOrdTypeRow, fsSrvOrdTypeRow.PostToSOSIPM.GetValueOrDefault() && fsSrvOrdTypeRow.PostTo != "PM");
    bool? nullable;
    int num;
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>() & isVisible & flag1)
    {
      nullable = fsSrvOrdTypeRow.PostToSOSIPM;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool flag2 = num != 0;
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.allocationOrderType>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetRequired<FSSrvOrdType.allocationOrderType>(cache, flag2);
    PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.allocationOrderType>(cache, (object) fsSrvOrdTypeRow, flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (!isVisible || fsSrvOrdTypeRow.PostTo == "NA" || fsSrvOrdTypeRow.PostTo == "PM")
    {
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltTermIDAP>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltTermIDARSO>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.combineSubFrom>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderType>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderTypeNegativeBalance>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
    }
    else
    {
      nullable = fsSrvOrdTypeRow.PostNegBalanceToAP;
      if (nullable.GetValueOrDefault())
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltTermIDAP>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 1);
      else
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltTermIDAP>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.dfltTermIDARSO>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 1);
      PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.combineSubFrom>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 1);
      if (fsSrvOrdTypeRow.PostTo == "SO")
      {
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderType>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 1);
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderTypeNegativeBalance>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 1);
      }
      else
      {
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderType>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
        PXDefaultAttribute.SetPersistingCheck<FSSrvOrdType.postOrderTypeNegativeBalance>(cache, (object) fsSrvOrdTypeRow, (PXPersistingCheck) 2);
      }
    }
  }

  public virtual void EnableDisable_Behavior(PXCache cache, FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool flag = true;
    if (cache.GetStatus((object) fsSrvOrdTypeRow) != 2)
      flag = PXSelectBase<FSSrvOrdType, PXSelectJoin<FSSrvOrdType, LeftJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSSrvOrdType.srvOrdType>>, LeftJoin<FSSchedule, On<FSSchedule.srvOrdType, Equal<FSSrvOrdType.srvOrdType>>>>, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>, And<Where<FSServiceOrder.sOID, IsNotNull, Or<FSSchedule.scheduleID, IsNotNull>>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
      {
        (object) fsSrvOrdTypeRow.SrvOrdType
      }).Count == 0;
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.behavior>(cache, (object) fsSrvOrdTypeRow, flag);
  }

  public virtual void EnableDisableFields(PXCache cache, FSSrvOrdType fsSrvOrdTypeRow)
  {
    bool flag1 = ((PXSelectBase<FSAppointmentDet>) this.SrvOrdAppointmentsPosted).Current != null && ((PXSelectBase<FSAppointmentDet>) this.SrvOrdAppointmentsPosted).Current.AppointmentID.HasValue;
    bool flag2 = fsSrvOrdTypeRow.PostTo != "NA";
    fsSrvOrdTypeRow.BAccountRequired = new bool?(fsSrvOrdTypeRow.Behavior != "IN");
    fsSrvOrdTypeRow.RequireRoute = new bool?(fsSrvOrdTypeRow.Behavior == "RO");
    bool? nullable = fsSrvOrdTypeRow.BAccountRequired;
    bool flag3 = false;
    if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
      fsSrvOrdTypeRow.RequireContact = new bool?(false);
    bool flag4 = ServiceManagementSetup.IsRoomManagementActive(cache.Graph, ((PXSelectBase<FSSetup>) this.SetupRecord)?.Current);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireRoom>(cache, (object) fsSrvOrdTypeRow, flag4);
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.requireRoom>(cache, (object) fsSrvOrdTypeRow, flag4);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.salesAcctSource>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.combineSubFrom>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.dfltTermIDARSO>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.postOrderType>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXCache pxCache1 = cache;
    FSSrvOrdType fsSrvOrdType1 = fsSrvOrdTypeRow;
    int num1;
    if (flag2)
    {
      nullable = fsSrvOrdTypeRow.PostNegBalanceToAP;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.dfltTermIDAP>(pxCache1, (object) fsSrvOrdType1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.postOrderTypeNegativeBalance>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.subID>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.allowInvoiceOnlyClosedAppointment>(cache, (object) fsSrvOrdTypeRow, flag2);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.postNegBalanceToAP>(cache, (object) fsSrvOrdTypeRow, fsSrvOrdTypeRow.PostTo == "AR");
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.enableINPosting>(cache, (object) fsSrvOrdTypeRow, flag2 && PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() && fsSrvOrdTypeRow.Behavior == "RO");
    PXCache pxCache2 = cache;
    FSSrvOrdType fsSrvOrdType2 = fsSrvOrdTypeRow;
    nullable = fsSrvOrdTypeRow.BAccountRequired;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireContact>(pxCache2, (object) fsSrvOrdType2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<FSSrvOrdType.postTo>(cache, (object) fsSrvOrdTypeRow, !flag1);
    this.EnableDisableEmployeeTimeCardIntegrationFields(cache, fsSrvOrdTypeRow);
    switch (fsSrvOrdTypeRow.Behavior)
    {
      case "IN":
        fsSrvOrdTypeRow.AppAddressSource = "BL";
        PXUIFieldAttribute.SetEnabled<FSSrvOrdType.appAddressSource>(cache, (object) fsSrvOrdTypeRow, false);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireCustomerSignature>(cache, (object) fsSrvOrdTypeRow, true);
        this.SetPostingSettingVisibility(cache, fsSrvOrdTypeRow, false);
        break;
      case "RO":
        PXUIFieldAttribute.SetEnabled<FSSrvOrdType.appAddressSource>(cache, (object) fsSrvOrdTypeRow, true);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireCustomerSignature>(cache, (object) fsSrvOrdTypeRow, true);
        this.SetPostingSettingVisibility(cache, fsSrvOrdTypeRow, true);
        break;
      default:
        if (fsSrvOrdTypeRow.Behavior == "RE")
          PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireCustomerSignature>(cache, (object) fsSrvOrdTypeRow, true);
        else
          PXUIFieldAttribute.SetEnabled<FSSrvOrdType.requireCustomerSignature>(cache, (object) fsSrvOrdTypeRow, false);
        PXUIFieldAttribute.SetEnabled<FSSrvOrdType.appAddressSource>(cache, (object) fsSrvOrdTypeRow, true);
        nullable = fsSrvOrdTypeRow.RequireContact;
        bool flag5 = false;
        if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue && fsSrvOrdTypeRow.AppAddressSource == "CC")
          fsSrvOrdTypeRow.AppAddressSource = "BA";
        this.SetPostingSettingVisibility(cache, fsSrvOrdTypeRow, fsSrvOrdTypeRow.Behavior != "QT" && fsSrvOrdTypeRow.Behavior != "IN");
        break;
    }
  }

  public virtual void TurnOffInvoiceOptions(
    PXCache cache,
    FSQuickProcessParameters fsQuickProcessParametersRow)
  {
    cache.SetValueExt<FSQuickProcessParameters.prepareInvoice>((object) fsQuickProcessParametersRow, (object) false);
    cache.SetValueExt<FSQuickProcessParameters.emailSalesOrder>((object) fsQuickProcessParametersRow, (object) false);
    cache.SetValueExt<FSQuickProcessParameters.releaseInvoice>((object) fsQuickProcessParametersRow, (object) false);
    cache.SetValueExt<FSQuickProcessParameters.emailInvoice>((object) fsQuickProcessParametersRow, (object) false);
    cache.SetValueExt<FSQuickProcessParameters.sOQuickProcess>((object) fsQuickProcessParametersRow, (object) false);
  }

  public virtual void ValidatePostToByFeatures(PXCache cache, FSSrvOrdType fsSrvOrdTypeRow)
  {
    if (fsSrvOrdTypeRow.Behavior == "IN")
      return;
    SharedFunctions.ValidatePostToByFeatures<FSSrvOrdType.postTo>(cache, (object) fsSrvOrdTypeRow, fsSrvOrdTypeRow.PostTo);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.appAddressSource> e)
  {
    if (e.Row == null)
      return;
    PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>()));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.appAddressSource>, FSSrvOrdType, object>) e).NewValue = (object) "BA";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.postTo> e)
  {
    if (e.Row == null)
      return;
    if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.postTo>, FSSrvOrdType, object>) e).NewValue = (object) "SI";
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.postTo>, FSSrvOrdType, object>) e).NewValue = (object) "AR";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.appContactInfoSource> e)
  {
    if (e.Row == null)
      return;
    FSSetup fsSetup = PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>()));
    if (fsSetup != null && fsSetup.DfltAppContactInfoSource != null)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.appContactInfoSource>, FSSrvOrdType, object>) e).NewValue = (object) fsSetup.DfltAppContactInfoSource;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FSSrvOrdType, FSSrvOrdType.appContactInfoSource>, FSSrvOrdType, object>) e).NewValue = (object) "BA";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FSSrvOrdType, FSSrvOrdType.appAddressSource> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    bool? requireContact = row.RequireContact;
    bool flag = false;
    if (!(requireContact.GetValueOrDefault() == flag & requireContact.HasValue) || !((string) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FSSrvOrdType, FSSrvOrdType.appAddressSource>>) e).NewValue == "CC"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<FSSrvOrdType, FSSrvOrdType.appAddressSource>>) e).Cache.RaiseExceptionHandling<FSSrvOrdType.appAddressSource>((object) row, (object) "CC", (Exception) new PXSetPropertyException("The appointment address cannot be copied from the customer contact because the Require Contact check box is not selected.", (PXErrorLevel) 4));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.completeSrvOrdWhenSrvDone> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    bool? srvOrdWhenSrvDone = row.CloseSrvOrdWhenSrvDone;
    if (!srvOrdWhenSrvDone.GetValueOrDefault())
      return;
    srvOrdWhenSrvDone = row.CompleteSrvOrdWhenSrvDone;
    bool flag = false;
    if (!(srvOrdWhenSrvDone.GetValueOrDefault() == flag & srvOrdWhenSrvDone.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.completeSrvOrdWhenSrvDone>>) e).Cache.SetValueExt<FSSrvOrdType.closeSrvOrdWhenSrvDone>((object) e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postTo> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    if (row.SalesAcctSource == null)
      row.SalesAcctSource = "CL";
    if (row.PostTo == "NA")
      PXDefaultAttribute.SetDefault<FSSrvOrdType.combineSubFrom>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postTo>>) e).Cache, (object) row);
    this.ValidatePostToByFeatures(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postTo>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.behavior> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    row.BAccountRequired = new bool?(row.Behavior != "IN");
    row.RequireRoute = new bool?(row.Behavior == "RO");
    bool? baccountRequired = row.BAccountRequired;
    bool flag = false;
    if (baccountRequired.GetValueOrDefault() == flag & baccountRequired.HasValue)
      row.RequireContact = new bool?(false);
    if (!(row.Behavior == "QT"))
      return;
    row.RequireCustomerSignature = new bool?(false);
    row.PostTo = "NA";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postOrderType> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderType, PXSelect<PX.Objects.SO.SOOrderType, Where<PX.Objects.SO.SOOrderType.orderType, Equal<Required<FSSrvOrdType.postOrderType>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PostOrderType
    }));
    if (soOrderType == null || !(soOrderType.Behavior != "IN"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postOrderType>>) e).Cache.RaiseExceptionHandling<FSSrvOrdType.postOrderType>((object) row, (object) row.PostOrderType, (Exception) new PXSetPropertyException("The recommended option is Invoice. Otherwise, sales orders will require shipments.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.allowQuickProcess> e)
  {
    if (e.Row == null || !e.Row.AllowQuickProcess.GetValueOrDefault() || ((PXSelectBase<FSQuickProcessParameters>) this.QuickProcessSettings).Current != null)
      return;
    ((PXSelectBase<FSQuickProcessParameters>) this.QuickProcessSettings).Insert();
    if (((PXSelectBase<FSQuickProcessParameters>) this.QuickProcessSettings).Current == null)
      return;
    ((PXSelectBase<FSQuickProcessParameters>) this.QuickProcessSettings).Current.GenerateInvoiceFromServiceOrder = new bool?(true);
    ((PXSelectBase<FSQuickProcessParameters>) this.QuickProcessSettings).Current.GenerateInvoiceFromAppointment = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.billingType> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    if (!((string) e.NewValue == "CC"))
      return;
    bool? releaseIssueOnInvoice = row.ReleaseIssueOnInvoice;
    bool flag = false;
    if (!(releaseIssueOnInvoice.GetValueOrDefault() == flag & releaseIssueOnInvoice.HasValue))
      return;
    row.ReleaseIssueOnInvoice = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postToSOSIPM> e)
  {
    if (e.Row == null)
      return;
    bool? postToSosipm = e.Row.PostToSOSIPM;
    bool? oldValue = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postToSOSIPM>, FSSrvOrdType, object>) e).OldValue;
    if (postToSosipm.GetValueOrDefault() == oldValue.GetValueOrDefault() & postToSosipm.HasValue == oldValue.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postToSOSIPM>>) e).Cache.SetValuePending<FSSrvOrdType.setLotSerialNbrInAppts>((object) e.Row, PXCache.NotSetValue);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSSrvOrdType, FSSrvOrdType.postToSOSIPM>>) e).Cache.SetDefaultExt<FSSrvOrdType.setLotSerialNbrInAppts>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSrvOrdType> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSrvOrdType>>) e).Cache;
    this.EnableDisableFields(cache, row);
    this.EnableDisable_Behavior(cache, row);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.inventory>();
    PXUIFieldAttribute.SetVisible<FSSrvOrdType.setLotSerialNbrInAppts>(cache, (object) row, flag);
    FSPostTo.SetLineTypeList<FSSrvOrdType.postTo>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSrvOrdType>>) e).Cache, (object) e.Row, true, true, row.Behavior == "RE");
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSrvOrdType> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    this.CheckAppointmenAddressContactOptions(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSSrvOrdType>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSrvOrdType> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSrvOrdType> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdType row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSSrvOrdType>>) e).Cache;
    if (e.Operation == 3)
    {
      if (PXSelectBase<FSServiceOrder, PXSelectGroupBy<FSServiceOrder, Where<FSServiceOrder.srvOrdType, Equal<Required<FSServiceOrder.srvOrdType>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SrvOrdType
      }).RowCount.Value > 0)
        throw new PXException("The service order type cannot be deleted because it is assigned to at least one service order.", new object[1]
        {
          (object) row
        });
      if (PXSelectBase<FSSchedule, PXSelectGroupBy<FSSchedule, Where<FSSchedule.srvOrdType, Equal<Required<FSSchedule.srvOrdType>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.SrvOrdType
      }).RowCount.Value > 0)
        throw new PXException("The service order type cannot be deleted because it is assigned to at least one service contract.", new object[1]
        {
          (object) row
        });
    }
    if (e.Operation != 2 && e.Operation != 1)
      return;
    this.ValidatePostToByFeatures(cache, row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSrvOrdType> e)
  {
  }

  protected virtual void FSSrvOrdType_CombineSubFrom_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (e.Exception == null || !(((PXSelectBase<FSSrvOrdType>) this.CurrentSrvOrdTypeRecord).Current.PostTo == "NA"))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSSrvOrdTypeProblem> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdTypeProblem row = e.Row;
    bool flag = !row.ProblemID.HasValue;
    PXUIFieldAttribute.SetEnabled<FSSrvOrdTypeProblem.problemID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSrvOrdTypeProblem>>) e).Cache, (object) row, flag);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSSrvOrdTypeProblem> e)
  {
    if (e.Row == null)
      return;
    FSSrvOrdTypeProblem row = e.Row;
    int? problemId = row.ProblemID;
    if (PXResultset<FSSrvOrdTypeProblem>.op_Implicit(PXSelectBase<FSSrvOrdTypeProblem, PXSelect<FSSrvOrdTypeProblem, Where<FSSrvOrdTypeProblem.problemID, Equal<Required<FSSrvOrdTypeProblem.problemID>>, And<FSSrvOrdTypeProblem.srvOrdType, Equal<Current<FSSrvOrdType.srvOrdType>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) problemId
    })) == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSSrvOrdTypeProblem>>) e).Cache.RaiseExceptionHandling<FSSrvOrdTypeProblem.problemID>((object) row, (object) problemId, (Exception) new PXSetPropertyException("This ID is already in use.", (PXErrorLevel) 5));
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSSrvOrdTypeProblem> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromServiceOrder> e)
  {
    if (e.Row == null)
      return;
    FSQuickProcessParameters row = e.Row;
    int num1 = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromServiceOrder>, FSQuickProcessParameters, object>) e).OldValue ? 1 : 0;
    bool? nullable = row.GenerateInvoiceFromServiceOrder;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    if (num1 == num2 & nullable.HasValue)
      return;
    nullable = row.GenerateInvoiceFromAppointment;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    this.TurnOffInvoiceOptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromServiceOrder>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromAppointment> e)
  {
    if (e.Row == null)
      return;
    FSQuickProcessParameters row = e.Row;
    int num1 = (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromAppointment>, FSQuickProcessParameters, object>) e).OldValue ? 1 : 0;
    bool? nullable = row.GenerateInvoiceFromAppointment;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    if (num1 == num2 & nullable.HasValue)
      return;
    nullable = row.GenerateInvoiceFromServiceOrder;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    this.TurnOffInvoiceOptions(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSQuickProcessParameters, FSQuickProcessParameters.generateInvoiceFromAppointment>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSQuickProcessParameters> e)
  {
    if (e.Row == null)
      return;
    FSQuickProcessParameters row = e.Row;
    this.SetQuickProcessSettingsVisibility(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSQuickProcessParameters>>) e).Cache, ((PXSelectBase<FSSrvOrdType>) this.CurrentSrvOrdTypeRecord).Current, row);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSQuickProcessParameters> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSQuickProcessParameters> e)
  {
  }
}
