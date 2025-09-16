// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiDescriptorExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXSubstitute(GraphType = typeof (WikiSetupMaint))]
[PXSubstitute(GraphType = typeof (WikiMaintenance))]
[PXTable(new System.Type[] {typeof (WikiDescriptorExt.pageID)})]
[Serializable]
public class WikiDescriptorExt : WikiDescriptorMaster
{
  [PXDBInt]
  [PXDefault(10)]
  [PX.SM.WikiArticleType]
  [PXUIField(DisplayName = "Article Type")]
  public override int? WikiArticleType
  {
    get => base.WikiArticleType;
    set => base.WikiArticleType = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Template")]
  [PXContainerTemplateSelector(typeof (Current<WikiDescriptorExt.pageID>))]
  public virtual Guid? RootPageID { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  [PXWikiPageName(typeof (WikiDescriptorExt.rootPageID))]
  public virtual 
  #nullable disable
  string RootPageName { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Print Template")]
  [PXContainerTemplateSelector(typeof (Current<WikiDescriptorExt.pageID>))]
  public virtual Guid? RootPrintPageID { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Header")]
  [PXWikiPageTemplateSelector(typeof (Current<WikiDescriptorExt.pageID>))]
  public virtual Guid? HeaderPageID { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  [PXWikiPageName(typeof (WikiDescriptorExt.headerPageID))]
  public virtual string HeaderPageName { get; set; }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Footer")]
  [PXWikiPageTemplateSelector(typeof (Current<WikiDescriptorExt.pageID>))]
  public virtual Guid? FooterPageID { get; set; }

  [PXString]
  [PXUIField(Visible = false)]
  [PXWikiPageName(typeof (WikiDescriptorExt.footerPageID))]
  public virtual string FooterPageName { get; set; }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorExt.pageID>
  {
  }

  public new abstract class wikiID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorExt.wikiID>
  {
  }

  public new abstract class wikiArticleType : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    WikiDescriptorExt.wikiArticleType>
  {
  }

  public abstract class rootPageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorExt.rootPageID>
  {
  }

  public abstract class rootPageName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiDescriptorExt.rootPageName>
  {
  }

  public abstract class rootPrintPageID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiDescriptorExt.rootPrintPageID>
  {
  }

  public abstract class headerPageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorExt.headerPageID>
  {
  }

  public abstract class headerPageName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiDescriptorExt.headerPageName>
  {
  }

  public abstract class footerPageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiDescriptorExt.footerPageID>
  {
  }

  public abstract class footerPageName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WikiDescriptorExt.footerPageName>
  {
  }
}
