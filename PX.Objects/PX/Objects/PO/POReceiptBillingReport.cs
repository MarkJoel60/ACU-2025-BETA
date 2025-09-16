// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptBillingReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PO;

public class POReceiptBillingReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReceiptNbr;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [POReceiptType.Numbering]
  [POReceiptType.RefNbr(typeof (Search2<POReceipt.receiptNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>, Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>, And<POReceipt.released, Equal<True>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>>, OrderBy<Desc<POReceipt.receiptNbr>>>), Filterable = true)]
  [PXUIField]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptBillingReport.receiptNbr>
  {
  }
}
