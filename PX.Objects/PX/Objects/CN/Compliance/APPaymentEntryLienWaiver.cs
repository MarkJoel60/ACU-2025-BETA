// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.APPaymentEntryLienWaiver
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Common.Services.DataProviders;
using PX.Objects.CN.Compliance.AP.CacheExtensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes;
using PX.Objects.CN.Compliance.PM.DAC;
using PX.Objects.CN.JointChecks;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance;

public class APPaymentEntryLienWaiver : PXGraphExtension<APPaymentEntry>
{
  private APPaymentEntryLienWaiver.LienWaiverConst lienWaiverTypes;
  [PXCopyPasteHiddenView]
  public PXSetup<LienWaiverSetup> lienWaiverSetup;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocument> LienWaivers;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentPaymentReference> LinkToPayments;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentReference> LienWaiversRefs;
  [PXCopyPasteHiddenView]
  public PXSelect<ComplianceDocumentBill> LinkToBills;
  public PXSelectJoin<APAdjust, InnerJoin<PX.Objects.AP.APInvoice, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APInvoice.docType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APInvoice.refNbr>>>, InnerJoin<PX.Objects.AP.APTran, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APTran.tranType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And<Where<APAdjust.adjdLineNbr, Equal<Zero>, Or<APAdjust.adjdLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>>>>, LeftJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.AP.APTran.pOOrderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.AP.APTran.pONbr>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>> Transactions;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<JointPayeePayment, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<JointPayeePayment.jointPayeeId>>>, Where<JointPayeePayment.paymentDocType, Equal<Current<APPayment.docType>>, And<JointPayeePayment.paymentRefNbr, Equal<Current<APPayment.refNbr>>, And<JointPayee.isMainPayee, Equal<False>>>>> JointPayments;
  public PXAction<APPayment> setAsFinal;
  private bool skipSelectorVerificationForPOLine;

  private APPaymentEntryLienWaiver.LienWaiverConst LienWaiverTypes
  {
    get
    {
      if (this.lienWaiverTypes == null)
      {
        int? waiverDocumentType = this.GetLienWaiverDocumentType();
        int? lienWaiverType1 = this.GetLienWaiverType(waiverDocumentType, "Conditional Partial");
        int? lienWaiverType2 = this.GetLienWaiverType(waiverDocumentType, "Conditional Final");
        int? lienWaiverType3 = this.GetLienWaiverType(waiverDocumentType, "Unconditional Partial");
        int? lienWaiverType4 = this.GetLienWaiverType(waiverDocumentType, "Unconditional Final");
        this.lienWaiverTypes = new APPaymentEntryLienWaiver.LienWaiverConst(waiverDocumentType, lienWaiverType1, lienWaiverType2, lienWaiverType3, lienWaiverType4);
      }
      return this.lienWaiverTypes;
    }
  }

  public bool IsPreparePaymentsMassProcessing { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXOverride]
  public virtual IEnumerable PutOnHold(PXAdapter adapter)
  {
    this.RemoveAutoLienWaivers();
    return adapter.Get();
  }

  [PXOverride]
  public virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    if (((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldStopPayments.GetValueOrDefault() && this.ContainsOutstandingLienWavers(((PXSelectBase<APPayment>) this.Base.Document).Current))
      throw new PXException($"The accounts payable bill has at least one outstanding lien waiver. Payment is not allowed because the Stop Payment of AP Bill When There Are Outstanding Lien Waivers check box is selected on the Compliance Preferences (CL301000) form.{Environment.NewLine}The AP payment or payments will be assigned the On Hold status.");
    this.GenerateLienWaivers();
    return adapter.Get();
  }

  [PXOverride]
  public void VoidCheckProc(APPayment payment, Action<APPayment> baseHandler)
  {
    baseHandler(payment);
    this.VoidAutomaticallyCreatedLienWaivers(payment);
  }

  [PXUIField(DisplayName = "Set As Final")]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SetAsFinal(PXAdapter adapter)
  {
    if (((PXSelectBase<ComplianceDocument>) this.LienWaivers).Current != null)
    {
      ComplianceDocument current = ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Current;
      int? nullable1 = current.DocumentTypeValue;
      int? conditionalPartial1 = this.LienWaiverTypes.ConditionalPartial;
      int? nullable2;
      if (!(nullable1.GetValueOrDefault() == conditionalPartial1.GetValueOrDefault() & nullable1.HasValue == conditionalPartial1.HasValue))
      {
        nullable2 = current.DocumentTypeValue;
        nullable1 = this.LienWaiverTypes.UnconditionalPartial;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          goto label_13;
      }
      if (!this.AllLinkedBillsPaid(current) && ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Ask("Set Lien Waiver as Final", this.GetDialogMessage(current), (MessageButtons) 1, (MessageIcon) 3, true) != 1)
        return adapter.Get();
      int? documentTypeValue = current.DocumentTypeValue;
      nullable1 = current.DocumentTypeValue;
      nullable2 = this.LienWaiverTypes.ConditionalPartial;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        current.DocumentTypeValue = this.LienWaiverTypes.ConditionalFinal;
      nullable2 = current.DocumentTypeValue;
      nullable1 = this.LienWaiverTypes.UnconditionalPartial;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        current.DocumentTypeValue = this.LienWaiverTypes.UnconditionalFinal;
      try
      {
        ComplianceDocumentLienWaiverTypeAttribute.ValidateNoFinalLienWaiverExists(((PXSelectBase) this.LienWaivers).Cache, current);
      }
      catch (PXSetPropertyException<ComplianceDocument.documentTypeValue> ex)
      {
        current.DocumentTypeValue = documentTypeValue;
        int? nullable3 = documentTypeValue;
        int? conditionalPartial2 = this.LienWaiverTypes.ConditionalPartial;
        string str = nullable3.GetValueOrDefault() == conditionalPartial2.GetValueOrDefault() & nullable3.HasValue == conditionalPartial2.HasValue ? "Conditional Partial" : "Unconditional Partial";
        ((PXSelectBase) this.LienWaivers).Cache.RaiseExceptionHandling<ComplianceDocument.documentTypeValue>((object) current, (object) str, (Exception) ex);
        return adapter.Get();
      }
      APPaymentEntryLienWaiver.RecalculateLWAmount((PXGraph) this.Base, current);
      ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Update(current);
    }
label_13:
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<APPayment> e)
  {
    APPayment row = e.Row;
    if (row == null)
      return;
    List<PXResult<ComplianceDocument>> list = ((IEnumerable<PXResult<ComplianceDocument>>) ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, LeftJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.apCheckId>>>, Where<ComplianceDocumentReference.type, Equal<Required<APPayment.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<APPayment.refNbr>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>>>>>((PXGraph) this.Base)).Select(new object[3]
    {
      (object) row.DocType,
      (object) row.RefNbr,
      (object) this.LienWaiverTypes.DocType
    })).ToList<PXResult<ComplianceDocument>>();
    ((PXAction) this.setAsFinal).SetEnabled(row.DocType == "CHK" && !row.Hold.GetValueOrDefault() && list.Count<PXResult<ComplianceDocument>>() > 0);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> e)
  {
    if (e.Row == null)
      return;
    ComplianceDocument row = e.Row;
    int? documentType = row.DocumentType;
    int? docType = this.LienWaiverTypes.DocType;
    if (!(documentType.GetValueOrDefault() == docType.GetValueOrDefault() & documentType.HasValue == docType.HasValue) || !row.ThroughDate.HasValue)
      return;
    DateTime? throughDate = row.ThroughDate;
    DateTime? businessDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
    if ((throughDate.HasValue & businessDate.HasValue ? (throughDate.GetValueOrDefault() < businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || row.Received.GetValueOrDefault())
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) row.VendorID
    }));
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ComplianceDocument>>) e).Cache.RaiseExceptionHandling<ComplianceDocument.throughDate>((object) row, (object) row.ThroughDate, (Exception) new PXSetPropertyException<ComplianceDocument.throughDate>("There is an outstanding lien waiver for the {0} vendor.", (PXErrorLevel) 2, new object[1]
    {
      (object) vendor.AcctName
    }));
  }

  [PXOverride]
  public virtual void CreatePayment(
    PX.Objects.AP.APInvoice apdoc,
    string paymentType,
    Action<PX.Objects.AP.APInvoice, string> baseMethod)
  {
    baseMethod(apdoc, paymentType);
    if (((PXSelectBase<APPayment>) this.Base.Document).Current.Hold.GetValueOrDefault())
      return;
    this.GenerateLienWaivers();
  }

  [PXOverride]
  public void Persist(Action baseMethod)
  {
    if (this.ShouldRegenerateLienWaiversOnSave(((PXSelectBase<APPayment>) this.Base.Document).Current))
    {
      this.RemoveAutoLienWaivers();
      this.GenerateLienWaivers();
    }
    baseMethod();
  }

  private bool ShouldRegenerateLienWaiversOnSave(APPayment row)
  {
    if (row == null || this.IsPreparePaymentsMassProcessing)
      return false;
    bool? nullable = row.Released;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = row.Voided;
    if (nullable.GetValueOrDefault())
      return false;
    nullable = row.Hold;
    if (nullable.GetValueOrDefault())
      return false;
    bool? valueOriginal1 = (bool?) ((PXSelectBase) this.Base.Document).Cache.GetValueOriginal<APPayment.hold>((object) row);
    nullable = row.Hold;
    if (!nullable.GetValueOrDefault() && valueOriginal1.GetValueOrDefault())
      return false;
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Adjustments).Cache.Inserted) || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Adjustments).Cache.Deleted))
      return true;
    foreach (APAdjust apAdjust in ((PXSelectBase) this.Base.Adjustments).Cache.Updated)
    {
      Decimal valueOriginal2 = (Decimal) ((PXSelectBase) this.Base.Adjustments).Cache.GetValueOriginal<APAdjust.curyAdjdAmt>((object) apAdjust);
      Decimal? curyAdjdAmt = apAdjust.CuryAdjdAmt;
      Decimal valueOrDefault = curyAdjdAmt.GetValueOrDefault();
      if (!(valueOriginal2 == valueOrDefault & curyAdjdAmt.HasValue))
        return true;
    }
    return !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Adjustments).Cache.Inserted) && !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Adjustments).Cache.Deleted) && ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) row) == 2 && NonGenericIEnumerableExtensions.Any_(((PXGraph) this.Base).Caches[typeof (ComplianceDocument)].Inserted);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<APPayment> e)
  {
    if (e.Row == null)
      return;
    this.RemoveAutoLienWaivers();
    foreach (PXResult<ComplianceDocument> pxResult in PXSelectBase<ComplianceDocument, PXSelectJoin<ComplianceDocument, InnerJoin<ComplianceDocumentReference, On<ComplianceDocument.apCheckId, Equal<ComplianceDocumentReference.complianceDocumentReferenceId>>>, Where<ComplianceDocumentReference.refNoteId, Equal<Current<APPayment.noteID>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.isCreatedAutomatically, Equal<False>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) this.LienWaiverTypes.DocType
    }))
    {
      ComplianceDocument complianceDocument1 = PXResult<ComplianceDocument>.op_Implicit(pxResult);
      Guid? nullable1 = complianceDocument1.BillID;
      if (nullable1.HasValue)
      {
        ComplianceDocument complianceDocument2 = complianceDocument1;
        nullable1 = new Guid?();
        Guid? nullable2 = nullable1;
        complianceDocument2.ApCheckID = nullable2;
        ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Update(complianceDocument1);
      }
      else
        ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Delete(complianceDocument1);
    }
  }

  private bool AllLinkedBillsPaid(ComplianceDocument lw)
  {
    foreach (PXResult<ComplianceDocumentBill> pxResult in ((PXSelectBase<ComplianceDocumentBill>) new PXSelect<ComplianceDocumentBill, Where<ComplianceDocumentBill.complianceDocumentID, Equal<Required<ComplianceDocument.complianceDocumentID>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) lw.ComplianceDocumentID
    }))
    {
      ComplianceDocumentBill complianceDocumentBill = PXResult<ComplianceDocumentBill>.op_Implicit(pxResult);
      APAdjust apAdjust = ((PXSelectBase<APAdjust>) new PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<ComplianceDocumentBill.docType>>, And<APAdjust.adjdRefNbr, Equal<Required<ComplianceDocumentBill.refNbr>>, And<APAdjust.adjdLineNbr, Equal<Required<ComplianceDocumentBill.lineNbr>>, And<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>>>>>>>((PXGraph) this.Base)).SelectSingle(new object[3]
      {
        (object) complianceDocumentBill.DocType,
        (object) complianceDocumentBill.RefNbr,
        (object) complianceDocumentBill.LineNbr
      });
      if (apAdjust != null)
      {
        Decimal? curyDocBal = apAdjust.CuryDocBal;
        Decimal num = 0M;
        if (!(curyDocBal.GetValueOrDefault() == num & curyDocBal.HasValue))
          return false;
      }
    }
    return true;
  }

  public static void RecalculateLWAmount(PXGraph graph, ComplianceDocument lw)
  {
    if (lw.JointVendorInternalId.HasValue || lw.JointVendorExternalName != null)
      return;
    PXResultset<ComplianceDocumentBill> pxResultset = ((PXSelectBase<ComplianceDocumentBill>) new PXSelect<ComplianceDocumentBill, Where<ComplianceDocumentBill.complianceDocumentID, Equal<Required<ComplianceDocument.complianceDocumentID>>>>(graph)).Select(new object[1]
    {
      (object) lw.ComplianceDocumentID
    });
    Decimal num1 = 0M;
    Decimal? nullable1 = new Decimal?(0M);
    foreach (PXResult<ComplianceDocumentBill> pxResult1 in pxResultset)
    {
      ComplianceDocumentBill complianceDocumentBill = PXResult<ComplianceDocumentBill>.op_Implicit(pxResult1);
      Decimal? nullable2 = nullable1;
      Decimal? nullable3 = complianceDocumentBill.AmountPaid;
      nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      PX.Objects.AP.APInvoice apInvoice = ((PXSelectBase<PX.Objects.AP.APInvoice>) new PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<ComplianceDocumentBill.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<ComplianceDocumentBill.refNbr>>>>>(graph)).SelectSingle(new object[2]
      {
        (object) complianceDocumentBill.DocType,
        (object) complianceDocumentBill.RefNbr
      });
      bool? nullable4 = apInvoice.IsJointPayees;
      if (nullable4.GetValueOrDefault())
      {
        nullable4 = apInvoice.PaymentsByLinesAllowed;
        if (nullable4.GetValueOrDefault())
        {
          foreach (PXResult<JointPayee> pxResult2 in ((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<ComplianceDocumentBill.docType>>, And<JointPayee.aPRefNbr, Equal<Required<ComplianceDocumentBill.refNbr>>, And<JointPayee.aPLineNbr, Equal<Required<ComplianceDocumentBill.lineNbr>>, And<JointPayee.isMainPayee, Equal<False>>>>>>(graph)).Select(new object[3]
          {
            (object) complianceDocumentBill.DocType,
            (object) complianceDocumentBill.RefNbr,
            (object) complianceDocumentBill.LineNbr
          }))
          {
            JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult2);
            Decimal num2 = num1;
            nullable3 = jointPayee.JointAmountOwed;
            Decimal valueOrDefault = nullable3.GetValueOrDefault();
            num1 = num2 + valueOrDefault;
          }
        }
        else
        {
          foreach (PXResult<JointPayee> pxResult3 in ((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<ComplianceDocumentBill.docType>>, And<JointPayee.aPRefNbr, Equal<Required<ComplianceDocumentBill.refNbr>>, And<JointPayee.aPLineNbr, IsNull, And<JointPayee.isMainPayee, Equal<False>>>>>>(graph)).Select(new object[2]
          {
            (object) complianceDocumentBill.DocType,
            (object) complianceDocumentBill.RefNbr
          }))
          {
            JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult3);
            Decimal num3 = num1;
            nullable3 = jointPayee.JointAmountOwed;
            Decimal valueOrDefault = nullable3.GetValueOrDefault();
            num1 = num3 + valueOrDefault;
          }
        }
      }
    }
    if (!(num1 > 0M))
      return;
    lw.JointLienWaiverAmount = new Decimal?(num1);
    ComplianceDocument complianceDocument = lw;
    Decimal? nullable5 = nullable1;
    Decimal num4 = num1;
    Decimal? nullable6 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + num4) : new Decimal?();
    complianceDocument.LienWaiverAmount = nullable6;
  }

  private string GetDialogMessage(ComplianceDocument lw)
  {
    int? documentTypeValue1 = lw.DocumentTypeValue;
    int? conditionalPartial = this.LienWaiverTypes.ConditionalPartial;
    string str1;
    if (!(documentTypeValue1.GetValueOrDefault() == conditionalPartial.GetValueOrDefault() & documentTypeValue1.HasValue == conditionalPartial.HasValue))
    {
      int? documentTypeValue2 = lw.DocumentTypeValue;
      int? conditionalFinal = this.LienWaiverTypes.ConditionalFinal;
      if (!(documentTypeValue2.GetValueOrDefault() == conditionalFinal.GetValueOrDefault() & documentTypeValue2.HasValue == conditionalFinal.HasValue))
      {
        str1 = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GroupByUnconditional;
        goto label_4;
      }
    }
    str1 = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GroupByConditional;
label_4:
    string str2 = str1;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, lw.ProjectID);
    PMTask pmTask = ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.taskID, Equal<Required<ComplianceDocument.costTaskID>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) lw.CostTaskID
    });
    ComplianceDocumentReference documentReference = ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<Required<ComplianceDocumentReference.complianceDocumentReferenceId>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) lw.PurchaseOrder
    });
    string str3 = "N/A";
    string str4 = documentReference == null ? (lw.Subcontract == null ? str3 : lw.Subcontract) : documentReference.ReferenceNumber;
    string dialogMessage;
    switch (str2)
    {
      case "CPT":
        dialogMessage = $"One or more bills from the {((PXSelectBase<APPayment>) this.Base.Document).Current.RefNbr} payment have open balance for the {str4} commitment, the {(pmProject != null ? (object) pmProject.ContractCD : (object) str3)} project, and the {(pmTask != null ? (object) pmTask.TaskCD : (object) str3)} project task. To set the lien waiver as final, click OK.";
        break;
      case "CP":
        dialogMessage = $"One or more bills from the {((PXSelectBase<APPayment>) this.Base.Document).Current.RefNbr} payment have open balance for the {str4} commitment and the {(pmProject != null ? (object) pmProject.ContractCD : (object) str3)} project. To set the lien waiver as final, click OK.";
        break;
      case "PT":
        dialogMessage = $"One or more bills from the {((PXSelectBase<APPayment>) this.Base.Document).Current.RefNbr} payment have open balance for the {(pmProject != null ? (object) pmProject.ContractCD : (object) str3)} project and the {(pmTask != null ? (object) pmTask.TaskCD : (object) str3)} project task. To set the lien waiver as final, click OK.";
        break;
      default:
        dialogMessage = $"One or more bills from the {((PXSelectBase<APPayment>) this.Base.Document).Current.RefNbr} payment have open balance for the {(pmProject != null ? (object) pmProject.ContractCD : (object) str3)} project. To set the lien waiver as final, click OK.";
        break;
    }
    return dialogMessage;
  }

  protected virtual void RemoveAutoLienWaivers()
  {
    foreach (PXResult<ComplianceDocument, ComplianceDocumentReference> pxResult in ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, InnerJoin<ComplianceDocumentReference, On<ComplianceDocument.apCheckId, Equal<ComplianceDocumentReference.complianceDocumentReferenceId>>>, Where<ComplianceDocumentReference.refNoteId, Equal<Current<APPayment.noteID>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.isCreatedAutomatically, Equal<True>>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) this.LienWaiverTypes.DocType
    }))
    {
      ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Delete(PXResult<ComplianceDocument, ComplianceDocumentReference>.op_Implicit(pxResult));
      ((PXSelectBase<ComplianceDocumentReference>) this.LienWaiversRefs).Delete(PXResult<ComplianceDocument, ComplianceDocumentReference>.op_Implicit(pxResult));
    }
    foreach (ComplianceDocument complianceDocument in ((PXGraph) this.Base).Caches[typeof (ComplianceDocument)].Inserted)
      ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Delete(complianceDocument);
    foreach (ComplianceDocumentReference documentReference in ((PXGraph) this.Base).Caches[typeof (ComplianceDocumentReference)].Inserted)
      ((PXSelectBase<ComplianceDocumentReference>) this.LienWaiversRefs).Delete(documentReference);
  }

  public virtual void GenerateLienWaivers()
  {
    APPaymentEntryLienWaiver.LienWaiverInfo leinWaiverData = this.GetLeinWaiverData(((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GenerateWithoutCommitmentConditional, ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GroupByConditional, true);
    foreach (ComplianceDocument manualLienWaiver in (IEnumerable<ComplianceDocument>) leinWaiverData.ManualLienWaivers)
    {
      if (!manualLienWaiver.ApCheckID.HasValue)
      {
        ComplianceDocumentPaymentReference paymentReference = new ComplianceDocumentPaymentReference();
        paymentReference.ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid());
        ((PXSelectBase<ComplianceDocumentPaymentReference>) this.LinkToPayments).Insert(paymentReference);
        manualLienWaiver.ApCheckID = paymentReference.ComplianceDocumentReferenceId;
        ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Update(manualLienWaiver);
      }
    }
    if (!this.IsAutoGenerateVendor())
      return;
    bool? nullable = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldGenerateConditional;
    if (nullable.GetValueOrDefault())
    {
      foreach (APPaymentEntryLienWaiver.LienWaiverData autoLienWaiver in (IEnumerable<APPaymentEntryLienWaiver.LienWaiverData>) leinWaiverData.AutoLienWaivers)
        this.GenerateLienWaiver(this.LienWaiverTypes.ConditionalPartial, autoLienWaiver);
    }
    nullable = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldGenerateUnconditional;
    if (!nullable.GetValueOrDefault())
      return;
    foreach (APPaymentEntryLienWaiver.LienWaiverData autoLienWaiver in (IEnumerable<APPaymentEntryLienWaiver.LienWaiverData>) this.GetLeinWaiverData(((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GenerateWithoutCommitmentUnconditional, ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.GroupByUnconditional, false).AutoLienWaivers)
      this.GenerateLienWaiver(this.LienWaiverTypes.UnconditionalPartial, autoLienWaiver);
  }

  private bool IsAutoGenerateVendor()
  {
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current != null)
    {
      VendorExtension extension = PXCacheEx.GetExtension<VendorExtension>((IBqlTable) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current);
      if (extension != null)
        return extension.ShouldGenerateLienWaivers.GetValueOrDefault();
    }
    return true;
  }

  public virtual APPayment PutOnHoldIfOutstandingLienWavers(APPayment payment)
  {
    if (((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldStopPayments.GetValueOrDefault() && this.ContainsOutstandingLienWavers(payment))
    {
      payment.Hold = new bool?(true);
      ((PXSelectBase<APPayment>) this.Base.Document).Update(payment);
    }
    return payment;
  }

  public virtual bool ContainsOutstandingLienWavers(APPayment payment)
  {
    if (PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectReadonly2<APAdjust, InnerJoin<PX.Objects.AP.APTran, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APTran.tranType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And2<Where<APAdjust.adjdLineNbr, Equal<Zero>, Or<APAdjust.adjdLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>, And<PX.Objects.AP.APTran.taskID, IsNotNull>>>>, LeftJoin<ComplianceDocument, On<ComplianceDocument.projectID, Equal<PX.Objects.AP.APTran.projectID>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<ComplianceDocument.complianceDocumentID, IsNotNull>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[3]
    {
      (object) payment.VendorID,
      (object) this.LienWaiverTypes.DocType,
      (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
    })) != null)
      return true;
    PXSelectReadonly2<APAdjust, InnerJoin<PX.Objects.AP.APTran, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APTran.tranType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And2<Where<APAdjust.adjdLineNbr, Equal<Zero>, Or<APAdjust.adjdLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>, And<PX.Objects.AP.APTran.taskID, IsNotNull>>>>, LeftJoin<ComplianceDocument, On<ComplianceDocument.projectID, Equal<PX.Objects.AP.APTran.projectID>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.jointVendorInternalId, Equal<Required<ComplianceDocument.jointVendorInternalId>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<ComplianceDocument.complianceDocumentID, IsNotNull>>>> pxSelectReadonly2 = new PXSelectReadonly2<APAdjust, InnerJoin<PX.Objects.AP.APTran, On<APAdjust.adjdDocType, Equal<PX.Objects.AP.APTran.tranType>, And<APAdjust.adjdRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And2<Where<APAdjust.adjdLineNbr, Equal<Zero>, Or<APAdjust.adjdLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>>>, And<PX.Objects.AP.APTran.taskID, IsNotNull>>>>, LeftJoin<ComplianceDocument, On<ComplianceDocument.projectID, Equal<PX.Objects.AP.APTran.projectID>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.jointVendorInternalId, Equal<Required<ComplianceDocument.jointVendorInternalId>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>>>, Where<APAdjust.adjgDocType, Equal<Current<APPayment.docType>>, And<APAdjust.adjgRefNbr, Equal<Current<APPayment.refNbr>>, And<ComplianceDocument.complianceDocumentID, IsNotNull>>>>((PXGraph) this.Base);
    foreach (PXResult<JointPayeePayment, JointPayee> pxResult in ((PXSelectBase<JointPayeePayment>) this.JointPayments).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayeePayment, JointPayee>.op_Implicit(pxResult);
      if (jointPayee.JointPayeeInternalId.HasValue)
      {
        if (PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) pxSelectReadonly2).SelectWindowed(0, 1, new object[4]
        {
          (object) payment.VendorID,
          (object) jointPayee.JointPayeeInternalId,
          (object) this.LienWaiverTypes.DocType,
          (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
        })) != null)
          return true;
      }
    }
    return false;
  }

  private APPaymentEntryLienWaiver.LienWaiverInfo GetLeinWaiverData(
    bool? generateWithoutCommitment,
    string groupBy,
    bool isConditionalCategory)
  {
    bool groupByTaskID = false;
    bool groupByOrderNbr = false;
    switch (groupBy)
    {
      case "PT":
        groupByTaskID = true;
        break;
      case "CP":
        groupByOrderNbr = true;
        break;
      case "CPT":
        groupByTaskID = true;
        groupByOrderNbr = true;
        break;
    }
    Dictionary<APPaymentEntryLienWaiver.LienWaiverKey, APPaymentEntryLienWaiver.LienWaiverData> dictionary = new Dictionary<APPaymentEntryLienWaiver.LienWaiverKey, APPaymentEntryLienWaiver.LienWaiverData>();
    HashSet<string> stringSet1 = new HashSet<string>();
    HashSet<string> stringSet2 = new HashSet<string>();
    List<ComplianceDocument> manualLienWaivers = new List<ComplianceDocument>();
    foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder> pxResult in ((PXSelectBase<APAdjust>) this.Transactions).Select(Array.Empty<object>()))
    {
      APAdjust adjust = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      PX.Objects.AP.APTran tran = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      PX.Objects.AP.APInvoice bill = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      PX.Objects.PO.POOrder order = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(pxResult);
      if (!(adjust.AdjdDocType == "QCK") && !ProjectDefaultAttribute.IsNonProject(tran.ProjectID))
      {
        string str = $"{bill.DocType}.{bill.RefNbr}";
        Decimal? nullable1;
        if (!stringSet1.Contains(str))
        {
          stringSet1.Add(str);
          List<ComplianceDocument> waiverLinkedToPayment = this.GetManualLienWaiverLinkedToPayment(bill);
          manualLienWaivers.AddRange((IEnumerable<ComplianceDocument>) waiverLinkedToPayment);
          Decimal num1 = 0M;
          foreach (ComplianceDocument complianceDocument in waiverLinkedToPayment)
          {
            if (isConditionalCategory)
            {
              int? nullable2 = complianceDocument.DocumentTypeValue;
              int? conditionalFinal = this.LienWaiverTypes.ConditionalFinal;
              if (!(nullable2.GetValueOrDefault() == conditionalFinal.GetValueOrDefault() & nullable2.HasValue == conditionalFinal.HasValue))
              {
                int? documentTypeValue = complianceDocument.DocumentTypeValue;
                nullable2 = this.LienWaiverTypes.ConditionalPartial;
                if (!(documentTypeValue.GetValueOrDefault() == nullable2.GetValueOrDefault() & documentTypeValue.HasValue == nullable2.HasValue))
                  continue;
              }
              Decimal num2 = num1;
              nullable1 = complianceDocument.LienWaiverAmount;
              Decimal valueOrDefault = nullable1.GetValueOrDefault();
              num1 = num2 + valueOrDefault;
            }
            else
            {
              int? nullable3 = complianceDocument.DocumentTypeValue;
              int? unconditionalFinal = this.LienWaiverTypes.UnconditionalFinal;
              if (!(nullable3.GetValueOrDefault() == unconditionalFinal.GetValueOrDefault() & nullable3.HasValue == unconditionalFinal.HasValue))
              {
                int? documentTypeValue = complianceDocument.DocumentTypeValue;
                nullable3 = this.LienWaiverTypes.UnconditionalPartial;
                if (!(documentTypeValue.GetValueOrDefault() == nullable3.GetValueOrDefault() & documentTypeValue.HasValue == nullable3.HasValue))
                  continue;
              }
              Decimal num3 = num1;
              nullable1 = complianceDocument.LienWaiverAmount;
              Decimal valueOrDefault = nullable1.GetValueOrDefault();
              num1 = num3 + valueOrDefault;
            }
          }
          Decimal num4 = num1;
          nullable1 = bill.CuryOrigDocAmt;
          Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
          if (num4 >= valueOrDefault1 & nullable1.HasValue)
            stringSet2.Add(str);
        }
        if (!stringSet2.Contains(str))
        {
          APPaymentEntryLienWaiver.LienWaiverKey key = new APPaymentEntryLienWaiver.LienWaiverKey(tran.ProjectID.GetValueOrDefault(), groupByTaskID ? tran.TaskID : new int?(), groupByOrderNbr ? tran.PONbr : (string) null);
          if ((tran.PONbr != null || generateWithoutCommitment.GetValueOrDefault()) && this.IsSupported(bill, tran, groupByTaskID, groupByOrderNbr))
          {
            APPaymentEntryLienWaiver.LienWaiverData lienWaiverData1 = (APPaymentEntryLienWaiver.LienWaiverData) null;
            if (dictionary.TryGetValue(key, out lienWaiverData1))
              lienWaiverData1.Records.Add(new PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>(adjust, bill, tran, order));
            else if (this.CheckLimitsForLienWaiver(tran, order))
            {
              lienWaiverData1 = new APPaymentEntryLienWaiver.LienWaiverData(((PXSelectBase<APPayment>) this.Base.Document).Current, PMProject.PK.Find((PXGraph) this.Base, tran.ProjectID), this.GetExistingLienWaiverStats(this.LienWaiverTypes.DocType, ((PXSelectBase<APPayment>) this.Base.Document).Current.VendorID, tran.ProjectID, order));
              lienWaiverData1.Records.Add(new PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>(adjust, bill, tran, order));
              dictionary.Add(key, lienWaiverData1);
            }
            if (lienWaiverData1 != null)
            {
              if (lienWaiverData1.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, APAdjust>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, APAdjust>) (r => ((PXResult) r).GetItem<APAdjust>())).Where<APAdjust>((Func<APAdjust, bool>) (r => r == adjust)).Count<APAdjust>() < 2)
              {
                nullable1 = adjust.AdjgBalSign;
                Decimal num5 = nullable1 ?? 1M;
                APPaymentEntryLienWaiver.LienWaiverData lienWaiverData2 = lienWaiverData1;
                Decimal totalPayed = lienWaiverData2.TotalPayed;
                Decimal num6 = num5;
                nullable1 = adjust.AdjAmt;
                Decimal valueOrDefault = nullable1.GetValueOrDefault();
                Decimal num7 = num6 * valueOrDefault;
                lienWaiverData2.TotalPayed = totalPayed + num7;
              }
              APPaymentEntryLienWaiver.LienWaiverData lienWaiverData3 = lienWaiverData1;
              Decimal billBalance = lienWaiverData3.BillBalance;
              nullable1 = tran.TranBal;
              Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
              lienWaiverData3.BillBalance = billBalance + valueOrDefault2;
            }
          }
        }
      }
    }
    return new APPaymentEntryLienWaiver.LienWaiverInfo((ICollection<APPaymentEntryLienWaiver.LienWaiverData>) dictionary.Values, (IList<ComplianceDocument>) manualLienWaivers);
  }

  private bool IsSupported(PX.Objects.AP.APInvoice bill, PX.Objects.AP.APTran tran, bool groupByTaskID, bool groupByOrderNbr)
  {
    if (bill.PaymentsByLinesAllowed.GetValueOrDefault())
      return true;
    PX.Objects.AP.APTran apTran;
    if (groupByOrderNbr)
    {
      if (groupByTaskID)
        apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(((PXSelectBase<PX.Objects.AP.APTran>) new PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lineType, NotEqual<SOLineType.discount>, And<Where<PX.Objects.AP.APTran.taskID, NotEqual<Required<PX.Objects.AP.APTran.taskID>>, Or<PX.Objects.AP.APTran.pONbr, NotEqual<Required<PX.Objects.AP.APTran.pONbr>>>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
        {
          (object) tran.TranType,
          (object) tran.RefNbr,
          (object) tran.TaskID,
          (object) tran.PONbr
        }));
      else
        apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(((PXSelectBase<PX.Objects.AP.APTran>) new PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lineType, NotEqual<SOLineType.discount>, And<Where<PX.Objects.AP.APTran.projectID, NotEqual<Required<PX.Objects.AP.APTran.projectID>>, Or<PX.Objects.AP.APTran.pONbr, NotEqual<Required<PX.Objects.AP.APTran.pONbr>>>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
        {
          (object) tran.TranType,
          (object) tran.RefNbr,
          (object) tran.ProjectID,
          (object) tran.PONbr
        }));
    }
    else if (groupByTaskID)
      apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(((PXSelectBase<PX.Objects.AP.APTran>) new PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lineType, NotEqual<SOLineType.discount>, And<PX.Objects.AP.APTran.taskID, NotEqual<Required<PX.Objects.AP.APTran.taskID>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[3]
      {
        (object) tran.TranType,
        (object) tran.RefNbr,
        (object) tran.TaskID
      }));
    else
      apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(((PXSelectBase<PX.Objects.AP.APTran>) new PXSelect<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.tranType, Equal<Required<PX.Objects.AP.APTran.tranType>>, And<PX.Objects.AP.APTran.refNbr, Equal<Required<PX.Objects.AP.APTran.refNbr>>, And<PX.Objects.AP.APTran.lineType, NotEqual<SOLineType.discount>, And<PX.Objects.AP.APTran.projectID, NotEqual<Required<PX.Objects.AP.APTran.projectID>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[3]
      {
        (object) tran.TranType,
        (object) tran.RefNbr,
        (object) tran.ProjectID
      }));
    return apTran == null;
  }

  private List<ComplianceDocument> GetManualLienWaiverLinkedToPayment(PX.Objects.AP.APInvoice bill)
  {
    return ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, InnerJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.billID>>>, Where<ComplianceDocumentReference.type, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>, And<ComplianceDocument.linkToPayment, Equal<True>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.isCreatedAutomatically, Equal<False>>>>>>>((PXGraph) this.Base)).Select(new object[3]
    {
      (object) bill.DocType,
      (object) bill.RefNbr,
      (object) this.LienWaiverTypes.DocType
    }).FirstTableItems.ToList<ComplianceDocument>();
  }

  private bool CheckLimitsForLienWaiver(PX.Objects.AP.APTran tran, PX.Objects.PO.POOrder order)
  {
    PXSelect<LienWaiverRecipient, Where<LienWaiverRecipient.projectId, Equal<Required<LienWaiverRecipient.projectId>>, And<LienWaiverRecipient.vendorClassId, Equal<Required<LienWaiverRecipient.vendorClassId>>>>> pxSelect = new PXSelect<LienWaiverRecipient, Where<LienWaiverRecipient.projectId, Equal<Required<LienWaiverRecipient.projectId>>, And<LienWaiverRecipient.vendorClassId, Equal<Required<LienWaiverRecipient.vendorClassId>>>>>((PXGraph) this.Base);
    LienWaiverRecipient lienWaiverRecipient;
    if (!((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID.HasValue)
    {
      lienWaiverRecipient = PXResultset<LienWaiverRecipient>.op_Implicit(((PXSelectBase<LienWaiverRecipient>) pxSelect).Select(new object[2]
      {
        (object) tran.ProjectID,
        (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.VendorClassID
      }));
    }
    else
    {
      JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID);
      if (!jointPayee.JointPayeeInternalId.HasValue)
        return true;
      PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this.Base, jointPayee.JointPayeeInternalId);
      lienWaiverRecipient = PXResultset<LienWaiverRecipient>.op_Implicit(((PXSelectBase<LienWaiverRecipient>) pxSelect).Select(new object[2]
      {
        (object) tran.ProjectID,
        (object) vendor.VendorClassID
      }));
    }
    if (lienWaiverRecipient == null)
      return false;
    if (order == null || order.OrderNbr == null)
      return true;
    Decimal? commitmentAmount = lienWaiverRecipient.MinimumCommitmentAmount;
    Decimal? curyOrderTotal = order.CuryOrderTotal;
    return commitmentAmount.GetValueOrDefault() <= curyOrderTotal.GetValueOrDefault() & commitmentAmount.HasValue & curyOrderTotal.HasValue;
  }

  private APPaymentEntryLienWaiver.ExistingLienWaiverStats GetExistingLienWaiverStats(
    int? lienWaverDocTypeID,
    int? vendorID,
    int? projectID,
    PX.Objects.PO.POOrder order)
  {
    List<object> objectList = new List<object>();
    PXSelect<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.lienNoticeAmount, IsNotNull, And<ComplianceDocument.isVoided, NotEqual<True>>>>>>> pxSelect = new PXSelect<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.lienNoticeAmount, IsNotNull, And<ComplianceDocument.isVoided, NotEqual<True>>>>>>>((PXGraph) this.Base);
    objectList.Add((object) lienWaverDocTypeID);
    objectList.Add((object) projectID);
    objectList.Add((object) vendorID);
    if (order.OrderType != "RS")
    {
      ((PXSelectBase<ComplianceDocument>) pxSelect).WhereAnd<Where<ComplianceDocument.subcontract, Equal<Required<ComplianceDocument.subcontract>>>>();
      objectList.Add((object) order.OrderNbr);
    }
    else
    {
      ((PXSelectBase<ComplianceDocument>) pxSelect).Join<InnerJoin<ComplianceDocumentReference, On<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<ComplianceDocument.purchaseOrder>>>>();
      ((PXSelectBase<ComplianceDocument>) pxSelect).WhereAnd<Where<ComplianceDocumentReference.refNoteId, Equal<Required<ComplianceDocumentReference.refNoteId>>>>();
      objectList.Add((object) order.NoteID);
    }
    List<ComplianceDocument> list1 = ((IEnumerable<ComplianceDocument>) ((PXSelectBase<ComplianceDocument>) pxSelect).Select<ComplianceDocument>(new object[1]
    {
      (object) objectList
    })).ToList<ComplianceDocument>();
    List<Decimal?> list2 = list1.Select<ComplianceDocument, Decimal?>((Func<ComplianceDocument, Decimal?>) (lw => lw.LienNoticeAmount)).Where<Decimal?>((Func<Decimal?, bool>) (lna => lna.HasValue)).Distinct<Decimal?>().ToList<Decimal?>();
    APPaymentEntryLienWaiver.ExistingLienWaiverStats existingLienWaiverStats = new APPaymentEntryLienWaiver.ExistingLienWaiverStats();
    if (list2.IsSingleElement<Decimal?>())
      existingLienWaiverStats.NoticeAmount = list2.First<Decimal?>();
    existingLienWaiverStats.TotalAmount = list1.Sum<ComplianceDocument>((Func<ComplianceDocument, Decimal?>) (l => l.LienWaiverAmount));
    return existingLienWaiverStats;
  }

  private ComplianceDocument GenerateLienWaiver(
    int? type,
    APPaymentEntryLienWaiver.LienWaiverData data)
  {
    ComplianceDocument complianceDocument = new ComplianceDocument();
    complianceDocument.DocumentType = this.LienWaiverTypes.DocType;
    complianceDocument.DocumentTypeValue = type;
    complianceDocument.ProjectID = data.Project.ContractID;
    complianceDocument.VendorID = data.Payment.VendorID;
    complianceDocument.Required = new bool?(true);
    complianceDocument.IsCreatedAutomatically = new bool?(true);
    complianceDocument.CostTaskID = data.GetOnlyTaskID();
    complianceDocument.CostCodeID = data.GetOnlyCostCodeID();
    complianceDocument.AccountID = data.GetOnlyAccountID();
    complianceDocument.CustomerID = data.Project.CustomerID;
    complianceDocument.SkipInit = new bool?(true);
    complianceDocument.ApPaymentMethodID = data.Payment.PaymentMethodID;
    PX.Objects.PO.POOrder onlyOrder = data.GetOnlyOrder();
    if (onlyOrder != null)
    {
      if (onlyOrder.OrderType != "RS")
      {
        complianceDocument.PurchaseOrder = this.NewDocRef(onlyOrder.OrderType, onlyOrder.OrderNbr, onlyOrder.NoteID);
        complianceDocument.PurchaseOrderLineItem = data.GetOnlyPOLineNbr();
      }
      else
      {
        complianceDocument.Subcontract = onlyOrder.OrderNbr;
        complianceDocument.SubcontractLineItem = data.GetOnlyPOLineNbr();
      }
    }
    ComplianceDocumentPaymentReference paymentReference = new ComplianceDocumentPaymentReference();
    paymentReference.ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid());
    ((PXSelectBase<ComplianceDocumentPaymentReference>) this.LinkToPayments).Insert(paymentReference);
    complianceDocument.ApCheckID = paymentReference.ComplianceDocumentReferenceId;
    List<PX.Objects.AP.APInvoice> invoices = data.GetInvoices();
    List<APAdjust> lwLines = data.GetLWLines();
    if (invoices.Count == 1)
    {
      complianceDocument.BillID = this.NewDocRef(invoices[0].DocType, invoices[0].RefNbr, invoices[0].NoteID);
      complianceDocument.BillAmount = new Decimal?(invoices[0].CuryOrigDocAmt.GetValueOrDefault());
    }
    complianceDocument.PaymentDate = data.Payment.AdjDate;
    complianceDocument.SourceType = "AP Bill";
    complianceDocument.LienWaiverAmount = new Decimal?(Math.Max(0M, data.TotalPayed));
    int? nullable1;
    if (((PXSelectBase<APPayment>) this.Base.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID;
      if (nullable1.HasValue)
      {
        JointPayee jointPayee = JointPayee.PK.Find((PXGraph) this.Base, ((PXSelectBase<APPayment>) this.Base.Document).Current.JointPayeeID);
        if (jointPayee != null)
        {
          nullable1 = jointPayee.JointPayeeInternalId;
          if (nullable1.HasValue)
            complianceDocument.JointVendorInternalId = jointPayee.JointPayeeInternalId;
          else if (jointPayee.JointPayeeExternalName != null)
            complianceDocument.JointVendorExternalName = jointPayee.JointPayeeExternalName;
        }
      }
    }
    if (((PXSelectBase<JointPayeePayment>) this.JointPayments).Select(Array.Empty<object>()).Count > 0)
    {
      complianceDocument.JointAmount = complianceDocument.LienWaiverAmount;
      complianceDocument.JointLienWaiverAmount = complianceDocument.LienWaiverAmount;
    }
    nullable1 = type;
    int? conditionalFinal = this.LienWaiverTypes.ConditionalFinal;
    string str;
    if (!(nullable1.GetValueOrDefault() == conditionalFinal.GetValueOrDefault() & nullable1.HasValue == conditionalFinal.HasValue))
    {
      int? nullable2 = type;
      nullable1 = this.LienWaiverTypes.ConditionalPartial;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        str = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ThroughDateSourceUnconditional;
        goto label_19;
      }
    }
    str = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ThroughDateSourceConditional;
label_19:
    switch (str)
    {
      case "AP Bill Date":
        complianceDocument.ThroughDate = data.GetMaxInvoiceDate();
        break;
      case "AP Check Date":
        complianceDocument.ThroughDate = data.Payment.AdjDate;
        break;
      default:
        complianceDocument.ThroughDate = new DateTime?(FinancialPeriodDataProvider.GetFinancialPeriod((PXGraph) this.Base, data.Payment.AdjFinPeriodID).EndDate.GetValueOrDefault().AddDays(-1.0));
        break;
    }
    try
    {
      ComplianceDocumentLienWaiverTypeAttribute.ValidateNoFinalLienWaiverExists(((PXSelectBase) this.LienWaivers).Cache, complianceDocument);
    }
    catch (PXSetPropertyException<ComplianceDocument.documentTypeValue> ex)
    {
      return (ComplianceDocument) null;
    }
    this.skipSelectorVerificationForPOLine = true;
    try
    {
      ComplianceDocument lienWaiver = ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Insert(complianceDocument);
      foreach (APAdjust apAdjust in lwLines)
        ((PXSelectBase<ComplianceDocumentBill>) this.LinkToBills).Insert(new ComplianceDocumentBill()
        {
          ComplianceDocumentID = lienWaiver.ComplianceDocumentID,
          DocType = apAdjust.AdjdDocType,
          RefNbr = apAdjust.AdjdRefNbr,
          LineNbr = apAdjust.AdjdLineNbr,
          AmountPaid = apAdjust.AdjAmt
        });
      return lienWaiver;
    }
    finally
    {
      this.skipSelectorVerificationForPOLine = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<ComplianceDocument, ComplianceDocument.purchaseOrderLineItem> e)
  {
    if (!this.skipSelectorVerificationForPOLine)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<ComplianceDocument, ComplianceDocument.purchaseOrderLineItem>>) e).Cancel = true;
  }

  private Guid? NewDocRef(string type, string refNbr, Guid? noteID)
  {
    ComplianceDocumentReference documentReference = new ComplianceDocumentReference();
    documentReference.ComplianceDocumentReferenceId = new Guid?(Guid.NewGuid());
    documentReference.Type = type;
    documentReference.ReferenceNumber = refNbr;
    documentReference.RefNoteId = noteID;
    ((PXSelectBase<ComplianceDocumentReference>) this.LienWaiversRefs).Insert(documentReference);
    return documentReference.ComplianceDocumentReferenceId;
  }

  private int? GetLienWaiverDocumentType()
  {
    return ((PXSelectBase<ComplianceAttributeType>) new PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) "Lien Waiver"
    })?.ComplianceAttributeTypeID;
  }

  private int? GetLienWaiverType(int? lienWaiverDocumentType, string lienWaiverType)
  {
    return ((PXSelectBase<ComplianceAttribute>) new PXSelect<ComplianceAttribute, Where<ComplianceAttribute.type, Equal<Required<ComplianceAttribute.type>>, And<ComplianceAttribute.value, Equal<Required<ComplianceAttribute.value>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
    {
      (object) lienWaiverDocumentType,
      (object) lienWaiverType
    })?.AttributeId;
  }

  private void VoidAutomaticallyCreatedLienWaivers(APPayment payment)
  {
    foreach (PXResult<ComplianceDocument> pxResult in ((PXSelectBase<ComplianceDocument>) new PXSelectJoin<ComplianceDocument, InnerJoin<ComplianceDocumentReference, On<ComplianceDocument.apCheckId, Equal<ComplianceDocumentReference.complianceDocumentReferenceId>>>, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.isCreatedAutomatically, Equal<True>, And<ComplianceDocumentReference.refNoteId, Equal<Required<ComplianceDocumentReference.refNoteId>>>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) this.LienWaiverTypes.DocType,
      (object) payment.NoteID
    }))
    {
      ComplianceDocument complianceDocument = PXResult<ComplianceDocument>.op_Implicit(pxResult);
      complianceDocument.IsVoided = new bool?(true);
      ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Update(complianceDocument);
    }
  }

  public class LienWaiverInfo
  {
    public ICollection<APPaymentEntryLienWaiver.LienWaiverData> AutoLienWaivers { get; private set; }

    public IList<ComplianceDocument> ManualLienWaivers { get; private set; }

    public LienWaiverInfo(
      ICollection<APPaymentEntryLienWaiver.LienWaiverData> autoLienWaivers,
      IList<ComplianceDocument> manualLienWaivers)
    {
      this.AutoLienWaivers = autoLienWaivers;
      this.ManualLienWaivers = manualLienWaivers;
    }
  }

  public class LienWaiverData
  {
    public Decimal BillBalance { get; set; }

    public Decimal TotalPayed { get; set; }

    public APPaymentEntryLienWaiver.ExistingLienWaiverStats Stats { get; private set; }

    public PMProject Project { get; private set; }

    public APPayment Payment { get; private set; }

    public List<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>> Records { get; private set; }

    public List<PX.Objects.AP.APInvoice> GetInvoices()
    {
      List<PX.Objects.AP.APInvoice> invoices = new List<PX.Objects.AP.APInvoice>();
      HashSet<string> stringSet = new HashSet<string>();
      foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder> record in this.Records)
      {
        PX.Objects.AP.APInvoice apInvoice = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(record);
        PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(record);
        string str = $"{apInvoice.DocType}.{apInvoice.RefNbr}";
        if (!stringSet.Contains(str))
        {
          stringSet.Add(str);
          invoices.Add(apInvoice);
        }
      }
      return invoices;
    }

    public List<APAdjust> GetLWLines()
    {
      List<APAdjust> lwLines = new List<APAdjust>();
      foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder> record in this.Records)
      {
        APAdjust apAdjust = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(record);
        lwLines.Add(apAdjust);
      }
      return lwLines;
    }

    public Decimal GetAmountPaid(PX.Objects.AP.APInvoice bill)
    {
      Decimal amountPaid = 0M;
      foreach (PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder> record in this.Records)
      {
        PX.Objects.AP.APInvoice apInvoice = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(record);
        APAdjust apAdjust = PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>.op_Implicit(record);
        if (apInvoice.DocType == bill.DocType && apInvoice.RefNbr == bill.RefNbr)
          amountPaid += apAdjust.AdjAmt.GetValueOrDefault();
      }
      return amountPaid;
    }

    public PX.Objects.PO.POOrder GetOnlyOrder()
    {
      Guid?[] array = this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, Guid?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, Guid?>) (r => ((PXResult) r).GetItem<PX.Objects.PO.POOrder>().NoteID)).Distinct<Guid?>().ToArray<Guid?>();
      return array.Length == 1 && array[0].HasValue ? ((PXResult) this.Records[0]).GetItem<PX.Objects.PO.POOrder>() : (PX.Objects.PO.POOrder) null;
    }

    public int? GetOnlyTaskID()
    {
      int?[] array = this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.AP.APTran>().TaskID)).Distinct<int?>().ToArray<int?>();
      return array.Length != 1 ? new int?() : array[0];
    }

    public int? GetOnlyCostCodeID()
    {
      int?[] array = this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.AP.APTran>().CostCodeID)).Distinct<int?>().ToArray<int?>();
      return array.Length != 1 ? new int?() : array[0];
    }

    public int? GetOnlyAccountID()
    {
      int?[] array = this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.AP.APTran>().AccountID)).Distinct<int?>().ToArray<int?>();
      return array.Length != 1 ? new int?() : array[0];
    }

    public int? GetOnlyPOLineNbr()
    {
      int?[] array = this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, int?>) (r => ((PXResult) r).GetItem<PX.Objects.AP.APTran>().POLineNbr)).Distinct<int?>().ToArray<int?>();
      return array.Length != 1 ? new int?() : array[0];
    }

    public DateTime? GetMaxInvoiceDate()
    {
      return this.Records.Select<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, DateTime?>((Func<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>, DateTime?>) (r => ((PXResult) r).GetItem<PX.Objects.AP.APInvoice>().DocDate)).Max<DateTime?>();
    }

    public LienWaiverData(
      APPayment payment,
      PMProject project,
      APPaymentEntryLienWaiver.ExistingLienWaiverStats stats)
    {
      this.Payment = payment;
      this.Project = project;
      this.Stats = stats;
      this.Records = new List<PXResult<APAdjust, PX.Objects.AP.APInvoice, PX.Objects.AP.APTran, PX.Objects.PO.POOrder>>();
    }
  }

  public class ExistingLienWaiverStats
  {
    public Decimal? TotalAmount;
    public Decimal? NoticeAmount;
  }

  private class LienWaiverConst
  {
    public int? DocType { get; private set; }

    public int? ConditionalPartial { get; private set; }

    public int? ConditionalFinal { get; private set; }

    public int? UnconditionalPartial { get; private set; }

    public int? UnconditionalFinal { get; private set; }

    public LienWaiverConst(
      int? docType,
      int? conditionalPartial,
      int? conditionalFinal,
      int? unconditionalPartial,
      int? unconditionalFinal)
    {
      this.DocType = docType;
      this.ConditionalFinal = conditionalFinal;
      this.ConditionalPartial = conditionalPartial;
      this.UnconditionalFinal = unconditionalFinal;
      this.UnconditionalPartial = unconditionalPartial;
    }
  }

  [DebuggerDisplay("{BillNbr}.{ProjectID}.{TaskID}.{OrderNbr}")]
  public struct LienWaiverKey(int projectID, int? taskID, string orderNbr)
  {
    public readonly int ProjectID = projectID;
    public readonly int? TaskID = taskID;
    public readonly string OrderNbr = orderNbr;

    public override int GetHashCode()
    {
      return ((17 * 23 + this.ProjectID.GetHashCode()) * 23 + (this.TaskID.HasValue ? this.TaskID.GetHashCode() : 0)) * 23 + (this.OrderNbr != null ? this.OrderNbr.GetHashCode() : 0);
    }
  }
}
