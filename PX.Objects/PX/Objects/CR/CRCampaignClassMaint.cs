// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCampaignClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public class CRCampaignClassMaint : PXGraph<CRCampaignClassMaint, CRCampaignType>
{
  [PXViewName("Campaign Class")]
  public PXSelect<CRCampaignType> CampaignClass;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<CRCampaignType, CRCampaign> Mapping;

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void CRCampaignType_TypeID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Attribute")]
  protected virtual void _(
    Events.CacheAttached<CSAttributeGroup.attributeID> e)
  {
  }
}
