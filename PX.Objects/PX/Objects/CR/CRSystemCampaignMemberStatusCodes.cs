// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRSystemCampaignMemberStatusCodes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[Obsolete]
public class CRSystemCampaignMemberStatusCodes
{
  public class Sent : BqlType<IBqlString, string>.Constant<
  #nullable disable
  CRSystemCampaignMemberStatusCodes.Sent>
  {
    public Sent()
      : base("S")
    {
    }
  }

  public class Responsed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRSystemCampaignMemberStatusCodes.Responsed>
  {
    public Responsed()
      : base("P")
    {
    }
  }
}
