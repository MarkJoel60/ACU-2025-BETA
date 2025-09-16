// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GINavigationCondition
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
[PXCacheName("Generic Inquiry Navigation Condition")]
public class GINavigationCondition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (GIDesign.designID))]
  [PXParent(typeof (Select<GINavigationScreen, Where<GINavigationScreen.designID, Equal<Current<GINavigationCondition.designID>>, And<GINavigationScreen.lineNbr, Equal<Current<GINavigationCondition.navigationScreenLineNbr>>>>>))]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GINavigationScreen))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (GINavigationScreen.lineNbr))]
  public virtual int? NavigationScreenLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Data Field")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public 
  #nullable disable
  string DataField { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [FilterRow.OpenBrackets]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? OpenBrackets { get; set; }

  [PXDefault(0)]
  [PXDBInt]
  [FilterRow.CloseBrackets]
  [PXUIField(DisplayName = "Brackets")]
  public virtual int? CloseBrackets { get; set; }

  [PXDBString(2, InputMask = "")]
  [PXUIField(DisplayName = "Condition")]
  [PXConditionList]
  [PXDefault("E")]
  public string Condition { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public bool? IsExpression { get; set; }

  [PXFormulaEditor(DisplayName = "Value")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ValueSt { get; set; }

  [PXFormulaEditor(DisplayName = "Value 2")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ValueSt2 { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Operator")]
  [PXStringList(new string[] {"A", "O"}, new string[] {"And", "Or"})]
  public virtual string Operator { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By", Enabled = false, Visible = false)]
  public Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Creation Date", Enabled = false, Visible = false)]
  public System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public System.DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<GINavigationCondition>.By<GINavigationCondition.designID, GINavigationCondition.navigationScreenLineNbr, GINavigationCondition.lineNbr>
  {
    public static GINavigationCondition Find(
      PXGraph graph,
      Guid? designID,
      int? navigationScreenLineNbr,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GINavigationCondition>.By<GINavigationCondition.designID, GINavigationCondition.navigationScreenLineNbr, GINavigationCondition.lineNbr>.FindBy(graph, (object) designID, (object) navigationScreenLineNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GINavigationCondition>.By<GINavigationCondition.designID>
    {
    }

    public class NavigationScreen : 
      PrimaryKeyOf<GINavigationScreen>.By<GINavigationScreen.designID, GINavigationScreen.lineNbr>.ForeignKeyOf<GINavigationCondition>.By<GINavigationCondition.designID, GINavigationCondition.navigationScreenLineNbr>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GINavigationCondition.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GINavigationCondition.lineNbr>
  {
  }

  public abstract class navigationScreenLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GINavigationCondition.navigationScreenLineNbr>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GINavigationCondition.isActive>
  {
  }

  /// <exclude />
  public abstract class dataField : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationCondition.dataField>
  {
  }

  public abstract class openBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GINavigationCondition.openBrackets>
  {
  }

  public abstract class closeBrackets : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GINavigationCondition.closeBrackets>
  {
  }

  /// <exclude />
  public abstract class condition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationCondition.condition>
  {
  }

  /// <exclude />
  public abstract class isExpression : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GINavigationCondition.isExpression>
  {
  }

  public abstract class valueSt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationCondition.valueSt>
  {
  }

  public abstract class valueSt2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GINavigationCondition.valueSt2>
  {
  }

  public abstract class @operator : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationCondition.@operator>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GINavigationCondition.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationCondition.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationCondition.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GINavigationCondition.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GINavigationCondition.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GINavigationCondition.lastModifiedDateTime>
  {
  }
}
