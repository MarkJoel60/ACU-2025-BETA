// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CustomerAndProspectRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[Obsolete("Use properly configured PX.Objects.CR.BAccountAttribute instead")]
public class CustomerAndProspectRestrictorAttribute : PXRestrictorAttribute
{
  public CustomerAndProspectRestrictorAttribute()
    : base(typeof (Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>), "Business Account is {0}.", new System.Type[1]
    {
      typeof (BAccount.type)
    })
  {
  }
}
