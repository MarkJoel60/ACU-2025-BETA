// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CountryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

public class CountryAttribute : CountryStateSelectorAttribute
{
  protected override string Type => "Countries";

  public CountryAttribute()
    : this(typeof (Search<PX.Objects.CS.Country.countryID>))
  {
  }

  public CountryAttribute(System.Type search)
    : base(search)
  {
    this.DescriptionField = typeof (PX.Objects.CS.Country.description);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && !this.Find(CountryStateSelectorAttribute.slot.Countries, sender, e) && !this.Find(CountryStateSelectorAttribute.slot.CountriesDescription, sender, e) && !this.FindRegex(CountryStateSelectorAttribute.slot.CountriesRegex, sender, e))
      throw new PXSetPropertyException("{0} '{1}' cannot be found in the system.", new object[2]
      {
        (object) $"[{((PXEventSubscriberAttribute) this)._FieldName}]",
        e.NewValue
      });
    ((CancelEventArgs) e).Cancel = true;
  }
}
