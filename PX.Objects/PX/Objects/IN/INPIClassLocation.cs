// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIClassLocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Physical Inventory Type by Location")]
[Serializable]
public class INPIClassLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PIClassID;
  protected int? _LocationID;

  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INPIClass.pIClassID))]
  [PXParent(typeof (INPIClassLocation.FK.PIClass))]
  public virtual string PIClassID
  {
    get => this._PIClassID;
    set => this._PIClassID = value;
  }

  [Location(typeof (INPIClass.siteID), IsKey = true)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INPIClassLocation>.By<INPIClassLocation.pIClassID, INPIClassLocation.locationID>
  {
    public static INPIClassLocation Find(
      PXGraph graph,
      string pIClassID,
      int? locationID,
      PKFindOptions options = 0)
    {
      return (INPIClassLocation) PrimaryKeyOf<INPIClassLocation>.By<INPIClassLocation.pIClassID, INPIClassLocation.locationID>.FindBy(graph, (object) pIClassID, (object) locationID, options);
    }
  }

  public static class FK
  {
    public class PIClass : 
      PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>.ForeignKeyOf<INPIClassLocation>.By<INPIClassLocation.pIClassID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INPIClassLocation>.By<INPIClassLocation.locationID>
    {
    }
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClassLocation.pIClassID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIClassLocation.locationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIClassLocation.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIClassLocation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClassLocation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClassLocation.createdDateTime>
  {
  }
}
