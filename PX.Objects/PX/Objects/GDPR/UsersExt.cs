// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.UsersExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GDPR;

[PXPersonalDataTable(typeof (Select<Users, Where<Users.pKID, Equal<Current<Contact.userID>>>>))]
[Serializable]
public sealed class UsersExt : PXCacheExtension<
#nullable disable
Users>, IPseudonymizable, IPostPseudonymizable
{
  [PXPseudonymizationStatusField]
  public int? PseudonymizationStatus { get; set; }

  public List<PXDataFieldParam> InterruptPseudonimyzationHandler(List<PXDataFieldParam> restricts)
  {
    return new List<PXDataFieldParam>()
    {
      (PXDataFieldParam) new PXDataFieldAssign<Users.isApproved>((object) false)
    };
  }

  public abstract class pseudonymizationStatus : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UsersExt.pseudonymizationStatus>
  {
  }
}
