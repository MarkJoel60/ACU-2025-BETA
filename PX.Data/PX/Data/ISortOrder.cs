// Decompiled with JetBrains decompiler
// Type: PX.Data.ISortOrder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The interface that the DAC that corresponds to the rows of a grid must implement to make it possible for the
/// <see cref="T:PX.Data.PXOrderedSelectBase`2">PXOrderedSelectBase</see>-derived classes to enumerate and reorder the rows in the grid.</summary>
/// <remarks>A <see cref="T:PX.Data.PXOrderedSelectBase`2">PXOrderedSelectBase</see>-derived class should be used in a graph for the grid's view.</remarks>
public interface ISortOrder
{
  /// <summary>The order of a row in the grid.</summary>
  /// <value>
  ///   <para>To persist the order of rows in a grid, the <tt>SortOrder</tt> property of the DAC that implements the <tt>ISortOrder</tt> interface must be mapped to the
  /// corresponding database column in the DAC's database table. This column stores the order of rows.</para>
  ///   <para>The <tt>SortOrder</tt> number for a new row is defaulted by <see cref="T:PX.Data.PXOrderedSelectBase`2">PXSelectOrderBase</see>, therefore you don't need any attributes
  /// on the <tt>SortOrder</tt> field to default it. By default, the value of <tt>SortOrder</tt> for a new row is taken from its <tt>LineNbr</tt> field. However,
  /// there is a possibility to define the action on the screen that will insert new row in the middle of a grid and renumber all rows after it.</para>
  /// </value>
  int? SortOrder { get; set; }

  /// <summary>The line number of a row in the grid.</summary>
  int? LineNbr { get; }
}
