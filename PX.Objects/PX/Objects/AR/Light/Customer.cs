// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Light.Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Light;

[PXTable(new Type[] {typeof (BAccount.bAccountID)})]
[PXCacheName("Light version of Customer DAC for Statements Printing")]
[Serializable]
public class Customer : BAccount
{
  [PXDBString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string CustomerClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string StatementCycleId { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Multi-Currency Statements")]
  public virtual bool? PrintCuryStatements { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Send Statements by Email")]
  public virtual bool? SendStatementByEmail { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Print Statements")]
  public virtual bool? PrintStatements { get; set; }

  [PXDBInt]
  [PXUIField]
  public virtual int? DefBillContactID { get; set; }

  [PXDBInt]
  public virtual int? DefBillAddressID { get; set; }

  /// <summary>
  /// The type of customer statements generated for the customer.
  /// The list of possible values of the field is determined by
  /// <see cref="T:PX.Objects.AR.StatementTypeAttribute" />.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Statement Type")]
  public virtual string StatementType { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.bAccountID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.curyID>
  {
  }

  public abstract class customerClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.customerClassID>
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

  public new abstract class consolidatingBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    Customer.consolidatingBAccountID>
  {
  }

  public abstract class printCuryStatements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.printCuryStatements>
  {
  }

  public abstract class sendStatementByEmail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Customer.sendStatementByEmail>
  {
  }

  public abstract class printStatements : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Customer.printStatements>
  {
  }

  public abstract class defBillContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defBillContactID>
  {
  }

  public abstract class defBillAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.defBillAddressID>
  {
  }

  public abstract class statementType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.statementType>
  {
  }
}
