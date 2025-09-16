// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SalesTerritory
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
/// Represents a Sales territory
/// Records of this type are created and edited through the Sales territory (CS204100) screen
/// (corresponds to the <see cref="T:PX.Objects.CS.SalesTerritoryMaint" /> graph).
/// </summary>
[PXCacheName("Sales Territory")]
[PXPrimaryGraph(typeof (SalesTerritoryMaint))]
[Serializable]
public class SalesTerritory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>The primary key.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Sales Territory")]
  [PXSelector(typeof (Search2<SalesTerritory.salesTerritoryID, LeftJoin<Country, On<Country.countryID, Equal<SalesTerritory.countryID>>>>), new System.Type[] {typeof (SalesTerritory.salesTerritoryID), typeof (SalesTerritory.name), typeof (SalesTerritory.salesTerritoryType), typeof (Country.countryID), typeof (Country.description), typeof (SalesTerritory.isActive)})]
  [PXReferentialIntegrityCheck]
  public virtual 
  #nullable disable
  string SalesTerritoryID { get; set; }

  /// <summary>The name of Sales territory.</summary>
  [PXDBLocalizableString(50, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Territory Name", FieldClass = "CRM")]
  public virtual string Name { get; set; }

  /// <summary>
  /// Indicates (if set to <see langword="true" />) that the sales territory is active.
  /// </summary>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? IsActive { get; set; }

  /// <summary>
  /// Type of sales territory <see cref="T:PX.Objects.CS.SalesTerritoryTypeAttribute" />.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PX.Objects.CS.SalesTerritoryType]
  [PXDefault("S")]
  public virtual string SalesTerritoryType { get; set; }

  /// <summary>
  /// The <see cref="P:PX.Objects.CR.Address.CountryID" /> identifier for Sales Territories of type <see cref="T:PX.Objects.CS.SalesTerritoryTypeAttribute.byState" />
  /// </summary>
  /// <inheritdoc cref="P:PX.Objects.CR.Address.CountryID" />
  [PXDBString(100)]
  [PXUIField]
  [PXDefault]
  [PXUIVisible(typeof (Where<BqlOperand<SalesTerritory.salesTerritoryType, IBqlString>.IsEqual<SalesTerritoryTypeAttribute.byState>>))]
  [PXUIRequired(typeof (Where<BqlOperand<SalesTerritory.salesTerritoryType, IBqlString>.IsEqual<SalesTerritoryTypeAttribute.byState>>))]
  [Country]
  public virtual string CountryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

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

  public class PK : PrimaryKeyOf<SalesTerritory>.By<SalesTerritory.salesTerritoryID>
  {
    public static SalesTerritory Find(PXGraph graph, string salesTerritoryID)
    {
      return (SalesTerritory) PrimaryKeyOf<SalesTerritory>.By<SalesTerritory.salesTerritoryID>.FindBy(graph, (object) salesTerritoryID, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class Country : 
      PrimaryKeyOf<Country>.By<Country.countryID>.ForeignKeyOf<SalesTerritory>.By<SalesTerritory.countryID>
    {
    }
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesTerritory.salesTerritoryID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesTerritory.name>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SalesTerritory.isActive>
  {
  }

  public abstract class salesTerritoryType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesTerritory.salesTerritoryType>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SalesTerritory.countryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SalesTerritory.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SalesTerritory.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SalesTerritory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesTerritory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesTerritory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SalesTerritory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SalesTerritory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SalesTerritory.lastModifiedDateTime>
  {
  }
}
