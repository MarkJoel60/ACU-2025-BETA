// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportLineSelector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.TX;

public class TaxReportLineSelector : PXSelectorAttribute
{
  public TaxReportLineSelector(Type search, params Type[] fields)
    : base(search, fields)
  {
    this.DescriptionField = typeof (TaxReportLine.descr);
    this._UnconditionalSelect = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Search<TaxReportLine.lineNbr, Where<TaxReportLine.vendorID, Equal<Current<TaxReportLine.vendorID>>, And<TaxReportLine.lineNbr, Equal<Required<TaxReportLine.lineNbr>>>>>)
    });
    this._CacheGlobal = false;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null || e.NewValue is int || int.TryParse(e.NewValue.ToString(), out int _))
      return;
    this.CustomMessageElementDoesntExist = "The value cannot be found in the system.";
    this.throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, this._PrimarySimpleSelect, e.Row), e.ExternalCall, e.NewValue);
  }
}
