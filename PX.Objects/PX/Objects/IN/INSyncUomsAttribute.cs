// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSyncUomsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// The attribute is supposed to synchronize values of different units of measure settings in the case if the Multiple Units of Measure feature is disabled.
/// </summary>
public class INSyncUomsAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
{
  protected readonly Type[] _uomFieldList;

  public INSyncUomsAttribute(params Type[] uomFieldList) => this._uomFieldList = uomFieldList;

  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
      return;
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    foreach (Type uomField in this._uomFieldList)
      sender.SetValue(e.Row, uomField.Name, obj);
  }
}
