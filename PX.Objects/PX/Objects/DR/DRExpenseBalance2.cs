// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRExpenseBalance2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.DR;

public class DRExpenseBalance2 : DRExpenseBalance
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    DRExpenseBalance2>.By<DRExpenseBalance2.branchID, DRExpenseBalance2.acctID, DRExpenseBalance2.subID, DRExpenseBalance2.componentID, DRExpenseBalance2.vendorID, DRExpenseBalance2.projectID, DRExpenseBalance2.finPeriodID>
  {
    public static DRExpenseBalance2 Find(
      PXGraph graph,
      int? branchID,
      int? acctID,
      int? subID,
      int? componentID,
      int? vendorID,
      int? projectID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (DRExpenseBalance2) PrimaryKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.branchID, DRExpenseBalance2.acctID, DRExpenseBalance2.subID, DRExpenseBalance2.componentID, DRExpenseBalance2.vendorID, DRExpenseBalance2.projectID, DRExpenseBalance2.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) vendorID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.componentID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.vendorID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRExpenseBalance2>.By<DRExpenseBalance2.projectID>
    {
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.branchID>
  {
  }

  public new abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.acctID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.subID>
  {
  }

  public new abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.componentID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.vendorID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRExpenseBalance2.projectID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRExpenseBalance2.finPeriodID>
  {
  }

  public new abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.begBalance>
  {
  }

  public new abstract class begProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.begProjected>
  {
  }

  public new abstract class pTDDeferred : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.pTDDeferred>
  {
  }

  public new abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.pTDRecognized>
  {
  }

  public new abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.pTDRecognizedSamePeriod>
  {
  }

  public new abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.pTDProjected>
  {
  }

  public new abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.endBalance>
  {
  }

  public new abstract class endProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.endProjected>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranBegBalance>
  {
  }

  public new abstract class tranBegProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranBegProjected>
  {
  }

  public new abstract class tranPTDDeferred : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranPTDDeferred>
  {
  }

  public new abstract class tranPTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranPTDRecognized>
  {
  }

  public new abstract class tranPTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranPTDRecognizedSamePeriod>
  {
  }

  public new abstract class tranPTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranPTDProjected>
  {
  }

  public new abstract class tranEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranEndBalance>
  {
  }

  public new abstract class tranEndProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRExpenseBalance2.tranEndProjected>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }
}
