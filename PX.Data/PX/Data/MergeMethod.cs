// Decompiled with JetBrains decompiler
// Type: PX.Data.MergeMethod
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines the ways attributes of the DAC and the DAC extension are merged.
/// The enumeration is used in the <tt>PXMergeAttributes</tt> attribute.</summary>
public enum MergeMethod
{
  /// <summary>Is used to add custom attributes to the existing ones.</summary>
  Append,
  /// <summary>Forces the system to use custom attributes instead of the existing ones. This option is used by default if you do not specify the <tt>PXMergeAttributes</tt>
  /// attribute on the customized field.</summary>
  Replace,
  /// <summary>Makes the system apply the union of the custom and existing attributes to the customized field.</summary>
  Merge,
}
