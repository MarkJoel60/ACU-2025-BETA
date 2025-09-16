// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Projections.ExcludedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Projections;

[PXCacheName("Attribute Excluded From Update of Matrix Items")]
[PXBreakInheritance]
[PXProjection(typeof (Select<INMatrixExcludedData, Where<INMatrixExcludedData.type, Equal<INMatrixExcludedData.type.attribute>>>), Persistent = true)]
public class ExcludedAttribute : INMatrixExcludedData
{
  /// <summary>Type of row: 'F' - field, 'A' - attribute.</summary>
  [PXDBString(1, IsKey = true, IsFixed = true, IsUnicode = false)]
  [INMatrixExcludedData.type.List]
  [PXDefault("A")]
  public override 
  #nullable disable
  string Type
  {
    get => base.Type;
    set => base.Type = value;
  }

  /// <summary>References to a DAC name.</summary>
  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXUIField(DisplayName = "Table Name", Required = true)]
  [PXDefault(typeof (Constants.DACName<CSAttribute>))]
  public override string TableName
  {
    get => base.TableName;
    set => base.TableName = value;
  }

  /// <summary>
  /// References to field name of related DAC <see cref="P:PX.Objects.IN.Matrix.DAC.Projections.ExcludedAttribute.TableName" />.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsKey = true)]
  [PXUIField(DisplayName = "Attribute", Required = true)]
  [PXDefault]
  [ExcludedFieldSelector("A")]
  public override string FieldName
  {
    get => base.FieldName;
    set => base.FieldName = value;
  }

  /// <summary>Template Inventory Item identifier.</summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (InventoryItem.inventoryID))]
  [PXParent(typeof (INMatrixExcludedData.FK.TemplateInventoryItem))]
  public override int? TemplateID
  {
    get => base.TemplateID;
    set => base.TemplateID = value;
  }

  public new class PK : 
    PrimaryKeyOf<ExcludedAttribute>.By<ExcludedAttribute.templateID, ExcludedAttribute.type, ExcludedAttribute.tableName, ExcludedAttribute.fieldName>
  {
    public static ExcludedAttribute Find(
      PXGraph graph,
      int? templateID,
      string type,
      string tableName,
      string fieldName,
      PKFindOptions options = 0)
    {
      return (ExcludedAttribute) PrimaryKeyOf<ExcludedAttribute>.By<ExcludedAttribute.templateID, ExcludedAttribute.type, ExcludedAttribute.tableName, ExcludedAttribute.fieldName>.FindBy(graph, (object) templateID, (object) type, (object) tableName, (object) fieldName, options);
    }
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExcludedAttribute.type>
  {
  }

  public new abstract class tableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExcludedAttribute.tableName>
  {
  }

  public new abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExcludedAttribute.fieldName>
  {
  }

  public new abstract class templateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ExcludedAttribute.templateID>
  {
  }
}
