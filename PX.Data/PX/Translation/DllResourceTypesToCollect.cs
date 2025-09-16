// Decompiled with JetBrains decompiler
// Type: PX.Translation.DllResourceTypesToCollect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Translation;

/// <summary>
/// Types of localizable resources present in DLLs that should be collected by strings collection process.
/// </summary>
[Flags]
internal enum DllResourceTypesToCollect
{
  None = 0,
  /// <summary>Localizable string constant.</summary>
  Message = 1,
  /// <summary>Localizable XML comment.</summary>
  XmlComment = 2,
  /// <summary>Display Name.</summary>
  DisplayName = 4,
  /// <summary>
  /// Labels from <see cref="T:PX.Data.PXStringListAttribute" /> and <see cref="T:PX.Data.PXIntListAttribute" /> attributes
  /// </summary>
  ListAttribute = 8,
  /// <summary>
  /// Text from <see cref="!:PXUILabelAttribute" />
  /// </summary>
  LabelAttribute = 16, // 0x00000010
  /// <summary>
  /// All types of localizable resources that can be discovered in DLL.
  /// </summary>
  All = LabelAttribute | ListAttribute | DisplayName | XmlComment | Message, // 0x0000001F
}
