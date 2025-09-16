// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.APReleaseProcessProjectsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class APReleaseProcessProjectsExt : PXGraphExtension<APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  /// <summary>
  /// Override of <see cref="M:PX.Objects.AP.APReleaseProcess.GetDeductibleLines(PX.Objects.TX.Tax,PX.Objects.AP.APTaxTran)" />
  /// </summary>
  [PXOverride]
  public virtual PXResultset<APTax> GetDeductibleLines(
    PX.Objects.TX.Tax salestax,
    APTaxTran x,
    APReleaseProcessProjectsExt.GetDeductibleLinesDelegate baseMethod)
  {
    return PXSelectBase<APTax, PXSelectJoin<APTax, InnerJoin<APTran, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>, LeftJoin<PX.Objects.PM.Lite.PMProject, On<APTran.projectID, Equal<PX.Objects.PM.Lite.PMProject.contractID>>, LeftJoin<PX.Objects.PM.Lite.PMTask, On<APTran.projectID, Equal<PX.Objects.PM.Lite.PMTask.projectID>, And<APTran.taskID, Equal<PX.Objects.PM.Lite.PMTask.taskID>>>>>>, Where<APTax.taxID, Equal<Required<APTax.taxID>>, And<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>, OrderBy<Desc<APTax.curyTaxAmt>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) salestax.TaxID,
      (object) x.TranType,
      (object) x.RefNbr
    });
  }

  [PXOverride]
  public virtual void TryToGetProjectAndTask(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task,
    APReleaseProcessProjectsExt.TryToGetProjectAndTaskDelegate baseMethod)
  {
    baseMethod(item, out project, out task);
    APTran apTran = item.GetItem<APTran>();
    if (!apTran.ProjectID.HasValue)
      return;
    int? projectId = apTran.ProjectID;
    int? nullable = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue)
      return;
    project = item.GetItem<PX.Objects.PM.Lite.PMProject>();
    nullable = apTran.TaskID;
    if (!nullable.HasValue)
      return;
    task = item.GetItem<PX.Objects.PM.Lite.PMTask>();
  }

  public delegate PXResultset<APTax> GetDeductibleLinesDelegate(PX.Objects.TX.Tax salestax, APTaxTran x);

  public delegate void TryToGetProjectAndTaskDelegate(
    PXResult item,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task);
}
