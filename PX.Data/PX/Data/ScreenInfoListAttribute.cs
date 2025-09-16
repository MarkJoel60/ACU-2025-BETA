// Decompiled with JetBrains decompiler
// Type: PX.Data.ScreenInfoListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Base class for all string list attributes that depend on screen meta information.
/// </summary>
public abstract class ScreenInfoListAttribute : PXStringListAttribute
{
  private readonly System.Type _itemType;
  private readonly string _screenIdFieldName;

  /// <param name="nodeIdFieldType">Screen node id.</param>
  protected ScreenInfoListAttribute(System.Type screenIdFieldType)
  {
    if (screenIdFieldType == (System.Type) null)
      throw new PXArgumentException(nameof (screenIdFieldType), "The argument cannot be null.");
    this._itemType = screenIdFieldType.IsNested && typeof (IBqlField).IsAssignableFrom(screenIdFieldType) ? BqlCommand.GetItemType(screenIdFieldType) : throw new PXArgumentException(nameof (screenIdFieldType), "A foreign key reference cannot be created from the type '{0}'.", new object[1]
    {
      (object) screenIdFieldType
    });
    this._screenIdFieldName = screenIdFieldType.Name;
  }

  protected PXSiteMapNode GetSiteMapNode(PXGraph graph)
  {
    return ScreenInfoHelper.GetSiteMapNode(graph.Caches[this._itemType], this._screenIdFieldName);
  }

  protected string GetScreenID(PXGraph graph)
  {
    return ScreenInfoHelper.GetScreenID(graph.Caches[this._itemType], this._screenIdFieldName);
  }

  protected string GetRawScreenID(PXCache sender)
  {
    return ScreenInfoHelper.GetRawScreenID(sender.Graph.Caches[this._itemType], this._screenIdFieldName);
  }

  protected PXSiteMap.ScreenInfo GetScreenInfo(PXCache sender)
  {
    return ScreenInfoHelper.GetScreenInfo(sender.Graph.Caches[this._itemType], this._screenIdFieldName);
  }

  internal ScreenMetadata GetScreenMetadata(PXCache sender)
  {
    return ScreenInfoHelper.GetScreenMetadata(sender.Graph.Caches[this._itemType], this._screenIdFieldName);
  }
}
