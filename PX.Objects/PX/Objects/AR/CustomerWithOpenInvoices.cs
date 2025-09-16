// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerWithOpenInvoices
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select5<PX.Objects.AR.Standalone.ARRegister, InnerJoin<Customer, On<Customer.bAccountID, Equal<PX.Objects.AR.Standalone.ARRegister.customerID>>>, Where<PX.Objects.AR.Standalone.ARRegister.released, Equal<True>, And<PX.Objects.AR.Standalone.ARRegister.openDoc, Equal<True>, And<Where<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ARDocType.invoice>, Or<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ARDocType.finCharge>, Or<PX.Objects.AR.Standalone.ARRegister.docType, Equal<ARDocType.debitMemo>>>>>>>, Aggregate<GroupBy<PX.Objects.AR.Standalone.ARRegister.customerID, GroupBy<Customer.statementCycleId>>>>), Persistent = false)]
[PXHidden]
public class CustomerWithOpenInvoices : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (ARRegister.customerID))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (Customer.statementCycleId))]
  public virtual 
  #nullable disable
  string StatementCycleId { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerWithOpenInvoices.customerID>
  {
  }

  public abstract class statementCycleId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerWithOpenInvoices.statementCycleId>
  {
  }
}
