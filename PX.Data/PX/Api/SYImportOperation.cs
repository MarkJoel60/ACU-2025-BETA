// Decompiled with JetBrains decompiler
// Type: PX.Api.SYImportOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYImportOperation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Operation")]
  [ProcessingOperation.StringList]
  [PXDefault("C", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual 
  #nullable disable
  string Operation { get; set; }

  [PXDBGuid(false)]
  public Guid? MappingID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Break on Error")]
  public bool? BreakOnError { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Break on Incorrect Target")]
  public bool? BreakOnTarget { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Validate Data (No Saving)")]
  public bool? Validate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Save if Data Is Valid")]
  public bool? ValidateAndSave { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Skip Headers")]
  public bool? SkipHeaders { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Parallel Processing")]
  public bool? ProcessInParallel { get; set; }

  [PXInt]
  public int? BatchSize { get; set; }

  internal SYImportOperation Clone() => this.MemberwiseClone() as SYImportOperation;

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYImportOperation.operation>
  {
  }

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYImportOperation.mappingID>
  {
  }

  public abstract class breakOnError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYImportOperation.breakOnError>
  {
  }

  public abstract class breakOnTarget : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYImportOperation.breakOnTarget>
  {
  }

  public abstract class validate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYImportOperation.validate>
  {
  }

  public abstract class validateAndSave : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYImportOperation.validateAndSave>
  {
  }

  public abstract class skipHeaders : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYImportOperation.skipHeaders>
  {
  }

  public abstract class processInParallel : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYImportOperation.processInParallel>
  {
  }

  public abstract class batchSize : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYImportOperation.batchSize>
  {
  }
}
