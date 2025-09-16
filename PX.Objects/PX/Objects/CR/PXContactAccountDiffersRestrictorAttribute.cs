// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXContactAccountDiffersRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Attributes;

#nullable disable
namespace PX.Objects.CR;

public class PXContactAccountDiffersRestrictorAttribute(System.Type where, params System.Type[] messageParameters) : 
  RestrictorWithParametersAttribute(where, "", messageParameters)
{
  public override object[] GetMessageParameters(PXCache sender, object itemres, object row)
  {
    Contact contact = PXResult.Unwrap<Contact>(itemres);
    if (contact != null && contact.ContactType == "EP")
      this._Message = "The {0} employee is not assigned to the {1} branch.";
    else
      this._Message = "The {0} contact is not associated with the {1} business account.";
    return base.GetMessageParameters(sender, itemres, row);
  }
}
