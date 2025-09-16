// Decompiled with JetBrains decompiler
// Type: PX.Data.DialogManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
internal static class DialogManager
{
  public static WebDialogResult Ask(
    PXView view,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool repaintControls)
  {
    return DialogManager.Ask(view, key, row, header, message, buttons, (IReadOnlyDictionary<WebDialogResult, string>) null, icon, repaintControls);
  }

  public static WebDialogResult Ask(
    PXView view,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    IReadOnlyDictionary<WebDialogResult, string> buttonNames,
    MessageIcon icon,
    bool repaintControls)
  {
    string viewName = view.With<PXView, string>((Func<PXView, string>) (_ => _.Name));
    if (!string.IsNullOrEmpty(viewName))
      return DialogManager.AskInternal(viewName, view.Graph, key, row, header, message, buttons, icon, repaintControls, (DialogManager.InitializePanel) null, customButtonNames: buttonNames);
    throw new PXException("A dialog cannot be requested.");
  }

  public static WebDialogResult Ask(
    PXGraph graph,
    string viewName,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool repaintControls)
  {
    return DialogManager.AskInternal(viewName, graph, key, row, header, message, buttons, icon, repaintControls, (DialogManager.InitializePanel) null);
  }

  public static WebDialogResult AskExt(
    PXView view,
    string key,
    DialogManager.InitializePanel initializeHandler,
    bool repaintControls)
  {
    string viewName = view.With<PXView, string>((Func<PXView, string>) (_ => _.Name));
    return DialogManager.AskExt(view.Graph, viewName, key, initializeHandler, repaintControls);
  }

  public static WebDialogResult AskExt(
    PXGraph graph,
    string viewName,
    string key,
    DialogManager.InitializePanel initializeHandler,
    bool repaintControls)
  {
    return DialogManager.AskInternal(viewName, graph, key, (object) null, (string) null, (string) null, MessageButtons.None, MessageIcon.None, repaintControls, initializeHandler);
  }

  public static WebDialogResult AskExt(PXView view, MessageButtons buttons)
  {
    return DialogManager.AskInternal(view.With<PXView, string>((Func<PXView, string>) (_ => _.Name)), view.With<PXView, PXGraph>((Func<PXView, PXGraph>) (_ => _.Graph)), (string) null, (object) null, (string) null, (string) null, buttons, MessageIcon.None, false, (DialogManager.InitializePanel) null);
  }

  public static WebDialogResult AskExt(PXView view, string header, List<string> commitFields)
  {
    return DialogManager.AskInternal(view.With<PXView, string>((Func<PXView, string>) (_ => _.Name)), view.With<PXView, PXGraph>((Func<PXView, PXGraph>) (_ => _.Graph)), (string) null, (object) null, header, (string) null, MessageButtons.None, MessageIcon.None, false, (DialogManager.InitializePanel) null, commitFields);
  }

  public static WebDialogResult AskExt(
    PXView view,
    string header,
    DialogManager.InitializePanel initializeHandler,
    List<string> commitFields,
    bool repaintControls = false)
  {
    return DialogManager.AskInternal(view.With<PXView, string>((Func<PXView, string>) (_ => _.Name)), view.With<PXView, PXGraph>((Func<PXView, PXGraph>) (_ => _.Graph)), (string) null, (object) null, header, (string) null, MessageButtons.None, MessageIcon.None, repaintControls, initializeHandler, commitFields);
  }

  public static WebDialogResult GetAnswer(PXView view, string key)
  {
    string viewName = view.With<PXView, string>((Func<PXView, string>) (_ => _.Name));
    return DialogManager.GetAnswer(view.Graph, viewName, key);
  }

  public static WebDialogResult GetAnswer(PXGraph graph, string viewName, string key)
  {
    if (!string.IsNullOrEmpty(viewName))
    {
      if (!string.IsNullOrEmpty(key))
        viewName = $"{viewName}__{key}";
      DialogAnswer dialogAnswer = (DialogAnswer) DialogManager.GetSelect(graph).Search<DialogAnswer.viewName>((object) viewName);
      if (dialogAnswer != null)
        return (WebDialogResult) dialogAnswer.Answer.Value;
    }
    return WebDialogResult.None;
  }

  public static void SetAnswer(PXView view, string key, WebDialogResult answer)
  {
    string viewName = view.With<PXView, string>((Func<PXView, string>) (_ => _.Name));
    DialogManager.SetAnswer(view.Graph, viewName, key, answer);
  }

  public static void SetAnswer(PXGraph graph, string viewName, string key, WebDialogResult answer)
  {
    if (string.IsNullOrEmpty(viewName))
      return;
    if (!string.IsNullOrEmpty(key))
      viewName = $"{viewName}__{key}";
    PXSelect<DialogAnswer> select = DialogManager.GetSelect(graph);
    DialogAnswer dialogAnswer = (DialogAnswer) select.Search<DialogAnswer.viewName>((object) viewName);
    if (dialogAnswer == null)
    {
      select.Insert(new DialogAnswer()
      {
        ViewName = viewName,
        Answer = new int?((int) answer)
      });
    }
    else
    {
      dialogAnswer.Answer = new int?((int) answer);
      select.Update(dialogAnswer);
    }
    select.Cache.IsDirty = false;
  }

  public static string Encode(PXGraph graph, string viewName, string key)
  {
    if (graph == null || string.IsNullOrEmpty(viewName))
      return string.Empty;
    string strA = viewName;
    if (!string.IsNullOrEmpty(key))
      strA = $"{strA}__{key}";
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DialogAnswer record in DialogManager.GetRecords(graph))
    {
      if (string.Compare(strA, record.ViewName, true) != 0)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append('|');
        stringBuilder.AppendFormat("{0}${1}", (object) record.ViewName, (object) record.Answer.Value);
      }
      else
        break;
    }
    return stringBuilder.ToString();
  }

  public static void Load(PXGraph graph, string data)
  {
    if (graph == null || string.IsNullOrEmpty(data))
      return;
    DialogManager.WrappDialogAnswerCache(graph);
    PXCache cach = graph.Caches[typeof (DialogAnswer)];
    cach.Clear();
    string str1 = data;
    char[] chArray = new char[1]{ '|' };
    foreach (string str2 in str1.Split(chArray))
    {
      int length = str2.LastIndexOf('$');
      if (length > -1)
        cach.Insert((object) new DialogAnswer()
        {
          ViewName = str2.Substring(0, length),
          Answer = new int?(str2.Length - length > 1 ? Convert.ToInt32(str2.Substring(length + 1)) : 0)
        });
    }
    cach.IsDirty = false;
  }

  public static void Clear(PXGraph graph)
  {
    PXCache pxCache;
    if (graph == null || !graph.Caches.TryGetValue(typeof (DialogAnswer), out pxCache))
      return;
    pxCache.Clear();
  }

  /// <summary>Returns positive answer for a set of buttons.</summary>
  public static WebDialogResult PositiveAnswerFor(MessageButtons buttons)
  {
    switch (buttons)
    {
      case MessageButtons.OK:
      case MessageButtons.OKCancel:
        return WebDialogResult.OK;
      case MessageButtons.YesNoCancel:
      case MessageButtons.YesNo:
        return WebDialogResult.Yes;
      default:
        throw new NotSupportedException("This set of buttons does not contain any positive dialog answer.");
    }
  }

  private static WebDialogResult AskInternal(
    string viewName,
    PXGraph graph,
    string key,
    object row,
    string header,
    string message,
    MessageButtons buttons,
    MessageIcon icon,
    bool repaintControls,
    DialogManager.InitializePanel initializeHandler,
    List<string> commitFields = null,
    IReadOnlyDictionary<WebDialogResult, string> customButtonNames = null)
  {
    string field0 = viewName != null && graph != null ? viewName : throw new PXException("A dialog cannot be requested.");
    if (!string.IsNullOrEmpty(key))
      field0 = $"{field0}__{key}";
    DialogAnswer dialogAnswer = (DialogAnswer) DialogManager.GetSelect(graph).Search<DialogAnswer.viewName>((object) field0);
    if (dialogAnswer != null)
    {
      int? answer = dialogAnswer.Answer;
      WebDialogResult webDialogResult;
      if ((webDialogResult = (WebDialogResult) answer.Value) != WebDialogResult.None)
        return webDialogResult;
    }
    ((DialogManager.DialogAnswerCache) graph.Caches[typeof (DialogAnswer)]).SaveAnswers();
    if (initializeHandler != null)
      initializeHandler(graph, viewName);
    PXDialogRequiredException requiredException = new PXDialogRequiredException(graph, viewName, key, row, header, message, buttons, customButtonNames, icon, repaintControls);
    if (commitFields != null && commitFields.Count > 0)
      requiredException.ForceCommit = commitFields;
    throw requiredException;
  }

  private static void WrappDialogAnswerCache(PXGraph graph)
  {
    PXCache cach = graph.Caches.ContainsKey(typeof (DialogAnswer)) ? graph.Caches[typeof (DialogAnswer)] : (PXCache) null;
    if (cach is DialogManager.DialogAnswerCache)
      return;
    graph.Caches[typeof (DialogAnswer)] = (PXCache) new DialogManager.DialogAnswerCache(graph, cach);
  }

  private static PXSelect<DialogAnswer> GetSelect(PXGraph graph)
  {
    DialogManager.WrappDialogAnswerCache(graph);
    if (graph.Views.ContainsKey("DialogAnswerView"))
    {
      PXView view = graph.Views["DialogAnswerView"];
      return (PXSelect<DialogAnswer>) graph.Views.GetExternalMember(view);
    }
    PXSelect<DialogAnswer> select = new PXSelect<DialogAnswer>(graph, (Delegate) (() => DialogManager.GetRecords(graph)));
    graph.Views.Add("DialogAnswerView", (PXSelectBase) select);
    return select;
  }

  internal static IEnumerable GetRecords(PXGraph graph)
  {
    DialogManager.WrappDialogAnswerCache(graph);
    foreach (DialogAnswer record in graph.Caches[typeof (DialogAnswer)].Inserted)
      yield return (object) record;
  }

  /// <exclude />
  public delegate void InitializePanel(PXGraph graph, string viewName);

  /// <exclude />
  public sealed class DialogAnswerCache : PXCache<DialogAnswer>
  {
    private bool _clear = true;

    public DialogAnswerCache(PXGraph graph)
      : this(graph, (PXCache) null)
    {
    }

    public DialogAnswerCache(PXGraph graph, PXCache source)
      : base(graph)
    {
      if (source == null)
        return;
      foreach (object obj in source.Cached)
        this.Update(this.CreateCopy(obj));
    }

    public void SaveAnswers() => this._clear = false;

    public override void Unload()
    {
      if (this._clear)
        this.Clear();
      base.Unload();
    }
  }
}
