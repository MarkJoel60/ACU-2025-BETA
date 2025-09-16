// Decompiled with JetBrains decompiler
// Type: PX.SM.TableReference
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity;
using System;
using System.Web.Compilation;

#nullable enable
namespace PX.SM;

[PXHidden]
[Serializable]
public class TableReference : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Parent Table", Enabled = false)]
  public 
  #nullable disable
  string ParentFullName { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Child Table", Enabled = false)]
  public string ChildFullName { get; set; }

  [PXUIField(DisplayName = "Parent Table", Enabled = false)]
  public string ParentDac { get; set; }

  [PXUIField(DisplayName = "Child Table", Enabled = false)]
  public string ChildDac { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Parent Key Fields", Enabled = false)]
  public string ParentFields { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Description", Enabled = false, Visible = false)]
  public string ChildDescription
  {
    get
    {
      if (string.IsNullOrEmpty(this.ChildFullName))
        return (string) null;
      System.Type type = PXBuildManager.GetType(this.ChildFullName, false);
      return (object) type == null ? (string) null : XmlDocsExtensions.GetXmlDocsSummary(type, true);
    }
  }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Child Key Fields", Enabled = false)]
  public string ChildFields { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Achieved By", Enabled = false)]
  [PXStringList(new string[] {"ForeignKeyApi", "ParentAttribute", "DeclareReferenceAttribute", "SelectorAttribute", "WhereInCustomSelect", "JoinInCustomSelect"}, new string[] {"Foreign Key API", "Parent Attribute", "Declared Reference Attribute", "Selector Attribute", "Where In Custom Select", "Join In Custom Select"})]
  public string AchievedBy { get; set; }

  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Behavior", Enabled = false)]
  [PXStringList(new string[] {"NoAction", "Restrict", "SetNull", "SetDefault", "Cascade"}, new string[] {"No Action", "Restrict", "Set Null", "Set Default", "Cascade"})]
  public string Behavior { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Parent Select", Enabled = false)]
  public string ParentSelect { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Child Select", Enabled = false)]
  public string ChildSelect { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Original Select", Enabled = false)]
  public string OriginalSelect { get; set; }

  public TableReference()
  {
  }

  public TableReference(Reference reference)
  {
    this.Reference = reference;
    this.ChildFullName = reference.Child.Table.FullName.Replace("+", "..");
    this.ChildDac = reference.Child.Table.Name;
    this.ChildFields = reference.Child.KeyFieldsToString;
    this.ParentFullName = reference.Parent.Table.FullName.Replace("+", "..");
    this.ParentDac = reference.Parent.Table.Name;
    this.ParentFields = reference.Parent.KeyFieldsToString;
    this.AchievedBy = reference.ReferenceOrigin.ToString();
    this.Behavior = reference.ReferenceBehavior.ToString();
    System.Type parentSelect = reference.ParentSelect;
    this.ParentSelect = this.ChopBql(((object) parentSelect != null ? parentSelect.ToCodeString() : (string) null) ?? "");
    System.Type childrenSelect = reference.ChildrenSelect;
    this.ChildSelect = this.ChopBql(((object) childrenSelect != null ? childrenSelect.ToCodeString() : (string) null) ?? "");
    System.Type originalSelect = reference.OriginalSelect;
    this.OriginalSelect = this.ChopBql(((object) originalSelect != null ? originalSelect.ToCodeString() : (string) null) ?? "");
  }

  public virtual Reference Reference { get; }

  protected string ChopBql(string bql)
  {
    string[] strArray = new string[13]
    {
      "Where<",
      "Where2<",
      "InnerJoin<",
      "LeftJoin<",
      "RightJoin<",
      "CrossJoin<",
      "FullJoin<",
      "And<",
      "And2<",
      "Or<",
      "Or2<",
      "OrderBy<",
      "GroupBy<"
    };
    string str = bql.Replace(Environment.NewLine, "");
    foreach (string oldValue in strArray)
      str = str.Replace(oldValue, Environment.NewLine + oldValue);
    return str;
  }

  public abstract class parentFullName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TableReference.parentFullName>
  {
  }

  public abstract class childFullName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TableReference.childFullName>
  {
  }

  public abstract class parentDac : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.parentDac>
  {
  }

  public abstract class childDac : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.childDac>
  {
  }

  public abstract class parentFields : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.parentFields>
  {
  }

  public abstract class childDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TableReference.childDescription>
  {
  }

  public abstract class childFields : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.childFields>
  {
  }

  public abstract class achievedBy : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.achievedBy>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.behavior>
  {
  }

  public abstract class parentSelect : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.parentSelect>
  {
  }

  public abstract class childSelect : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TableReference.childSelect>
  {
  }

  public abstract class originalSelect : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TableReference.originalSelect>
  {
  }
}
