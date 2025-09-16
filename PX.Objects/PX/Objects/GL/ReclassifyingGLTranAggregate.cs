// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReclassifyingGLTranAggregate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXProjection(typeof (Select4<GLTran, Where2<Where<GLTran.tranDate, GreaterEqual<CurrentValue<AccountByPeriodFilter.startDate>>, Or<CurrentValue<AccountByPeriodFilter.startDate>, IsNull>>, And2<Where<CurrentValue<AccountByPeriodFilter.endDate>, IsNull, Or<GLTran.tranDate, Less<CurrentValue<AccountByPeriodFilter.endDate>>>>, And<GLTran.isReclassReverse, Equal<True>, And<GLTran.posted, Equal<IIf<Where<CurrentValue<AccountByPeriodFilter.includeUnposted>, Equal<True>>, GLTran.posted, True>>, And<GLTran.released, Equal<IIf<Where<CurrentValue<AccountByPeriodFilter.includeUnreleased>, Equal<True>>, GLTran.released, True>>, And<GLTran.origModule, IsNotNull, And<GLTran.origBatchNbr, IsNotNull, And<GLTran.origLineNbr, IsNotNull>>>>>>>>, Aggregate<Sum<GLTran.creditAmt, Sum<GLTran.debitAmt, Sum<GLTran.curyCreditAmt, Sum<GLTran.curyDebitAmt, GroupBy<GLTran.origModule, GroupBy<GLTran.origBatchNbr, GroupBy<GLTran.origLineNbr>>>>>>>>>))]
public class ReclassifyingGLTranAggregate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (GLTran.origModule))]
  public virtual 
  #nullable disable
  string Module { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (GLTran.origBatchNbr))]
  public virtual string BatchNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (GLTran.origLineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBBaseCury(typeof (GLTranR.ledgerID), BqlField = typeof (GLTran.debitAmt))]
  public Decimal? DebitAmt { get; set; }

  [PXDBBaseCury(typeof (GLTranR.ledgerID), BqlField = typeof (GLTran.creditAmt))]
  public Decimal? CreditAmt { get; set; }

  [PXDBCury(typeof (GLTranR.curyID), BqlField = typeof (GLTran.curyDebitAmt))]
  public Decimal? CuryDebitAmt { get; set; }

  [PXDBCury(typeof (GLTranR.curyID), BqlField = typeof (GLTran.curyCreditAmt))]
  public Decimal? CuryCreditAmt { get; set; }

  public abstract class module : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.module>
  {
  }

  public abstract class batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReclassifyingGLTranAggregate.lineNbr>
  {
  }

  public abstract class debitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.debitAmt>
  {
  }

  public abstract class creditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.creditAmt>
  {
  }

  public abstract class curyDebitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.curyDebitAmt>
  {
  }

  public abstract class curyCreditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ReclassifyingGLTranAggregate.curyCreditAmt>
  {
  }
}
