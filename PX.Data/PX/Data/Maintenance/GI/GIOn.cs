// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIOn
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
[PXCacheName("Generic Inquiry Relation Dependencies")]
public class GIOn : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<GIRelation, Where<GIRelation.designID, Equal<Current<GIOn.designID>>, And<GIRelation.lineNbr, Equal<Current<GIOn.relationNbr>>>>>))]
  public int? RelationNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIOn.designID>>>>), LeaveChildren = true)]
  public int? LineNbr { get; set; }

  [PXDBString(9, InputMask = "")]
  [PXUIField(DisplayName = "Brackets")]
  [PXStringList("(;(,((;((,(((;(((,((((;((((,(((((;(((((,((((((;((((((,(((((((;(((((((,((((((((;((((((((,(((((((((;(((((((((", IsLocalizable = false)]
  public 
  #nullable disable
  string OpenBrackets { get; set; }

  [PXFormulaEditor(DisplayName = "Parent Field")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  [PXDefault]
  public string ParentField { get; set; }

  [PXDBString(2, InputMask = "")]
  [PXUIField(DisplayName = "Condition")]
  [PXConditionList]
  [PXDefault("E")]
  public string Condition { get; set; }

  [PXFormulaEditor(DisplayName = "Child Field")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {""}, new string[] {""}, ExclusiveValues = false)]
  public string ChildField { get; set; }

  [PXDBString(9, InputMask = "")]
  [PXUIField(DisplayName = "Brackets")]
  [PXStringList(");),));)),)));))),))));)))),)))));))))),))))));)))))),)))))));))))))),))))))));)))))))),)))))))));)))))))))", IsLocalizable = false)]
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

  public class PK : PrimaryKeyOf<GIOn>.By<GIOn.designID, GIOn.relationNbr, GIOn.lineNbr>
  {
    public static GIOn Find(
      PXGraph graph,
      Guid? designID,
      int? relationNbr,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIOn>.By<GIOn.designID, GIOn.relationNbr, GIOn.lineNbr>.FindBy(graph, (object) designID, (object) relationNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIOn>.By<GIOn.designID>
    {
    }

    public class Relation : 
      PrimaryKeyOf<GIRelation>.By<GIRelation.designID, GIRelation.lineNbr>.ForeignKeyOf<GIOn>.By<GIOn.designID, GIOn.relationNbr>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIOn.designID>
  {
  }

  /// <exclude />
  public abstract class relationNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIOn.relationNbr>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIOn.lineNbr>
  {
  }

  /// <exclude />
  public abstract class openBrackets : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.openBrackets>
  {
  }

  /// <exclude />
  public abstract class parentField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.parentField>
  {
  }

  /// <exclude />
  public abstract class condition : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.condition>
  {
  }

  /// <exclude />
  public abstract class childField : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.childField>
  {
  }

  /// <exclude />
  public abstract class closeBrackets : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.closeBrackets>
  {
  }

  /// <exclude />
  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.operation>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIOn.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIOn.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIOn.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  GIOn.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIOn.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIOn.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIOn.lastModifiedDateTime>
  {
  }
}
