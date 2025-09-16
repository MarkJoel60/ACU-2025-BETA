// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GenericInquiryHelpers
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

#nullable enable
namespace PX.Data.GenericInquiry;

internal static class GenericInquiryHelpers
{
  internal const 
  #nullable disable
  string DescriptionFieldSuffix = "_description";

  /// <summary>
  /// Checks that <paramref name="tableAlias" /> will be used in Select statement based on
  /// configured <paramref name="allRelations" /> and <paramref name="firstTable" />
  /// </summary>
  /// <param name="tableAlias">Alias of the table to be checked</param>
  /// <param name="allRelations">All configured relations</param>
  /// <param name="firstTable">First table in the tables list</param>
  /// <remarks>
  /// There are two cases when a specific table is used in the query:
  /// 1. When there are no relations configured in the GI, first table from the tables list is used to select data.
  /// 2. When there are some relations configured in the GI, first table from the relations list is used in the
  ///    FROM statement, then other tables from the relations list are used in the JOIN statements.
  /// There is a limitation of the current GI engine that relations should be in the right order, so in some cases
  /// table might not be used in the query even if it presents in the relations list. Current implementation
  /// checks that <paramref name="tableAlias" /> is a descendant of the table used in the FROM statement.
  /// The following examples show when TableX considered as not used:
  ///    1. TableA join TableB, TableC join TableX. There is no relations between TableB and TableC.
  ///    2. TableA join TableB, TableX join TableB (A -&gt; B &lt;- X). TableA is not a ancestor of the TableX.
  /// </remarks>
  internal static bool IsTableUsedInSelect(
    string tableAlias,
    IEnumerable<GIRelation> allRelations,
    IEnumerable<GIResult> allResults,
    IEnumerable<GITable> allTables)
  {
    List<GIRelation> list1 = allRelations.Where<GIRelation>((Func<GIRelation, bool>) (x =>
    {
      bool? isActive = x.IsActive;
      bool flag = true;
      return isActive.GetValueOrDefault() == flag & isActive.HasValue && !string.IsNullOrEmpty(x.ChildTable) && !string.IsNullOrEmpty(x.ParentTable) && !x.ParentTable.Equals(x.ChildTable, StringComparison.OrdinalIgnoreCase);
    })).ToList<GIRelation>();
    if (!list1.Any<GIRelation>())
    {
      GITable[] array1 = allTables.ToArray<GITable>();
      if (array1.Length == 0 || !((IEnumerable<GITable>) array1).Any<GITable>((Func<GITable, bool>) (table => table.Alias != null && table.Alias.Equals(tableAlias, StringComparison.OrdinalIgnoreCase))))
        return false;
      if (array1[0].Alias != null && array1[0].Alias.Equals(tableAlias, StringComparison.OrdinalIgnoreCase))
        return true;
      string[] array2 = allResults.Where<GIResult>((Func<GIResult, bool>) (result =>
      {
        bool? isActive = result.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue;
      })).Select<GIResult, string>((Func<GIResult, string>) (result => result.ObjectName)).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToArray<string>();
      return array2.Length == 1 && array2[0].Equals(tableAlias, StringComparison.OrdinalIgnoreCase);
    }
    GIRelation giRelation = list1.First<GIRelation>();
    if (giRelation.ParentTable.Equals(tableAlias, StringComparison.OrdinalIgnoreCase) || giRelation.ChildTable.Equals(tableAlias, StringComparison.OrdinalIgnoreCase))
      return true;
    List<GIRelation> list2 = list1.Where<GIRelation>((Func<GIRelation, bool>) (x => x.ChildTable.Equals(tableAlias, StringComparison.OrdinalIgnoreCase))).ToList<GIRelation>();
    if (!list2.Any<GIRelation>())
      return false;
    string parentTable = giRelation.ParentTable;
    HashSet<string> hashSet = list2.Select<GIRelation, string>((Func<GIRelation, string>) (x => x.ParentTable)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    HashSet<string> visitedParents = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    HashSet<string> parents;
    for (; hashSet.Any<string>(); hashSet = list1.Where<GIRelation>((Func<GIRelation, bool>) (x => parents.Contains(x.ChildTable) && !visitedParents.Contains(x.ParentTable))).Select<GIRelation, string>((Func<GIRelation, string>) (x => x.ParentTable)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      if (hashSet.Contains(parentTable))
        return true;
      EnumerableExtensions.AddRange<string>((ISet<string>) visitedParents, (IEnumerable<string>) hashSet);
      parents = hashSet;
    }
    return false;
  }

  public static void SetAccessRightsFromAllGraphs(this PXGraph graph, System.Type cacheType)
  {
    PXCache cach = graph.Caches[cacheType];
    string screenID = graph.Accessinfo.ScreenID?.Replace(".", "");
    string str = string.Join(string.Empty, PXAccess.GetAllFeaturesInfo().Select<PXAccess.FeatureInfo, string>((Func<PXAccess.FeatureInfo, string>) (x => x.ID + x.Visible.ToString())));
    GenericInquiryHelpers.AccessRightsFromAllGraphs slot = PXDatabase.GetSlot<GenericInquiryHelpers.AccessRightsFromAllGraphs, GenericInquiryHelpers.AccessRightsFromAllGraphs.Parameters>(string.Join("$", (object) graph.GetType().FullName, (object) cacheType.FullName, (object) str, (object) GenericInquiryHelpers.GetRolesHashCode()), new GenericInquiryHelpers.AccessRightsFromAllGraphs.Parameters(cacheType, cach.GetItemType(), GenericInquiryHelpers.GetGraphFullName(graph), screenID), typeof (PX.SM.RolesInGraph), typeof (PX.SM.RolesInCache), typeof (PX.SM.RolesInMember));
    PXCacheRights rights = slot.Rights;
    List<string> invisible = slot.Invisible;
    List<string> disabled = slot.Disabled;
    PXAccess.SetRights(cach, rights, invisible, disabled);
    foreach (PXEventSubscriberAttribute attribute in cach.GetAttributes((string) null))
      PXAccess.Secure(cach, attribute);
    graph.Caches[cacheType] = cach;
  }

  private static int GetRolesHashCode()
  {
    return string.Join("$", (IEnumerable<string>) ((PXContext.PXIdentity.AuthUser is ClaimsPrincipal authUser ? authUser.Claims : (IEnumerable<Claim>) null) ?? Enumerable.Empty<Claim>()).Where<Claim>((Func<Claim, bool>) (x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Select<Claim, string>((Func<Claim, string>) (x => x.Value)).OrderBy<string, string>((Func<string, string>) (r => r))).GetHashCode();
  }

  private static string GetGraphFullName(PXGraph graph)
  {
    return CustomizedTypeManager.GetTypeNotCustomized(graph).FullName;
  }

  internal static bool IsDescriptionField(string fieldName)
  {
    return fieldName != null && fieldName.EndsWith("_description", StringComparison.OrdinalIgnoreCase);
  }

  internal static string GetBaseFieldNameForDescriptionField(string fieldName)
  {
    return fieldName == null ? (string) null : fieldName.RemoveFromEnd("_description", StringComparison.OrdinalIgnoreCase);
  }

  public static 
  #nullable enable
  PXSiteMapNode? FindSiteMapNodeByGi(GIDesign giDesign)
  {
    foreach (string genericInquiryPath in UrlConstants.Path.GenericInquiryPaths)
    {
      PXSiteMapNode siteMapNodeUnsecure1 = PXSiteMap.Provider.FindSiteMapNodeUnsecure($"~{genericInquiryPath}?{"id"}={giDesign.DesignID}");
      if (siteMapNodeUnsecure1 != null)
        return siteMapNodeUnsecure1;
      PXSiteMapNode siteMapNodeUnsecure2 = PXSiteMap.Provider.FindSiteMapNodeUnsecure($"~{genericInquiryPath}?name={giDesign.Name}");
      if (siteMapNodeUnsecure2 != null)
        return siteMapNodeUnsecure2;
    }
    return (PXSiteMapNode) null;
  }

  private class AccessRightsFromAllGraphs : 
    IPrefetchable<
    #nullable disable
    GenericInquiryHelpers.AccessRightsFromAllGraphs.Parameters>,
    IPXCompanyDependent
  {
    public PXCacheRights Rights;
    public List<string> Invisible;
    public List<string> Disabled;

    public void Prefetch(
      GenericInquiryHelpers.AccessRightsFromAllGraphs.Parameters parameter)
    {
      StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
      System.Type cacheType = parameter.CacheType;
      PXAccess.GetRights(parameter.ScreenID, parameter.GraphFullName, cacheType, out this.Rights, out this.Invisible, out this.Disabled);
      System.Type dacType = parameter.DacType;
      List<string> invisible1 = this.Invisible;
      HashSet<string> source1 = (invisible1 != null ? invisible1.ToHashSet<string>((IEqualityComparer<string>) ordinalIgnoreCase) : (HashSet<string>) null) ?? new HashSet<string>((IEqualityComparer<string>) ordinalIgnoreCase);
      List<string> disabled1 = this.Disabled;
      HashSet<string> source2 = (disabled1 != null ? disabled1.ToHashSet<string>((IEqualityComparer<string>) ordinalIgnoreCase) : (HashSet<string>) null) ?? new HashSet<string>((IEqualityComparer<string>) ordinalIgnoreCase);
      HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) ordinalIgnoreCase)
      {
        parameter.GraphFullName
      };
      Dictionary<string, PXCacheRights> source3 = new Dictionary<string, PXCacheRights>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (PXPrimaryGraphBaseAttribute primaryAttribute in PXPrimaryGraphAttribute.GetPrimaryAttributes(dacType, false))
      {
        foreach (System.Type allGraphType in primaryAttribute.GetAllGraphTypes())
        {
          string fullName = allGraphType.FullName;
          string primaryView = PXPageIndexingService.GetPrimaryView(fullName);
          if (!(GraphHelper.GetGraphView(fullName, primaryView)?.Cache.CacheType != cacheType) && stringSet.Add(fullName))
          {
            foreach (string str in PXSiteMap.Provider.GetScreenIdsByGraphType(allGraphType))
            {
              PXCacheRights rights;
              List<string> invisible2;
              List<string> disabled2;
              PXAccess.GetRights(str, fullName, dacType, out rights, out invisible2, out disabled2);
              source3[str] = rights;
              if (invisible2 != null)
                EnumerableExtensions.AddRange<string>((ISet<string>) source1, (IEnumerable<string>) invisible2);
              if (disabled2 != null)
                EnumerableExtensions.AddRange<string>((ISet<string>) source2, (IEnumerable<string>) disabled2);
            }
          }
        }
      }
      if (!source3.Any<KeyValuePair<string, PXCacheRights>>())
        return;
      string[] array = source3.Keys.ToArray<string>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (((IEnumerable<string>) array).All<string>(GenericInquiryHelpers.AccessRightsFromAllGraphs.\u003C\u003EO.\u003C0\u003E__IsScreenHiddenByFeature ?? (GenericInquiryHelpers.AccessRightsFromAllGraphs.\u003C\u003EO.\u003C0\u003E__IsScreenHiddenByFeature = new Func<string, bool>(PXAccess.IsScreenHiddenByFeature))))
      {
        this.Rights = PXCacheRights.Denied;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        EnumerableExtensions.RemoveRange<string, PXCacheRights>((IDictionary<string, PXCacheRights>) source3, ((IEnumerable<string>) array).Where<string>(GenericInquiryHelpers.AccessRightsFromAllGraphs.\u003C\u003EO.\u003C0\u003E__IsScreenHiddenByFeature ?? (GenericInquiryHelpers.AccessRightsFromAllGraphs.\u003C\u003EO.\u003C0\u003E__IsScreenHiddenByFeature = new Func<string, bool>(PXAccess.IsScreenHiddenByFeature))));
        this.Rights = source3.Values.Append<PXCacheRights>(this.Rights).Min<PXCacheRights>();
      }
      if (source1.Any<string>() && (this.Invisible == null || this.Invisible.Count < source1.Count))
        this.Invisible = source1.ToList<string>();
      if (!source2.Any<string>() || this.Disabled != null && this.Disabled.Count >= source2.Count)
        return;
      this.Disabled = source2.ToList<string>();
    }

    public class Parameters
    {
      public readonly System.Type CacheType;
      public readonly System.Type DacType;
      public readonly string GraphFullName;
      public readonly string ScreenID;

      public Parameters(System.Type cacheType, System.Type dacType, string graphFullName, string screenID)
      {
        this.CacheType = cacheType;
        this.DacType = dacType;
        this.GraphFullName = graphFullName;
        this.ScreenID = screenID;
      }
    }
  }
}
