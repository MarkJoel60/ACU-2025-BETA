// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CS.GraphExtensions.CsAttributeMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CN.Compliance.CS.GraphExtensions;

public class CsAttributeMaintExt : PXGraphExtension<CSAttributeMaint>
{
  protected virtual void CSAttributeDetail_RowDeleting(
    PXCache cache,
    PXRowDeletingEventArgs arguments,
    PXRowDeleting baseHandler)
  {
    if (!(arguments.Row is CSAttributeDetail row))
      return;
    baseHandler.Invoke(cache, arguments);
    if (!this.DoesAnyAttributeRelationExist((CSAttribute) null, row))
      return;
    ((CancelEventArgs) arguments).Cancel = true;
  }

  protected virtual void CSAttribute_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs arguments,
    PXRowSelected baseHandler)
  {
    if (!(arguments.Row is CSAttribute row))
      return;
    baseHandler.Invoke(cache, arguments);
    cache.AllowDelete = !this.DoesAnyAttributeRelationExist(row, (CSAttributeDetail) null);
  }

  protected virtual void CSAttribute_ControlType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs arguments,
    PXFieldUpdated baseHandler)
  {
    if (!(arguments.Row is CSAttribute row))
      return;
    baseHandler.Invoke(cache, arguments);
    cache.AllowDelete = !this.DoesAnyAttributeRelationExist(row, (CSAttributeDetail) null);
  }

  private bool DoesAnyAttributeRelationExist(
    CSAttribute attribute,
    CSAttributeDetail attributeDetail)
  {
    bool flag = this.DoesAttributeGroupExist(attribute);
    return ((attributeDetail == null ? 1 : (this.DoesAttributeExist(attributeDetail) ? 1 : 0)) & (flag ? 1 : 0)) != 0;
  }

  private bool DoesAttributeGroupExist(CSAttribute attribute)
  {
    return ((PXSelectBase<CSAttributeGroup>) new PXSelect<CSAttributeGroup, Where<CSAttributeGroup.attributeID, Equal<Required<CSAttributeGroup.attributeID>>>>((PXGraph) this.Base)).Any<CSAttributeGroup>((object) attribute?.AttributeID);
  }

  private bool DoesAttributeExist(CSAttributeDetail attributeDetail)
  {
    return ((PXSelectBase<CSAnswers>) new PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>, And<CSAnswers.value, Equal<Required<CSAnswers.value>>>>>((PXGraph) this.Base)).Any<CSAnswers>((object) attributeDetail?.AttributeID, (object) attributeDetail?.ValueID);
  }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();
}
