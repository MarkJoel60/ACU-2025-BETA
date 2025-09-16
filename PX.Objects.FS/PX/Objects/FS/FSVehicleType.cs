// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSVehicleType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Vehicle Type")]
[PXPrimaryGraph(typeof (VehicleTypeMaint))]
[Serializable]
public class FSVehicleType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? VehicleTypeID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  [PXSelector(typeof (Search<FSVehicleType.vehicleTypeCD>))]
  public virtual 
  #nullable disable
  string VehicleTypeCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

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

  [PXString]
  [PXUIField(DisplayName = "Vehicle Type")]
  public virtual string VehicleTypeGICD { get; set; }

  public class PK : PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>
  {
    public static FSVehicleType Find(PXGraph graph, int? vehicleTypeID, PKFindOptions options = 0)
    {
      return (FSVehicleType) PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.FindBy(graph, (object) vehicleTypeID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeCD>
  {
    public static FSVehicleType Find(PXGraph graph, string vehicleTypeCD, PKFindOptions options = 0)
    {
      return (FSVehicleType) PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeCD>.FindBy(graph, (object) vehicleTypeCD, options);
    }
  }

  public abstract class vehicleTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicleType.vehicleTypeID>
  {
  }

  public abstract class vehicleTypeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSVehicleType.vehicleTypeCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSVehicleType.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSVehicleType.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSVehicleType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSVehicleType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSVehicleType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSVehicleType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSVehicleType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSVehicleType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSVehicleType.Tstamp>
  {
  }

  public abstract class vehicleTypeGICD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSVehicleType.vehicleTypeGICD>
  {
  }
}
