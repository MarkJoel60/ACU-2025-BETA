// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIRelation
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
[PXCacheName("Generic Inquiry Relation")]
public class GIRelation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIRelation.designID>>>>))]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Parent Table")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public 
  #nullable disable
  string ParentTable { get; set; }

  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Child Table")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public string ChildTable { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(1, InputMask = "")]
  [PXDefault("I")]
  [PXUIField(DisplayName = "Join Type")]
  [PXJoinTypeList]
  public string JoinType { get; set; }

  [PXFormula(typeof (IsDacAliases<GIRelation.parentTable, GIRelation.childTable>))]
  [PXUIField(DisplayName = "", Visibility = PXUIVisibility.Invisible, Visible = false, Enabled = false)]
  [PXBool]
  public bool? IsAddRelatedTableAllowed { get; set; }

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

  public class PK : PrimaryKeyOf<GIRelation>.By<GIRelation.designID, GIRelation.lineNbr>
  {
    public static GIRelation Find(
      PXGraph graph,
      Guid? designID,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIRelation>.By<GIRelation.designID, GIRelation.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIRelation>.By<GIRelation.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRelation.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIRelation.lineNbr>
  {
  }

  /// <exclude />
  public abstract class parentTable : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIRelation.parentTable>
  {
  }

  /// <exclude />
  public abstract class childTable : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIRelation.childTable>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIRelation.isActive>
  {
  }

  /// <exclude />
  public abstract class joinType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIRelation.joinType>
  {
  }

  public abstract class isAddRelatedTableAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GIRelation.isAddRelatedTableAllowed>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRelation.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRelation.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIRelation.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIRelation.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIRelation.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIRelation.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIRelation.lastModifiedDateTime>
  {
  }
}
