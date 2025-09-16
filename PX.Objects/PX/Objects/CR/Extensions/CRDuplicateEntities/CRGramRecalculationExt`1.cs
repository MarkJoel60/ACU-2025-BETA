// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRGramRecalculationExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

public class CRGramRecalculationExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<CRSetup, TypeArrayOf<IFbqlJoin>.Empty>, CRSetup>.View Setup;

  protected static bool IsFeatureActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.contactDuplicate>();
  }

  [PXOverride]
  public virtual void Persist(Action del)
  {
    if (WebDialogResultExtension.IsPositive(((PXSelectBase) this.Setup).View.Answer))
      PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<CRGrammProcess>(), (PXRedirectHelper.WindowMode) 0);
    bool flag = this.RequiresGramRecalculation();
    del();
    if (((this.Base.IsImport ? 0 : (!this.Base.IsExport ? 1 : 0)) & (flag ? 1 : 0)) == 0)
      return;
    ((PXSelectBase) this.Setup).View.Ask((object) null, "Warning", PXMessages.Localize("Duplicate validation rules have been changed. To apply new settings, you need to recalculate validation scores. Would you like to open the Calculate Grams (CR503400) form?"), (MessageButtons) 4, (MessageIcon) 3);
  }

  protected virtual bool RequiresGramRecalculation()
  {
    return ((PXCache) GraphHelper.Caches<CRValidation>((PXGraph) this.Base)).Updated.OfType<CRValidation>().Any<CRValidation>((Func<CRValidation, bool>) (v => !v.GramValidationDateTime.HasValue));
  }
}
