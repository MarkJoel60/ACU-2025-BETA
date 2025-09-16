// Decompiled with JetBrains decompiler
// Type: PX.Data.AddingNewSiteMapNodeEventArgs
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class AddingNewSiteMapNodeEventArgs : EventArgs
{
  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Data.AddingNewSiteMapNodeEventArgs" /> class with its <tt>Cancel</tt>
  /// property set to <tt>false</tt>.
  /// </summary>
  internal AddingNewSiteMapNodeEventArgs(PXSiteMapNode node)
    : this(node, false)
  {
  }

  /// <summary>
  /// </summary>
  /// <param name="node"></param>
  /// 
  ///             Initializes a new instance of the
  ///             <see cref="T:PX.Data.AddingNewSiteMapNodeEventArgs" />
  /// 
  ///             class with the Cancel property set to the specified value.
  ///             <param name="cancel"></param>
  internal AddingNewSiteMapNodeEventArgs(PXSiteMapNode node, bool cancel)
  {
    this.Node = node ?? throw new ArgumentNullException(nameof (node));
    this.Cancel = cancel;
  }

  /// <summary>
  /// Gets or sets a value indicating whether the event should be canceled.
  /// </summary>
  public bool Cancel { get; set; }

  /// <summary>Gets the node that is being added.</summary>
  public PXSiteMapNode Node { get; }
}
