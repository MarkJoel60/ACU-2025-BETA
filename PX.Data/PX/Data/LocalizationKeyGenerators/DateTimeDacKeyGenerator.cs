// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.DateTimeDacKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
internal class DateTimeDacKeyGenerator : DateTimeBaseKeyGenerator
{
  public DateTimeDacKeyGenerator(PXCache cache)
  {
    if (cache == null)
      throw new PXArgumentException(nameof (cache));
    this.DateKey = cache.BqlTable.FullName + "_Date";
    this.TimeKey = cache.BqlTable.FullName + "_Time";
  }
}
