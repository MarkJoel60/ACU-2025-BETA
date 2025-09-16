// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.JointChecks.APInvoiceEntryJointCheck
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.JointChecks;

public class APInvoiceEntryJointCheck : PXGraphExtension<APInvoiceEntry>
{
  [PXCopyPasteHiddenFields(new Type[] {typeof (JointPayee.curyJointBalance), typeof (JointPayee.curyJointAmountPaid)})]
  [PXViewDetailsButton(typeof (Vendor), typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<JointPayee.jointPayeeInternalId>>>>))]
  public PXSelect<JointPayee, Where2<Where<JointPayee.aPDocType, Equal<Current<APInvoice.docType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.refNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<False>, And<JointPayee.isMainPayee, Equal<False>>>>>, Or<Where<JointPayee.aPDocType, Equal<Current<APInvoice.origDocType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.origRefNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<True>, And<JointPayee.isMainPayee, Equal<False>>>>>>>> JointPayees;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<JointPayeePayment, InnerJoin<JointPayee, On<JointPayee.jointPayeeId, Equal<JointPayeePayment.jointPayeeId>, And<JointPayee.isMainPayee, NotEqual<True>>>, InnerJoin<APPayment, On<APPayment.docType, Equal<JointPayeePayment.paymentDocType>, And<APPayment.refNbr, Equal<JointPayeePayment.paymentRefNbr>>>>>, Where<JointPayeePayment.invoiceDocType, Equal<Current<APInvoice.docType>>, And<JointPayeePayment.invoiceRefNbr, Equal<Current<APInvoice.refNbr>>>>> JointAmountApplications;
  public PXSetup<LienWaiverSetup> lienWaiverSetup;
  public PXAction<APInvoice> viewApPayment;
  private int? lienWaiverDocType;

  [PXMergeAttributes]
  [PXDefault(typeof (APInvoice.docType))]
  protected virtual void _(Events.CacheAttached<JointPayee.aPDocType> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (APInvoice.refNbr))]
  [PXParent(typeof (Select<APInvoice, Where<APInvoice.docType, Equal<Current<JointPayee.aPDocType>>, And<APInvoice.refNbr, Equal<Current<JointPayee.aPRefNbr>>>>>))]
  protected virtual void _(Events.CacheAttached<JointPayee.aPRefNbr> e)
  {
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewApPayment(PXAdapter adapter)
  {
    JointPayeePayment current = ((PXSelectBase<JointPayeePayment>) this.JointAmountApplications).Current;
    if (current != null && !string.IsNullOrWhiteSpace(current.PaymentRefNbr))
    {
      APPayment apPayment = APPayment.PK.Find((PXGraph) this.Base, current.PaymentDocType, current.PaymentRefNbr);
      if (apPayment != null)
      {
        APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
        ((PXSelectBase<APPayment>) instance.Document).Current = apPayment;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Payment");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXOverride]
  public IEnumerable PayInvoice(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseMethod)
  {
    if (((PXSelectBase<APInvoice>) this.Base.Document).Current != null && this.IsJointPayee(((PXSelectBase<APInvoice>) this.Base.Document).Current))
    {
      PXSelect<JointPayee, Where2<Where<JointPayee.aPDocType, Equal<Current<APInvoice.docType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.refNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<False>>>>, Or<Where<JointPayee.aPDocType, Equal<Current<APInvoice.origDocType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.origRefNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<True>>>>>>> pxSelect1 = new PXSelect<JointPayee, Where2<Where<JointPayee.aPDocType, Equal<Current<APInvoice.docType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.refNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<False>>>>, Or<Where<JointPayee.aPDocType, Equal<Current<APInvoice.origDocType>>, And<JointPayee.aPRefNbr, Equal<Current<APInvoice.origRefNbr>>, And<Current<APInvoice.isRetainageDocument>, Equal<True>>>>>>>((PXGraph) this.Base);
      HashSet<int> intSet = new HashSet<int>();
      object[] objArray = Array.Empty<object>();
      foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) pxSelect1).Select(objArray))
      {
        JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
        intSet.Add(jointPayee.JointPayeeId.Value);
      }
      PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APAdjust.adjdRefNbr, Equal<Current<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<False>, And<APAdjust.jointPayeeID, IsNotNull>>>>, OrderBy<Asc<APAdjust.adjdRefNbr>>> pxSelect2 = new PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APAdjust.adjdRefNbr, Equal<Current<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<False>, And<APAdjust.jointPayeeID, IsNotNull>>>>, OrderBy<Asc<APAdjust.adjdRefNbr>>>((PXGraph) this.Base);
      foreach (PXResult<APAdjust> pxResult in ((PXSelectBase<APAdjust>) pxSelect2).Select(Array.Empty<object>()))
      {
        APAdjust apAdjust = PXResult<APAdjust>.op_Implicit(pxResult);
        intSet.Remove(apAdjust.JointPayeeID.Value);
      }
      if (intSet.Count == 0)
      {
        APAdjust apAdjust = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) pxSelect2).SelectWindowed(0, 1, Array.Empty<object>()));
        if (apAdjust != null)
        {
          APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
          ((PXSelectBase<APPayment>) instance.Document).Current = PXResultset<APPayment>.op_Implicit(((PXSelectBase<APPayment>) instance.Document).Search<APPayment.refNbr>((object) apAdjust.AdjgRefNbr, new object[1]
          {
            (object) apAdjust.AdjgDocType
          }));
          throw new PXRedirectRequiredException((PXGraph) instance, nameof (PayInvoice));
        }
      }
      APPayBills instance1 = PXGraph.CreateInstance<APPayBills>();
      ((PXGraph) instance1).Clear();
      PayBillsFilter copy = (PayBillsFilter) ((PXSelectBase) instance1.Filter).Cache.CreateCopy((object) ((PXSelectBase<PayBillsFilter>) instance1.Filter).Current);
      copy.VendorID = ((PXSelectBase<APInvoice>) this.Base.Document).Current.VendorID;
      copy.ShowPayInLessThan = new bool?(false);
      copy.PayTypeID = ((PXSelectBase<APInvoice>) this.Base.Document).Current.PayTypeID;
      copy.PayAccountID = ((PXSelectBase<APInvoice>) this.Base.Document).Current.PayAccountID;
      copy.DocType = ((PXSelectBase<APInvoice>) this.Base.Document).Current.DocType;
      copy.RefNbr = ((PXSelectBase<APInvoice>) this.Base.Document).Current.RefNbr;
      ((PXSelectBase<PayBillsFilter>) instance1.Filter).Update(copy);
      throw new PXRedirectRequiredException((PXGraph) instance1, "Pay Bills");
    }
    return baseMethod(adapter);
  }

  [PXOverride]
  public virtual void Persist(Action baseMethod)
  {
    if (this.IsJointPayee(((PXSelectBase<APInvoice>) this.Base.Document).Current))
    {
      if (((PXSelectBase<APInvoice>) this.Base.Document).Current.PaymentsByLinesAllowed.GetValueOrDefault())
        this.RecalculateAmountOwedByLine();
      else
        this.RecalculateAmountOwed();
    }
    baseMethod();
  }

  [PXOverride]
  public virtual void ClearRetainageSummary(APInvoice document, Action<APInvoice> baseMethod)
  {
    if (document.IsJointPayees.GetValueOrDefault() && document.DocType != "INV")
      document.IsJointPayees = new bool?(false);
    baseMethod(document);
  }

  protected virtual void _(Events.RowPersisting<APInvoice> e)
  {
    if (e.Row == null)
      return;
    APInvoice row = e.Row;
    if (row.IsJointPayees.GetValueOrDefault() && ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()).Count == 0)
      throw new PXSetPropertyException((IBqlTable) row, "At least one Joint Payee is required to be specified.");
  }

  protected virtual void _(
    Events.FieldUpdating<APInvoice, APInvoice.isJointPayees> e)
  {
    if (((PXGraph) this.Base).IsExport || ((PXGraph) this.Base).IsImport || ((PXGraph) this.Base).IsImportFromExcel || ((bool?) ((Events.FieldUpdatingBase<Events.FieldUpdating<APInvoice, APInvoice.isJointPayees>>) e).NewValue).GetValueOrDefault() || ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()).Count == 0 || ((PXSelectBase<APInvoice>) this.Base.Document).Ask("Warning", "If you clear the Joint Payees check box, the joint payee details specified on the Joint Payees tab will be removed from the document. Do you want to proceed?", (MessageButtons) 4, (MessageIcon) 3) == 6)
      return;
    ((Events.FieldUpdatingBase<Events.FieldUpdating<APInvoice, APInvoice.isJointPayees>>) e).Cancel = true;
    ((Events.FieldUpdatingBase<Events.FieldUpdating<APInvoice, APInvoice.isJointPayees>>) e).NewValue = (object) true;
  }

  protected virtual void _(
    Events.FieldUpdated<APInvoice, APInvoice.isJointPayees> e)
  {
    if (((bool?) e.NewValue).GetValueOrDefault() || e.Row.IsRetainageDocument.GetValueOrDefault() || ((PXSelectBase<JointPayeePayment>) this.JointAmountApplications).Select(Array.Empty<object>()).Count > 0)
      return;
    this.DeleteAllPayees();
  }

  public virtual void _(
    Events.FieldVerifying<APTran, APTran.curyLineAmt> e)
  {
    if (e.Row == null || !PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>())
      return;
    APInvoice current1 = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.PaymentsByLinesAllowed;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 == 0)
      return;
    APInvoice current2 = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.IsJointPayees;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 == 0)
      return;
    Decimal? newValue = (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<APTran, APTran.curyLineAmt>, APTran, object>) e).NewValue;
    Decimal num3 = 0M;
    if (newValue.GetValueOrDefault() < num3 & newValue.HasValue)
      throw new PXSetPropertyException("Negative lines are not allowed in documents with the Joint Payees check box selected.");
  }

  protected virtual void _(
    Events.FieldVerifying<JointPayee, JointPayee.curyJointAmountOwed> e)
  {
    if (e.Row.IsMainPayee.GetValueOrDefault())
      return;
    if (((PXSelectBase<APInvoice>) this.Base.Document).Current.PaymentsByLinesAllowed.GetValueOrDefault())
      this.ValidatePayByLine(e.Row, (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<JointPayee, JointPayee.curyJointAmountOwed>, JointPayee, object>) e).NewValue);
    else
      this.ValidatePayByDocument(e.Row, (Decimal?) ((Events.FieldVerifyingBase<Events.FieldVerifying<JointPayee, JointPayee.curyJointAmountOwed>, JointPayee, object>) e).NewValue);
  }

  private void ValidatePayByDocument(JointPayee row, Decimal? newValue)
  {
    Decimal num1 = 0M;
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      int? jointPayeeId1 = jointPayee.JointPayeeId;
      int? jointPayeeId2 = row.JointPayeeId;
      if (!(jointPayeeId1.GetValueOrDefault() == jointPayeeId2.GetValueOrDefault() & jointPayeeId1.HasValue == jointPayeeId2.HasValue))
        num1 += jointPayee.CuryJointBalance.GetValueOrDefault();
    }
    APInvoice originalBill = this.GetOriginalBill();
    PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) this.Base, originalBill.TermsID);
    Decimal? nullable;
    Decimal num2;
    if (terms == null)
    {
      num2 = 0M;
    }
    else
    {
      nullable = terms.DiscPercent;
      num2 = nullable.GetValueOrDefault();
    }
    Decimal num3 = num2;
    nullable = originalBill.CuryRetainageUnreleasedAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = originalBill.CuryRetainageUnreleasedAmt;
    Decimal num4 = nullable.GetValueOrDefault() * num3;
    Decimal num5 = valueOrDefault1 - num4;
    Decimal num6 = 0M;
    bool? released = originalBill.Released;
    if (released.GetValueOrDefault())
    {
      APInvoice retainageAggregate = this.GetRetainageAggregate(originalBill.DocType, originalBill.RefNbr);
      if (retainageAggregate != null)
      {
        nullable = retainageAggregate.CuryDocBal;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        nullable = retainageAggregate.CuryDiscBal;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        num6 = valueOrDefault2 - valueOrDefault3;
      }
    }
    released = originalBill.Released;
    Decimal valueOrDefault4;
    if (!released.GetValueOrDefault())
    {
      nullable = originalBill.CuryOrigDiscAmt;
      valueOrDefault4 = nullable.GetValueOrDefault();
    }
    else
    {
      nullable = originalBill.CuryDiscBal;
      valueOrDefault4 = nullable.GetValueOrDefault();
    }
    Decimal num7 = valueOrDefault4;
    Decimal totalForMainVendor = this.GetUnreleasedPaymentTotalForMainVendor(row);
    nullable = row.CuryJointAmountPaid;
    Decimal num8 = nullable.GetValueOrDefault() + this.GetUnreleasedPaymentTotalByJointPayee(row.JointPayeeId);
    nullable = row.CuryJointAmountPaid;
    Decimal valueOrDefault5 = nullable.GetValueOrDefault();
    nullable = originalBill.CuryDocBal;
    Decimal valueOrDefault6 = nullable.GetValueOrDefault();
    Decimal num9 = valueOrDefault5 + valueOrDefault6 - totalForMainVendor - num7 + num5 + num6 - num1;
    nullable = newValue;
    Decimal num10 = num9;
    if (!(nullable.GetValueOrDefault() > num10 & nullable.HasValue))
    {
      nullable = newValue;
      Decimal num11 = num8;
      if (!(nullable.GetValueOrDefault() < num11 & nullable.HasValue))
        return;
    }
    throw new PXSetPropertyException("The entered Joint Amount Owed is incorrect. It must be greater than or equal to {0} and less than or equal to {1}.", new object[2]
    {
      (object) num8.ToString("n2"),
      (object) num9.ToString("n2")
    });
  }

  private void ValidatePayByLine(JointPayee row, Decimal? newValue)
  {
    Decimal num1 = 0M;
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      int? nullable1 = jointPayee.APLineNbr;
      int? nullable2 = row.APLineNbr;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = jointPayee.JointPayeeId;
        nullable1 = row.JointPayeeId;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          num1 += jointPayee.CuryJointBalance.GetValueOrDefault();
      }
    }
    APInvoice originalBill = this.GetOriginalBill();
    APTran apTran = APTran.PK.Find((PXGraph) this.Base, row.APDocType, row.APRefNbr, row.APLineNbr);
    if (apTran != null)
    {
      Decimal num2 = 0M;
      Decimal? nullable3;
      if (originalBill.CuryLineTotal.GetValueOrDefault() != 0M)
      {
        nullable3 = apTran.CuryTranAmt;
        Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
        nullable3 = originalBill.CuryLineTotal;
        Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
        num2 = valueOrDefault1 / valueOrDefault2;
      }
      bool? released = apTran.Released;
      Decimal? nullable4 = released.GetValueOrDefault() ? apTran.CuryTranBal : apTran.CuryTranAmt;
      released = apTran.Released;
      Decimal? nullable5 = released.GetValueOrDefault() ? apTran.CuryRetainageBal : apTran.CuryRetainageAmt;
      released = apTran.Released;
      Decimal? nullable6;
      if (!released.GetValueOrDefault())
      {
        Decimal num3 = num2;
        nullable3 = originalBill.CuryOrigDiscAmt;
        nullable6 = nullable3.HasValue ? new Decimal?(num3 * nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable6 = apTran.CuryCashDiscBal;
      Decimal? nullable7 = nullable6;
      PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) this.Base, originalBill.TermsID);
      if (terms != null)
      {
        nullable3 = terms.DiscPercent;
        nullable3.GetValueOrDefault();
      }
      Decimal totalForMainVendor = this.GetUnreleasedPaymentTotalForMainVendor(row);
      nullable3 = row.CuryJointAmountPaid;
      Decimal num4 = nullable3.GetValueOrDefault() + this.GetUnreleasedPaymentTotalByJointPayee(row.JointPayeeId);
      nullable3 = row.CuryJointAmountPaid;
      Decimal num5 = nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault() - totalForMainVendor - nullable7.GetValueOrDefault() + nullable5.GetValueOrDefault() - num1;
      nullable3 = newValue;
      Decimal num6 = num5;
      if (!(nullable3.GetValueOrDefault() > num6 & nullable3.HasValue))
      {
        nullable3 = newValue;
        Decimal num7 = num4;
        if (!(nullable3.GetValueOrDefault() < num7 & nullable3.HasValue))
          return;
      }
      throw new PXSetPropertyException("The entered Joint Amount Owed is incorrect. It must be greater than or equal to {0} and less than or equal to {1}.", new object[2]
      {
        (object) num4.ToString("n2"),
        (object) num5.ToString("n2")
      });
    }
  }

  private APInvoice GetOriginalBill()
  {
    APInvoice current = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    if (current.IsRetainageDocument.GetValueOrDefault())
      current = APInvoice.PK.Find((PXGraph) this.Base, current.OrigDocType, current.OrigRefNbr);
    return current;
  }

  private APInvoice GetRetainageAggregate(string docType, string refNbr)
  {
    return PXResultset<APInvoice>.op_Implicit(((PXSelectBase<APInvoice>) new PXSelectGroupBy<APInvoice, Where<APInvoice.isRetainageDocument, Equal<True>, And<APInvoice.origDocType, Equal<Required<APInvoice.docType>>, And<APInvoice.origRefNbr, Equal<Required<APInvoice.refNbr>>>>>, Aggregate<Sum<APInvoice.curyDocBal, Sum<APInvoice.curyDiscBal>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) docType,
      (object) refNbr
    }));
  }

  /// <summary>
  /// Returns Sum of all unreleased payments for the current document and all related retainage bills.
  /// </summary>
  private Decimal GetUnreleasedPaymentTotal()
  {
    Decimal unreleasedPaymentTotal = 0M;
    if (((PXSelectBase<APInvoice>) this.Base.Document).Current.Released.GetValueOrDefault())
    {
      APAdjust apAdjust1 = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectGroupBy<APAdjust, Where<APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APAdjust.adjdRefNbr, Equal<Current<APInvoice.refNbr>>>>, Aggregate<Sum<APAdjust.curyAdjdAmt>>>((PXGraph) this.Base)).Select(Array.Empty<object>()));
      Decimal? curyAdjdAmt;
      if (apAdjust1 != null)
      {
        curyAdjdAmt = apAdjust1.CuryAdjdAmt;
        unreleasedPaymentTotal = curyAdjdAmt.GetValueOrDefault();
      }
      APAdjust apAdjust2 = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectJoinGroupBy<APAdjust, InnerJoin<APRegister, On<APAdjust.adjdDocType, Equal<APRegister.docType>, And<APAdjust.adjdRefNbr, Equal<APRegister.refNbr>>>>, Where<APRegister.origDocType, Equal<Current<APInvoice.docType>>, And<APRegister.origRefNbr, Equal<Current<APInvoice.refNbr>>, And<APRegister.isRetainageDocument, Equal<True>, And<APAdjust.released, Equal<False>>>>>, Aggregate<Sum<APAdjust.curyAdjdAmt>>>((PXGraph) this.Base)).Select(Array.Empty<object>()));
      if (apAdjust2 != null)
      {
        Decimal num = unreleasedPaymentTotal;
        curyAdjdAmt = apAdjust2.CuryAdjdAmt;
        Decimal valueOrDefault = curyAdjdAmt.GetValueOrDefault();
        unreleasedPaymentTotal = num + valueOrDefault;
      }
    }
    return unreleasedPaymentTotal;
  }

  private Decimal GetUnreleasedPaymentTotal(int? lineNbr)
  {
    Decimal unreleasedPaymentTotal = 0M;
    if (((PXSelectBase<APInvoice>) this.Base.Document).Current.Released.GetValueOrDefault())
    {
      APAdjust apAdjust1 = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectGroupBy<APAdjust, Where<APAdjust.adjdDocType, Equal<Current<APInvoice.docType>>, And<APAdjust.adjdRefNbr, Equal<Current<APInvoice.refNbr>>, And<APAdjust.adjdLineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>, Aggregate<Sum<APAdjust.curyAdjdAmt>>>((PXGraph) this.Base)).Select(new object[1]
      {
        (object) lineNbr
      }));
      Decimal? curyAdjdAmt;
      if (apAdjust1 != null)
      {
        curyAdjdAmt = apAdjust1.CuryAdjdAmt;
        unreleasedPaymentTotal = curyAdjdAmt.GetValueOrDefault();
      }
      APAdjust apAdjust2 = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectJoinGroupBy<APAdjust, InnerJoin<APRegister, On<APAdjust.adjdDocType, Equal<APRegister.docType>, And<APAdjust.adjdRefNbr, Equal<APRegister.refNbr>, And<APAdjust.adjdLineNbr, Equal<Required<APAdjust.adjdLineNbr>>>>>>, Where<APRegister.origDocType, Equal<Current<APInvoice.docType>>, And<APRegister.origRefNbr, Equal<Current<APInvoice.refNbr>>, And<APRegister.isRetainageDocument, Equal<True>, And<APAdjust.released, Equal<False>>>>>, Aggregate<Sum<APAdjust.curyAdjdAmt>>>((PXGraph) this.Base)).Select(new object[1]
      {
        (object) lineNbr
      }));
      if (apAdjust2 != null)
      {
        Decimal num = unreleasedPaymentTotal;
        curyAdjdAmt = apAdjust2.CuryAdjdAmt;
        Decimal valueOrDefault = curyAdjdAmt.GetValueOrDefault();
        unreleasedPaymentTotal = num + valueOrDefault;
      }
    }
    return unreleasedPaymentTotal;
  }

  private Decimal GetUnreleasedPaymentTotalByJointPayee(int? jointPayeeID)
  {
    APAdjust apAdjust = PXResultset<APAdjust>.op_Implicit(((PXSelectBase<APAdjust>) new PXSelectGroupBy<APAdjust, Where<APAdjust.jointPayeeID, Equal<Required<APAdjust.jointPayeeID>>, And<APAdjust.released, Equal<False>>>, Aggregate<Sum<APAdjust.curyAdjdAmt>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) jointPayeeID
    }));
    return apAdjust != null ? apAdjust.CuryAdjdAmt.GetValueOrDefault() : 0M;
  }

  private Decimal GetUnreleasedPaymentTotalForMainVendor(JointPayee jointPayee)
  {
    JointPayee jointPayee1;
    if (((PXSelectBase<APInvoice>) this.Base.Document).Current.PaymentsByLinesAllowed.GetValueOrDefault())
      jointPayee1 = ((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.aPLineNbr, Equal<Required<JointPayee.aPLineNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>>((PXGraph) this.Base)).SelectSingle(new object[3]
      {
        (object) jointPayee.APDocType,
        (object) jointPayee.APRefNbr,
        (object) jointPayee.APLineNbr
      });
    else
      jointPayee1 = ((PXSelectBase<JointPayee>) new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>((PXGraph) this.Base)).SelectSingle(new object[2]
      {
        (object) jointPayee.APDocType,
        (object) jointPayee.APRefNbr
      });
    return jointPayee1 != null ? this.GetUnreleasedPaymentTotalByJointPayee(jointPayee1.JointPayeeId) : 0M;
  }

  protected virtual void _(Events.RowSelected<APInvoice> e)
  {
    APInvoice row = e.Row;
    if (row == null)
      return;
    bool valueOrDefault = row.PaymentsByLinesAllowed.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<JointPayee.aPLineNbr>(((PXSelectBase) this.JointPayees).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<JointPayee.billLineAmount>(((PXSelectBase) this.JointPayees).Cache, (object) null, valueOrDefault);
    PXCache cache = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<APInvoice>>) e).Cache;
    APInvoice apInvoice = row;
    bool? nullable = row.OpenDoc;
    int num = !nullable.GetValueOrDefault() ? 0 : (((PXSelectBase<JointPayeePayment>) this.JointAmountApplications).Select(Array.Empty<object>()).Count == 0 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<APInvoice.isJointPayees>(cache, (object) apInvoice, num != 0);
    nullable = ((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldWarnOnBillEntry;
    if (!nullable.GetValueOrDefault() || !this.ContainsOutstandingLienWaversByVendor(row))
      return;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<APInvoice>>) e).Cache.RaiseExceptionHandling<APInvoice.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException<APInvoice.vendorID>("The vendor has at least one outstanding lien waiver.  ", (PXErrorLevel) 2));
  }

  protected virtual void _(Events.RowSelected<JointPayee> e)
  {
    if (e.Row == null || !e.Row.JointPayeeInternalId.HasValue || !((PXSelectBase<LienWaiverSetup>) this.lienWaiverSetup).Current.ShouldWarnOnBillEntry.GetValueOrDefault() || !this.ContainsOutstandingLienWaversByJointPayee(((PXSelectBase<APInvoice>) this.Base.Document).Current, e.Row.JointPayeeInternalId))
      return;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<JointPayee>>) e).Cache.RaiseExceptionHandling<JointPayee.jointPayeeInternalId>((object) e.Row, (object) e.Row.JointPayeeInternalId, (Exception) new PXSetPropertyException<JointPayee.jointPayeeInternalId>("The joint payee has at least one outstanding lien waiver.", (PXErrorLevel) 2));
  }

  protected virtual void _(Events.RowDeleted<APTran> e)
  {
    APInvoice current1 = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = current1.IsRetainageDocument;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 == 0)
      return;
    APInvoice current2 = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.PaymentsByLinesAllowed;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num2 == 0)
      return;
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      int? apLineNbr = jointPayee.APLineNbr;
      int? lineNbr = e.Row.LineNbr;
      if (apLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & apLineNbr.HasValue == lineNbr.HasValue)
        ((PXSelectBase<JointPayee>) this.JointPayees).Delete(jointPayee);
    }
  }

  protected virtual void _(Events.RowPersisting<JointPayee> e)
  {
    if (e.Row == null || e.Operation == 3 || e.Row.APLineNbr.HasValue)
      return;
    APInvoice current = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    if ((current != null ? (current.PaymentsByLinesAllowed.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<JointPayee>>) e).Cache.RaiseExceptionHandling<JointPayee.aPLineNbr>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[aPLineNbr]"
    }));
  }

  private void DeleteAllPayees()
  {
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
      ((PXSelectBase<JointPayee>) this.JointPayees).Delete(PXResult<JointPayee>.op_Implicit(pxResult));
  }

  private bool IsJointPayee(APInvoice doc) => doc != null && doc.IsJointPayees.GetValueOrDefault();

  private void RecalculateAmountOwed()
  {
    PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>> pxSelect = new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>((PXGraph) this.Base);
    APInvoice current = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    if (current.IsRetainageDocument.GetValueOrDefault())
      current = APInvoice.PK.Find((PXGraph) this.Base, current.OrigDocType, current.OrigRefNbr);
    Decimal num1 = 0M;
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      num1 += jointPayee.CuryJointAmountOwed.GetValueOrDefault();
    }
    Decimal num2 = Math.Max(0M, current.CuryOrigDocAmtWithRetainageTotal.GetValueOrDefault() - num1);
    JointPayee jointPayee1 = PXResultset<JointPayee>.op_Implicit(((PXSelectBase<JointPayee>) pxSelect).Select(new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    }));
    if (jointPayee1 != null)
    {
      Decimal? nullable = jointPayee1.CuryJointAmountPaid;
      if (nullable.HasValue)
      {
        nullable = jointPayee1.CuryJointBalance;
        if (nullable.HasValue)
          goto label_13;
      }
      jointPayee1.CuryJointAmountPaid = new Decimal?(0M);
      jointPayee1.CuryJointBalance = new Decimal?(0M);
      jointPayee1 = ((PXSelectBase<JointPayee>) this.JointPayees).Update(jointPayee1);
label_13:
      jointPayee1.CuryJointAmountOwed = new Decimal?(num2);
      ((PXSelectBase<JointPayee>) this.JointPayees).Update(jointPayee1);
    }
    else
      ((PXSelectBase<JointPayee>) this.JointPayees).Insert(new JointPayee()
      {
        IsMainPayee = new bool?(true),
        APDocType = current.DocType,
        APRefNbr = current.RefNbr,
        JointPayeeInternalId = ((PXSelectBase<APInvoice>) this.Base.Document).Current.VendorID,
        CuryJointAmountOwed = new Decimal?(num2)
      });
  }

  private void RecalculateAmountOwedByLine()
  {
    PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>> pxSelect = new PXSelect<JointPayee, Where<JointPayee.aPDocType, Equal<Required<JointPayee.aPDocType>>, And<JointPayee.aPRefNbr, Equal<Required<JointPayee.aPRefNbr>>, And<JointPayee.isMainPayee, Equal<True>>>>>((PXGraph) this.Base);
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) this.JointPayees).Select(Array.Empty<object>()))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      Dictionary<int, Decimal> dictionary2 = dictionary1;
      int? apLineNbr = jointPayee.APLineNbr;
      int valueOrDefault1 = apLineNbr.GetValueOrDefault();
      if (!dictionary2.ContainsKey(valueOrDefault1))
      {
        Dictionary<int, Decimal> dictionary3 = dictionary1;
        apLineNbr = jointPayee.APLineNbr;
        int valueOrDefault2 = apLineNbr.GetValueOrDefault();
        dictionary3.Add(valueOrDefault2, 0M);
      }
      Dictionary<int, Decimal> dictionary4 = dictionary1;
      apLineNbr = jointPayee.APLineNbr;
      int valueOrDefault3 = apLineNbr.GetValueOrDefault();
      dictionary4[valueOrDefault3] += jointPayee.CuryJointAmountOwed.GetValueOrDefault();
    }
    Dictionary<int, JointPayee> dictionary5 = new Dictionary<int, JointPayee>();
    APInvoice current = ((PXSelectBase<APInvoice>) this.Base.Document).Current;
    if (current.IsRetainageDocument.GetValueOrDefault())
      current = APInvoice.PK.Find((PXGraph) this.Base, current.OrigDocType, current.OrigRefNbr);
    foreach (PXResult<JointPayee> pxResult in ((PXSelectBase<JointPayee>) pxSelect).Select(new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    }))
    {
      JointPayee jointPayee = PXResult<JointPayee>.op_Implicit(pxResult);
      dictionary5.Add(jointPayee.APLineNbr.GetValueOrDefault(), jointPayee);
    }
    Dictionary<int?, Decimal> dictionary6 = GraphHelper.RowCast<APTax>((IEnumerable) ((PXSelectBase) this.Base.Tax_Rows).View.SelectMultiBound(new object[1]
    {
      (object) current
    }, Array.Empty<object>())).GroupBy<APTax, int?>((Func<APTax, int?>) (t => t.LineNbr)).ToDictionary<IGrouping<int?, APTax>, int?, Decimal>((Func<IGrouping<int?, APTax>, int?>) (g => g.Key), (Func<IGrouping<int?, APTax>, Decimal>) (g => g.Sum<APTax>((Func<APTax, Decimal>) (t => t.CuryTaxAmt.GetValueOrDefault()))));
    foreach (APTran apTran in ((PXSelectBase) this.Base.Transactions).View.SelectMultiBound(new object[1]
    {
      (object) current
    }, Array.Empty<object>()))
    {
      Dictionary<int, Decimal> dictionary7 = dictionary1;
      int? lineNbr = apTran.LineNbr;
      int key1 = lineNbr.Value;
      Decimal num1;
      ref Decimal local1 = ref num1;
      dictionary7.TryGetValue(key1, out local1);
      Dictionary<int?, Decimal> dictionary8 = dictionary6;
      lineNbr = apTran.LineNbr;
      int? key2 = new int?(lineNbr.Value);
      Decimal num2;
      ref Decimal local2 = ref num2;
      dictionary8.TryGetValue(key2, out local2);
      Decimal? nullable = apTran.CuryTranAmt;
      Decimal num3 = nullable.GetValueOrDefault() + num2;
      nullable = apTran.CuryRetainageAmt;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal num4 = Math.Max(0M, num3 + valueOrDefault - num1);
      Dictionary<int, JointPayee> dictionary9 = dictionary5;
      lineNbr = apTran.LineNbr;
      int key3 = lineNbr.Value;
      JointPayee jointPayee;
      ref JointPayee local3 = ref jointPayee;
      if (dictionary9.TryGetValue(key3, out local3))
      {
        jointPayee.CuryJointAmountOwed = new Decimal?(num4);
        ((PXSelectBase<JointPayee>) this.JointPayees).Update(jointPayee);
      }
      else
        jointPayee = ((PXSelectBase<JointPayee>) this.JointPayees).Insert(new JointPayee()
        {
          IsMainPayee = new bool?(true),
          JointPayeeInternalId = apTran.VendorID,
          APDocType = current.DocType,
          APRefNbr = current.RefNbr,
          APLineNbr = apTran.LineNbr,
          CuryJointAmountOwed = new Decimal?(num4)
        });
    }
  }

  private int? LienWaiverDocType
  {
    get
    {
      if (!this.lienWaiverDocType.HasValue)
        this.lienWaiverDocType = this.GetLienWaiverDocumentType();
      return this.lienWaiverDocType;
    }
  }

  private int? GetLienWaiverDocumentType()
  {
    return ((PXSelectBase<ComplianceAttributeType>) new PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.type, Equal<Required<ComplianceAttributeType.type>>>>((PXGraph) this.Base)).SelectSingle(new object[1]
    {
      (object) "Lien Waiver"
    })?.ComplianceAttributeTypeID;
  }

  private bool ContainsOutstandingLienWaversByVendor(APInvoice invoice)
  {
    if (!invoice.HasMultipleProjects.GetValueOrDefault())
    {
      if (PXResultset<ComplianceDocument>.op_Implicit(((PXSelectBase<ComplianceDocument>) new PXSelectReadonly<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[4]
      {
        (object) this.LienWaiverDocType,
        (object) invoice.VendorID,
        (object) invoice.ProjectID,
        (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
      })) != null)
        return true;
    }
    else
    {
      string str1 = invoice.IsRetainageDocument.GetValueOrDefault() ? invoice.OrigDocType : invoice.DocType;
      string str2 = invoice.IsRetainageDocument.GetValueOrDefault() ? invoice.OrigRefNbr : invoice.RefNbr;
      if (PXResultset<APTran>.op_Implicit(((PXSelectBase<APTran>) new PXSelectReadonly2<APTran, InnerJoin<ComplianceDocument, On<ComplianceDocument.projectID, Equal<APTran.projectID>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[5]
      {
        (object) invoice.VendorID,
        (object) this.LienWaiverDocType,
        (object) ((PXGraph) this.Base).Accessinfo.BusinessDate,
        (object) str1,
        (object) str2
      })) != null)
        return true;
    }
    return false;
  }

  private bool ContainsOutstandingLienWaversByJointPayee(APInvoice invoice, int? vendorID)
  {
    if (!invoice.HasMultipleProjects.GetValueOrDefault())
    {
      if (PXResultset<ComplianceDocument>.op_Implicit(((PXSelectBase<ComplianceDocument>) new PXSelectReadonly<ComplianceDocument, Where<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.jointVendorInternalId, Equal<Required<ComplianceDocument.jointVendorInternalId>>, And<ComplianceDocument.projectID, Equal<Required<ComplianceDocument.projectID>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[5]
      {
        (object) this.LienWaiverDocType,
        (object) invoice.VendorID,
        (object) vendorID,
        (object) invoice.ProjectID,
        (object) ((PXGraph) this.Base).Accessinfo.BusinessDate
      })) != null)
        return true;
    }
    else
    {
      string str1 = invoice.IsRetainageDocument.GetValueOrDefault() ? invoice.OrigDocType : invoice.DocType;
      string str2 = invoice.IsRetainageDocument.GetValueOrDefault() ? invoice.OrigRefNbr : invoice.RefNbr;
      if (PXResultset<APTran>.op_Implicit(((PXSelectBase<APTran>) new PXSelectReadonly2<APTran, InnerJoin<ComplianceDocument, On<ComplianceDocument.projectID, Equal<APTran.projectID>, And<ComplianceDocument.vendorID, Equal<Required<ComplianceDocument.vendorID>>, And<ComplianceDocument.jointVendorInternalId, Equal<Required<ComplianceDocument.jointVendorInternalId>>, And<ComplianceDocument.documentType, Equal<Required<ComplianceDocument.documentType>>, And<ComplianceDocument.throughDate, Less<Required<ComplianceDocument.throughDate>>, And<ComplianceDocument.received, NotEqual<True>>>>>>>>, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[6]
      {
        (object) invoice.VendorID,
        (object) vendorID,
        (object) this.LienWaiverDocType,
        (object) ((PXGraph) this.Base).Accessinfo.BusinessDate,
        (object) str1,
        (object) str2
      })) != null)
        return true;
    }
    return false;
  }

  private class JointPayment
  {
    public Decimal Original;
    public Decimal Retainage;
  }
}
