// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.GISiteMapDefinition
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data.Access;

internal abstract class GISiteMapDefinition : SiteMapDefinitionBase<GIDesign, GIDesign.designID>
{
  private readonly string[] _inquiryUrls = new string[2]
  {
    "~/GenericInquiry/GenericInquiry.aspx",
    "~/Scripts/Screens/GenericInquiry.html"
  };

  protected GISiteMapDefinition(params PXDataField[] additionalFields)
    : base(additionalFields)
  {
  }

  protected GISiteMapDefinition(YaqlCondition additionalCondition)
    : base(additionalCondition)
  {
  }

  protected override YaqlCondition GetBaseCondition()
  {
    YaqlCondition baseCondition = (YaqlCondition) null;
    foreach (string inquiryUrl in this._inquiryUrls)
    {
      YaqlCondition condition = SiteMapDefinitionBase<GIDesign, GIDesign.designID>.GetCondition(inquiryUrl, "ID", typeof (GIDesign.designID));
      baseCondition = Yaql.or(baseCondition != null ? Yaql.or(baseCondition, condition) : condition, SiteMapDefinitionBase<GIDesign, GIDesign.designID>.GetCondition(inquiryUrl, "Name", typeof (GIDesign.name)));
    }
    return baseCondition;
  }
}
