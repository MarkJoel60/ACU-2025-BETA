// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.Organization
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.DAC;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

[PXCacheName("Company")]
[Serializable]
public class Organization : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted,
  I1099Settings
{
  protected bool? _Selected = new bool?(false);
  protected Guid? _DefaultPrinterID;
  protected 
  #nullable disable
  byte[] _GroupMask;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBIdentity]
  public virtual int? OrganizationID { get; set; }

  [PXDimension("COMPANY")]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  [PXDBDefault(typeof (OrganizationBAccount.acctCD))]
  public virtual string OrganizationCD { get; set; }

  [PXDBString(15)]
  [OrganizationTypes.List]
  [PXDefault("WithoutBranches")]
  [PXUIField(DisplayName = "Company Type")]
  public virtual string OrganizationType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "File Taxes by Branch")]
  public virtual bool? FileTaxesByBranches { get; set; }

  /// <summary>
  /// The base <see cref="T:PX.Objects.CM.Currency" /> of the company.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Base Currency ID")]
  [PXSelector(typeof (Search<CurrencyList.curyID>), DescriptionField = typeof (CurrencyList.description))]
  public virtual string BaseCuryID { get; set; }

  [Branch(null, null, false, true, false, DisplayName = "Dunning Letter Branch", FieldClass = "COMPANYBRANCH")]
  public virtual int? DunningFeeBranchID { get; set; }

  [PXDBInt]
  public virtual int? ActualLedgerID { get; set; }

  [PXString(10, IsUnicode = true)]
  public virtual string ActualLedgerCD { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Active", FieldClass = "MULTICOMPANY")]
  [PXDefault(true)]
  public virtual bool? Active { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Status", IsReadOnly = true)]
  [PXDefault("A")]
  [OrganizationStatus.List]
  public string Status { get; set; }

  /// <summary>The name of the organization.</summary>
  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string OrganizationName { get; set; }

  /// <summary>
  /// The name of the <see cref="T:PX.SM.Roles">Role</see> to be used to grant users access to the data of the Organization.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.SM.Roles.Rolename" /> field.
  /// </value>
  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXSelector(typeof (Search<Roles.rolename, Where<Roles.guest, Equal<False>>>), DescriptionField = typeof (Roles.descr))]
  [PXUIField(DisplayName = "Access Role")]
  public virtual string RoleName { get; set; }

  /// <summary>
  /// The mask used to display phone numbers for the objects, which belong to this Organization.
  /// See also the <see cref="P:PX.Objects.GL.Company.PhoneMask" />.
  /// </summary>
  [PXDBString(50)]
  [PXDefault]
  [PXUIField(DisplayName = "Phone Mask")]
  public virtual string PhoneMask { get; set; }

  /// <summary>
  /// Identifier of the default Country of the Organization.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Country.CountryID" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Default Country")]
  [PXSelector(typeof (PX.Objects.CS.Country.countryID), DescriptionField = typeof (PX.Objects.CS.Country.description))]
  public virtual string CountryID { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Localization")]
  [PXDefault("00")]
  [OrganizationLocalizationList]
  public virtual string OrganizationLocalizationCode { get; set; }

  /// <summary>Represents the type of cash discount base.</summary>
  /// <value>
  /// The field can have one of the values listed in the <see cref="T:PX.Objects.CS.CashDiscountBases" /> class.
  /// </value>
  [PXDBString(2)]
  [CashDiscountBases.List]
  [PXDefault("DA")]
  [PXUIField(DisplayName = "Cash Discount Base")]
  public virtual string CashDiscountBase { get; set; }

  [PXPrinterSelector]
  [PXForeignReference(typeof (Field<Organization.defaultPrinterID>.IsRelatedTo<SMPrinter.printerID>))]
  public virtual Guid? DefaultPrinterID
  {
    get => this._DefaultPrinterID;
    set => this._DefaultPrinterID = value;
  }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Carrier Facility")]
  public virtual string CarrierFacility { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (PXThemeVariableAttribute.ThemeHasVariables))]
  [PXUIField(DisplayName = "Override Colors for the Selected Company")]
  public bool? OverrideThemeVariables { get; set; }

  [PXString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Primary Color")]
  [PXThemeVariable("--primary-color", PersistDefaultValue = typeof (Organization.overrideThemeVariables))]
  [PXUIEnabled(typeof (Organization.overrideThemeVariables))]
  public string PrimaryColor { get; set; }

  [PXString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Background Color")]
  [PXThemeVariable("--background-color", PersistDefaultValue = typeof (Organization.overrideThemeVariables))]
  [PXUIEnabled(typeof (Organization.overrideThemeVariables))]
  public string BackgroundColor { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.CR.BAccount">Business Account</see> of the Organization.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(Visible = true, Enabled = false)]
  [PXSelector(typeof (BAccountR.bAccountID), ValidateValue = false)]
  [PXDBDefault(typeof (OrganizationBAccount.bAccountID))]
  public virtual int? BAccountID { get; set; }

  /// <summary>The name of the logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Logo File")]
  public string LogoName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Logo File")]
  public string LogoNameGetter
  {
    get => this.LogoName;
    set
    {
    }
  }

  /// <summary>The name of the report logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Report Logo File")]
  public string LogoNameReport { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Logo File")]
  public string LogoNameReportGetter
  {
    get => this.LogoNameReport;
    set
    {
    }
  }

  /// <summary>Transmitter Control Code (TCC) for the 1099 form.</summary>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Transmitter Control Code (TCC)")]
  public virtual string TCC { get; set; }

  /// <summary>
  /// Indicates whether the Organization is considered a Foreign Entity in the context of 1099 form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Foreign Entity")]
  public virtual bool? ForeignEntity { get; set; }

  /// <summary>Combined Federal/State Filer for the 1099 form.</summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Combined Federal/State Filer")]
  public virtual bool? CFSFiler { get; set; }

  /// <summary>Contact Name for the 1099 form.</summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Name")]
  public virtual string ContactName { get; set; }

  /// <summary>Contact Phone Number for the 1099 form.</summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Telephone Number")]
  public virtual string CTelNumber { get; set; }

  /// <summary>Contact E-mail for the 1099 form.</summary>
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact E-mail")]
  public virtual string CEmail { get; set; }

  /// <summary>Name Control for the 1099 form.</summary>
  [PXDBString(4, IsUnicode = true)]
  [PXUIField(DisplayName = "Name Control")]
  public virtual string NameControl { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "1099-MISC Reporting Entity")]
  [PXUIEnabled(typeof (Where<BqlOperand<Organization.reporting1099ByBranches, IBqlBool>.IsNotEqual<True>>))]
  public virtual bool? Reporting1099 { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIVisible(typeof (Where<BqlOperand<Organization.organizationType, IBqlString>.IsEqual<OrganizationTypes.withBranchesBalancing>>))]
  [PXUIField(DisplayName = "File 1099-MISC by Branch")]
  public virtual bool? Reporting1099ByBranches { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the Branch belongs to.
  /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL104000) form
  /// (which corresponds to the <see cref="T:PX.Objects.GL.GLAccess" /> graph).
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask { get; set; }

  /// <summary>
  /// An unbound field used in the User Interface to include the Organization into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public class PK : PrimaryKeyOf<Organization>.By<Organization.organizationID>
  {
    public static Organization Find(PXGraph graph, int? organizationID, PKFindOptions options = 0)
    {
      return (Organization) PrimaryKeyOf<Organization>.By<Organization.organizationID>.FindBy(graph, (object) organizationID, options);
    }
  }

  public class UK : PrimaryKeyOf<Organization>.By<Organization.organizationCD>
  {
    public static Organization Find(PXGraph graph, string organizationCD, PKFindOptions options = 0)
    {
      return (Organization) PrimaryKeyOf<Organization>.By<Organization.organizationCD>.FindBy(graph, (object) organizationCD, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<Organization>.By<Organization.bAccountID>
    {
    }

    public class ActualLedger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<Organization>.By<Organization.actualLedgerID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<Organization>.By<Organization.countryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.selected>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Organization.organizationID>
  {
  }

  public abstract class organizationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.organizationCD>
  {
  }

  public abstract class organizationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.organizationType>
  {
  }

  public abstract class fileTaxesByBranches : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Organization.fileTaxesByBranches>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.baseCuryID>
  {
  }

  public abstract class dunningFeeBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Organization.dunningFeeBranchID>
  {
  }

  public abstract class actualLedgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Organization.actualLedgerID>
  {
  }

  public abstract class actualLedgerCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.actualLedgerCD>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.active>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.status>
  {
  }

  public abstract class organizationName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.organizationName>
  {
  }

  public abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.roleName>
  {
  }

  public abstract class phoneMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.phoneMask>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.countryID>
  {
  }

  public abstract class organizationLocalizationCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.organizationLocalizationCode>
  {
  }

  public abstract class cashDiscountBase : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.cashDiscountBase>
  {
  }

  public abstract class defaultPrinterID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    Organization.defaultPrinterID>
  {
  }

  public abstract class carrierFacility : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.carrierFacility>
  {
  }

  public abstract class overrideThemeVariables : IBqlField, IBqlOperand
  {
  }

  public abstract class primaryColor : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.primaryColor>
  {
  }

  public abstract class backgroundColor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.backgroundColor>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Organization.bAccountID>
  {
  }

  public abstract class logoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.logoName>
  {
  }

  public abstract class logoNameReport : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.logoNameReport>
  {
  }

  public abstract class tCC : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.tCC>
  {
  }

  public abstract class foreignEntity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.foreignEntity>
  {
  }

  public abstract class cFSFiler : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.cFSFiler>
  {
  }

  public abstract class contactName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.contactName>
  {
  }

  public abstract class cTelNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.cTelNumber>
  {
  }

  public abstract class cEmail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.cEmail>
  {
  }

  public abstract class nameControl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Organization.nameControl>
  {
  }

  public abstract class reporting1099 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.reporting1099>
  {
  }

  public abstract class reporting1099ByBranches : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Organization.reporting1099ByBranches>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Organization.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Organization.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    Organization.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Organization.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Organization.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Organization.Tstamp>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Organization.groupMask>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Organization.included>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Organization.noteID>
  {
  }
}
