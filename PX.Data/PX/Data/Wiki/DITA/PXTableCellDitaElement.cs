// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXTableCellDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXTableCellDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("entry");
    this.WriteAttributs(stream);
    this.WriteChilds(stream, filemanager);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
    int num = 0;
    string str1;
    if (this.Attributs.TryGetValue("morerows", out str1))
    {
      try
      {
        num = (int) Convert.ToInt16(str1);
      }
      catch
      {
      }
    }
    if (num > 0)
    {
      globalContent1_1.Append("\n|");
      globalContent1_1.Append("rowspan=");
      string str2 = $"\"{(num + 1).ToString()}\"";
      globalContent1_1.Append(str2);
      globalContent1_1.Append(" | ");
    }
    else if (_context.IsHeader)
      globalContent1_1.Append("\n!");
    else
      globalContent1_1.Append("\n|");
    return this.ReadChilds(globalContent1_1, _context);
  }
}
