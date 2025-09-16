// Decompiled with JetBrains decompiler
// Type: PX.Data.ForeignKeyChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class ForeignKeyChecker
{
  private PXCache _sender;
  private object _row;
  private System.Type _fieldType;
  private System.Type _searchType;
  public string CustomMessage;

  public ForeignKeyChecker(PXCache sender, object row, System.Type fieldType, System.Type searchType)
  {
    this._sender = sender;
    this._row = row;
    this._fieldType = fieldType;
    this._searchType = searchType;
  }

  public void DoCheck()
  {
    string currentTableName;
    string foreingTableName;
    if (this.isExists(out currentTableName, out foreingTableName))
    {
      string message1 = string.IsNullOrEmpty(this.CustomMessage) ? "The {0} record is referenced by the {1} record and cannot be deleted." : this.CustomMessage;
      string message2;
      if (message1.Contains("{0}") && message1.Contains("{1}"))
        message2 = PXLocalizer.LocalizeFormat(message1, (object) currentTableName, (object) foreingTableName);
      else
        message2 = PXLocalizer.Localize(message1);
      throw new PXException(message2);
    }
  }

  private bool isExists(out string currentTableName, out string foreingTableName)
  {
    if (this._searchType != (System.Type) null && !typeof (IBqlSearch).IsAssignableFrom(this._searchType))
      throw new PXArgumentException("selectType", "An invalid argument has been specified.");
    System.Type type1 = this._row.GetType();
    System.Type itemType = BqlCommand.GetItemType(this._fieldType);
    foreingTableName = this.getTableName(itemType);
    currentTableName = this.getTableName(type1);
    System.Type currentFieldType = this.getCurrentFieldType(this._sender, this._fieldType);
    if (currentFieldType == (System.Type) null)
      return false;
    System.Type type2;
    if (this._searchType == (System.Type) null)
    {
      type2 = BqlCommand.Compose(typeof (Search<,>), this._fieldType, typeof (Where<,>), this._fieldType, typeof (Equal<>), typeof (Current<>), currentFieldType);
    }
    else
    {
      List<System.Type> typeList = !(((IBqlSearch) Activator.CreateInstance(this._searchType)).GetType() != this._searchType) ? new List<System.Type>()
      {
        this._searchType.GetGenericTypeDefinition()
      } : throw new PXArgumentException("selectType", "An invalid argument has been specified.");
      typeList.AddRange((IEnumerable<System.Type>) this._searchType.GetGenericArguments());
      int index = typeList.FindIndex((Predicate<System.Type>) (arg => typeof (IBqlWhere).IsAssignableFrom(arg)));
      if (index == -1)
        throw new PXArgumentException("selectType", "An invalid argument has been specified.");
      typeList[index] = BqlCommand.Compose(typeof (Where2<,>), typeof (Where<,>), this._fieldType, typeof (Equal<>), typeof (Current<>), currentFieldType, typeof (And<>), typeList[index]);
      type2 = BqlCommand.Compose(typeList.ToArray());
    }
    return new PXView(this._sender.Graph, false, BqlCommand.CreateInstance(type2)).SelectSingleBound(new object[1]
    {
      this._row
    }) != null;
  }

  private string getTableName(System.Type TableType)
  {
    return TableType.IsDefined(typeof (PXCacheNameAttribute), true) ? ((PXNameAttribute) TableType.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName() : TableType.Name;
  }

  private System.Type getCurrentFieldType(PXCache sender, System.Type foreignFieldType)
  {
    return sender.Graph.Caches[BqlCommand.GetItemType(foreignFieldType)].GetAttributesReadonly(foreignFieldType.Name).OfType<PXSelectorAttribute>().Select<PXSelectorAttribute, System.Type>((Func<PXSelectorAttribute, System.Type>) (s => s.Field)).FirstOrDefault<System.Type>();
  }
}
