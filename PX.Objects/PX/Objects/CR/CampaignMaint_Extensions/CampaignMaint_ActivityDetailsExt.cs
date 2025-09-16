// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CampaignMaint_Extensions.CampaignMaint_ActivityDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR.Extensions;

#nullable disable
namespace PX.Objects.CR.CampaignMaint_Extensions;

public class CampaignMaint_ActivityDetailsExt : 
  ActivityDetailsExt<CampaignMaint, CRCampaign, CRCampaign.noteID>
{
  public override System.Type GetLinkConditionClause()
  {
    return typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRPMTimeActivity.documentNoteID, Equal<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>>>.Or<BqlOperand<CRPMTimeActivity.refNoteID, IBqlGuid>.IsEqual<BqlField<CRCampaign.noteID, IBqlGuid>.FromCurrent>>>);
  }
}
