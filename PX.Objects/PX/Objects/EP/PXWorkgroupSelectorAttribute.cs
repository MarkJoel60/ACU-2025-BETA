// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXWorkgroupSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.TM;

#nullable disable
namespace PX.Objects.EP;

public class PXWorkgroupSelectorAttribute : PXSelectorAttribute
{
  public PXWorkgroupSelectorAttribute()
    : this((System.Type) null)
  {
  }

  public PXWorkgroupSelectorAttribute(System.Type rootWorkgroupID)
  {
    System.Type type;
    if (!(rootWorkgroupID == (System.Type) null))
      type = BqlCommand.Compose(new System.Type[23]
      {
        typeof (Search2<,,,>),
        typeof (EPCompanyTree.workGroupID),
        typeof (InnerJoin<EPCompanyTreeH, On<EPCompanyTreeH.workGroupID, Equal<EPCompanyTree.workGroupID>>, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTree.workGroupID>, And<EPCompanyTreeMember.isOwner, Equal<True>>>, LeftJoin<CREmployee, On<CREmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>>>),
        typeof (Where2<,>),
        typeof (Where<,,>),
        typeof (Current<>),
        rootWorkgroupID,
        typeof (IsNotNull),
        typeof (And<,>),
        typeof (EPCompanyTreeH.parentWGID),
        typeof (Equal<>),
        typeof (Current<>),
        rootWorkgroupID,
        typeof (Or<>),
        typeof (Where<,,>),
        typeof (Current<>),
        rootWorkgroupID,
        typeof (IsNull),
        typeof (And<,>),
        typeof (EPCompanyTreeH.parentWGID),
        typeof (Equal<>),
        typeof (EPCompanyTreeH.workGroupID),
        typeof (OrderBy<Asc<EPCompanyTree.description, Asc<EPCompanyTree.workGroupID>>>)
      });
    else
      type = typeof (Search3<EPCompanyTree.workGroupID, LeftJoin<EPCompanyTreeMember, On<EPCompanyTreeMember.workGroupID, Equal<EPCompanyTree.workGroupID>, And<EPCompanyTreeMember.isOwner, Equal<True>>>, LeftJoin<CREmployee, On<CREmployee.defContactID, Equal<EPCompanyTreeMember.contactID>>>>, OrderBy<Asc<EPCompanyTree.description, Asc<EPCompanyTree.workGroupID>>>>);
    System.Type[] typeArray = new System.Type[3]
    {
      typeof (EPCompanyTree.description),
      typeof (CREmployee.acctCD),
      typeof (CREmployee.acctName)
    };
    // ISSUE: explicit constructor call
    base.\u002Ector(type, typeArray);
    this.SubstituteKey = typeof (EPCompanyTree.description);
  }
}
