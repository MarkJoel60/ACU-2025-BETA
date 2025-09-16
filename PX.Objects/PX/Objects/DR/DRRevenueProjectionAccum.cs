// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRRevenueProjectionAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.DR;

[DRRevenueAccum]
[PXHidden]
[Serializable]
public class DRRevenueProjectionAccum : DRRevenueProjection
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjectionAccum.branchID>
  {
  }

  public new abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjectionAccum.acctID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRRevenueProjectionAccum.subID>
  {
  }

  public new abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRevenueProjectionAccum.componentID>
  {
  }

  public new abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRevenueProjectionAccum.customerID>
  {
  }

  public new abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DRRevenueProjectionAccum.projectID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DRRevenueProjectionAccum.finPeriodID>
  {
  }

  public new abstract class pTDProjected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjectionAccum.pTDProjected>
  {
  }

  public new abstract class pTDRecognized : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjectionAccum.pTDRecognized>
  {
  }

  public new abstract class pTDRecognizedSamePeriod : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRRevenueProjectionAccum.pTDRecognizedSamePeriod>
  {
  }
}
