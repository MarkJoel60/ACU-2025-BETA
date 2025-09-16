// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIWhere
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
[PXCacheName("Generic Inquiry Where Statement")]
public class GIWhere : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIWhere.designID>>>>))]
  public int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(9, InputMask = "")]
  [PXUIField(DisplayName = "Brackets")]
  [PXStringList("(;(,((;((,(((;(((,((((;((((,(((((;(((((,((((((;((((((,(((((((;(((((((,((((((((;((((((((,(((((((((;(((((((((")]
  public 
  #nullable disable
  string OpenBrackets { get; set; }

  [PXDBString(512 /*0x0200*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Data Field")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public string DataFieldName { get; set; }

  [PXDBString(2, InputMask = "")]
  [PXUIField(DisplayName = "Condition")]
  [PXConditionList]
  [PXDefault("E")]
  public string Condition { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsExpression { get; set; }

  [PXFormulaEditor(DisplayName = "Value 1")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  public string Value1 { get; set; }

  [PXFormulaEditor(DisplayName = "Value 2")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  public string Value2 { get; set; }

  [PXDBString(9, InputMask = "")]
  [PXUIField(DisplayName = "Brackets")]
  [PXStringList(");),));)),)));))),))));)))),)))));))))),))))));)))))),)))))));))))))),))))))));)))))))),)))))))));)))))))))")]
  public string CloseBrackets { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Operator")]
  [PXStringList(new string[] {"A", "O"}, new string[] {"And", "Or"})]
  public string Operation { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<GIWhere>.By<GIWhere.designID, GIWhere.lineNbr>
  {
    public static GIWhere Find(PXGraph graph, Guid? designID, int? lineNbr, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIWhere>.By<GIWhere.designID, GIWhere.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIWhere>.By<GIWhere.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIWhere.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIWhere.lineNbr>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIWhere.isActive>
  {
  }

  /// <exclude />
  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.openBrackets>
  {
  }

  /// <exclude />
  public abstract class dataFieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.dataFieldName>
  {
  }

  /// <exclude />
  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.condition>
  {
  }

  /// <exclude />
  public abstract class isExpression : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIWhere.isExpression>
  {
  }

  /// <exclude />
  public abstract class value1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.value1>
  {
  }

  /// <exclude />
  public abstract class value2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.value2>
  {
  }

  /// <exclude />
  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.closeBrackets>
  {
  }

  /// <exclude />
  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIWhere.operation>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIWhere.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIWhere.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIWhere.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIWhere.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIWhere.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIWhere.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIWhere.lastModifiedDateTime>
  {
  }
}
