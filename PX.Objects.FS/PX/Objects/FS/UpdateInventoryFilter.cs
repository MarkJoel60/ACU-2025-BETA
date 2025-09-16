// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.UpdateInventoryFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class UpdateInventoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  public virtual int? BranchID { get; set; }

  [PXDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Up to Date")]
  public virtual DateTime? CutOffDate { get; set; }

  [PXDate]
  [PXFormula(typeof (UpdateInventoryFilter.cutOffDate))]
  [PXUIField(DisplayName = "Document Date")]
  public virtual DateTime? DocumentDate { get; set; }

  [AROpenPeriod(typeof (UpdateInventoryFilter.documentDate), typeof (UpdateInventoryFilter.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
  [PXUIField]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Route Nbr.")]
  [FSSelectorRouteDocumentPostingIN]
  [PXRestrictor(typeof (Where<FSRouteDocument.date, LessEqual<Current<UpdateInventoryFilter.cutOffDate>>>), "", new Type[] {})]
  [PXRestrictor(typeof (Where<FSRouteDocument.status, Equal<ListField_Status_Route.Closed>, Or<FSRouteDocument.status, Equal<ListField_Status_Route.Completed>>>), "", new Type[] {})]
  [PXRestrictor(typeof (Where<FSSrvOrdType.behavior, Equal<ListField.ServiceOrderTypeBehavior.routeAppointment>, And<FSSrvOrdType.enableINPosting, Equal<True>>>), "", new Type[] {})]
  public virtual int? RouteDocumentID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  [FSSelectorAppointmentPostingIN]
  public virtual int? AppointmentID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UpdateInventoryFilter.branchID>
  {
  }

  public abstract class cutOffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UpdateInventoryFilter.cutOffDate>
  {
  }

  public abstract class documentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    UpdateInventoryFilter.documentDate>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateInventoryFilter.finPeriodID>
  {
  }

  public abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateInventoryFilter.routeDocumentID>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateInventoryFilter.appointmentID>
  {
  }
}
