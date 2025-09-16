// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Country
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>
/// Represents a country, in which the organization has operations, customers or vendors, and provides
/// information used for defining <see cref="T:PX.Objects.GL.Branch">Branches</see> and creating
/// <see cref="T:PX.Objects.AP.Vendor">Vendors</see> and <see cref="T:PX.Objects.AR.Customer">Customers</see>.
/// Records of this type are created and edited through the Countries/States (CS204000) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.CountryMaint" /> graph).
/// </summary>
[PXCacheName]
[PXPrimaryGraph(new Type[] {typeof (CountryMaint)}, new Type[] {typeof (Select<Country, Where<Country.countryID, Equal<Current<Country.countryID>>>>)})]
[Serializable]
public class Country : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CountryID;
  protected string _Description;
  protected string _ZipCodeMask;
  protected string _ZipCodeRegexp;
  protected string _PhoneCountryCode;
  protected string _PhoneMask;
  protected string _PhoneRegexp;
  protected string _AddressValidatorPluginID;
  protected string _LanguageID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>Indicates whether the record is selected or not.</summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected { get; set; }

  /// <summary>
  /// Key field.
  /// The unique two-letter identifier of the Country.
  /// </summary>
  /// <value>
  /// The identifiers of the countries are defined by the ISO 3166 standard.
  /// </value>
  [PXDBString(100, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Country.countryID), CacheGlobal = true, DescriptionField = typeof (Country.description))]
  [PXReferentialIntegrityCheck]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <summary>The complete name of the Country.</summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [Country.countryValidationMethod.PXCountryValidationMethod]
  [PXDefault("I")]
  [PXUIField(DisplayName = "Validation Mode")]
  public virtual string CountryValidationMethod { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Validation Regexp")]
  public virtual string CountryRegexp { get; set; }

  [PXDBString(1, IsFixed = true)]
  [Country.stateValidationMethod.PXStateValidationMethod]
  [PXDefault("I")]
  [PXUIField(DisplayName = "Validation Mode")]
  public virtual string StateValidationMethod { get; set; }

  /// <summary>
  /// A mask that is used to validate postal codes belonging to this Country, when they are entered.
  /// </summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Input Mask")]
  public virtual string ZipCodeMask
  {
    get => this._ZipCodeMask;
    set => this._ZipCodeMask = value;
  }

  /// <summary>
  /// A regular expression that is used to validate postal codes belonging to this Country, when they are entered.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Validation Regexp")]
  public virtual string ZipCodeRegexp
  {
    get => this._ZipCodeRegexp;
    set => this._ZipCodeRegexp = value;
  }

  /// <summary>The phone code of the Country.</summary>
  [PXDBString(5)]
  [PXUIField(DisplayName = "Country Phone Code")]
  public virtual string PhoneCountryCode
  {
    get => this._PhoneCountryCode;
    set => this._PhoneCountryCode = value;
  }

  /// <summary>
  /// A mask that is used to validate phone numbers belonging to this Country, when they are entered.
  /// </summary>
  [PXDBString(50)]
  [PXUIField(DisplayName = "Input Mask")]
  public virtual string PhoneMask
  {
    get => this._PhoneMask;
    set => this._PhoneMask = value;
  }

  /// <summary>
  /// A regular expression that is used to validate phone numbers belonging to this Country, when they are entered.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Phone Validation Reg. Exp.")]
  public virtual string PhoneRegexp
  {
    get => this._PhoneRegexp;
    set => this._PhoneRegexp = value;
  }

  /// <summary>
  /// !REV!
  /// Obsolete field.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Tax Registration Required")]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual bool? IsTaxRegistrationRequired { get; set; }

  /// <summary>
  /// !REV!
  /// Obsolete field.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Tax Registration Mask")]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual string TaxRegistrationMask { get; set; }

  /// <summary>
  /// !REV!
  /// Obsolete field.
  /// </summary>
  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Tax Reg. Validation Reg. Exp.")]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public virtual string TaxRegistrationRegexp { get; set; }

  /// <value>
  /// <see cref="T:PX.Objects.CS.AddressValidatorPlugin.addressValidatorPluginID" /> of a address validator which will bu used.
  /// </value>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Validation Plug-In")]
  [PXSelector(typeof (AddressValidatorPlugin.addressValidatorPluginID), DescriptionField = typeof (AddressValidatorPlugin.description))]
  [PXRestrictor(typeof (Where<AddressValidatorPlugin.isActive, Equal<True>>), "The address validation plug-in you have typed is not active. Click the magnifier icon to view the list of active plug-ins.", new Type[] {})]
  public virtual string AddressValidatorPluginID
  {
    get => this._AddressValidatorPluginID;
    set => this._AddressValidatorPluginID = value;
  }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Address Automatically")]
  public virtual bool? AutoOverrideAddress { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Language/Locale")]
  [PXSelector(typeof (Locale.localeName), DescriptionField = typeof (Locale.description))]
  public virtual string LanguageID
  {
    get => this._LanguageID;
    set => this._LanguageID = value;
  }

  /// <summary>
  /// The reference to <see cref="T:PX.Objects.CS.SalesTerritory.salesTerritoryID" /> to that country belongs to.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<SalesTerritory.salesTerritoryID, Where<SalesTerritory.salesTerritoryType, Equal<SalesTerritoryTypeAttribute.byCountry>>>), new Type[] {typeof (SalesTerritory.salesTerritoryID), typeof (SalesTerritory.name)}, DescriptionField = typeof (SalesTerritory.name))]
  [PXRestrictor(typeof (Where<SalesTerritory.isActive, Equal<True>>), "The sales territory is inactive.", new Type[] {}, ShowWarning = true)]
  [PXForeignReference]
  public virtual string SalesTerritoryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <exclude />
  public class PK : PrimaryKeyOf<Country>.By<Country.countryID>
  {
    public static Country Find(PXGraph graph, string countryID, PKFindOptions options = 0)
    {
      return (Country) PrimaryKeyOf<Country>.By<Country.countryID>.FindBy(graph, (object) countryID, options);
    }
  }

  public static class FK
  {
    public class SalesTerritory : 
      PrimaryKeyOf<SalesTerritory>.By<SalesTerritory.salesTerritoryID>.ForeignKeyOf<Country>.By<Country.salesTerritoryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Country.selected>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.countryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.description>
  {
  }

  public abstract class countryValidationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.countryValidationMethod>
  {
    public const string ID = "I";
    public const string Name = "N";
    public const string NameRegex = "R";

    public class PXCountryValidationMethodAttribute : PXStringListAttribute
    {
      public PXCountryValidationMethodAttribute()
        : base(new string[3]{ "I", "N", "R" }, new string[3]
        {
          "By Country ID",
          "By Country ID and Name",
          "By Country ID, Name, and Regexp"
        })
      {
      }
    }
  }

  public abstract class countryRegexp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.countryRegexp>
  {
  }

  public abstract class stateValidationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.stateValidationMethod>
  {
    public const string No = "X";
    public const string ID = "I";
    public const string Name = "N";
    public const string NameRegex = "R";

    public class PXStateValidationMethodAttribute : PXStringListAttribute
    {
      public PXStateValidationMethodAttribute()
        : base(new string[4]{ "X", "I", "N", "R" }, new string[4]
        {
          "No Validation",
          "By State ID",
          "By State ID and Name",
          "By State ID, Name, and Regexp"
        })
      {
      }
    }
  }

  public abstract class zipCodeMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.zipCodeMask>
  {
  }

  public abstract class zipCodeRegexp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.zipCodeRegexp>
  {
  }

  public abstract class phoneCountryCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.phoneCountryCode>
  {
  }

  public abstract class phoneMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.phoneMask>
  {
  }

  public abstract class phoneRegexp : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.phoneRegexp>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class isTaxRegistrationRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Country.isTaxRegistrationRequired>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class taxRegistrationMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.taxRegistrationMask>
  {
  }

  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2017R2.")]
  public abstract class taxRegistrationRegexp : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.taxRegistrationRegexp>
  {
  }

  public abstract class addressValidatorPluginID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.addressValidatorPluginID>
  {
  }

  /// <exclude />
  public abstract class autoOverrideAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Country.autoOverrideAddress>
  {
  }

  public abstract class languageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Country.languageID>
  {
  }

  public abstract class salesTerritoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.salesTerritoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Country.noteID>
  {
  }

  /// <exclude />
  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Country.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Country.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Country.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Country.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Country.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Country.lastModifiedDateTime>
  {
  }
}
