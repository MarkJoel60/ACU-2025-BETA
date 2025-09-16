// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteEmployee
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
public class FSRouteEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FSRoute.routeID))]
  [PXParent(typeof (Select<FSRoute, Where<FSRoute.routeID, Equal<Current<FSRouteEmployee.routeID>>>>))]
  public virtual int? RouteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID", Required = true)]
  [FSSelector_Driver_All]
  public virtual int? EmployeeID { get; set; }

  [PXDBInt]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Priority Preference", Required = true)]
  public virtual int? PriorityPreference { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteEmployee.routeID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteEmployee.employeeID>
  {
  }

  public abstract class priorityPreference : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteEmployee.priorityPreference>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRouteEmployee.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteEmployee.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteEmployee.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSRouteEmployee.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteEmployee.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteEmployee.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSRouteEmployee.Tstamp>
  {
  }
}
