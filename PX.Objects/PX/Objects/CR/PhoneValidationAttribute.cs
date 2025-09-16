// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PhoneValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public sealed class PhoneValidationAttribute : PXPhoneValidationAttribute
{
  public PhoneValidationAttribute()
    : base("")
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXEventSubscriberAttribute) this).CacheAttached(sender);
    PhoneValidationAttribute.Definition slot = PXDatabase.GetSlot<PhoneValidationAttribute.Definition>("CompanyPhoneMask", new System.Type[1]
    {
      typeof (Company)
    });
    if (slot == null)
      return;
    this._mask = slot.PhoneMask ?? "";
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public string PhoneMask;

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Company>(new PXDataField[1]
      {
        new PXDataField(typeof (Company.phoneMask).Name)
      }))
      {
        if (pxDataRecord == null)
          return;
        this.PhoneMask = pxDataRecord.GetString(0);
      }
    }
  }
}
