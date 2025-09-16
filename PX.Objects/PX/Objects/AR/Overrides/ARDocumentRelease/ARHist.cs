// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Overrides.ARDocumentRelease.ARHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Overrides.ARDocumentRelease;

[PXAccumulator(new Type[] {typeof (ARHistory.finYtdBalance), typeof (ARHistory.tranYtdBalance), typeof (ARHistory.finYtdBalance), typeof (ARHistory.tranYtdBalance), typeof (ARHistory.finYtdDeposits), typeof (ARHistory.tranYtdDeposits), typeof (ARHistory.finYtdRetainageReleased), typeof (ARHistory.tranYtdRetainageReleased), typeof (ARHistory.finYtdRetainageWithheld), typeof (ARHistory.tranYtdRetainageWithheld)}, new Type[] {typeof (ARHistory.finBegBalance), typeof (ARHistory.tranBegBalance), typeof (ARHistory.finYtdBalance), typeof (ARHistory.tranYtdBalance), typeof (ARHistory.finYtdDeposits), typeof (ARHistory.tranYtdDeposits), typeof (ARHistory.finYtdRetainageReleased), typeof (ARHistory.tranYtdRetainageReleased), typeof (ARHistory.finYtdRetainageWithheld), typeof (ARHistory.tranYtdRetainageWithheld)})]
[PXHidden]
[Serializable]
public class ARHist : ARHistory, IBaseARHist
{
  [PXDBInt(IsKey = true)]
  public override int? BranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? AccountID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? SubID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CustomerID { get; set; }

  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string FinPeriodID { get; set; }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHist.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHist.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHist.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHist.customerID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHist.finPeriodID>
  {
  }
}
