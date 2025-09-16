// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXNowikiTagRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXNowikiTagRenderer : PXditaRenderer
{
  private NowikiElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (NowikiElement) elem;
    PXTextDitaElement pxTextDitaElement = new PXTextDitaElement();
    int num;
    for (int startIndex = 0; (num = this._e.Value.IndexOf("&#95", startIndex)) > -1; this._e.Value = this._e.Value.Insert(startIndex, ";"))
      startIndex = num + 4;
    this._e.Value = this._e.Value.Replace("<", "&lt;");
    this._e.Value = this._e.Value.Replace(">", "&gt;");
    this._e.Value = this._e.Value.Replace("& ", "&#38;");
    this._e.Value = this._e.Value.Replace("&amp;", "&#38;");
    this._e.Value = this._e.Value.Replace("&trade;", "&#8482;");
    this._e.Value = this._e.Value.Replace("&iquest;", "&#191;");
    this._e.Value = this._e.Value.Replace("&copy;", "&#169;");
    this._e.Value = this._e.Value.Replace("&iexcl;", "&#161;");
    this._e.Value = this._e.Value.Replace("&lsaquo;", "&#60;");
    this._e.Value = this._e.Value.Replace("&rsaquo;", "&#62;");
    this._e.Value = this._e.Value.Replace("&reg;", "&#174;");
    this._e.Value = this._e.Value.Replace("&sect;", "&#167;");
    this._e.Value = this._e.Value.Replace("&cent;", "&#187;");
    this._e.Value = this._e.Value.Replace("&para;", "&#182;");
    this._e.Value = this._e.Value.Replace("&laquo;", "&#171;");
    this._e.Value = this._e.Value.Replace("&raquo;", "&#187;");
    this._e.Value = this._e.Value.Replace("&euro;", "&#8364;");
    this._e.Value = this._e.Value.Replace("&dagger;", "&#8224;");
    this._e.Value = this._e.Value.Replace("&lsquo;", "&#8216;");
    this._e.Value = this._e.Value.Replace("&rsquo;", "&#8217;");
    this._e.Value = this._e.Value.Replace("&yen;", "&#165;");
    this._e.Value = this._e.Value.Replace("&Dagger;", "&#8225;");
    this._e.Value = this._e.Value.Replace("&pound;", "&#163;");
    this._e.Value = this._e.Value.Replace("&bull;", "&#8226;");
    this._e.Value = this._e.Value.Replace("&curren;", "&#164;");
    this._e.Value = this._e.Value.Replace("&ndash;", "&#8211;");
    this._e.Value = this._e.Value.Replace("&ldquo;", "&#8220;");
    this._e.Value = this._e.Value.Replace("&rdquo;", "&#8221;");
    this._e.Value = this._e.Value.Replace("&mdash;", "&#151;");
    pxTextDitaElement.Content = this._e.Value;
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxTextDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) pxTextDitaElement);
  }
}
