// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEntryScreenRightsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <summary>Is used for actions on Lists as Entry Points screens (substitute forms) if it is necessary to explicitly define inheritance of access rights.</summary>
/// <remarks>
///   <para>If there is a List as Entry Point form for an entry form in an instance of Acumatica ERP, the access rights to an action on the list screen are
/// automatically inherited from the entry screen if both of the following conditions are met:</para>
///   <list type="bullet">
///     <item><description>The actions in both the list and entry screens are defined on the same primary DAC.</description></item>
///     <item><description>The actions in both the list and entry screens are defined with the same name.</description></item>
///   </list>
///   <para>If any of the conditions is not fulfilled, to inherit the access right, on the list screen, you have to define the <tt>PXEntryScreenRights</tt> attribute
/// and specify the name and the DAC type of the appropriate action on the entry screen.</para>
/// </remarks>
/// <example><para>The code below shows how the &lt;tt&gt;PXEntryScreenRights&lt;/tt&gt; attribute is used in the &lt;tt&gt;PX.Objects.EP.TimecardPrimary&lt;/tt&gt; graph to bind the DAC type and the action name defined in the entry screen for the &lt;tt&gt;Create&lt;/tt&gt; action.</para>
///   <code title="Example" lang="CS">
/// public PXFilter&lt;TimecardFilter&gt; Filter;
/// [PXFilterable()]
/// public PXSelectJoin&lt;TimecardWithTotals, ...&gt; Items;
/// 
///   public PXAction&lt;TimecardFilter&gt; create;
///   [PXButton(SpecialType = PXSpecialButtonType.Insert, Tooltip = "Add New Timecard",
///             ImageKey = PX.Web.UI.Sprite.Main.AddNew)]
///   [PXUIField]
///   [PXEntryScreenRights(typeof(EPTimeCard), nameof(TimeCardMaint.Insert))]
///   protected virtual void Create()
///   {
///     ...
///   }</code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class PXEntryScreenRightsAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  private static readonly IEqualityComparer<Tuple<string, string>> _mapRightsEqualityComparer = (IEqualityComparer<Tuple<string, string>>) new OrdinalStringTupleComparer();
  private static readonly ConcurrentDictionary<string, PXGraph> _entryGraphs = new ConcurrentDictionary<string, PXGraph>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private static readonly ConcurrentDictionary<Tuple<string, string>, Tuple<PXCacheRights, PXCacheRights>> _mapRights = new ConcurrentDictionary<Tuple<string, string>, Tuple<PXCacheRights, PXCacheRights>>(PXEntryScreenRightsAttribute._mapRightsEqualityComparer);
  private readonly System.Type _cacheType;
  private string _memberName;
  private bool _enableRights;
  private bool _viewRights;

  /// <summary>Initializes an instance of the attribute and specifies the DAC type on which <tt>PXAction</tt> is declared in the entry screen.</summary>
  /// <param name="cacheType">The DAC type from which access rights must be inherited.</param>
  public PXEntryScreenRightsAttribute(System.Type cacheType)
    : this(cacheType, (string) null)
  {
  }

  /// <summary>Initializes an instance of the attribute and specifies the DAC type on which PXAction is declared in the entry screen and the name of the corresponding action
  /// in the entry screen from which the access rights should be inherited.</summary>
  /// <param name="cacheType">The DAC type on which the action is declared in the entry screen.</param>
  /// <param name="memberName">The name of the action of the entry screen from which the access rights should be inherited.</param>
  public PXEntryScreenRightsAttribute(System.Type cacheType, string memberName)
  {
    this._cacheType = !(cacheType == (System.Type) null) ? cacheType : throw new ArgumentNullException(nameof (cacheType));
    this._memberName = memberName;
  }

  public override void CacheAttached(PXCache sender)
  {
    if (this._memberName == null)
      this._memberName = this.FieldName;
    string listScreenID = sender.Graph.Accessinfo.ScreenID?.Replace(".", "");
    string entryScreenId = listScreenID == null ? (string) null : PXList.Provider.GetEntryScreenID(listScreenID);
    if (string.IsNullOrEmpty(entryScreenId))
      return;
    Tuple<PXCacheRights, PXCacheRights> orAdd = PXEntryScreenRightsAttribute._mapRights.GetOrAdd(Tuple.Create<string, string>(entryScreenId, this._memberName), (Func<Tuple<string, string>, Tuple<PXCacheRights, PXCacheRights>>) (k => PXEntryScreenRightsAttribute.GetMapRights(k.Item1, k.Item2, this._cacheType)));
    PXUIFieldAttribute pxuiFieldAttribute = new PXUIFieldAttribute();
    pxuiFieldAttribute.FieldName = this._memberName;
    pxuiFieldAttribute.MapEnableRights = orAdd.Item1;
    pxuiFieldAttribute.MapViewRights = orAdd.Item2;
    PXUIFieldAttribute attribute = pxuiFieldAttribute;
    PXAccess.Secure(sender.Graph.Caches[this._cacheType], (PXEventSubscriberAttribute) attribute);
    this._enableRights = attribute.EnableRights;
    this._viewRights = attribute.ViewRights;
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!this._viewRights && e.ExternalCall && e.ReturnState is PXFieldState returnState)
      returnState.Visibility = PXUIVisibility.HiddenByAccessRights;
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, enabled: this._enableRights ? new bool?() : new bool?(false), visible: this._viewRights ? new bool?() : new bool?(false), visibility: this._viewRights ? PXUIVisibility.Undefined : PXUIVisibility.HiddenByAccessRights);
  }

  private static PXGraph CreateGraph(string screenID)
  {
    string graphType = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(screenID).GraphType;
    if (string.IsNullOrEmpty(graphType))
      return (PXGraph) null;
    System.Type type = PXBuildManager.GetType(graphType, true);
    using (new PXScreenIDScope(screenID))
      return PXGraph.CreateInstance(type);
  }

  private static Tuple<PXCacheRights, PXCacheRights> GetMapRights(
    string screenID,
    string memberName,
    System.Type cacheType)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXGraph orAdd = PXEntryScreenRightsAttribute._entryGraphs.GetOrAdd(screenID, PXEntryScreenRightsAttribute.\u003C\u003EO.\u003C0\u003E__CreateGraph ?? (PXEntryScreenRightsAttribute.\u003C\u003EO.\u003C0\u003E__CreateGraph = new Func<string, PXGraph>(PXEntryScreenRightsAttribute.CreateGraph)));
    if (orAdd == null)
      return Tuple.Create<PXCacheRights, PXCacheRights>(PXCacheRights.Update, PXCacheRights.Select);
    PXAction action = orAdd.Actions[memberName];
    IEnumerable<PXUIFieldAttribute> pxuiFieldAttributes = (action == null ? (IEnumerable) orAdd.Caches[cacheType].GetAttributesReadonly(memberName) : (IEnumerable) action.Attributes).OfType<PXUIFieldAttribute>();
    PXCacheRights pxCacheRights1 = PXCacheRights.Delete;
    PXCacheRights pxCacheRights2 = PXCacheRights.Delete;
    foreach (PXUIFieldAttribute pxuiFieldAttribute in pxuiFieldAttributes)
    {
      pxCacheRights1 = pxuiFieldAttribute.MapEnableRights < pxCacheRights1 ? pxuiFieldAttribute.MapEnableRights : pxCacheRights1;
      pxCacheRights2 = pxuiFieldAttribute.MapViewRights < pxCacheRights2 ? pxuiFieldAttribute.MapViewRights : pxCacheRights2;
    }
    return Tuple.Create<PXCacheRights, PXCacheRights>(pxCacheRights1, pxCacheRights2);
  }
}
