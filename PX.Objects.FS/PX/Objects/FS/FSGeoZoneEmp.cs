// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGeoZoneEmp
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Area - Employee")]
[Serializable]
public class FSGeoZoneEmp : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Service Area ID")]
  [PXDBDefault(typeof (FSGeoZone.geoZoneID))]
  [PXParent(typeof (Select<FSGeoZone, Where<FSGeoZone.geoZoneID, Equal<Current<FSGeoZoneEmp.geoZoneID>>>>))]
  public virtual int? GeoZoneID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Employee ID")]
  [FSSelector_Employee_All]
  public virtual int? EmployeeID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSGeoZoneEmp>.By<FSGeoZoneEmp.geoZoneID, FSGeoZoneEmp.employeeID>
  {
    public static FSGeoZoneEmp Find(
      PXGraph graph,
      int? geoZoneID,
      int? employeeID,
      PKFindOptions options = 0)
    {
      return (FSGeoZoneEmp) PrimaryKeyOf<FSGeoZoneEmp>.By<FSGeoZoneEmp.geoZoneID, FSGeoZoneEmp.employeeID>.FindBy(graph, (object) geoZoneID, (object) employeeID, options);
    }
  }

  public static class FK
  {
    public class GeoZone : 
      PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneID>.ForeignKeyOf<FSGeoZoneEmp>.By<FSGeoZoneEmp.geoZoneID>
    {
    }

    public class Employee : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSGeoZoneEmp>.By<FSGeoZoneEmp.employeeID>
    {
    }
  }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGeoZoneEmp.geoZoneID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGeoZoneEmp.employeeID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGeoZoneEmp.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZoneEmp.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZoneEmp.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSGeoZoneEmp.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZoneEmp.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZoneEmp.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSGeoZoneEmp.Tstamp>
  {
  }
}
