// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.SubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField(DisplayName = "Subaccount Mask", Visibility = PXUIVisibility.Visible, FieldClass = "SUBACCOUNT")]
[APAcctSubDefault.ClassList]
public sealed class SubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "APSETUP";

  public SubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "APSETUP", "L", new APAcctSubDefault.ClassListAttribute().AllowedValues, new APAcctSubDefault.ClassListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    this._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = this._Attributes.Count - 1;
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    subscribers.Remove(this._Attributes.OfType<APAcctSubDefault.ClassListAttribute>().FirstOrDefault<APAcctSubDefault.ClassListAttribute>() as ISubscriber);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    APAcctSubDefault.ClassListAttribute classListAttribute = (APAcctSubDefault.ClassListAttribute) this._Attributes.First<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (x => x.GetType() == typeof (APAcctSubDefault.ClassListAttribute)));
    ((PXDimensionMaskAttribute) this._Attributes.First<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (x => x.GetType() == typeof (PXDimensionMaskAttribute)))).SynchronizeLabels(classListAttribute.AllowedValues, classListAttribute.AllowedLabels);
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, System.Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new APAcctSubDefault.ClassListAttribute().AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      return (string) null;
    }
  }
}
