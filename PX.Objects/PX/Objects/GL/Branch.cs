// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Branch
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
namespace PX.Objects.GL;

/// <summary>
/// A branch of the company.
/// Records of this type are added and edited on the Branches (CS102000) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.BranchMaint" /> graph).
/// </summary>
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (OrganizationMaint), typeof (BranchMaint), typeof (BranchMaint), typeof (BranchMaint)}, new System.Type[] {typeof (Select<OrganizationBAccount, Where<Current<Branch.bAccountID>, Equal<OrganizationBAccount.bAccountID>, And<Not<FeatureInstalled<FeaturesSet.branch>>>>>), typeof (Select<BranchMaint.BranchBAccount, Where<BranchMaint.BranchBAccount.branchBAccountID, Equal<Current<Branch.bAccountID>>>>), typeof (Where<Branch.branchID, Less<Zero>>), typeof (Where<True, Equal<True>>)})]
[PXCacheName("Branch", CacheGlobal = true)]
[Serializable]
public class Branch : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted,
  I1099Settings
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _BranchCD;
  private string _RoleName;
  protected int? _LedgerID;
  protected string _LedgerCD;
  protected int? _BAccountID;
  protected string _PhoneMask;
  protected string _CountryID;
  protected byte[] _GroupMask;
  protected int? _AcctMapNbr;
  protected string _AcctName;
  protected byte[] _tstamp;
  protected bool? _Included;
  protected Guid? _DefaultPrinterID;

  /// <summary>
  /// Reference to <see cref="T:PX.Objects.GL.DAC.Organization" /> record to which the Branch belongs.
  /// </summary>
  [Organization(true, typeof (Search<PX.Objects.GL.DAC.Organization.organizationID>), null)]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Database identity.
  /// Unique identifier of the Branch.
  /// </summary>
  [PXDBIdentity]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Key field.
  /// User-friendly unique identifier of the Branch.
  /// </summary>
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (Branch.BAccount.acctCD))]
  [PXDimensionSelector("BRANCH", typeof (Search<Branch.branchCD, Where<Match<Current<AccessInfo.userName>>>>), typeof (Branch.branchCD))]
  [PXUIField]
  public virtual string BranchCD
  {
    get => this._BranchCD;
    set => this._BranchCD = value;
  }

  /// <summary>
  /// The name of the <see cref="T:PX.SM.Roles">Role</see> to be used to grant users access to the data of the Branch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.SM.Roles.Rolename" /> field.
  /// </value>
  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXSelector(typeof (Search<Roles.rolename, Where<Roles.guest, Equal<False>>>), DescriptionField = typeof (Roles.descr))]
  [PXUIField(DisplayName = "Access Role")]
  public string RoleName
  {
    get => this._RoleName;
    set => this._RoleName = value;
  }

  /// <summary>The name of the logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Logo File")]
  public string LogoName { get; set; }

  /// <summary>The name of the report logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Report Logo File")]
  public string LogoNameReport { get; set; }

  /// <summary>The name of the main logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Logo File")]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public string MainLogoName { get; set; }

  /// <summary>The name of the main logo image file.</summary>
  [PXDBString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Organization Logo File")]
  public string OrganizationLogoNameReport { get; set; }

  /// <summary>The name of the main logo image file.</summary>
  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Branch or Organization Report Logo File")]
  [PXFormula(typeof (IsNull<Branch.logoNameReport, Branch.organizationLogoNameReport>))]
  public string BranchOrOrganizationLogoNameReport { get; set; }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the transactions belonging to this Branch are posted by default.
  /// </summary>
  /// <value>
  /// Corresonds to the <see cref="P:PX.Objects.GL.Ledger.LedgerID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<Ledger.ledgerID, Where<Ledger.balanceType, Equal<PX.Objects.CM.ActualLedger>>>), DescriptionField = typeof (Ledger.descr), SubstituteKey = typeof (Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>
  /// User-friendly identifier of the <see cref="T:PX.Objects.GL.Ledger" />, to which the transactions belonging to this Branch are posted by default.
  /// </summary>
  /// <value>
  /// Corresonds to the <see cref="P:PX.Objects.GL.Ledger.LedgerCD" /> field.
  /// </value>
  [PXString(10, IsUnicode = true)]
  public virtual string LedgerCD
  {
    get => this._LedgerCD;
    set => this._LedgerCD = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch.BAccount">Business Account</see> of the Branch.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccount.BAccountID" /> field.
  /// </value>
  [PXDBInt]
  [PXUIField(Visible = true, Enabled = false)]
  [PXSelector(typeof (BAccountR.bAccountID), ValidateValue = false)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>Indicates whether the Branch is active.</summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public bool? Active { get; set; }

  /// <summary>
  /// The mask used to display phone numbers for the objects, which belong to this Branch.
  /// See also the <see cref="P:PX.Objects.GL.Company.PhoneMask" />.
  /// </summary>
  [PXDBString(50)]
  [PXDefault]
  [PXUIField(DisplayName = "Phone Mask")]
  public virtual string PhoneMask
  {
    get => this._PhoneMask;
    set => this._PhoneMask = value;
  }

  /// <summary>Identifier of the default Country of the Branch.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CS.Country.CountryID" /> field.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Default Country")]
  [PXSelector(typeof (PX.Objects.CS.Country.countryID), DescriptionField = typeof (PX.Objects.CS.Country.description))]
  public virtual string CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  /// <summary>
  /// The group mask showing which <see cref="T:PX.SM.RelationGroup">restriction groups</see> the Branch belongs to.
  /// To learn more about the way restriction groups are managed, see the documentation for the GL Account Access (GL104000) form
  /// (which corresponds to the <see cref="T:PX.Objects.GL.GLAccess" /> graph).
  /// </summary>
  [PXDBGroupMask]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  /// <summary>
  /// The counter of the Branch Account Mapping records associated with this Branch.
  /// This field is used to assign consistent values to the <see cref="P:PX.Objects.GL.BranchAcctMap.LineNbr" />,
  /// <see cref="P:PX.Objects.GL.BranchAcctMapFrom.LineNbr" /> and <see cref="P:PX.Objects.GL.BranchAcctMapTo.LineNbr" />.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? AcctMapNbr
  {
    get => this._AcctMapNbr;
    set => this._AcctMapNbr = value;
  }

  /// <summary>The name of the branch.</summary>
  /// <value>
  /// This unbound field corresponds to the <see cref="P:PX.Objects.GL.Branch.BAccount.AcctName" /> field.
  /// </value>
  [PXDBScalar(typeof (Search<Branch.BAccount.acctName, Where<Branch.BAccount.bAccountID, Equal<Branch.bAccountID>>>))]
  [PXString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  /// <summary>
  /// The base <see cref="T:PX.Objects.CM.Currency" /> of the Branch.
  /// </summary>
  /// <value>
  /// This unbound field corresponds to the <see cref="P:PX.Objects.GL.DAC.Organization.BaseCuryID" />.
  /// </value>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Base Currency ID")]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  public virtual string BaseCuryID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// An unbound field used in the User Interface to include the Branch into a <see cref="T:PX.SM.RelationGroup">restriction group</see>.
  /// </summary>
  [PXUnboundDefault(false)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  /// <summary>Transmitter Control Code (TCC) for the 1099 form.</summary>
  [PXDBString(5, IsUnicode = true)]
  [PXUIField(DisplayName = "Transmitter Control Code (TCC)")]
  public virtual string TCC { get; set; }

  /// <summary>
  /// Indicates whether the Branch is considered a Foreign Entity in the context of 1099 form.
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
  public virtual bool? Reporting1099 { get; set; }

  /// <summary>The identifier of the consolidation branch.</summary>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXDBInt]
  public virtual int? ParentBranchID { get; set; }

  [PXPrinterSelector]
  [PXForeignReference(typeof (Field<Branch.defaultPrinterID>.IsRelatedTo<SMPrinter.printerID>))]
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
  [PXUIField(DisplayName = "Override Colors for the Selected Branch")]
  public bool? OverrideThemeVariables { get; set; }

  public class PK : PrimaryKeyOf<Branch>.By<Branch.branchID>
  {
    public static Branch Find(PXGraph graph, int? branchID, PKFindOptions options = 0)
    {
      return (Branch) PrimaryKeyOf<Branch>.By<Branch.branchID>.FindBy(graph, (object) branchID, options);
    }
  }

  public class UK : PrimaryKeyOf<Branch>.By<Branch.branchCD>
  {
    public static Branch Find(PXGraph graph, string branchCD, PKFindOptions options = 0)
    {
      return (Branch) PrimaryKeyOf<Branch>.By<Branch.branchCD>.FindBy(graph, (object) branchCD, options);
    }
  }

  public static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<Branch>.By<Branch.bAccountID>
    {
    }

    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<Branch>.By<Branch.organizationID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<Branch>.By<Branch.ledgerID>
    {
    }

    public class BaseCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Branch>.By<Branch.baseCuryID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<Branch>.By<Branch.countryID>
    {
    }
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.branchID>
  {
  }

  public abstract class branchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.branchCD>
  {
  }

  public abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.roleName>
  {
  }

  public abstract class logoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.logoName>
  {
  }

  public abstract class logoNameReport : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.logoNameReport>
  {
  }

  public abstract class mainLogoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.mainLogoName>
  {
  }

  public abstract class organizationLogoNameReport : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Branch.organizationLogoNameReport>
  {
  }

  public abstract class branchOrOrganizationLogoNameReport : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Branch.branchOrOrganizationLogoNameReport>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.ledgerID>
  {
  }

  public abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.ledgerCD>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.bAccountID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.active>
  {
  }

  public abstract class phoneMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.phoneMask>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.countryID>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Branch.groupMask>
  {
  }

  public abstract class acctMapNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.acctMapNbr>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.acctName>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.baseCuryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Branch.Tstamp>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.included>
  {
  }

  public abstract class tCC : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.tCC>
  {
  }

  public abstract class foreignEntity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.foreignEntity>
  {
  }

  public abstract class cFSFiler : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.cFSFiler>
  {
  }

  public abstract class contactName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.contactName>
  {
  }

  public abstract class cTelNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.cTelNumber>
  {
  }

  public abstract class cEmail : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.cEmail>
  {
  }

  public abstract class nameControl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.nameControl>
  {
  }

  public abstract class reporting1099 : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Branch.reporting1099>
  {
  }

  public abstract class parentBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Branch.parentBranchID>
  {
  }

  public abstract class defaultPrinterID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Branch.defaultPrinterID>
  {
  }

  public abstract class carrierFacility : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Branch.carrierFacility>
  {
  }

  public abstract class overrideThemeVariables : IBqlField, IBqlOperand
  {
  }

  [PXHidden]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BAccountID;
    protected string _AcctCD;
    protected string _AcctName;

    [PXDBIdentity]
    [PXUIField]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField]
    public virtual string AcctCD
    {
      get => this._AcctCD;
      set => this._AcctCD = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string AcctName
    {
      get => this._AcctName;
      set => this._AcctName = value;
    }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Branch.BAccount.bAccountID>
    {
    }

    public abstract class acctCD : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Branch.BAccount.acctCD>
    {
    }

    public abstract class acctName : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Branch.BAccount.acctName>
    {
    }
  }
}
