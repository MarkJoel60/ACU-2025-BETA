// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.SubAccountMaskARAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.DR;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
[SubAccountARList.ClassList]
public sealed class SubAccountMaskARAttribute : PXEntityAttribute
{
  private string _DimensionName = "SUBACCOUNT";
  private string _MaskName = "DRCODEAR";

  public SubAccountMaskARAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute(this._DimensionName, this._MaskName, "D", new SubAccountARList.ClassListAttribute().AllowedValues, new SubAccountARList.ClassListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    subscribers.Remove(((IEnumerable) ((PXAggregateAttribute) this)._Attributes).OfType<SubAccountARList.ClassListAttribute>().FirstOrDefault<SubAccountARList.ClassListAttribute>() as ISubscriber);
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    SubAccountARList.ClassListAttribute classListAttribute = (SubAccountARList.ClassListAttribute) ((IEnumerable<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).First<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (x => x.GetType() == typeof (SubAccountARList.ClassListAttribute)));
    ((PXDimensionMaskAttribute) ((IEnumerable<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).First<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (x => x.GetType() == typeof (PXDimensionMaskAttribute)))).SynchronizeLabels(classListAttribute.AllowedValues, classListAttribute.AllowedLabels);
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new SubAccountARList.ClassListAttribute().AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) new SubAccountARList.ClassListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
