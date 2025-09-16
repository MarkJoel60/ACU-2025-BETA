// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UDFHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.CR.Extensions.CRCreateActions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public static class UDFHelper
{
  /// <summary>
  /// Return list of UDF fields for <paramref name="screenID" /> and <paramref name="udfTypeField" />
  /// </summary>
  /// <param name="screenID"></param>
  /// <param name="udfTypeField"></param>
  /// <returns></returns>
  public static IEnumerable<KeyValueHelper.ScreenAttribute> GetUDFFields(
    string screenID,
    string udfTypeField = null)
  {
    List<string> attributes = new List<string>();
    KeyValueHelper.Definition def = KeyValueHelper.Def;
    IOrderedEnumerable<KeyValueHelper.ScreenAttribute> orderedEnumerable;
    if (def == null)
    {
      orderedEnumerable = (IOrderedEnumerable<KeyValueHelper.ScreenAttribute>) null;
    }
    else
    {
      KeyValueHelper.ScreenAttribute[] attributes1 = def.GetAttributes(ScreenHelper.UnmaskScreenID(screenID));
      if (attributes1 == null)
      {
        orderedEnumerable = (IOrderedEnumerable<KeyValueHelper.ScreenAttribute>) null;
      }
      else
      {
        IEnumerable<KeyValueHelper.ScreenAttribute> source = ((IEnumerable<KeyValueHelper.ScreenAttribute>) attributes1).Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (f => string.Equals(f.TypeValue, udfTypeField, StringComparison.CurrentCultureIgnoreCase) || string.IsNullOrEmpty(f.TypeValue)));
        orderedEnumerable = source != null ? source.OrderBy<KeyValueHelper.ScreenAttribute, string>((Func<KeyValueHelper.ScreenAttribute, string>) (f => f.AttributeID)).ThenByDescending<KeyValueHelper.ScreenAttribute, string>((Func<KeyValueHelper.ScreenAttribute, string>) (f => f.TypeValue)) : (IOrderedEnumerable<KeyValueHelper.ScreenAttribute>) null;
      }
    }
    foreach (KeyValueHelper.ScreenAttribute udfField in (IEnumerable<KeyValueHelper.ScreenAttribute>) orderedEnumerable)
    {
      if (!attributes.Contains(udfField.AttributeID))
      {
        attributes.Add(udfField.AttributeID);
        yield return udfField;
      }
    }
  }

  /// <summary>
  /// Return list of UDF fields for <paramref name="graphType" /> and <paramref name="udfTypeField" />
  /// </summary>
  /// <param name="graphType"></param>
  /// <param name="udfTypeField"></param>
  /// <returns></returns>
  public static IEnumerable<KeyValueHelper.ScreenAttribute> GetUDFFields(
    System.Type graphType,
    string udfTypeField = null)
  {
    return UDFHelper.GetUDFFields(PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graphType)?.ScreenID, udfTypeField);
  }

  /// <summary>
  /// Return list of UDF fields for <paramref name="graph" /> and <paramref name="udfTypeField" />
  /// </summary>
  /// <param name="graph"></param>
  /// <param name="udfTypeField"></param>
  /// <returns></returns>
  public static IEnumerable<KeyValueHelper.ScreenAttribute> GetUDFFields(
    PXGraph graph,
    string udfTypeField = null)
  {
    return UDFHelper.GetUDFFields(graph.Accessinfo.ScreenID, udfTypeField);
  }

  /// <summary>
  /// Return list of required UDF fields of <paramref name="targetGraph" />
  /// </summary>
  /// <param name="sourceCache"></param>
  /// <param name="targetRow"></param>
  /// <param name="targetGraph"></param>
  /// <param name="udfTypeField"></param>
  /// <returns></returns>
  public static IEnumerable<PopupUDFAttributes> GetRequiredUDFFields(
    PXCache sourceCache,
    object targetRow,
    System.Type targetGraph,
    string udfTypeField = null)
  {
    IOrderedEnumerable<KeyValueHelper.ScreenAttribute> orderedEnumerable = UDFHelper.GetUDFFields(targetGraph, udfTypeField).Where<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (x => x.Required)).OrderBy<KeyValueHelper.ScreenAttribute, short>((Func<KeyValueHelper.ScreenAttribute, short>) (x => x.Column)).ThenBy<KeyValueHelper.ScreenAttribute, short>((Func<KeyValueHelper.ScreenAttribute, short>) (x => x.Row));
    IEnumerable<KeyValueHelper.ScreenAttribute> sourceAttr = UDFHelper.GetUDFFields(sourceCache.Graph, udfTypeField);
    string screenID = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, targetGraph)?.ScreenID;
    PXCache cache = sourceCache.Graph.Caches[typeof (PopupUDFAttributes)];
    PXCache destCache = sourceCache.Graph.Caches[GraphHelper.GetPrimaryCache(targetGraph.FullName).CacheType];
    foreach (KeyValueHelper.ScreenAttribute screenAttribute in (IEnumerable<KeyValueHelper.ScreenAttribute>) orderedEnumerable)
    {
      KeyValueHelper.ScreenAttribute attr = screenAttribute;
      string defaultValue = ((PXCache) GraphHelper.Caches<PopupUDFAttributes>(sourceCache.Graph)).Cached.Cast<PopupUDFAttributes>().Where<PopupUDFAttributes>((Func<PopupUDFAttributes, bool>) (x => x.AttributeID == attr.AttributeID && x.ScreenID.Equals(screenID, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault<PopupUDFAttributes>()?.Value;
      if (string.IsNullOrEmpty(defaultValue))
      {
        defaultValue = destCache.GetStateExt(targetRow, "Attribute" + attr.AttributeID) is PXFieldState stateExt1 ? stateExt1.Value?.ToString() : (string) null;
        if (string.IsNullOrEmpty(defaultValue))
        {
          defaultValue = sourceCache.GetStateExt(sourceCache.Current, "Attribute" + attr.AttributeID) is PXFieldState stateExt2 ? stateExt2.Value?.ToString() : (string) null;
          if (string.IsNullOrEmpty(defaultValue) && !sourceAttr.Any<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (at => at.AttributeID == attr.AttributeID)))
            defaultValue = attr.DefaultValue;
        }
      }
      PopupUDFAttributes popupUdfAttributes1 = new PopupUDFAttributes();
      popupUdfAttributes1.Selected = new bool?(false);
      ((FieldValue) popupUdfAttributes1).CacheName = ((PXInfo) GraphHelper.GetPrimaryCache(targetGraph.FullName)).Name;
      popupUdfAttributes1.ScreenID = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, targetGraph)?.ScreenID;
      popupUdfAttributes1.Name = attr.AttributeID;
      popupUdfAttributes1.DisplayName = attr.Attribute.Description;
      popupUdfAttributes1.AttributeID = attr.AttributeID;
      popupUdfAttributes1.Value = defaultValue;
      popupUdfAttributes1.Order = new int?((int) attr.Column * 10000 + (int) attr.Row);
      popupUdfAttributes1.Required = new bool?(attr.Required);
      PopupUDFAttributes popupUdfAttributes2 = popupUdfAttributes1;
      PopupUDFAttributes popupUdfAttributes3 = (PopupUDFAttributes) cache.Locate((object) popupUdfAttributes2);
      if (popupUdfAttributes3 == null)
        GraphHelper.Hold(cache, (object) popupUdfAttributes2);
      yield return popupUdfAttributes3 ?? popupUdfAttributes2;
    }
  }

  /// <summary>
  /// Fill <paramref name="destCache" /> by UDF values from <paramref name="sourceUDFPopupCache" /> values entered on pop-up dialog
  /// </summary>
  /// <param name="destCache"></param>
  /// <param name="sourceUDFPopupCache"></param>
  /// <param name="destRow"></param>
  /// <returns></returns>
  public static void FillfromPopupUDF(
    PXCache destCache,
    PXCache sourceUDFPopupCache,
    System.Type targetGraphType,
    object destRow)
  {
    string screenId = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, targetGraphType)?.ScreenID;
    foreach (PopupUDFAttributes popupUdfAttributes in sourceUDFPopupCache.Cached.OfType<PopupUDFAttributes>())
    {
      if (screenId != null && screenId.Equals(popupUdfAttributes.ScreenID, StringComparison.InvariantCultureIgnoreCase))
        destCache.SetValueExt(destRow, "Attribute" + popupUdfAttributes.AttributeID, (object) popupUdfAttributes.Value);
    }
  }

  /// <summary>
  /// Return PXFieldSatte for UDF <paramref name="attributeID" /> from <paramref name="graphType" />
  /// </summary>
  /// <param name="graphType"></param>
  /// <param name="attributeID"></param>
  /// <returns></returns>
  public static PXFieldState GetGraphUDFFieldState(System.Type graphType, string attributeID)
  {
    return ((IEnumerable<Tuple<PXFieldState, short, short, string>>) KeyValueHelper.GetAttributeFields(PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graphType)?.ScreenID)).Where<Tuple<PXFieldState, short, short, string>>((Func<Tuple<PXFieldState, short, short, string>, bool>) (a => a.Item1.Name.Equals("Attribute" + attributeID, StringComparison.CurrentCultureIgnoreCase))).Select<Tuple<PXFieldState, short, short, string>, PXFieldState>((Func<Tuple<PXFieldState, short, short, string>, PXFieldState>) (x => x.Item1)).FirstOrDefault<PXFieldState>();
  }

  /// <summary>
  /// Copy UDF from <paramref name="sourceCache" /> into <paramref name="destCache" />
  /// </summary>
  /// <param name="sourceCache"></param>
  /// <param name="sourceData"></param>
  /// <param name="destCache"></param>
  /// <param name="destData"></param>
  /// <param name="udfTypeField"></param>
  /// <returns></returns>
  public static void CopyAttributes(
    PXCache sourceCache,
    object sourceData,
    PXCache destCache,
    object destData,
    string udfTypeField)
  {
    foreach (KeyValueHelper.ScreenAttribute udfField in UDFHelper.GetUDFFields(destCache.Graph.GetType(), udfTypeField))
    {
      string str = "Attribute" + udfField.AttributeID;
      object obj1 = sourceCache.GetValueExt(sourceData ?? sourceCache.Current, str) is PXFieldState valueExt1 ? valueExt1.Value : (object) null;
      if (obj1 == null || string.Empty.Equals(obj1))
      {
        object obj2 = destCache.GetValueExt(destCache.Current, str) is PXFieldState valueExt2 ? valueExt2.Value : (object) null;
        if (obj2 != null && !string.Empty.Equals(obj2))
          destCache.SetValueExt(destData, str, obj2);
        else if (!string.IsNullOrEmpty(udfField.DefaultValue))
          destCache.SetValueExt(destData, str, (object) udfField.DefaultValue);
      }
      else
        destCache.SetValueExt(destData, str, obj1);
    }
  }

  public static void AddRequiredUDFFieldsEvents(PXGraph graph)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    graph.FieldVerifying.AddHandler<PopupUDFAttributes.displayName>(UDFHelper.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (UDFHelper.\u003C\u003Ec.\u003C\u003E9__7_0 = new PXFieldVerifying((object) UDFHelper.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddRequiredUDFFieldsEvents\u003Eb__7_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    graph.FieldSelecting.AddHandler<PopupUDFAttributes.value>(UDFHelper.\u003C\u003Ec.\u003C\u003E9__7_1 ?? (UDFHelper.\u003C\u003Ec.\u003C\u003E9__7_1 = new PXFieldSelecting((object) UDFHelper.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddRequiredUDFFieldsEvents\u003Eb__7_1))));
  }
}
