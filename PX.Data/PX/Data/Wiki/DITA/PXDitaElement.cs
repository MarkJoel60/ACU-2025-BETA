// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.DITA.PXDitaElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Data.Wiki.DITA;

public abstract class PXDitaElement
{
  public List<PXDitaElement> Childs;
  public Dictionary<string, string> Attributs;

  public PXDitaElement()
  {
    this.Childs = new List<PXDitaElement>();
    this.Attributs = new Dictionary<string, string>();
  }

  public void AddChild(PXDitaElement pxditaelement) => this.Childs.Add(pxditaelement);

  public void AddAttribute(string attribute, string value)
  {
    if (this.Attributs.ContainsKey(attribute))
      this.Attributs[attribute] = value;
    else
      this.Attributs.Add(attribute, value);
  }

  public abstract void Write(XmlTextWriter stream, IFileManager filemanager);

  public void WriteAttributs(XmlTextWriter stream)
  {
    foreach (KeyValuePair<string, string> attribut in this.Attributs)
      stream.WriteAttributeString(attribut.Key, attribut.Value);
  }

  public abstract StringBuilder Read(StringBuilder globalContent, ExportContext _context);

  public void ReadAttributs()
  {
  }
}
