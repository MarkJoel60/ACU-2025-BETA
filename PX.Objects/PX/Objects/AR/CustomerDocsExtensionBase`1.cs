// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerDocsExtensionBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

public abstract class CustomerDocsExtensionBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual PXResultset<ARInvoice> GetCustDocs(
    ARPaymentEntry.LoadOptions opts,
    ARPayment currentARPayment,
    ARSetup currentARSetup)
  {
    ARPaymentEntry.LoadOptions loadOptions1 = opts;
    int? maxDocs;
    int num1;
    if (loadOptions1 == null)
    {
      num1 = 0;
    }
    else
    {
      maxDocs = loadOptions1.MaxDocs;
      int num2 = 0;
      num1 = maxDocs.GetValueOrDefault() == num2 & maxDocs.HasValue ? 1 : 0;
    }
    if (num1 != 0 || currentARPayment == null)
      return new PXResultset<ARInvoice>();
    PXSelectBase<ARInvoice> cmd = (PXSelectBase<ARInvoice>) new PXSelectReadonly2<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.curyTranBal, NotEqual<decimal0>>>>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<Where<ARAdjust.adjgDocType, NotEqual<Required<ARRegister.docType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Required<ARRegister.refNbr>>>>>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>>>>>>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, NotEqual<P.AsString.ASCII>>>>, And<BqlOperand<ARInvoice.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARInvoice.openDoc, IBqlBool>.IsEqual<True>>>, And<BqlOperand<ARInvoice.hold, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsNull>>, And<BqlOperand<ARAdjust2.adjgRefNbr, IBqlString>.IsNull>>, And<BqlOperand<ARInvoice.pendingPPD, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<ARRegister.isUnderCorrection, IBqlBool>.IsNotEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>>>>>.Or<BqlOperand<ARInvoice.pendingPayment, IBqlBool>.IsEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.paymentsByLinesAllowed, NotEqual<True>>>>>.Or<BqlOperand<ARTran.refNbr, IBqlString>.IsNotNull>>>, OrderBy<Asc<ARInvoice.dueDate, Asc<ARInvoice.refNbr, Asc<ARTran.refNbr>>>>>((PXGraph) this.Base);
    object[] array = this.AddParametersFromOptions(opts, currentARPayment, cmd).ToArray<object>();
    ARPaymentEntry.LoadOptions loadOptions2 = opts;
    int num3;
    if (loadOptions2 == null)
    {
      num3 = 1;
    }
    else
    {
      maxDocs = loadOptions2.MaxDocs;
      num3 = !maxDocs.HasValue ? 1 : 0;
    }
    PXResultset<ARInvoice> custDocs;
    if (num3 == 0)
    {
      PXSelectBase<ARInvoice> pxSelectBase = cmd;
      maxDocs = opts.MaxDocs;
      int num4 = maxDocs.Value;
      object[] objArray = array;
      custDocs = pxSelectBase.SelectWindowed(0, num4, objArray);
    }
    else
      custDocs = cmd.Select(array);
    custDocs.Sort((Comparison<PXResult<ARInvoice>>) ((a, b) => ARPaymentEntry.CompareCustDocs(currentARPayment, currentARSetup, opts, PXResult.Unwrap<ARInvoice>((object) a), PXResult.Unwrap<ARInvoice>((object) b), PXResult.Unwrap<ARTran>((object) a), PXResult.Unwrap<ARTran>((object) b))));
    return custDocs;
  }

  public virtual int GetCustDocsCount(
    ARPaymentEntry.LoadOptions opts,
    ARPayment currentARPayment,
    ARSetup currentARSetup)
  {
    PXSelectBase<ARInvoice> cmd = (PXSelectBase<ARInvoice>) new PXSelectJoinGroupBy<ARInvoice, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<ARInvoice.curyInfoID>>, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, LeftJoin<ARTran, On<ARInvoice.paymentsByLinesAllowed, Equal<True>, And<ARTran.tranType, Equal<ARInvoice.docType>, And<ARTran.refNbr, Equal<ARInvoice.refNbr>, And<ARTran.curyTranBal, NotEqual<decimal0>>>>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>, And<Where<ARAdjust.adjgDocType, NotEqual<Required<ARRegister.docType>>, Or<ARAdjust.adjgRefNbr, NotEqual<Required<ARRegister.refNbr>>>>>>>>>, LeftJoin<ARAdjust2, On<ARAdjust2.adjgDocType, Equal<ARInvoice.docType>, And<ARAdjust2.adjgRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust2.released, NotEqual<True>, And<ARAdjust2.voided, NotEqual<True>>>>>>>>>>, Where<ARInvoice.docType, NotEqual<Required<ARPayment.docType>>, And<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARAdjust.adjgRefNbr, IsNull, And<ARAdjust2.adjgRefNbr, IsNull, And<ARInvoice.pendingPPD, NotEqual<True>, And<ARRegister.isUnderCorrection, NotEqual<True>>>>>>>>, Aggregate<Count>>((PXGraph) this.Base);
    object[] array = this.AddParametersFromOptions(opts, currentARPayment, cmd).ToArray<object>();
    using (new PXFieldScope(((PXSelectBase) cmd).View, new Type[3]
    {
      typeof (ARInvoice.docType),
      typeof (ARInvoice.refNbr),
      typeof (ARTran.lineNbr)
    }))
      return cmd.Select(array).RowCount.GetValueOrDefault();
  }

  public virtual IEnumerable<object> AddParametersFromOptions(
    ARPaymentEntry.LoadOptions opts,
    ARPayment currentARPayment,
    PXSelectBase<ARInvoice> cmd)
  {
    CustomerDocsExtensionBase<TGraph> docsExtensionBase = this;
    yield return (object) currentARPayment.DocType;
    yield return (object) currentARPayment.RefNbr;
    yield return (object) currentARPayment.DocType;
    ARPaymentEntry.LoadOptions loadOptions1 = opts;
    DateTime? nullable1;
    int num1;
    if (loadOptions1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = loadOptions1.FromDate;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      cmd.WhereAnd<Where<ARInvoice.docDate, GreaterEqual<Required<ARPaymentEntry.LoadOptions.fromDate>>>>();
      yield return (object) opts.FromDate;
    }
    ARPaymentEntry.LoadOptions loadOptions2 = opts;
    int num2;
    if (loadOptions2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable1 = loadOptions2.TillDate;
      num2 = nullable1.HasValue ? 1 : 0;
    }
    if (num2 != 0)
    {
      cmd.WhereAnd<Where<ARInvoice.docDate, LessEqual<Required<ARPaymentEntry.LoadOptions.tillDate>>>>();
      yield return (object) opts.TillDate;
    }
    if (!string.IsNullOrEmpty(opts?.StartRefNbr))
    {
      cmd.WhereAnd<Where<ARInvoice.refNbr, GreaterEqual<Required<ARPaymentEntry.LoadOptions.startRefNbr>>>>();
      yield return (object) opts.StartRefNbr;
    }
    if (!string.IsNullOrEmpty(opts?.EndRefNbr))
    {
      cmd.WhereAnd<Where<ARInvoice.refNbr, LessEqual<Required<ARPaymentEntry.LoadOptions.endRefNbr>>>>();
      yield return (object) opts.EndRefNbr;
    }
    ARPaymentEntry.LoadOptions loadOptions3 = opts;
    int? nullable2;
    int num3;
    if (loadOptions3 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable2 = loadOptions3.BranchID;
      num3 = nullable2.HasValue ? 1 : 0;
    }
    if (num3 != 0)
    {
      cmd.WhereAnd<Where<ARInvoice.branchID, Equal<Required<ARPaymentEntry.LoadOptions.branchID>>>>();
      yield return (object) opts.BranchID;
    }
    else
    {
      ARPaymentEntry.LoadOptions loadOptions4 = opts;
      int num4;
      if (loadOptions4 == null)
      {
        num4 = 0;
      }
      else
      {
        nullable2 = loadOptions4.OrganizationID;
        num4 = nullable2.HasValue ? 1 : 0;
      }
      if (num4 != 0)
      {
        cmd.WhereAnd<Where<ARInvoice.branchID, In<Required<ARPaymentEntry.LoadOptions.branchID>>>>();
        yield return (object) BranchMaint.GetChildBranches((PXGraph) docsExtensionBase.Base, opts.OrganizationID).Select<PX.Objects.GL.Branch, int>((Func<PX.Objects.GL.Branch, int>) (o => o.BranchID.Value)).ToArray<int>();
      }
    }
    string str = opts == null ? "NOONE" : opts.LoadChildDocuments;
    switch (str)
    {
      case "INCRM":
        cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>, Or<Customer.consolidatingBAccountID, Equal<Required<ARRegister.customerID>>>>>();
        break;
      case "EXCRM":
        cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>, Or<Customer.consolidatingBAccountID, Equal<Required<ARRegister.customerID>>, And<ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>>();
        break;
      default:
        cmd.WhereAnd<Where<ARInvoice.customerID, Equal<Required<ARRegister.customerID>>>>();
        break;
    }
    if (str == "INCRM" || str == "EXCRM")
    {
      yield return (object) currentARPayment.CustomerID;
      yield return (object) currentARPayment.CustomerID;
    }
    else
      yield return (object) currentARPayment.CustomerID;
    switch (currentARPayment.DocType)
    {
      case "PMT":
        cmd.WhereAnd<Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<ARInvoice.docType, Equal<ARDocType.creditMemo>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>>>>>>>();
        break;
      case "PPM":
        cmd.WhereAnd<Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.prepaymentInvoice>, Or<ARInvoice.docType, Equal<ARDocType.creditMemo>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>>>>>>>();
        break;
      case "CRM":
        cmd.WhereAnd<Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>>>>>();
        break;
      default:
        cmd.WhereAnd<Where<True, Equal<False>>>();
        break;
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.interBranch>() && currentARPayment.BranchID.HasValue)
    {
      cmd.WhereAnd<Where<ARInvoice.branchID, In<Required<ARInvoice.branchID>>>>();
      yield return (object) BranchHelper.GetBranchesToApplyDocuments((PXGraph) docsExtensionBase.Base, currentARPayment.BranchID);
    }
  }
}
