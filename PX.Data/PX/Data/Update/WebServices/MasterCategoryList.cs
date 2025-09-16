// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.WebServices.MasterCategoryList
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.WebServices;

[XmlType(AnonymousType = true, Namespace = "CategoryList.xsd")]
[XmlRoot(ElementName = "categories", Namespace = "CategoryList.xsd", IsNullable = false)]
[Serializable]
public class MasterCategoryList
{
  [XmlElement("category")]
  public List<Category> Categories { get; set; }

  [XmlIgnore]
  public Guid? DefaultCategory { get; set; }

  [XmlAttribute("default")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public string DefaultCategoryText
  {
    get => !this.DefaultCategory.HasValue ? string.Empty : this.DefaultCategory.ToString();
    set
    {
      Guid result;
      this.DefaultCategory = Guid.TryParse(value, out result) ? new Guid?(result) : new Guid?();
    }
  }

  [XmlAttribute("lastSavedSession")]
  public int LastSavedSession { get; set; }

  [XmlAttribute("lastSavedTime")]
  public System.DateTime LastSavedTime { get; set; }
}
