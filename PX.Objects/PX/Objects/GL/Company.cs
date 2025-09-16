// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Company
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Provides access to the settings of the company.
/// The information related to the company is edited on the Branches (CS102000) form
/// (which corresponds to the <see cref="T:PX.Objects.CS.BranchMaint" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (OrganizationMaint))]
[PXCacheName("Company")]
[Serializable]
public class Company : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CompanyCD;
  protected string _BaseCuryID;
  protected string _PhoneMask;
  protected byte[] _tstamp;

  /// <summary>Unique user-friendly identifier of the Company.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.AcctCD" /> field of the associated <see cref="T:PX.Objects.CR.BAccount">Business Account</see>.
  /// </value>
  [PXDBString(128 /*0x80*/, IsFixed = false, IsUnicode = true)]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXUIField]
  public virtual string CompanyCD
  {
    get => this._CompanyCD;
    set => this._CompanyCD = value;
  }

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
  public virtual string BaseCuryID
  {
    get => this._BaseCuryID;
    set => this._BaseCuryID = value;
  }

  /// <summary>
  /// The mask used to display phone numbers for all branches of the company.
  /// </summary>
  [PXDBString(50)]
  [PXDefault]
  [PXUIField(DisplayName = "Phone Mask")]
  public virtual string PhoneMask
  {
    get => this._PhoneMask;
    set => this._PhoneMask = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class UK : PrimaryKeyOf<Company>.By<Company.companyCD>
  {
    public static Company Find(PXGraph graph, string companyCD, PKFindOptions options = 0)
    {
      return (Company) PrimaryKeyOf<Company>.By<Company.companyCD>.FindBy(graph, (object) companyCD, options);
    }
  }

  public static class FK
  {
    public class BaseCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Company>.By<Company.baseCuryID>
    {
    }
  }

  public abstract class companyCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Company.companyCD>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Company.baseCuryID>
  {
  }

  public abstract class phoneMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Company.phoneMask>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Company.Tstamp>
  {
  }
}
