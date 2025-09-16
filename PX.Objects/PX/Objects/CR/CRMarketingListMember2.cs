// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMarketingListMember2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class CRMarketingListMember2 : CRMarketingListMember
{
  public new abstract class contactID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CRMarketingListMember2.contactID>
  {
  }

  public new abstract class marketingListID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRMarketingListMember2.marketingListID>
  {
  }
}
