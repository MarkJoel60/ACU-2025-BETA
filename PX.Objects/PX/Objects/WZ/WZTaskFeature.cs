// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskFeature
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.WZ;

[PXHidden]
[Serializable]
public class WZTaskFeature : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _ScenarioID;
  protected Guid? _TaskID;
  protected bool? _Required;
  protected 
  #nullable disable
  string _Feature;
  protected string _DisplayName;
  protected int? _Order;
  protected int? _Offset;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBGuid(false, IsKey = true)]
  [PXParent(typeof (Select<WZScenario, Where<WZScenario.scenarioID, Equal<Current<WZTaskFeature.scenarioID>>>>))]
  public virtual Guid? ScenarioID
  {
    get => this._ScenarioID;
    set => this._ScenarioID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<WZTask, Where<WZTask.taskID, Equal<Current<WZTaskFeature.taskID>>>>))]
  public virtual Guid? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Required")]
  public virtual bool? Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  [PXDBString(50, IsUnicode = true, IsKey = true)]
  public virtual string Feature
  {
    get => this._Feature;
    set => this._Feature = value;
  }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [PXInt]
  public virtual int? Order
  {
    get => this._Order;
    set => this._Order = value;
  }

  [PXInt]
  public virtual int? Offset
  {
    get => this._Offset;
    set => this._Offset = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  public abstract class scenarioID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskFeature.scenarioID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskFeature.taskID>
  {
  }

  public abstract class required : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  WZTaskFeature.required>
  {
  }

  public abstract class feature : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTaskFeature.feature>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  WZTaskFeature.displayName>
  {
  }

  public abstract class order : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTaskFeature.order>
  {
  }

  public abstract class offset : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WZTaskFeature.offset>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WZTaskFeature.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WZTaskFeature.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTaskFeature.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTaskFeature.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WZTaskFeature.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WZTaskFeature.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WZTaskFeature.lastModifiedDateTime>
  {
  }
}
