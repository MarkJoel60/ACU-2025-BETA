// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Overrides.APDocumentRelease.APHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP.Overrides.APDocumentRelease;

[PXAccumulator(new System.Type[] {typeof (APHistory.finYtdBalance), typeof (APHistory.tranYtdBalance), typeof (APHistory.finYtdBalance), typeof (APHistory.tranYtdBalance), typeof (APHistory.finYtdDeposits), typeof (APHistory.tranYtdDeposits), typeof (APHistory.finYtdRetainageReleased), typeof (APHistory.tranYtdRetainageReleased), typeof (APHistory.finYtdRetainageWithheld), typeof (APHistory.tranYtdRetainageWithheld)}, new System.Type[] {typeof (APHistory.finBegBalance), typeof (APHistory.tranBegBalance), typeof (APHistory.finYtdBalance), typeof (APHistory.tranYtdBalance), typeof (APHistory.finYtdDeposits), typeof (APHistory.tranYtdDeposits), typeof (APHistory.finYtdRetainageReleased), typeof (APHistory.tranYtdRetainageReleased), typeof (APHistory.finYtdRetainageWithheld), typeof (APHistory.tranYtdRetainageWithheld)})]
[PXHidden]
[Serializable]
public class APHist : APHistory, IBaseAPHist
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

  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHist.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHist.subID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHist.vendorID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APHist.finPeriodID>
  {
  }
}
