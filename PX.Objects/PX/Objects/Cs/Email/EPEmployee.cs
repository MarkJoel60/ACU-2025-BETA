// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.EPEmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.CS.Email;

[PXTable(new Type[] {typeof (BAccount.bAccountID)})]
[PXCacheName("Employee")]
[PXPrimaryGraph(new Type[] {typeof (EmployeeMaint)}, new Type[] {typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>)})]
[PXHidden]
public sealed class EPEmployee : BAccount
{
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  public string CalendarID { get; set; }

  [PXDBGuid(false)]
  [PXUIField]
  public Guid? UserID { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.bAccountID>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EPEmployee.parentBAccountID>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEmployee.defContactID>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctName>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.acctCD>
  {
  }

  public abstract class calendarID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEmployee.calendarID>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.userID>
  {
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEmployee.vStatus>
  {
  }
}
