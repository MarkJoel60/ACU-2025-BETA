// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_SOInvoiceEntryCorrectionExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.FS.DAC;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class SM_SOInvoiceEntryCorrectionExt : PXGraphExtension<Correction, SOInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOInvoiceEntry.ReleaseInvoiceProc(System.Collections.Generic.List{PX.Objects.AR.ARRegister},System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseInvoiceProc(
    List<PX.Objects.AR.ARRegister> list,
    bool isMassProcess,
    Action<List<PX.Objects.AR.ARRegister>, bool> baseMethod)
  {
    foreach (PX.Objects.AR.ARInvoice arInvoice in list.OfType<PX.Objects.AR.ARInvoice>().Where<PX.Objects.AR.ARInvoice>((Func<PX.Objects.AR.ARInvoice, bool>) (d => d.DocType == "CRM")))
    {
      List<ARTran> list1 = ((PXSelectBase) ((PXGraphExtension<SOInvoiceEntry>) this).Base.Transactions).View.SelectMultiBound(new object[1]
      {
        (object) arInvoice
      }, Array.Empty<object>()).Cast<ARTran>().Where<ARTran>((Func<ARTran, bool>) (arTran => arTran.OrigInvoiceLineNbr.HasValue && arTran.OrigInvoiceNbr != null && arTran.OrigInvoiceType != null)).ToList<ARTran>();
      if (list1.Any<ARTran>((Func<ARTran, bool>) (arTran => ((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base).Caches[typeof (ARTran)].GetExtension<FSxARTran>((object) arTran).IsFSRelated.GetValueOrDefault())))
      {
        bool flag = true;
        HashSet<SM_SOInvoiceEntryCorrectionExt.ARTranKey> arTranKeySet = new HashSet<SM_SOInvoiceEntryCorrectionExt.ARTranKey>();
        foreach (ARTran arTran in list1)
        {
          IEnumerable<SM_SOInvoiceEntryCorrectionExt.ARTranKey> arTranKeys = ((IEnumerable<PXResult<ARTran>>) PXSelectBase<ARTran, PXViewOf<ARTran>.BasedOn<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base, new object[2]
          {
            (object) arTran.OrigInvoiceType,
            (object) arTran.OrigInvoiceNbr
          })).AsEnumerable<PXResult<ARTran>>().Select<PXResult<ARTran>, SM_SOInvoiceEntryCorrectionExt.ARTranKey>((Func<PXResult<ARTran>, SM_SOInvoiceEntryCorrectionExt.ARTranKey>) (r => SM_SOInvoiceEntryCorrectionExt.ARTranKey.FromTran(((PXResult) r).GetItem<ARTran>())));
          EnumerableExtensions.AddRange<SM_SOInvoiceEntryCorrectionExt.ARTranKey>((ISet<SM_SOInvoiceEntryCorrectionExt.ARTranKey>) arTranKeySet, arTranKeys);
        }
        foreach (ARTran aRTran in list1)
        {
          SM_SOInvoiceEntryCorrectionExt.ARTranKey arTranKey = SM_SOInvoiceEntryCorrectionExt.ARTranKey.FromOrigin(aRTran);
          flag &= arTranKeySet.Remove(arTranKey);
        }
        if (!(flag & arTranKeySet.Count < 1))
          throw new PXException("The {0} credit memo cannot be released because it does not contain all lines of the related service order or appointment. Please delete the credit memo and create the new one by using the Reverse Service Invoice command in the original invoice.", new object[2]
          {
            (object) arInvoice.RefNbr,
            (object) arInvoice.OrigRefNbr
          });
      }
      ((PXSelectBase) ((PXGraphExtension<SOInvoiceEntry>) this).Base.Transactions).Cache.Clear();
    }
    baseMethod(list, isMassProcess);
  }

  [PXOverride]
  public virtual void SetupCorrectActionsState(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e,
    bool isVisible,
    bool isEnabled,
    SM_SOInvoiceEntryCorrectionExt.SetupFunc baseMethod)
  {
    baseMethod(e, isVisible, isEnabled);
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>();
    bool flag2 = this.AnyFieldServiceLine();
    bool flag3 = false;
    if (flag2)
      flag3 = e.Row.DocType == "INV" && this.AnyRelatedCreditMemo(e.Row);
    ((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base).Actions["Corrections Category"]?.SetVisible("ReverseDirectInvoice", isVisible & flag1 & flag2);
    ((PXAction) this.Base1.cancelInvoice).SetEnabled(isEnabled && (!flag2 || !flag1));
    ((PXAction) this.Base1.correctInvoice).SetEnabled(isEnabled && !flag2);
    ((PXAction) this.Base1.reverseDirectInvoice).SetEnabled(isEnabled & flag2 && !flag3);
  }

  [PXOverride]
  public virtual ARInvoiceState GetDocumentState(
    PXCache cache,
    PX.Objects.AR.ARInvoice doc,
    SM_SOInvoiceEntryCorrectionExt.GetDocumentStateFunc baseMethod)
  {
    ARInvoiceState documentState = baseMethod(cache, doc);
    bool flag = doc.DocType == "CRM" && doc.OrigRefNbr != null && this.AnyFieldServiceLine();
    documentState.AllowDeleteTransactions &= !flag;
    documentState.AllowInsertTransactions &= !flag;
    return documentState;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<ARTran> e)
  {
    ARTran row = e.Row;
    if (row == null)
      return;
    PX.Objects.AR.ARInvoice current = ((PXSelectBase<PX.Objects.AR.ARInvoice>) ((PXGraphExtension<SOInvoiceEntry>) this).Base.Document).Current;
    if (current == null)
      return;
    string docType = current.DocType;
    if (!(docType != null ? new bool?(EnumerableExtensions.IsIn<string>(docType, "CRM", "INV")) : new bool?()).GetValueOrDefault() || ((PXSelectBase) ((PXGraphExtension<SOInvoiceEntry>) this).Base.Document).Cache.GetStatus((object) current) == 3)
      return;
    FSxARTran extension = ((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base).Caches[typeof (ARTran)].GetExtension<FSxARTran>((object) row);
    if (extension.AppointmentRefNbr != null || extension.ServiceOrderRefNbr != null || extension.ServiceContractRefNbr != null)
      throw new PXException("The line cannot be deleted because it is linked to an appointment or a service order. To proceed, ensure that all lines of the related appointment or service order are added to the invoice.");
  }

  private bool AnyFieldServiceLine()
  {
    return GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) ((PXGraphExtension<SOInvoiceEntry>) this).Base.Transactions).Select(Array.Empty<object>())).Any<ARTran>((Func<ARTran, bool>) (tr => ((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base).Caches[typeof (ARTran)].GetExtension<FSxARTran>((object) tr).IsFSRelated.GetValueOrDefault()));
  }

  private bool AnyRelatedCreditMemo(PX.Objects.AR.ARInvoice invoice)
  {
    return PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origDocType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.IsEqual<P.AsString>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<SOInvoiceEntry>) this).Base, (object[]) null, new object[2]
    {
      (object) invoice.DocType,
      (object) invoice.RefNbr
    }).RowCount.GetValueOrDefault() > 0;
  }

  public delegate void SetupFunc(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e, bool isVisible, bool isEnabled);

  public delegate IEnumerable ReverseDirectInvoiceFunc(PXAdapter adapter);

  public delegate ARInvoiceState GetDocumentStateFunc(PXCache cache, PX.Objects.AR.ARInvoice doc);

  public delegate void ReleaseInvoiceFunc(List<PX.Objects.AR.ARRegister> list, bool isMassProcess);

  private class ARTranKey
  {
    public string RefNbr { get; }

    public string TranType { get; }

    public int? LineNbr { get; }

    private ARTranKey(string refNbr, string tranType, int? lineNbr)
    {
      this.RefNbr = refNbr;
      this.TranType = tranType;
      this.LineNbr = lineNbr;
    }

    public static SM_SOInvoiceEntryCorrectionExt.ARTranKey FromTran(ARTran aRTran)
    {
      return new SM_SOInvoiceEntryCorrectionExt.ARTranKey(aRTran.RefNbr, aRTran.TranType, aRTran.LineNbr);
    }

    public static SM_SOInvoiceEntryCorrectionExt.ARTranKey FromOrigin(ARTran aRTran)
    {
      return new SM_SOInvoiceEntryCorrectionExt.ARTranKey(aRTran.OrigInvoiceNbr, aRTran.OrigInvoiceType, aRTran.OrigInvoiceLineNbr);
    }

    public override bool Equals(object obj)
    {
      if (this == obj)
        return true;
      if (!(obj is SM_SOInvoiceEntryCorrectionExt.ARTranKey arTranKey) || !(this.RefNbr == arTranKey.RefNbr) || !(this.TranType == arTranKey.TranType))
        return false;
      int? lineNbr1 = this.LineNbr;
      int? lineNbr2 = arTranKey.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    }

    public override int GetHashCode()
    {
      return this.RefNbr.GetHashCode() ^ this.TranType.GetHashCode() ^ this.LineNbr.GetHashCode();
    }
  }
}
