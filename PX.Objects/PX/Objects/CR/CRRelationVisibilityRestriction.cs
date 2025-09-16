// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelationVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CR;

public sealed class CRRelationVisibilityRestriction : PXCacheExtension<CRRelation>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [PXSelector(typeof (Search<BAccount.bAccountID, Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.combinedType>, Or2<Where<BAccount.type, Equal<BAccountType.vendorType>>, And<BAccount.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>>>>, And<Match<Current<AccessInfo.userName>>>>>), new System.Type[] {typeof (BAccount.acctCD), typeof (BAccount.acctName), typeof (BAccount.classID), typeof (BAccount.type), typeof (BAccount.parentBAccountID), typeof (BAccount.acctReferenceNbr)}, SubstituteKey = typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName), Filterable = true, DirtyRead = true)]
  public int? EntityID { get; set; }
}
