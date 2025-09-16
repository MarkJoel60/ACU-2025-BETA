// Decompiled with JetBrains decompiler
// Type: PX.CS.RMUnitSet
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.CS;

[PXCacheName("Unit Set")]
[PXPrimaryGraph(typeof (RMUnitSetMaint))]
public class RMUnitSet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IRMType
{
  protected 
  #nullable disable
  string _UnitSetCode;
  protected string _Description;
  protected string _Type;
  protected Guid? _NoteID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Code", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  [PXFieldDescription]
  [PXSelector(typeof (RMUnitSet.unitSetCode))]
  [PXReferentialIntegrityCheck]
  public virtual string UnitSetCode
  {
    get => this._UnitSetCode;
    set => this._UnitSetCode = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXFieldDescription]
  [PXDefault]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Type", Required = true, Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [PXNote]
  [PXSearchable(65535 /*0xFFFF*/, "Unit Set: {0}", new System.Type[] {typeof (RMUnitSet.unitSetCode)}, new System.Type[] {typeof (RMUnitSet.unitSetCode), typeof (RMUnitSet.type), typeof (RMUnitSet.description)}, Line1Format = "{0}", Line1Fields = new System.Type[] {typeof (RMUnitSet.description)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (RMUnitSet.type)})]
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
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

  public string RMType => this.Type;

  public class PK : PrimaryKeyOf<RMUnitSet>.By<RMUnitSet.unitSetCode>
  {
    public static RMUnitSet Find(PXGraph graph, string unitSetCode, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RMUnitSet>.By<RMUnitSet.unitSetCode>.FindBy(graph, (object) unitSetCode, options);
    }
  }

  public abstract class unitSetCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnitSet.unitSetCode>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnitSet.description>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMUnitSet.type>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnitSet.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnitSet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMUnitSet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMUnitSet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RMUnitSet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RMUnitSet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    RMUnitSet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RMUnitSet.Tstamp>
  {
  }
}
