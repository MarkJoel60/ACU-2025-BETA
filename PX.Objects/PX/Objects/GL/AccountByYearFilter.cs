// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountByYearFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.Descriptor;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class AccountByYearFilter : GLHistoryEnqFilter
{
  protected 
  #nullable disable
  string _FinYear;

  public override string BegFinPeriod
  {
    get
    {
      return this._FinYear == null ? (string) null : GLHistoryEnqFilter.FirstPeriodOfYear(this._FinYear);
    }
  }

  [PXDBString(4)]
  [PXDefault]
  [PXUIField]
  [GenericFinYearSelector(null, typeof (AccessInfo.businessDate), typeof (AccountByYearFilter.branchID), null, typeof (GLHistoryEnqFilter.organizationID), false, false, typeof (AccountByYearFilter.useMasterCalendar), false, null)]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByYearFilter.branchID>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByYearFilter.ledgerID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByYearFilter.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccountByYearFilter.subID>
  {
  }

  public new abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountByYearFilter.subCD>
  {
  }

  public new abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByYearFilter.subCDWildcard>
  {
  }

  public new abstract class begFinPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AccountByYearFilter.begFinPeriod>
  {
  }

  public new abstract class showCuryDetail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByYearFilter.showCuryDetail>
  {
  }

  public new abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AccountByYearFilter.useMasterCalendar>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccountByYearFilter.finYear>
  {
  }
}
