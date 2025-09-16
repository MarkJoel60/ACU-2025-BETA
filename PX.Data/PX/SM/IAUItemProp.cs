// Decompiled with JetBrains decompiler
// Type: PX.SM.IAUItemProp
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public interface IAUItemProp : IBqlTable, IBqlTableSystemDataStorage
{
  string PropertyID { get; set; }

  string PropertyName { get; set; }

  string OriginalValue { get; set; }

  string OverrideValue { get; set; }

  string PropertyValue { get; set; }

  bool? IsOverride { get; set; }

  bool? Inherit { get; set; }

  int? SortOrder { get; set; }
}
