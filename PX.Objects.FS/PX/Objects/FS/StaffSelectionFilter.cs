// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.StaffSelectionFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class StaffSelectionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Ref. Nbr.")]
  [FSSelectorAppointmentSODetID]
  public virtual 
  #nullable disable
  string ServiceLineRef { get; set; }

  [PXDefault(typeof (Search2<FSAddress.postalCode, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Current<FSServiceOrder.sOID>>>>))]
  [PXString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Postal Code", Enabled = false)]
  public virtual string PostalCode { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Service Area", Required = false)]
  [PXSelector(typeof (FSGeoZone.geoZoneID), SubstituteKey = typeof (FSGeoZone.geoZoneCD), DescriptionField = typeof (FSGeoZone.descr))]
  public virtual int? GeoZoneID { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Scheduled Date")]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  [PXInt]
  [PXDefault(typeof (Search<FSServiceOrder.projectID, Where<FSServiceOrder.sOID, Equal<Current<FSServiceOrder.sOID>>>>))]
  [PXFormula(typeof (Default<FSServiceOrder.sOID>))]
  [PXUIField(DisplayName = "Project ID")]
  public virtual int? ProjectID { get; set; }

  public virtual bool? ExistContractEmployees { get; set; }

  public abstract class serviceLineRef : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StaffSelectionFilter.serviceLineRef>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    StaffSelectionFilter.postalCode>
  {
  }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StaffSelectionFilter.geoZoneID>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    StaffSelectionFilter.scheduledDateTimeBegin>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  StaffSelectionFilter.projectID>
  {
  }

  public abstract class existContractEmployees : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    StaffSelectionFilter.existContractEmployees>
  {
  }
}
