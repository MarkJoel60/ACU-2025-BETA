// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenDefinition
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
public class AUScreenDefinition : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ScreenID;
  protected Guid? _ProjectID;
  protected string _GraphName;
  protected string _ViewName;
  protected string _TableName;
  protected string _BqlTableName;
  protected string _TimeStampName;
  protected bool? _IsWorkflowEnabled;
  protected bool? _IsFlowIdentifierRequired;
  protected string _FlowIdentifier;
  protected string _StateIdentifier;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string ScreenID
  {
    get => this._ScreenID;
    set => this._ScreenID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField(DisplayName = "Project")]
  [PXDefault]
  public virtual Guid? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string GraphName
  {
    get => this._GraphName;
    set => this._GraphName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string ViewName
  {
    get => this._ViewName;
    set => this._ViewName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXDefault]
  public virtual string BqlTableName
  {
    get => this._BqlTableName;
    set => this._BqlTableName = value;
  }

  [PXDBString(128 /*0x80*/)]
  public virtual string TimeStampName
  {
    get => this._TimeStampName;
    set => this._TimeStampName = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Workflow Enabled")]
  public virtual bool? IsWorkflowEnabled
  {
    get => this._IsWorkflowEnabled;
    set => this._IsWorkflowEnabled = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Flow Identifier Required")]
  public virtual bool? IsFlowIdentifierRequired
  {
    get => this._IsFlowIdentifierRequired;
    set => this._IsFlowIdentifierRequired = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Flow Identifier")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FlowIdentifier
  {
    get => this._FlowIdentifier;
    set => this._FlowIdentifier = value;
  }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "State Identifier")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string StateIdentifier
  {
    get => this._StateIdentifier;
    set => this._StateIdentifier = value;
  }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
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
  [PXUIField(DisplayName = "Last Modified By")]
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
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenDefinition.screenID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenDefinition.projectID>
  {
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenDefinition.graphName>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenDefinition.viewName>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUScreenDefinition.tableName>
  {
  }

  public abstract class bqlTableName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.bqlTableName>
  {
  }

  public abstract class timeStampName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.timeStampName>
  {
  }

  public abstract class isWorkflowEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenDefinition.isWorkflowEnabled>
  {
  }

  public abstract class isFlowIdentifierRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenDefinition.isFlowIdentifierRequired>
  {
  }

  public abstract class flowIdentifier : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.flowIdentifier>
  {
  }

  public abstract class stateIdentifier : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.stateIdentifier>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUScreenDefinition.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenDefinition.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUScreenDefinition.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUScreenDefinition.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUScreenDefinition.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUScreenDefinition.tStamp>
  {
  }
}
