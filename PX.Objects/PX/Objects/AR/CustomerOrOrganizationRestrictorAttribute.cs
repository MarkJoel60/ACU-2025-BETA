// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerOrOrganizationRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.AR;

internal class CustomerOrOrganizationRestrictorAttribute : PXRestrictorAttribute
{
  public CustomerOrOrganizationRestrictorAttribute()
    : base(typeof (Where<Customer.type, IsNotNull, Or<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType>>>), "Only a customer or company business account can be specified.", Array.Empty<System.Type>())
  {
  }

  public CustomerOrOrganizationRestrictorAttribute(System.Type shipmentTypeField)
    : base(BqlTemplate.OfCondition<Where<Current<BqlPlaceholder.A>, NotEqual<INDocType.transfer>, And<Customer.type, IsNotNull, Or<Current<BqlPlaceholder.A>, Equal<INDocType.transfer>, And<Where<BAccountR.type, In3<BAccountType.branchType, BAccountType.organizationType>, Or<PX.Objects.CR.BAccount.isBranch, Equal<True>>>>>>>>.Replace<BqlPlaceholder.A>(shipmentTypeField).ToType(), "Incorrect value specified. Specify a customer (for shipments of the Shipment type) or a company business account (for shipments of the Transfer type).", Array.Empty<System.Type>())
  {
  }
}
