// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.GLTranFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.EP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class GLTranFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _BranchID;
  protected int? _EmployeeID;
  protected 
  #nullable disable
  string _Department;
  protected Decimal? _AcquisitionCost;
  protected Decimal? _CurrentCost;
  protected Decimal? _AccrualBalance;
  protected Decimal? _UnreconciledAmt;
  protected Decimal? _SelectionAmt;
  protected Decimal? _ExpectedCost;
  protected Decimal? _ExpectedAccrualBal;
  protected string _ReconType;
  protected DateTime? _TranDate;
  protected string _PeriodID;

  [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And2<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>, And<Where<PX.Objects.GL.Account.curyID, IsNull, Or<PX.Objects.GL.Account.curyID, Equal<Current<Company.baseCuryID>>>>>>>>>))]
  [PXDefault(typeof (FASetup.fAAccrualAcctID))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccountAny]
  [PXDefault(typeof (FASetup.fAAccrualSubID))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Branch(null, null, true, true, false, IsDetail = false)]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.CR.Location.vBranchID, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<EPEmployee.defLocationID, Equal<PX.Objects.CR.Location.locationID>>>>, Where<EPEmployee.bAccountID, Equal<Current<GLTranFilter.employeeID>>>>, Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXUIField(DisplayName = "Base Currency ID")]
  [PXDefault(typeof (AccessInfo.baseCuryID))]
  public virtual string BranchBaseCuryID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Custodian")]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<GLTranFilter.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField(DisplayName = "Department")]
  public virtual string Department
  {
    get => this._Department;
    set => this._Department = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Acquisition Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<FADetails.acquisitionCost, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  public Decimal? AcquisitionCost
  {
    get => this._AcquisitionCost;
    set => this._AcquisitionCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<FABookHistory.ytdAcquired, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FABook.updateGL, Desc<FABookHistory.finPeriodID>>>>))]
  public Decimal? CurrentCost
  {
    get => this._CurrentCost;
    set => this._CurrentCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Accrual Balance", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<FABookHistory.ytdReconciled, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FABookHistory.finPeriodID>>>))]
  public Decimal? AccrualBalance
  {
    get => this._AccrualBalance;
    set => this._AccrualBalance = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Unreconciled Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? UnreconciledAmt
  {
    get => this._UnreconciledAmt;
    set => this._UnreconciledAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Selection Total", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? SelectionAmt
  {
    get => this._SelectionAmt;
    set => this._SelectionAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Expected Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<FABookHistory.ytdAcquired, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, OrderBy<Desc<FABookHistory.finPeriodID>>>))]
  public Decimal? ExpectedCost
  {
    get => this._ExpectedCost;
    set => this._ExpectedCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Expected Accrual Balance", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search2<FABookHistory.ytdReconciled, LeftJoin<FABook, On<FABook.bookID, Equal<FABookHistory.bookID>>>, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABook.updateGL, Equal<True>>>, OrderBy<Desc<FABookHistory.finPeriodID>>>))]
  public Decimal? ExpectedAccrualBal
  {
    get => this._ExpectedAccrualBal;
    set => this._ExpectedAccrualBal = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("+")]
  [PXUIField(DisplayName = "Reconciliation Type")]
  [GLTranFilter.reconType.List]
  public virtual string ReconType
  {
    get => this._ReconType;
    set => this._ReconType = value;
  }

  [PXDBDate]
  [PXDefault(typeof (Search<FADetails.depreciateFromDate, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>))]
  [PXUIField(DisplayName = "Tran. Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXUIField(DisplayName = "Addition Period")]
  [FinPeriodSelector(null, typeof (GLTranFilter.tranDate), typeof (FixedAsset.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
  public virtual string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Show Transactions Marked as Reconciled")]
  [PXDefault(false)]
  public bool? ShowReconciled { get; set; }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranFilter.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranFilter.subID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranFilter.branchID>
  {
  }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLTranFilter.branchBaseCuryID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLTranFilter.employeeID>
  {
  }

  public abstract class department : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranFilter.department>
  {
  }

  public abstract class acquisitionCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranFilter.acquisitionCost>
  {
  }

  public abstract class currentCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranFilter.currentCost>
  {
  }

  public abstract class accrualBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranFilter.accrualBalance>
  {
  }

  public abstract class unreconciledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranFilter.unreconciledAmt>
  {
  }

  public abstract class selectionAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranFilter.selectionAmt>
  {
  }

  public abstract class expectedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLTranFilter.expectedCost>
  {
  }

  public abstract class expectedAccrualBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLTranFilter.expectedAccrualBal>
  {
  }

  public abstract class reconType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranFilter.reconType>
  {
    public const string Addition = "+";
    public const string Deduction = "-";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "+", "-" }, new string[2]
        {
          "Addition",
          "Deduction"
        })
      {
      }
    }

    public class addition : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTranFilter.reconType.addition>
    {
      public addition()
        : base("+")
      {
      }
    }

    public class deduction : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    GLTranFilter.reconType.deduction>
    {
      public deduction()
        : base("-")
      {
      }
    }
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  GLTranFilter.tranDate>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLTranFilter.periodID>
  {
  }

  public abstract class showReconciled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GLTranFilter.showReconciled>
  {
  }
}
