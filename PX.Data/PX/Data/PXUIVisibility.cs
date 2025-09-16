// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIVisibility
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Is used to define where an input control for a field appears in the user interface.</summary>
/// <remarks>This enumeration can be used to define the following:
/// <list type="bullet">
/// <item><description>The visibility of an input
/// control or a grid column at runtime</description></item>
/// <item><description>The default set of
/// columns displayed in the lookup table of the <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see> input
/// control</description></item>
/// <item><description>The set of columns that are
/// automatically added to the grid control</description></item>
/// </list>
/// </remarks>
[Flags]
public enum PXUIVisibility
{
  /// <summary>This value is for internal use. Do not use it.</summary>
  Undefined = 0,
  /// <summary>The field input control or column does not appear in the user interface.</summary>
  /// <example>
  /// <code>
  /// [PXUIField(Visibility = PXUIVisibility.Invisible, Visible = false)]
  /// public string MappingKey { get; set; }
  /// </code>
  /// </example>
  Invisible = 1,
  /// <summary>The field input control or column is visible in the user interface.</summary>
  /// <example>
  /// <code>
  /// PXUIFieldAttribute.SetVisibility&lt;APRegister.vendorID&gt;(Documents.Cache, null, PXUIVisibility.Visible);
  /// </code>
  /// </example>
  Visible = 3,
  SelectorVisible = 7,
  /// <summary>
  /// The field column is automatically added to the grid control
  /// if the <tt>AutoGenerateColumns</tt> property of <tt>PXGrid</tt> is set to <tt>AppendDynamic</tt> (in the Classic UI)
  /// or
  /// if the <tt>generateColumns</tt> property of the <tt>qp-grid</tt> control is set to
  /// <tt>GridColumnGeneration.AppendDynamic</tt> (in the Modern UI).
  /// </summary>
  /// <example>
  /// <code>
  ///  [PXUIField(DisplayName = "Operation", Visibility = PXUIVisibility.Dynamic)]
  /// public virtual string Operation { get; set; }
  /// </code>
  /// </example>
  Dynamic = 9,
  /// <summary>This value is for internal use. Do not use it.</summary>
  Service = 19, // 0x00000013
  /// <summary>This value is for internal use. Do not use it.</summary>
  HiddenByAccessRights = 33, // 0x00000021
}
