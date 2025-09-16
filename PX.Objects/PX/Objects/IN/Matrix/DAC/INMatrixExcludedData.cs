// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.INMatrixExcludedData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC;

[PXCacheName("Data Excluded From Update of Matrix Items")]
public class INMatrixExcludedData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Type of row: 'F' - field, 'A' - attribute.</summary>
  [PXDBString(1, IsKey = true, IsFixed = true, IsUnicode = false)]
  [INMatrixExcludedData.type.List]
  public virtual 
  #nullable disable
  string Type { get; set; }

  /// <summary>References to a DAC name.</summary>
  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXUIField(DisplayName = "Table Name", Required = true)]
  [PXDefault]
  [PXStringList]
  public virtual string TableName { get; set; }

  /// <summary>
  /// References to field name of related DAC <see cref="P:PX.Objects.IN.Matrix.DAC.INMatrixExcludedData.TableName" />.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXUIField(DisplayName = "Field Name", Required = true)]
  [PXDefault]
  public virtual string FieldName { get; set; }

  /// <summary>Template Inventory Item identifier.</summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  public virtual int? TemplateID { get; set; }

  /// <summary>Indicates if the exclusion is active at the moment</summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public virtual bool? IsActive { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INMatrixExcludedData>.By<INMatrixExcludedData.templateID, INMatrixExcludedData.type, INMatrixExcludedData.tableName, INMatrixExcludedData.fieldName>
  {
    public static INMatrixExcludedData Find(
      PXGraph graph,
      int? templateID,
      string type,
      string tableName,
      string fieldName,
      PKFindOptions options = 0)
    {
      return (INMatrixExcludedData) PrimaryKeyOf<INMatrixExcludedData>.By<INMatrixExcludedData.templateID, INMatrixExcludedData.type, INMatrixExcludedData.tableName, INMatrixExcludedData.fieldName>.FindBy(graph, (object) templateID, (object) type, (object) tableName, (object) fieldName, options);
    }
  }

  public static class FK
  {
    public class TemplateInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INMatrixGenerationRule>.By<INMatrixExcludedData.templateID>
    {
    }
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMatrixExcludedData.type>
  {
    public const string Field = "F";
    public const string Attribute = "A";

    [PXLocalizable]
    public class DisplayNames
    {
      public const string Field = "Field";
      public const string Attribute = "Attribute";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("F", "Field"),
          PXStringListAttribute.Pair("A", "Attribute")
        })
      {
      }
    }

    public class field : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INMatrixExcludedData.type.field>
    {
      public field()
        : base("F")
      {
      }
    }

    public class attribute : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INMatrixExcludedData.type.attribute>
    {
      public attribute()
        : base("A")
      {
      }
    }
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMatrixExcludedData.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMatrixExcludedData.fieldName>
  {
  }

  public abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INMatrixExcludedData.templateID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INMatrixExcludedData.isActive>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INMatrixExcludedData.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INMatrixExcludedData.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixExcludedData.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMatrixExcludedData.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INMatrixExcludedData.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMatrixExcludedData.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMatrixExcludedData.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INMatrixExcludedData.Tstamp>
  {
  }
}
