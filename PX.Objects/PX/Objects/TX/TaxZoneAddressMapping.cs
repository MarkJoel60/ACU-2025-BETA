// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxZoneAddressMapping
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
namespace PX.Objects.TX;

[PXCacheName("Tax Zone Address Mapping")]
public class TaxZoneAddressMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXParent(typeof (Select<TaxZone, Where<TaxZone.taxZoneID, Equal<Current<TaxZoneAddressMapping.taxZoneID>>>>))]
  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXDefault(typeof (TaxZone.taxZoneID))]
  public virtual 
  #nullable disable
  string TaxZoneID { get; set; }

  [PXUIField(DisplayName = "Country", Required = true)]
  [PXDBString(2, IsKey = true)]
  [PXSelector(typeof (PX.Objects.CS.Country.countryID), CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Country.description))]
  [PXDefault(typeof (TaxZone.countryID))]
  public virtual string CountryID { get; set; }

  [PXUIField(DisplayName = "State", Required = true)]
  [PXDBString(50, IsUnicode = true, IsKey = true)]
  [State(typeof (TaxZoneAddressMapping.countryID))]
  [PXUIRequired(typeof (Where<TaxZone.mappingType, Equal<MappingTypesAttribute.oneOrMoreStates>>))]
  [PXDefault("")]
  public virtual string StateID { get; set; }

  [PXUIField(DisplayName = "Name", Enabled = false)]
  [PXString(20)]
  [PXUIVisible(typeof (Where<BqlField<TaxZone.mappingType, IBqlString>.FromCurrent, NotEqual<MappingTypesAttribute.oneOrMorePostalCodes>>))]
  [PXFormula(typeof (Switch<Case<Where<BqlField<TaxZone.mappingType, IBqlString>.FromCurrent, Equal<MappingTypesAttribute.oneOrMoreStates>>, Selector<TaxZoneAddressMapping.stateID, PX.Objects.CS.State.name>>, Selector<TaxZoneAddressMapping.countryID, PX.Objects.CS.Country.description>>))]
  public virtual string Description { get; set; }

  [PXUIField(DisplayName = "From Postal Code", Required = true)]
  [PXDBString(20, IsKey = true, InputMask = "")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (TaxZoneAddressMapping.countryID))]
  [PXUIRequired(typeof (Where<TaxZone.mappingType, Equal<MappingTypesAttribute.oneOrMorePostalCodes>>))]
  [PXDefault("")]
  public virtual string FromPostalCode { get; set; }

  [PXUIField(DisplayName = "To Postal Code", Required = true)]
  [PXDBString(20)]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (TaxZoneAddressMapping.countryID))]
  [PXUIRequired(typeof (Where<TaxZone.mappingType, Equal<MappingTypesAttribute.oneOrMorePostalCodes>>))]
  [PXDefault("")]
  public virtual string ToPostalCode { get; set; }

  [PXString(20)]
  [PXDBCalced(typeof (IIf<BqlOperand<TaxZoneAddressMapping.fromPostalCode, IBqlString>.IsEqual<Space>, Space, BqlOperand<TaxZoneAddressMapping.toPostalCode, IBqlString>.Concat<ToPostalCodeSuffix>>), typeof (string))]
  public virtual string ToPostalCodeSuffixed { get; set; }

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

  public class PK : 
    PrimaryKeyOf<TaxZoneAddressMapping>.By<TaxZoneAddressMapping.countryID, TaxZoneAddressMapping.stateID, TaxZoneAddressMapping.fromPostalCode, TaxZoneAddressMapping.taxZoneID>
  {
    public static TaxZoneAddressMapping Find(
      PXGraph graph,
      string countryID,
      string stateID,
      string fromPostalCode,
      string taxZoneID,
      PKFindOptions options = 0)
    {
      return (TaxZoneAddressMapping) PrimaryKeyOf<TaxZoneAddressMapping>.By<TaxZoneAddressMapping.countryID, TaxZoneAddressMapping.stateID, TaxZoneAddressMapping.fromPostalCode, TaxZoneAddressMapping.taxZoneID>.FindBy(graph, (object) countryID, (object) stateID, (object) fromPostalCode, (object) taxZoneID, options);
    }
  }

  public static class FK
  {
    public class TaxZone : 
      PrimaryKeyOf<TaxZone>.By<TaxZone.taxZoneID>.ForeignKeyOf<TaxZoneAddressMapping>.By<TaxZoneAddressMapping.taxZoneID>
    {
    }
  }

  public abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.taxZoneID>
  {
  }

  public abstract class countryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.countryID>
  {
  }

  public abstract class stateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxZoneAddressMapping.stateID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.description>
  {
  }

  public abstract class fromPostalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.fromPostalCode>
  {
  }

  public abstract class toPostalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.toPostalCode>
  {
  }

  public abstract class toPostalCodeSuffixed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.toPostalCodeSuffixed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxZoneAddressMapping.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxZoneAddressMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZoneAddressMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxZoneAddressMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxZoneAddressMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxZoneAddressMapping.lastModifiedDateTime>
  {
  }
}
