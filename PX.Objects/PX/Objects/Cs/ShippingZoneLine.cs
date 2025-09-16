// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShippingZoneLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// A detail line of a <see cref="T:PX.Objects.CS.ShippingZone" /> record.
/// </summary>
[PXCacheName("Shipping Zone Line")]
[Serializable]
public class ShippingZoneLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The ID of the parent shipping zone.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (ShippingZone.zoneID))]
  [PXParent(typeof (ShippingZoneLine.FK.ShippingZone))]
  public virtual 
  #nullable disable
  string ZoneID { get; set; }

  /// <summary>
  /// The sequence number of the <see cref="T:PX.Objects.CS.ShippingZoneLine" /> detail line.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (ShippingZone.shippingZoneLineCntr))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(100)]
  [PXDefault]
  [Country]
  [PXUIField(DisplayName = "Country")]
  [PXForeignReference(typeof (ShippingZoneLine.FK.Country))]
  public string CountryID { get; set; }

  /// <summary>The ID of the state.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXDefault]
  [PXFormula(typeof (Default<ShippingZoneLine.countryID>))]
  [State(typeof (ShippingZoneLine.countryID), DescriptionField = typeof (State.name))]
  [PXUIEnabled(typeof (Where<ShippingZoneLine.countryID, IsNotNull>))]
  [PXUIField(DisplayName = "State")]
  [PXForeignReference(typeof (ShippingZoneLine.FK.State))]
  public string StateID { get; set; }

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

  public class PK : 
    PrimaryKeyOf<ShippingZoneLine>.By<ShippingZoneLine.zoneID, ShippingZoneLine.lineNbr>
  {
    public static ShippingZoneLine Find(
      PXGraph graph,
      string zoneID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ShippingZoneLine) PrimaryKeyOf<ShippingZoneLine>.By<ShippingZoneLine.zoneID, ShippingZoneLine.lineNbr>.FindBy(graph, (object) zoneID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ShippingZone : 
      PrimaryKeyOf<ShippingZone>.By<ShippingZone.zoneID>.ForeignKeyOf<ShippingZoneLine>.By<ShippingZoneLine.zoneID>
    {
    }

    public class Country : 
      PrimaryKeyOf<Country>.By<Country.countryID>.ForeignKeyOf<ShippingZoneLine>.By<ShippingZoneLine.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<State>.By<State.countryID, State.stateID>.ForeignKeyOf<ShippingZoneLine>.By<ShippingZoneLine.countryID, ShippingZoneLine.stateID>
    {
    }
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ShippingZoneLine.zoneID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ShippingZoneLine.lineNbr>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ShippingZoneLine.countryID>
  {
  }

  public abstract class stateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ShippingZoneLine.stateID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ShippingZoneLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ShippingZoneLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ShippingZoneLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ShippingZoneLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ShippingZoneLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ShippingZoneLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ShippingZoneLine.Tstamp>
  {
  }
}
