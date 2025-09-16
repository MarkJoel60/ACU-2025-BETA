// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Extensions.SlaveCuryIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM.Extensions;

public class SlaveCuryIDAttribute : PXAggregateAttribute, IPXRowPersistingSubscriber
{
  private readonly Type SourceField;

  public SlaveCuryIDAttribute(Type sourceField)
  {
    this.SourceField = sourceField;
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDBStringAttribute(5)
    {
      IsUnicode = true,
      InputMask = ">LLLLL"
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUIFieldAttribute()
    {
      DisplayName = "Currency"
    });
    this._Attributes.Add((PXEventSubscriberAttribute) new PXSelectorAttribute(typeof (Currency.curyID)));
  }

  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    long? persistedCuryInfoId = CurrencyInfoAttribute.GetPersistedCuryInfoID(sender, (long?) sender.GetValue(e.Row, this.SourceField.Name));
    CurrencyInfo currencyInfo = PXResultset<CurrencyInfo>.op_Implicit(PXSelectBase<CurrencyInfo, PXSelect<CurrencyInfo, Where<CurrencyInfo.curyInfoID, Equal<Required<CurrencyInfo.curyInfoID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) persistedCuryInfoId
    }));
    if (currencyInfo == null || currencyInfo.CuryID == null)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName, (object) currencyInfo.CuryID);
  }
}
