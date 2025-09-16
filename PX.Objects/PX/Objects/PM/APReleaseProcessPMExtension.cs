// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APReleaseProcessPMExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PM;

public class APReleaseProcessPMExtension : PXGraphExtension<APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual APRegister? OnBeforeRelease(
    APRegister? document,
    Func<APRegister?, APRegister?> baseMethod)
  {
    document = baseMethod(document);
    if (document == null)
      return document;
    if (!document.ProjectID.HasValue)
    {
      document.ProjectID = ProjectDefaultAttribute.NonProject();
      document.HasMultipleProjects = new bool?(true);
    }
    APTran apTran1 = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXViewOf<APTran>.BasedOn<SelectFromBase<APTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APTran.tranType, Equal<P.AsString>>>>, And<BqlOperand<APTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APTran.projectID, IBqlInt>.IsNotEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) document.DocType,
      (object) document.RefNbr,
      (object) document.ProjectID
    }));
    if (apTran1 == null)
    {
      document.HasMultipleProjects = new bool?(false);
      return document;
    }
    APTran apTran2 = PXResultset<APTran>.op_Implicit(PXSelectBase<APTran, PXViewOf<APTran>.BasedOn<SelectFromBase<APTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APTran.tranType, Equal<P.AsString>>>>, And<BqlOperand<APTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<APTran.projectID, IBqlInt>.IsNotEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) document.DocType,
      (object) document.RefNbr,
      (object) apTran1.ProjectID
    }));
    document.ProjectID = apTran2 != null ? ProjectDefaultAttribute.NonProject() : apTran1.ProjectID;
    document.HasMultipleProjects = new bool?(apTran2 != null);
    return document;
  }
}
