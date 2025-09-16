// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentCCProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

[PXHidden]
public class ARPaymentCCProcessing : PXGraph<ARPaymentCCProcessing>
{
  public PXSelect<ARPayment> Document;
  public PXSelectReadonly<ExternalTransaction, Where<ExternalTransaction.refNbr, Equal<Current<ARPayment.refNbr>>, And<Where<ExternalTransaction.docType, Equal<Current<ARPayment.docType>>, Or<ARDocType.voidPayment, Equal<Current<ARPayment.docType>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>> ExtTran;
}
