// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.IGenericInquiryReferenceInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry;

internal interface IGenericInquiryReferenceInfoProvider
{
  /// <summary>
  /// Checks whether GI with identifier <paramref name="designIdToCheckForReferences" />
  /// has reference to GI with identifier <paramref name="designIdToLookFor" />
  /// </summary>
  bool HasReferenceTo(Guid designIdToCheckForReferences, Guid designIdToLookFor);

  /// <summary>
  /// Gets GIs that have references to GI with identifier <paramref name="designIdToLookFor" />
  /// </summary>
  IEnumerable<(string Name, Guid designId)> GetReferencesTo(Guid designIdToLookFor);

  /// <summary>
  /// Gets GIs that the GI with identifier <paramref name="designIdToLookFor" /> has references to
  /// </summary>
  IEnumerable<(string Name, Guid designId)> GetReferencesFrom(Guid designIdToLookFor);
}
