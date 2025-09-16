// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ViewAskExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions;

[PXInternalUseOnly]
public static class ViewAskExtensions
{
  public static WebDialogResult Ask_YesNoCancel_WithCallback(
    this PXView view,
    object row,
    string header,
    string message,
    MessageIcon icon)
  {
    PXView pxView = view;
    object obj = row;
    string str1 = header;
    string str2 = message;
    Dictionary<WebDialogResult, string> dictionary = new Dictionary<WebDialogResult, string>();
    dictionary[(WebDialogResult) 3] = "Yes";
    dictionary[(WebDialogResult) 4] = "No";
    dictionary[(WebDialogResult) 5] = "Cancel";
    MessageIcon messageIcon = icon;
    switch (pxView.Ask(obj, str1, str2, (MessageButtons) 2, (IReadOnlyDictionary<WebDialogResult, string>) dictionary, messageIcon) - 3)
    {
      case 0:
        view.Answer = (WebDialogResult) 6;
        break;
      case 1:
        view.Answer = (WebDialogResult) 7;
        break;
      case 2:
        view.Answer = (WebDialogResult) 2;
        break;
    }
    return view.Answer;
  }

  internal static PXView WithAnswerFor(this PXView view, bool predicate, WebDialogResult answer)
  {
    if (predicate && view.Answer == null)
      view.Answer = answer;
    return view;
  }

  public static PXView WithActionIfNoAnswerFor(this PXView view, bool predicate, Action action)
  {
    if (view.Answer == 0 & predicate)
      action();
    return view;
  }

  public static T WithActionIfNoAnswerFor<T>(this T select, bool predicate, Action action) where T : PXSelectBase
  {
    select.View.WithActionIfNoAnswerFor(predicate, action);
    return select;
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the graph in Contact-Based API context (<see cref="F:PX.Data.PXGraph.IsContractBasedAPI" />).
  /// </summary>
  /// <param name="view"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="view" /></returns>
  public static PXView WithAnswerForCbApi(this PXView view, WebDialogResult answer)
  {
    return view.WithAnswerFor(view.Graph.IsContractBasedAPI, answer);
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the graph in Contact-Based API context (<see cref="F:PX.Data.PXGraph.IsContractBasedAPI" />).
  /// </summary>
  /// <param name="select"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="select" /></returns>
  public static T WithAnswerForCbApi<T>(this T select, WebDialogResult answer) where T : PXSelectBase
  {
    select.View.WithAnswerForCbApi(answer);
    return select;
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the graph in unattended mode (<see cref="M:PX.Data.PXPreserveScope.IsScoped" />).
  /// </summary>
  /// <param name="view"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="view" /></returns>
  public static PXView WithAnswerForUnattendedMode(this PXView view, WebDialogResult answer)
  {
    return view.WithAnswerFor(view.Graph.UnattendedMode, answer);
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the graph in unattended mode (<see cref="M:PX.Data.PXPreserveScope.IsScoped" />).
  /// </summary>
  /// <param name="select"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="select" /></returns>
  public static T WithAnswerForUnattendedMode<T>(this T select, WebDialogResult answer) where T : PXSelectBase
  {
    select.View.WithAnswerForUnattendedMode(answer);
    return select;
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the request came from mobile application (<see cref="P:PX.Data.PXGraph.IsMobile" />).
  /// </summary>
  /// <param name="view"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="view" /></returns>
  public static PXView WithAnswerForMobile(this PXView view, WebDialogResult answer)
  {
    return view.WithAnswerFor(view.Graph.IsMobile, answer);
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the request came from mobile application (<see cref="P:PX.Data.PXGraph.IsMobile" />).
  /// </summary>
  /// <param name="select"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="select" /></returns>
  public static T WithAnswerForMobile<T>(this T select, WebDialogResult answer) where T : PXSelectBase
  {
    select.View.WithAnswerForMobile(answer);
    return select;
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the request came from import scenario (<see cref="F:PX.Data.PXGraph.IsImport" />).
  /// </summary>
  /// <param name="view"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="view" /></returns>
  public static PXView WithAnswerForImport(this PXView view, WebDialogResult answer)
  {
    return view.WithAnswerFor(view.Graph.IsImport && !view.Graph.IsMobile, answer);
  }

  /// <summary>
  /// Uses predefined <see cref="T:PX.Data.WebDialogResult" /> if <see cref="P:PX.Data.PXView.Answer" />
  /// is not set (<see cref="F:PX.Data.WebDialogResult.None" />)
  /// and the request came from import scenario (<see cref="F:PX.Data.PXGraph.IsImport" />).
  /// </summary>
  /// <param name="select"></param>
  /// <param name="answer"></param>
  /// <returns><paramref name="select" /></returns>
  public static T WithAnswerForImport<T>(this T select, WebDialogResult answer) where T : PXSelectBase
  {
    select.View.WithAnswerForImport(answer);
    return select;
  }

  public static void ClearAnswers(this PXSelectBase select, bool clearCurrent = false)
  {
    if (clearCurrent)
    {
      select.Cache.Remove(select.Cache.Current);
      select.Cache.Current = (object) null;
    }
    select.View.Answer = (WebDialogResult) 0;
  }
}
