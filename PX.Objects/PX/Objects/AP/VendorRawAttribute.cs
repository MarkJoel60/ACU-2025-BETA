// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

[PXDBString(30, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField(DisplayName = "Vendor", Visibility = PXUIVisibility.Visible)]
public sealed class VendorRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "VENDOR";

  public VendorRawAttribute()
    : this((System.Type) null)
  {
  }

  public VendorRawAttribute(System.Type where)
  {
    System.Type[] typeArray1 = new System.Type[4]
    {
      typeof (Search2<,,>),
      typeof (Vendor.acctCD),
      typeof (LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Vendor.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Vendor.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Vendor.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<Vendor.defAddressID>>>>>),
      null
    };
    System.Type type1;
    if (!(where == (System.Type) null))
      type1 = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<PX.Data.Match<Current<AccessInfo.userName>>>), typeof (And<>), where);
    else
      type1 = typeof (Where<PX.Data.Match<Current<AccessInfo.userName>>>);
    typeArray1[3] = type1;
    System.Type type2 = BqlCommand.Compose(typeArray1);
    PXAggregateAttribute.AggregatedAttributesCollection attributes = this._Attributes;
    System.Type type3 = type2;
    System.Type substituteKey = typeof (Vendor.acctCD);
    System.Type[] typeArray2 = new System.Type[7]
    {
      typeof (Vendor.acctCD),
      typeof (Vendor.acctName),
      typeof (Vendor.vendorClassID),
      typeof (Vendor.vStatus),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("VENDOR", type3, substituteKey, typeArray2);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (Vendor.acctName);
    this._SelAttrIndex = this._Attributes.Count - 1;
    this.Filterable = true;
    ((PXDimensionSelectorAttribute) this._Attributes[this._SelAttrIndex]).CacheGlobal = true;
  }
}
