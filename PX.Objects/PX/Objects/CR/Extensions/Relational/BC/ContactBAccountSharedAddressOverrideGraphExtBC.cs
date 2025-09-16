// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.BC.ContactBAccountSharedAddressOverrideGraphExtBC
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.Relational.BC;

[PXInternalUseOnly]
[Obsolete("To use only for C-b Adapter purposes")]
public class ContactBAccountSharedAddressOverrideGraphExtBC : 
  SharedChildOverrideGraphExtBC<ContactMaint.ContactBAccountSharedAddressOverrideGraphExt, ContactMaint, Contact.isAddressSameAsMain, Contact.overrideAddress>
{
}
