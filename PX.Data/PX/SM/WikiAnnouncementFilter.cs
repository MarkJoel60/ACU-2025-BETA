// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAnnouncementFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class WikiAnnouncementFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _WikiID;
  protected Guid? _CreatedByID;
  protected System.DateTime? _DateFrom;
  protected System.DateTime? _DateTo;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Wiki ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXWikiSelector(11, typeof (WikiDescriptor.pageID))]
  public virtual Guid? WikiID
  {
    get => this._WikiID;
    set => this._WikiID = value;
  }

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Created by")]
  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username))]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "From")]
  public System.DateTime? DateFrom
  {
    get => this._DateFrom;
    set => this._DateFrom = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "To")]
  public System.DateTime? DateTo
  {
    get => this._DateTo;
    set => this._DateTo = value;
  }

  /// <exclude />
  public abstract class wikiID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  WikiAnnouncementFilter.wikiID>
  {
  }

  /// <exclude />
  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WikiAnnouncementFilter.createdByID>
  {
  }

  /// <exclude />
  public abstract class dateFrom : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    WikiAnnouncementFilter.dateFrom>
  {
  }

  /// <exclude />
  public abstract class dateTo : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  WikiAnnouncementFilter.dateTo>
  {
  }
}
