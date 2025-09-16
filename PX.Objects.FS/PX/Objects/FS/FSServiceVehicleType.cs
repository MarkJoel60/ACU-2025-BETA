// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceVehicleType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (NonStockItemMaint))]
[Serializable]
public class FSServiceVehicleType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSServiceVehicleType.serviceID>>>>))]
  public virtual int? ServiceID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vehicle Type ID")]
  [PXSelector(typeof (Search<FSVehicleType.vehicleTypeID>), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD))]
  public virtual int? VehicleTypeID { get; set; }

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

  public class PK : 
    PrimaryKeyOf<FSServiceVehicleType>.By<FSServiceVehicleType.serviceID, FSServiceVehicleType.vehicleTypeID>
  {
    public static FSServiceVehicleType Find(
      PXGraph graph,
      int? serviceID,
      int? vehicleTypeID,
      PKFindOptions options = 0)
    {
      return (FSServiceVehicleType) PrimaryKeyOf<FSServiceVehicleType>.By<FSServiceVehicleType.serviceID, FSServiceVehicleType.vehicleTypeID>.FindBy(graph, (object) serviceID, (object) vehicleTypeID, options);
    }
  }

  public static class FK
  {
    public class Service : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSServiceVehicleType>.By<FSServiceVehicleType.serviceID>
    {
    }
  }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceVehicleType.serviceID>
  {
  }

  public abstract class vehicleTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceVehicleType.vehicleTypeID>
  {
  }

  public abstract class priorityPreference : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceVehicleType.priorityPreference>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceVehicleType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceVehicleType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceVehicleType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceVehicleType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceVehicleType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceVehicleType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceVehicleType.Tstamp>
  {
  }
}
