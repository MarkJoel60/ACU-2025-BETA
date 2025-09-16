// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UserPreferenceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.IN;

public sealed class UserPreferenceExt : PXCacheExtension<
#nullable disable
UserPreferences>
{
  [Site(DisplayName = "Default Warehouse")]
  [PXForeignReference(typeof (Field<UserPreferenceExt.defaultSite>.IsRelatedTo<INSite.siteID>))]
  public int? DefaultSite { get; set; }

  public static int? GetDefaultSite(PXGraph graph)
  {
    UserPreferences userPreferences = PXResultset<UserPreferences>.op_Implicit(PXSelectBase<UserPreferences, PXViewOf<UserPreferences>.BasedOn<SelectFromBase<UserPreferences, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>.Config>.Select(graph, Array.Empty<object>()));
    return (userPreferences != null ? PXCacheEx.GetExtension<UserPreferences, UserPreferenceExt>(userPreferences) : (UserPreferenceExt) null)?.DefaultSite;
  }

  public abstract class defaultSite : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UserPreferenceExt.defaultSite>
  {
  }
}
