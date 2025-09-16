// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSrvOrdTypeProblem
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSSrvOrdTypeProblem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXParent(typeof (Select<FSSrvOrdType, Where<FSSrvOrdType.srvOrdType, Equal<Current<FSSrvOrdTypeProblem.srvOrdType>>>>))]
  [PXDBDefault(typeof (FSSrvOrdType.srvOrdType))]
  public virtual 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Problem ID")]
  [PXSelector(typeof (Search<FSProblem.problemID>), SubstituteKey = typeof (FSProblem.problemCD))]
  public virtual int? ProblemID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.srvOrdType>
  {
  }

  public abstract class problemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSSrvOrdTypeProblem.problemID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSSrvOrdTypeProblem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSSrvOrdTypeProblem.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSSrvOrdTypeProblem.Tstamp>
  {
  }
}
