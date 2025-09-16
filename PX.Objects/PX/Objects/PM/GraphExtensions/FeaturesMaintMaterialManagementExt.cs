// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.FeaturesMaintMaterialManagementExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CT;
using System;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class FeaturesMaintMaterialManagementExt : PXGraphExtension<FeaturesMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.materialManagement> e)
  {
    bool? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.materialManagement>, object, object>) e).NewValue as bool?;
    bool flag = false;
    if (!(newValue.GetValueOrDefault() == flag & newValue.HasValue))
      return;
    this.ValidateMaterialManagementDisabling(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet.materialManagement>>) e).Cache);
  }

  protected virtual void ValidateMaterialManagementDisabling(PXCache cache)
  {
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>>.And<BqlOperand<PMProject.accountingMode, IBqlString>.IsNotEqual<ProjectAccountingModes.linked>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())) == null)
      return;
    PXSetPropertyException<FeaturesSet.materialManagement> propertyException = new PXSetPropertyException<FeaturesSet.materialManagement>("The Project-Specific Inventory feature is in use. Before you disable it, you should transfer all inventory from the project-specific cost layers to free stock, and if you plan to use stock items with any project, change its inventory tracking mode to Track by Location.", (PXErrorLevel) 4);
    if (!cache.RaiseExceptionHandling<FeaturesSet.materialManagement>(cache.Current, (object) false, (Exception) propertyException))
      throw propertyException;
  }
}
