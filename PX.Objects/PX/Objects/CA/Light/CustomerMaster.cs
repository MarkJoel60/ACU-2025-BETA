// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.CustomerMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[Serializable]
public class CustomerMaster : Customer
{
  [Customer(IsKey = true, DisplayName = "Customer ID")]
  public override int? BAccountID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  public new class PK : PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.bAccountID>
  {
    public static CustomerMaster Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (CustomerMaster) PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.acctCD>
  {
    public static CustomerMaster Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (CustomerMaster) PrimaryKeyOf<CustomerMaster>.By<CustomerMaster.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerMaster.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerMaster.acctCD>
  {
  }

  public new abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerMaster.statementCycleId>
  {
  }
}
