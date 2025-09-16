// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXXrefDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXXrefDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    throw new NotImplementedException();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
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
          globalContent1_1.Append("\n");
          globalContent1_1.Append("*");
        }
        globalContent1_1.Append("[Help\\");
      }
      if (startIndex2 > -1)
      {
        str = str.Remove(startIndex2, str.Length - startIndex2);
        globalContent1_1.Append("[");
      }
      if (startIndex1 <= -1 && startIndex2 <= -1)
        globalContent1_1.Append("[");
      if (str.LastIndexOf("#") > -1)
      {
        if (_context.IsRelatedLink)
        {
          globalContent1_1.Append("\n");
          globalContent1_1.Append("*");
        }
        int num = str.LastIndexOf("/");
        str = str.Remove(0, num + 1);
        globalContent1_1.Append("#");
      }
    }
    globalContent1_1.Append(str);
    globalContent1_1.Append("|");
    StringBuilder stringBuilder = this.ReadChilds(globalContent1_1, _context);
    stringBuilder.Append("]");
    return stringBuilder;
  }
}
