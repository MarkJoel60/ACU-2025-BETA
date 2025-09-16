// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAnnouncement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.SM;

[PXTable(new System.Type[] {typeof (WikiPage.pageID)})]
[DebuggerDisplay("{Title} ({StartDate})")]
[Serializable]
public class WikiAnnouncement : WikiPage
{
  protected System.DateTime? _StartDate;
  protected System.DateTime? _ExpireDate;
  protected bool? _KeepOnTop;
  protected bool? _HideOnExpire;
  protected bool? _HiddenDashboard;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Folder")]
  [PXSelector(typeof (Search<WikiPageSimple.pageID, Where<WikiPageSimple.wikiID, Equal<Current<WikiPage.wikiID>>, Or<WikiPageSimple.pageID, Equal<Current<WikiPage.wikiID>>>>>), SubstituteKey = typeof (WikiPageSimple.name))]
  public override Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBInt]
  [PXDefault(11)]
  public override int? ArticleType
  {
    get => this._ArticleType;
    set => this._ArticleType = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true)]
  [PXUIField(DisplayName = "Start Date")]
  [PXDefault]
  public virtual System.DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  [PXUIField(DisplayName = "Expiration Date")]
  public virtual System.DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Keep on Top")]
  public virtual bool? KeepOnTop
  {
    get => this._KeepOnTop;
    set => this._KeepOnTop = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hide From Dashboards on Expiration")]
  [PXDefault(false)]
  public virtual bool? HideOnExpire
  {
    get => this._HideOnExpire;
    set => this._HideOnExpire = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Hide on Dashboard")]
  [PXDefault(false)]
  public virtual bool? HiddenDashboard
  {
    get => this._HiddenDashboard;
    set => this._HiddenDashboard = value;
  }

  public new abstract class wikiID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAnnouncement.wikiID>
  {
  }

  public new abstract class pageID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAnnouncement.pageID>
  {
  }

  public new abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAnnouncement.parentUID>
  {
  }

  public new abstract class articleType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiAnnouncement.articleType>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  WikiAnnouncement.startDate>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiAnnouncement.expireDate>
  {
  }

  public abstract class keepOnTop : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiAnnouncement.keepOnTop>
  {
  }

  public abstract class hideOnExpire : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WikiAnnouncement.hideOnExpire>
  {
  }

  public abstract class hiddenDashboard : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    WikiAnnouncement.hiddenDashboard>
  {
    public class True : BqlType<
    #nullable enable
    IBqlBool, bool>.Constant<
    #nullable disable
    WikiAnnouncement.hiddenDashboard.True>
    {
      public True()
        : base(true)
      {
      }
    }

    public class Zero : BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    WikiAnnouncement.hiddenDashboard.Zero>
    {
      public Zero()
        : base(0)
      {
      }
    }
  }

  public new abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAnnouncement.createdByID>
  {
  }
}
