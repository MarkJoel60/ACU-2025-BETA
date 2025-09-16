// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXLinkRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

internal class PXLinkRenderer : PXditaRenderer
{
  private PXLinkElement _e;

  protected override void Render(PXElement elem, PXditaRenderContext resultTxt)
  {
    this._e = (PXLinkElement) elem;
    string str = "";
    string filename1 = "";
    foreach (PXElement linkElement in this._e.GetLinkElements())
    {
      if (linkElement is PXTextElement)
        str += ((PXTextElement) linkElement).Value;
    }
    foreach (PXElement captionElement in this._e.GetCaptionElements())
    {
      if (captionElement is PXTextElement)
        filename1 += ((PXTextElement) captionElement).Value;
    }
    if (string.Compare(str, "anchor", true) == 0)
    {
      PXSectionDitaElement sectionDitaElement = new PXSectionDitaElement();
      string filename2 = filename1.Length > 1 ? filename1.Substring(1) : filename1;
      sectionDitaElement.AddAttribute("id", PXLinkRenderer.RemoveUnint(filename2));
      if (resultTxt.CurrentParent.Count != 0)
        resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) sectionDitaElement);
      else
        resultTxt.CurrentTopic.AddElement((PXDitaElement) sectionDitaElement);
    }
    else
    {
      PXLinkDitaElement pxLinkDitaElement1 = new PXLinkDitaElement();
      pxLinkDitaElement1.Topic = (Topic) resultTxt.Settings.GetExchangedata()["topic"];
      if (str.IndexOf("#") > -1)
      {
        pxLinkDitaElement1.Link = str.Replace("#", "");
        pxLinkDitaElement1.Caption = PXLinkRenderer.RemoveUnint(filename1);
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxLinkDitaElement1);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxLinkDitaElement1);
        pxLinkDitaElement1.IsInternal = true;
      }
      else
      {
        try
        {
          Guid _guid = new Guid(str);
          PXDitaContext.CalcLinkCollection calcLinkCollection = (PXDitaContext.CalcLinkCollection) resultTxt.Settings.GetExchangedata()["calcreflist"];
          pxLinkDitaElement1.File = calcLinkCollection.GetLink(_guid);
          pxLinkDitaElement1.Caption = filename1;
          if (resultTxt.CurrentParent.Count != 0)
          {
            resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxLinkDitaElement1);
            return;
          }
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxLinkDitaElement1);
          return;
        }
        catch (Exception ex)
        {
        }
        PXLinkDitaElement pxLinkDitaElement2 = new PXLinkDitaElement();
        pxLinkDitaElement2.IsExternal = true;
        pxLinkDitaElement2.Link = str;
        pxLinkDitaElement2.Caption = filename1;
        pxLinkDitaElement2.Topic = (Topic) resultTxt.Settings.GetExchangedata()["topic"];
        if (resultTxt.CurrentParent.Count != 0)
          resultTxt.CurrentParent.Peek().AddChild((PXDitaElement) pxLinkDitaElement2);
        else
          resultTxt.CurrentTopic.AddElement((PXDitaElement) pxLinkDitaElement2);
      }
    }
  }

  internal static string RemoveUnint(string filename)
  {
    filename = filename.Replace(" ", "");
    filename = filename.Replace("-", "");
    filename = filename.Replace("/", "");
    return filename;
  }
}
