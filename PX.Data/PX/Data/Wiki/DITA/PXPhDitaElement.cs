// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXPhDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXPhDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    stream.WriteStartElement("ph");
    this.WriteAttributs(stream);
    this.WriteChilds(stream, filemanager);
    stream.WriteEndElement();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
    string str1 = (string) null;
    string str2 = (string) null;
    if (!this.Attributs.TryGetValue("props", out str2))
      str2 = (string) null;
    if (!this.Attributs.TryGetValue("id", out str1))
      str1 = (string) null;
    if (str1 != null)
    {
      if (str2 != null)
      {
        _context.IsShared = true;
        _context.currentShared = str1;
      }
      else
      {
        globalContent1_1.Append("\n");
        globalContent1_1.Append("[anchor|#");
        globalContent1_1.Append(str1);
        globalContent1_1.Append("]");
      }
    }
    if (!this.Attributs.TryGetValue("conref", out str1))
      str1 = (string) null;
    if (str1 != null)
    {
      int num = str1.LastIndexOf("/");
      string key = str1.Remove(0, num + 1);
      try
      {
        globalContent1_1.Append(_context._ConRef[key]);
      }
      catch (Exception ex)
      {
      }
    }
    _context.IsPh = true;
    StringBuilder stringBuilder = this.ReadChilds(globalContent1_1, _context);
    _context.IsPh = false;
    _context.IsShared = false;
    return stringBuilder;
  }
}
