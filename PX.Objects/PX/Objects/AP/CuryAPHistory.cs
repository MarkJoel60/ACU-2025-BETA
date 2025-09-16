// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.CuryAPHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// An accounts payable history record that accumulates a number
/// of important year-to-date and period-to-date amounts (such as sales, debit and credit
/// adjustments, gains and losses) in foreign currency. The history is accumulated separately
/// across the following dimensions: branch, GL account, GL subaccount, financial period,
/// vendor, and currency. History records are created and updated during the document
/// release process (see <see cref="T:PX.Objects.AP.APDocumentRelease" /> graph). Various helper projections
/// over this DAC are used in a number of AR inquiry forms and reports, such as Vendor
/// Summary (AP401000).
/// </summary>
[PXCacheName("Currency AP History")]
[Serializable]
public class CuryAPHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _VendorID;
  protected string _CuryID;
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
  protected Decimal? _CuryFinBegBalance;
  protected Decimal? _CuryFinPtdPurchases;
  protected Decimal? _CuryFinPtdPayments;
  protected Decimal? _CuryFinPtdDrAdjustments;
  protected Decimal? _CuryFinPtdCrAdjustments;
  protected Decimal? _CuryFinPtdDiscTaken;
  protected Decimal? _CuryFinPtdWhTax;
  protected Decimal? _CuryFinYtdBalance;
  protected Decimal? _CuryFinPtdDeposits;
  protected Decimal? _CuryFinYtdDeposits;
  protected Decimal? _CuryTranBegBalance;
  protected Decimal? _CuryTranPtdPurchases;
  protected Decimal? _CuryTranPtdPayments;
  protected Decimal? _CuryTranPtdDrAdjustments;
  protected Decimal? _CuryTranPtdCrAdjustments;
  protected Decimal? _CuryTranPtdDiscTaken;
  protected Decimal? _CuryTranPtdWhTax;
  protected Decimal? _CuryTranYtdBalance;
  protected Decimal? _CuryTranPtdDeposits;
  protected Decimal? _CuryTranYtdDeposits;
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

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDefault]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
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

  [PXDBDecimal(4)]
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

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinBegBalance
  {
    get => this._CuryFinBegBalance;
    set => this._CuryFinBegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdPurchases
  {
    get => this._CuryFinPtdPurchases;
    set => this._CuryFinPtdPurchases = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdPayments
  {
    get => this._CuryFinPtdPayments;
    set => this._CuryFinPtdPayments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdDrAdjustments
  {
    get => this._CuryFinPtdDrAdjustments;
    set => this._CuryFinPtdDrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdCrAdjustments
  {
    get => this._CuryFinPtdCrAdjustments;
    set => this._CuryFinPtdCrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdDiscTaken
  {
    get => this._CuryFinPtdDiscTaken;
    set => this._CuryFinPtdDiscTaken = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdWhTax
  {
    get => this._CuryFinPtdWhTax;
    set => this._CuryFinPtdWhTax = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinYtdBalance
  {
    get => this._CuryFinYtdBalance;
    set => this._CuryFinYtdBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdDeposits
  {
    get => this._CuryFinPtdDeposits;
    set => this._CuryFinPtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinYtdDeposits
  {
    get => this._CuryFinYtdDeposits;
    set => this._CuryFinYtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranBegBalance
  {
    get => this._CuryTranBegBalance;
    set => this._CuryTranBegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdPurchases
  {
    get => this._CuryTranPtdPurchases;
    set => this._CuryTranPtdPurchases = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdPayments
  {
    get => this._CuryTranPtdPayments;
    set => this._CuryTranPtdPayments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdDrAdjustments
  {
    get => this._CuryTranPtdDrAdjustments;
    set => this._CuryTranPtdDrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdCrAdjustments
  {
    get => this._CuryTranPtdCrAdjustments;
    set => this._CuryTranPtdCrAdjustments = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdDiscTaken
  {
    get => this._CuryTranPtdDiscTaken;
    set => this._CuryTranPtdDiscTaken = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdWhTax
  {
    get => this._CuryTranPtdWhTax;
    set => this._CuryTranPtdWhTax = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranYtdBalance
  {
    get => this._CuryTranYtdBalance;
    set => this._CuryTranYtdBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdDeposits
  {
    get => this._CuryTranPtdDeposits;
    set => this._CuryTranPtdDeposits = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranYtdDeposits
  {
    get => this._CuryTranYtdDeposits;
    set => this._CuryTranYtdDeposits = value;
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdCrAdjustments), typeof (CuryAPHistory.tranPtdCrAdjustments)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdDrAdjustments), typeof (CuryAPHistory.tranPtdDrAdjustments)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdPurchases), typeof (CuryAPHistory.tranPtdPurchases)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdPayments), typeof (CuryAPHistory.tranPtdPayments)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdDiscTaken), typeof (CuryAPHistory.tranPtdDiscTaken)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdWhTax), typeof (CuryAPHistory.tranPtdWhTax)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdRGOL), typeof (CuryAPHistory.tranPtdRGOL)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finYtdBalance), typeof (CuryAPHistory.tranYtdBalance)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finBegBalance), typeof (CuryAPHistory.tranBegBalance)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdDeposits), typeof (CuryAPHistory.tranPtdDeposits)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finYtdDeposits), typeof (CuryAPHistory.tranYtdDeposits)})] get
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

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdCrAdjustments
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdCrAdjustments), typeof (CuryAPHistory.curyTranPtdCrAdjustments)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdCrAdjustments : this._CuryFinPtdCrAdjustments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdCrAdjustments = value;
      else
        this._CuryTranPtdCrAdjustments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdDrAdjustments
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdDrAdjustments), typeof (CuryAPHistory.curyTranPtdDrAdjustments)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdDrAdjustments : this._CuryFinPtdDrAdjustments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDrAdjustments = value;
      else
        this._CuryTranPtdDrAdjustments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdPurchases
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdPurchases), typeof (CuryAPHistory.curyTranPtdPurchases)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdPurchases : this._CuryFinPtdPurchases;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdPurchases = value;
      else
        this._CuryTranPtdPurchases = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdPayments
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdPayments), typeof (CuryAPHistory.curyTranPtdPayments)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdPayments : this._CuryFinPtdPayments;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdPayments = value;
      else
        this._CuryTranPtdPayments = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdDiscTaken
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdDiscTaken), typeof (CuryAPHistory.curyTranPtdDiscTaken)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdDiscTaken : this._CuryFinPtdDiscTaken;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDiscTaken = value;
      else
        this._CuryTranPtdDiscTaken = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdWhTax
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdWhTax), typeof (CuryAPHistory.curyTranPtdWhTax)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdWhTax : this._CuryFinPtdWhTax;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdWhTax = value;
      else
        this._CuryTranPtdWhTax = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinYtdBalance), typeof (CuryAPHistory.curyTranYtdBalance)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranYtdBalance : this._CuryFinYtdBalance;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinYtdBalance = value;
      else
        this._CuryTranYtdBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryBegBalance
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinBegBalance), typeof (CuryAPHistory.curyTranBegBalance)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranBegBalance : this._CuryFinBegBalance;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinBegBalance = value;
      else
        this._CuryTranBegBalance = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdDeposits
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdDeposits), typeof (CuryAPHistory.curyTranPtdDeposits)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdDeposits : this._CuryFinPtdDeposits;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDeposits = value;
      else
        this._CuryTranPtdDeposits = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdDeposits
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinYtdDeposits), typeof (CuryAPHistory.curyTranYtdDeposits)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranYtdDeposits : this._CuryFinYtdDeposits;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinYtdDeposits = value;
      else
        this._CuryTranYtdDeposits = value;
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdRetainageWithheld), typeof (CuryAPHistory.tranPtdRetainageWithheld)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finYtdRetainageWithheld), typeof (CuryAPHistory.tranYtdRetainageWithheld)})] get
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
  public virtual Decimal? CuryFinPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinYtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdRetainageWithheld { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranYtdRetainageWithheld { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdRetainageWithheld
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdRetainageWithheld), typeof (CuryAPHistory.curyTranPtdRetainageWithheld)})] get
    {
      return !this._FinFlag.Value ? this.CuryTranPtdRetainageWithheld : this.CuryFinPtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.Value)
        this.CuryFinPtdRetainageWithheld = value;
      else
        this.CuryTranPtdRetainageWithheld = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdRetainageWithheld
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinYtdRetainageWithheld), typeof (CuryAPHistory.curyTranYtdRetainageWithheld)})] get
    {
      return !this._FinFlag.Value ? this.CuryTranYtdRetainageWithheld : this.CuryFinYtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.Value)
        this.CuryFinYtdRetainageWithheld = value;
      else
        this.CuryTranYtdRetainageWithheld = value;
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finPtdRetainageReleased), typeof (CuryAPHistory.tranPtdRetainageReleased)})] get
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
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.finYtdRetainageReleased), typeof (CuryAPHistory.tranYtdRetainageReleased)})] get
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

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinYtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdRetainageReleased { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranYtdRetainageReleased { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdRetainageReleased
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinPtdRetainageReleased), typeof (CuryAPHistory.curyTranPtdRetainageReleased)})] get
    {
      return !this._FinFlag.Value ? this.CuryTranPtdRetainageReleased : this.CuryFinPtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.Value)
        this.CuryFinPtdRetainageReleased = value;
      else
        this.CuryTranPtdRetainageReleased = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdRetainageReleased
  {
    [PXDependsOnFields(new System.Type[] {typeof (CuryAPHistory.finFlag), typeof (CuryAPHistory.curyFinYtdRetainageReleased), typeof (CuryAPHistory.curyTranYtdRetainageReleased)})] get
    {
      return !this._FinFlag.Value ? this.CuryTranYtdRetainageReleased : this.CuryFinYtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.Value)
        this.CuryFinYtdRetainageReleased = value;
      else
        this.CuryTranYtdRetainageReleased = value;
    }
  }

  public class PK : 
    PrimaryKeyOf<CuryAPHistory>.By<CuryAPHistory.branchID, CuryAPHistory.accountID, CuryAPHistory.subID, CuryAPHistory.curyID, CuryAPHistory.vendorID, CuryAPHistory.finPeriodID>
  {
    public static CuryAPHistory Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      string curyID,
      int? vendorID,
      string finPeriodID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<CuryAPHistory>.By<CuryAPHistory.branchID, CuryAPHistory.accountID, CuryAPHistory.subID, CuryAPHistory.curyID, CuryAPHistory.vendorID, CuryAPHistory.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) curyID, (object) vendorID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CuryAPHistory>.By<CuryAPHistory.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CuryAPHistory>.By<CuryAPHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<CuryAPHistory>.By<CuryAPHistory.subID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CuryAPHistory>.By<CuryAPHistory.curyID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<CuryAPHistory>.By<CuryAPHistory.vendorID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistory.finPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHistory.vendorID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHistory.curyID>
  {
  }

  public abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryAPHistory.detDeleted>
  {
  }

  public abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finBegBalance>
  {
  }

  public abstract class finPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdPurchases>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdPayments>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdDiscTaken>
  {
  }

  public abstract class finPtdWhTax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryAPHistory.finPtdWhTax>
  {
  }

  public abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryAPHistory.finPtdRGOL>
  {
  }

  public abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finYtdBalance>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdDeposits>
  {
  }

  public abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finYtdDeposits>
  {
  }

  public abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdRevalued>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranBegBalance>
  {
  }

  public abstract class tranPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdPurchases>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdPayments>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdDiscTaken>
  {
  }

  public abstract class tranPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdWhTax>
  {
  }

  public abstract class tranPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryAPHistory.tranPtdRGOL>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranYtdBalance>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdDeposits>
  {
  }

  public abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranYtdDeposits>
  {
  }

  public abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinBegBalance>
  {
  }

  public abstract class curyFinPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdPurchases>
  {
  }

  public abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdPayments>
  {
  }

  public abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdDrAdjustments>
  {
  }

  public abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdCrAdjustments>
  {
  }

  public abstract class curyFinPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdDiscTaken>
  {
  }

  public abstract class curyFinPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdWhTax>
  {
  }

  public abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinYtdBalance>
  {
  }

  public abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdDeposits>
  {
  }

  public abstract class curyFinYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinYtdDeposits>
  {
  }

  public abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranBegBalance>
  {
  }

  public abstract class curyTranPtdPurchases : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdPurchases>
  {
  }

  public abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdPayments>
  {
  }

  public abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdDrAdjustments>
  {
  }

  public abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdCrAdjustments>
  {
  }

  public abstract class curyTranPtdDiscTaken : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdDiscTaken>
  {
  }

  public abstract class curyTranPtdWhTax : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdWhTax>
  {
  }

  public abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranYtdBalance>
  {
  }

  public abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdDeposits>
  {
  }

  public abstract class curyTranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranYtdDeposits>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CuryAPHistory.Tstamp>
  {
  }

  public abstract class finFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryAPHistory.finFlag>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdRetainageWithheld>
  {
  }

  public abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finYtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranYtdRetainageWithheld>
  {
  }

  public abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdRetainageWithheld>
  {
  }

  public abstract class curyFinYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinYtdRetainageWithheld>
  {
  }

  public abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdRetainageWithheld>
  {
  }

  public abstract class curyTranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranYtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finPtdRetainageReleased>
  {
  }

  public abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.finYtdRetainageReleased>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranPtdRetainageReleased>
  {
  }

  public abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.tranYtdRetainageReleased>
  {
  }

  public abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinPtdRetainageReleased>
  {
  }

  public abstract class curyFinYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyFinYtdRetainageReleased>
  {
  }

  public abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranPtdRetainageReleased>
  {
  }

  public abstract class curyTranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryAPHistory.curyTranYtdRetainageReleased>
  {
  }
}
