// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.AddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<Address, Where<Address.addressID, Equal<Current<BAccount.defAddressID>>>>))]
[PXPersonalDataTable(typeof (Select2<Address, InnerJoin<Location, On<Address.addressID, Equal<Location.defAddressID>>>, Where<Location.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Location.locationID, Equal<Current<BAccount.defLocationID>>>>>))]
[Serializable]
public sealed class AddressExt : PXCacheExtension<
#nullable disable
Address>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AddressExt.pseudonymizationStatus>
  {
  }
}
