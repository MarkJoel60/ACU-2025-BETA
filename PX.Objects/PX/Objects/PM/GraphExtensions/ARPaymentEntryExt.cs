// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ARPaymentEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using System.Linq;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ARPaymentEntryExt : PXGraphExtension<ARPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void CheckDocumentBeforeVoiding(PXGraph graph, ARPayment payment)
  {
    PXResult<ARAdjust> pxResult = ((IQueryable<PXResult<ARAdjust>>) PXSelectBase<ARAdjust, PXViewOf<ARAdjust>.BasedOn<SelectFromBase<ARAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARPayment>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARPayment.docType>>>>>.And<BqlOperand<ARAdjust.adjgRefNbr, IBqlString>.IsEqual<ARPayment.refNbr>>>>, FbqlJoins.Inner<ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdRefNbr, Equal<ARTran.refNbr>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARTran.tranType>>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<ARTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<P.AsString>>>>, And<BqlOperand<ARPayment.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.released, Equal<True>>>>>.And<BqlOperand<ARAdjust.voided, IBqlBool>.IsEqual<False>>>>>.And<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.closed>>>>.ReadOnly.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) payment?.DocType,
      (object) payment?.RefNbr
    })).FirstOrDefault<PXResult<ARAdjust>>();
    if (pxResult != null)
      throw new PXException(PXMessages.LocalizeFormat("The {0} project is closed.", new object[1]
      {
        (object) ((PXResult) pxResult).GetItem<PMProject>().ContractCD.Trim()
      }));
  }

  [PXOverride]
  public virtual void CheckDocumentBeforeReversing(PXGraph graph, ARAdjust application)
  {
    PXResult<ARTran> pxResult = ((IQueryable<PXResult<ARTran>>) PXSelectBase<ARTran, PXViewOf<ARTran>.BasedOn<SelectFromBase<ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<ARTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.refNbr, Equal<P.AsString>>>>, And<BqlOperand<ARTran.tranType, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARTran.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.closed>>>>.ReadOnly.Config>.SelectWindowed(graph, 0, 1, new object[2]
    {
      (object) application?.AdjdRefNbr,
      (object) application?.AdjdDocType
    })).FirstOrDefault<PXResult<ARTran>>();
    if (pxResult != null)
      throw new PXException(PXMessages.LocalizeFormat("The {0} project is closed.", new object[1]
      {
        (object) ((PXResult) pxResult).GetItem<PMProject>().ContractCD.Trim()
      }));
  }
}
