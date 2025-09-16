// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXSteps_informalDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public class PXSteps_informalDitaElement : PXDitaContainer
{
  public override void Write(XmlTextWriter stream, IFileManager filemanager)
  {
    throw new NotImplementedException();
  }

  public override StringBuilder Read(StringBuilder globalContent1, ExportContext _context)
  {
    StringBuilder globalContent1_1 = new StringBuilder();
    globalContent1_1.Append((object) globalContent1);
    return this.ReadChilds(globalContent1_1, _context);
  }
}
