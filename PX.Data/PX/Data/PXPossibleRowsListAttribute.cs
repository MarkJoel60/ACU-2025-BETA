// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPossibleRowsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>User friendly name of a DAC class.</summary>
/// <exclude />
public class PXPossibleRowsListAttribute : Attribute
{
  protected BqlCommand _Select;
  protected string _IDFieldName;
  protected string _ValueFieldName;

  public PXPossibleRowsListAttribute(System.Type select, System.Type idField, System.Type valueField)
  {
    if (select == (System.Type) null)
      throw new PXArgumentException(nameof (select), "The argument cannot be null.");
    if (valueField == (System.Type) null)
      throw new PXArgumentException(nameof (valueField), "The argument cannot be null.");
    if (idField == (System.Type) null)
      throw new PXArgumentException(nameof (idField), "The argument cannot be null.");
    this._ValueFieldName = char.ToUpper(valueField.Name[0]).ToString() + (valueField.Name.Length > 1 ? valueField.Name.Substring(1) : "");
    this._IDFieldName = char.ToUpper(idField.Name[0]).ToString() + (idField.Name.Length > 1 ? idField.Name.Substring(1) : "");
    if (typeof (IBqlSearch).IsAssignableFrom(select))
    {
      this._Select = BqlCommand.CreateInstance(select);
      BqlCommand.GetItemType(((IBqlSearch) this._Select).GetField());
    }
    else
      this._Select = select.IsNested && typeof (IBqlField).IsAssignableFrom(select) ? BqlCommand.CreateInstance(typeof (Search<>), select) : throw new PXArgumentException("type", "A foreign key reference cannot be created from the type '{0}'.", new object[1]
      {
        (object) select
      });
  }

  public virtual List<string> GetPossibleRows(
    PXGraph graph,
    out string idField,
    out string valueField)
  {
    idField = this._IDFieldName;
    valueField = this._ValueFieldName;
    HashSet<string> source = new HashSet<string>();
    PXView pxView = new PXView(graph, true, this._Select);
    string name = ((IBqlSearch) this._Select).GetField().Name;
    foreach (object data in pxView.SelectMulti())
    {
      string str = pxView.Cache.GetValue(data, name) as string;
      if (!string.IsNullOrWhiteSpace(str))
        source.Add(str);
    }
    return source.ToList<string>();
  }
}
