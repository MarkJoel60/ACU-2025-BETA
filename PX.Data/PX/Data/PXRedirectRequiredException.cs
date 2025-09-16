// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRedirectRequiredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>Opens the specified application page in either the same window or a new one.</summary>
/// <remarks>By default, the user is redirected in the same window.</remarks>
/// <example><para>In the action handler, you create a new instance of the ProductMaint graph that is the controller for the product editing page. In the graph, you set the Current property of the Product cache to the product if it is found by the specified ID. If the current data record is set for the cache object, you throw the PXRedirectRequiredException to open the page with the current data record displayed.</para>
/// <code title="Example" lang="CS">
/// public PXAction&lt;SupplierFilter&gt; ViewProduct;
/// [PXButton]
/// protected virtual void viewProduct()
/// {
///     SupplierProduct row = SupplierProducts.Current;
///     // Creating the instance of the graph
///     ProductMaint graph = PXGraph.CreateInstance&lt;ProductMaint&gt;();
///     // Setting the current product for the graph
///     graph.Products.Current = graph.Products.Search&lt;Product.productID&gt;(
///     row.ProductID);
///     // If the product is found by its ID, throw an exception to open
///     // a new window (tab) in the browser
///     if (graph.Products.Current != null)
///     {
///         throw new PXRedirectRequiredException(graph, true, "Product Details");
///     }
/// }</code>
/// </example>
public class PXRedirectRequiredException : PXBaseRedirectException
{
  protected PXGraph _Graph;
  protected string _Url;
  public string ScreenId;
  public string TargetFrame;

  public PXGraph Graph
  {
    get => this._Graph;
    private set
    {
      this._Graph = value;
      PXBaseRedirectException.populateGraphTimeStamp(this._Graph);
      this._Graph.EnsureIfArchived();
      PXReusableGraphFactory.SetRedirect();
    }
  }

  public string Url => this._Url;

  public PXRedirectRequiredException(PXGraph graph, string message)
    : base(message)
  {
    this.Graph = graph;
  }

  public PXRedirectRequiredException(PXGraph graph, string message, bool repaintControls)
    : base(message, repaintControls)
  {
    this.Graph = graph;
  }

  public PXRedirectRequiredException(PXGraph graph, string format, params object[] args)
    : base(format, args)
  {
    this.Graph = graph;
  }

  public PXRedirectRequiredException(PXGraph graph, bool newWindow, string message)
    : base(message)
  {
    this.Graph = graph;
    if (!newWindow)
      return;
    this.Mode = PXBaseRedirectException.WindowMode.New;
  }

  public PXRedirectRequiredException(
    string url,
    PXGraph graph,
    PXBaseRedirectException.WindowMode windowMode,
    string message)
    : base(message)
  {
    this._Url = url;
    this.Graph = graph;
    this.Mode = windowMode;
  }

  public PXRedirectRequiredException(
    PXGraph graph,
    bool newWindow,
    string format,
    params object[] args)
    : base(format, args)
  {
    this.Graph = graph;
    if (!newWindow)
      return;
    this.Mode = PXBaseRedirectException.WindowMode.New;
  }

  public PXRedirectRequiredException(string url, PXGraph graph, string message)
    : this(graph, message)
  {
    this._Url = url;
  }

  public PXRedirectRequiredException(
    string url,
    PXGraph graph,
    string message,
    bool repaintControls)
    : this(graph, message)
  {
    this._Url = url;
    this.RepaintControls = repaintControls;
  }

  public PXRedirectRequiredException(
    string url,
    PXGraph graph,
    string format,
    params object[] args)
    : this(graph, format, args)
  {
    this._Url = url;
  }

  public PXRedirectRequiredException(string url, PXGraph graph, bool newWindow, string message)
    : this(graph, newWindow, message)
  {
    this._Url = url;
  }

  public PXRedirectRequiredException(
    string url,
    PXGraph graph,
    bool newWindow,
    string format,
    params object[] args)
    : this(graph, newWindow, format, args)
  {
    this._Url = url;
  }

  public PXRedirectRequiredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXRedirectRequiredException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXRedirectRequiredException>(this, info);
    base.GetObjectData(info, context);
  }
}
