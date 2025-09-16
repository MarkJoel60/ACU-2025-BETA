// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.LinkRecognizedLinePMExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.PM;

public class LinkRecognizedLinePMExtension : 
  PXGraphExtension<LinkRecognizedLineExtension, APInvoiceRecognitionEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  protected virtual void _(PX.Data.Events.RowSelected<APRecognizedInvoice> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = this.IsIntegratedWithAP();
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.costCodes>();
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APTran.projectID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisibility<PX.Objects.AP.APTran.projectID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APTran.taskID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisibility<PX.Objects.AP.APTran.taskID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APTran.pOLineNbr>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisibility<PX.Objects.AP.APTran.pOLineNbr>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APTran.costCodeID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1 & flag2);
    PXUIFieldAttribute.SetVisibility<PX.Objects.AP.APTran.costCodeID>(((PXSelectBase) ((PXGraphExtension<APInvoiceRecognitionEntry>) this).Base.Transactions).Cache, (object) null, flag1 & flag2 ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<POReceiptLineS.projectID>(((PXSelectBase) this.Base1.linkLineReceiptTran).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<POReceiptLineS.taskID>(((PXSelectBase) this.Base1.linkLineReceiptTran).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<POReceiptLineS.costCodeID>(((PXSelectBase) this.Base1.linkLineReceiptTran).Cache, (object) null, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<POLineS.projectID>(((PXSelectBase) this.Base1.linkLineOrderTran).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<POLineS.taskID>(((PXSelectBase) this.Base1.linkLineOrderTran).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<POLineS.costCodeID>(((PXSelectBase) this.Base1.linkLineOrderTran).Cache, (object) null, flag1 & flag2);
  }

  [PXOverride]
  public APRecognizedTran ClearAPTranReferences(
    APRecognizedTran apTran,
    LinkRecognizedLinePMExtension.ClearAPTranReferencesDelegate baseMethod)
  {
    apTran = baseMethod(apTran);
    if (this.IsIntegratedWithAP())
      apTran.ProjectID = ProjectDefaultAttribute.NonProject();
    return apTran;
  }

  protected virtual bool IsIntegratedWithAP()
  {
    return ProjectAttribute.IsPMVisible("AP") && PXAccess.FeatureInstalled<FeaturesSet.projectRelatedDocumentsRecognition>();
  }

  public delegate APRecognizedTran ClearAPTranReferencesDelegate(APRecognizedTran apTran);
}
