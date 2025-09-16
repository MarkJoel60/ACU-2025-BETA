// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.CuryARHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

[PXAccumulator(new Type[] {typeof (CuryARHistory.finYtdBalance), typeof (CuryARHistory.tranYtdBalance), typeof (CuryARHistory.curyFinYtdBalance), typeof (CuryARHistory.curyTranYtdBalance), typeof (CuryARHistory.finYtdBalance), typeof (CuryARHistory.tranYtdBalance), typeof (CuryARHistory.curyFinYtdBalance), typeof (CuryARHistory.curyTranYtdBalance), typeof (CuryARHistory.finYtdDeposits), typeof (CuryARHistory.tranYtdDeposits), typeof (CuryARHistory.curyFinYtdDeposits), typeof (CuryARHistory.curyTranYtdDeposits), typeof (CuryARHistory.finYtdRetainageReleased), typeof (CuryARHistory.tranYtdRetainageReleased), typeof (CuryARHistory.finYtdRetainageWithheld), typeof (CuryARHistory.tranYtdRetainageWithheld), typeof (CuryARHistory.curyFinYtdRetainageReleased), typeof (CuryARHistory.curyTranYtdRetainageReleased), typeof (CuryARHistory.curyFinYtdRetainageWithheld), typeof (CuryARHistory.curyTranYtdRetainageWithheld)}, new Type[] {typeof (CuryARHistory.finBegBalance), typeof (CuryARHistory.tranBegBalance), typeof (CuryARHistory.curyFinBegBalance), typeof (CuryARHistory.curyTranBegBalance), typeof (CuryARHistory.finYtdBalance), typeof (CuryARHistory.tranYtdBalance), typeof (CuryARHistory.curyFinYtdBalance), typeof (CuryARHistory.curyTranYtdBalance), typeof (CuryARHistory.finYtdDeposits), typeof (CuryARHistory.tranYtdDeposits), typeof (CuryARHistory.curyFinYtdDeposits), typeof (CuryARHistory.curyTranYtdDeposits), typeof (CuryARHistory.finYtdRetainageReleased), typeof (CuryARHistory.tranYtdRetainageReleased), typeof (CuryARHistory.finYtdRetainageWithheld), typeof (CuryARHistory.tranYtdRetainageWithheld), typeof (CuryARHistory.curyFinYtdRetainageReleased), typeof (CuryARHistory.curyTranYtdRetainageReleased), typeof (CuryARHistory.curyFinYtdRetainageWithheld), typeof (CuryARHistory.curyTranYtdRetainageWithheld)})]
[PXHidden]
[Serializable]
public class CuryARHist : CuryARHistory, ICuryARHist, IBaseARHist
{
  [PXDBInt(IsKey = true)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(5, IsUnicode = true, IsKey = true, InputMask = ">LLLLL")]
  [PXDefault]
  public override 
  #nullable disable
  string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHist.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHist.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryARHist.customerID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHist.curyID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryARHist.finPeriodID>
  {
  }
}
