// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CuryARHistory
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
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable history record, which accumulates a number
/// of important year-to-date and period-to-date amounts (such as sales, debit and credit
/// adjustments, and gains and losses) in foreign currency. The history is accumulated separately
/// across the following dimensions: branch, GL account, GL subaccount, financial period,
/// customer, and currency. History records are created and updated during the document
/// release process (see <see cref="T:PX.Objects.AR.ARDocumentRelease" /> graph). Various helper projections
/// over this DAC are used in a number of AR inquiry forms and reports, such as Customer
/// Summary (AR401000).
/// </summary>
[PXCacheName("Currency AR History")]
[Serializable]
public class CuryARHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _CustomerID;
  protected string _CuryID;
  protected bool? _DetDeleted;
  protected Decimal? _FinPtdDrAdjustments;
  protected Decimal? _FinPtdCrAdjustments;
  protected Decimal? _FinPtdSales;
  protected Decimal? _FinPtdPayments;
  protected Decimal? _FinPtdDiscounts;
  protected Decimal? _FinYtdBalance;
  protected Decimal? _FinBegBalance;
  protected Decimal? _FinPtdCOGS;
  protected Decimal? _FinPtdRGOL;
  protected Decimal? _FinPtdFinCharges;
  protected Decimal? _FinPtdRevalued;
  protected Decimal? _FinPtdDeposits;
  protected Decimal? _FinYtdDeposits;
  protected Decimal? _TranPtdDrAdjustments;
  protected Decimal? _TranPtdCrAdjustments;
  protected Decimal? _TranPtdSales;
  protected Decimal? _TranPtdPayments;
  protected Decimal? _TranPtdDiscounts;
  protected Decimal? _TranYtdBalance;
  protected Decimal? _TranBegBalance;
  protected Decimal? _TranPtdRGOL;
  protected Decimal? _TranPtdCOGS;
  protected Decimal? _TranPtdFinCharges;
  protected Decimal? _TranPtdDeposits;
  protected Decimal? _TranYtdDeposits;
  protected Decimal? _CuryFinPtdDrAdjustments;
  protected Decimal? _CuryFinPtdCrAdjustments;
  protected Decimal? _CuryFinPtdSales;
  protected Decimal? _CuryFinPtdPayments;
  protected Decimal? _CuryFinPtdDiscounts;
  protected Decimal? _CuryFinPtdFinCharges;
  protected Decimal? _CuryFinYtdBalance;
  protected Decimal? _CuryFinBegBalance;
  protected Decimal? _CuryFinPtdDeposits;
  protected Decimal? _CuryFinYtdDeposits;
  protected Decimal? _CuryTranPtdDrAdjustments;
  protected Decimal? _CuryTranPtdCrAdjustments;
  protected Decimal? _CuryTranPtdSales;
  protected Decimal? _CuryTranPtdPayments;
  protected Decimal? _CuryTranPtdDiscounts;
  protected Decimal? _CuryTranPtdFinCharges;
  protected Decimal? _CuryTranYtdBalance;
  protected Decimal? _CuryTranBegBalance;
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
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency ID")]
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
  public virtual Decimal? FinPtdSales
  {
    get => this._FinPtdSales;
    set => this._FinPtdSales = value;
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
  public virtual Decimal? FinPtdDiscounts
  {
    get => this._FinPtdDiscounts;
    set => this._FinPtdDiscounts = value;
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
  public virtual Decimal? FinBegBalance
  {
    get => this._FinBegBalance;
    set => this._FinBegBalance = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinPtdCOGS
  {
    get => this._FinPtdCOGS;
    set => this._FinPtdCOGS = value;
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
  public virtual Decimal? FinPtdFinCharges
  {
    get => this._FinPtdFinCharges;
    set => this._FinPtdFinCharges = value;
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
  public virtual Decimal? TranPtdSales
  {
    get => this._TranPtdSales;
    set => this._TranPtdSales = value;
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
  public virtual Decimal? TranPtdDiscounts
  {
    get => this._TranPtdDiscounts;
    set => this._TranPtdDiscounts = value;
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
  public virtual Decimal? TranBegBalance
  {
    get => this._TranBegBalance;
    set => this._TranBegBalance = value;
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
  public virtual Decimal? TranPtdCOGS
  {
    get => this._TranPtdCOGS;
    set => this._TranPtdCOGS = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranPtdFinCharges
  {
    get => this._TranPtdFinCharges;
    set => this._TranPtdFinCharges = value;
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
  public virtual Decimal? CuryFinPtdSales
  {
    get => this._CuryFinPtdSales;
    set => this._CuryFinPtdSales = value;
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
  public virtual Decimal? CuryFinPtdDiscounts
  {
    get => this._CuryFinPtdDiscounts;
    set => this._CuryFinPtdDiscounts = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryFinPtdFinCharges
  {
    get => this._CuryFinPtdFinCharges;
    set => this._CuryFinPtdFinCharges = value;
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
  public virtual Decimal? CuryFinBegBalance
  {
    get => this._CuryFinBegBalance;
    set => this._CuryFinBegBalance = value;
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
  public virtual Decimal? CuryTranPtdSales
  {
    get => this._CuryTranPtdSales;
    set => this._CuryTranPtdSales = value;
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
  public virtual Decimal? CuryTranPtdDiscounts
  {
    get => this._CuryTranPtdDiscounts;
    set => this._CuryTranPtdDiscounts = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranPtdFinCharges
  {
    get => this._CuryTranPtdFinCharges;
    set => this._CuryTranPtdFinCharges = value;
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
  public virtual Decimal? CuryTranBegBalance
  {
    get => this._CuryTranBegBalance;
    set => this._CuryTranBegBalance = value;
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdCrAdjustments), typeof (CuryARHistory.tranPtdCrAdjustments)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdDrAdjustments), typeof (CuryARHistory.tranPtdDrAdjustments)})] get
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
  public virtual Decimal? PtdSales
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdSales), typeof (CuryARHistory.tranPtdSales)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdSales : this._FinPtdSales;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdSales = value;
      else
        this._TranPtdSales = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdPayments
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdPayments), typeof (CuryARHistory.tranPtdPayments)})] get
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
  public virtual Decimal? PtdDiscounts
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdDiscounts), typeof (CuryARHistory.tranPtdDiscounts)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdDiscounts : this._FinPtdDiscounts;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDiscounts = value;
      else
        this._TranPtdDiscounts = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdBalance
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finYtdBalance), typeof (CuryARHistory.tranYtdBalance)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finBegBalance), typeof (CuryARHistory.tranBegBalance)})] get
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
  public virtual Decimal? PtdCOGS
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdCOGS), typeof (CuryARHistory.tranPtdCOGS)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdCOGS : this._FinPtdCOGS;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdCOGS = value;
      else
        this._TranPtdCOGS = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdRGOL
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdRGOL), typeof (CuryARHistory.tranPtdRGOL)})] get
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
  public virtual Decimal? PtdFinCharges
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdFinCharges), typeof (CuryARHistory.tranPtdFinCharges)})] get
    {
      return !this._FinFlag.Value ? this._TranPtdFinCharges : this._FinPtdFinCharges;
    }
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdFinCharges = value;
      else
        this._TranPtdFinCharges = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? PtdDeposits
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdDeposits), typeof (CuryARHistory.tranPtdDeposits)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finYtdDeposits), typeof (CuryARHistory.tranYtdDeposits)})] get
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
  public virtual Decimal? PtdItemDiscounts { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdCrAdjustments
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdCrAdjustments), typeof (CuryARHistory.curyTranPtdCrAdjustments)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdDrAdjustments), typeof (CuryARHistory.curyTranPtdDrAdjustments)})] get
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
  public virtual Decimal? CuryPtdSales
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdSales), typeof (CuryARHistory.curyTranPtdSales)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdSales : this._CuryFinPtdSales;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdSales = value;
      else
        this._CuryTranPtdSales = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdPayments
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdPayments), typeof (CuryARHistory.curyTranPtdPayments)})] get
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
  public virtual Decimal? CuryPtdDiscounts
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdDiscounts), typeof (CuryARHistory.curyTranPtdDiscounts)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdDiscounts : this._CuryFinPtdDiscounts;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDiscounts = value;
      else
        this._CuryTranPtdDiscounts = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryPtdFinCharges
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdFinCharges), typeof (CuryARHistory.curyTranPtdFinCharges)})] get
    {
      return !this._FinFlag.Value ? this._CuryTranPtdFinCharges : this._CuryFinPtdFinCharges;
    }
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdFinCharges = value;
      else
        this._CuryTranPtdFinCharges = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdBalance
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinYtdBalance), typeof (CuryARHistory.curyTranYtdBalance)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinBegBalance), typeof (CuryARHistory.curyTranBegBalance)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdDeposits), typeof (CuryARHistory.curyTranPtdDeposits)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinYtdDeposits), typeof (CuryARHistory.curyTranYtdDeposits)})] get
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdRetainageWithheld), typeof (CuryARHistory.tranPtdRetainageWithheld)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.TranPtdRetainageWithheld : this.FinPtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
        this.FinPtdRetainageWithheld = value;
      else
        this.TranPtdRetainageWithheld = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageWithheld
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finYtdRetainageWithheld), typeof (CuryARHistory.tranYtdRetainageWithheld)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.TranYtdRetainageWithheld : this.FinYtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdRetainageWithheld), typeof (CuryARHistory.curyTranPtdRetainageWithheld)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.CuryTranPtdRetainageWithheld : this.CuryFinPtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
        this.CuryFinPtdRetainageWithheld = value;
      else
        this.CuryTranPtdRetainageWithheld = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdRetainageWithheld
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinYtdRetainageWithheld), typeof (CuryARHistory.curyTranYtdRetainageWithheld)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.CuryTranYtdRetainageWithheld : this.CuryFinYtdRetainageWithheld;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finPtdRetainageReleased), typeof (CuryARHistory.tranPtdRetainageReleased)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.TranPtdRetainageReleased : this.FinPtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
        this.FinPtdRetainageReleased = value;
      else
        this.TranPtdRetainageReleased = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? YtdRetainageReleased
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.finYtdRetainageReleased), typeof (CuryARHistory.tranYtdRetainageReleased)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.TranYtdRetainageReleased : this.FinYtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
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
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinPtdRetainageReleased), typeof (CuryARHistory.curyTranPtdRetainageReleased)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.CuryTranPtdRetainageReleased : this.CuryFinPtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
        this.CuryFinPtdRetainageReleased = value;
      else
        this.CuryTranPtdRetainageReleased = value;
    }
  }

  [PXDecimal(4)]
  public virtual Decimal? CuryYtdRetainageReleased
  {
    [PXDependsOnFields(new Type[] {typeof (CuryARHistory.finFlag), typeof (CuryARHistory.curyFinYtdRetainageReleased), typeof (CuryARHistory.curyTranYtdRetainageReleased)})] get
    {
      return !this._FinFlag.GetValueOrDefault() ? this.CuryTranYtdRetainageReleased : this.CuryFinYtdRetainageReleased;
    }
    set
    {
      if (this._FinFlag.GetValueOrDefault())
        this.CuryFinYtdRetainageReleased = value;
      else
        this.CuryTranYtdRetainageReleased = value;
    }
  }

  public class PK : 
    PrimaryKeyOf<CuryARHistory>.By<CuryARHistory.branchID, CuryARHistory.accountID, CuryARHistory.subID, CuryARHistory.curyID, CuryARHistory.customerID, CuryARHistory.finPeriodID>
  {
    public static CuryARHistory Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      string curyID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CuryARHistory) PrimaryKeyOf<CuryARHistory>.By<CuryARHistory.branchID, CuryARHistory.accountID, CuryARHistory.subID, CuryARHistory.curyID, CuryARHistory.customerID, CuryARHistory.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) curyID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CuryARHistory>.By<CuryARHistory.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CuryARHistory>.By<CuryARHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<CuryARHistory>.By<CuryARHistory.subID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CuryARHistory>.By<CuryARHistory.curyID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<CuryARHistory>.By<CuryARHistory.customerID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistory.finPeriodID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHistory.customerID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHistory.curyID>
  {
  }

  public abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryARHistory.detDeleted>
  {
  }

  public abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdDrAdjustments>
  {
  }

  public abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdCrAdjustments>
  {
  }

  public abstract class finPtdSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryARHistory.finPtdSales>
  {
  }

  public abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdPayments>
  {
  }

  public abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdDiscounts>
  {
  }

  public abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finYtdBalance>
  {
  }

  public abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finBegBalance>
  {
  }

  public abstract class finPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryARHistory.finPtdCOGS>
  {
  }

  public abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryARHistory.finPtdRGOL>
  {
  }

  public abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdFinCharges>
  {
  }

  public abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdRevalued>
  {
  }

  public abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdDeposits>
  {
  }

  public abstract class finYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finYtdDeposits>
  {
  }

  public abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdDrAdjustments>
  {
  }

  public abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdCrAdjustments>
  {
  }

  public abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdSales>
  {
  }

  public abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdPayments>
  {
  }

  public abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdDiscounts>
  {
  }

  public abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranYtdBalance>
  {
  }

  public abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranBegBalance>
  {
  }

  public abstract class tranPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryARHistory.tranPtdRGOL>
  {
  }

  public abstract class tranPtdCOGS : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CuryARHistory.tranPtdCOGS>
  {
  }

  public abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdFinCharges>
  {
  }

  public abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdDeposits>
  {
  }

  public abstract class tranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranYtdDeposits>
  {
  }

  public abstract class curyFinPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdDrAdjustments>
  {
  }

  public abstract class curyFinPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdCrAdjustments>
  {
  }

  public abstract class curyFinPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdSales>
  {
  }

  public abstract class curyFinPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdPayments>
  {
  }

  public abstract class curyFinPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdDiscounts>
  {
  }

  public abstract class curyFinPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdFinCharges>
  {
  }

  public abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinYtdBalance>
  {
  }

  public abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinBegBalance>
  {
  }

  public abstract class curyFinPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdDeposits>
  {
  }

  public abstract class curyFinYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinYtdDeposits>
  {
  }

  public abstract class curyTranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdDrAdjustments>
  {
  }

  public abstract class curyTranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdCrAdjustments>
  {
  }

  public abstract class curyTranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdSales>
  {
  }

  public abstract class curyTranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdPayments>
  {
  }

  public abstract class curyTranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdDiscounts>
  {
  }

  public abstract class curyTranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdFinCharges>
  {
  }

  public abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranYtdBalance>
  {
  }

  public abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranBegBalance>
  {
  }

  public abstract class curyTranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdDeposits>
  {
  }

  public abstract class curyTranYtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranYtdDeposits>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CuryARHistory.Tstamp>
  {
  }

  public abstract class finFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CuryARHistory.finFlag>
  {
  }

  public abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdRetainageWithheld>
  {
  }

  public abstract class finYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finYtdRetainageWithheld>
  {
  }

  public abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdRetainageWithheld>
  {
  }

  public abstract class tranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranYtdRetainageWithheld>
  {
  }

  public abstract class curyFinPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdRetainageWithheld>
  {
  }

  public abstract class curyFinYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinYtdRetainageWithheld>
  {
  }

  public abstract class curyTranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdRetainageWithheld>
  {
  }

  public abstract class curyTranYtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranYtdRetainageWithheld>
  {
  }

  public abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finPtdRetainageReleased>
  {
  }

  public abstract class finYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.finYtdRetainageReleased>
  {
  }

  public abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranPtdRetainageReleased>
  {
  }

  public abstract class tranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.tranYtdRetainageReleased>
  {
  }

  public abstract class curyFinPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinPtdRetainageReleased>
  {
  }

  public abstract class curyFinYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyFinYtdRetainageReleased>
  {
  }

  public abstract class curyTranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranPtdRetainageReleased>
  {
  }

  public abstract class curyTranYtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CuryARHistory.curyTranYtdRetainageReleased>
  {
  }
}
