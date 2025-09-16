// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ValueValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public abstract class ValueValidationAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber
{
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    string validationRegexp = this.FindValidationRegexp(sender, e.Row);
    if (!this.ValidateValue(newValue, validationRegexp))
      throw new PXSetPropertyException("Provided value does not pass validation rules defined for this field.");
  }

  protected abstract string FindValidationRegexp(PXCache sender, object row);

  protected virtual bool ValidateValue(string val, string regex)
  {
    return val == null || regex == null || new Regex(regex).IsMatch(val);
  }
}
