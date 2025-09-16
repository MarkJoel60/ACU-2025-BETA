// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAttributeLevel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public enum PXAttributeLevel
{
  /// <summary>
  /// Original attributes of a DAC.
  /// Cannot be obtained directly.<para />
  /// Are instantiated once for each type on
  /// first instantiating of its <see cref="T:PX.Data.PXCache" /><para />
  /// Attributes of this level cannot be modified in runtime.
  /// </summary>
  Type,
  /// <summary>
  /// Attributes that copied from attributes of <see cref="F:PX.Data.PXAttributeLevel.Type" />,
  /// merged with <see cref="T:PX.Data.PXGraph" /> attributes, relevant to the DAC,
  /// and then linked to a certain <see cref="T:PX.Data.PXCache" /> instance.<para />
  /// Are instantiated on every <see cref="T:PX.Data.PXCache" /> instantiating
  /// by copying of <see cref="F:PX.Data.PXAttributeLevel.Type" /> attributes.<para />
  /// Attributes of this level can be modified in runtime, usually by calling
  /// attributes static methods that have signature corresponding to
  /// <code>"AttributeName.SetSomeValue(PXCache sender, object row, SomeType someValue)"</code>
  /// and passing <code>null</code> as a second parameter.
  /// </summary>
  Cache,
  /// <summary>
  /// Attributes that copied from attributes of <see cref="F:PX.Data.PXAttributeLevel.Cache" />,
  /// and then linked to a certain row instance in a <see cref="T:PX.Data.PXCache" />.<para />
  /// Are instantiated by copying of <see cref="F:PX.Data.PXAttributeLevel.Cache" /> attributes
  /// at certain data row attributes demanding.<para />
  /// Attributes of this level can be modified in runtime, usually by calling
  /// attributes static methods that have signature corresponding to
  /// <code>"AttributeName.SetSomeValue(PXCache sender, object row, SomeType someValue)"</code>
  /// and passing a certain row as a second parameter.
  /// </summary>
  Item,
}
