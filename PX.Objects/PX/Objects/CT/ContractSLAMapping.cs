// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractSLAMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXPrimaryGraph(typeof (ContractMaint))]
[PXCacheName("Contract SLA Mapping")]
[Serializable]
public class ContractSLAMapping : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ContractSLAMappingID;
  protected int? _ContractID;
  protected 
  #nullable disable
  string _Severity;
  protected int? _Period;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false)]
  public virtual int? ContractSLAMappingID
  {
    get => this._ContractSLAMappingID;
    set => this._ContractSLAMappingID = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (Contract.contractID))]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractSLAMapping.contractID>>>>))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Severity")]
  [PXStringList(new string[] {"H", "L", "M"}, new string[] {"High", "Low", "Medium"}, BqlField = typeof (CRCase.severity))]
  public virtual string Severity
  {
    get => this._Severity;
    set => this._Severity = value;
  }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Terms")]
  public virtual int? Period
  {
    get => this._Period;
    set => this._Period = value;
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
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<ContractSLAMapping>.By<ContractSLAMapping.contractSLAMappingID>
  {
    public static ContractSLAMapping Find(
      PXGraph graph,
      int? contractSLAMappingID,
      PKFindOptions options = 0)
    {
      return (ContractSLAMapping) PrimaryKeyOf<ContractSLAMapping>.By<ContractSLAMapping.contractSLAMappingID>.FindBy(graph, (object) contractSLAMappingID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractSLAMapping>.By<ContractSLAMapping.contractID>
    {
    }
  }

  public abstract class contractSLAMappingID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractSLAMapping.contractSLAMappingID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractSLAMapping.contractID>
  {
  }

  public abstract class severity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractSLAMapping.severity>
  {
  }

  public abstract class period : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractSLAMapping.period>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractSLAMapping.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractSLAMapping.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractSLAMapping.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractSLAMapping.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractSLAMapping.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractSLAMapping.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractSLAMapping.Tstamp>
  {
  }
}
