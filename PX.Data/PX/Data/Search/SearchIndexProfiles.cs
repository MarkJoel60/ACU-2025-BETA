// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.SearchIndexProfiles
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Descriptor.Attributes;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class SearchIndexProfiles : IPrefetchable, IPXCompanyDependent
{
  public const string ProfileTitle = "Acumatica profile:";

  /// <exclude />
  void IPrefetchable.Prefetch()
  {
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<SearchIndexEntityRank>((PXDataField) new PXDataField<SearchIndexEntityRank.entityType>(), (PXDataField) new PXDataField<SearchIndexEntityRank.entityRank>()))
      this.Profiles.Add(pxDataRecord.GetString(0));
  }

  private HashSet<string> Profiles { get; } = new HashSet<string>();

  public bool IsProfile(System.Type profile) => this.Profiles.Contains(profile.FullName);

  public static SearchIndexProfiles GetSlot()
  {
    return PXDatabase.GetSlot<SearchIndexProfiles>(typeof (SearchIndexEntityRank).FullName, typeof (SearchIndexEntityRank));
  }
}
