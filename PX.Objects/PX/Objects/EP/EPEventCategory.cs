// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEventCategory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.EP;

[PXCacheName("Event Category")]
[Serializable]
public class EPEventCategory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CategoryID;
  protected 
  #nullable disable
  string _Description;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? CategoryID
  {
    get => this._CategoryID;
    set => this._CategoryID = value;
  }

  [PXDBString(50, InputMask = "", IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXUIField(DisplayName = "Style")]
  [PXDBString]
  [PXStringList("default,bad,good,neutral,red,red60,red40,red20,red0,orange,orange60,orange40,orange20,orange0,green,green60,green40,green20,green0,blue,blue60,blue40,blue20,blue0,yellow,yellow60,yellow40,yellow20,yellow0,purple,purple60,purple40,purple20,purple0", IsLocalizable = false)]
  public virtual string Style { get; set; }

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

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPEventCategory>.By<EPEventCategory.categoryID>
  {
    public static EPEventCategory Find(PXGraph graph, int? categoryID, PKFindOptions options = 0)
    {
      return (EPEventCategory) PrimaryKeyOf<EPEventCategory>.By<EPEventCategory.categoryID>.FindBy(graph, (object) categoryID, options);
    }
  }

  public abstract class categoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEventCategory.categoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEventCategory.description>
  {
  }

  public abstract class style : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEventCategory.style>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEventCategory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEventCategory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEventCategory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEventCategory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEventCategory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEventCategory.lastModifiedDateTime>
  {
  }
}
