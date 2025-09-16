// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerOrOrganizationInNoUpdateDocRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.AR;

internal class CustomerOrOrganizationInNoUpdateDocRestrictorAttribute : PXRestrictorAttribute
{
  public CustomerOrOrganizationInNoUpdateDocRestrictorAttribute()
    : base(typeof (Where<Customer.type, IsNotNull, Or<Current<PX.Objects.SO.SOOrder.aRDocType>, Equal<ARDocType.noUpdate>, And<Current<PX.Objects.SO.SOOrder.behavior>, Equal<SOBehavior.tR>, And<Where<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType>, Or<PX.Objects.CR.BAccount.isBranch, Equal<True>>>>>>>), "Only a customer or company business account can be specified.", Array.Empty<System.Type>())
  {
  }
}
