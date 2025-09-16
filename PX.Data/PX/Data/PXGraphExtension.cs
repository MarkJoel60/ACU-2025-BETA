// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.WorkflowAPI;

#nullable disable
namespace PX.Data;

/// <summary>A base class for graph extensions. You use graph extensions
/// to define a custom business logic for an existing graph or graph extension.</summary>
/// <remarks>
/// A graph extension must include the public static IsActive method
/// with no parameters and the <see cref="T:System.Boolean" /> return type. For example, you can make an extension active
/// if the feature for which this extension is needed is enabled.
/// Extensions that are constantly active reduce performance. For more information,
/// see <a href="Help?ScreenId=ShowWiki&amp;pageid=cd70b408-b389-4bd8-8502-3d9c12b11112" target="_blank">To Enable a Graph Extension Conditionally (IsActive)</a>.
/// </remarks>
public abstract class PXGraphExtension
{
  /// <summary>Initializes the graph extension.</summary>
  public virtual void Initialize()
  {
  }

  /// <summary>Configures the graph extension.</summary>
  /// <param name="configuration">The screen configuration for the workflow.</param>
  /// <remarks>
  /// We do not recommend that you create a graph instance or access the base graph
  /// in this method because it can lead to deadlocks. To access a graph or a DAC property, use database slots.
  /// </remarks>
  public virtual void Configure(PXScreenConfiguration configuration)
  {
  }
}
