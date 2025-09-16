// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXLinkDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXLinkDitaElement : PXDitaContainer
{
  public Package.TempCalcLink File;
  public string Caption;
  public string Link;
  public bool IsInternal;
  public bool IsExternal;
  public Topic Topic;
  public string _guid;
  private string _href;

  public PXLinkDitaElement()
  {
    this.File = (Package.TempCalcLink) null;
    this.IsInternal = false;
    this.IsExternal = false;
    this._href = "";
    this.Caption = "";
    this.Link = "";
  }

  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("xref");
    this.WriteAttributs(stream);
    if (this.File != null || this.IsInternal)
    {
      this._href = filemanager.GetLink(this.File, this.Topic, this.Link);
      this._href = $"{this._href}#topic{this._guid}";
      this.AddAttribute("href", this._href);
      this.AddAttribute("scope", "local");
    }
    if (this.IsExternal)
    {
      this._href = this.Link;
      this.AddAttribute("href", this._href);
      this.AddAttribute("scope", "external");
    }
    if (this._href == "" || this._href == null)
    {
      this._href = "Unknown.dita";
      this.AddAttribute("href", this._href);
      this.AddAttribute("scope", "local");
    }
    this.WriteAttributs(stream);
    stream.WriteRaw(this.Caption);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent = new StringBuilder();
    globalContent.Append((object) globalContent1);
    string str;
    try
    {
      str = this.Attributs["href"];
    }
    catch
    {
      str = (string) null;
    }
    if (str != null)
    {
      int startIndex1 = str.LastIndexOf(".dita");
      int startIndex2 = str.LastIndexOf(".aspx");
      if (startIndex1 > -1)
      {
        str = str.Remove(startIndex1, str.Length - startIndex1);
        int num = str.LastIndexOf("/");
        if (num > -1)
          str = str.Remove(0, num + 1);
        if (_context.IsRelatedLink)
        {
          globalContent.Append("\n");
          globalContent.Append("*");
        }
        globalContent.Append("[Help\\");
      }
      if (startIndex2 > -1)
      {
        if (_context.IsRelatedLink)
          globalContent.Append("*");
        str = str.Remove(startIndex2, str.Length - startIndex2);
        globalContent.Append("[");
      }
      if (startIndex1 <= -1 && startIndex2 <= -1)
      {
        if (_context.IsRelatedLink)
        {
          globalContent.Append("\n");
          globalContent.Append("*");
        }
        globalContent.Append("[");
      }
      if (str.LastIndexOf("#") > -1)
      {
        if (_context.IsRelatedLink)
        {
          globalContent.Append("\n");
          globalContent.Append("*");
        }
        int num = str.LastIndexOf("/");
        str = str.Remove(0, num + 1);
        globalContent.Append("#");
      }
    }
    globalContent.Append(str);
    bool flag = false;
    foreach (PXDitaElement child in this.Childs)
    {
      if (child is PXLinkTextDitaElement)
      {
        globalContent = child.Read(globalContent, _context);
        flag = true;
        globalContent.Append("]");
      }
      else
      {
        if (!flag)
        {
          flag = true;
          globalContent.Append("]");
        }
        globalContent = child.Read(globalContent, _context);
      }
    }
    if (!flag)
      globalContent.Append("]");
    return globalContent;
  }
}
