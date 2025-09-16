// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryYtd
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
public class ARHistoryYtd : ARHistory
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARHistoryYtd>.By<ARHistoryYtd.branchID, ARHistoryYtd.accountID, ARHistoryYtd.subID, ARHistoryYtd.customerID, ARHistoryYtd.finPeriodID>
  {
    public static ARHistoryYtd Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistoryYtd) PrimaryKeyOf<ARHistoryYtd>.By<ARHistoryYtd.branchID, ARHistoryYtd.accountID, ARHistoryYtd.subID, ARHistoryYtd.customerID, ARHistoryYtd.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtd.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtd.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtd.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryYtd.customerID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARHistoryYtd.finPeriodID>
  {
  }

  public new abstract class finPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdSales>
  {
  }

  public new abstract class finPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdDrAdjustments>
  {
  }

  public new abstract class finPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdFinCharges>
  {
  }

  public new abstract class finPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdPayments>
  {
  }

  public new abstract class finPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdCrAdjustments>
  {
  }

  public new abstract class finPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdDiscounts>
  {
  }

  public new abstract class finPtdRGOL : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARHistoryYtd.finPtdRGOL>
  {
  }

  public new abstract class finPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdDeposits>
  {
  }

  public new abstract class finPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdRetainageReleased>
  {
  }

  public new abstract class finPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.finPtdRetainageWithheld>
  {
  }

  public new abstract class tranPtdSales : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdSales>
  {
  }

  public new abstract class tranPtdDrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdDrAdjustments>
  {
  }

  public new abstract class tranPtdFinCharges : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdFinCharges>
  {
  }

  public new abstract class tranPtdPayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdPayments>
  {
  }

  public new abstract class tranPtdCrAdjustments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdCrAdjustments>
  {
  }

  public new abstract class tranPtdDiscounts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdDiscounts>
  {
  }

  public new abstract class tranPtdRGOL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdRGOL>
  {
  }

  public new abstract class tranPtdDeposits : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdDeposits>
  {
  }

  public new abstract class tranPtdRetainageReleased : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdRetainageReleased>
  {
  }

  public new abstract class tranPtdRetainageWithheld : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARHistoryYtd.tranPtdRetainageWithheld>
  {
  }
}
