// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXParagraphDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXParagraphDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("p");
    this.WriteChilds(stream, filemanager);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent = new StringBuilder();
    globalContent.Append((object) globalContent1);
    bool flag = false;
    foreach (PXDitaElement child in this.Childs)
    {
      if (!flag)
      {
        if (child is PXTextDitaElement)
        {
          globalContent.Append("\n");
          globalContent.Append("\n");
        }
        flag = true;
      }
      globalContent = child.Read(globalContent, _context);
    }
    return globalContent;
  }
}
