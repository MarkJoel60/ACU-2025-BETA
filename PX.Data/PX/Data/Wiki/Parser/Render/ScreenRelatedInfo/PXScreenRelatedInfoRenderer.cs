// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.PXScreenRelatedInfoRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Wiki.Parser.Render.ScreenRelatedInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.ScreenRelatedInfo;

[PXInternalUseOnly]
public class PXScreenRelatedInfoRenderer : PXRenderer
{
  private readonly ICollection<PXScreenRelatedInfoSection> _result;

  public override string GetResultingString(PXBlockParser.ParseContext parseContext)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<PXScreenRelatedInfoSection> GetResultingObject()
  {
    return (IEnumerable<PXScreenRelatedInfoSection>) this._result;
  }

  /// <summary>Initializes a new instance of PXJsonRenderer class.</summary>
  protected PXScreenRelatedInfoRenderer()
  {
    this._result = (ICollection<PXScreenRelatedInfoSection>) new LinkedList<PXScreenRelatedInfoSection>();
  }

  /// <summary>Initializes a new instance of PXJsonRenderer class.</summary>
  /// <param name="settings">Formatting settings.</param>
  public PXScreenRelatedInfoRenderer(PXWikiParserContext settings)
    : this()
  {
    this.Context = settings;
  }

  public sealed override void Render(WikiArticle source)
  {
    List<PXElement> allElements = source.GetAllElements();
    PXSectionRenderer pxSectionRenderer = new PXSectionRenderer();
    foreach (PXSectionElement section in allElements.OfType<PXSectionElement>())
    {
      PXScreenRelatedInfoSection relatedInfoSection = pxSectionRenderer.RenderWithSettings(section, this.Context);
      if (relatedInfoSection != null)
        this._result.Add(relatedInfoSection);
    }
  }
}
