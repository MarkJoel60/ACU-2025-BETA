// Decompiled with JetBrains decompiler
// Type: PX.Data.PXHiddenAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Hides a data access class (DAC), the business logic controller (graph), or the view from the selectors of DACs and graphs as well as from the web wervice API clients.
/// </summary>
/// <remarks>
/// If you want an object to be visible to the web service APIs, you can set the <see cref="P:PX.Data.PXHiddenAttribute.ServiceVisible" /> property to <see langword="true" />.
/// The hidden feature of a class is not inherited. If you derive a class from another class with <see cref="T:PX.Data.PXHiddenAttribute" />, the derived class is not hidden.
/// </remarks>
/// <example><para>In the example below, the attribute is placed on the DAC declaration.</para>
/// <code title="Example" lang="CS">
/// [Serializable]
/// [PXHidden]
/// public partial class ActivitySource : PXBqlTable, IBqlTable { ... }</code>
/// <code title="Example2" description="In the example below, the attribute is placed on the graph declaration." groupname="Example" lang="CS">
/// [PXHidden()]
/// public class CAReleaseProcess : PXGraph&lt;CAReleaseProcess&gt; { ... }</code>
/// <code title="Example3" description="In the example below, the attribute is placed on the view declaration in some graph." groupname="Example2" lang="CS">
/// [PXHidden]
/// public PXSelect&lt;CurrencyInfo&gt; CurrencyInfoSelect;</code>
/// </example>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
public sealed class PXHiddenAttribute : Attribute
{
  private bool _ServiceVisible;

  /// <summary>Gets or sets the value that indicates whether the object
  /// marked with the attribute is visible to the Web Service API (in
  /// particular, to the Report Designer). By default the property
  /// equals <tt>false</tt>, and the object is hidden from all
  /// selectors.</summary>
  public bool ServiceVisible
  {
    get => this._ServiceVisible;
    set => this._ServiceVisible = value;
  }

  /// <exclude />
  public System.Type Target { get; set; }
}
