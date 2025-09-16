// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReadBranchRestrictedScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class PXReadBranchRestrictedScope : IDisposable
{
  private readonly bool prevReadBranchRestricted;
  private readonly string prevSpecificTable;
  private readonly List<int> prevBranches;

  public string SpecificBranchTable
  {
    get => PXDatabase.SpecificBranchTable;
    set => PXDatabase.SpecificBranchTable = value;
  }

  public PXReadBranchRestrictedScope()
    : this((int?[]) null, (int?[]) null, false)
  {
  }

  public PXReadBranchRestrictedScope(
    int?[] organizationIDs,
    int?[] branchIDs,
    bool restrictByAccessRights = true,
    bool requireAccessForAllSpecified = false)
  {
    this.prevSpecificTable = this.SpecificBranchTable;
    this.prevReadBranchRestricted = PXDatabase.ReadBranchRestricted;
    this.prevBranches = PXDatabase.BranchIDs;
    if (organizationIDs != null || branchIDs != null)
    {
      HashSet<int> intSet1 = new HashSet<int>();
      if (organizationIDs != null && ((IEnumerable<int?>) organizationIDs).Any<int?>())
        EnumerableExtensions.AddRange<int>((ISet<int>) intSet1, ((IEnumerable<int?>) organizationIDs).Select<int?, int[]>((Func<int?, int[]>) (orgID => PXAccess.GetChildBranchIDs(orgID, false))).SelectMany<int[], int>((Func<int[], IEnumerable<int>>) (b => (IEnumerable<int>) b)));
      if (branchIDs != null && ((IEnumerable<int?>) branchIDs).Any<int?>())
      {
        HashSet<int> hashSet = ((IEnumerable<int?>) branchIDs).Where<int?>((Func<int?, bool>) (b => b.HasValue)).Select<int?, int>((Func<int?, int>) (b => b.Value)).Distinct<int>().ToHashSet<int>();
        if (intSet1.Any<int>())
          intSet1.IntersectWith((IEnumerable<int>) hashSet);
        else
          EnumerableExtensions.AddRange<int>((ISet<int>) intSet1, (IEnumerable<int>) hashSet);
      }
      if (intSet1.Any<int>())
      {
        if (restrictByAccessRights | requireAccessForAllSpecified)
        {
          HashSet<int> intSet2 = new HashSet<int>((IEnumerable<int>) intSet1);
          intSet2.IntersectWith((IEnumerable<int>) PXAccess.GetBranchIDs());
          if (requireAccessForAllSpecified && intSet2.Count != intSet1.Count)
          {
            HashSet<int> source = new HashSet<int>((IEnumerable<int>) intSet1);
            source.ExceptWith((IEnumerable<int>) intSet2);
            throw new PXException("You do not have access rights to view or modify data of the {0} branch or branches.", new object[1]
            {
              (object) string.Join(",", source.Select<int, string>((Func<int, string>) (branchID => $"'{PXAccess.GetBranchCD(new int?(branchID)).Trim()}'")))
            });
          }
          PXDatabase.BranchIDs = intSet2.ToList<int>();
        }
        else
          PXDatabase.BranchIDs = intSet1.ToList<int>();
      }
      else
        PXDatabase.BranchIDs = new List<int>() { 0 };
      PXDatabase.ReadBranchRestricted = false;
      this.SpecificBranchTable = (string) null;
    }
    else
      PXDatabase.ReadBranchRestricted = true;
  }

  void IDisposable.Dispose()
  {
    PXDatabase.ReadBranchRestricted = this.prevReadBranchRestricted;
    PXDatabase.BranchIDs = this.prevBranches;
    this.SpecificBranchTable = this.prevSpecificTable;
  }
}
