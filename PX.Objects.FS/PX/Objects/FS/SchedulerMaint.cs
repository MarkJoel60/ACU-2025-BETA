// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using Autofac;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.FS;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.FS.Interfaces;
using PX.Objects.FS.Services;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.FS;

public class SchedulerMaint : PXGraph<
#nullable disable
SchedulerMaint>
{
  private const int MAXIMUM_ROWS = 2000;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.BAccount>.View BAccounts;
  [PXHidden]
  public FbqlSelect<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>, BAccountR>.View BAccountsR;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.GL.Branch>.View Branches;
  [PXHidden]
  public FbqlSelect<SelectFromBase<BranchAlias, TypeArrayOf<IFbqlJoin>.Empty>, BranchAlias>.View BranchAliases;
  public FbqlSelect<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>, FSServiceOrder>.View AllServiceOrders;
  public FbqlSelect<SelectFromBase<FSAppointment, TypeArrayOf<IFbqlJoin>.Empty>, FSAppointment>.View AllAppointments;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Location>.View AllLocations;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.CR.Contact>.View AllContacts;
  public FbqlSelect<SelectFromBase<FSRoom, TypeArrayOf<IFbqlJoin>.Empty>, FSRoom>.View AllRooms;
  public FbqlSelect<SelectFromBase<FSServiceContract, TypeArrayOf<IFbqlJoin>.Empty>, FSServiceContract>.View AllContracts;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.InventoryItem>.View InventoryItems;
  public FbqlSelect<SelectFromBase<FSSetup, TypeArrayOf<IFbqlJoin>.Empty>, FSSetup>.View Setup;
  [PXHidden]
  public PXFilter<SchedulerDatesFilter> DatesFilter;
  [PXHidden]
  public PXFilter<SchedulerInitData> InitData;
  [PXHidden]
  public PXFilter<SchedulerSOFilter> SOFilter;
  [PXHidden]
  public PXFilter<PX.Objects.FS.LastUpdatedAppointmentFilter> LastUpdatedAppointmentFilter;
  public PXFilter<SchedulerAppointmentFilter> AppointmentFilter;
  public PXFilter<SchedulerDevicesFilter> DevicesFilter;
  [PXFilterable(new System.Type[] {})]
  public PXViewOf<SchedulerServiceOrder>.BasedOn<SelectFromBase<SchedulerServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODet>.On<BqlOperand<
  #nullable enable
  FSSODet.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.sOID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSSODet.inventoryID>>>, FbqlJoins.Left<INItemClass>.On<BqlOperand<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.IN.InventoryItem.itemClassID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSSODet.inventoryID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSSODet.inventoryID>>>, FbqlJoins.Left<FSSOEmployee>.On<BqlOperand<
  #nullable enable
  FSSOEmployee.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.sOID>>>>>.ReadOnly ServiceOrders;
  [PXHidden]
  public PXViewOf<SchedulerServiceOrder>.BasedOn<SelectFromBase<SchedulerServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODet>.On<BqlOperand<
  #nullable enable
  FSSODet.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.sOID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSSODet.inventoryID>>>, FbqlJoins.Left<INItemClass>.On<BqlOperand<
  #nullable enable
  INItemClass.itemClassID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.IN.InventoryItem.itemClassID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSSODet.lineType, 
  #nullable disable
  Equal<ListField_LineType_UnifyTabs.Service>>>>, And<BqlOperand<
  #nullable enable
  FSSODet.status, IBqlString>.IsNotEqual<
  #nullable disable
  FSSODet.ListField_Status_SODet.Completed>>>, And<BqlOperand<
  #nullable enable
  FSSODet.status, IBqlString>.IsNotEqual<
  #nullable disable
  FSSODet.ListField_Status_SODet.Canceled>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.quote, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.hold, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.closed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.canceled, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.completed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.appointmentsNeeded, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>.And<Where<Exists<SelectFromBase<FSSODetAlias, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSSODetAlias.sOID, 
  #nullable disable
  Equal<SchedulerServiceOrder.sOID>>>>, And<BqlOperand<
  #nullable enable
  FSSODetAlias.status, IBqlString>.IsEqual<
  #nullable disable
  FSSODet.ListField_Status_SODet.ScheduleNeeded>>>>.And<BqlOperand<
  #nullable enable
  FSSODet.lineType, IBqlString>.IsEqual<
  #nullable disable
  ListField_LineType_UnifyTabs.Service>>>>>>>>.ReadOnly ServiceOrdersDelegateQuery;
  public PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.defContactID>>>, FbqlJoins.Inner<FSSOEmployee>.On<BqlOperand<
  #nullable enable
  FSSOEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>>.Where<BqlOperand<
  #nullable enable
  FSSOEmployee.sOID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerSOFilter.sOID, IBqlInt>.FromCurrent>>>.ReadOnly SelectedSOEmployees;
  [PXFilterable(new System.Type[] {})]
  public 
  #nullable disable
  PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.employeeID>>>, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.employeeID>>>, FbqlJoins.Left<BranchAlias>.On<BqlOperand<
  #nullable enable
  BranchAlias.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.parentBAccountID>>>>>.ReadOnly SearchAppointments;
  [PXHidden]
  public PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>>.Aggregate<To<GroupBy<SchedulerAppointment.appointmentID>>>>.ReadOnly SearchAppointmentsBaseDelegate;
  [PXHidden]
  public PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.searchAppointmentID, IBqlInt>.FromCurrent>>>.ReadOnly HighlightedSearchAppointment;
  public 
  #nullable disable
  PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.appointmentID, IBqlInt>.FromCurrent>>>.ReadOnly SelectedAppointment;
  public 
  #nullable disable
  PXViewOf<SchedulerServiceOrder>.BasedOn<SelectFromBase<SchedulerServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerServiceOrder.sOID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  SchedulerSOFilter.sOID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  Match<Current<AccessInfo.userName>>>>>.ReadOnly SelectedSO;
  public PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.defContactID>>>, FbqlJoins.Inner<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>>.Where<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.appointmentID, IBqlInt>.FromCurrent>>>.ReadOnly SelectedAppointmentEmployees;
  public 
  #nullable disable
  PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Inner<SchedulerAppointment>.On<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.appointmentID>>>, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<
  #nullable enable
  FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<
  #nullable enable
  FSSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceSkill.skillID>>>>.Where<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.LastUpdatedAppointmentFilter.appointmentID, IBqlInt>.FromCurrent>>>.ReadOnly LastUpdatedAppointment;
  [PXHidden]
  public 
  #nullable disable
  PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.employeeID>>>, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.employeeID>>>>.Where<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.LastUpdatedAppointmentFilter.appointmentID, IBqlInt>.FromCurrent>>>.ReadOnly LastUpdatedAppointment_Reversed;
  public 
  #nullable disable
  PXViewOf<SchedulerSuitableEmployee>.BasedOn<SelectFromBase<SchedulerSuitableEmployee, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly SuitableEmployees;
  [PXFilterable(new System.Type[] {})]
  public PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.bAccountID>>>, FbqlJoins.Left<FSEmployeeSkill>.On<BqlOperand<
  #nullable enable
  FSEmployeeSkill.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeSkill>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSEmployeeSkill.skillID>>>, FbqlJoins.Left<FSLicense>.On<BqlOperand<
  #nullable enable
  FSLicense.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeLicenseType>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSLicense.licenseTypeID>>>, FbqlJoins.Left<FSGeoZoneEmp>.On<BqlOperand<
  #nullable enable
  FSGeoZoneEmp.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeGeoZone>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZoneEmp.geoZoneID>>>, FbqlJoins.Left<BranchAlias>.On<BqlOperand<
  #nullable enable
  BranchAlias.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.BAccount.parentBAccountID>>>, FbqlJoins.Left<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerAppointment>.On<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.appointmentID>>>, FbqlJoins.Left<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<SchedulerEmployeeInventoryItem>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeInventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>>>.ReadOnly AppointmentsAllStaff;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.bAccountID>>>, FbqlJoins.Left<FSEmployeeSkill>.On<BqlOperand<
  #nullable enable
  FSEmployeeSkill.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeSkill>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSEmployeeSkill.skillID>>>, FbqlJoins.Left<FSLicense>.On<BqlOperand<
  #nullable enable
  FSLicense.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeLicenseType>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSLicense.licenseTypeID>>>, FbqlJoins.Left<FSGeoZoneEmp>.On<BqlOperand<
  #nullable enable
  FSGeoZoneEmp.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Left<SchedulerEmployeeGeoZone>.On<BqlOperand<
  #nullable enable
  SchedulerEmployeeGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZoneEmp.geoZoneID>>>, FbqlJoins.Left<BranchAlias>.On<BqlOperand<
  #nullable enable
  BranchAlias.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CR.BAccount.parentBAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AP.Vendor.vStatus, 
  #nullable disable
  NotEqual<VendorStatus.inactive>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSxEPEmployee.sDEnabled, 
  #nullable disable
  Equal<True>>>>>.Or<BqlOperand<
  #nullable enable
  FSxVendor.sDEnabled, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>.Aggregate<To<GroupBy<PX.Objects.AP.Vendor.bAccountID>>>, PX.Objects.AP.Vendor>.View AppointmentsAllStaff_Employees;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.bAccountID>>>, FbqlJoins.Left<FSTimeSlot>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSTimeSlot.employeeID, 
  #nullable disable
  Equal<PX.Objects.AP.Vendor.bAccountID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSTimeSlot.timeStart, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  SchedulerDatesFilter.dateEnd, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  FSTimeSlot.timeEnd, IBqlDateTime>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerDatesFilter.dateBegin, IBqlDateTime>.FromCurrent>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  FSTimeSlot.slotLevel, IBqlInt>.IsNotEqual<
  #nullable disable
  Zero>>>>.And<BqlOperand<
  #nullable enable
  FSTimeSlot.scheduleType, IBqlString>.IsEqual<
  #nullable disable
  ListField_ScheduleType.Availability>>>>>>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsIn<
  #nullable disable
  P.AsInt>>, PX.Objects.AP.Vendor>.View AppointmentsAllStaff_TimeSlots;
  [PXHidden]
  public PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Inner<SchedulerAppointment>.On<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.appointmentID>>>, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<
  #nullable enable
  FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<
  #nullable enable
  FSSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceSkill.skillID>>>, FbqlJoins.Left<EPEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.bAccountID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerAppointment.scheduledDateTimeBegin, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  SchedulerDatesFilter.dateEnd, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  SchedulerAppointment.scheduledDateTimeBegin, IBqlDateTime>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerDatesFilter.dateBegin, IBqlDateTime>.FromCurrent>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerAppointment.isVisible, 
  #nullable disable
  Equal<True>>>>>.Or<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.searchAppointmentID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsIn<
  #nullable disable
  P.AsInt>>>>.Aggregate<To<GroupBy<SchedulerAppointment.appointmentID>>>>.ReadOnly AppointmentsAllStaff_Appointments;
  [PXHidden]
  public PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.AP.Vendor.bAccountID>>>, FbqlJoins.Inner<SchedulerAppointment>.On<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointmentEmployee.appointmentID>>>, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<
  #nullable enable
  FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<
  #nullable enable
  FSSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceSkill.skillID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerAppointment.appointmentID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  SchedulerAppointmentFilter.searchAppointmentID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsIn<
  #nullable disable
  P.AsInt>>>>.ReadOnly AppointmentsAllStaff_HighlightedAppointment;
  [PXHidden]
  public PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<
  #nullable enable
  FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<
  #nullable enable
  FSSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceSkill.skillID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerAppointment.scheduledDateTimeBegin, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  SchedulerDatesFilter.dateEnd, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  SchedulerAppointment.scheduledDateTimeBegin, IBqlDateTime>.IsGreaterEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerDatesFilter.dateBegin, IBqlDateTime>.FromCurrent>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, IBqlInt>.IsNull>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<
  #nullable enable
  SchedulerAppointment.canceled, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerAppointment.completed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, And<BqlOperand<
  #nullable enable
  SchedulerAppointment.closed, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Or<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.searchAppointmentID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  Match<Current<AccessInfo.userName>>>>.Aggregate<To<GroupBy<SchedulerAppointment.appointmentID>>>>.ReadOnly AppointmentsAllStaff_NoEmployee;
  [PXHidden]
  public PXViewOf<SchedulerAppointment>.BasedOn<SelectFromBase<SchedulerAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SchedulerAppointmentEmployee>.On<BqlOperand<
  #nullable enable
  SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<
  #nullable enable
  SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.sOID>>>, FbqlJoins.Left<FSAppointmentDet>.On<BqlOperand<
  #nullable enable
  FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  SchedulerAppointment.appointmentID>>>, FbqlJoins.Left<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSGeoZonePostalCode>.On<BqlOperand<
  #nullable enable
  FSGeoZonePostalCode.postalCode, IBqlString>.IsEqual<
  #nullable disable
  SchedulerServiceOrder.postalCode>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<
  #nullable enable
  FSGeoZone.geoZoneID, IBqlInt>.IsEqual<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>>>, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<
  #nullable enable
  FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<
  #nullable enable
  FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<
  #nullable enable
  FSServiceSkill.serviceID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointmentDet.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<
  #nullable enable
  FSSkill.skillID, IBqlInt>.IsEqual<
  #nullable disable
  FSServiceSkill.skillID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SchedulerAppointmentEmployee.employeeID, 
  #nullable disable
  IsNull>>>, And<BqlOperand<
  #nullable enable
  SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SchedulerAppointmentFilter.searchAppointmentID, IBqlInt>.FromCurrent>>>>.And<
  #nullable disable
  Match<Current<AccessInfo.userName>>>>>.ReadOnly AppointmentsAllStaff_NoEmployeeHighlighted;
  public PXSelectJoin<UserPreferences, InnerJoin<EPEmployee, On<EPEmployee.userID, Equal<UserPreferences.userID>>>, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>> UserPreferencesByEmployee;
  public PXViewOf<SchedulerTrackingHistory>.BasedOn<SelectFromBase<SchedulerTrackingHistory, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly EmployeeDevices;
  public FbqlSelect<SelectFromBase<FSAppointment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  FSAppointment.customerID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>.And<BqlOperand<
  #nullable enable
  FSAppointment.appointmentID, IBqlInt>.IsEqual<
  #nullable disable
  P.AsInt>>>, FSAppointment>.View AppointmentById;
  [PXHidden]
  public FbqlSelect<SelectFromBase<FSServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FSServiceOrder.srvOrdType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.srvOrdType, IBqlString>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  FSServiceOrder.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.sORefNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  FSServiceOrder>.View ServiceOrderByFilter;
  [PXHidden]
  public FbqlSelect<SelectFromBase<FSSrvOrdType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  FSSrvOrdType.srvOrdType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.srvOrdType, IBqlString>.FromCurrent>>, 
  #nullable disable
  FSSrvOrdType>.View ServiceOrderTypeByFilter;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.customerID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, 
  #nullable disable
  IsNull>>>>.Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, PX.Objects.AR.Customer>.View CustomerByFilter;
  public PXFilter<PX.Objects.FS.MainAppointmentFilter> MainAppointmentFilter;
  public PXFilter<PX.Objects.FS.MainAppointmentFilter> MainAppointmentFilterBase;
  public FbqlSelect<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.locationID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PX.Objects.CR.Location>.View EditedAppointmentLocation;
  public FbqlSelect<SelectFromBase<FSContact, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  FSContact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.FS.MainAppointmentFilter.contactID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  FSContact>.View EditedAppointmentContact;
  public PXViewOf<EPEmployee>.BasedOn<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  EPEmployee.defContactID>>>>.Where<BqlOperand<
  #nullable enable
  EPEmployee.bAccountID, IBqlInt>.IsIn<
  #nullable disable
  P.AsInt>>>.ReadOnly EditedAppointmentEmployees;
  public PXAction<PX.Objects.FS.MainAppointmentFilter> ScheduleAppointment;
  [PXHidden]
  public PXFilter<UpdateAppointmentFilter> UpdatedAppointment;
  public PXAction<PX.Objects.FS.MainAppointmentFilter> UpdateAppointment;
  public PXAction<PX.Objects.FS.MainAppointmentFilter> cloneAppointment;
  public PXAction<PX.Objects.FS.MainAppointmentFilter> DeleteAppointment;

  [InjectDependency]
  private ISchedulerDataHandler _dataHandler { get; set; }

  protected virtual IEnumerable datesFilter()
  {
    SchedulerDatesFilter current = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current;
    if ((current != null ? (!current.PeriodKind.HasValue ? 1 : 0) : 1) != 0)
    {
      ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current = this._dataHandler.LoadDatesFilter(PXContext.GetScreenID());
      ((PXSelectBase) this.DatesFilter).Cache.SetStatus((object) ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current, (PXEntryStatus) 0);
    }
    return (IEnumerable) new SchedulerDatesFilter[1]
    {
      ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current
    };
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SchedulerDatesFilter> e)
  {
    SchedulerDatesFilter current1 = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current;
    DateTime? nullable1 = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateBegin;
    ref DateTime? local1 = ref nullable1;
    DateTime valueOrDefault;
    DateTime? nullable2;
    if (!local1.HasValue)
    {
      nullable2 = new DateTime?();
    }
    else
    {
      valueOrDefault = local1.GetValueOrDefault();
      nullable2 = new DateTime?(valueOrDefault.ToUniversalTime());
    }
    current1.DateBegin = nullable2;
    SchedulerDatesFilter current2 = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current;
    nullable1 = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateEnd;
    ref DateTime? local2 = ref nullable1;
    DateTime? nullable3;
    if (!local2.HasValue)
    {
      nullable3 = new DateTime?();
    }
    else
    {
      valueOrDefault = local2.GetValueOrDefault();
      nullable3 = new DateTime?(valueOrDefault.ToUniversalTime());
    }
    current2.DateEnd = nullable3;
  }

  protected virtual void _(PX.Data.Events.RowInserted<SchedulerInitData> e)
  {
    ((PXSelectBase<SchedulerInitData>) this.InitData).Current.MapAPIKey = SharedFunctions.GetMapApiKey((PXGraph) this);
    ((PXSelectBase<SchedulerInitData>) this.InitData).Current.EnableGPSTracking = SharedFunctions.IsGPSTrackingEnabled((PXGraph) this);
    ((PXSelectBase<SchedulerInitData>) this.InitData).Current.GPSRefreshTrackingTime = SharedFunctions.GetGPSRefreshTrackingTime((PXGraph) this);
  }

  protected virtual IEnumerable serviceOrders()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    int num1 = 0;
    int num2 = 0;
    BqlCommand bqlCommand = ((PXSelectBase) this.ServiceOrdersDelegateQuery).View.BqlSelect;
    object[] objArray = new object[0];
    PXFilterRow[] filters = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    PXFilterRow[] pxFilterRowArray1 = filters.ForDACs(new string[4]
    {
      "FSServiceLicenseType",
      "FSServiceSkill",
      "FSLicenseType",
      "FSSkill"
    });
    if (pxFilterRowArray1.Length != 0)
    {
      IEnumerable<PX.Objects.IN.InventoryItem> source = GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) ((PXSelectBase) new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<FSServiceSkill.serviceID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<FSSkill.skillID, IBqlInt>.IsEqual<FSServiceSkill.skillID>>>>>.ReadOnly((PXGraph) this)).View.Select(PXView.Currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray1, ref num1, PXView.MaximumRows, ref num2));
      objArray = EnumerableExtensions.Append<object>(objArray, (object[]) new object[1][]
      {
        source.Select<PX.Objects.IN.InventoryItem, object>((Func<PX.Objects.IN.InventoryItem, object>) (obj => (object) obj.InventoryID)).ToArray<object>()
      });
      bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<FSSODet.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
    }
    PXFilterRow[] pxFilterRowArray2 = filters.ForDACs(new string[5]
    {
      "FSSOEmployee",
      "EPEmployee",
      "Vendor",
      "FSGeoZone",
      "BranchAlias"
    });
    if (pxFilterRowArray2.Length != 0)
    {
      IEnumerable<FSSOEmployee> source = GraphHelper.RowCast<FSSOEmployee>((IEnumerable) ((PXSelectBase) new PXViewOf<FSSOEmployee>.BasedOn<SelectFromBase<FSSOEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<FSSOEmployee.employeeID>>>, FbqlJoins.Left<EPEmployee>.On<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<FSSOEmployee.employeeID>>>, FbqlJoins.Left<FSGeoZoneEmp>.On<BqlOperand<FSGeoZoneEmp.employeeID, IBqlInt>.IsEqual<EPEmployee.bAccountID>>>, FbqlJoins.Left<FSGeoZone>.On<BqlOperand<FSGeoZone.geoZoneID, IBqlInt>.IsEqual<FSGeoZoneEmp.geoZoneID>>>, FbqlJoins.Left<BranchAlias>.On<BqlOperand<BranchAlias.bAccountID, IBqlInt>.IsEqual<EPEmployee.parentBAccountID>>>>>.ReadOnly((PXGraph) this)).View.Select(PXView.Currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray2, ref num1, PXView.MaximumRows, ref num2));
      objArray = EnumerableExtensions.Append<object>(objArray, (object[]) new object[1][]
      {
        source.Select<FSSOEmployee, object>((Func<FSSOEmployee, object>) (obj => (object) obj.EmployeeID)).ToArray<object>()
      });
      bqlCommand = bqlCommand.WhereAnd<Where<Exists<SelectFromBase<FSSOEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSOEmployee.employeeID, In<P.AsInt>>>>>.And<BqlOperand<FSSOEmployee.sOID, IBqlInt>.IsEqual<SchedulerServiceOrder.sOID>>>>>>();
    }
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    int startRow = PXView.StartRow;
    int num3 = 0;
    int num4 = startRow;
    PXFilterRow[] pxFilterRowArray3 = filters.ExceptForDACs(new string[9]
    {
      "FSServiceLicenseType",
      "FSServiceSkill",
      "FSLicenseType",
      "FSSkill",
      "FSSOEmployee",
      "EPEmployee",
      "Vendor",
      "FSGeoZone",
      "BranchAlias"
    });
    using (new PXFieldScope(pxView, (IEnumerable<System.Type>) this.GetSOFieldScopeFields(), true))
    {
      List<object> collection = pxView.Select(PXView.Currents, objArray, PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowArray3, ref num4, PXView.MaximumRows, ref num3);
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable searchAppointments()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    int num1 = 0;
    int num2 = 0;
    BqlCommand bqlCommand = ((PXSelectBase) this.SearchAppointmentsBaseDelegate).View.BqlSelect;
    object[] objArray = new object[0];
    PXFilterRow[] filters = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    PXFilterRow[] pxFilterRowArray1 = filters.ForDACs(new string[4]
    {
      "FSServiceLicenseType",
      "FSServiceSkill",
      "FSLicenseType",
      "FSSkill"
    });
    if (pxFilterRowArray1.Length != 0)
    {
      IEnumerable<PX.Objects.IN.InventoryItem> source = GraphHelper.RowCast<PX.Objects.IN.InventoryItem>((IEnumerable) ((PXSelectBase) new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FSServiceLicenseType>.On<BqlOperand<FSServiceLicenseType.serviceID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Left<FSLicenseType>.On<BqlOperand<FSLicenseType.licenseTypeID, IBqlInt>.IsEqual<FSServiceLicenseType.licenseTypeID>>>, FbqlJoins.Left<FSServiceSkill>.On<BqlOperand<FSServiceSkill.serviceID, IBqlInt>.IsEqual<PX.Objects.IN.InventoryItem.inventoryID>>>, FbqlJoins.Left<FSSkill>.On<BqlOperand<FSSkill.skillID, IBqlInt>.IsEqual<FSServiceSkill.skillID>>>>>.ReadOnly((PXGraph) this)).View.Select(PXView.Currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray1, ref num1, PXView.MaximumRows, ref num2));
      objArray = EnumerableExtensions.Append<object>(objArray, (object[]) new object[1][]
      {
        source.Select<PX.Objects.IN.InventoryItem, object>((Func<PX.Objects.IN.InventoryItem, object>) (obj => (object) obj.InventoryID)).ToArray<object>()
      });
      bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<FSAppointmentDet.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
    }
    PXFilterRow[] pxFilterRowArray2 = filters.ForDACs(new string[3]
    {
      "Vendor",
      "EPEmployee",
      "BranchAlias"
    });
    if (pxFilterRowArray2.Length != 0)
    {
      IEnumerable<PX.Objects.AP.Vendor> source = GraphHelper.RowCast<PX.Objects.AP.Vendor>((IEnumerable) ((PXSelectBase) new PXViewOf<PX.Objects.CR.BAccount>.BasedOn<SelectFromBase<PX.Objects.CR.BAccount, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.bAccountID>>>, FbqlJoins.Left<EPEmployee>.On<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.bAccountID>>>, FbqlJoins.Left<BranchAlias>.On<BqlOperand<BranchAlias.bAccountID, IBqlInt>.IsEqual<EPEmployee.parentBAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSxEPEmployee.sDEnabled, Equal<True>>>>>.Or<BqlOperand<FSxVendor.sDEnabled, IBqlBool>.IsEqual<True>>>>>.ReadOnly((PXGraph) this)).View.Select(PXView.Currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray2, ref num1, 2000, ref num2));
      objArray = EnumerableExtensions.Append<object>(objArray, (object[]) new object[1][]
      {
        source.Select<PX.Objects.AP.Vendor, object>((Func<PX.Objects.AP.Vendor, object>) (obj => (object) obj.BAccountID)).ToArray<object>()
      });
      bqlCommand = bqlCommand.WhereAnd<Where<Exists<SelectFromBase<SchedulerAppointmentEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerAppointmentEmployee.employeeID, In<P.AsInt>>>>>.And<BqlOperand<SchedulerAppointmentEmployee.appointmentID, IBqlInt>.IsEqual<SchedulerAppointment.appointmentID>>>>>>();
    }
    int count1 = ((PXSelectBase) this.SearchAppointmentsBaseDelegate).Cache.Keys.Count;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    int count2 = sortColumns.Length - 2;
    string[] strArray = EnumerableExtensions.Append<string>(((IEnumerable<string>) sortColumns).Take<string>(count2).ToArray<string>(), "CreatedDateTime");
    bool[] flagArray = EnumerableExtensions.Append<bool>(((IEnumerable<bool>) descendings).Take<bool>(count2).ToArray<bool>(), true);
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    int startRow = PXView.StartRow;
    int num3 = 0;
    int num4 = startRow;
    PXFilterRow[] pxFilterRowArray3 = filters.ExceptForDACs(new string[3]
    {
      "Vendor",
      "EPEmployee",
      "BranchAlias"
    });
    using (new PXFieldScope(pxView, (IEnumerable<System.Type>) this.GetAppointmentFieldScopeFields(), true))
    {
      List<object> collection = pxView.Select(PXView.Currents, objArray, PXView.Searches, strArray, flagArray, pxFilterRowArray3, ref num4, PXView.MaximumRows, ref num3);
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable selectedAppointment()
  {
    if (!(new PXView((PXGraph) this, true, ((PXSelectBase) this.SelectedAppointment).View.BqlSelect).SelectSingle(Array.Empty<object>()) is SchedulerAppointment schedulerAppointment))
      return (IEnumerable) new SchedulerAppointment[1];
    ((PXSelectBase<FSAppointment>) this.AllAppointments).Current = new FSAppointment()
    {
      SrvOrdType = schedulerAppointment.SrvOrdType,
      RefNbr = schedulerAppointment.RefNbr
    };
    return (IEnumerable) new SchedulerAppointment[1]
    {
      schedulerAppointment
    };
  }

  protected virtual IEnumerable selectedSO()
  {
    if (!(new PXView((PXGraph) this, true, ((PXSelectBase) this.SelectedSO).View.BqlSelect).SelectSingle(Array.Empty<object>()) is SchedulerServiceOrder schedulerServiceOrder))
      return (IEnumerable) new SchedulerServiceOrder[1];
    ((PXSelectBase<FSServiceOrder>) this.AllServiceOrders).Current = new FSServiceOrder()
    {
      SrvOrdType = schedulerServiceOrder.SrvOrdType,
      RefNbr = schedulerServiceOrder.RefNbr
    };
    return (IEnumerable) new SchedulerServiceOrder[1]
    {
      schedulerServiceOrder
    };
  }

  protected virtual IEnumerable lastUpdatedAppointment()
  {
    PXDelegateResult pxDelegateResult1 = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    int num1 = 0;
    int num2 = 0;
    foreach (PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee> pxResult1 in ((PXSelectBase) this.LastUpdatedAppointment_Reversed).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, PXView.MaximumRows, ref num2))
    {
      PXDelegateResult pxDelegateResult2 = pxDelegateResult1;
      PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee> pxResult2 = pxResult1;
      PX.Objects.AP.Vendor vendor = pxResult2 != null ? PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee>.op_Implicit(pxResult2) : new PX.Objects.AP.Vendor();
      PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee> pxResult3 = pxResult1;
      EPEmployee epEmployee = pxResult3 != null ? PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee>.op_Implicit(pxResult3) : new EPEmployee();
      PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee> pxResult4 = pxResult1;
      SchedulerAppointmentEmployee appointmentEmployee = pxResult4 != null ? PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee>.op_Implicit(pxResult4) : new SchedulerAppointmentEmployee();
      SchedulerAppointment schedulerAppointment = PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee>.op_Implicit(pxResult1);
      SchedulerServiceOrder schedulerServiceOrder = PXResult<SchedulerAppointment, SchedulerServiceOrder, SchedulerAppointmentEmployee, PX.Objects.AP.Vendor, EPEmployee>.op_Implicit(pxResult1);
      PXResult<PX.Objects.AP.Vendor, EPEmployee, SchedulerAppointmentEmployee, SchedulerAppointment, SchedulerServiceOrder> pxResult5 = new PXResult<PX.Objects.AP.Vendor, EPEmployee, SchedulerAppointmentEmployee, SchedulerAppointment, SchedulerServiceOrder>(vendor, epEmployee, appointmentEmployee, schedulerAppointment, schedulerServiceOrder);
      ((List<object>) pxDelegateResult2).Add((object) pxResult5);
    }
    return (IEnumerable) pxDelegateResult1;
  }

  protected virtual IEnumerable suitableEmployees()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    SchedulerAppointmentFilter appointmentFilter = ((PXSelectBase<SchedulerAppointmentFilter>) this.AppointmentFilter).SelectSingle(Array.Empty<object>());
    ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilter).SelectSingle(Array.Empty<object>());
    FSSetup setup = ((PXSelectBase<FSSetup>) this.Setup).SelectSingle(Array.Empty<object>());
    bool flag = appointmentFilter.AssignableSOID.GetValueOrDefault() != 0;
    bool useAssignableSODet = appointmentFilter.AssignableSODetID.GetValueOrDefault() != 0;
    bool useAssignableAppointment = appointmentFilter.AssignableAppointmentID.GetValueOrDefault() != 0;
    HashSet<PX.Objects.AP.Vendor> hashSet1 = this.GetStaff((object[]) null, new PXFilterRow[0]).ToHashSet<PX.Objects.AP.Vendor>((IEqualityComparer<PX.Objects.AP.Vendor>) new VendorComparer());
    if (hashSet1.Count == 0)
      return (IEnumerable) pxDelegateResult;
    HashSet<int?> hashSet2 = hashSet1.Select<PX.Objects.AP.Vendor, int?>((Func<PX.Objects.AP.Vendor, int?>) (x => x.BAccountID)).ToHashSet<int?>();
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) hashSet1.Select<PX.Objects.AP.Vendor, SchedulerSuitableEmployee>((Func<PX.Objects.AP.Vendor, SchedulerSuitableEmployee>) (staff => new SchedulerSuitableEmployee()
    {
      BAccountID = staff.BAccountID,
      Type = staff.Type
    })));
    if (!flag && !useAssignableAppointment)
      return (IEnumerable) pxDelegateResult;
    HashSet<int?> defaultEmployees = (flag ? ((PXSelectBase) new FbqlSelect<SelectFromBase<FSSOEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSSOEmployee.sOID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableSOID, IBqlInt>.FromCurrent>>, FSSOEmployee>.View((PXGraph) this)).View : ((PXSelectBase) new FbqlSelect<SelectFromBase<FSSOEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerAppointment>.On<BqlOperand<SchedulerAppointment.sOID, IBqlInt>.IsEqual<FSSOEmployee.sOID>>>>.Where<BqlOperand<SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableAppointmentID, IBqlInt>.FromCurrent>>, FSSOEmployee>.View((PXGraph) this)).View).BqlSelect.Select<FSSOEmployee>((PXGraph) this, (object[]) null).Select<FSSOEmployee, int?>((Func<FSSOEmployee, int?>) (x => x.EmployeeID)).ToHashSet<int?>();
    BqlCommand bqlCommand = (flag ? ((PXSelectBase) new FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerServiceOrder.projectID, Equal<PMProject.contractID>>>>>.And<BqlOperand<SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableSOID, IBqlInt>.FromCurrent>>>>>, PMProject>.View((PXGraph) this)).View : ((PXSelectBase) new FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerServiceOrder>.On<BqlOperand<SchedulerServiceOrder.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>, FbqlJoins.Inner<SchedulerAppointment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerAppointment.sOID, Equal<SchedulerServiceOrder.sOID>>>>>.And<BqlOperand<SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableAppointmentID, IBqlInt>.FromCurrent>>>>>, PMProject>.View((PXGraph) this)).View).BqlSelect.WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.isActive, Equal<True>>>>>.And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.project>>>>();
    HashSet<int?> employessInProject = (HashSet<int?>) null;
    PMProject restrictToProject = PXResult<PMProject, SchedulerServiceOrder>.op_Implicit((PXResult<PMProject, SchedulerServiceOrder>) new PXView((PXGraph) this, true, bqlCommand).SelectSingle(Array.Empty<object>()));
    if (restrictToProject != null && restrictToProject.ContractID.Value != NonProject.ID)
      employessInProject = ((IEnumerable<SchedulerSuitableEmployee>) ((PXSelectBase<SchedulerSuitableEmployee>) new PXViewOf<SchedulerSuitableEmployee>.BasedOn<SelectFromBase<SchedulerSuitableEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployeePosition>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPEmployeePosition.employeeID, Equal<SchedulerSuitableEmployee.bAccountID>>>>>.And<BqlOperand<EPEmployeePosition.isActive, IBqlBool>.IsEqual<True>>>>, FbqlJoins.Inner<EPEmployeeContract>.On<BqlOperand<EPEmployeeContract.employeeID, IBqlInt>.IsEqual<SchedulerSuitableEmployee.bAccountID>>>>.Where<BqlOperand<EPEmployeeContract.contractID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly((PXGraph) this)).Select<SchedulerSuitableEmployee>(new object[1]
      {
        (object) restrictToProject.ContractID.Value
      })).Select<SchedulerSuitableEmployee, int?>((Func<SchedulerSuitableEmployee, int?>) (x => x.BAccountID)).ToHashSet<int?>();
    HashSet<int?> skilledEmployees = setup.DenyWarnBySkill == "N" ? hashSet2 : this.GetStaff((object[]) null, new PXFilterRow[0], filterByLicenses: false, useAssignableAppointment: useAssignableAppointment, useAssignableSO: flag, useAssignableSODet: useAssignableSODet).Select<PX.Objects.AP.Vendor, int?>((Func<PX.Objects.AP.Vendor, int?>) (x => x.BAccountID)).ToHashSet<int?>();
    HashSet<int?> licensedEmployees = setup.DenyWarnByLicense == "N" ? hashSet2 : this.GetStaff((object[]) null, new PXFilterRow[0], false, useAssignableAppointment: useAssignableAppointment, useAssignableSO: flag, useAssignableSODet: useAssignableSODet).Select<PX.Objects.AP.Vendor, int?>((Func<PX.Objects.AP.Vendor, int?>) (x => x.BAccountID)).ToHashSet<int?>();
    HashSet<int?> employeesInArea = setup.DenyWarnByGeoZone == "N" ? hashSet2 : this.GetEmployeesByGeozone(flag);
    EnumerableExtensions.ForEach<SchedulerSuitableEmployee>(GraphHelper.RowCast<SchedulerSuitableEmployee>((IEnumerable) pxDelegateResult), (Action<SchedulerSuitableEmployee>) (employee =>
    {
      if (defaultEmployees.Contains(employee.BAccountID))
      {
        employee.Label = "Default Staff";
        employee.IsDefault = new bool?(true);
      }
      if (employessInProject != null && !employessInProject.Contains(employee.BAccountID))
      {
        if (restrictToProject.RestrictToEmployeeList.GetValueOrDefault())
        {
          employee.Label = "Not in Project";
          employee.IsNonRecommended = new bool?(true);
        }
        SchedulerSuitableEmployee suitableEmployee = employee;
        bool? isUnsuitable = suitableEmployee.IsUnsuitable;
        suitableEmployee.IsUnsuitable = restrictToProject.RestrictToEmployeeList.GetValueOrDefault() ? new bool?(true) : isUnsuitable;
      }
      if (!skilledEmployees.Contains(employee.BAccountID) && !employee.IsUnsuitable.GetValueOrDefault())
      {
        employee.Label = "Skill Mismatch";
        SchedulerSuitableEmployee suitableEmployee1 = employee;
        bool? isUnsuitable = suitableEmployee1.IsUnsuitable;
        suitableEmployee1.IsUnsuitable = setup.DenyWarnBySkill == "D" ? new bool?(true) : isUnsuitable;
        SchedulerSuitableEmployee suitableEmployee2 = employee;
        bool? isNonRecommended = suitableEmployee2.IsNonRecommended;
        suitableEmployee2.IsNonRecommended = setup.DenyWarnBySkill != "D" ? new bool?(true) : isNonRecommended;
      }
      if (!licensedEmployees.Contains(employee.BAccountID) && !employee.IsUnsuitable.GetValueOrDefault())
      {
        employee.Label = "License Mismatch";
        SchedulerSuitableEmployee suitableEmployee3 = employee;
        bool? isUnsuitable = suitableEmployee3.IsUnsuitable;
        suitableEmployee3.IsUnsuitable = setup.DenyWarnByLicense == "D" ? new bool?(true) : isUnsuitable;
        SchedulerSuitableEmployee suitableEmployee4 = employee;
        bool? isNonRecommended = suitableEmployee4.IsNonRecommended;
        suitableEmployee4.IsNonRecommended = setup.DenyWarnByLicense != "D" ? new bool?(true) : isNonRecommended;
      }
      if (!employeesInArea.Contains(employee.BAccountID) && !employee.IsUnsuitable.GetValueOrDefault())
      {
        employee.Label = "Not in Service Area";
        SchedulerSuitableEmployee suitableEmployee5 = employee;
        bool? isUnsuitable = suitableEmployee5.IsUnsuitable;
        suitableEmployee5.IsUnsuitable = setup.DenyWarnByGeoZone == "D" ? new bool?(true) : isUnsuitable;
        SchedulerSuitableEmployee suitableEmployee6 = employee;
        bool? isNonRecommended = suitableEmployee6.IsNonRecommended;
        suitableEmployee6.IsNonRecommended = setup.DenyWarnByGeoZone != "D" ? new bool?(true) : isNonRecommended;
      }
      if (!employee.IsUnsuitable.GetValueOrDefault())
        return;
      employee.IsNonRecommended = new bool?(false);
    }));
    return (IEnumerable) pxDelegateResult;
  }

  protected HashSet<int?> GetEmployeesByGeozone(bool hasAssignableSO)
  {
    HashSet<int?> employeesByGeozone = new HashSet<int?>();
    string input = (hasAssignableSO ? ((PXSelectBase<SchedulerServiceOrder>) new FbqlSelect<SelectFromBase<SchedulerServiceOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SchedulerServiceOrder.sOID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableSOID, IBqlInt>.FromCurrent>>, SchedulerServiceOrder>.View((PXGraph) this)).SelectSingle(Array.Empty<object>()) : ((PXSelectBase<SchedulerServiceOrder>) new FbqlSelect<SelectFromBase<SchedulerServiceOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SchedulerAppointment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerAppointment.sOID, Equal<SchedulerServiceOrder.sOID>>>>>.And<BqlOperand<SchedulerAppointment.appointmentID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableAppointmentID, IBqlInt>.FromCurrent>>>>>, SchedulerServiceOrder>.View((PXGraph) this)).SelectSingle(Array.Empty<object>())).PostalCode?.Trim() ?? "";
    foreach (PXResult<EPEmployee, FSGeoZoneEmp, FSGeoZonePostalCode> pxResult in ((PXSelectBase<EPEmployee>) new FbqlSelect<SelectFromBase<EPEmployee, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSGeoZoneEmp>.On<BqlOperand<FSGeoZoneEmp.employeeID, IBqlInt>.IsEqual<EPEmployee.bAccountID>>>, FbqlJoins.Inner<FSGeoZonePostalCode>.On<BqlOperand<FSGeoZonePostalCode.geoZoneID, IBqlInt>.IsEqual<FSGeoZoneEmp.geoZoneID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPEmployee.parentBAccountID, IsNotNull>>>, And<BqlOperand<PX.Objects.AP.Vendor.vStatus, IBqlString>.IsNotEqual<VendorStatus.inactive>>>>.And<BqlOperand<FSxEPEmployee.sDEnabled, IBqlBool>.IsEqual<True>>>, EPEmployee>.View((PXGraph) this)).Select(Array.Empty<object>()))
    {
      FSGeoZoneEmp fsGeoZoneEmp = PXResult<EPEmployee, FSGeoZoneEmp, FSGeoZonePostalCode>.op_Implicit(pxResult);
      FSGeoZonePostalCode geoZonePostalCode = PXResult<EPEmployee, FSGeoZoneEmp, FSGeoZonePostalCode>.op_Implicit(pxResult);
      if (Regex.Match(input, geoZonePostalCode.PostalCode.Trim()).Success)
        employeesByGeozone.Add(fsGeoZoneEmp.EmployeeID);
    }
    return employeesByGeozone;
  }

  protected virtual IEnumerable appointmentsAllStaff()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    this._dataHandler.StoreDatesFilter(((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current, PXContext.GetScreenID());
    string[] dacNames = new string[8]
    {
      "Vendor",
      "EPEmployee",
      "SchedulerEmployeeSkill",
      "SchedulerEmployeeLicenseType",
      "FSGeoZoneEmp",
      "SchedulerEmployeeGeoZone",
      "BranchAlias",
      "SchedulerEmployeeInventoryItem"
    };
    List<int?>[] employeeParams = new List<int?>[1]
    {
      this.GetStaff(PXView.Searches, PXView.Filters.ForDACs(dacNames, true)).Select<PX.Objects.AP.Vendor, int?>((Func<PX.Objects.AP.Vendor, int?>) (x => x.BAccountID)).ToList<int?>()
    };
    SchedulerAppointmentFilter appointmentFilter1 = ((PXSelectBase<SchedulerAppointmentFilter>) this.AppointmentFilter).SelectSingle(Array.Empty<object>());
    PX.Objects.FS.MainAppointmentFilter appointmentFilter2 = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilter).SelectSingle(Array.Empty<object>());
    int num1;
    if (appointmentFilter1 != null)
    {
      int? searchAppointmentId = appointmentFilter1.SearchAppointmentID;
      int num2 = 0;
      if (searchAppointmentId.GetValueOrDefault() > num2 & searchAppointmentId.HasValue)
      {
        num1 = this.HighlightedEntriesCountMatches(employeeParams) ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 1;
label_4:
    bool flag = num1 != 0;
    object[] searches = flag ? PXView.Searches : new object[0];
    PXFilterRow[] filters1 = flag ? PXView.PXFilterRowCollection.op_Implicit(PXView.Filters) : new PXFilterRow[0];
    PXFilterRow[] filters2 = filters1.ForDACs(dacNames, true);
    PXFilterRow[] pxFilterRowArray = filters1.ExceptForDACs(dacNames, true);
    if (!flag)
    {
      employeeParams = new List<int?>[1]
      {
        this.GetStaff(searches, filters2).Select<PX.Objects.AP.Vendor, int?>((Func<PX.Objects.AP.Vendor, int?>) (x => x.BAccountID)).ToList<int?>()
      };
      appointmentFilter2.ResetFilters = new bool?(true);
      ((PXSelectBase) this.MainAppointmentFilter).Cache.Update((object) appointmentFilter1);
    }
    if (employeeParams[0].Count == 0)
      return (IEnumerable) pxDelegateResult;
    using (new PXFieldScope(((PXSelectBase) this.AppointmentsAllStaff_TimeSlots).View, (IEnumerable<System.Type>) this.GetAppointmentFieldScopeFields(), true))
    {
      int num3 = 0;
      int num4 = 0;
      List<object> objectList = ((PXSelectBase) this.AppointmentsAllStaff_TimeSlots).View.Select(PXView.Currents, (object[]) employeeParams, searches, PXView.SortColumns, PXView.Descendings, new PXFilterRow[0], ref num3, PXView.MaximumRows, ref num4);
      List<FSTimeSlot> fsTimeSlotList = (List<FSTimeSlot>) null;
      foreach (PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot> pxResult in objectList)
      {
        if (PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot>.op_Implicit(pxResult).Type == "VE")
        {
          if (fsTimeSlotList == null)
            fsTimeSlotList = this.GetVendorTimeSlots();
          if (fsTimeSlotList.Count == 0)
          {
            ((List<object>) pxDelegateResult).Add((object) pxResult);
          }
          else
          {
            foreach (FSTimeSlot fsTimeSlot in fsTimeSlotList)
            {
              PX.Objects.AP.Vendor vendor = PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot>.op_Implicit(pxResult);
              fsTimeSlot.EmployeeID = vendor.BAccountID;
              ((List<object>) pxDelegateResult).Add((object) new PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot>(PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot>.op_Implicit(pxResult), PXResult<PX.Objects.AP.Vendor, EPEmployee, FSTimeSlot>.op_Implicit(pxResult), fsTimeSlot));
            }
          }
        }
        else
          ((List<object>) pxDelegateResult).Add((object) pxResult);
      }
    }
    using (new PXFieldScope(((PXSelectBase) this.AppointmentsAllStaff_Appointments).View, (IEnumerable<System.Type>) this.GetAppointmentFieldScopeFields(), true))
    {
      int num5 = 0;
      int num6 = 0;
      List<object> objectList = ((PXSelectBase) this.AppointmentsAllStaff_Appointments).View.Select(PXView.Currents, (object[]) employeeParams, (object[]) null, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) null, ref num5, PXView.MaximumRows, ref num6);
      int num7 = 0;
      num6 = 0;
      HashSet<int?> hashSet = GraphHelper.RowCast<SchedulerAppointment>((IEnumerable) ((PXSelectBase) this.AppointmentsAllStaff_Appointments).View.Select(PXView.Currents, (object[]) employeeParams, searches, PXView.SortColumns, PXView.Descendings, pxFilterRowArray, ref num7, PXView.MaximumRows, ref num6)).Select<SchedulerAppointment, int?>((Func<SchedulerAppointment, int?>) (obj => obj.AppointmentID)).ToHashSet<int?>();
      foreach (PXResult<PX.Objects.AP.Vendor, SchedulerAppointmentEmployee, SchedulerAppointment, SchedulerServiceOrder, FSAppointmentDet, PX.Objects.IN.InventoryItem, FSGeoZonePostalCode, FSGeoZone, FSServiceLicenseType, FSLicenseType, FSServiceSkill, FSSkill, EPEmployee> pxResult in objectList)
      {
        SchedulerAppointment schedulerAppointment = PXResult<PX.Objects.AP.Vendor, SchedulerAppointmentEmployee, SchedulerAppointment, SchedulerServiceOrder, FSAppointmentDet, PX.Objects.IN.InventoryItem, FSGeoZonePostalCode, FSGeoZone, FSServiceLicenseType, FSLicenseType, FSServiceSkill, FSSkill, EPEmployee>.op_Implicit(pxResult);
        PXResult<PX.Objects.AP.Vendor, SchedulerAppointmentEmployee, SchedulerAppointment, SchedulerServiceOrder, FSAppointmentDet, PX.Objects.IN.InventoryItem, FSGeoZonePostalCode, FSGeoZone, FSServiceLicenseType, FSLicenseType, FSServiceSkill, FSSkill, EPEmployee>.op_Implicit(pxResult).IsFilteredOut = new bool?(!hashSet.Contains(schedulerAppointment.AppointmentID));
        ((List<object>) pxDelegateResult).Add((object) pxResult);
      }
    }
    using (new PXFieldScope(((PXSelectBase) this.AppointmentsAllStaff_NoEmployee).View, (IEnumerable<System.Type>) this.GetAppointmentFieldScopeFields(), true))
    {
      int num8 = 0;
      int num9 = 0;
      foreach (PXResult<SchedulerAppointment, SchedulerAppointmentEmployee, SchedulerServiceOrder> pxResult in ((PXSelectBase) this.AppointmentsAllStaff_NoEmployee).View.Select(PXView.Currents, PXView.Parameters, searches, PXView.SortColumns, PXView.Descendings, pxFilterRowArray, ref num8, PXView.MaximumRows, ref num9))
        ((List<object>) pxDelegateResult).Add((object) new PXResult<PX.Objects.AP.Vendor, EPEmployee, SchedulerAppointment, SchedulerAppointmentEmployee, SchedulerServiceOrder>(new PX.Objects.AP.Vendor(), new EPEmployee(), PXResult<SchedulerAppointment, SchedulerAppointmentEmployee, SchedulerServiceOrder>.op_Implicit(pxResult), PXResult<SchedulerAppointment, SchedulerAppointmentEmployee, SchedulerServiceOrder>.op_Implicit(pxResult), PXResult<SchedulerAppointment, SchedulerAppointmentEmployee, SchedulerServiceOrder>.op_Implicit(pxResult)));
    }
    appointmentFilter1.SearchAppointmentID = new int?(0);
    ((PXSelectBase) this.AppointmentFilter).Cache.SetStatus((object) appointmentFilter1, (PXEntryStatus) 6);
    return (IEnumerable) pxDelegateResult;
  }

  protected List<PX.Objects.AP.Vendor> GetStaff(
    object[] searches,
    PXFilterRow[] filters,
    bool filterBySkills = true,
    bool filterByLicenses = true,
    bool useAssignableAppointment = false,
    bool useAssignableSO = false,
    bool useAssignableSODet = false)
  {
    int num1 = 0;
    int num2 = 0;
    BqlCommand bqlCommand = ((PXSelectBase) this.AppointmentsAllStaff_Employees).View.BqlSelect;
    object[] objArray = new object[0];
    List<int?> nullableList = (List<int?>) null;
    PXFilterRow[] pxFilterRowArray1 = filters.ForDACs(new string[1]
    {
      "SchedulerEmployeeInventoryItem"
    });
    if (useAssignableAppointment)
      nullableList = ((IEnumerable<SchedulerEmployeeInventoryItem>) ((PXSelectBase<SchedulerEmployeeInventoryItem>) new PXViewOf<SchedulerEmployeeInventoryItem>.BasedOn<SelectFromBase<SchedulerEmployeeInventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSAppointmentDet>.On<BqlOperand<FSAppointmentDet.inventoryID, IBqlInt>.IsEqual<SchedulerEmployeeInventoryItem.inventoryID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerEmployeeInventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.inactive>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>>>.And<BqlOperand<FSAppointmentDet.appointmentID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableAppointmentID, IBqlInt>.FromCurrent>>>>.ReadOnly((PXGraph) this)).Select<SchedulerEmployeeInventoryItem>(Array.Empty<object>())).Select<SchedulerEmployeeInventoryItem, int?>((Func<SchedulerEmployeeInventoryItem, int?>) (obj => obj.InventoryID)).ToList<int?>();
    else if (useAssignableSO)
    {
      PXView view = ((PXSelectBase) new FbqlSelect<SelectFromBase<SchedulerEmployeeInventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSSODet>.On<BqlOperand<FSSODet.inventoryID, IBqlInt>.IsEqual<SchedulerEmployeeInventoryItem.inventoryID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerEmployeeInventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.inactive>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>>, And<BqlOperand<FSSODet.lineType, IBqlString>.IsEqual<ListField_LineType_UnifyTabs.Service>>>, And<BqlOperand<FSSODet.status, IBqlString>.IsEqual<FSSODet.ListField_Status_SODet.ScheduleNeeded>>>>.And<BqlOperand<FSSODet.sOID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableSOID, IBqlInt>.FromCurrent>>>, SchedulerEmployeeInventoryItem>.View((PXGraph) this)).View;
      BqlCommand command = view.BqlSelect;
      if (useAssignableSODet)
        command = view.BqlSelect.WhereAnd<Where<BqlOperand<FSSODet.sODetID, IBqlInt>.IsEqual<BqlField<SchedulerAppointmentFilter.assignableSODetID, IBqlInt>.FromCurrent>>>();
      nullableList = command.SelectReadonly<SchedulerEmployeeInventoryItem>((PXGraph) this, (object[]) null).Select<SchedulerEmployeeInventoryItem, int?>((Func<SchedulerEmployeeInventoryItem, int?>) (obj => obj.InventoryID)).ToList<int?>();
    }
    else if (pxFilterRowArray1.Length != 0)
      nullableList = GraphHelper.RowCast<SchedulerEmployeeInventoryItem>((IEnumerable) ((PXSelectBase) new PXViewOf<SchedulerEmployeeInventoryItem>.BasedOn<SelectFromBase<SchedulerEmployeeInventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SchedulerEmployeeInventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.inactive>>>, And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>>.And<BqlOperand<SchedulerEmployeeInventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>>>.ReadOnly((PXGraph) this)).View.Select(PXView.Currents, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray1, ref num1, PXView.MaximumRows, ref num2)).Select<SchedulerEmployeeInventoryItem, int?>((Func<SchedulerEmployeeInventoryItem, int?>) (obj => obj.InventoryID)).ToList<int?>();
    if (nullableList != null)
    {
      foreach (int? nullable in nullableList)
      {
        if (filterBySkills)
        {
          bqlCommand = bqlCommand.WhereAnd<Where<Not<Exists<SelectFromBase<FSServiceSkill, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceSkill.serviceID, Equal<P.AsInt>>>>>.And<Not<Exists<SelectFromBase<FSEmployeeSkill, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSEmployeeSkill.skillID, Equal<FSServiceSkill.skillID>>>>>.And<BqlOperand<FSEmployeeSkill.employeeID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>>>>>>>>>>();
          objArray = EnumerableExtensions.Append<object>(objArray, (object) nullable);
        }
        if (filterByLicenses)
        {
          bqlCommand = bqlCommand.WhereAnd<Where<Not<Exists<SelectFromBase<FSServiceLicenseType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceLicenseType.serviceID, Equal<P.AsInt>>>>>.And<Not<Exists<SelectFromBase<FSLicense, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSLicense.licenseTypeID, Equal<FSServiceLicenseType.licenseTypeID>>>>>.And<BqlOperand<FSLicense.employeeID, IBqlInt>.IsEqual<PX.Objects.AP.Vendor.bAccountID>>>>>>>>>>>();
          objArray = EnumerableExtensions.Append<object>(objArray, (object) nullable);
        }
      }
    }
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    int num3 = 0;
    num2 = 0;
    PXFilterRow[] pxFilterRowArray2 = filters.ExceptForDACs(new string[1]
    {
      "SchedulerEmployeeInventoryItem"
    });
    return GraphHelper.RowCast<PX.Objects.AP.Vendor>((IEnumerable) pxView.Select(PXView.Currents, objArray, searches, PXView.SortColumns, PXView.Descendings, pxFilterRowArray2, ref num3, 2000, ref num2)).ToList<PX.Objects.AP.Vendor>();
  }

  protected bool HighlightedEntriesCountMatches(List<int?>[] employeeParams)
  {
    SchedulerAppointment schedulerAppointment = ((PXSelectBase<SchedulerAppointment>) this.HighlightedSearchAppointment).SelectSingle(Array.Empty<object>());
    if (schedulerAppointment == null)
      return false;
    int num1 = 0;
    int num2 = 0;
    int? staffCntr1 = schedulerAppointment.StaffCntr;
    int num3 = 0;
    if (staffCntr1.GetValueOrDefault() == num3 & staffCntr1.HasValue)
      return ((PXSelectBase) this.AppointmentsAllStaff_NoEmployeeHighlighted).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, PXView.MaximumRows, ref num2).Count > 0;
    int count = ((PXSelectBase) this.AppointmentsAllStaff_HighlightedAppointment).View.Select(PXView.Currents, (object[]) employeeParams, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, PXView.MaximumRows, ref num2).Count;
    int? staffCntr2 = schedulerAppointment.StaffCntr;
    int valueOrDefault = staffCntr2.GetValueOrDefault();
    return count == valueOrDefault & staffCntr2.HasValue;
  }

  protected virtual IEnumerable employeeDevices()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    SchedulerDevicesFilter schedulerDevicesFilter = ((PXSelectBase<SchedulerDevicesFilter>) this.DevicesFilter).SelectSingle(Array.Empty<object>());
    if (string.IsNullOrEmpty(schedulerDevicesFilter.SelectedEmployeeIDs) || !((PXGraph) this).Accessinfo.BusinessDate.HasValue)
      return (IEnumerable) pxDelegateResult;
    DateTime now = PXTimeZoneInfo.Now;
    DateTime dateTime1 = PXDBDateAndTimeAttribute.CombineDateTime(new DateTime?(((PXGraph) this).Accessinfo.BusinessDate.Value), new DateTime?(PXTimeZoneInfo.Now)).Value;
    DateTime? nullable = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateBegin;
    DateTime dateTime2 = dateTime1;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0) == 0)
    {
      nullable = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateEnd;
      DateTime dateTime3 = dateTime1;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime3 ? 1 : 0) : 0) == 0)
      {
        string[] strArray = schedulerDevicesFilter.SelectedEmployeeIDs.Split(';');
        List<int> intList = new List<int>();
        foreach (string s in strArray)
        {
          int result;
          if (int.TryParse(s, out result))
            intList.Add(result);
        }
        if (intList.Count == 0)
          return (IEnumerable) pxDelegateResult;
        foreach (int num in intList)
        {
          UserPreferences userPreferences = ((PXSelectBase<UserPreferences>) this.UserPreferencesByEmployee).SelectSingle(new object[1]
          {
            (object) num
          });
          if (userPreferences != null)
          {
            FSxUserPreferences extension = PXCache<UserPreferences>.GetExtension<FSxUserPreferences>(userPreferences);
            if (extension == null || !extension.TrackLocation.GetValueOrDefault())
              continue;
          }
          PXView pxView = new PXView((PXGraph) this, true, (BqlCommand) new SelectFromBase<FSGPSTrackingHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FSGPSTrackingRequest>.On<BqlOperand<FSGPSTrackingRequest.trackingID, IBqlGuid>.IsEqual<FSGPSTrackingHistory.trackingID>>>, FbqlJoins.Inner<Users>.On<BqlOperand<Users.username, IBqlString>.IsEqual<FSGPSTrackingRequest.userName>>>, FbqlJoins.Inner<EPEmployee>.On<BqlOperand<EPEmployee.userID, IBqlGuid>.IsEqual<Users.pKID>>>>.Where<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<P.AsInt>>.OrderBy<Desc<FSGPSTrackingHistory.executionDate>>());
          using (new PXFieldScope(pxView, (IEnumerable<System.Type>) this.GetTrackingHistoryFieldScopeFields(), true))
          {
            PXResult<FSGPSTrackingHistory, FSGPSTrackingRequest, Users, EPEmployee> pxResult = (PXResult<FSGPSTrackingHistory, FSGPSTrackingRequest, Users, EPEmployee>) pxView.SelectSingle(new object[1]
            {
              (object) num
            });
            if (pxResult != null)
            {
              EPEmployee epEmployee = PXResult<FSGPSTrackingHistory, FSGPSTrackingRequest, Users, EPEmployee>.op_Implicit(pxResult);
              Users users = PXResult<FSGPSTrackingHistory, FSGPSTrackingRequest, Users, EPEmployee>.op_Implicit(pxResult);
              FSGPSTrackingHistory fsgpsTrackingHistory = PXResult<FSGPSTrackingHistory, FSGPSTrackingRequest, Users, EPEmployee>.op_Implicit(pxResult);
              ((List<object>) pxDelegateResult).Add((object) new SchedulerTrackingHistory()
              {
                BAccountID = epEmployee.BAccountID,
                FullName = users.FullName,
                ExecutionDate = fsgpsTrackingHistory.ExecutionDate,
                Latitude = fsgpsTrackingHistory.Latitude,
                Longitude = fsgpsTrackingHistory.Longitude,
                Altitude = fsgpsTrackingHistory.Altitude
              });
            }
          }
        }
        return (IEnumerable) pxDelegateResult;
      }
    }
    return (IEnumerable) pxDelegateResult;
  }

  [PXMergeAttributes]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Staff Type", Visible = true)]
  [BAccountType.SalesPersonTypeList]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.Vendor.type> e)
  {
  }

  [PXMergeAttributes]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Staff Type", Visible = true)]
  [BAccountType.SalesPersonTypeList]
  public virtual void _(PX.Data.Events.CacheAttached<FSSOEmployee.type> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Staff Member ID", Visible = true)]
  [PXSelector(typeof (SearchFor<PX.Objects.AP.Vendor.acctCD>.In<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<EPEmployee.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSxEPEmployee.sDEnabled, Equal<True>>>>>.Or<BqlOperand<FSxVendor.sDEnabled, IBqlBool>.IsEqual<True>>>>>), new System.Type[] {typeof (PX.Objects.AP.Vendor.acctCD), typeof (PX.Objects.AP.Vendor.acctName), typeof (PX.Objects.AP.Vendor.type)}, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true)]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.Vendor.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDimensionSelectorAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDimensionSelectorAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.inventoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSrvOrdType.postToSOSIPM> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  public virtual void _(
    PX.Data.Events.CacheAttached<FSSrvOrdType.allowInventoryItems> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUnboundDefaultAttribute))]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.Vendor.included> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDimensionSelectorAttribute))]
  [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.isActive, Equal<True>, And<Where<PX.Objects.CR.Location.locType, Equal<LocTypeList.customerLoc>, Or<PX.Objects.CR.Location.locType, Equal<LocTypeList.combinedLoc>>>>>>), new System.Type[] {typeof (PX.Objects.CR.Location.locationID), typeof (PX.Objects.CR.Location.descr), typeof (PX.Objects.CR.Location.bAccountID)}, CacheGlobal = true, SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual void _(
    PX.Data.Events.CacheAttached<SchedulerServiceOrder.locationID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXRestrictor(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.markedForDeletion>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotEqual<InventoryItemStatus.noSales>>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)})]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.IN.InventoryItem.itemType, IBqlString>.IsEqual<INItemTypes.serviceItem>>), "An inventory item must be of the Service type.", new System.Type[] {})]
  [PXSelector(typeof (SearchFor<PX.Objects.IN.InventoryItem.inventoryID>), new System.Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), typeof (PX.Objects.IN.InventoryItem.itemClassID)}, SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.IN.InventoryItem.inventoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PXUIField(DisplayName = "Service Class")]
  [PXSelector(typeof (Search<INItemClass.itemClassID, Where<INItemClass.itemType, Equal<INItemTypes.serviceItem>>>), SubstituteKey = typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual void _(PX.Data.Events.CacheAttached<INItemClass.itemClassID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<PMTask.taskID, Where<PMTask.isCancelled, Equal<False>, And<PMTask.isCompleted, Equal<False>>>>), SubstituteKey = typeof (PMTask.taskCD), DescriptionField = typeof (PMTask.description))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID>), CacheGlobal = true, SubstituteKey = typeof (FSEquipment.refNbr), DescriptionField = typeof (FSEquipment.descr))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.SMequipmentID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID>), CacheGlobal = true, SubstituteKey = typeof (FSEquipment.refNbr), DescriptionField = typeof (FSEquipment.descr))]
  public virtual void _(
    PX.Data.Events.CacheAttached<FSAppointmentDet.SMequipmentID> e)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<FSModelTemplateComponent.componentID, Where<FSModelTemplateComponent.active, Equal<True>>>), CacheGlobal = true, SubstituteKey = typeof (FSModelTemplateComponent.componentCD), DescriptionField = typeof (FSModelTemplateComponent.descr))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.componentID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (CostCodeDimensionSelectorAttribute))]
  [PXSelector(typeof (Search<PMCostCode.costCodeID, Where<PMCostCode.isActive, Equal<True>>>), CacheGlobal = true, SubstituteKey = typeof (PMCostCode.costCodeCD), DescriptionField = typeof (PMCostCode.description))]
  public virtual void _(PX.Data.Events.CacheAttached<FSSODet.costCodeID> e)
  {
  }

  public SchedulerMaint()
  {
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.docDesc>(((PXGraph) this).Caches[typeof (SchedulerServiceOrder)], "Service Order Description");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.locationID>(((PXGraph) this).Caches[typeof (SchedulerServiceOrder)], "Customer Location ID");
    PXUIFieldAttribute.SetDisplayName<FSSODet.tranDesc>(((PXGraph) this).Caches[typeof (FSSODet)], "Service Order Detail Line Desc.");
    PXUIFieldAttribute.SetDisplayName<BranchAlias.branchCD>(((PXGraph) this).Caches[typeof (BranchAlias)], "Employee Branch ID");
    PXUIFieldAttribute.SetDisplayName<BranchAlias.acctName>(((PXGraph) this).Caches[typeof (BranchAlias)], "Employee Branch Name");
    PXUIFieldAttribute.SetDisplayName<FSGeoZone.descr>(((PXGraph) this).Caches[typeof (FSGeoZone)], "Service Area Description");
    PXUIFieldAttribute.SetVisible<FSTimeSlot.timeStart>(((PXGraph) this).Caches[typeof (FSTimeSlot)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSTimeSlot.timeEnd>(((PXGraph) this).Caches[typeof (FSTimeSlot)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSTimeSlot.employeeID>(((PXGraph) this).Caches[typeof (FSTimeSlot)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSSODet.estimatedDuration>(((PXGraph) this).Caches[typeof (FSSODet)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSSODet.lineNbr>(((PXGraph) this).Caches[typeof (FSSODet)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSSODet.inventoryID>(((PXGraph) this).Caches[typeof (FSSODet)], (object) null, false);
    PXUIFieldAttribute.SetVisible<FSSODet.status>(((PXGraph) this).Caches[typeof (FSSODet)], (object) null, false);
    PXUIFieldAttribute.SetVisible<SchedulerServiceOrder.sOID>(((PXGraph) this).Caches[typeof (SchedulerServiceOrder)], (object) null, false);
    PXUIFieldAttribute.SetVisible<SchedulerServiceOrder.projectID>(((PXGraph) this).Caches[typeof (SchedulerServiceOrder)], (object) null, true);
    PXUIFieldAttribute.SetVisible<SchedulerServiceOrder.contactID>(((PXGraph) this).Caches[typeof (SchedulerServiceOrder)], (object) null, true);
    PXUIFieldAttribute.SetDisplayName<SchedulerAppointment.status>(((PXSelectBase) this.SelectedAppointment).Cache, "Status");
    PXUIFieldAttribute.SetDisplayName<SchedulerAppointment.scheduledDateTimeBegin>(((PXSelectBase) this.SelectedAppointment).Cache, "Scheduled Start");
    PXUIFieldAttribute.SetDisplayName<SchedulerAppointment.scheduledDateTimeEnd>(((PXSelectBase) this.SelectedAppointment).Cache, "Scheduled End");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.refNbr>(((PXSelectBase) this.SelectedSO).Cache, "Order Nbr.");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.status>(((PXSelectBase) this.SelectedSO).Cache, "Status");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.docDesc>(((PXSelectBase) this.SelectedSO).Cache, "Description");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.projectID>(((PXSelectBase) this.SelectedSO).Cache, "Project ID");
    PXUIFieldAttribute.SetDisplayName<SchedulerServiceOrder.waitingForParts>(((PXSelectBase) this.SelectedSO).Cache, "Waiting for Parts");
    PXUIFieldAttribute.SetRequired<SchedulerAppointment.scheduledDateTimeBegin>(((PXSelectBase) this.SelectedAppointment).Cache, false);
    PXUIFieldAttribute.SetRequired<SchedulerAppointment.scheduledDateTimeEnd>(((PXSelectBase) this.SelectedAppointment).Cache, false);
    PXUIFieldAttribute.SetRequired<SchedulerServiceOrder.projectID>(((PXSelectBase) this.SelectedSO).Cache, false);
    PXUIFieldAttribute.SetRequired<SchedulerServiceOrder.branchLocationCD>(((PXSelectBase) this.SelectedSO).Cache, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.AP.Vendor.acctName>(((PXGraph) this).Caches[typeof (PX.Objects.AP.Vendor)], "Staff Member Name");
    PXUIFieldAttribute.SetDisplayName<FSServiceLicenseType.licenseTypeID>(((PXGraph) this).Caches[typeof (FSServiceLicenseType)], "License Type ID");
    PXUIFieldAttribute.SetDisplayName<FSLicenseType.descr>(((PXGraph) this).Caches[typeof (FSLicenseType)], "License Type Description");
    PXUIFieldAttribute.SetDisplayName<FSSkill.descr>(((PXGraph) this).Caches[typeof (FSSkill)], "Skill Description");
  }

  public virtual List<System.Type> GetSOFieldScopeFields()
  {
    List<System.Type> appointmentsFields = new List<System.Type>();
    EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) new System.Type[71]
    {
      typeof (PX.Objects.AP.Vendor.bAccountID),
      typeof (PX.Objects.AP.Vendor.acctCD),
      typeof (PX.Objects.AP.Vendor.acctName),
      typeof (PX.Objects.AP.Vendor.bAccountID),
      typeof (FSSOEmployee.employeeID),
      typeof (FSSOEmployee.type),
      typeof (BranchAlias.branchCD),
      typeof (BranchAlias.acctName),
      typeof (SchedulerServiceOrder.srvOrdType),
      typeof (SchedulerServiceOrder.refNbr),
      typeof (SchedulerServiceOrder.sOID),
      typeof (SchedulerServiceOrder.status),
      typeof (SchedulerServiceOrder.customerID),
      typeof (SchedulerServiceOrder.estimatedDurationTotal),
      typeof (SchedulerServiceOrder.priority),
      typeof (SchedulerServiceOrder.severity),
      typeof (SchedulerServiceOrder.docDesc),
      typeof (SchedulerServiceOrder.sLAETA),
      typeof (SchedulerServiceOrder.docDesc),
      typeof (SchedulerServiceOrder.projectID),
      typeof (SchedulerServiceOrder.custPORefNbr),
      typeof (SchedulerServiceOrder.branchLocationCD),
      typeof (SchedulerServiceOrder.serviceContractRefNbr),
      typeof (SchedulerServiceOrder.waitingForParts),
      typeof (SchedulerServiceOrder.orderDate),
      typeof (SchedulerServiceOrder.createdDateTime),
      typeof (SchedulerServiceOrder.assignedEmpID),
      typeof (SchedulerServiceOrder.locationID),
      typeof (SchedulerServiceOrder.contactID),
      typeof (SchedulerServiceOrder.customerAcctCD),
      typeof (SchedulerServiceOrder.customerAcctName),
      typeof (SchedulerServiceOrder.customerClassID),
      typeof (SchedulerServiceOrder.phone1),
      typeof (SchedulerServiceOrder.email),
      typeof (SchedulerServiceOrder.addressLine1),
      typeof (SchedulerServiceOrder.addressLine2),
      typeof (SchedulerServiceOrder.city),
      typeof (SchedulerServiceOrder.state),
      typeof (SchedulerServiceOrder.city),
      typeof (SchedulerServiceOrder.postalCode),
      typeof (SchedulerServiceOrder.countryID),
      typeof (SchedulerServiceOrder.fullAddress),
      typeof (SchedulerServiceOrder.branchCD),
      typeof (SchedulerServiceOrder.branchName),
      typeof (SchedulerServiceOrder.branchLocationCD),
      typeof (SchedulerServiceOrder.branchLocationDescr),
      typeof (SchedulerServiceOrder.problemCD),
      typeof (SchedulerServiceOrder.problemDescr),
      typeof (FSSODet.sODetID),
      typeof (FSSODet.lineNbr),
      typeof (FSSODet.refNbr),
      typeof (FSSODet.estimatedDuration),
      typeof (FSSODet.tranDesc),
      typeof (FSSODet.inventoryID),
      typeof (FSSODet.sOLineType),
      typeof (FSSODet.isPrepaid),
      typeof (FSSODet.projectTaskID),
      typeof (FSSODet.SMequipmentID),
      typeof (FSSODet.componentID),
      typeof (FSSODet.equipmentLineRef),
      typeof (FSSODet.discountSequenceID),
      typeof (FSSODet.status),
      typeof (FSSODet.costCodeID),
      typeof (FSSODet.costCodeDescr),
      typeof (PX.Objects.IN.InventoryItem.inventoryID),
      typeof (FSServiceSkill.skillID),
      typeof (FSSkill.descr),
      typeof (FSServiceLicenseType.licenseTypeID),
      typeof (FSLicenseType.descr),
      typeof (FSGeoZone.geoZoneCD),
      typeof (FSGeoZone.descr)
    }, (Action<System.Type>) (f => appointmentsFields.Add(f)));
    return appointmentsFields;
  }

  public virtual List<System.Type> GetAppointmentFieldScopeFields()
  {
    List<System.Type> appointmentsFields = new List<System.Type>();
    EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) new System.Type[82]
    {
      typeof (PX.Objects.AP.Vendor.bAccountID),
      typeof (PX.Objects.AP.Vendor.acctCD),
      typeof (PX.Objects.AP.Vendor.acctName),
      typeof (PX.Objects.AP.Vendor.type),
      typeof (EPEmployee.departmentID),
      typeof (BranchAlias.branchCD),
      typeof (BranchAlias.acctName),
      typeof (FSTimeSlot.employeeID),
      typeof (FSTimeSlot.timeStart),
      typeof (FSTimeSlot.timeEnd),
      typeof (SchedulerAppointment.appointmentID),
      typeof (SchedulerAppointment.srvOrdType),
      typeof (SchedulerAppointment.refNbr),
      typeof (SchedulerAppointment.scheduledDateTimeBegin),
      typeof (SchedulerAppointment.scheduledDateTimeEnd),
      typeof (SchedulerAppointment.status),
      typeof (SchedulerAppointment.mapLatitude),
      typeof (SchedulerAppointment.mapLongitude),
      typeof (SchedulerAppointment.confirmed),
      typeof (SchedulerAppointment.validatedByDispatcher),
      typeof (SchedulerAppointment.estimatedDurationTotal),
      typeof (SchedulerAppointment.staffCntr),
      typeof (SchedulerAppointment.docDesc),
      typeof (SchedulerAppointment.createdDateTime),
      typeof (SchedulerAppointment.locked),
      typeof (SchedulerAppointment.bandColor),
      typeof (SchedulerServiceOrder.srvOrdType),
      typeof (SchedulerServiceOrder.refNbr),
      typeof (SchedulerServiceOrder.sOID),
      typeof (SchedulerServiceOrder.status),
      typeof (SchedulerServiceOrder.estimatedDurationTotal),
      typeof (SchedulerServiceOrder.priority),
      typeof (SchedulerServiceOrder.severity),
      typeof (SchedulerServiceOrder.docDesc),
      typeof (SchedulerServiceOrder.sLAETA),
      typeof (SchedulerServiceOrder.docDesc),
      typeof (SchedulerServiceOrder.projectID),
      typeof (SchedulerServiceOrder.custPORefNbr),
      typeof (SchedulerServiceOrder.branchLocationCD),
      typeof (SchedulerServiceOrder.serviceContractRefNbr),
      typeof (SchedulerServiceOrder.waitingForParts),
      typeof (SchedulerServiceOrder.assignedEmpID),
      typeof (SchedulerServiceOrder.locationID),
      typeof (SchedulerServiceOrder.contactID),
      typeof (SchedulerServiceOrder.orderDate),
      typeof (SchedulerServiceOrder.roomID),
      typeof (SchedulerServiceOrder.customerAcctCD),
      typeof (SchedulerServiceOrder.customerAcctName),
      typeof (SchedulerServiceOrder.customerClassID),
      typeof (SchedulerServiceOrder.phone1),
      typeof (SchedulerServiceOrder.email),
      typeof (SchedulerServiceOrder.addressLine1),
      typeof (SchedulerServiceOrder.addressLine2),
      typeof (SchedulerServiceOrder.city),
      typeof (SchedulerServiceOrder.state),
      typeof (SchedulerServiceOrder.city),
      typeof (SchedulerServiceOrder.postalCode),
      typeof (SchedulerServiceOrder.countryID),
      typeof (SchedulerServiceOrder.fullAddress),
      typeof (SchedulerServiceOrder.branchCD),
      typeof (SchedulerServiceOrder.branchName),
      typeof (SchedulerServiceOrder.branchLocationCD),
      typeof (SchedulerServiceOrder.branchLocationDescr),
      typeof (SchedulerServiceOrder.problemCD),
      typeof (SchedulerServiceOrder.problemDescr),
      typeof (FSAppointmentDet.inventoryID),
      typeof (FSAppointmentDet.SMequipmentID),
      typeof (PX.Objects.IN.InventoryItem.inventoryID),
      typeof (PX.Objects.IN.InventoryItem.itemClassID),
      typeof (FSEmployeeSkill.skillID),
      typeof (FSServiceSkill.skillID),
      typeof (FSSkill.isDriverSkill),
      typeof (FSServiceLicenseType.licenseTypeID),
      typeof (FSLicenseType.descr),
      typeof (SchedulerEmployeeLicenseType.licenseTypeID),
      typeof (SchedulerEmployeeLicenseType.descr),
      typeof (FSGeoZone.geoZoneCD),
      typeof (FSGeoZone.descr),
      typeof (SchedulerEmployeeInventoryItem.inventoryID),
      typeof (SchedulerEmployeeGeoZone.geoZoneID),
      typeof (SchedulerEmployeeGeoZone.geoZoneCD),
      typeof (SchedulerEmployeeGeoZone.descr)
    }, (Action<System.Type>) (f => appointmentsFields.Add(f)));
    return appointmentsFields;
  }

  private List<FSTimeSlot> GetVendorTimeSlots()
  {
    FSSetup fsSetup = ((PXSelectBase<FSSetup>) this.Setup).SelectSingle(Array.Empty<object>());
    if (fsSetup == null || fsSetup.CalendarID == null || !((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateBegin.HasValue)
      return new List<FSTimeSlot>();
    PX.Objects.CS.CSCalendar csCalendar = PXResultset<PX.Objects.CS.CSCalendar>.op_Implicit(PXSelectBase<PX.Objects.CS.CSCalendar, PXSelect<PX.Objects.CS.CSCalendar>.Config>.Search<PX.Objects.CS.CSCalendar.calendarID>((PXGraph) this, (object) fsSetup.CalendarID, Array.Empty<object>()));
    List<FSTimeSlot> vendorTimeSlots = new List<FSTimeSlot>();
    DateTime? nullable = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateBegin;
    DateTime date = nullable.Value;
    nullable = ((PXSelectBase<SchedulerDatesFilter>) this.DatesFilter).Current.DateEnd;
    for (DateTime dateTime1 = nullable ?? date.AddDays(1.0); date < dateTime1; date = date.AddDays(1.0))
    {
      CSCalendarExceptions calendarExceptions = PXResultset<CSCalendarExceptions>.op_Implicit(PXSelectBase<CSCalendarExceptions, PXSelect<CSCalendarExceptions>.Config>.Search<CSCalendarExceptions.calendarID, CSCalendarExceptions.date>((PXGraph) this, (object) fsSetup.CalendarID, (object) date, Array.Empty<object>()));
      if (calendarExceptions != null)
      {
        if (calendarExceptions.WorkDay.GetValueOrDefault())
        {
          DateTime dateTime2 = this.SetTime(date, calendarExceptions.StartTime);
          DateTime dateTime3 = this.SetTime(date, calendarExceptions.EndTime);
          vendorTimeSlots.Add(new FSTimeSlot()
          {
            TimeStart = new DateTime?(dateTime2),
            TimeEnd = new DateTime?(dateTime3)
          });
        }
      }
      else if (((bool?) typeof (PX.Objects.CS.CSCalendar).GetProperty(date.ToString("ddd") + "WorkDay").GetValue((object) csCalendar, (object[]) null)).GetValueOrDefault())
      {
        DateTime? time1 = (DateTime?) typeof (PX.Objects.CS.CSCalendar).GetProperty(date.ToString("ddd") + "StartTime").GetValue((object) csCalendar, (object[]) null);
        DateTime? time2 = (DateTime?) typeof (PX.Objects.CS.CSCalendar).GetProperty(date.ToString("ddd") + "EndTime").GetValue((object) csCalendar, (object[]) null);
        DateTime dateTime4 = this.SetTime(date, time1);
        DateTime dateTime5 = this.SetTime(date, time2);
        vendorTimeSlots.Add(new FSTimeSlot()
        {
          TimeStart = new DateTime?(dateTime4),
          TimeEnd = new DateTime?(dateTime5)
        });
      }
    }
    return vendorTimeSlots;
  }

  public virtual List<System.Type> GetTrackingHistoryFieldScopeFields()
  {
    List<System.Type> trackingFields = new List<System.Type>();
    EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) new System.Type[12]
    {
      typeof (EPEmployee.bAccountID),
      typeof (EPEmployee.userID),
      typeof (Users.pKID),
      typeof (Users.username),
      typeof (Users.fullName),
      typeof (FSGPSTrackingRequest.userName),
      typeof (FSGPSTrackingRequest.trackingID),
      typeof (FSGPSTrackingHistory.trackingID),
      typeof (FSGPSTrackingHistory.latitude),
      typeof (FSGPSTrackingHistory.longitude),
      typeof (FSGPSTrackingHistory.altitude),
      typeof (FSGPSTrackingHistory.executionDate)
    }, (Action<System.Type>) (f => trackingFields.Add(f)));
    return trackingFields;
  }

  private DateTime SetTime(DateTime date, DateTime? time)
  {
    return time.HasValue ? new DateTime(date.Year, date.Month, date.Day, time.Value.Hour, time.Value.Minute, time.Value.Second, time.Value.Millisecond) : date;
  }

  protected virtual IEnumerable mainAppointmentFilter()
  {
    PXCache cache = ((PXSelectBase) this.MainAppointmentFilter).Cache;
    FSServiceOrder fsServiceOrder = ((PXSelectBase<FSServiceOrder>) this.ServiceOrderByFilter).SelectSingle(Array.Empty<object>());
    FSSrvOrdType fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) this.ServiceOrderTypeByFilter).SelectSingle(Array.Empty<object>());
    PX.Objects.AR.Customer customer = ((PXSelectBase<PX.Objects.AR.Customer>) this.CustomerByFilter).SelectSingle(Array.Empty<object>());
    PX.Objects.FS.MainAppointmentFilter appointmentFilter1 = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilterBase).SelectSingle(Array.Empty<object>());
    if (fsServiceOrder != null)
    {
      ((PXSelectBase) this.ServiceOrderByFilter).Cache.SetStatus((object) fsServiceOrder, (PXEntryStatus) 0);
      appointmentFilter1.CustomerID = fsServiceOrder.CustomerID;
      customer = ((PXSelectBase<PX.Objects.AR.Customer>) this.CustomerByFilter).SelectSingle(Array.Empty<object>());
    }
    ((PXSelectBase<FSServiceOrder>) this.ServiceOrderByFilter).Current = fsServiceOrder;
    if (string.IsNullOrEmpty(appointmentFilter1.SrvOrdType))
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.FS.MainAppointmentFilter.srvOrdType>((object) appointmentFilter1, ref obj);
      appointmentFilter1.SrvOrdType = (string) obj;
    }
    bool flag = fsSrvOrdType?.Behavior == "IN";
    if (flag)
      customer = (PX.Objects.AR.Customer) null;
    if (customer != null)
      ((PXSelectBase) this.CustomerByFilter).Cache.SetStatus((object) customer, (PXEntryStatus) 0);
    ((PXSelectBase<PX.Objects.AR.Customer>) this.CustomerByFilter).Current = customer;
    int? nullable1;
    if (customer != null)
    {
      nullable1 = appointmentFilter1.LocationID;
      PX.Objects.CR.Location location = PXResult<PX.Objects.CR.Location>.op_Implicit(nullable1.HasValue ? ((IQueryable<PXResult<PX.Objects.CR.Location>>) ((PXSelectBase<PX.Objects.CR.Location>) this.AllLocations).Search<PX.Objects.CR.Location.locationID>((object) appointmentFilter1.LocationID, Array.Empty<object>())).FirstOrDefault<PXResult<PX.Objects.CR.Location>>() : (PXResult<PX.Objects.CR.Location>) null);
      int? nullable2;
      if (location != null)
      {
        nullable1 = location.BAccountID;
        nullable2 = customer.BAccountID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          goto label_12;
      }
      object locationId = (object) appointmentFilter1.LocationID;
      cache.RaiseFieldDefaulting<PX.Objects.FS.MainAppointmentFilter.locationID>((object) appointmentFilter1, ref locationId);
      appointmentFilter1.LocationID = (int?) locationId;
label_12:
      nullable2 = appointmentFilter1.ContactID;
      PX.Objects.CR.Contact contact = PXResult<PX.Objects.CR.Contact>.op_Implicit(nullable2.HasValue ? ((IQueryable<PXResult<PX.Objects.CR.Contact>>) ((PXSelectBase<PX.Objects.CR.Contact>) this.AllContacts).Search<PX.Objects.CR.Contact.contactID>((object) appointmentFilter1.ContactID, Array.Empty<object>())).FirstOrDefault<PXResult<PX.Objects.CR.Contact>>() : (PXResult<PX.Objects.CR.Contact>) null);
      if (contact != null)
      {
        nullable2 = contact.BAccountID;
        nullable1 = customer.BAccountID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          goto label_16;
      }
      object contactId = (object) appointmentFilter1.ContactID;
      cache.RaiseFieldDefaulting<PX.Objects.FS.MainAppointmentFilter.contactID>((object) appointmentFilter1, ref contactId);
      appointmentFilter1.ContactID = (int?) contactId;
    }
    else
    {
      appointmentFilter1.LocationID = new int?();
      appointmentFilter1.ContactID = new int?();
    }
label_16:
    PXUIFieldAttribute.SetEnabled<PX.Objects.FS.MainAppointmentFilter.customerID>(cache, (object) appointmentFilter1, fsServiceOrder == null && !flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.FS.MainAppointmentFilter.status>(cache, (object) appointmentFilter1, fsServiceOrder != null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.FS.MainAppointmentFilter.contactID>(cache, (object) appointmentFilter1, customer != null);
    PXUIFieldAttribute.SetEnabled<PX.Objects.FS.MainAppointmentFilter.locationID>(cache, (object) appointmentFilter1, customer != null && !flag);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.EditedAppointmentLocation).Cache, (string) null, customer != null);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.EditedAppointmentContact).Cache, (string) null, customer != null);
    PX.Objects.FS.MainAppointmentFilter appointmentFilter2 = appointmentFilter1;
    int? nullable3;
    if (fsServiceOrder == null)
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    else
      nullable3 = fsServiceOrder.SOID;
    appointmentFilter2.SOID = nullable3;
    appointmentFilter1.Status = fsServiceOrder?.Status;
    cache.Update((object) appointmentFilter1);
    return (IEnumerable) new PX.Objects.FS.MainAppointmentFilter[1]
    {
      appointmentFilter1
    };
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.FS.MainAppointmentFilter> e)
  {
    PX.Objects.FS.MainAppointmentFilter current = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilter).Current;
    DateTime? scheduledDateTimeBegin = e.Row.ScheduledDateTimeBegin;
    ref DateTime? local = ref scheduledDateTimeBegin;
    DateTime? nullable = local.HasValue ? new DateTime?(local.GetValueOrDefault().ToUniversalTime()) : new DateTime?();
    current.ScheduledDateTimeBegin = nullable;
  }

  protected virtual IEnumerable editedAppointmentContact()
  {
    return (IEnumerable) new FSContact[1]
    {
      new PXView((PXGraph) this, false, ((PXSelectBase) this.EditedAppointmentContact).View.BqlSelect).SelectSingle(Array.Empty<object>()) as FSContact
    };
  }

  protected virtual IEnumerable editedAppointmentEmployees()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
    PX.Objects.FS.MainAppointmentFilter appointmentFilter = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilterBase).SelectSingle(Array.Empty<object>());
    bool? nullable;
    if (appointmentFilter == null)
    {
      nullable = new bool?();
    }
    else
    {
      string resources = appointmentFilter.Resources;
      nullable = resources != null ? new bool?(Str.IsNullOrEmpty(resources)) : new bool?();
    }
    int[] numArray;
    if (nullable ?? true)
      numArray = new int[0];
    else if (appointmentFilter == null)
    {
      numArray = (int[]) null;
    }
    else
    {
      string resources = appointmentFilter.Resources;
      if (resources == null)
        numArray = (int[]) null;
      else
        numArray = ((IEnumerable<string>) resources.Split(',')).Select<string, int>((Func<string, int>) (x => int.Parse(x))).ToArray<int>();
    }
    List<object> collection = new PXView((PXGraph) this, true, ((PXSelectBase) this.EditedAppointmentEmployees).View.BqlSelect).SelectMulti((object[]) new int[1][]
    {
      numArray
    });
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    return (IEnumerable) pxDelegateResult;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable scheduleAppointment(PXAdapter adapter)
  {
    ((PXSelectBase<PX.Objects.FS.LastUpdatedAppointmentFilter>) this.LastUpdatedAppointmentFilter).Current = (PX.Objects.FS.LastUpdatedAppointmentFilter) null;
    WrkProcess instance = PXGraph.CreateInstance<WrkProcess>();
    PX.Objects.FS.MainAppointmentFilter query = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilterBase).SelectSingle(Array.Empty<object>());
    PXCache cache = ((PXSelectBase) this.MainAppointmentFilter).Cache;
    if (!query.CustomerID.HasValue)
    {
      object obj;
      cache.RaiseFieldDefaulting<PX.Objects.FS.MainAppointmentFilter.customerID>((object) query, ref obj);
      if (obj != null)
        query.CustomerID = new int?((int) obj);
    }
    bool? openEditor = query.OpenEditor;
    if (!openEditor.GetValueOrDefault() && !this.MainAppointmentFilter.VerifyRequired())
      return adapter.Get();
    FSWrkProcess fsWrkProcess = new FSWrkProcess();
    fsWrkProcess.RoomID = string.Empty;
    fsWrkProcess.SOID = query.SOID;
    fsWrkProcess.SrvOrdType = query.SrvOrdType;
    fsWrkProcess.BranchID = new int?();
    fsWrkProcess.BranchLocationID = query.BranchLocationID;
    fsWrkProcess.CustomerID = query.CustomerID;
    fsWrkProcess.SMEquipmentID = new int?();
    fsWrkProcess.ScheduledDateTimeBegin = query.ScheduledDateTimeBegin;
    DateTime? scheduledDateTimeBegin = query.ScheduledDateTimeBegin;
    TimeSpan timeSpan;
    ref TimeSpan local = ref timeSpan;
    int? nullable = query.Duration;
    long ticks = (long) ((nullable ?? 60) * 60 * 1000) * 10000L;
    local = new TimeSpan(ticks);
    fsWrkProcess.ScheduledDateTimeEnd = scheduledDateTimeBegin.HasValue ? new DateTime?(scheduledDateTimeBegin.GetValueOrDefault() + timeSpan) : new DateTime?();
    fsWrkProcess.TargetScreenID = "FS300200";
    fsWrkProcess.EmployeeIDList = query.Resources;
    nullable = query.SODetID;
    int num = 0;
    string empty;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
    {
      nullable = query.SODetID;
      empty = nullable.ToString();
    }
    else
      empty = string.Empty;
    fsWrkProcess.LineRefList = empty;
    fsWrkProcess.EquipmentIDList = string.Empty;
    FSWrkProcess fsWrkProcessRow = fsWrkProcess;
    openEditor = query.OpenEditor;
    if (openEditor.GetValueOrDefault())
    {
      instance.LaunchAppointmentEntryScreen(fsWrkProcessRow, fromCalendar: true, query: query, editorMode: new PXBaseRedirectException.WindowMode?((PXBaseRedirectException.WindowMode) 2));
      return adapter.Get();
    }
    this.SetLastUpdatedFilter(instance.LaunchAppointmentEntryScreen(fsWrkProcessRow, false, true, query));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable updateAppointment(PXAdapter adapter)
  {
    UpdateAppointmentFilter appointmentFilter1 = ((PXSelectBase<UpdateAppointmentFilter>) this.UpdatedAppointment).SelectSingle(Array.Empty<object>());
    UpdateAppointmentFilter appointmentFilter2 = appointmentFilter1;
    DateTime? nullable1 = appointmentFilter1.NewBegin;
    ref DateTime? local1 = ref nullable1;
    DateTime? nullable2 = local1.HasValue ? new DateTime?(local1.GetValueOrDefault().ToUniversalTime()) : new DateTime?();
    appointmentFilter2.NewBegin = nullable2;
    UpdateAppointmentFilter appointmentFilter3 = appointmentFilter1;
    nullable1 = appointmentFilter1.NewEnd;
    ref DateTime? local2 = ref nullable1;
    DateTime? nullable3 = local2.HasValue ? new DateTime?(local2.GetValueOrDefault().ToUniversalTime()) : new DateTime?();
    appointmentFilter3.NewEnd = nullable3;
    ((PXSelectBase<PX.Objects.FS.LastUpdatedAppointmentFilter>) this.LastUpdatedAppointmentFilter).Current = (PX.Objects.FS.LastUpdatedAppointmentFilter) null;
    FSAppointment appointment = ((PXSelectBase<FSAppointment>) this.AppointmentById).SelectSingle(new object[1]
    {
      (object) appointmentFilter1.AppointmentID
    });
    if (appointment == null)
      throw new PXException("The appointment you select cannot be found. Refresh the appointment and try again.");
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    ((PXSelectBase) instance.AppointmentRecords).Cache.SetStatus((object) appointment, (PXEntryStatus) 0);
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = appointment;
    nullable1 = appointmentFilter1.NewBegin;
    if (nullable1.HasValue)
    {
      nullable1 = appointmentFilter1.NewEnd;
      if (nullable1.HasValue)
      {
        nullable1 = appointment.ScheduledDateTimeBegin;
        DateTime? nullable4 = appointmentFilter1.NewBegin;
        if ((nullable1.HasValue == nullable4.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          nullable4 = appointment.ScheduledDateTimeEnd;
          nullable1 = appointmentFilter1.NewEnd;
          if ((nullable4.HasValue == nullable1.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            goto label_7;
        }
        appointment.HandleManuallyScheduleTime = new bool?(true);
        appointment.ScheduledDateTimeBegin = appointmentFilter1.NewBegin;
        appointment.ScheduledDateTimeEnd = appointmentFilter1.NewEnd;
      }
    }
label_7:
    FSAppointment fsAppointment1 = appointment;
    bool? nullable5 = appointmentFilter1.Confirmed;
    bool? nullable6 = nullable5 ?? appointment.Confirmed;
    fsAppointment1.Confirmed = nullable6;
    FSAppointment fsAppointment2 = appointment;
    nullable5 = appointmentFilter1.ValidatedByDispatcher;
    bool? nullable7 = nullable5 ?? appointment.ValidatedByDispatcher;
    fsAppointment2.ValidatedByDispatcher = nullable7;
    this.UpdateAppointmentEmployee(instance, appointment, appointmentFilter1.NewResourceID, appointmentFilter1.OldResourceID);
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Update(appointment);
    instance.DisableServiceOrderUnboundFieldCalc = true;
    instance.SkipEarningTypeCheck = true;
    ((PXGraph) instance).SelectTimeStamp();
    ExternalControls.DispatchBoardAppointmentMessages messages = new ExternalControls.DispatchBoardAppointmentMessages();
    ((PXGraph) instance).PressSave(messages);
    if (messages.ErrorMessages.Count > 0)
      throw new PXException(messages.ErrorMessages[0]);
    this.SetLastUpdatedFilter(appointment.AppointmentID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CloneAppointment(PXAdapter adapter)
  {
    PX.Objects.FS.MainAppointmentFilter appointmentFilter = ((PXSelectBase<PX.Objects.FS.MainAppointmentFilter>) this.MainAppointmentFilter).SelectSingle(Array.Empty<object>());
    CloneAppointmentProcess instance = PXGraph.CreateInstance<CloneAppointmentProcess>();
    ((PXSelectBase<FSCloneAppointmentFilter>) instance.Filter).Current.SrvOrdType = appointmentFilter.SrvOrdType;
    ((PXSelectBase<FSCloneAppointmentFilter>) instance.Filter).Current.RefNbr = appointmentFilter.RefNbr;
    ((PXSelectBase<FSAppointment>) instance.AppointmentSelected).Current = PXResultset<FSAppointment>.op_Implicit(((PXSelectBase<FSAppointment>) instance.AppointmentSelected).Select(Array.Empty<object>()));
    ((PXAction) instance.cancel).Press();
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable deleteAppointment(PXAdapter adapter)
  {
    FSAppointment fsAppointment = ((PXSelectBase<FSAppointment>) this.AppointmentById).SelectSingle(new object[1]
    {
      (object) ((PXSelectBase<UpdateAppointmentFilter>) this.UpdatedAppointment).SelectSingle(Array.Empty<object>()).AppointmentID
    });
    AppointmentEntry instance = PXGraph.CreateInstance<AppointmentEntry>();
    ((PXSelectBase) instance.AppointmentRecords).Cache.SetStatus((object) fsAppointment, (PXEntryStatus) 0);
    ((PXSelectBase<FSAppointment>) instance.AppointmentRecords).Current = fsAppointment;
    ExternalControls.DispatchBoardAppointmentMessages messages = new ExternalControls.DispatchBoardAppointmentMessages();
    ((PXGraph) instance).PressDelete(messages);
    if (messages.ErrorMessages.Count > 0)
      throw new PXException(messages.ErrorMessages[0]);
    return adapter.Get();
  }

  protected void SetLastUpdatedFilter(int? appointmentID)
  {
    PX.Objects.FS.LastUpdatedAppointmentFilter appointmentFilter = ((PXSelectBase<PX.Objects.FS.LastUpdatedAppointmentFilter>) this.LastUpdatedAppointmentFilter).SelectSingle(Array.Empty<object>());
    appointmentFilter.AppointmentID = appointmentID;
    if (appointmentFilter != null)
      ((PXSelectBase) this.LastUpdatedAppointmentFilter).Cache.SetStatus((object) appointmentFilter, (PXEntryStatus) 0);
    ((PXSelectBase<PX.Objects.FS.LastUpdatedAppointmentFilter>) this.LastUpdatedAppointmentFilter).Current = appointmentFilter;
  }

  protected void UpdateAppointmentEmployee(
    AppointmentEntry entryGraph,
    FSAppointment appointment,
    int? newEmployeeId,
    int? oldEmployeeId)
  {
    int? nullable1 = newEmployeeId;
    int? nullable2 = oldEmployeeId;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    int? nullable3 = newEmployeeId;
    int num1 = 0;
    if (nullable3.GetValueOrDefault() > num1 & nullable3.HasValue)
    {
      PXResultset<FSAppointmentEmployee> pxResultset = ((PXSelectBase<FSAppointmentEmployee>) entryGraph.AppointmentServiceEmployees).Search<FSAppointmentEmployee.appointmentID, FSAppointmentEmployee.employeeID>((object) appointment.AppointmentID, (object) newEmployeeId, Array.Empty<object>());
      if ((pxResultset != null ? NonGenericIEnumerableExtensions.FirstOrDefault_((IEnumerable) pxResultset) : (object) null) != null)
        throw new PXException("This employee has already been assigned to this appointment.");
    }
    nullable3 = oldEmployeeId;
    int num2 = 0;
    if (nullable3.GetValueOrDefault() > num2 & nullable3.HasValue)
    {
      FSAppointmentEmployee[] array = ((IQueryable<PXResult<FSAppointmentEmployee>>) ((PXSelectBase<FSAppointmentEmployee>) entryGraph.AppointmentServiceEmployees).Select(Array.Empty<object>())).Select<PXResult<FSAppointmentEmployee>, FSAppointmentEmployee>((Expression<Func<PXResult<FSAppointmentEmployee>, FSAppointmentEmployee>>) (x => (FSAppointmentEmployee) x)).Where<FSAppointmentEmployee>((Expression<Func<FSAppointmentEmployee, bool>>) (x => x.EmployeeID == oldEmployeeId)).ToArray<FSAppointmentEmployee>();
      bool? primaryDriver = (bool?) ((IEnumerable<FSAppointmentEmployee>) array).ElementAtOrDefault<FSAppointmentEmployee>(0)?.PrimaryDriver;
      foreach (FSAppointmentEmployee appointmentEmployee1 in array)
      {
        nullable3 = newEmployeeId;
        int num3 = 0;
        if (nullable3.GetValueOrDefault() > num3 & nullable3.HasValue)
        {
          FSAppointmentEmployee appointmentEmployee2 = new FSAppointmentEmployee()
          {
            AppointmentID = appointment.AppointmentID,
            EmployeeID = newEmployeeId,
            ServiceLineRef = appointmentEmployee1.ServiceLineRef,
            PrimaryDriver = primaryDriver
          };
          ((PXSelectBase<FSAppointmentEmployee>) entryGraph.AppointmentServiceEmployees).Insert(appointmentEmployee2);
        }
        ((PXSelectBase<FSAppointmentEmployee>) entryGraph.AppointmentServiceEmployees).Delete(appointmentEmployee1);
      }
    }
    else
    {
      nullable3 = newEmployeeId;
      int num4 = 0;
      if (!(nullable3.GetValueOrDefault() > num4 & nullable3.HasValue))
        return;
      FSAppointmentEmployee appointmentEmployee = new FSAppointmentEmployee()
      {
        AppointmentID = appointment.AppointmentID,
        EmployeeID = newEmployeeId
      };
      ((PXSelectBase<FSAppointmentEmployee>) entryGraph.AppointmentServiceEmployees).Insert(appointmentEmployee);
    }
  }

  public class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      RegistrationExtensions.RegisterType<SchedulerDataHandler>(builder).As<ISchedulerDataHandler>().InstancePerLifetimeScope();
    }
  }
}
