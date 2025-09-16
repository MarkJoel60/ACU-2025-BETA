// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageUpdatableProps
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class WikiPageUpdatableProps : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ParentUID;
  protected 
  #nullable disable
  string _Keywords;
  protected int? _Versioned;
  protected int? _Hold;
  protected int? _Approve;
  protected int? _Reject;
  protected int? _TagID;

  [PXDBGuid(false)]
  [PXUIField(DisplayName = "Parent Folder")]
  public virtual Guid? ParentUID
  {
    get => this._ParentUID;
    set => this._ParentUID = value;
  }

  [PXDBString(InputMask = "")]
  [PXUIField(DisplayName = "Keywords")]
  public virtual string Keywords
  {
    get => this._Keywords;
    set => this._Keywords = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Do Not Change", "Maintain Versions", "Do Not Maintain Versions"})]
  [PXUIField(DisplayName = "Versioned")]
  public virtual int? Versioned
  {
    get => this._Versioned;
    set => this._Versioned = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Do Not Change", "On Hold", "Published"})]
  [PXUIField(DisplayName = "Status")]
  public virtual int? Hold
  {
    get => this._Hold;
    set
    {
      this._Hold = value;
      int? nullable = value;
      int num = 0;
      if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
        return;
      this.Reject = new int?(0);
      this.Approve = new int?(0);
    }
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Do Not Change", "True", "False"})]
  [PXUIField(DisplayName = "Approve")]
  public virtual int? Approve
  {
    get => this._Approve;
    set
    {
      this._Approve = value;
      int? nullable = value;
      int num = 0;
      if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
        return;
      this.Hold = new int?(0);
      this.Reject = new int?(0);
    }
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Do Not Change", "True", "False"})]
  [PXUIField(DisplayName = "Reject")]
  public virtual int? Reject
  {
    get => this._Reject;
    set
    {
      this._Reject = value;
      int? nullable = value;
      int num = 0;
      if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
        return;
      this.Hold = new int?(0);
      this.Approve = new int?(0);
    }
  }

  [PXDBInt]
  [PXSelector(typeof (Search<WikiTag.tagID, Where<WikiTag.wikiID, Equal<Current<WikiPageStatusFilter.wikiID>>>>), SubstituteKey = typeof (WikiTag.description))]
  [PXUIField(DisplayName = "Tag")]
  public virtual int? TagID
  {
    get => this._TagID;
    set => this._TagID = value;
  }

  public abstract class parentUID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WikiPageUpdatableProps.parentUID>
  {
  }

  public abstract class keywords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WikiPageUpdatableProps.keywords>
  {
  }

  public abstract class versioned : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageUpdatableProps.versioned>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageUpdatableProps.hold>
  {
  }

  public abstract class approve : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageUpdatableProps.approve>
  {
  }

  public abstract class reject : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageUpdatableProps.reject>
  {
  }

  public abstract class tagID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WikiPageUpdatableProps.tagID>
  {
  }
}
