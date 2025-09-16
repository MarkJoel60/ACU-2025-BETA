// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerSharedCredit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Customer Shared Credit")]
[PXProjection(typeof (Select<Customer>))]
[Serializable]
public class CustomerSharedCredit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (Customer))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(30, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(60, IsUnicode = true, BqlTable = typeof (Customer))]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  [PXPersonalDataField]
  public virtual string AcctName { get; set; }

  [PXDBInt(BqlTable = typeof (Customer))]
  public virtual int? SharedCreditCustomerID { get; set; }

  [PXDBBool(BqlTable = typeof (Customer))]
  public virtual bool? SharedCreditPolicy { get; set; }

  [PXDBString(1, IsFixed = true, BqlTable = typeof (Customer))]
  [PX.Objects.AR.CreditRule]
  [PXUIField(DisplayName = "Credit Verification")]
  public virtual string CreditRule { get; set; }

  [PXDBBaseCury(null, null, BqlTable = typeof (Customer))]
  [PXUIField(DisplayName = "Credit Limit")]
  public virtual Decimal? CreditLimit { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerSharedCredit.bAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerSharedCredit.acctCD>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerSharedCredit.acctName>
  {
  }

  public abstract class sharedCreditCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerSharedCredit.sharedCreditCustomerID>
  {
  }

  public abstract class sharedCreditPolicy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CustomerSharedCredit.sharedCreditPolicy>
  {
  }

  public abstract class creditRule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerSharedCredit.creditRule>
  {
  }

  public abstract class creditLimit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CustomerSharedCredit.creditLimit>
  {
  }
}
