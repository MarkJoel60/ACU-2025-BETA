// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoicePrintFormFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.SO;

public class SOInvoicePrintFormFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [ARInvoiceType.RefNbr(typeof (Search2<ARRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AR.ARInvoice, On<PX.Objects.AR.ARInvoice.docType, Equal<ARRegisterAlias.docType>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<ARRegisterAlias.refNbr>>>, InnerJoinSingleTable<PX.Objects.AR.Customer, On<ARRegisterAlias.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<ARRegisterAlias.docType, Equal<Optional<PX.Objects.AR.ARInvoice.docType>>, And<ARRegisterAlias.origModule, Equal<BatchModule.moduleSO>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, OrderBy<Desc<ARRegisterAlias.refNbr>>>), Filterable = true)]
  [ARInvoiceType.Numbering]
  [ARInvoiceNbr]
  public 
  #nullable disable
  string RefNbr { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOInvoicePrintFormFilter.refNbr>
  {
  }
}
