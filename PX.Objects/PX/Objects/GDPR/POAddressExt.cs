// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.POAddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<POAddress, Where<POAddress.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>))]
[PXPersonalDataTable(typeof (Select5<POAddress, InnerJoin<POOrder, On<POOrder.shipAddressID, Equal<POAddress.addressID>, And<Where<POOrder.shipDestType, Equal<POShippingDestination.customer>, Or<POOrder.shipDestType, Equal<POShippingDestination.vendor>>>>>>, Where<POOrder.vendorID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, And<POAddress.isDefaultAddress, Equal<False>>>, Aggregate<GroupBy<POAddress.noteID>>>))]
[PXPersonalDataTable(typeof (Select5<POAddress, InnerJoin<POOrder, On<POOrder.remitAddressID, Equal<POAddress.addressID>, And<POOrder.shipDestType, Equal<POShippingDestination.vendor>>>>, Where<POOrder.vendorID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>, Aggregate<GroupBy<POAddress.noteID>>>))]
[Serializable]
public sealed class POAddressExt : PXCacheExtension<
#nullable disable
POAddress>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAddressExt.pseudonymizationStatus>
  {
  }
}
