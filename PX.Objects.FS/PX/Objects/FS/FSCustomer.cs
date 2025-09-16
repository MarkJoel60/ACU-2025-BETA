// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCustomer
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (CustomerMaintBridge))]
[Serializable]
public class FSCustomer : PX.Objects.AR.Customer
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  FSCustomer>.By<PX.Objects.AR.Customer.acctCD>
  {
    public static FSCustomer Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (FSCustomer) PrimaryKeyOf<FSCustomer>.By<PX.Objects.AR.Customer.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCustomer.bAccountID>
  {
  }

  public new abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCustomer.cOrgBAccountID>
  {
  }
}
