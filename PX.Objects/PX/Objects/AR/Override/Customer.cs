// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Override.Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Override;

[Serializable]
public class Customer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? BAccountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string StatementCycleId { get; set; }

  [PXDBBool]
  public virtual bool? ConsolidateStatements { get; set; }

  [PXDBInt]
  public virtual int? SharedCreditCustomerID { get; set; }

  [PXDBBool]
  public virtual bool? SharedCreditPolicy { get; set; }

  [PXDBDate]
  public virtual DateTime? StatementLastDate { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.bAccountID>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Customer.statementCycleId>
  {
  }

  public abstract class consolidateStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.consolidateStatements>
  {
  }

  public abstract class sharedCreditCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Customer.sharedCreditCustomerID>
  {
  }

  public abstract class sharedCreditPolicy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.sharedCreditPolicy>
  {
  }

  public abstract class statementLastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Customer.statementLastDate>
  {
  }
}
