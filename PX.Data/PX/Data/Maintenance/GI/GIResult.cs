// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXCacheName("Generic Inquiry Result")]
public class GIResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIResult.designID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Object", Visibility = PXUIVisibility.SelectorVisible)]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public 
  #nullable disable
  string ObjectName { get; set; }

  [PXFormulaEditor(DisplayName = "Data Field")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXDefault]
  public string Field { get; set; }

  [PXString(InputMask = "", IsUnicode = true)]
  public string FieldName { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Schema Field")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public string SchemaField { get; set; }

  [PXDBLocalizableString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Caption", Visible = false)]
  public string Caption { get; set; }

  [PXFormulaEditor(DisplayName = "Style")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor.AddStyles]
  public string StyleFormula { get; set; }

  [PXDBInt(MinValue = 1, MaxValue = 99999)]
  [PXUIField(DisplayName = "Width (px)")]
  public virtual int? Width { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Visible")]
  public bool? IsVisible { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Default Navigation")]
  public bool? DefaultNav { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBGuid(true)]
  [PXDefault]
  public Guid? RowID { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Aggregate Function", Visible = false)]
  [PXAggregateFunctionsList(false)]
  public string AggregateFunction { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Total Aggregate Function", Visible = false)]
  [PXAggregateFunctionsList(true)]
  public string TotalAggregateFunction { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Navigate To")]
  [PXIntList(new int[] {-1}, new string[] {""})]
  public int? NavigationNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Quick Filter", Visible = false)]
  public bool? QuickFilter { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use in Quick Search", Visible = false)]
  public bool? FastFilter { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<GIResult>.By<GIResult.designID, GIResult.lineNbr>
  {
    public static GIResult Find(
      PXGraph graph,
      Guid? designID,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIResult>.By<GIResult.designID, GIResult.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIResult>.By<GIResult.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIResult.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIResult.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIResult.sortOrder>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIResult.isActive>
  {
  }

  /// <exclude />
  public abstract class objectName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.objectName>
  {
  }

  /// <exclude />
  public abstract class field : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.field>
  {
  }

  /// <exclude />
  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.fieldName>
  {
  }

  /// <exclude />
  public abstract class schemaField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.schemaField>
  {
  }

  /// <exclude />
  public abstract class caption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.caption>
  {
  }

  /// <exclude />
  public abstract class styleFormula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.styleFormula>
  {
  }

  public abstract class width : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIResult.width>
  {
  }

  /// <exclude />
  public abstract class isVisible : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIResult.isVisible>
  {
  }

  /// <exclude />
  public abstract class defaultNav : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIResult.defaultNav>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIResult.noteID>
  {
  }

  public abstract class rowID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIResult.rowID>
  {
  }

  /// <exclude />
  public abstract class aggregateFunction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIResult.aggregateFunction>
  {
  }

  /// <exclude />
  public abstract class totalAggregateFunction : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class navigationNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIResult.navigationNbr>
  {
  }

  public abstract class quickFilter : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIResult.quickFilter>
  {
  }

  public abstract class fastFilter : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIResult.fastFilter>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIResult.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIResult.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIResult.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIResult.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIResult.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIResult.lastModifiedDateTime>
  {
  }
}
