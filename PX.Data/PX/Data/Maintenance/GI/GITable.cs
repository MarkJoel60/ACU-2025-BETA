// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.GITable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Web.Compilation;

#nullable enable
namespace PX.Data.Maintenance.GI;

/// <exclude />
[PXCacheName("Generic Inquiry Table")]
public class GITable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public Guid? DesignID { get; set; }

  [PXDBString(IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Alias", Visibility = PXUIVisibility.SelectorVisible)]
  [PXParent(typeof (Select<GIDesign, Where<GIDesign.designID, Equal<Current<GITable.designID>>>>))]
  public 
  #nullable disable
  string Alias { get; set; }

  [PXDBString(InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Source Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXGenericInquirySourceSelector(Filterable = true)]
  public string Name { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public string Description
  {
    get
    {
      int? type1 = this.Type;
      int num = 1;
      if (!(type1.GetValueOrDefault() == num & type1.HasValue))
      {
        if (string.IsNullOrEmpty(this.Name))
          return (string) null;
        System.Type type2 = PXBuildManager.GetType(this.Name, false);
        return (object) type2 == null ? (string) null : XmlDocsExtensions.GetXmlDocsSummary(type2, true);
      }
      Guid result;
      return GIScreenHelper.TryGetSiteMapNode(PXGenericInqGrph.Def[Guid.TryParse(this.Name, out result) ? result : Guid.Empty]?.Design)?.Title;
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Type", Visible = false)]
  [PXFormula(typeof (BqlOperand<PXGenericInquirySourceSelectorAttribute.GenericInquirySource.type, IBqlInt>.FromSelectorOf<GITable.name>))]
  public int? Type { get; set; }

  [PXFormula(typeof (Switch<Case<Where<GITable.type, Equal<One>>, False>, True>))]
  [PXUIField(DisplayName = "", Visibility = PXUIVisibility.Invisible, Visible = false, Enabled = false)]
  [PXBool]
  public bool? IsAddRelatedTableAllowed { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<GITable>.By<GITable.designID, GITable.alias>
  {
    public static GITable Find(PXGraph graph, Guid? designID, string alias, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<GITable>.By<GITable.designID, GITable.alias>.FindBy(graph, (object) designID, (object) alias, options);
    }
  }

  public static class FK
  {
    public class Design : 
      PrimaryKeyOf<GIDesign>.By<GIDesign.designID>.ForeignKeyOf<GITable>.By<GITable.designID>
    {
    }
  }

  /// <exclude />
  public abstract class designID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GITable.designID>
  {
  }

  /// <exclude />
  public abstract class alias : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GITable.alias>
  {
  }

  /// <exclude />
  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GITable.name>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GITable.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GITable.type>
  {
  }

  public abstract class isAddRelatedTableAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    GITable.isAddRelatedTableAllowed>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GITable.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GITable.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GITable.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GITable.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GITable.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GITable.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    GITable.lastModifiedDateTime>
  {
  }
}
