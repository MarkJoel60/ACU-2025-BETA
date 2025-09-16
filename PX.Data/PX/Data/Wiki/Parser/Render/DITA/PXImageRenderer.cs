// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXImageRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXImageRenderer : PXditaRenderer
{
  private PXImageElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXImageElement) elem;
    PXImageDitaElement imageDitaElement = new PXImageDitaElement();
    imageDitaElement.Caption = this._e.Caption;
    imageDitaElement.IsFigure = this._e.Type == ImageType.Popup;
    imageDitaElement.Topic = (Topic) resultTxt.Settings.GetExchangedata()["topic"];
    PXDitaContext.CalcLinkCollection calcLinkCollection = (PXDitaContext.CalcLinkCollection) resultTxt.Settings.GetExchangedata()["calcreflist"];
    try
    {
      Guid _guid = new Guid(this._e.Name);
      imageDitaElement.File = calcLinkCollection.GetLink(_guid);
    }
    catch (Exception ex)
    {
    }
    imageDitaElement.AddAttribute("width", this._e.Width.ToString());
    imageDitaElement.AddAttribute("height", this._e.Height.ToString());
    switch (this._e.Location)
    {
      case ImageLocation.Left:
        imageDitaElement.AddAttribute("align", "left");
        break;
      case ImageLocation.Right:
        imageDitaElement.AddAttribute("align", "right");
        break;
      case ImageLocation.Center:
        imageDitaElement.AddAttribute("align", "center");
        break;
      default:
        imageDitaElement.AddAttribute("align", "left");
        break;
    }
    switch (this._e.Type)
    {
      case ImageType.Border:
      case ImageType.Frame:
      case ImageType.Popup:
        imageDitaElement.AddAttribute("placement", "break");
        break;
      case ImageType.Thumb:
        imageDitaElement.AddAttribute("placement", "inline");
        break;
      default:
        imageDitaElement.AddAttribute("placement", "inline");
        break;
    }
    if (resultTxt.CurrentParent.Count != 0)
      resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) imageDitaElement);
    else
      resultTxt.CurrentTopic.AddElement((PXDitaElement) imageDitaElement);
  }
}
