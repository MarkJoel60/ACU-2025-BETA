// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Mobile.SignManager.SignManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Api.Mobile.SignManager.DAC;
using System;
using System.Collections;

#nullable disable
namespace PX.Data.Api.Mobile.SignManager;

/// <summary>
///   <para>Provides a static method for capturing a user's signature on a mobile device and storing it in a PDF version of the report.</para>
/// </summary>
internal class SignManager
{
  private static string GetEndViewName(string viewName) => viewName + "__Signature";

  /// <summary>
  ///   <para>Captures a user's signature on a mobile device and stores it in a PDF version of the report.</para>
  /// </summary>
  /// <param name="view">The data view of the graph.</param>
  /// <param name="fileId">The file identifier of the report to be signed.</param>
  /// <param name="signedFileId">The file identifier of the signed PDF.</param>
  /// <remarks>When the method is called, the following things happen on the mobile device:
  /// <list type="number"><item><description>The mobile device displays the contents of the report whose file identifier is specified in the input parameter of the method. A user can review the
  /// contents by scrolling, panning, or zooming in on the report.</description></item>
  /// <item><description>The user either declines to sign the report or signs the report by writing a signature on the device screen and providing their name.</description></item></list>
  /// <para>This method has no effect in the web version of Acumatica ERP.</para></remarks>
  /// <example>
  /// When a signed report is required, a developer calls the method in the application business logic code, as shown in the following example. (In this example,
  /// Document is a data view of a graph.)
  /// <code title="Example" description="" lang="CS">
  /// Document.View.Sign(file.UID, out signedReportUID)</code></example>
  public static SignDialogResult Sign(PXView view, Guid fileId, out Guid? signedFileId)
  {
    string viewName = view.With<PXView, string>((Func<PXView, string>) (_ => _.Name));
    if (!string.IsNullOrEmpty(viewName))
      return PX.Data.Api.Mobile.SignManager.SignManager.SignInternal(viewName, view.Graph, fileId, out signedFileId);
    throw new PXException("A dialog cannot be requested.");
  }

  private static SignDialogResult SignInternal(
    string viewName,
    PXGraph graph,
    Guid fileId,
    out Guid? signedFileId)
  {
    SignResult signResult = viewName != null && graph != null ? (SignResult) PX.Data.Api.Mobile.SignManager.SignManager.GetSelect(graph).Search<SignResult.viewName>((object) PX.Data.Api.Mobile.SignManager.SignManager.GetEndViewName(viewName)) : throw new PXException("A dialog cannot be requested.");
    if (signResult != null)
    {
      int? result = signResult.Result;
      SignDialogResult signDialogResult;
      if ((signDialogResult = (SignDialogResult) result.Value) != SignDialogResult.None)
      {
        if (signDialogResult == SignDialogResult.Complete)
          signedFileId = signResult.SignedFileId;
        else
          signedFileId = new Guid?();
        return signDialogResult;
      }
    }
    throw new PXSignatureRequiredException(graph, viewName, fileId);
  }

  private static PXSelect<SignResult> GetSelect(PXGraph graph)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.WrappSignResultCache(graph);
    if (graph.Views.ContainsKey("SignResultView"))
    {
      PXView view = graph.Views["SignResultView"];
      return (PXSelect<SignResult>) graph.Views.GetExternalMember(view);
    }
    PXSelect<SignResult> select = new PXSelect<SignResult>(graph, (Delegate) (() => PX.Data.Api.Mobile.SignManager.SignManager.GetRecords(graph)));
    graph.Views.Add("SignResultView", (PXSelectBase) select);
    return select;
  }

  private static void WrappSignResultCache(PXGraph graph)
  {
    PXCache cach = graph.Caches.ContainsKey(typeof (SignResult)) ? graph.Caches[typeof (SignResult)] : (PXCache) null;
    if (cach is PX.Data.Api.Mobile.SignManager.SignManager.SignResultCache)
      return;
    graph.Caches[typeof (SignResult)] = (PXCache) new PX.Data.Api.Mobile.SignManager.SignManager.SignResultCache(graph, cach);
  }

  internal static IEnumerable GetRecords(PXGraph graph)
  {
    PX.Data.Api.Mobile.SignManager.SignManager.WrappSignResultCache(graph);
    foreach (SignResult record in graph.Caches[typeof (SignResult)].Inserted)
      yield return (object) record;
  }

  public static void SetResult(
    PXGraph graph,
    string viewName,
    SignDialogResult result,
    Guid? signedFileId)
  {
    string endViewName = PX.Data.Api.Mobile.SignManager.SignManager.GetEndViewName(viewName);
    PXSelect<SignResult> select = PX.Data.Api.Mobile.SignManager.SignManager.GetSelect(graph);
    SignResult signResult = (SignResult) select.Search<SignResult.viewName>((object) endViewName);
    if (signResult == null)
    {
      select.Insert(new SignResult()
      {
        ViewName = endViewName,
        Result = new int?((int) result),
        SignedFileId = signedFileId,
        Signature = string.Empty,
        FullName = string.Empty,
        SignatureId = new Guid?()
      });
    }
    else
    {
      signResult.Result = new int?((int) result);
      signResult.SignedFileId = signedFileId;
      select.Update(signResult);
    }
    select.Cache.IsDirty = false;
  }

  public static SignResult GetResult(PXGraph graph, string viewName)
  {
    string endViewName = PX.Data.Api.Mobile.SignManager.SignManager.GetEndViewName(viewName);
    return (SignResult) PX.Data.Api.Mobile.SignManager.SignManager.GetSelect(graph).Search<SignResult.viewName>((object) endViewName);
  }

  public static Guid? SetSignature(PXGraph graph, string viewName, string signature)
  {
    Guid? nullable = string.IsNullOrEmpty(signature) ? new Guid?() : new Guid?(Guid.NewGuid());
    string endViewName = PX.Data.Api.Mobile.SignManager.SignManager.GetEndViewName(viewName);
    PXSelect<SignResult> select = PX.Data.Api.Mobile.SignManager.SignManager.GetSelect(graph);
    SignResult signResult = (SignResult) select.Search<SignResult.viewName>((object) endViewName);
    if (signResult == null)
    {
      select.Insert(new SignResult()
      {
        ViewName = endViewName,
        Result = new int?(0),
        SignedFileId = new Guid?(),
        Signature = signature,
        FullName = string.Empty,
        SignatureId = nullable
      });
    }
    else
    {
      signResult.Signature = signature;
      signResult.SignatureId = nullable;
      select.Update(signResult);
    }
    select.Cache.IsDirty = false;
    return nullable;
  }

  public static void SetSignatureMeta(PXGraph graph, string viewName, string signatureMeta)
  {
    string endViewName = PX.Data.Api.Mobile.SignManager.SignManager.GetEndViewName(viewName);
    PXSelect<SignResult> select = PX.Data.Api.Mobile.SignManager.SignManager.GetSelect(graph);
    SignResult signResult = (SignResult) select.Search<SignResult.viewName>((object) endViewName);
    if (signResult == null)
    {
      select.Insert(new SignResult()
      {
        ViewName = endViewName,
        Result = new int?(0),
        SignedFileId = new Guid?(),
        Signature = string.Empty,
        FullName = signatureMeta
      });
    }
    else
    {
      signResult.FullName = signatureMeta;
      select.Update(signResult);
    }
    select.Cache.IsDirty = false;
  }

  public sealed class SignResultCache : PXCache<SignResult>
  {
    public SignResultCache(PXGraph graph)
      : this(graph, (PXCache) null)
    {
    }

    public SignResultCache(PXGraph graph, PXCache source)
      : base(graph)
    {
      if (source == null)
        return;
      foreach (object obj in source.Cached)
        this.Update(this.CreateCopy(obj));
    }
  }
}
