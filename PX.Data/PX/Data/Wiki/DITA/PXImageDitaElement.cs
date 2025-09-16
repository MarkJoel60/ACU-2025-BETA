// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXImageDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXImageDitaElement : PXDitaContainer
{
  public Package.TempCalcLink File;
  public Topic Topic;
  public string Caption;
  public bool IsFigure;

  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    if (this.Caption == null || !this.IsFigure)
    {
      stream.WriteStartElement("image");
      if (this.File != null)
        this.AddAttribute("href", filemanager.GetLink(this.File, this.Topic, ""));
      this.WriteAttributs(stream);
      this.WriteChilds(stream, filemanager);
      stream.WriteEndElement();
    }
    else
    {
      stream.WriteStartElement("fig");
      stream.WriteStartElement("title");
      stream.WriteRaw(this.Caption);
      stream.WriteEndElement();
      stream.WriteStartElement("image");
      if (this.File != null)
        this.AddAttribute("href", filemanager.GetLink(this.File, this.Topic, ""));
      this.WriteAttributs(stream);
      this.WriteChilds(stream, filemanager);
      stream.WriteEndElement();
      stream.WriteEndElement();
    }
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
    string str1 = "";
    this.Attributs.TryGetValue("href", out str1);
    if (!string.IsNullOrEmpty(str1))
    {
      if (this.Attributs["placement"] == "break")
      {
        if (_context.IsFirstContainerInList && _context.listlevel.Count > 0)
        {
          globalContent1_1.Append("\n");
          for (int count = _context.listlevel.Count; count > 0; --count)
          {
            if (_context.listlevel[count - 1])
              globalContent1_1.Append("#");
            else
              globalContent1_1.Append("*");
          }
          globalContent1_1.Append(":");
          _context.IsFirstContainerInList = false;
          globalContent1_1.Append("[image:");
          globalContent1_1.Append(str1);
        }
        else
        {
          globalContent1_1.Append("\n");
          globalContent1_1.Append("[image:");
          globalContent1_1.Append(str1);
        }
        globalContent1_1.Append("|popup");
      }
      else
      {
        globalContent1_1.Append("[image:");
        globalContent1_1.Append(str1);
      }
      if (!string.IsNullOrEmpty(_context.Title.ToString()))
      {
        globalContent1_1.Append("|");
        globalContent1_1.Append((object) _context.Title);
      }
      string str2;
      if (this.Attributs.TryGetValue("width", out str2))
      {
        globalContent1_1.Append("|");
        globalContent1_1.Append(str2);
        globalContent1_1.Append("px");
      }
      globalContent1_1.Append("]");
      _context.ImageLinks.Add(str1);
    }
    return this.ReadChilds(globalContent1_1, _context);
  }
}
