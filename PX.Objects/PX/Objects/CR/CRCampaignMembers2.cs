// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCampaignMembers2
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
public class CRCampaignMembers2 : CRCampaignMembers
{
  public new abstract class contactID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CRCampaignMembers2.contactID>
  {
  }

  public new abstract class campaignID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRCampaignMembers2.campaignID>
  {
  }
}
