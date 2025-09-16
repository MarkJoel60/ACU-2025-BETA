// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRContactCacheNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class CRContactCacheNameAttribute(string name) : PXCacheNameAttribute(name)
{
  public virtual string GetName(object row)
  {
    if (!(row is Contact contact))
      return ((PXNameAttribute) this).GetName();
    string name = "Contact";
    switch (contact.ContactType)
    {
      case "LD":
        name = "Lead";
        break;
      case "EP":
        name = "Employee";
        break;
    }
    return name;
  }
}
