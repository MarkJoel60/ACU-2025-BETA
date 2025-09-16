// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderEntry_CRRelationDetailsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

/// <inheritdoc />
public class SOOrderEntry_CRRelationDetailsExt : 
  CRRelationDetailsExt<SOOrderEntry, SOOrder, SOOrder.noteID>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.customerModule>();

  [CRRoleTypeList.ShortList]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRRelation.role> e)
  {
  }
}
