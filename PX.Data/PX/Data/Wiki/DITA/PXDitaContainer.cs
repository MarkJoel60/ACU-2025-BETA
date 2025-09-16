// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXDitaContainer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public abstract class PXDitaContainer : PXDitaElement
{
  public void WriteChilds(XmlTextWriter stream, IFileManager filemanager)
  {
    foreach (PXDitaElement child in this.Childs)
      child.Write(stream, filemanager);
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    throw new NotImplementedException();
  }

  public StringBuilder ReadChilds(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent = new StringBuilder();
    globalContent.Append((object) globalContent1);
    foreach (PXDitaElement child in this.Childs)
      globalContent = child.Read(globalContent, _context);
    return globalContent;
  }
}
