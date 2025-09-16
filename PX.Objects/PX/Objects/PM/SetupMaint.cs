// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Metadata;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM;

public class SetupMaint : PXGraph<SetupMaint>
{
  public PXSelect<PMSetup> Setup;
  public PXSave<PMSetup> Save;
  public PXCancel<PMSetup> Cancel;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSelect<PMProject, Where<PMProject.nonProject, Equal<True>>> DefaultProject;
  public PXSelect<PMCostCode, Where<PMCostCode.isDefault, Equal<True>>> DefaultCostCode;
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.itemStatus, Equal<InventoryItemStatus.unknown>>> EmptyItem;
  public CRNotificationSetupList<PMNotification> Notifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<PMNotification.setupID>>>> Recipients;
  public PXAction<PMProject> addNewProjectTemplate;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [PXDBIdentity(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.contractID> e)
  {
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Project Template ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.contractCD> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.customerID> e)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.nonProject> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.templateID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.baseCuryID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.billingCuryID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyInfoID> e)
  {
  }

  [PXDBString(30, IsUnicode = true)]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCostCode.costCodeCD> e)
  {
  }

  [PXDBIdentity(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCostCode.costCodeID> e)
  {
  }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.inventoryCD> e)
  {
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.baseUnit> e)
  {
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.salesUnit> e)
  {
  }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.purchaseUnit> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.reasonCodeSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.salesAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.salesSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.invtAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.invtSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.cOGSAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.cOGSSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.stdCstRevAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.stdCstRevSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.stdCstVarAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.stdCstVarSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.pPVAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.pPVSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.pOAccrualAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.pOAccrualSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.lCVarianceAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.lCVarianceSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.deferralAcctID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.deferralSubID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.defaultSubItemID> e)
  {
  }

  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.itemClassID> e)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.taxCategoryID> e)
  {
  }

  [PXDBString(10)]
  [PXDefault]
  [NotificationContactType.ProjectTemplateList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetupRecipient.setupID>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactType> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<EPEmployee, On<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>, And<EPEmployee.acctCD, IsNotNull>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactID> e)
  {
  }

  public SetupMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID))
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (PX.Objects.GL.Company), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Companies")
      });
    PXDefaultAttribute.SetPersistingCheck<PMProject.defaultSalesSubID>(((PXSelectBase) this.DefaultProject).Cache, (object) null, (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PMProject.defaultExpenseSubID>(((PXSelectBase) this.DefaultProject).Cache, (object) null, (PXPersistingCheck) 2);
  }

  [PXUIField]
  [PXButton]
  public virtual void AddNewProjectTemplate()
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<TemplateMaint>(), "New Project Template");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMSetup> e)
  {
    PMProject nonProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.DefaultProject).SelectWindowed(0, 1, Array.Empty<object>()));
    if (nonProject != null && this.IsInvalid(nonProject))
    {
      nonProject.IsActive = new bool?(true);
      nonProject.Status = "A";
      nonProject.RestrictToEmployeeList = new bool?(false);
      nonProject.RestrictToResourceList = new bool?(false);
      nonProject.VisibleInAP = new bool?(true);
      nonProject.VisibleInAR = new bool?(true);
      nonProject.VisibleInCA = new bool?(true);
      nonProject.VisibleInCR = new bool?(true);
      nonProject.VisibleInEA = new bool?(true);
      nonProject.VisibleInGL = new bool?(true);
      nonProject.VisibleInIN = new bool?(true);
      nonProject.VisibleInPO = new bool?(true);
      nonProject.VisibleInSO = new bool?(true);
      nonProject.VisibleInTA = new bool?(true);
      nonProject.CustomerID = new int?();
      if (((PXSelectBase) this.DefaultProject).Cache.GetStatus((object) nonProject) == null)
        ((PXSelectBase) this.DefaultProject).Cache.SetStatus((object) nonProject, (PXEntryStatus) 1);
      ((PXSelectBase) this.DefaultProject).Cache.IsDirty = true;
    }
    this.SetVisibilityToCostProjectionRows();
    this.SetVisibilityForProjectQuoteSettings();
    bool flag = e.Row != null && (e.Row.UnbilledRemainderAccountID.HasValue || e.Row.UnbilledRemainderOffsetAccountID.HasValue);
    PXUIFieldAttribute.SetRequired<PMSetup.unbilledRemainderAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, flag);
    PXUIFieldAttribute.SetRequired<PMSetup.unbilledRemainderOffsetAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, flag);
    PXUIFieldAttribute.SetRequired<PMSetup.unbilledRemainderSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, flag);
    PXUIFieldAttribute.SetRequired<PMSetup.unbilledRemainderOffsetSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, flag);
    PXDefaultAttribute.SetPersistingCheck<PMSetup.unbilledRemainderAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PMSetup.unbilledRemainderOffsetAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PMSetup.unbilledRemainderSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<PMSetup.unbilledRemainderOffsetSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    if (e.Row == null || PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    PXStringListAttribute.SetList<PMSetup.dropshipExpenseAccountSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMSetup>>) e).Cache, (object) e.Row, new string[3]
    {
      "O",
      "P",
      "T"
    }, new string[3]{ "Item", "Project", "Task" });
  }

  protected virtual void _(PX.Data.Events.RowSelected<NotificationSetup> e)
  {
    if (e.Row == null)
      return;
    if (e.Row.NotificationCD == "PROFORMA" || e.Row.NotificationCD == "CHANGE ORDER")
      PXUIFieldAttribute.SetEnabled<NotificationSetup.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSetup>>) e).Cache, (object) e.Row, false);
    else
      PXUIFieldAttribute.SetEnabled<NotificationSetup.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSetup>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMSetup, PMSetup.costCommitmentTracking> e)
  {
    PXPageCacheUtils.InvalidateCachedPages();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMSetup, PMSetup.calculateProjectSpecificTaxes> e)
  {
    if (e.Row != null && !(bool) e.NewValue)
      throw new PXSetPropertyException<PMSetup.calculateProjectSpecificTaxes>("This functionality is in use. If you clear this check box, the system will not use project-specific tax zones and addresses in project-related documents.", (PXErrorLevel) 2);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMSetup, PMSetup.dropshipReceiptProcessing> e)
  {
    if (e.Row == null || !(e.NewValue.ToString() == "S"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMSetup, PMSetup.dropshipReceiptProcessing>>) e).Cache.SetValueExt<PMSetup.dropshipExpenseRecording>((object) e.Row, (object) "B");
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMNotification.reportID> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMNotification.reportID>, object, object>) e).NewValue == "PM644000" || (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMNotification.reportID>, object, object>) e).NewValue == "PM644500")
      throw new PXSetPropertyException("The AIA Report ({0}) and AIA Report with Quantity ({1}) reports cannot be used in mailing settings. These reports are to be generated only by clicking the AIA Report button on the form toolbar of the Projects (PM301000) and Pro Forma Invoices (PM307000) forms.", new object[2]
      {
        (object) "PM644000",
        (object) "PM644500"
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMSetup, PMSetup.nonProjectCode> e)
  {
    if (e.Row == null)
      return;
    string code = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup, PMSetup.nonProjectCode>, PMSetup, object>) e).NewValue?.ToString();
    if (string.IsNullOrWhiteSpace(code))
      return;
    if (this.IsNonProjectCodeInUse(code))
      throw new PXSetPropertyException<PMSetup.nonProjectCode>("Specify another non-project code. The {0} identifier is already in use in the system.", new object[1]
      {
        (object) code
      });
    Dimension dimension = Dimension.PK.Find((PXGraph) this, "PROJECT");
    if (dimension == null)
      return;
    short? length1 = dimension.Length;
    int? nullable = length1.HasValue ? new int?((int) length1.GetValueOrDefault()) : new int?();
    int length2 = code.Length;
    if (nullable.GetValueOrDefault() < length2 & nullable.HasValue)
      throw new PXSetPropertyException<PMSetup.nonProjectCode>("The length of the non-project code cannot exceed the length of the {0} segmented key (which is {1} symbols).", (PXErrorLevel) 4, new object[2]
      {
        (object) "PROJECT",
        (object) dimension.Length
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.nonProjectCode> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.nonProjectCode>, PMSetup, object>) e).NewValue = (object) "X";
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.nonProjectCode>>) e).Cancel = true;
  }

  private bool IsNonProjectCodeInUse(string code)
  {
    return PXResultset<PX.Objects.CT.Contract>.op_Implicit(PXSelectBase<PX.Objects.CT.Contract, PXViewOf<PX.Objects.CT.Contract>.BasedOn<SelectFromBase<PX.Objects.CT.Contract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CT.Contract.contractCD, Equal<P.AsString>>>>>.And<BqlOperand<PX.Objects.CT.Contract.nonProject, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) code
    })) != null;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.stockInitRequired> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.stockInitRequired>, PMSetup, object>) e).NewValue = (object) PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();
  }

  public virtual void PMSetup_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    PMSetup row = (PMSetup) e.Row;
    if (row == null)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.DefaultProject).SelectWindowed(0, 1, Array.Empty<object>()));
    if (pmProject == null)
    {
      this.InsertDefaultProject(row);
    }
    else
    {
      pmProject.ContractCD = row.NonProjectCode;
      pmProject.IsActive = new bool?(true);
      pmProject.Status = "A";
      pmProject.VisibleInAP = new bool?(true);
      pmProject.VisibleInAR = new bool?(true);
      pmProject.VisibleInCA = new bool?(true);
      pmProject.VisibleInCR = new bool?(true);
      pmProject.VisibleInEA = new bool?(true);
      pmProject.VisibleInGL = new bool?(true);
      pmProject.VisibleInIN = new bool?(true);
      pmProject.VisibleInPO = new bool?(true);
      pmProject.VisibleInSO = new bool?(true);
      pmProject.VisibleInTA = new bool?(true);
      pmProject.RestrictToEmployeeList = new bool?(false);
      pmProject.RestrictToResourceList = new bool?(false);
      pmProject.CuryID = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
      GraphHelper.MarkUpdated(((PXSelectBase) this.DefaultProject).Cache, (object) pmProject, true);
    }
    this.EnsureDefaultCostCode(row);
    this.EnsureEmptyItem(row);
  }

  public virtual void PMSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.DefaultProject).SelectWindowed(0, 1, Array.Empty<object>()));
    PMSetup row = (PMSetup) e.Row;
    if (row == null)
      return;
    if (pmProject == null)
      this.InsertDefaultProject(row);
    else if (!sender.ObjectsEqual<PMSetup.nonProjectCode>(e.Row, e.OldRow))
    {
      pmProject.ContractCD = row.NonProjectCode;
      GraphHelper.MarkUpdated(((PXSelectBase) this.DefaultProject).Cache, (object) pmProject, true);
    }
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) this.EmptyItem).SelectWindowed(0, 1, Array.Empty<object>()));
    if (inventoryItem == null)
      this.InsertEmptyItem(row);
    else if (!sender.ObjectsEqual<PMSetup.emptyItemCode>(e.Row, e.OldRow))
    {
      inventoryItem.InventoryCD = row.EmptyItemCode;
      if (((PXSelectBase) this.EmptyItem).Cache.GetStatus((object) inventoryItem) == null)
        ((PXSelectBase) this.EmptyItem).Cache.SetStatus((object) inventoryItem, (PXEntryStatus) 1);
    }
    this.EnsureDefaultCostCode(row);
    this.EnsureEmptyItem(row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMSetup, PMSetup.dropshipExpenseRecording> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup, PMSetup.dropshipExpenseRecording>, PMSetup, object>) e).NewValue == "R") || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    INSetup inSetup = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelect<INSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if ((inSetup != null ? (!inSetup.UpdateGL.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXSetPropertyException<PMSetup.dropshipExpenseRecording>("The On Receipt Release option cannot be selected because the Update GL check box is cleared on the Inventory Preferences (IN101000) form.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMSetup, PMSetup.emptyItemCode> e)
  {
    string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup, PMSetup.emptyItemCode>, PMSetup, object>) e).NewValue as string;
    if (e.Row == null || string.IsNullOrEmpty(newValue))
      return;
    if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newValue
    })) != null)
      throw new PXSetPropertyException<PMSetup.emptyItemCode>("Specify another empty item code. The {0} identifier is already in use for an inventory item.", new object[1]
      {
        (object) newValue
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.emptyItemCode> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.emptyItemCode>, PMSetup, object>) e).NewValue = (object) "<N/A>";
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMSetup, PMSetup.emptyItemCode>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<PMSetup.emptyItemUOM> e)
  {
    if (!(e.Row is PMSetup) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup.emptyItemUOM>, object, object>) e).NewValue == null)
      return;
    if (PXResultset<INUnit>.op_Implicit(PXSelectBase<INUnit, PXViewOf<INUnit>.BasedOn<SelectFromBase<INUnit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.fromUnit, Equal<P.AsString>>>>>.And<BqlOperand<INUnit.unitType, IBqlShort>.IsEqual<INUnitType.global>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup.emptyItemUOM>, object, object>) e).NewValue
    })) == null)
      throw new PXSetPropertyException<PMSetup.emptyItemUOM>("The {0} unit of measure is not found.", new object[1]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMSetup.emptyItemUOM>, object, object>) e).NewValue
      });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMSetup> e)
  {
    PMSetup row = e.Row;
    if (row == null || row.EmptyItemUOM == null)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMSetup>>) e).Cache.VerifyFieldAndRaiseException<PMSetup.emptyItemUOM>((object) row);
  }

  public virtual bool IsInvalid(PMProject nonProject)
  {
    bool? isActive = nonProject.IsActive;
    bool flag1 = false;
    if (isActive.GetValueOrDefault() == flag1 & isActive.HasValue || nonProject.Status != "A" || nonProject.RestrictToEmployeeList.GetValueOrDefault() || nonProject.RestrictToResourceList.GetValueOrDefault())
      return true;
    bool? visibleInAp = nonProject.VisibleInAP;
    bool flag2 = false;
    if (visibleInAp.GetValueOrDefault() == flag2 & visibleInAp.HasValue)
      return true;
    bool? visibleInAr = nonProject.VisibleInAR;
    bool flag3 = false;
    if (visibleInAr.GetValueOrDefault() == flag3 & visibleInAr.HasValue)
      return true;
    bool? visibleInCa = nonProject.VisibleInCA;
    bool flag4 = false;
    if (visibleInCa.GetValueOrDefault() == flag4 & visibleInCa.HasValue)
      return true;
    bool? visibleInCr = nonProject.VisibleInCR;
    bool flag5 = false;
    if (visibleInCr.GetValueOrDefault() == flag5 & visibleInCr.HasValue)
      return true;
    bool? visibleInEa = nonProject.VisibleInEA;
    bool flag6 = false;
    if (visibleInEa.GetValueOrDefault() == flag6 & visibleInEa.HasValue)
      return true;
    bool? visibleInGl = nonProject.VisibleInGL;
    bool flag7 = false;
    if (visibleInGl.GetValueOrDefault() == flag7 & visibleInGl.HasValue)
      return true;
    bool? visibleInIn = nonProject.VisibleInIN;
    bool flag8 = false;
    if (visibleInIn.GetValueOrDefault() == flag8 & visibleInIn.HasValue)
      return true;
    bool? visibleInPo = nonProject.VisibleInPO;
    bool flag9 = false;
    if (visibleInPo.GetValueOrDefault() == flag9 & visibleInPo.HasValue)
      return true;
    bool? visibleInSo = nonProject.VisibleInSO;
    bool flag10 = false;
    if (visibleInSo.GetValueOrDefault() == flag10 & visibleInSo.HasValue)
      return true;
    bool? visibleInTa = nonProject.VisibleInTA;
    bool flag11 = false;
    return visibleInTa.GetValueOrDefault() == flag11 & visibleInTa.HasValue || nonProject.CustomerID.HasValue;
  }

  public virtual void InsertDefaultProject(PMSetup row)
  {
    PMProject pmProject = new PMProject();
    pmProject.CustomerID = new int?();
    pmProject.ContractCD = row.NonProjectCode;
    pmProject.Description = PXLocalizer.Localize("Non-Project Code.");
    PXDBLocalizableStringAttribute.SetTranslationsFromMessage<PMProject.description>(((PXGraph) this).Caches[typeof (PMProject)], (object) pmProject, "Non-Project Code.");
    pmProject.StartDate = new DateTime?(new DateTime(DateTime.Now.Year, 1, 1));
    pmProject.IsActive = new bool?(true);
    pmProject.Status = "A";
    pmProject.ServiceActivate = new bool?(false);
    pmProject.VisibleInAP = new bool?(true);
    pmProject.VisibleInAR = new bool?(true);
    pmProject.VisibleInCA = new bool?(true);
    pmProject.VisibleInCR = new bool?(true);
    pmProject.VisibleInEA = new bool?(true);
    pmProject.VisibleInGL = new bool?(true);
    pmProject.VisibleInIN = new bool?(true);
    pmProject.VisibleInPO = new bool?(true);
    pmProject.VisibleInSO = new bool?(true);
    pmProject.VisibleInTA = new bool?(true);
    ((PXSelectBase<PMProject>) this.DefaultProject).Insert(pmProject);
  }

  public virtual void EnsureDefaultCostCode(PMSetup row)
  {
    PMCostCode pmCostCode = PXResultset<PMCostCode>.op_Implicit(((PXSelectBase<PMCostCode>) this.DefaultCostCode).SelectWindowed(0, 1, Array.Empty<object>()));
    if (pmCostCode == null)
    {
      this.InsertDefaultCostCode(row);
    }
    else
    {
      if (pmCostCode.CostCodeCD.Length != (int) this.GetCostCodeLength())
      {
        pmCostCode.CostCodeCD = new string('0', (int) this.GetCostCodeLength());
        if (((PXSelectBase) this.DefaultCostCode).Cache.GetStatus((object) pmCostCode) == null)
          ((PXSelectBase) this.DefaultCostCode).Cache.SetStatus((object) pmCostCode, (PXEntryStatus) 1);
        ((PXSelectBase) this.DefaultCostCode).Cache.IsDirty = true;
      }
      if (pmCostCode.NoteID.HasValue)
        return;
      pmCostCode.NoteID = new Guid?(Guid.NewGuid());
      if (((PXSelectBase) this.DefaultCostCode).Cache.GetStatus((object) pmCostCode) == null)
        ((PXSelectBase) this.DefaultCostCode).Cache.SetStatus((object) pmCostCode, (PXEntryStatus) 1);
      ((PXSelectBase) this.DefaultCostCode).Cache.IsDirty = true;
    }
  }

  public virtual void InsertDefaultCostCode(PMSetup row)
  {
    ((PXSelectBase<PMCostCode>) this.DefaultCostCode).Insert(new PMCostCode()
    {
      CostCodeCD = new string('0', (int) this.GetCostCodeLength()),
      Description = "DEFAULT",
      IsDefault = new bool?(true)
    });
  }

  public virtual short GetCostCodeLength()
  {
    Dimension dimension = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) "COSTCODE"
    }));
    if (dimension != null)
    {
      short? length = dimension.Length;
      if (length.HasValue)
      {
        length = dimension.Length;
        return length.Value;
      }
    }
    return 4;
  }

  public virtual void EnsureEmptyItem(PMSetup row)
  {
    PX.Objects.IN.InventoryItem rec = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) this.EmptyItem).SelectWindowed(0, 1, Array.Empty<object>()));
    if (rec == null)
      this.InsertEmptyItem(row);
    else
      this.UpdateEmptyItem(rec, row);
  }

  public virtual void InsertEmptyItem(PMSetup row)
  {
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.EmptyItem).Insert(new PX.Objects.IN.InventoryItem()
    {
      InventoryCD = row.EmptyItemCode,
      ItemStatus = "XX",
      ItemType = "N",
      BaseUnit = row.EmptyItemUOM,
      SalesUnit = row.EmptyItemUOM,
      PurchaseUnit = row.EmptyItemUOM,
      StkItem = new bool?(false),
      NonStockReceipt = new bool?(false),
      TaxCalcMode = "T"
    });
  }

  protected virtual void UpdateEmptyItem(PX.Objects.IN.InventoryItem rec, PMSetup row)
  {
    if (!(rec.BaseUnit != row.EmptyItemUOM) && !(rec.SalesUnit != row.EmptyItemUOM) && !(rec.PurchaseUnit != row.EmptyItemUOM))
      return;
    rec.BaseUnit = row.EmptyItemUOM;
    rec.SalesUnit = row.EmptyItemUOM;
    rec.PurchaseUnit = row.EmptyItemUOM;
    GraphHelper.MarkUpdated(((PXSelectBase) this.EmptyItem).Cache, (object) rec, true);
  }

  private void SetVisibilityToCostProjectionRows()
  {
    bool visible1 = PXAccess.FeatureInstalled<FeaturesSet.construction>();
    bool visible2 = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() & visible1;
    this.SetSetupPropertyVisible<PMSetup.costProjectionApprovalMapID>(visible2);
    this.SetSetupPropertyVisible<PMSetup.costProjectionByDateApprovalMapID>(visible2);
    this.SetSetupPropertyVisible<PMSetup.costProjectionApprovalNotificationID>(visible2);
    this.SetSetupPropertyVisible<PMSetup.costProjectionByDateApprovalNotificationID>(visible2);
    this.SetSetupPropertyVisible<PMSetup.wipAdjustmentOverbillingSubID>(visible1);
    this.SetSetupPropertyVisible<PMSetup.wipAdjustmentUnderbillingSubID>(visible1);
    this.SetSetupPropertyVisible<PMSetup.wipAdjustmentRevenueSubID>(visible1);
  }

  private void SetVisibilityForProjectQuoteSettings()
  {
    bool visible = PXAccess.FeatureInstalled<FeaturesSet.projectQuotes>();
    this.SetSetupPropertyVisible<PMSetup.quoteApprovalMapID>(visible);
    this.SetSetupPropertyVisible<PMSetup.quoteApprovalNotificationID>(visible);
  }

  private void SetSetupPropertyVisible<TPropertyType>(bool visible) where TPropertyType : IBqlField
  {
    PXUIFieldAttribute.SetVisibility<TPropertyType>(((PXSelectBase) this.Setup).Cache, (object) null, visible ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<TPropertyType>(((PXSelectBase) this.Setup).Cache, (object) null, visible);
  }

  public virtual void Persist()
  {
    bool flag = false;
    if (((PXSelectBase<PMSetup>) this.Setup).Current != null && ((bool?) ((PXSelectBase) this.Setup).Cache.GetValueOriginal<PMSetup.calculateProjectSpecificTaxes>((object) ((PXSelectBase<PMSetup>) this.Setup).Current)).GetValueOrDefault() != ((PXSelectBase<PMSetup>) this.Setup).Current.CalculateProjectSpecificTaxes.GetValueOrDefault())
      flag = true;
    ((PXGraph) this).Persist();
    if (flag)
      PXDatabase.Update<FeaturesSet>(new PXDataFieldParam[1]
      {
        (PXDataFieldParam) new PXDataFieldAssign(typeof (FeaturesSet.projectAccounting).Name, (PXDbType) 2, (object) 1)
      });
    string screenID;
    if (!((PXGraph) this).TryGetScreenIdFor<ProformaEntry>(out screenID))
      return;
    this.ScreenInfoCacheControl.InvalidateCache(screenID);
  }
}
