// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// An accounts payable history record, which accumulates a number
/// of important year-to-date and period-to-date amounts (such as purchases, debit and credit
/// adjustments, gains and losses) in base currency. The history is accumulated separately
/// across the following dimensions: branch, GL account, GL subaccount, financial period,
/// and vendor. History records are created and updated during the document release
/// process (see <see cref="T:PX.Objects.AP.APDocumentRelease" /> graph). Various helper projections
/// over this DAC are used in a number of AR inquiry forms and reports, such as Vendor
/// Summary (AP401000).
/// </summary>
[PXCacheName("AP History")]
[Serializable]
public class APHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _VendorID;
  protected bool? _DetDeleted;
  protected Decimal? _FinBegBalance;
  protected Decimal? _FinPtdPurchases;
  protected Decimal? _FinPtdPayments;
  protected Decimal? _FinPtdDrAdjustments;
  protected Decimal? _FinPtdCrAdjustments;
  protected Decimal? _FinPtdDiscTaken;
  protected Decimal? _FinPtdWhTax;
  protected Decimal? _FinPtdRGOL;
  protected Decimal? _FinYtdBalance;
  protected Decimal? _FinPtdDeposits;
  protected Decimal? _FinYtdDeposits;
  protected Decimal? _FinPtdRevalued;
  protected Decimal? _TranBegBalance;
  protected Decimal? _TranPtdPurchases;
  protected Decimal? _TranPtdPayments;
  protected Decimal? _TranPtdDrAdjustments;
  protected Decimal? _TranPtdCrAdjustments;
  protected Decimal? _TranPtdDiscTaken;
  protected Decimal? _TranPtdWhTax;
  protected Decimal? _TranPtdRGOL;
  protected Decimal? _TranYtdBalance;
  protected Decimal? _TranPtdDeposits;
  protected Decimal? _TranYtdDeposits;
  protected byte[] _tstamp;
  protected bool? _FinFlag = new bool?(true);

  [PXDBInt(IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [Vendor(IsKey = true, DisplayName = "Vendor ID")]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DetDeleted
  {
    get => this._DetDeleted;
    set => this._DetDeleted = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinBegBalance
  {
    get => this._FinBegBalance;
    set => this._FinBegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdPurchases
  {
    get => this._FinPtdPurchases;
    set => this._FinPtdPurchases = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdPayments
  {
    get => this._FinPtdPayments;
    set => this._FinPtdPayments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDrAdjustments
  {
    get => this._FinPtdDrAdjustments;
    set => this._FinPtdDrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCrAdjustments
  {
    get => this._FinPtdCrAdjustments;
    set => this._FinPtdCrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDiscTaken
  {
    get => this._FinPtdDiscTaken;
    set => this._FinPtdDiscTaken = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdWhTax
  {
    get => this._FinPtdWhTax;
    set => this._FinPtdWhTax = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRGOL
  {
    get => this._FinPtdRGOL;
    set => this._FinPtdRGOL = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdBalance
  {
    get => this._FinYtdBalance;
    set => this._FinYtdBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdDeposits
  {
    get => this._FinPtdDeposits;
    set => this._FinPtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdDeposits
  {
    get => this._FinYtdDeposits;
    set => this._FinYtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PTD Revalued Amount")]
  public virtual Decimal? FinPtdRevalued
  {
    get => this._FinPtdRevalued;
    set => this._FinPtdRevalued = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranBegBalance
  {
    get => this._TranBegBalance;
    set => this._TranBegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdPurchases
  {
    get => this._TranPtdPurchases;
    set => this._TranPtdPurchases = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdPayments
  {
    get => this._TranPtdPayments;
    set => this._TranPtdPayments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDrAdjustments
  {
    get => this._TranPtdDrAdjustments;
    set => this._TranPtdDrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdCrAdjustments
  {
    get => this._TranPtdCrAdjustments;
    set => this._TranPtdCrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDiscTaken
  {
    get => this._TranPtdDiscTaken;
    set => this._TranPtdDiscTaken = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdWhTax
  {
    get => this._TranPtdWhTax;
    set => this._TranPtdWhTax = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRGOL
  {
    get => this._TranPtdRGOL;
    set => this._TranPtdRGOL = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdBalance
  {
    get => this._TranYtdBalance;
    set => this._TranYtdBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdDeposits
  {
    get => this._TranPtdDeposits;
    set => this._TranPtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdDeposits
  {
    get => this._TranYtdDeposits;
    set => this._TranYtdDeposits = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXBool]
  public virtual bool? FinFlag
  {
    get => this._FinFlag;
    set => this._FinFlag = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdCrAdjustments
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdCrAdjustments), typeof (APHistory.tranPtdCrAdjustments)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdCrAdjustments : this._FinPtdCrAdjustments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdCrAdjustments = value;
      else
        this._TranPtdCrAdjustments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdDrAdjustments
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdDrAdjustments), typeof (APHistory.tranPtdDrAdjustments)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdDrAdjustments : this._FinPtdDrAdjustments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDrAdjustments = value;
      else
        this._TranPtdDrAdjustments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdPurchases
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdPurchases), typeof (APHistory.tranPtdPurchases)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdPurchases : this._FinPtdPurchases;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdPurchases = value;
      else
        this._TranPtdPurchases = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdPayments
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdPayments), typeof (APHistory.tranPtdPayments)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdPayments : this._FinPtdPayments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdPayments = value;
      else
        this._TranPtdPayments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdDiscTaken
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdDiscTaken), typeof (APHistory.tranPtdDiscTaken)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdDiscTaken : this._FinPtdDiscTaken;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDiscTaken = value;
      else
        this._TranPtdDiscTaken = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdWhTax
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdWhTax), typeof (APHistory.tranPtdWhTax)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdWhTax : this._FinPtdWhTax;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdWhTax = value;
      else
        this._TranPtdWhTax = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdRGOL
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdRGOL), typeof (APHistory.tranPtdRGOL)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdRGOL : this._FinPtdRGOL;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdRGOL = value;
      else
        this._TranPtdRGOL = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finYtdBalance), typeof (APHistory.tranYtdBalance)})] get
    {
      return !this._FinFlag.Value ? this._TranYtdBalance : this._FinYtdBalance;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinYtdBalance = value;
      else
        this._TranYtdBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? BegBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finBegBalance), typeof (APHistory.tranBegBalance)})] get
    {
      return !this._FinFlag.Value ? this._TranBegBalance : this._FinBegBalance;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinBegBalance = value;
      else
        this._TranBegBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdDeposits
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdDeposits), typeof (APHistory.tranPtdDeposits)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdDeposits : this._FinPtdDeposits;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDeposits = value;
      else
        this._TranPtdDeposits = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdDeposits
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finYtdDeposits), typeof (APHistory.tranYtdDeposits)})] get
    {
      return !this._FinFlag.Value ? this._TranYtdDeposits : this._FinYtdDeposits;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinYtdDeposits = value;
      else
        this._TranYtdDeposits = value;
    }
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PtdRetainageWithheld
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdRetainageWithheld), typeof (APHistory.tranPtdRetainageWithheld)})] get
    {
      return !this._FinFlag.Value ? this.TranPtdRetainageWithheld : this.FinPtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.Value)
        this.FinPtdRetainageWithheld = value;
      else
        this.TranPtdRetainageWithheld = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageWithheld
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finYtdRetainageWithheld), typeof (APHistory.tranYtdRetainageWithheld)})] get
    {
      return !this._FinFlag.Value ? this.TranYtdRetainageWithheld : this.FinYtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.Value)
        this.FinYtdRetainageWithheld = value;
      else
        this.TranYtdRetainageWithheld = value;
    }
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinYtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranYtdRetainageReleased { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PtdRetainageReleased
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finPtdRetainageReleased), typeof (APHistory.tranPtdRetainageReleased)})] get
    {
      return !this._FinFlag.Value ? this.TranPtdRetainageReleased : this.FinPtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.Value)
        this.FinPtdRetainageReleased = value;
      else
        this.TranPtdRetainageReleased = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageReleased
  {
    [PXDependsOnFields(new System.Type[] {typeof (APHistory.finFlag), typeof (APHistory.finYtdRetainageReleased), typeof (APHistory.tranYtdRetainageReleased)})] get
    {
      return !this._FinFlag.Value ? this.TranYtdRetainageReleased : this.FinYtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.Value)
        this.FinYtdRetainageReleased = value;
      else
        this.TranYtdRetainageReleased = value;
    }
  }

  public class PK : 
    PrimaryKeyOf<APHistory>.By<APHistory.branchID, APHistory.accountID, APHistory.subID, APHistory.vendorID, APHistory.finPeriodID>
  {
    public static APHistory Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? vendorID,
      string finPeriodID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APHistory>.By<APHistory.branchID, APHistory.accountID, APHistory.subID, APHistory.vendorID, APHistory.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) vendorID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APHistory>.By<APHistory.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APHistory>.By<APHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<APHistory>.By<APHistory.subID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APHistory>.By<APHistory.vendorID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APHistory.finPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistory.vendorID>
  {
  }

  public abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APHistory.detDeleted>
  {
  }

  public abstract class finBegBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.finBegBalance>
  {
  }

  public abstract class finPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdPurchases>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdPayments>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdDiscTaken>
  {
  }

  public abstract class finPtdWhTax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.finPtdWhTax>
  {
  }

  public abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.finPtdRGOL>
  {
  }

  public abstract class finYtdBalance : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.finYtdBalance>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdDeposits>
  {
  }

  public abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finYtdDeposits>
  {
  }

  public abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdRevalued>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranBegBalance>
  {
  }

  public abstract class tranPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdPurchases>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdPayments>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdDiscTaken>
  {
  }

  public abstract class tranPtdWhTax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.tranPtdWhTax>
  {
  }

  public abstract class tranPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APHistory.tranPtdRGOL>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranYtdBalance>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdDeposits>
  {
  }

  public abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranYtdDeposits>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APHistory.Tstamp>
  {
  }

  public abstract class finFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APHistory.finFlag>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdRetainageWithheld>
  {
  }

  public abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finYtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranYtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finPtdRetainageReleased>
  {
  }

  public abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.finYtdRetainageReleased>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranPtdRetainageReleased>
  {
  }

  public abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APHistory.tranYtdRetainageReleased>
  {
  }
}
