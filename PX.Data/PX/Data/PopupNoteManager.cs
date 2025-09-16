// Decompiled with JetBrains decompiler
// Type: PX.Data.PopupNoteManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.Data;

public static class PopupNoteManager
{
  private const string _slotMessageKey = "PopupNoteManagerMessageKey";
  private const string _slotPreserveCopyPasteWarningsKey = "PopupNoteManagerPreserveWarningsKey";
  private const string _longRunWarningsKey = "PopupNoteManagerLongRunWarningsKey";
  private const string _sessionMessageKey = "PopupNoteManagerSessionMessageKey";
  private const string _sessingWarningsKey = "PopupNoteManagerSessionWarningsKey";

  private static string GenericMessage
  {
    get
    {
      return PXLocalizer.Localize("Multiple notes have been found for the inserted records.", typeof (Messages).FullName);
    }
  }

  public static string Message
  {
    get => PXContext.GetSlot<string>("PopupNoteManagerMessageKey");
    set
    {
      string slot = PXContext.GetSlot<string>("PopupNoteManagerMessageKey");
      if (!string.IsNullOrEmpty(slot) && !slot.Equals(value, StringComparison.Ordinal))
        PXContext.SetSlot<string>("PopupNoteManagerMessageKey", PopupNoteManager.GenericMessage);
      else
        PXContext.SetSlot<string>("PopupNoteManagerMessageKey", value);
    }
  }

  public static void StoreMessageForRedirect()
  {
    string message = PopupNoteManager.Message;
    if (string.IsNullOrEmpty(message))
      return;
    PXContext.Session.SetString("PopupNoteManagerSessionMessageKey", message);
  }

  public static void ShowMessageAfterRedirect()
  {
    string str = PXContext.Session["PopupNoteManagerSessionMessageKey"] as string;
    if (string.IsNullOrEmpty(str))
      return;
    PXContext.Session.Remove("PopupNoteManagerSessionMessageKey");
    PopupNoteManager.Message = str;
  }

  internal static void StoreWarningsForRedirect(IEnumerable<PXUIWarningInfo> warnings)
  {
    PXContext.Session["PopupNoteManagerSessionWarningsKey"] = (object) warnings;
  }

  internal static void ShowWarningsAfterRedirect(PXGraph graph)
  {
    if (!(PXContext.Session["PopupNoteManagerSessionWarningsKey"] is IEnumerable<PXUIWarningInfo> warnings) || !PopupNoteManager.ShowWarnings(graph, warnings))
      return;
    PXContext.Session["PopupNoteManagerSessionWarningsKey"] = (object) null;
  }

  public static List<PXUIWarningInfo> CopyPasteWarnings
  {
    get
    {
      List<PXUIWarningInfo> copyPasteWarnings = PXContext.GetSlot<List<PXUIWarningInfo>>("PopupNoteManagerPreserveWarningsKey");
      if (copyPasteWarnings == null)
      {
        copyPasteWarnings = new List<PXUIWarningInfo>();
        PXContext.SetSlot<List<PXUIWarningInfo>>("PopupNoteManagerPreserveWarningsKey", copyPasteWarnings);
      }
      return copyPasteWarnings;
    }
    set => PXContext.SetSlot<List<PXUIWarningInfo>>("PopupNoteManagerPreserveWarningsKey", value);
  }

  public static bool PreserveErrors(PXCache cache, string fieldName)
  {
    List<PXUIWarningInfo> copyPasteWarnings = PopupNoteManager.CopyPasteWarnings;
    return copyPasteWarnings != null && copyPasteWarnings.Any<PXUIWarningInfo>((Func<PXUIWarningInfo, bool>) (w => w.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase) && w.CacheItemType == cache.GetItemType()));
  }

  private static void StoreCopyPasteWarning(PXCache cache, string fieldName)
  {
    PopupNoteManager.CopyPasteWarnings.Add(new PXUIWarningInfo(cache.GetItemType(), (object) null, fieldName, (string) null));
  }

  internal static void StoreLongRunWarning(
    PXCache cache,
    object data,
    string fieldName,
    string text)
  {
    if (!(PXLongOperation.GetCustomInfoForCurrentThread("PopupNoteManagerLongRunWarningsKey") is HashSet<PXUIWarningInfo> info))
    {
      info = new HashSet<PXUIWarningInfo>();
      PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), "PopupNoteManagerLongRunWarningsKey", (object) info);
    }
    info.Add(new PXUIWarningInfo(cache.GetItemType(), data, fieldName, text));
  }

  [Obsolete("Use StoreLongRunWarning instead of this method")]
  public static void StoreImportFromExcelWarning(
    PXCache cache,
    object data,
    string fieldName,
    string text)
  {
    PopupNoteManager.StoreLongRunWarning(cache, data, fieldName, text);
  }

  internal static HashSet<PXUIWarningInfo> GetLongRunWarnings(object key)
  {
    return PXLongOperation.GetCustomInfo(key, "PopupNoteManagerLongRunWarningsKey") as HashSet<PXUIWarningInfo>;
  }

  [Obsolete("Use GetLongRunWarning instead of this method")]
  public static List<PXUIWarningInfo> GetImportFromExcelWarning(object key)
  {
    HashSet<PXUIWarningInfo> longRunWarnings = PopupNoteManager.GetLongRunWarnings(key);
    return longRunWarnings == null ? (List<PXUIWarningInfo>) null : longRunWarnings.ToList<PXUIWarningInfo>();
  }

  internal static bool ShowWarnings(PXGraph graph, IEnumerable<PXUIWarningInfo> warnings)
  {
    int num = 0;
    string str = (string) null;
    foreach (PXUIWarningInfo warning in warnings)
    {
      if (graph.Caches.ContainsKey(warning.CacheItemType))
      {
        PXCache cach = graph.Caches[warning.CacheItemType];
        object data = cach.Locate(warning.CacheItem);
        if (data != null)
        {
          PXUIFieldAttribute.SetWarning(cach, data, warning.FieldName, warning.WarningText);
          str = warning.WarningText;
          ++num;
        }
      }
    }
    if (num > 1)
      PopupNoteManager.Message = PXLocalizer.Localize("Multiple notes have been found for the inserted records.", typeof (Messages).FullName);
    else if (!string.IsNullOrWhiteSpace(str))
      PopupNoteManager.Message = str;
    return num > 0;
  }

  [Obsolete("Use ShowLongRunWarning instead of this method")]
  public static void ShowImportFromExcelWarnings(
    PXGraph graph,
    IEnumerable<PXUIWarningInfo> warnings)
  {
    PopupNoteManager.ShowWarnings(graph, warnings);
  }

  private static void ShowPopupWarning(PXCache cache, string fieldName)
  {
    foreach (PXSelectorAttribute selectorAttribute in cache.GetAttributes(fieldName).OfType<PXSelectorAttribute>())
      selectorAttribute.ShowPopupWarning = true;
  }

  public static void RegisterText(PXCache cache, object data, string fieldName, string text)
  {
    PXGraph graph = cache.Graph;
    if ((string.IsNullOrEmpty(text) || graph.IsContractBasedAPI || graph.IsImport && !graph.IsCopyPasteContext || graph.IsExport && !graph.IsCopyPasteContext || graph.IsMobile || PortalHelper.IsPortalContext() ? 1 : (graph.IsPageGeneratorRequest ? 1 : 0)) != 0)
      return;
    bool flag = string.IsNullOrEmpty(PXUIFieldAttribute.GetError(cache, data, fieldName));
    if (graph.IsImportFromExcel & flag)
      PopupNoteManager.StoreLongRunWarning(cache, data, fieldName, text);
    else if (graph.IsCopyPasteContext & flag)
    {
      PopupNoteManager.ShowPopupWarning(cache, fieldName);
      PopupNoteManager.StoreCopyPasteWarning(cache, fieldName);
      PopupNoteManager.Message = text;
    }
    else
    {
      if (flag)
        PopupNoteManager.ShowPopupWarning(cache, fieldName);
      PopupNoteManager.StoreLongRunWarning(cache, data, fieldName, text);
      PopupNoteManager.Message = HttpUtility.HtmlEncode(text);
    }
  }
}
