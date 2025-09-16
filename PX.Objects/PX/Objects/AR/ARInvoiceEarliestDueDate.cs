// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEarliestDueDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Internal DAC used to execute the <see cref="P:PX.Objects.AR.ARBalances.OldInvoiceDate" /> update statement in the <see cref="T:PX.Objects.AR.OldInvoiceDateRefresher" />.
/// </summary>
[PXHidden]
[PXProjection(typeof (Select4<ARRegister, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.released, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.openDoc, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.dueDate, IsNotNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, In3<ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge>>>>>.And<BqlOperand<ARRegister.origDocAmt, IBqlDecimal>.IsGreater<decimal0>>>>>>, Aggregate<Min<ARRegister.dueDate, GroupBy<ARRegister.customerID, GroupBy<ARRegister.customerLocationID, GroupBy<ARRegister.branchID>>>>>>))]
public class ARInvoiceEarliestDueDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="!:Branch" />.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (ARRegister.branchID))]
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer" />.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (ARRegister.customerID))]
  public virtual int? CustomerID { get; set; }

  /// <summary>
  /// The identifier of the <see cref="!:Location">Customer Location</see>.
  /// </summary>
  [PXDBInt(IsKey = true, BqlField = typeof (ARRegister.customerLocationID))]
  public virtual int? CustomerLocationID { get; set; }

  /// <summary>
  /// The earliest due date among all documents of the corresponding <see cref="P:PX.Objects.AR.ARInvoiceEarliestDueDate.BranchID" />, <see cref="P:PX.Objects.AR.ARInvoiceEarliestDueDate.CustomerID" />, <see cref="P:PX.Objects.AR.ARInvoiceEarliestDueDate.CustomerLocationID" />.
  /// </summary>
  [PXDBDate(BqlField = typeof (ARRegister.dueDate))]
  public virtual DateTime? DueDate { get; set; }

  public abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceEarliestDueDate.branchID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARInvoiceEarliestDueDate.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARInvoiceEarliestDueDate.customerLocationID>
  {
  }

  public abstract class dueDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARInvoiceEarliestDueDate.dueDate>
  {
  }
}
