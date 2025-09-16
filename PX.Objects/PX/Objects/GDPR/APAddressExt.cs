// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.APAddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<APAddress, Where<APAddress.vendorID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>))]
[PXPersonalDataTable(typeof (Select5<APAddress, InnerJoin<APPayment, On<APPayment.remitAddressID, Equal<APAddress.addressID>>>, Where<APPayment.vendorID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>, Aggregate<GroupBy<APAddress.noteID>>>))]
[Serializable]
public sealed class APAddressExt : PXCacheExtension<
#nullable disable
APAddress>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APAddressExt.pseudonymizationStatus>
  {
  }
}
