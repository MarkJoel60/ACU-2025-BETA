// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapNodeSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

/// <summary>The attribute that is added to the field that stores the screen ID and is used to display the site map location.</summary>
/// <remarks>
///   <tt>PXSiteMapNodeSelector</tt> shows the list of screens, but does not contain <strong>Workspace</strong> and <strong>Category</strong> columns in the
/// selector.
///   If you need to display these columns, add a reference to the <tt>PX.Web.UI.Frameset</tt> namespace and use the <tt>SiteMapSelectorAttribute</tt>
/// attribute.
/// </remarks>
/// <example><para>The following example shows the use of the attribute.</para>
///   <code title="Example" lang="CS">
/// #region ScreenID
/// public abstract class screenID : PX.Data.BQL.BqlString.Field&lt;screenID&gt; { }
/// 
/// [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
/// [PXDefault]
/// [PXUIField(DisplayName = "Screen Name", Visibility = PXUIVisibility.SelectorVisible)]
/// [PXSiteMapNodeSelector]
/// public virtual string ScreenID { get; set; }
/// #endregion</code>
/// <para>You should use the PXSelector control in the corresponding ASPX page, as shown in the following code example.</para>
///   <code title="Example2" description="" lang="XML">
/// &lt;px:PXSelector ID="edScreen" runat="server" DataField="ScreenID"
///     DisplayMode="Text" FilterByAllFields="true" CommitChanges="True" /&gt;</code>
/// </example>
public class PXSiteMapNodeSelectorAttribute : PXCustomSelectorAttribute
{
  protected Func<PX.SM.SiteMap, bool> _isRestricted = (Func<PX.SM.SiteMap, bool>) (s => false);

  /// <exclude />
  public PXSiteMapNodeSelectorAttribute()
    : base(PXSiteMapNodeSelectorAttribute.GetSelectorType(), PXSiteMapNodeSelectorAttribute.GetScreenIDType(), typeof (PX.SM.SiteMap.title))
  {
    this.DescriptionField = typeof (PX.SM.SiteMap.title);
  }

  /// <exclude />
  public PXSiteMapNodeSelectorAttribute(
    System.Type joinedTableType,
    System.Type screenIDType,
    System.Type notNullableFieldType)
    : base(PXSiteMapNodeSelectorAttribute.GetSelectorType(joinedTableType, screenIDType, notNullableFieldType), PXSiteMapNodeSelectorAttribute.GetScreenIDType(), typeof (PX.SM.SiteMap.title))
  {
    this.DescriptionField = typeof (PX.SM.SiteMap.title);
  }

  private static System.Type GetSelectorType(
    System.Type joinedTableType,
    System.Type screenIDType,
    System.Type notNullableFieldType)
  {
    return BqlTemplate.OfCommand<Search5<BqlPlaceholder.A, LeftJoin<BqlPlaceholder.B, On<BqlPlaceholder.A, Equal<BqlPlaceholder.C>>>, Where<BqlPlaceholder.D, IsNotNull>, Aggregate<GroupBy<BqlPlaceholder.A>>, OrderBy<Asc<PX.SM.SiteMap.title>>>>.Replace<BqlPlaceholder.A>(PXSiteMapNodeSelectorAttribute.GetScreenIDType()).Replace<BqlPlaceholder.B>(joinedTableType).Replace<BqlPlaceholder.C>(screenIDType).Replace<BqlPlaceholder.D>(notNullableFieldType).ToType();
  }

  private static System.Type GetSelectorType()
  {
    return BqlTemplate.OfCommand<Search<BqlPlaceholder.A, Where<BqlPlaceholder.A, IsNotNull, And<PX.SM.SiteMap.url, IsNotNull>>>>.Replace<BqlPlaceholder.A>(PXSiteMapNodeSelectorAttribute.GetScreenIDType()).ToType();
  }

  private static System.Type GetScreenIDType()
  {
    return !PXSiteMap.IsPortal ? typeof (PX.SM.SiteMap.screenID) : typeof (PX.SM.PortalMap.screenID);
  }

  /// <exclude />
  public PXSiteMapNodeSelectorAttribute(System.Type type, params System.Type[] fieldList)
    : base(type, fieldList)
  {
  }

  /// <exclude />
  public static void SetRestriction(
    PXCache cache,
    object data,
    string name,
    Func<PX.SM.SiteMap, bool> isRestricted)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (PXSiteMapNodeSelectorAttribute selectorAttribute in cache.GetAttributesOfType<PXSiteMapNodeSelectorAttribute>(data, name))
    {
      if (selectorAttribute != null)
        selectorAttribute._isRestricted = isRestricted;
    }
  }

  /// <exclude />
  public static void SetRestriction<Field>(
    PXCache cache,
    object data,
    Func<PX.SM.SiteMap, bool> isRestricted)
    where Field : IBqlField
  {
    PXSiteMapNodeSelectorAttribute.SetRestriction(cache, data, typeof (Field).Name, isRestricted);
  }

  protected virtual IEnumerable GetRecords() => (IEnumerable) null;
}
