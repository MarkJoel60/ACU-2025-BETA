// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDynamicMaskAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that the input values of the field are restricted by the dynamic mask that is specified in <see cref="T:PX.Data.BqlCommand" /> in constructor. For the UI, the attribute
/// restricts entering of the symbols that doesn't match the mask. For the contact-based API, the mask could be disabled, enabled for field selecting, or checked
/// during row persisting as specified by the <see cref="P:PX.Data.PXDynamicMaskAttribute.CBApiValidationType" /> property.</summary>
/// <example>
///   <code title="Example" description="" lang="C#">
/// [PXDBString(20)]
/// [PXUIField(DisplayName = "Postal Code")]
/// [PXDynamicMask(typeof(Search&lt;Country.zipCodeMask, Where&lt;Country.countryID, Equal&lt;Current&lt;Address.countryID&gt;&gt;&gt;&gt;))]
/// public virtual string PostalCode { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDynamicMaskAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXRowPersistingSubscriber
{
  protected BqlCommand _MaskSearch;
  protected string _DefaultMask = string.Empty;

  /// <summary>Gets or sets the default mask that is used if the dynamic mask returns nothing.</summary>
  /// <value>The default value is <see cref="F:System.String.Empty" />, which means that no mask is applied.</value>
  public virtual string DefaultMask
  {
    get => this._DefaultMask;
    set => this._DefaultMask = value;
  }

  /// <summary>Specifies how the mask validation works for the contact-based API import (that is, for the graphs with <see cref="F:PX.Data.PXGraph.IsContractBasedAPI" /> set to <see langword="true"></see>).</summary>
  /// <value>The default value is <see cref="F:PX.Data.PXDynamicMaskAttribute.ValidationType.RowPersisting" />.</value>
  public PXDynamicMaskAttribute.ValidationType CBApiValidationType { get; set; } = PXDynamicMaskAttribute.ValidationType.RowPersisting;

  /// <summary>Specifies how the mask validation works for the copied-and-pasted values (that is, for the graphs with <see cref="F:PX.Data.PXGraph.IsCopyPasteContext" /> set to <see langword="true"></see>).</summary>
  /// <value>The default value is <see cref="F:PX.Data.PXDynamicMaskAttribute.ValidationType.None" />.</value>
  public PXDynamicMaskAttribute.ValidationType CopyPasteValidationType { get; set; }

  /// <summary>Creates an instance of the attribute.</summary>
  /// <param name="maskSearch">A search BQL command that specifies a mask for the attribute.</param>
  public PXDynamicMaskAttribute(System.Type maskSearch)
  {
    this._MaskSearch = !(maskSearch == (System.Type) null) && typeof (IBqlSearch).IsAssignableFrom(maskSearch) ? BqlCommand.CreateInstance(maskSearch) : throw new PXArgumentException(nameof (maskSearch), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
    {
      (object) maskSearch
    });
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered || sender.Graph.IsContractBasedAPI && this.CBApiValidationType != PXDynamicMaskAttribute.ValidationType.FieldSelecting || sender.Graph.IsCopyPasteContext && this.CopyPasteValidationType != PXDynamicMaskAttribute.ValidationType.FieldSelecting)
      return;
    string mask;
    this.TryGetMask(sender.Graph, e.Row, out mask);
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(), mask, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  /// <exclude />
  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (sender.Graph.IsContractBasedAPI && this.CBApiValidationType != PXDynamicMaskAttribute.ValidationType.RowPersisting || sender.Graph.IsCopyPasteContext && this.CopyPasteValidationType != PXDynamicMaskAttribute.ValidationType.FieldSelecting)
      return;
    string mask;
    this.TryGetMask(sender.Graph, e.Row, out mask);
    if (mask != null && mask != string.Empty && sender.GetValue(e.Row, this.FieldOrdinal) is string b && !Mask.Format(mask, b).Trim().OrdinalEquals(b))
      throw new PXRowPersistingException(this.FieldName, (object) b, "The entered string '{0}' does not match the required input mask '{1}'.", new object[2]
      {
        (object) b,
        (object) mask
      });
  }

  /// <summary>Tries to get the mask for the specified field.</summary>
  /// <param name="graph">A graph instance.</param>
  /// <param name="row"></param>
  /// <param name="mask">The found validation mask, or <see cref="P:PX.Data.PXDynamicMaskAttribute.DefaultMask" />, depending on the returned value.</param>
  /// <returns>Returns <see langword="true"></see> if the mask is found, otherwise returns <see langword="false"></see>, which means that <see cref="P:PX.Data.PXDynamicMaskAttribute.DefaultMask" /> is used.</returns>
  public bool TryGetMask(PXGraph graph, object row, out string mask)
  {
    PXView view = graph.TypedViews.GetView(this._MaskSearch, true);
    object data = view.SelectSingleBound(new object[1]
    {
      row
    });
    if (data != null)
    {
      System.Type field = ((IBqlSearch) this._MaskSearch).GetField();
      if (data is PXResult pxResult)
        data = pxResult[BqlCommand.GetItemType(field)];
      mask = PXFieldState.UnwrapValue(view.Cache.GetValueExt(data, field.Name)) as string;
      if (mask != null)
        return true;
    }
    mask = this.DefaultMask;
    return false;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    sender.SetAltered(this._FieldName, true);
    base.CacheAttached(sender);
  }

  /// <summary>
  /// Specifies how the mask validation works for the contact-based API import
  /// (that is, for the graphs with <see cref="F:PX.Data.PXGraph.IsContractBasedAPI" /> set to <see langword="true" />)
  /// or for the copy-paste context (that is, for the graphs with <see cref="F:PX.Data.PXGraph.IsCopyPasteContext" /> set to <see langword="true" />).
  /// </summary>
  public enum ValidationType
  {
    /// <summary>The mask validation doesn't work.</summary>
    None,
    /// <summary>
    /// The mask validation works during <see cref="M:PX.Data.IPXFieldSelectingSubscriber.FieldSelecting(PX.Data.PXCache,PX.Data.PXFieldSelectingEventArgs)" />.
    /// </summary>
    FieldSelecting,
    /// <summary>
    /// The mask validation works during <see cref="M:PX.Data.IPXRowPersistingSubscriber.RowPersisting(PX.Data.PXCache,PX.Data.PXRowPersistingEventArgs)" />.
    /// </summary>
    RowPersisting,
  }
}
