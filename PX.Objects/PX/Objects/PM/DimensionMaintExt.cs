// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DimensionMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.CT;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class DimensionMaintExt : PXGraphExtension<DimensionMaint>
{
  public PXSelect<Dimension, Where<Dimension.dimensionID, InFieldClassActivated, Or<Dimension.dimensionID, IsNull, Or<Dimension.dimensionID, Equal<ProjectAttribute.dimension>, Or<Dimension.dimensionID, Equal<ProjectAttribute.dimensionTM>, Or<Dimension.dimensionID, Equal<ContractAttribute.dimension>, Or<Dimension.dimensionID, Equal<ContractTemplateAttribute.dimension>>>>>>>> Header;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<Dimension.dimensionID, Where<Dimension.dimensionID, InFieldClassActivated, Or<Dimension.dimensionID, Equal<ProjectAttribute.dimension>, Or<Dimension.dimensionID, Equal<ProjectAttribute.dimensionTM>, Or<Dimension.dimensionID, Equal<ContractAttribute.dimension>, Or<Dimension.dimensionID, Equal<ContractTemplateAttribute.dimension>>>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<Dimension.dimensionID> e)
  {
  }

  [PXOverride]
  public virtual IEnumerable GetSimpleDetails(Dimension dim)
  {
    PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>, And<Where<Segment.dimensionID, InFieldClassActivated, Or<Segment.dimensionID, Equal<ProjectAttribute.dimension>, Or<Segment.dimensionID, Equal<ProjectAttribute.dimensionTM>, Or<Segment.dimensionID, Equal<ContractAttribute.dimension>, Or<Segment.dimensionID, Equal<ContractTemplateAttribute.dimension>>>>>>>>> pxSelect = new PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>, And<Where<Segment.dimensionID, InFieldClassActivated, Or<Segment.dimensionID, Equal<ProjectAttribute.dimension>, Or<Segment.dimensionID, Equal<ProjectAttribute.dimensionTM>, Or<Segment.dimensionID, Equal<ContractAttribute.dimension>, Or<Segment.dimensionID, Equal<ContractTemplateAttribute.dimension>>>>>>>>>((PXGraph) this.Base);
    object[] objArray = new object[1]
    {
      (object) dim.DimensionID
    };
    foreach (PXResult<Segment> pxResult in ((PXSelectBase<Segment>) pxSelect).Select(objArray))
      yield return (object) PXResult<Segment>.op_Implicit(pxResult);
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.contractManagement>();
  }
}
