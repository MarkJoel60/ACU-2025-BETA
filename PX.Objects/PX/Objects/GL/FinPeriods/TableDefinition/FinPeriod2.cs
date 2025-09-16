// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.FinPeriods.TableDefinition;

public class FinPeriod2 : FinPeriod
{
  public new abstract class organizationID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  FinPeriod2.organizationID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod2.finPeriodID>
  {
  }

  public new abstract class startDate : IBqlField, IBqlOperand
  {
  }

  public new abstract class endDate : IBqlField, IBqlOperand
  {
  }

  public new abstract class descr : IBqlField, IBqlOperand
  {
  }

  public new abstract class status : IBqlField, IBqlOperand
  {
  }

  public new abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.closed>
  {
  }

  public new abstract class aPClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.aPClosed>
  {
  }

  public new abstract class aRClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.aRClosed>
  {
  }

  public new abstract class iNClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.iNClosed>
  {
  }

  public new abstract class cAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.cAClosed>
  {
  }

  public new abstract class fAClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.fAClosed>
  {
  }

  public new abstract class dateLocked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.dateLocked>
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.active>
  {
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod2.finYear>
  {
  }

  public new abstract class periodNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FinPeriod2.periodNbr>
  {
  }

  public new abstract class custom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FinPeriod2.custom>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }

  public new abstract class createdByID : IBqlField, IBqlOperand
  {
  }

  public new abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod2.createdByScreenID>
  {
  }

  public new abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriod2.createdDateTime>
  {
  }

  public new abstract class lastModifiedByID : IBqlField, IBqlOperand
  {
  }

  public new abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod2.lastModifiedByScreenID>
  {
  }

  public new abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FinPeriod2.lastModifiedDateTime>
  {
  }

  public new abstract class noteID : IBqlField, IBqlOperand
  {
  }

  public new abstract class masterFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FinPeriod2.masterFinPeriodID>
  {
  }
}
