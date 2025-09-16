// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ContacLanguageDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class ContacLanguageDefaultAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldDefaultingSubscriber
{
  protected readonly Type address;

  public ContacLanguageDefaultAttribute(Type address) => this.address = address;

  public virtual void CacheAttached(PXCache sender)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ContacLanguageDefaultAttribute.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new ContacLanguageDefaultAttribute.\u003C\u003Ec__DisplayClass2_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.sender = sender;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass20.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    base.CacheAttached(cDisplayClass20.sender);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass20.sender.Graph.FieldUpdated.AddHandler(this.address.DeclaringType, this.address.Name, new PXFieldUpdated((object) cDisplayClass20, __methodptr(\u003CCacheAttached\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass20.sender.Graph.RowInserted.AddHandler(this.address.DeclaringType, new PXRowInserted((object) cDisplayClass20, __methodptr(\u003CCacheAttached\u003Eb__1)));
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[this.address.DeclaringType];
    Country country = (Country) PXSelectorAttribute.Select<Country.countryID>(cach, cach.Current);
    e.NewValue = (object) country?.LanguageID;
  }
}
