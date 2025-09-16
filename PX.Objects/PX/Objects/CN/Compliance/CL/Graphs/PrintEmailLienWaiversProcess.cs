// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Graphs.PrintEmailLienWaiversProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Descriptor;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.CN.Subcontracts.SC.DAC;
using PX.Objects.CS;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.Graphs;

public class PrintEmailLienWaiversProcess : PXGraph<
#nullable disable
PrintEmailLienWaiversProcess>
{
  public PXCancel<ProcessLienWaiversFilter> Cancel;
  public PXFilter<ProcessLienWaiversFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<ComplianceDocument, ProcessLienWaiversFilter, LeftJoin<ComplianceAttribute, On<BqlOperand<
  #nullable enable
  ComplianceDocument.documentTypeValue, IBqlInt>.IsEqual<
  #nullable disable
  ComplianceAttribute.attributeId>>, InnerJoin<ComplianceAttributeType, On<BqlOperand<
  #nullable enable
  ComplianceDocument.documentType, IBqlInt>.IsEqual<
  #nullable disable
  ComplianceAttributeType.complianceAttributeTypeID>>, LeftJoin<PrintEmailLienWaiversProcess.ComplianceDocumentAPDocumentReference, On<BqlOperand<
  #nullable enable
  ComplianceDocument.billID, IBqlGuid>.IsEqual<
  #nullable disable
  PrintEmailLienWaiversProcess.ComplianceDocumentAPDocumentReference.complianceDocumentReferenceId>>, LeftJoin<PrintEmailLienWaiversProcess.ComplianceDocumentAPPaymentReference, On<BqlOperand<
  #nullable enable
  ComplianceDocument.apCheckId, IBqlGuid>.IsEqual<
  #nullable disable
  PrintEmailLienWaiversProcess.ComplianceDocumentAPPaymentReference.complianceDocumentReferenceId>>, LeftJoin<PMProject, On<ComplianceDocument.projectID, Equal<PMProject.contractID>>, LeftJoin<Subcontract, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  Subcontract.orderType, 
  #nullable disable
  Equal<POOrderType.regularSubcontract>>>>>.And<BqlOperand<
  #nullable enable
  Subcontract.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  ComplianceDocument.subcontract>>>, LeftJoin<ComplianceDocumentReference, On<BqlOperand<
  #nullable enable
  ComplianceDocumentReference.complianceDocumentReferenceId, IBqlGuid>.IsEqual<
  #nullable disable
  ComplianceDocument.purchaseOrder>>, LeftJoin<PX.Objects.PO.POOrder, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.PO.POOrder.orderType, 
  #nullable disable
  Equal<ComplianceDocumentReference.type>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.PO.POOrder.orderNbr, IBqlString>.IsEqual<
  #nullable disable
  ComplianceDocumentReference.referenceNumber>>>>>>>>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceAttributeType.type, 
  #nullable disable
  Equal<ComplianceDocumentType.lienWaiver>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ProcessLienWaiversFilter.projectId, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.projectId>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.vendorID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ProcessLienWaiversFilter.vendorId, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.vendorId>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceAttribute.value, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  ProcessLienWaiversFilter.lienWaiverType, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  Or<BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.lienWaiverType>, IBqlString>.IsNull>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.lienWaiverType>, IBqlString>.IsEqual<
  #nullable disable
  Constants.LienWaiverDocumentTypeValues.all>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.creationDate, 
  #nullable disable
  GreaterEqual<BqlField<
  #nullable enable
  ProcessLienWaiversFilter.startDate, IBqlDateTime>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.startDate>, IBqlDateTime>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.creationDate, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  ProcessLienWaiversFilter.endDate, IBqlDateTime>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.endDate>, IBqlDateTime>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  ComplianceDocument.isProcessed, 
  #nullable disable
  Equal<False>>>>, Or<BqlOperand<
  #nullable enable
  ComplianceDocument.isProcessed, IBqlBool>.IsNull>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  ProcessLienWaiversFilter.shouldShowProcessed>, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.contractID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>>> LienWaivers;

  [InjectDependency]
  internal IPrintLienWaiversService PrintLienWaiversService { get; set; }

  [InjectDependency]
  internal IEmailLienWaiverService EmailLienWaiverService { get; set; }

  protected virtual void _(PX.Data.Events.RowInserted<ProcessLienWaiversFilter> args)
  {
    ProcessLienWaiversFilter row = args.Row;
    if (row == null)
      return;
    row.StartDate = ((PXGraph) this).Accessinfo.BusinessDate;
    row.EndDate = ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void _(PX.Data.Events.RowSelected<ProcessLienWaiversFilter> args)
  {
    ProcessLienWaiversFilter row = args.Row;
    if (row == null || row.Action == null)
      return;
    this.InitializeProcessDelegate(row.Action);
    PrintEmailLienWaiversProcess.SetPrintSettingFieldsVisibility(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ProcessLienWaiversFilter>>) args).Cache, row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceDocument> args)
  {
    ComplianceDocument row = args.Row;
    if (row == null || this.PrintLienWaiversService.IsLienWaiverValid(row))
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ComplianceDocument>>) args).Cache.RaiseException<ComplianceDocument.documentTypeValue>((object) row, "The lien waiver cannot be processed, because at least one of the following values is not specified in its settings: Document Category, Vendor, or Project.", errorLevel: (PXErrorLevel) 3);
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Subcontract Total")]
  protected virtual void _(PX.Data.Events.CacheAttached<Subcontract.orderTotal> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "PO Total")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.PO.POOrder.orderTotal> e)
  {
  }

  private void InitializeProcessDelegate(string action)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PrintEmailLienWaiversProcess.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new PrintEmailLienWaiversProcess.\u003C\u003Ec__DisplayClass17_0();
    switch (action)
    {
      case "Print Lien Waivers":
        ((PXProcessingBase<ComplianceDocument>) this.LienWaivers).SetAsyncProcessDelegate((Func<List<ComplianceDocument>, CancellationToken, System.Threading.Tasks.Task>) ((l, ct) => this.PrintLienWaiversService.Process(l, ct)));
        break;
      case "Email Lien Waivers":
        ((PXProcessingBase<ComplianceDocument>) this.LienWaivers).SetAsyncProcessDelegate((Func<List<ComplianceDocument>, CancellationToken, System.Threading.Tasks.Task>) ((l, ct) => this.EmailLienWaiverService.Process(l, ct)));
        break;
      case "Set as Final":
        PrintEmailLienWaiversProcessComplianceDocumentTypeExt extension = ((PXGraph) this).GetExtension<PrintEmailLienWaiversProcessComplianceDocumentTypeExt>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass170.conditionalPartial = (int?) extension.GetLienWaiverConditionalPartialType()?.AttributeId;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass170.unconditionalPartial = (int?) extension.GetLienWaiverUnconditionalPartialType()?.AttributeId;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass170.conditionalFinal = (int?) extension.GetLienWaiverConditionalFinalType()?.AttributeId;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass170.unconditionalFinal = (int?) extension.GetLienWaiverUnconditionalFinalType()?.AttributeId;
        // ISSUE: method pointer
        ((PXProcessingBase<ComplianceDocument>) this.LienWaivers).SetProcessDelegate<PrintEmailLienWaiversProcess>(new PXProcessingBase<ComplianceDocument>.ProcessItemDelegate<PrintEmailLienWaiversProcess>((object) cDisplayClass170, __methodptr(\u003CInitializeProcessDelegate\u003Eb__2)));
        break;
    }
  }

  private static void SetPrintSettingFieldsVisibility(
    PXCache cache,
    ProcessLienWaiversFilter filter)
  {
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() && filter.Action == "Print Lien Waivers";
    PXUIFieldAttribute.SetVisible<ProcessLienWaiversFilter.printWithDeviceHub>(cache, (object) filter, flag);
    PXUIFieldAttribute.SetVisible<ProcessLienWaiversFilter.definePrinterManually>(cache, (object) filter, flag);
    PXUIFieldAttribute.SetVisible<ProcessLienWaiversFilter.printerID>(cache, (object) filter, flag);
    PXUIFieldAttribute.SetVisible<ProcessLienWaiversFilter.numberOfCopies>(cache, (object) filter, flag);
  }

  protected virtual void SetLienWaiverAsFinal(
    ComplianceDocument lienWaiver,
    int? conditionalPartialType,
    int? unconditionalPartialType,
    int? conditionalFinalType,
    int? unconditionalFinalType)
  {
    int? documentTypeValue1 = lienWaiver.DocumentTypeValue;
    int? nullable1 = conditionalPartialType;
    if (documentTypeValue1.GetValueOrDefault() == nullable1.GetValueOrDefault() & documentTypeValue1.HasValue == nullable1.HasValue)
    {
      lienWaiver.DocumentTypeValue = conditionalFinalType;
    }
    else
    {
      int? documentTypeValue2 = lienWaiver.DocumentTypeValue;
      int? nullable2 = unconditionalPartialType;
      if (documentTypeValue2.GetValueOrDefault() == nullable2.GetValueOrDefault() & documentTypeValue2.HasValue == nullable2.HasValue)
        lienWaiver.DocumentTypeValue = unconditionalFinalType;
    }
    APPaymentEntryLienWaiver.RecalculateLWAmount((PXGraph) this, lienWaiver);
    ((PXSelectBase<ComplianceDocument>) this.LienWaivers).Update(lienWaiver);
    ((PXGraph) this).Persist();
  }

  [PXCacheName("Compliance AP Document Reference")]
  public class ComplianceDocumentAPDocumentReference : ComplianceDocumentReference
  {
    [PXDBString]
    [APDocType.List]
    [PXUIField(DisplayName = "AP Document Type")]
    public override string Type { get; set; }

    [PXDBString]
    [PXUIField(DisplayName = "AP Document Ref. Nbr.")]
    public override string ReferenceNumber { get; set; }

    public new abstract class complianceDocumentReferenceId : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPDocumentReference.complianceDocumentReferenceId>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPDocumentReference.type>
    {
    }

    public new abstract class referenceNumber : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPDocumentReference.referenceNumber>
    {
    }
  }

  [PXCacheName("Compliance Document AP Payment Reference")]
  public class ComplianceDocumentAPPaymentReference : ComplianceDocumentReference
  {
    [PXDBString]
    [APDocType.List]
    [PXUIField(DisplayName = "AP Payment Type")]
    public override string Type { get; set; }

    [PXDBString]
    [PXUIField(DisplayName = "AP Payment Ref. Nbr.")]
    public override string ReferenceNumber { get; set; }

    public new abstract class complianceDocumentReferenceId : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPPaymentReference.complianceDocumentReferenceId>
    {
    }

    public new abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPPaymentReference.type>
    {
    }

    public new abstract class referenceNumber : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PrintEmailLienWaiversProcess.ComplianceDocumentAPPaymentReference.referenceNumber>
    {
    }
  }
}
