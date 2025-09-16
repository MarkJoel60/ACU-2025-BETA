// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.POContactExt
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

[PXPersonalDataTable(typeof (Select<POContact, Where<POContact.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>))]
[PXPersonalDataTable(typeof (Select5<POContact, InnerJoin<POOrder, On<POOrder.shipContactID, Equal<POContact.contactID>, And<Where<POOrder.shipDestType, Equal<POShippingDestination.customer>, Or<POOrder.shipDestType, Equal<POShippingDestination.vendor>>>>>>, Where<POOrder.shipToBAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>, And<POContact.isDefaultContact, Equal<False>>>, Aggregate<GroupBy<POContact.noteID>>>))]
[PXPersonalDataTable(typeof (Select5<POContact, InnerJoin<POOrder, On<POOrder.remitContactID, Equal<POContact.contactID>, And<POOrder.shipDestType, Equal<POShippingDestination.vendor>>>>, Where<POOrder.vendorID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>, Aggregate<GroupBy<POContact.noteID>>>))]
[Serializable]
public sealed class POContactExt : PXCacheExtension<
#nullable disable
POContact>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POContactExt.pseudonymizationStatus>
  {
  }
}
