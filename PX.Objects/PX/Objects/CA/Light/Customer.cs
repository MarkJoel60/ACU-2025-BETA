// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.Customer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[PXTable(new Type[] {typeof (BAccount.bAccountID)})]
[PXCacheName("Customer")]
[Serializable]
public class Customer : BAccount
{
  [PXDBString(10, IsUnicode = true)]
  public virtual 
  #nullable disable
  string CustomerClassID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string StatementCycleId { get; set; }

  public new class PK : PrimaryKeyOf<Customer>.By<Customer.bAccountID>
  {
    public static Customer Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (Customer) PrimaryKeyOf<Customer>.By<Customer.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<Customer>.By<Customer.acctCD>
  {
    public static Customer Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (Customer) PrimaryKeyOf<Customer>.By<Customer.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Customer.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.acctName>
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

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Customer.noteID>
  {
  }
}
