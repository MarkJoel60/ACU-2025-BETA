// Decompiled with JetBrains decompiler
// Type: PX.CS.RMUnit
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Unit")]
public class RMUnit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _UnitSetCode;
  protected string _UnitCode;
  protected string _ParentCode;
  protected string _Description;
  protected string _Formula;
  protected string _GroupID;
  protected int? _DataSourceID;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Unit Set")]
  [PXDefault(typeof (RMUnitSet.unitSetCode))]
  [PXParent(typeof (Select<RMUnitSet, Where<RMUnitSet.unitSetCode, Equal<Current<RMUnit.unitSetCode>>>>))]
  public virtual string UnitSetCode
  {
    get => this._UnitSetCode;
    set => this._UnitSetCode = value;
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Code", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  public virtual string UnitCode
  {
    get => this._UnitCode;
    set => this._UnitCode = value;
  }

  /// <summary>Display field that combines UnitCode and Description.</summary>
  [PXString(IsUnicode = true)]
  [PXUIField(DisplayName = "", Visibility = PXUIVisibility.Invisible, Visible = true)]
  [PXDependsOnFields(new System.Type[] {typeof (RMUnit.unitCode), typeof (RMUnit.description)})]
  public virtual string CodeAndDescription => $"{this.UnitCode} - {this.Description}";

  [PXParent(typeof (Select<RMUnitSet, Where<RMUnitSet.unitSetCode, Equal<Current<RMUnit.parentCode>>>>))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(Visible = false)]
  public virtual string ParentCode
  {
    get => this._ParentCode;
    set => this._ParentCode = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBLocalizableString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Formula
  {
    get => this._Formula;
    set => this._Formula = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Printing Group")]
  public virtual string GroupID
  {
    get => this._GroupID;
    set => this._GroupID = value;
  }

  [RMDataSource]
  [PXForeignReference(typeof (RMUnit.FK.DataSource))]
  public virtual int? DataSourceID
  {
    get => this._DataSourceID;
    set => this._DataSourceID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<RMUnit>.By<RMUnit.unitSetCode, RMUnit.unitCode>
  {
    public static RMUnit Find(
      PXGraph graph,
      string unitSetCode,
      string unitCode,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMUnit>.By<RMUnit.unitSetCode, RMUnit.unitCode>.FindBy(graph, (object) unitSetCode, (object) unitCode, options);
    }
  }

  public static class FK
  {
    public class UnitSet : 
      PrimaryKeyOf<RMUnitSet>.By<RMUnitSet.unitSetCode>.ForeignKeyOf<RMUnit>.By<RMUnit.unitSetCode>
    {
    }

    public class ParentUnitSet : 
      PrimaryKeyOf<RMUnitSet>.By<RMUnitSet.unitSetCode>.ForeignKeyOf<RMUnit>.By<RMUnit.parentCode>
    {
    }

    public class DataSource : 
      PrimaryKeyOf<RMDataSource>.By<RMDataSource.dataSourceID>.ForeignKeyOf<RMUnit>.By<RMUnit.dataSourceID>
    {
    }
  }

  public abstract class unitSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.unitSetCode>
  {
  }

  public abstract class unitCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.unitCode>
  {
  }

  public abstract class parentCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.parentCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.description>
  {
  }

  public abstract class formula : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.formula>
  {
  }

  public abstract class groupID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnit.groupID>
  {
  }

  public abstract class dataSourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RMUnit.dataSourceID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnit.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMUnit.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMUnit.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMUnit.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMUnit.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMUnit.Tstamp>
  {
  }
}
