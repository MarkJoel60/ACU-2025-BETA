// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXTextRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Drawing;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

internal class PXTextRenderer : PXPdfRenderer
{
  protected override void Render(PXElement elem, PXPdfRenderContext resultPdf)
  {
    PXTextElement pxTextElement = (PXTextElement) elem;
    using (Font font = this.CreateFont(resultPdf))
    {
      foreach (string splitByWord in Str.SplitByWords(pxTextElement.Value))
        this.DrawWord(splitByWord, resultPdf, font);
    }
  }

  private Font CreateFont(PXPdfRenderContext context)
  {
    FontStyle style = FontStyle.Regular;
    foreach (FontStyle appliedStyle in context.FontInfo.AppliedStyles)
      style |= appliedStyle;
    return new Font(context.FontInfo.Name, context.FontInfo.Size, style);
  }

  private void DrawWord(string word, PXPdfRenderContext context, Font font)
  {
    word = word.Replace(Environment.NewLine, "");
    word = word.Replace("\t", "       ");
    Size size = this.MeasureString(word, font);
    if (!context.IsFit(size.Width))
      context.NewLine();
    context.Writer.CurrentStream.DrawString(context.XPos, context.YPos, word, font.Name, font.Style, (int) font.SizeInPoints, Color.Black);
    context.XPos += size.Width;
  }

  private Size MeasureString(string text, Font font)
  {
    using (Bitmap bitmap = new Bitmap(2, 2))
    {
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.PageUnit = GraphicsUnit.Point;
        SizeF sizeF = graphics.MeasureString(text, font, 1000, StringFormat.GenericDefault);
        return new Size((int) sizeF.Width, (int) sizeF.Height);
      }
    }
  }
}
