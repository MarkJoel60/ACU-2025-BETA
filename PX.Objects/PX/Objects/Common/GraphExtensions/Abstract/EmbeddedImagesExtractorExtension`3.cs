// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.EmbeddedImagesExtractorExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using HtmlAgilityPack;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField> : 
  AttachmentsHandlerExtension<
  #nullable disable
  TGraph>
  where TGraph : PXGraph, new()
  where TDocument : class, INotable, IBqlTable, new()
  where THtmlField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  THtmlField>
{
  protected virtual bool ShowHtmlFieldWarningForException => true;

  internal virtual bool ExtractionIsActive
  {
    get => ((PXCache) GraphHelper.Caches<TDocument>((PXGraph) this.Base)).Current != null;
  }

  [PXOverride]
  public virtual void Persist(Action persist)
  {
    if (!this.ExtractionIsActive)
    {
      persist();
    }
    else
    {
      Action revertCallback = (Action) null;
      EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour? nullable1 = new EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour?();
      ImageExtractor.Base64FormatException base64FormatException = (ImageExtractor.Base64FormatException) null;
      try
      {
        this.ExtractEmbeddedImages(out revertCallback);
        this.ProcessWhenNoExceptionThrown();
      }
      catch (ImageExtractor.Base64FormatException ex)
      {
        PXTrace.WriteError((Exception) ex);
        if (revertCallback != null)
        {
          revertCallback();
          revertCallback = (Action) null;
        }
        nullable1 = new EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour?(this.ProcessBase64FormatException(ex));
        EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour? nullable2 = nullable1;
        EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour exceptionBehaviour = EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.Throw;
        if (nullable2.GetValueOrDefault() == exceptionBehaviour & nullable2.HasValue)
          throw new PXSetPropertyException((Exception) ex, (PXErrorLevel) 4, "An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content.", Array.Empty<object>());
        base64FormatException = ex;
      }
      try
      {
        persist();
      }
      catch
      {
        if (revertCallback != null)
          revertCallback();
        throw;
      }
      if (base64FormatException != null && nullable1.GetValueOrDefault() == EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.ThrowWarningAfterPersist)
        throw new PXSetPropertyException((Exception) base64FormatException, (PXErrorLevel) 2, "Base64 content cannot be decoded.", Array.Empty<object>());
    }
  }

  private protected virtual EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour ProcessBase64FormatException(
    ImageExtractor.Base64FormatException e)
  {
    if (this.ShowHtmlFieldWarningForException)
    {
      PXCache<TDocument> pxCache = GraphHelper.Caches<TDocument>((PXGraph) this.Base);
      object current = ((PXCache) pxCache).Current;
      if (current != null)
      {
        ((PXCache) pxCache).RaiseExceptionHandling<THtmlField>(current, ((PXCache) pxCache).GetValue<THtmlField>(current), (Exception) new PXSetPropertyException<THtmlField>("Base64 content cannot be decoded.", (PXErrorLevel) 3));
        return EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.ThrowWarningAfterPersist;
      }
    }
    return EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.Throw;
  }

  private protected virtual void ProcessWhenNoExceptionThrown()
  {
  }

  private void ExtractEmbeddedImages(out Action revertCallback)
  {
    revertCallback = (Action) null;
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (TDocument));
    PXCache documentCache = this.Base.Caches[typeof (TDocument)];
    object current = documentCache.Current;
    if (current == null)
      return;
    Guid? noteId = documentCache.GetValue(current, "NoteID") as Guid?;
    if (!noteId.HasValue)
      return;
    string html = documentCache.GetValue<THtmlField>(current) as string;
    if (string.IsNullOrEmpty(html))
      return;
    ImageExtractor imageExtractor = new ImageExtractor();
    Action revertCallback_ = (Action) null;
    try
    {
      string str;
      ICollection<ImageExtractor.ImageInfo> imageInfos;
      if (!imageExtractor.ExtractEmbedded(html, new Func<ImageExtractor.ImageInfo, (string, string)>(getSrcAndTitle), ref str, ref imageInfos))
        return;
      documentCache.SetValue<THtmlField>(current, (object) str);
    }
    finally
    {
      revertCallback = revertCallback_ + (Action) (() => documentCache.SetValue<THtmlField>(documentCache.Current, (object) html));
    }

    (string src, string title) getSrcAndTitle(ImageExtractor.ImageInfo img)
    {
      (string src, string title, Action revertCallback) tuple = this.AddExtractedImage(noteId.Value, img);
      string src = tuple.src;
      string title = tuple.title;
      revertCallback_ += tuple.revertCallback;
      return (src, title);
    }
  }

  private (string src, string title, Action revertCallback) AddExtractedImage(
    Guid noteId,
    ImageExtractor.ImageInfo img)
  {
    Action revertCallback;
    this.InsertFile(new FileDto(noteId, img.Name, img.Bytes, new Guid?(img.ID)), out revertCallback);
    return ("~/Frames/GetFile.ashx?fileID=" + img.CID, img.Name, revertCallback);
  }

  private protected enum FormatExceptionBehaviour
  {
    Throw,
    ThrowWarningAfterPersist,
    DontThrow,
  }

  public abstract class WithFieldForExceptionPersistence<TExceptionField> : 
    EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>
    where TExceptionField : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TExceptionField>
  {
    protected override bool ShowHtmlFieldWarningForException => false;

    private protected override EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour ProcessBase64FormatException(
      ImageExtractor.Base64FormatException e)
    {
      PXCache<TDocument> pxCache = GraphHelper.Caches<TDocument>((PXGraph) this.Base);
      object current = ((PXCache) pxCache).Current;
      if (current == null)
        return base.ProcessBase64FormatException(e);
      string str = ((PXCache) pxCache).GetValue<TExceptionField>(current) as string;
      if (!Str.IsNullOrEmpty(str))
      {
        try
        {
          HtmlDocument htmlDocument = new HtmlDocument();
          htmlDocument.LoadHtml(str);
          if (htmlDocument.DocumentNode.Descendants("div").Where<HtmlNode>((Func<HtmlNode, bool>) (el => el.Attributes["data-px-marker-exception"]?.Value == "Base64ContentExtraction")).Any<HtmlNode>())
            return EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.DontThrow;
          str = $"{str}<br><br>{$"<div style=\"color:orange\" data-px-marker-exception=\"Base64ContentExtraction\">{PXMessages.LocalizeNoPrefix("An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content.")}</div>"}";
        }
        catch (Exception ex)
        {
          PXTrace.WriteError(ex);
        }
      }
      else
        str = $"<div style=\"color:orange\" data-px-marker-exception=\"Base64ContentExtraction\">{PXMessages.LocalizeNoPrefix("An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content.")}</div>";
      ((PXCache) pxCache).SetValue<TExceptionField>(current, (object) str);
      return EmbeddedImagesExtractorExtension<TGraph, TDocument, THtmlField>.FormatExceptionBehaviour.DontThrow;
    }

    private protected override void ProcessWhenNoExceptionThrown()
    {
      PXCache<TDocument> pxCache = GraphHelper.Caches<TDocument>((PXGraph) this.Base);
      object current = ((PXCache) pxCache).Current;
      if (current == null && ((PXCache) pxCache).GetValue<TExceptionField>(current) == null)
        return;
      string str = ((PXCache) pxCache).GetValue<TExceptionField>(current) as string;
      if (Str.IsNullOrEmpty(str))
        return;
      try
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(str);
        foreach (HtmlNode htmlNode in htmlDocument.DocumentNode.Descendants("div").Where<HtmlNode>((Func<HtmlNode, bool>) (el => el.Attributes["data-px-marker-exception"]?.Value == "Base64ContentExtraction")).ToList<HtmlNode>())
        {
          if (htmlNode.PreviousSibling?.Name == "br")
          {
            htmlDocument.DocumentNode.RemoveChild(htmlNode.PreviousSibling);
            if (htmlNode.PreviousSibling?.PreviousSibling?.Name == "br")
              htmlDocument.DocumentNode.RemoveChild(htmlNode.PreviousSibling.PreviousSibling);
          }
          if (htmlNode.NextSibling?.Name == "br")
            htmlDocument.DocumentNode.RemoveChild(htmlNode.NextSibling);
          htmlDocument.DocumentNode.RemoveChild(htmlNode);
        }
        ((PXCache) pxCache).SetValue<TExceptionField>(current, (object) htmlDocument.DocumentNode.OuterHtml);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
      }
    }

    [PXInternalUseOnly]
    public static class ExceptionHtmlConsts
    {
      public const string Tag = "div";
      public const string Style = "style=\"color:orange\"";
      public const string Base64FormatExceptionDataTagValue = "Base64ContentExtraction";
      public const string PXExceptionDataTag = "data-px-marker-exception";
      public const string HtmlExceptionTag = "<div style=\"color:orange\" data-px-marker-exception=\"Base64ContentExtraction\">{0}</div>";
    }
  }
}
