// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.DateTimeCacheAttachedKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
internal class DateTimeCacheAttachedKeyGenerator : DateTimeBaseKeyGenerator
{
  public DateTimeCacheAttachedKeyGenerator(PXCache cache)
  {
    if (cache == null)
      throw new PXArgumentException(nameof (cache));
    string str = $"{cache.BqlTable.FullName} {cache.Graph.GetType().FullName}";
    this.DateKey = str + "_Date";
    this.TimeKey = str + "_Time";
  }
}
