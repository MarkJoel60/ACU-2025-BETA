// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DataIntegrity.DataIntegrityLog
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.DataIntegrity;

[PXHidden]
[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2019 R2.")]
[Serializable]
public class DataIntegrityLog : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  public virtual int? LogEntryID { get; set; }

  [PXDBDateAndTime]
  public virtual DateTime? UtcTime { get; set; }

  [PXDBString(30)]
  public virtual 
  #nullable disable
  string InconsistencyCode { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string ExceptionMessage { get; set; }

  [PXDBText(IsUnicode = true)]
  public virtual string ContextInfo { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? UserID { get; set; }

  [PXDBInt]
  public virtual int? UserBranchID { get; set; }

  public abstract class logEntryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DataIntegrityLog.logEntryID>
  {
  }

  public abstract class utcTime : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DataIntegrityLog.utcTime>
  {
  }

  public abstract class inconsistencyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DataIntegrityLog.inconsistencyCode>
  {
  }

  public abstract class exceptionMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DataIntegrityLog.exceptionMessage>
  {
  }

  public abstract class contextInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DataIntegrityLog.contextInfo>
  {
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  DataIntegrityLog.userID>
  {
  }

  public abstract class userBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DataIntegrityLog.userBranchID>
  {
  }
}
