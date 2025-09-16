// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.CuryAPHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

[PXAccumulator(new System.Type[] {typeof (CuryAPHistory.finYtdBalance), typeof (CuryAPHistory.tranYtdBalance), typeof (CuryAPHistory.curyFinYtdBalance), typeof (CuryAPHistory.curyTranYtdBalance), typeof (CuryAPHistory.finYtdBalance), typeof (CuryAPHistory.tranYtdBalance), typeof (CuryAPHistory.curyFinYtdBalance), typeof (CuryAPHistory.curyTranYtdBalance), typeof (CuryAPHistory.finYtdDeposits), typeof (CuryAPHistory.tranYtdDeposits), typeof (CuryAPHistory.curyFinYtdDeposits), typeof (CuryAPHistory.curyTranYtdDeposits), typeof (CuryAPHistory.finYtdRetainageReleased), typeof (CuryAPHistory.tranYtdRetainageReleased), typeof (CuryAPHistory.finYtdRetainageWithheld), typeof (CuryAPHistory.tranYtdRetainageWithheld), typeof (CuryAPHistory.curyFinYtdRetainageReleased), typeof (CuryAPHistory.curyTranYtdRetainageReleased), typeof (CuryAPHistory.curyFinYtdRetainageWithheld), typeof (CuryAPHistory.curyTranYtdRetainageWithheld)}, new System.Type[] {typeof (CuryAPHistory.finBegBalance), typeof (CuryAPHistory.tranBegBalance), typeof (CuryAPHistory.curyFinBegBalance), typeof (CuryAPHistory.curyTranBegBalance), typeof (CuryAPHistory.finYtdBalance), typeof (CuryAPHistory.tranYtdBalance), typeof (CuryAPHistory.curyFinYtdBalance), typeof (CuryAPHistory.curyTranYtdBalance), typeof (CuryAPHistory.finYtdDeposits), typeof (CuryAPHistory.tranYtdDeposits), typeof (CuryAPHistory.curyFinYtdDeposits), typeof (CuryAPHistory.curyTranYtdDeposits), typeof (CuryAPHistory.finYtdRetainageReleased), typeof (CuryAPHistory.tranYtdRetainageReleased), typeof (CuryAPHistory.finYtdRetainageWithheld), typeof (CuryAPHistory.tranYtdRetainageWithheld), typeof (CuryAPHistory.curyFinYtdRetainageReleased), typeof (CuryAPHistory.curyTranYtdRetainageReleased), typeof (CuryAPHistory.curyFinYtdRetainageWithheld), typeof (CuryAPHistory.curyTranYtdRetainageWithheld)})]
[PXHidden]
[Serializable]
public class CuryAPHist : CuryAPHistory, ICuryAPHist, IBaseAPHist
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
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
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
  CuryAPHist.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHist.subID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CuryAPHist.vendorID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHist.curyID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CuryAPHist.finPeriodID>
  {
  }
}
