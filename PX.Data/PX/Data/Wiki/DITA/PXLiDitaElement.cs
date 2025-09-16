// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXLiDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXLiDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("li");
    this.WriteAttributs(stream);
    this.WriteChilds(stream, filemanager);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent = new StringBuilder();
    globalContent.Append((object) globalContent1);
    globalContent.Append("\n");
    for (int count = _context.listlevel.Count; count > 0; --count)
    {
      if (_context.listlevel[count - 1])
        globalContent.Append("#");
      else
        globalContent.Append("*");
    }
    bool flag = true;
    foreach (PXDitaElement child in this.Childs)
    {
      if (!flag)
      {
        if (child is PXNoteDitaElement || child is PXTableDitaElement || child is PXSimpletableDitaElement)
        {
          globalContent.Append("\n");
          for (int count = _context.listlevel.Count; count > 0; --count)
          {
            if (_context.listlevel[count - 1])
              globalContent.Append("#");
            else
              globalContent.Append("*");
          }
          globalContent.Append(":");
        }
        if (child is PXParagraphDitaElement)
        {
          globalContent.Append("\n");
          for (int count = _context.listlevel.Count; count > 0; --count)
          {
            if (_context.listlevel[count - 1])
              globalContent.Append("#");
            else
              globalContent.Append("*");
          }
          globalContent.Append(":");
          _context.IsFirstContainerInList = false;
        }
        else if (!(child is PXImageDitaElement))
          _context.IsFirstContainerInList = true;
      }
      flag = false;
      globalContent = child.Read(globalContent, _context);
    }
    return globalContent;
  }
}
