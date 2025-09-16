// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SMEquipmentMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FS;

public class SMEquipmentMaint : PXGraph<
#nullable disable
SMEquipmentMaint, FSEquipment>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccount;
  [PXHidden]
  public PXSelect<FSAppointment> AppointmentRecords;
  [PXHidden]
  public PXSelect<FSAppointmentDet> AppointmentDetRecords;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  [PXViewName("Answers")]
  public CRAttributeList<FSEquipment> Answers;
  public PXSelect<FSEquipment> EquipmentRecords;
  public PXSelect<FSEquipment, Where<FSEquipment.SMequipmentID, Equal<Current<FSEquipment.SMequipmentID>>>> EquipmentSelected;
  public PXSelect<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Current<FSEquipment.SMequipmentID>>>, OrderBy<Asc<FSEquipmentComponent.lineNbr>>> EquipmentWarranties;
  public PXSelectReadonly<FSEquipmentComponent, Where<FSEquipmentComponent.SMequipmentID, Equal<Current<SMEquipmentMaint.Filter.smEquipmentID>>, And<FSEquipmentComponent.lineNbr, Equal<Current<SMEquipmentMaint.Filter.lineNbr>>>>> ComponentSelected;
  public PXFilter<SMEquipmentMaint.RComponent> ReplaceComponentInfo;
  public PXFilter<SMEquipmentMaint.Filter> compFilter;
  public PXAction<FSEquipment> replaceComponent;
  public PXAction<FSEquipment> openEmployeeBoard;
  public PXAction<FSEquipment> openUserCalendar;
  public PXAction<FSEquipment> openSource;
  public PXAction<FSEquipment> targetEquipmentInquiry;
  public PXAction<FSEquipment> resourceEquipmentInquiry;
  public PXAction<FSEquipment> report;

  [InjectDependency]
  protected PXSiteMapProvider SiteMapProvider { get; private set; }

  public SMEquipmentMaint()
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null || ((PXSelectBase<FSSetup>) this.SetupRecord).Current.EquipmentNumberingID == null)
      throw new PXSetupNotEnteredException("The equipment numbering sequence has not been specified. Specify it in the Equipment Numbering Sequence box on the {0} form.", typeof (FSEquipmentSetup), new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSEquipmentSetup))
      });
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [FSSelectorSMEquipmentRefNbr]
  [AutoNumber(typeof (Search<FSSetup.equipmentNumberingID>), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual void FSEquipment_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Name", Enabled = false)]
  protected virtual void BAccount_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  protected virtual void FSAppointment_SrvOrdType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [FSSelectorSORefNbr_Appointment]
  protected virtual void FSAppointment_SORefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Appointment Status")]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void FSAppointment_Status_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual Date", DisplayNameTime = "Actual Start Time")]
  [PXUIField]
  protected virtual void FSAppointment_ActualDateTimeBegin_CacheAttached(PXCache sender)
  {
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual Date", DisplayNameTime = "Actual End Time")]
  [PXUIField]
  protected virtual void FSAppointment_ActualDateTimeEnd_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [FSAppointmentDet.ListField_Status_AppointmentDet.List]
  [PXDefault("NS")]
  [PXUIField(DisplayName = "Detail Status")]
  protected virtual void FSAppointmentDet_Status_CacheAttached(PXCache sender)
  {
  }

  [Service(null, CacheGlobal = false)]
  protected virtual void FSAppointmentDet_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual void ReplaceComponent()
  {
    if (((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current == null)
      return;
    if (((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.ComponentReplaced.HasValue)
      throw new PXException("Component selected was already replaced.");
    SMEquipmentMaint.Filter filter = new SMEquipmentMaint.Filter()
    {
      SMequipmentID = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.SMEquipmentID,
      LineNbr = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.LineNbr
    };
    this.compFilter.Reset();
    ((PXSelectBase<SMEquipmentMaint.Filter>) this.compFilter).Current = ((PXSelectBase<SMEquipmentMaint.Filter>) this.compFilter).Insert(filter);
    if (((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).AskExt() != 1 || !this.IsTheReplacementInfoValid())
      return;
    this.ApplyComponentReplacement(((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current, new FSEquipmentComponent()
    {
      SalesDate = ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.SalesDate,
      InstallationDate = ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.InstallationDate,
      ComponentID = ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.ComponentID,
      InventoryID = ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.InventoryID,
      CpnyWarrantyDuration = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.CpnyWarrantyDuration,
      CpnyWarrantyEndDate = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.CpnyWarrantyEndDate,
      CpnyWarrantyType = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.CpnyWarrantyType,
      VendorWarrantyDuration = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.VendorWarrantyDuration,
      VendorWarrantyEndDate = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.VendorWarrantyEndDate,
      VendorWarrantyType = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.VendorWarrantyType,
      VendorID = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.VendorID,
      ItemClassID = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Current.ItemClassID
    });
  }

  [PXUIField]
  [PXButton(Category = "Scheduling")]
  public virtual void OpenEmployeeBoard()
  {
    if (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.CustomerID.HasValue && ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID.HasValue)
    {
      string str = ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.CustomerID.Value.ToString();
      throw PXRedirectToBoardRequiredException.GenerateMultiEmployeeRedirect(this.SiteMapProvider, new KeyValuePair<string, string>[2]
      {
        new KeyValuePair<string, string>(typeof (FSEquipment.customerID).Name, str),
        new KeyValuePair<string, string>(typeof (FSEquipment.SMequipmentID).Name, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID.Value.ToString())
      }, new MainAppointmentFilter()
      {
        InitialCustomerID = str
      });
    }
  }

  [PXUIField]
  [PXButton(Category = "Scheduling")]
  public virtual void OpenUserCalendar()
  {
    if (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.CustomerID.HasValue && ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID.HasValue)
    {
      KeyValuePair<string, string>[] parameters = new KeyValuePair<string, string>[2];
      string name1 = typeof (FSEquipment.customerID).Name;
      int? nullable = ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.CustomerID;
      int num = nullable.Value;
      string str1 = num.ToString();
      parameters[0] = new KeyValuePair<string, string>(name1, str1);
      string name2 = typeof (FSEquipment.SMequipmentID).Name;
      nullable = ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID;
      num = nullable.Value;
      string str2 = num.ToString();
      parameters[1] = new KeyValuePair<string, string>(name2, str2);
      throw new PXRedirectToBoardRequiredException("pages/fs/calendars/SingleEmpDispatch/FS300400.aspx", parameters);
    }
  }

  [PXUIField]
  [PXButton]
  public virtual void OpenSource()
  {
    if (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current == null)
      return;
    switch (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SourceType)
    {
      case "ARI":
        SOInvoiceEntry instance1 = PXGraph.CreateInstance<SOInvoiceEntry>();
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance1.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SourceRefNbr, new object[1]
        {
          (object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SourceDocType
        }));
        PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
        ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException1;
      case "EPE":
        EquipmentMaint instance2 = PXGraph.CreateInstance<EquipmentMaint>();
        ((PXSelectBase<EPEquipment>) instance2.Equipment).Current = PXResultset<EPEquipment>.op_Implicit(((PXSelectBase<EPEquipment>) instance2.Equipment).Search<EPEquipment.equipmentID>((object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SourceID, Array.Empty<object>()));
        PXRedirectRequiredException requiredException2 = new PXRedirectRequiredException((PXGraph) instance2, (string) null);
        ((PXBaseRedirectException) requiredException2).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException2;
      case "VEH":
        VehicleMaint instance3 = PXGraph.CreateInstance<VehicleMaint>();
        ((PXSelectBase<EPEquipment>) instance3.EPEquipmentRecords).Current = PXResultset<EPEquipment>.op_Implicit(((PXSelectBase<EPEquipment>) instance3.EPEquipmentRecords).Search<EPEquipment.equipmentID>((object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SourceID, Array.Empty<object>()));
        PXRedirectRequiredException requiredException3 = new PXRedirectRequiredException((PXGraph) instance3, (string) null);
        ((PXBaseRedirectException) requiredException3).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException3;
    }
  }

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual void TargetEquipmentInquiry() => this.OpenInquiry();

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual void ResourceEquipmentInquiry()
  {
    if (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current != null && ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID.HasValue)
    {
      AppointmentInq instance = PXGraph.CreateInstance<AppointmentInq>();
      ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Current = ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Insert(new AppointmentInq.AppointmentInqFilter());
      ((PXSelectBase<AppointmentInq.AppointmentInqFilter>) instance.Filter).Current.SMEquipmentID = ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.SMEquipmentID;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Reports")]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<FSEquipment> list = adapter.Get<FSEquipment>().ToList<FSEquipment>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      PXReportRequiredException requiredException = (PXReportRequiredException) null;
      foreach (FSEquipment fsEquipment in list)
      {
        dictionary["FSEquipment.RefNbr"] = fsEquipment.RefNbr;
        requiredException = PXReportRequiredException.CombineReport(requiredException, reportID, dictionary, (CurrentLocalization) null);
      }
      if (requiredException != null)
        throw requiredException;
    }
    return adapter.Get();
  }

  public virtual EPEquipment GetRelatedEPEquipmentRow(PXGraph graph, int? epEquipmentID)
  {
    return PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select(graph, new object[1]
    {
      (object) epEquipmentID
    }));
  }

  /// <summary>
  /// Allows to enable/disable the vehicle controls depending on the selection of the isVehicle checkbox.
  /// </summary>
  public virtual void EnableDisableVehicleControls(PXCache cache, FSEquipment fsEquipmentRow)
  {
    bool flag = fsEquipmentRow != null && fsEquipmentRow.IsVehicle.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<FSEquipment.axles>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.fuelType>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.fuelTank1>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.fuelTank2>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.grossVehicleWeight>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.maxMiles>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.tareWeight>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.weightCapacity>(cache, (object) fsEquipmentRow, flag);
    PXUIFieldAttribute.SetEnabled<FSEquipment.engineNo>(cache, (object) fsEquipmentRow, flag);
  }

  /// <summary>Enable or Disable Cache Update/Delete.</summary>
  public virtual void EnableDisableCache(PXCache cache, FSEquipment fsEquipmentRow)
  {
    cache.AllowDelete = true;
    cache.AllowUpdate = true;
    if (!(fsEquipmentRow.SourceType == "VEH"))
      return;
    cache.AllowDelete = false;
    cache.AllowUpdate = false;
  }

  /// <summary>Enable/Disable Document fields.</summary>
  public virtual void EnableDisableEquipment(PXCache cache, FSEquipment fsEquipmentRow)
  {
    PXUIFieldAttribute.SetEnabled<FSEquipment.ownerID>(cache, (object) fsEquipmentRow, fsEquipmentRow.OwnerType == "TP");
    PXUIFieldAttribute.SetEnabled<FSEquipment.resourceEquipment>(cache, (object) fsEquipmentRow, fsEquipmentRow.OwnerType == "OW");
    PXUIFieldAttribute.SetEnabled<FSEquipment.dateInstalled>(cache, (object) fsEquipmentRow, !this.AreThereAnyReplacements());
    PXUIFieldAttribute.SetEnabled<FSEquipment.inventoryID>(cache, (object) fsEquipmentRow, !this.AreThereAnyReplacements());
    PXUIFieldAttribute.SetEnabled<FSEquipment.customerID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CU");
    PXUIFieldAttribute.SetEnabled<FSEquipment.customerLocationID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CU");
    PXUIFieldAttribute.SetEnabled<FSEquipment.branchID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CO");
    PXUIFieldAttribute.SetEnabled<FSEquipment.branchLocationID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CO");
    PXUIFieldAttribute.SetVisible<FSEquipment.customerID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CU");
    PXUIFieldAttribute.SetVisible<FSEquipment.customerLocationID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CU");
    PXUIFieldAttribute.SetVisible<FSEquipment.branchID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CO");
    PXUIFieldAttribute.SetVisible<FSEquipment.branchLocationID>(cache, (object) fsEquipmentRow, fsEquipmentRow.LocationType == "CO");
    PXUIFieldAttribute.SetEnabled<FSEquipment.manufacturerID>(cache, (object) fsEquipmentRow, !fsEquipmentRow.InventoryID.HasValue);
    PXUIFieldAttribute.SetEnabled<FSEquipment.manufacturerModelID>(cache, (object) fsEquipmentRow, !fsEquipmentRow.InventoryID.HasValue);
  }

  public virtual bool AreThereAnyReplacements()
  {
    bool flag = false;
    foreach (PXResult<FSEquipmentComponent> pxResult in ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Select(Array.Empty<object>()))
    {
      if (PXResult<FSEquipmentComponent>.op_Implicit(pxResult).LastReplacementDate.HasValue)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public virtual void SetEquipmentPersistingChecksAndWarnings(
    PXCache cache,
    FSEquipment fsEquipmentRow,
    FSSetup fsSetupRow)
  {
    PXPersistingCheck pxPersistingCheck1 = fsEquipmentRow.OwnerType == "TP" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXPersistingCheck pxPersistingCheck2 = fsEquipmentRow.LocationType == "CU" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXPersistingCheck pxPersistingCheck3 = fsEquipmentRow.LocationType == "CO" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    PXUIFieldAttribute.SetRequired<FSEquipment.ownerID>(cache, fsEquipmentRow.OwnerType == "TP");
    PXUIFieldAttribute.SetRequired<FSEquipment.customerID>(cache, fsEquipmentRow.LocationType == "CU");
    PXUIFieldAttribute.SetRequired<FSEquipment.branchID>(cache, fsEquipmentRow.LocationType == "CO");
    PXDefaultAttribute.SetPersistingCheck<FSEquipment.ownerID>(cache, (object) fsEquipmentRow, pxPersistingCheck1);
    PXDefaultAttribute.SetPersistingCheck<FSEquipment.customerID>(cache, (object) fsEquipmentRow, pxPersistingCheck2);
    PXDefaultAttribute.SetPersistingCheck<FSEquipment.branchID>(cache, (object) fsEquipmentRow, pxPersistingCheck3);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (fsSetupRow != null)
    {
      if (fsSetupRow.EquipmentCalculateWarrantyFrom == "AD" && !fsEquipmentRow.DateInstalled.HasValue)
        propertyException = new PXSetPropertyException("The warranty cannot be calculated because Installation Date is empty. Specify the installation date on the General Info tab.", (PXErrorLevel) 4);
      if (fsSetupRow.EquipmentCalculateWarrantyFrom == "SD" && !fsEquipmentRow.SalesDate.HasValue)
        propertyException = new PXSetPropertyException("The warranty cannot be calculated because Sales Date is empty. Specify the sales date on the General Info tab.", (PXErrorLevel) 4);
      if ((fsSetupRow.EquipmentCalculateWarrantyFrom == "ED" || fsSetupRow.EquipmentCalculateWarrantyFrom == "LD") && !fsEquipmentRow.DateInstalled.HasValue && !fsEquipmentRow.SalesDate.HasValue)
        propertyException = new PXSetPropertyException("The warranty cannot be calculated because both Installation Date and Sales Date are empty.", (PXErrorLevel) 4);
    }
    int? nullable;
    if (fsEquipmentRow.CpnyWarrantyValue.HasValue)
    {
      nullable = fsEquipmentRow.CpnyWarrantyValue;
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        cache.RaiseExceptionHandling<FSEquipment.cpnyWarrantyEndDate>((object) fsEquipmentRow, (object) null, (Exception) propertyException);
    }
    nullable = fsEquipmentRow.VendorWarrantyValue;
    if (!nullable.HasValue)
      return;
    nullable = fsEquipmentRow.VendorWarrantyValue;
    int num1 = 0;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    cache.RaiseExceptionHandling<FSEquipment.vendorWarrantyEndDate>((object) fsEquipmentRow, (object) null, (Exception) propertyException);
  }

  /// <summary>
  /// Checks that at least one checkbox of the equipment Type is selected (Is Vehicle, Is Target Equipment, Is Resource Equipment).
  /// </summary>
  public virtual void SetRequiredEquipmentTypeError(PXCache cache, FSEquipment fsEquipmentRow)
  {
    if (fsEquipmentRow == null)
      return;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    bool? nullable = fsEquipmentRow.IsVehicle;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = fsEquipmentRow.RequireMaintenance;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = fsEquipmentRow.ResourceEquipment;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          propertyException = new PXSetPropertyException("You must select at least one option.", (PXErrorLevel) 4);
      }
    }
    cache.RaiseExceptionHandling<FSEquipment.requireMaintenance>((object) fsEquipmentRow, (object) fsEquipmentRow.RequireMaintenance, (Exception) propertyException);
    cache.RaiseExceptionHandling<FSEquipment.resourceEquipment>((object) fsEquipmentRow, (object) fsEquipmentRow.ResourceEquipment, (Exception) propertyException);
  }

  public virtual void SetValuesFromInventoryItem(PXCache cache, FSEquipment fsEquipmentRow)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, fsEquipmentRow.InventoryID);
    if (inventoryItem == null)
      return;
    FSxEquipmentModel extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItem);
    if (extension == null || !extension.EQEnabled.GetValueOrDefault())
      return;
    cache.SetValueExt<FSEquipment.manufacturerID>((object) fsEquipmentRow, (object) extension.ManufacturerID);
    cache.SetValueExt<FSEquipment.manufacturerModelID>((object) fsEquipmentRow, (object) extension.ManufacturerModelID);
    if (extension.EquipmentTypeID.HasValue)
      cache.SetValueExt<FSEquipment.equipmentTypeID>((object) fsEquipmentRow, (object) extension.EquipmentTypeID);
    cache.SetValueExt<FSEquipment.cpnyWarrantyType>((object) fsEquipmentRow, (object) extension.CpnyWarrantyType);
    cache.SetValueExt<FSEquipment.cpnyWarrantyValue>((object) fsEquipmentRow, (object) extension.CpnyWarrantyValue);
    cache.SetValueExt<FSEquipment.vendorWarrantyType>((object) fsEquipmentRow, (object) extension.VendorWarrantyType);
    cache.SetValueExt<FSEquipment.vendorWarrantyValue>((object) fsEquipmentRow, (object) extension.VendorWarrantyValue);
    this.Answers.CopyAllAttributes((object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, (object) inventoryItem);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, ((PXGraph) this).Caches[typeof (FSEquipment)], (object) ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, new bool?(false), new bool?(true));
    fsEquipmentRow.ImageUrl = inventoryItem.ImageUrl;
    if (!(extension.ModelType == "EQ"))
      return;
    this.ClearComponents();
    this.InsertEquipmentModelComponents(fsEquipmentRow, inventoryItem);
  }

  public virtual void ClearComponents()
  {
    foreach (PXResult<FSEquipmentComponent> pxResult in ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Select(Array.Empty<object>()))
      ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Delete(PXResult<FSEquipmentComponent>.op_Implicit(pxResult));
  }

  public virtual void InsertEquipmentModelComponents(
    FSEquipment fsEquipmentRow,
    PX.Objects.IN.InventoryItem inventoryItemRow)
  {
    using (IEnumerator<PXResult<FSModelComponent>> enumerator = PXSelectBase<FSModelComponent, PXSelect<FSModelComponent, Where<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>, And<FSModelComponent.active, Equal<True>, And<FSModelComponent.optional, Equal<False>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItemRow.InventoryID
    }).GetEnumerator())
    {
label_5:
      while (enumerator.MoveNext())
      {
        FSModelComponent fsModelComponent = PXResult<FSModelComponent>.op_Implicit(enumerator.Current);
        int num1 = 0;
        while (true)
        {
          int num2 = num1;
          int? qty = fsModelComponent.Qty;
          int valueOrDefault = qty.GetValueOrDefault();
          if (num2 < valueOrDefault & qty.HasValue)
          {
            FSEquipmentComponent equipmentComponent = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Insert(new FSEquipmentComponent()
            {
              ComponentID = fsModelComponent.ComponentID,
              ItemClassID = fsModelComponent.ClassID,
              InventoryID = fsModelComponent.InventoryID,
              RequireSerial = fsModelComponent.RequireSerial,
              LongDescr = fsModelComponent.Descr
            });
            ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.vendorID>((object) equipmentComponent, (object) fsModelComponent.VendorID);
            ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.vendorWarrantyDuration>((object) equipmentComponent, (object) fsModelComponent.VendorWarrantyValue);
            ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.vendorWarrantyType>((object) equipmentComponent, (object) fsModelComponent.VendorWarrantyType);
            ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.cpnyWarrantyDuration>((object) equipmentComponent, (object) fsModelComponent.CpnyWarrantyValue);
            ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.cpnyWarrantyType>((object) equipmentComponent, (object) fsModelComponent.CpnyWarrantyType);
            ++num1;
          }
          else
            goto label_5;
        }
      }
    }
  }

  public virtual void SetComponentPersistingChecksAndWarnings(
    PXCache cache,
    FSEquipmentComponent fsEquipmentComponentRow,
    FSSetup fsSetupRow)
  {
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    if (fsEquipmentComponentRow.RequireSerial.GetValueOrDefault() && fsEquipmentComponentRow.SerialNumber == null)
      propertyException1 = new PXSetPropertyException("The serial number is required.", (PXErrorLevel) 2);
    cache.RaiseExceptionHandling<FSEquipmentComponent.serialNumber>((object) fsEquipmentComponentRow, (object) null, (Exception) propertyException1);
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    if (fsSetupRow != null)
    {
      DateTime? nullable;
      if (fsSetupRow.EquipmentCalculateWarrantyFrom == "AD")
      {
        nullable = fsEquipmentComponentRow.InstallationDate;
        if (!nullable.HasValue)
          propertyException2 = new PXSetPropertyException("The warranty cannot be calculated because Installation Date is empty. Specify the installation date on the General Info tab.", (PXErrorLevel) 2);
      }
      if (fsSetupRow.EquipmentCalculateWarrantyFrom == "SD")
      {
        nullable = fsEquipmentComponentRow.SalesDate;
        if (!nullable.HasValue)
          propertyException2 = new PXSetPropertyException("The warranty cannot be calculated because Sales Date is empty. Specify the sales date on the General Info tab.", (PXErrorLevel) 2);
      }
      if (fsSetupRow.EquipmentCalculateWarrantyFrom == "ED" || fsSetupRow.EquipmentCalculateWarrantyFrom == "LD")
      {
        nullable = fsEquipmentComponentRow.InstallationDate;
        if (!nullable.HasValue)
        {
          nullable = fsEquipmentComponentRow.SalesDate;
          if (!nullable.HasValue)
            propertyException2 = new PXSetPropertyException("The warranty cannot be calculated because both Installation Date and Sales Date are empty.", (PXErrorLevel) 2);
        }
      }
    }
    int? warrantyDuration = fsEquipmentComponentRow.CpnyWarrantyDuration;
    if (warrantyDuration.HasValue)
    {
      warrantyDuration = fsEquipmentComponentRow.CpnyWarrantyDuration;
      int num = 0;
      if (!(warrantyDuration.GetValueOrDefault() == num & warrantyDuration.HasValue))
        cache.RaiseExceptionHandling<FSEquipmentComponent.cpnyWarrantyEndDate>((object) fsEquipmentComponentRow, (object) null, (Exception) propertyException2);
    }
    warrantyDuration = fsEquipmentComponentRow.VendorWarrantyDuration;
    if (!warrantyDuration.HasValue)
      return;
    warrantyDuration = fsEquipmentComponentRow.VendorWarrantyDuration;
    int num1 = 0;
    if (warrantyDuration.GetValueOrDefault() == num1 & warrantyDuration.HasValue)
      return;
    cache.RaiseExceptionHandling<FSEquipmentComponent.vendorWarrantyEndDate>((object) fsEquipmentComponentRow, (object) null, (Exception) propertyException2);
  }

  public virtual void VerifySource(PXCache cache, FSEquipment fsEquipmentRow)
  {
    if (!fsEquipmentRow.SourceID.HasValue || !(fsEquipmentRow.SourceType == "EPE"))
      return;
    if (PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelect<EPEquipment, Where<EPEquipment.equipmentID, Equal<Required<EPEquipment.equipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fsEquipmentRow.SourceID
    })) != null)
      return;
    cache.RaiseExceptionHandling<FSEquipment.refNbr>((object) fsEquipmentRow, (object) fsEquipmentRow.RefNbr, (Exception) new PXSetPropertyException("The source of this record has been deleted.", (PXErrorLevel) 2));
  }

  public virtual void EnableDisable_ActionButtons(PXCache cache, FSEquipment fsEquipmentRow)
  {
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    bool flag4 = true;
    if (fsEquipmentRow == null || cache.GetStatus((object) fsEquipmentRow) == 2)
    {
      flag1 = false;
    }
    else
    {
      if (!fsEquipmentRow.CustomerID.HasValue)
        flag2 = false;
      bool? nullable = fsEquipmentRow.RequireMaintenance;
      flag3 = nullable.GetValueOrDefault();
      nullable = fsEquipmentRow.ResourceEquipment;
      flag4 = nullable.GetValueOrDefault();
    }
    ((PXAction) this.openUserCalendar).SetEnabled(flag1 & flag2);
    ((PXAction) this.openEmployeeBoard).SetEnabled(flag1 & flag2);
    ((PXAction) this.targetEquipmentInquiry).SetEnabled(flag1 & flag3);
    ((PXAction) this.resourceEquipmentInquiry).SetEnabled(flag1 & flag4);
    ((PXAction) this.replaceComponent).SetEnabled(flag1);
  }

  public virtual void OpenInquiry()
  {
    if (((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current != null && ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.RefNbr != null)
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>()
      {
        ["TargetEquipment"] = ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current.RefNbr
      };
      string g = "6e27759d-b6ac-4105-ba3e-fa62a6cd0c67";
      throw new PXRedirectRequiredException(PXRedirectToGIWithParametersRequiredException.BuildUrl(new Guid(g), parameters), (PXGraph) PXGenericInqGrph.CreateInstance(g, (string) null, parameters, true), (string) null)
      {
        ScreenId = "FS400500"
      };
    }
  }

  public virtual void CalculateEndWarrantyDate(
    FSSetup fsSetupRow,
    FSEquipment fsEquipmentRow,
    FSEquipmentComponent fsEquipmentComponentRow,
    string warrantySource)
  {
    if (fsSetupRow == null)
      return;
    DateTime? componentSalesDate = this.GetComponentSalesDate(fsEquipmentRow, fsEquipmentComponentRow);
    DateTime? installationDate = this.GetComponentInstallationDate(fsEquipmentRow, fsEquipmentComponentRow);
    switch (fsSetupRow.EquipmentCalculateWarrantyFrom)
    {
      case "SD":
        this.SetWarrantyBySource(fsEquipmentRow, fsEquipmentComponentRow, warrantySource, componentSalesDate);
        break;
      case "AD":
        this.SetWarrantyBySource(fsEquipmentRow, fsEquipmentComponentRow, warrantySource, installationDate);
        break;
      case "ED":
      case "LD":
        DateTime? warrantyStartDate = this.GetWarrantyStartDate(componentSalesDate, installationDate, fsSetupRow.EquipmentCalculateWarrantyFrom);
        this.SetWarrantyBySource(fsEquipmentRow, fsEquipmentComponentRow, warrantySource, warrantyStartDate);
        break;
    }
  }

  public virtual DateTime? GetComponentSalesDate(
    FSEquipment fsEquipmentRow,
    FSEquipmentComponent fsEquipmentComponentRow)
  {
    if (fsEquipmentRow == null && fsEquipmentComponentRow == null)
      return new DateTime?();
    return fsEquipmentComponentRow == null ? fsEquipmentRow.SalesDate : fsEquipmentComponentRow.SalesDate;
  }

  public virtual DateTime? GetComponentInstallationDate(
    FSEquipment fsEquipmentRow,
    FSEquipmentComponent fsEquipmentComponentRow)
  {
    if (fsEquipmentRow == null && fsEquipmentComponentRow == null)
      return new DateTime?();
    return fsEquipmentComponentRow == null ? fsEquipmentRow.DateInstalled : fsEquipmentComponentRow.InstallationDate;
  }

  public virtual DateTime? GetWarrantyStartDate(
    DateTime? salesDate,
    DateTime? dateInstalled,
    string equipmentCalculateWarrantyFrom)
  {
    if (!salesDate.HasValue && !dateInstalled.HasValue)
      return new DateTime?();
    if (salesDate.HasValue && !dateInstalled.HasValue)
      return salesDate;
    if (!salesDate.HasValue && dateInstalled.HasValue)
      return dateInstalled;
    int num = DateTime.Compare(salesDate.Value, dateInstalled.Value);
    if (num < 0 && equipmentCalculateWarrantyFrom == "ED")
      return salesDate;
    if (num < 0 && equipmentCalculateWarrantyFrom == "LD" || num > 0 && equipmentCalculateWarrantyFrom == "ED")
      return dateInstalled;
    return num > 0 && equipmentCalculateWarrantyFrom == "LD" || num == 0 ? salesDate : new DateTime?();
  }

  public virtual DateTime? GetWarrantyEndDate(
    DateTime? originalDate,
    DateTime? startDate,
    int? duration,
    string warrantyDurationType)
  {
    if (duration.HasValue)
    {
      int? nullable = duration;
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        if (!startDate.HasValue)
          return new DateTime?();
        switch (warrantyDurationType)
        {
          case "M":
            return new DateTime?(startDate.Value.AddMonths(duration.Value));
          case "Y":
            return new DateTime?(startDate.Value.AddYears(duration.Value));
          case "D":
            return new DateTime?(startDate.Value.AddDays((double) duration.Value));
          default:
            return originalDate;
        }
      }
    }
    return new DateTime?();
  }

  public virtual void SetWarrantyBySource(
    FSEquipment fsEquipmentRow,
    FSEquipmentComponent fsEquipmentComponentRow,
    string warrantySource,
    DateTime? startDate)
  {
    switch (warrantySource)
    {
      case "C":
        if (fsEquipmentRow != null && fsEquipmentComponentRow == null)
          fsEquipmentRow.CpnyWarrantyEndDate = this.GetWarrantyEndDate(fsEquipmentRow.CpnyWarrantyEndDate, startDate, fsEquipmentRow.CpnyWarrantyValue, fsEquipmentRow.CpnyWarrantyType);
        if (fsEquipmentComponentRow == null)
          break;
        fsEquipmentComponentRow.CpnyWarrantyEndDate = this.GetWarrantyEndDate(fsEquipmentComponentRow.CpnyWarrantyEndDate, startDate, fsEquipmentComponentRow.CpnyWarrantyDuration, fsEquipmentComponentRow.CpnyWarrantyType);
        break;
      case "V":
        if (fsEquipmentRow != null && fsEquipmentComponentRow == null)
          fsEquipmentRow.VendorWarrantyEndDate = this.GetWarrantyEndDate(fsEquipmentRow.VendorWarrantyEndDate, startDate, fsEquipmentRow.VendorWarrantyValue, fsEquipmentRow.VendorWarrantyType);
        if (fsEquipmentComponentRow == null)
          break;
        fsEquipmentComponentRow.VendorWarrantyEndDate = this.GetWarrantyEndDate(fsEquipmentComponentRow.VendorWarrantyEndDate, startDate, fsEquipmentComponentRow.VendorWarrantyDuration, fsEquipmentComponentRow.VendorWarrantyType);
        break;
    }
  }

  public virtual bool ItemBelongsToClass(
    int? inventoryID,
    int? itemClassID,
    out int? newItemClassID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryID);
    if (inventoryItem != null)
    {
      newItemClassID = inventoryItem.ItemClassID;
      int? nullable = itemClassID;
      int? itemClassId = inventoryItem.ItemClassID;
      return nullable.GetValueOrDefault() == itemClassId.GetValueOrDefault() & nullable.HasValue == itemClassId.HasValue;
    }
    newItemClassID = new int?();
    return false;
  }

  public virtual void SetComponentValuesFromInventory(
    PXCache cache,
    FSEquipmentComponent fsEquipmentComponentRow)
  {
    if (fsEquipmentComponentRow == null || !fsEquipmentComponentRow.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, fsEquipmentComponentRow.InventoryID);
    if (inventoryItem == null)
      return;
    FSxEquipmentModel extension = PXCache<PX.Objects.IN.InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItem);
    if (extension != null)
    {
      if (extension.CpnyWarrantyType != null)
        fsEquipmentComponentRow.CpnyWarrantyType = extension.CpnyWarrantyType;
      if (extension.CpnyWarrantyValue.HasValue)
        cache.SetValueExt<FSEquipmentComponent.cpnyWarrantyDuration>((object) fsEquipmentComponentRow, (object) extension.CpnyWarrantyValue);
      if (extension.VendorWarrantyType != null)
        fsEquipmentComponentRow.VendorWarrantyType = extension.VendorWarrantyType;
      if (extension.VendorWarrantyValue.HasValue)
        cache.SetValueExt<FSEquipmentComponent.vendorWarrantyDuration>((object) fsEquipmentComponentRow, (object) extension.VendorWarrantyValue);
    }
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, fsEquipmentComponentRow.InventoryID, ((PXGraph) this).Accessinfo.BaseCuryID);
    POVendorInventory poVendorInventory = PXResultset<POVendorInventory>.op_Implicit(PXSelectBase<POVendorInventory, PXSelect<POVendorInventory, Where<POVendorInventory.inventoryID, Equal<Required<POVendorInventory.inventoryID>>, And<POVendorInventory.vendorID, Equal<Required<POVendorInventory.vendorID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
    {
      (object) inventoryItem.InventoryID,
      (object) itemCurySettings.PreferredVendorID
    }));
    if (poVendorInventory == null)
      return;
    fsEquipmentComponentRow.VendorID = poVendorInventory.VendorID;
  }

  public virtual bool IsTheReplacementInfoValid()
  {
    bool flag = true;
    if (!((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.SalesDate.HasValue)
    {
      ((PXSelectBase) this.ReplaceComponentInfo).Cache.RaiseExceptionHandling<SMEquipmentMaint.RComponent.salesDate>((object) ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current, (object) ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.SalesDate, (Exception) new PXException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SMEquipmentMaint.RComponent.salesDate>(((PXSelectBase) this.ReplaceComponentInfo).Cache)
      })));
      flag = false;
    }
    if (!((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.InstallationDate.HasValue)
    {
      ((PXSelectBase) this.ReplaceComponentInfo).Cache.RaiseExceptionHandling<SMEquipmentMaint.RComponent.installationDate>((object) ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current, (object) ((PXSelectBase<SMEquipmentMaint.RComponent>) this.ReplaceComponentInfo).Current.InstallationDate, (Exception) new PXException(PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SMEquipmentMaint.RComponent.installationDate>(((PXSelectBase) this.ReplaceComponentInfo).Cache)
      })));
      flag = false;
    }
    return flag;
  }

  public virtual FSEquipmentComponent ApplyComponentReplacement(
    FSEquipmentComponent replacedCompt,
    FSEquipmentComponent replacementCompt)
  {
    replacementCompt = ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Insert(replacementCompt);
    replacedCompt.ComponentReplaced = replacementCompt.LineNbr;
    replacedCompt.Status = "D";
    ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Update(replacedCompt);
    ((PXAction) this.Save).Press();
    return replacementCompt;
  }

  public virtual void VerifyQtyEquipmentComponents(PXCache cache, int? modelComponentID)
  {
    if (!modelComponentID.HasValue)
      return;
    bool flag = false;
    foreach (PXResult<FSModelComponent> pxResult in PXSelectBase<FSModelComponent, PXSelect<FSModelComponent, Where<FSModelComponent.modelID, Equal<Required<FSModelComponent.modelID>>>>.Config>.Select(cache.Graph, new object[1]
    {
      (object) modelComponentID
    }))
    {
      FSModelComponent fsModelComponentRow = PXResult<FSModelComponent>.op_Implicit(pxResult);
      int? qty = fsModelComponentRow.Qty;
      if (qty.HasValue)
      {
        IEnumerable<PXResult<FSEquipmentComponent>> source = ((IEnumerable<PXResult<FSEquipmentComponent>>) ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Select(Array.Empty<object>())).AsEnumerable<PXResult<FSEquipmentComponent>>().Where<PXResult<FSEquipmentComponent>>((Func<PXResult<FSEquipmentComponent>, bool>) (x =>
        {
          int? componentId1 = PXResult<FSEquipmentComponent>.op_Implicit(x).ComponentID;
          int? componentId2 = fsModelComponentRow.ComponentID;
          return componentId1.GetValueOrDefault() == componentId2.GetValueOrDefault() & componentId1.HasValue == componentId2.HasValue && PXResult<FSEquipmentComponent>.op_Implicit(x).Status == "A";
        }));
        int num = source.Count<PXResult<FSEquipmentComponent>>();
        qty = fsModelComponentRow.Qty;
        int valueOrDefault = qty.GetValueOrDefault();
        if (num > valueOrDefault & qty.HasValue)
        {
          ((PXSelectBase) this.EquipmentWarranties).Cache.RaiseExceptionHandling<FSEquipmentComponent.lineRef>((object) PXResult<FSEquipmentComponent>.op_Implicit(source.First<PXResult<FSEquipmentComponent>>()), (object) null, (Exception) new PXSetPropertyException("The quantity of this component exceeds the original quantity specified for the model equipment.", (PXErrorLevel) 4));
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXException("Some errors related to the quantity of the components occurred.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    this.SetValuesFromInventoryItem(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.inventoryID>>) e).Cache, row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.locationType> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    if (row.LocationType == "CO")
    {
      row.CustomerID = new int?();
      row.CustomerLocationID = new int?();
    }
    else
    {
      row.BranchID = new int?();
      row.BranchLocationID = new int?();
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.manufacturerID> e)
  {
    if (e.Row == null)
      return;
    e.Row.ManufacturerModelID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.manufacturerModelID> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    FSManufacturerModel manufacturerModel = (FSManufacturerModel) PXSelectorAttribute.Select<FSEquipment.manufacturerModelID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.manufacturerModelID>>) e).Cache, (object) row, (object) row.ManufacturerModelID);
    if (manufacturerModel == null)
      return;
    row.EquipmentTypeID = manufacturerModel.EquipmentTypeID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.dateInstalled> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, row, (FSEquipmentComponent) null, "C");
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, row, (FSEquipmentComponent) null, "V");
    foreach (PXResult<FSEquipmentComponent> pxResult in ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Select(Array.Empty<object>()))
      ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.installationDate>((object) PXResult<FSEquipmentComponent>.op_Implicit(pxResult), (object) row.DateInstalled);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.salesDate> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, row, (FSEquipmentComponent) null, "C");
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, row, (FSEquipmentComponent) null, "V");
    foreach (PXResult<FSEquipmentComponent> pxResult in ((PXSelectBase<FSEquipmentComponent>) this.EquipmentWarranties).Select(Array.Empty<object>()))
      ((PXSelectBase) this.EquipmentWarranties).Cache.SetValueExt<FSEquipmentComponent.salesDate>((object) PXResult<FSEquipmentComponent>.op_Implicit(pxResult), (object) row.SalesDate);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.cpnyWarrantyValue> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, e.Row, (FSEquipmentComponent) null, "C");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.cpnyWarrantyType> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, e.Row, (FSEquipmentComponent) null, "C");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.vendorWarrantyValue> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, e.Row, (FSEquipmentComponent) null, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.vendorWarrantyType> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, e.Row, (FSEquipmentComponent) null, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.equipmentTypeID> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSEquipment, FSEquipment.equipmentTypeID>>) e).Cache.SetDefaultExt<FSEquipment.equipmentTypeCD>((object) row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSEquipment> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEquipment>>) e).Cache;
    this.VerifySource(cache, row);
    this.EnableDisableCache(cache, row);
    this.EnableDisableEquipment(cache, row);
    this.SetEquipmentPersistingChecksAndWarnings(cache, row, ((PXSelectBase<FSSetup>) this.SetupRecord).Current);
    this.EnableDisableVehicleControls(cache, row);
    this.EnableDisable_ActionButtons(cache, row);
    this.SetRequiredEquipmentTypeError(cache, row);
    if (!(row.LocationType == "CU"))
      return;
    row.BranchID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSEquipment> e)
  {
    if (e.Row == null)
      return;
    if (((IQueryable<PXResult<PX.Objects.SO.SOLine>>) PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<FSxSOLine.sMEquipmentID, Equal<Required<FSEquipment.SMequipmentID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SMEquipmentID
    })).Any<PXResult<PX.Objects.SO.SOLine>>())
    {
      e.Cancel = true;
      throw new PXSetPropertyException<CSAttributeGroup.attributeCategory>("Equipment {0} cannot be deleted because it is referenced in multiple tables. For details, see trace.", new object[1]
      {
        (object) e.Row.RefNbr
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSEquipment> e)
  {
    if (e.Row == null)
      return;
    FSEquipment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FSEquipment>>) e).Cache;
    if (e.Operation != 2 && e.Operation != 1)
      return;
    this.VerifyQtyEquipmentComponents(cache, row.InventoryID);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSEquipment> e)
  {
    if (e.Row == null || e.TranStatus != null)
      return;
    FSEquipment row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<FSEquipment>>) e).Cache;
    if (!(row.SourceType == "EPE") && !(row.SourceType == "VEH") || !row.SourceID.HasValue)
      return;
    EPEquipment relatedEpEquipmentRow = this.GetRelatedEPEquipmentRow(cache.Graph, row.SourceID);
    PXCache<EPEquipment> cacheEPEquipment = new PXCache<EPEquipment>(cache.Graph);
    if (!EquipmentHelper.UpdateEPEquipmentWithFSEquipment((PXCache) cacheEPEquipment, relatedEpEquipmentRow, cache, row))
      return;
    cacheEPEquipment.Update(relatedEpEquipmentRow);
    ((PXCache) cacheEPEquipment).Persist((PXDBOperation) 1);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FSEquipmentComponent, FSEquipmentComponent.cpnyWarrantyDuration> e)
  {
    if (e.Row == null)
      return;
    int? newValue = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FSEquipmentComponent, FSEquipmentComponent.cpnyWarrantyDuration>, FSEquipmentComponent, object>) e).NewValue;
    int num = 0;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue)
      throw new PXSetPropertyException("A warranty duration cannot be less than zero.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.installationDate> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, (FSEquipment) null, row, "C");
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, (FSEquipment) null, row, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.salesDate> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, (FSEquipment) null, row, "C");
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, (FSEquipment) null, row, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.componentID> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    if (!row.ComponentID.HasValue)
      return;
    FSModelComponent fsModelComponent = PXResultset<FSModelComponent>.op_Implicit(PXSelectBase<FSModelComponent, PXSelect<FSModelComponent, Where<FSModelComponent.modelID, Equal<Current<FSEquipment.inventoryID>>, And<FSModelComponent.componentID, Equal<Required<FSModelComponent.componentID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ComponentID
    }));
    row.LongDescr = fsModelComponent.Descr;
    row.ItemClassID = fsModelComponent.ClassID;
    row.CpnyWarrantyDuration = fsModelComponent.CpnyWarrantyValue;
    row.CpnyWarrantyType = fsModelComponent.CpnyWarrantyType;
    row.VendorWarrantyDuration = fsModelComponent.VendorWarrantyValue;
    row.VendorWarrantyType = fsModelComponent.VendorWarrantyType;
    row.VendorID = fsModelComponent.VendorID;
    row.RequireSerial = fsModelComponent.RequireSerial;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, row, "C");
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, row, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.cpnyWarrantyDuration> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    if (!row.CpnyWarrantyDuration.HasValue)
      row.CpnyWarrantyEndDate = new DateTime?();
    else
      this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, row, "C");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.cpnyWarrantyType> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, e.Row, "C");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.vendorWarrantyDuration> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    if (!row.VendorWarrantyDuration.HasValue)
      row.VendorWarrantyEndDate = new DateTime?();
    else
      this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, row, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.vendorWarrantyType> e)
  {
    if (e.Row == null)
      return;
    this.CalculateEndWarrantyDate(((PXSelectBase<FSSetup>) this.SetupRecord).Current, ((PXSelectBase<FSEquipment>) this.EquipmentRecords).Current, e.Row, "V");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.itemClassID> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    int? nullable1 = row.InventoryID;
    if (!nullable1.HasValue || this.ItemBelongsToClass(row.InventoryID, row.ItemClassID, out int? _))
      return;
    FSEquipmentComponent equipmentComponent = row;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    equipmentComponent.InventoryID = nullable2;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.inventoryID> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    int? newItemClassID;
    if (row.InventoryID.HasValue && !this.ItemBelongsToClass(row.InventoryID, row.ItemClassID, out newItemClassID))
      row.ItemClassID = newItemClassID;
    this.SetComponentValuesFromInventory(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSEquipmentComponent, FSEquipmentComponent.inventoryID>>) e).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSEquipmentComponent> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSEquipmentComponent.itemClassID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEquipmentComponent>>) e).Cache, (object) row, !row.ItemClassID.HasValue);
    this.SetComponentPersistingChecksAndWarnings(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEquipmentComponent>>) e).Cache, row, ((PXSelectBase<FSSetup>) this.SetupRecord).Current);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSEquipmentComponent> e)
  {
    if (e.Row == null)
      return;
    FSEquipmentComponent row = e.Row;
    if (row.LineRef != null)
      return;
    row.LineRef = row.LineNbr.Value.ToString("00000");
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSEquipmentComponent> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSEquipmentComponent> e)
  {
  }

  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    public virtual int? SMequipmentID { get; set; }

    [PXInt]
    public virtual int? LineNbr { get; set; }

    public abstract class smEquipmentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SMEquipmentMaint.Filter.smEquipmentID>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SMEquipmentMaint.Filter.lineNbr>
    {
    }
  }

  public class RComponent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    [PXUIField(DisplayName = "Installation Date", Required = true)]
    public virtual DateTime? InstallationDate { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Sales Date", Required = true)]
    public virtual DateTime? SalesDate { get; set; }

    [PXInt]
    [PXDefault(typeof (FSEquipmentComponent.componentID))]
    [FSSelectorComponentIDEquipment]
    [PXUIField(DisplayName = "Component ID", Required = true)]
    public virtual int? ComponentID { get; set; }

    [PXInt]
    [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where2<Where<Current<FSEquipmentComponent.itemClassID>, IsNotNull, And<PX.Objects.IN.InventoryItem.itemClassID, Equal<Current<FSEquipmentComponent.itemClassID>>, Or<Where<Current<FSEquipmentComponent.itemClassID>, IsNull>>>>, And<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
    [PXUIField(DisplayName = "Inventory ID")]
    public virtual int? InventoryID { get; set; }

    public abstract class installationDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SMEquipmentMaint.RComponent.installationDate>
    {
    }

    public abstract class salesDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SMEquipmentMaint.RComponent.salesDate>
    {
    }

    public abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SMEquipmentMaint.RComponent.componentID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SMEquipmentMaint.RComponent.inventoryID>
    {
    }
  }
}
