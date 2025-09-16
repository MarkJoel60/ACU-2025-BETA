// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.ExcludedFieldSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.Matrix.Attributes;

public class ExcludedFieldSelectorAttribute : PXCustomSelectorAttribute
{
  protected 
  #nullable disable
  string _type;

  public virtual ICreateMatrixHelperFactory CreateMatrixHelperFactory { get; set; }

  public ExcludedFieldSelectorAttribute(string type)
    : base(type == "F" ? typeof (Search<ExcludedFieldSelectorAttribute.ExcludedFieldName.name, Where<ExcludedFieldSelectorAttribute.ExcludedFieldName.tableName, Equal<Current<ExcludedField.tableName>>>>) : typeof (Search<ExcludedFieldSelectorAttribute.ExcludedFieldName.name, Where<ExcludedFieldSelectorAttribute.ExcludedFieldName.tableName, Equal<Constants.DACName<CSAttribute>>>>), new Type[2]
    {
      typeof (ExcludedFieldSelectorAttribute.ExcludedFieldName.description),
      typeof (ExcludedFieldSelectorAttribute.ExcludedFieldName.name)
    })
  {
    this._type = type;
    ((PXSelectorAttribute) this).CacheGlobal = false;
    ((PXSelectorAttribute) this).ValidateValue = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (ExcludedFieldSelectorAttribute.ExcludedFieldName.description);
    ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) 24;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (this.CreateMatrixHelperFactory != null)
      return;
    this.CreateMatrixHelperFactory = sender.Graph as ICreateMatrixHelperFactory;
  }

  protected virtual IEnumerable GetRecords()
  {
    List<ExcludedFieldSelectorAttribute.ExcludedFieldName> records = new List<ExcludedFieldSelectorAttribute.ExcludedFieldName>();
    CreateMatrixItemsHelper matrixItemsHelper = this.CreateMatrixHelperFactory?.GetCreateMatrixItemsHelper();
    if (matrixItemsHelper == null)
      return (IEnumerable) records;
    switch (this._type)
    {
      case "A":
        records.AddRange(this.GetAttributes(matrixItemsHelper));
        break;
      case "F":
        object[] parameters = PXView.Parameters;
        if ((parameters != null ? (parameters.Length != 0 ? 1 : 0) : 0) != 0)
        {
          string tableName = PXView.Parameters[0] as string;
          (Type Dac, string DisplayName)[] tablesToUpdateItem = matrixItemsHelper.GetTablesToUpdateItem();
          Type table = tablesToUpdateItem != null ? ((IEnumerable<(Type, string)>) tablesToUpdateItem).Where<(Type, string)>((Func<(Type, string), bool>) (t => t.Dac.FullName == tableName)).FirstOrDefault<(Type, string)>().Item1 : (Type) null;
          if (table != (Type) null)
          {
            records.AddRange(this.GetFields(table, matrixItemsHelper));
            break;
          }
          break;
        }
        break;
      default:
        throw new NotImplementedException();
    }
    return (IEnumerable) records;
  }

  protected virtual IEnumerable<ExcludedFieldSelectorAttribute.ExcludedFieldName> GetFields(
    Type table,
    CreateMatrixItemsHelper itemsHelper)
  {
    return ((IEnumerable<(string, string)>) itemsHelper.GetFieldsToUpdateItem(table)).Select<(string, string), ExcludedFieldSelectorAttribute.ExcludedFieldName>((Func<(string, string), ExcludedFieldSelectorAttribute.ExcludedFieldName>) (field => new ExcludedFieldSelectorAttribute.ExcludedFieldName()
    {
      TableName = table.FullName,
      Name = field.FieldName,
      Description = field.DisplayName
    }));
  }

  protected virtual IEnumerable<ExcludedFieldSelectorAttribute.ExcludedFieldName> GetAttributes(
    CreateMatrixItemsHelper itemsHelper)
  {
    InventoryItem current = (InventoryItem) ((PXCache) GraphHelper.Caches<InventoryItem>(this._Graph)).Current;
    return current == null ? (IEnumerable<ExcludedFieldSelectorAttribute.ExcludedFieldName>) new ExcludedFieldSelectorAttribute.ExcludedFieldName[0] : ((IEnumerable<(string, string)>) itemsHelper.GetAttributesToUpdateItem(current)).Select<(string, string), ExcludedFieldSelectorAttribute.ExcludedFieldName>((Func<(string, string), ExcludedFieldSelectorAttribute.ExcludedFieldName>) (field => new ExcludedFieldSelectorAttribute.ExcludedFieldName()
    {
      TableName = typeof (CSAttribute).FullName,
      Name = field.FieldName,
      Description = field.DisplayName
    }));
  }

  [PXCacheName("Field Name Excluded From Update of Matrix Items")]
  [PXVirtual]
  public class ExcludedFieldName : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "Table Name")]
    public string TableName { get; set; }

    [PXString(IsKey = true)]
    [PXUIField(DisplayName = "DB Field Name")]
    public string Name { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Box Name")]
    public string Description { get; set; }

    public abstract class tableName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExcludedFieldSelectorAttribute.ExcludedFieldName.tableName>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExcludedFieldSelectorAttribute.ExcludedFieldName.name>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExcludedFieldSelectorAttribute.ExcludedFieldName.description>
    {
    }
  }
}
