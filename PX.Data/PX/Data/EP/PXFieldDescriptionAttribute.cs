// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXFieldDescriptionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.EP;

/// <summary>Marks the DAC field that is included in
/// the entity description—the string that describes a particular
/// data records. The description includes values from fields marked with
/// the <tt>PXFieldDescription</tt> attribute joined by comma.</summary>
/// <remarks>
/// When a DAC field is marked with the <see cref="T:PX.Data.PXRefNoteAttribute">PXRefNote</see>
/// attribute, you can generate the <tt>RefNoteSelector</tt> control for it.
/// The value displayed in the control consists of the values of fields
/// marked with the <tt>PXFieldDescription</tt> attribute and joined by comma.
/// </remarks>
/// <example>
/// The code below shows the usage of the <tt>PXFieldDescription</tt> attribute
/// in the definition of the DAC field.
/// <code>
/// [PXDBString(3, IsKey = true, IsFixed = true)]
/// [PXFieldDescription]
/// public virtual String DocType { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class PXFieldDescriptionAttribute : PXEventSubscriberAttribute
{
  /// <summary>
  /// The flag indicating whether the <see langword="null" /> values and empty or whitespace strings are added to the DAC descriptor during its generation.<br />
  /// The default value is <see langword="true" />.
  /// </summary>
  /// <value>
  /// True if include <see langword="null" /> values and empty or whitespace strings in DAC descriptor, false if not.
  /// </value>
  public bool IncludeNullAndEmptyValuesInDacDescriptor { get; set; } = true;
}
