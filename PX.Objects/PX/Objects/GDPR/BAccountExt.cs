// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.BAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<BAccount, Where<BAccount.defContactID, Equal<Current<Contact.contactID>>>>))]
[Serializable]
public sealed class BAccountExt : PXCacheExtension<
#nullable disable
BAccount>, IPseudonymizable, IPostPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public List<PXDataFieldParam> InterruptPseudonimyzationHandler(List<PXDataFieldParam> restricts)
  {
    return new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign<BAccount.status>((object) "I"),
      (PXDataFieldParam) new PXDataFieldAssign<BAccount.vStatus>((object) "I")
    };
  }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountExt.pseudonymizationStatus>
  {
  }
}
