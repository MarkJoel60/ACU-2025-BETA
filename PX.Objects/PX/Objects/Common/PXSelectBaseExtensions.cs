// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXSelectBaseExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public static class PXSelectBaseExtensions
{
  public static bool Any<T>(this PXSelectBase<T> select, params object[] parameters) where T : class, IBqlTable, new()
  {
    return select.SelectWindowed(0, 1, parameters).Count > 0;
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask((object) null, string.Empty, message, buttons, buttonNames, (MessageIcon) 0);
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    string header,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask((object) null, header, message, buttons, buttonNames, (MessageIcon) 0);
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask((object) null, header, message, buttons, buttonNames, icon);
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool isRefreshRequired,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask((object) null, header, message, buttons, buttonNames, icon, isRefreshRequired);
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask(row, header, message, buttons, buttonNames, icon);
  }

  public static WebDialogResult Ask(
    this PXSelectBase select,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool isRefreshRequired,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames)
  {
    return select.View.Ask(row, header, message, buttons, buttonNames, icon, isRefreshRequired);
  }
}
