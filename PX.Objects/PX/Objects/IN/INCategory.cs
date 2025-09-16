// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Item Sales Category")]
[Serializable]
public class INCategory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CategoryID;
  protected 
  #nullable disable
  string _Description;
  protected int? _ParentID;
  protected int? _TempChildID;
  protected int? _TempParentID;
  protected Guid? _NoteID;
  protected int? _SortOrder;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  public virtual int? CategoryID
  {
    get => this._CategoryID;
    set => this._CategoryID = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Parent Category")]
  public virtual int? ParentID
  {
    get => this._ParentID;
    set => this._ParentID = new int?(value.GetValueOrDefault());
  }

  [PXInt]
  public virtual int? TempChildID
  {
    get => this._TempChildID;
    set => this._TempChildID = value;
  }

  [PXInt]
  public virtual int? TempParentID
  {
    get => this._TempParentID;
    set => this._TempParentID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDefault(0)]
  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<INCategory>.By<INCategory.categoryID>
  {
    public static INCategory Find(PXGraph graph, int? categoryID, PKFindOptions options = 0)
    {
      return (INCategory) PrimaryKeyOf<INCategory>.By<INCategory.categoryID>.FindBy(graph, (object) categoryID, options);
    }
  }

  public static class FK
  {
    public class Parent : 
      PrimaryKeyOf<INCategory>.By<INCategory.categoryID>.ForeignKeyOf<INCategory>.By<INCategory.parentID>
    {
    }
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCategory.categoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCategory.description>
  {
  }

  public abstract class parentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCategory.parentID>
  {
  }

  public abstract class tempChildID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCategory.tempChildID>
  {
  }

  public abstract class tempParentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCategory.tempParentID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCategory.noteID>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCategory.sortOrder>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCategory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCategory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCategory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCategory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCategory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCategory.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCategory.Tstamp>
  {
  }
}
