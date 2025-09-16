// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRevenueBalance2
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

public class DRRevenueBalance2 : DRRevenueBalance
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    DRRevenueBalance2>.By<DRRevenueBalance2.branchID, DRRevenueBalance2.acctID, DRRevenueBalance2.subID, DRRevenueBalance2.componentID, DRRevenueBalance2.customerID, DRRevenueBalance2.projectID, DRRevenueBalance2.finPeriodID>
  {
    public static DRRevenueBalance2 Find(
      PXGraph graph,
      int? branchID,
      int? acctID,
      int? subID,
      int? componentID,
      int? customerID,
      int? projectID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (DRRevenueBalance2) PrimaryKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.branchID, DRRevenueBalance2.acctID, DRRevenueBalance2.subID, DRRevenueBalance2.componentID, DRRevenueBalance2.customerID, DRRevenueBalance2.projectID, DRRevenueBalance2.finPeriodID>.FindBy(graph, (object) branchID, (object) acctID, (object) subID, (object) componentID, (object) customerID, (object) projectID, (object) finPeriodID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.subID>
    {
    }

    public class Component : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.componentID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.customerID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<DRRevenueBalance2>.By<DRRevenueBalance2.projectID>
    {
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.branchID>
  {
  }

  public new abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.acctID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.subID>
  {
  }

  public new abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.componentID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.customerID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueBalance2.projectID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRRevenueBalance2.finPeriodID>
  {
  }

  public new abstract class begBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.begBalance>
  {
  }

  public new abstract class begProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.begProjected>
  {
  }

  public new abstract class pTDDeferred : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.pTDDeferred>
  {
  }

  public new abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.pTDRecognized>
  {
  }

  public new abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.pTDRecognizedSamePeriod>
  {
  }

  public new abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.pTDProjected>
  {
  }

  public new abstract class endBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.endBalance>
  {
  }

  public new abstract class endProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.endProjected>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranBegBalance>
  {
  }

  public new abstract class tranBegProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranBegProjected>
  {
  }

  public new abstract class tranPTDDeferred : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranPTDDeferred>
  {
  }

  public new abstract class tranPTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranPTDRecognized>
  {
  }

  public new abstract class tranPTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranPTDRecognizedSamePeriod>
  {
  }

  public new abstract class tranPTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranPTDProjected>
  {
  }

  public new abstract class tranEndBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranEndBalance>
  {
  }

  public new abstract class tranEndProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueBalance2.tranEndProjected>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }
}
