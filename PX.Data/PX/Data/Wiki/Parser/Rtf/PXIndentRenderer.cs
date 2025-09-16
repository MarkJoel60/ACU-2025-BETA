// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Rtf.PXIndentRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Rtf;

/// <summary>Represents a class for PXIndentElement RTF rendering.</summary>
internal class PXIndentRenderer : PXRtfRenderer
{
  private Random rand = new Random();

  public override void Render(PXElement elem, PXRtfBuilder rtf)
  {
    this.Render((PXIndentContainer) elem, rtf, 0);
    rtf.Paragraph();
  }

  private void Render(PXIndentContainer e, PXRtfBuilder rtf, int level)
  {
    ListNumberType numberType = e.Type == '*' ? ListNumberType.Bullet : (e.Type == '#' ? ListNumberType.Arabic : ListNumberType.NoNumber);
    int id = this.rand.Next();
    foreach (PXElement child1 in e.Children)
    {
      PXRtfBuilder rtf1 = new PXRtfBuilder(rtf.Document.Settings);
      rtf1.Settings = rtf.Settings;
      rtf1.CurrentTableLevel = rtf.CurrentTableLevel;
      switch (child1)
      {
        case PXIndentContainer _:
          this.Render((PXIndentContainer) child1, rtf1, level + 1);
          rtf.Document.Content.Append(rtf1.Document.Content.ToString());
          break;
        case PXIndentElement _:
          PXListItem elem = new PXListItem(rtf.Document, numberType, level, id);
          elem.TableLevel = rtf.CurrentTableLevel;
          rtf1.CurrentIndent = (elem.Level + 1) * elem.LevelIndent;
          foreach (PXElement child2 in ((PXContainerElement) child1).Children)
            this.DoRender(child2, rtf1);
          elem.Children.Add((PXRtfElement) new PXRawText(rtf.Document, rtf1.Document.Content.ToString()));
          rtf.AddRtfElement((PXRtfElement) elem);
          break;
      }
    }
  }
}
