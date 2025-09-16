// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.SOAddressExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<SOAddress, Where<SOAddress.customerID, Equal<Current<BAccount.bAccountID>>>>))]
[Serializable]
public sealed class SOAddressExt : PXCacheExtension<
#nullable disable
SOAddress>, IPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOAddressExt.pseudonymizationStatus>
  {
  }
}
