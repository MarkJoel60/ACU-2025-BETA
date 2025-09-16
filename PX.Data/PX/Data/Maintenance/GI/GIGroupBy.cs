// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GIGroupBy
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
[PXCacheName("Generic Inquiry Grouping")]
public class GIGroupBy : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (GIDesign))]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GIGroupBy.designID>>>>))]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXFormulaEditor(DisplayName = "Data Field")]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  [PXDefault]
  public 
  #nullable disable
  string DataFieldName { get; set; }

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

  public class PK : PrimaryKeyOf<GIGroupBy>.By<GIGroupBy.designID, GIGroupBy.lineNbr>
  {
    public static GIGroupBy Find(
      PXGraph graph,
      Guid? designID,
      int? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GIGroupBy>.By<GIGroupBy.designID, GIGroupBy.lineNbr>.FindBy(graph, (object) designID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GIGroupBy>.By<GIGroupBy.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIGroupBy.designID>
  {
  }

  /// <exclude />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GIGroupBy.lineNbr>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  GIGroupBy.isActive>
  {
  }

  /// <exclude />
  public abstract class dataFieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GIGroupBy.dataFieldName>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIGroupBy.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIGroupBy.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIGroupBy.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIGroupBy.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GIGroupBy.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GIGroupBy.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GIGroupBy.lastModifiedDateTime>
  {
  }
}
