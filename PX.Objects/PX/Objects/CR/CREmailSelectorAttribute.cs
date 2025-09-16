// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CREmailSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[method: Obsolete("Will be removed in 7.0 version")]
public sealed class CREmailSelectorAttribute(bool all) : ContactSelectorAttribute(typeof (Contact.eMail), false, new System.Type[3]
{
  typeof (ContactTypesAttribute.person),
  typeof (ContactTypesAttribute.lead),
  typeof (ContactTypesAttribute.employee)
}, new System.Type[7]
{
  typeof (Contact.displayName),
  typeof (Contact.contactType),
  typeof (Contact.eMail),
  typeof (Contact.salutation),
  typeof (BAccount.acctCD),
  typeof (BAccount.acctName),
  typeof (Contact.searchSuggestion)
})
{
  public CREmailSelectorAttribute()
    : this(false)
  {
  }

  protected virtual bool IsReadDeletedSupported => false;

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string str = e.NewValue == null ? string.Empty : e.NewValue.ToString();
    e.NewValue = (object) PXDBEmailAttribute.ToRFC(str);
  }

  public virtual void ReadDeletedFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
