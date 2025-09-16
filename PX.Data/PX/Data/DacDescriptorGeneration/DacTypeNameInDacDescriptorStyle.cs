// Decompiled with JetBrains decompiler
// Type: PX.Data.DacDescriptorGeneration.DacTypeNameInDacDescriptorStyle
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DacDescriptorGeneration;

/// <summary>Styles of DAC type name in a DAC descriptor.</summary>
public enum DacTypeNameInDacDescriptorStyle
{
  /// <summary>
  /// Use <see cref="P:System.Type.FullName" /> as a name of the DAC type in the DAC descriptor.
  /// </summary>
  FullTypeName,
  /// <summary>
  /// Use <see cref="!:Type.Name" /> as a name of the DAC type in the DAC descriptor.
  /// </summary>
  ShortTypeName,
  /// <summary>
  /// Use a user-friendly type name from the <see cref="T:PX.Data.PXCacheNameAttribute" /> as a name of the DAC type in the DAC descriptor.<br />
  /// If there is no <see cref="T:PX.Data.PXCacheNameAttribute" /> on a DAC type, then <see cref="P:System.Type.FullName" /> will be used.
  /// </summary>
  UserFriendlyTypeName,
}
