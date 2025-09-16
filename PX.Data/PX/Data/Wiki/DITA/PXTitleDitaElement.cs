// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXTitleDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXTitleDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("title");
    this.WriteAttributs(stream);
    this.WriteChilds(stream, filemanager);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
    if (!_context.firsttitle)
    {
      StringBuilder globalContent1_2 = new StringBuilder();
      _context.WikiPageTitle = this.ReadChilds(globalContent1_2, _context);
      _context.firsttitle = true;
      return globalContent1_1;
    }
    if (_context.IsFigure)
    {
      StringBuilder globalContent1_3 = new StringBuilder();
      _context.Title = this.ReadChilds(globalContent1_3, _context);
      return globalContent1_1;
    }
    if (_context.IsTable)
    {
      globalContent1_1.Append("\n");
      globalContent1_1.Append("===");
      StringBuilder stringBuilder = this.ReadChilds(globalContent1_1, _context);
      stringBuilder.Append("===");
      return stringBuilder;
    }
    globalContent1_1.Append("\n");
    globalContent1_1.Append("==");
    StringBuilder stringBuilder1 = this.ReadChilds(globalContent1_1, _context);
    stringBuilder1.Append("==");
    return stringBuilder1;
  }
}
