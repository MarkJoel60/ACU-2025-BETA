// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPrintJobParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Print Job Parameter")]
[Serializable]
public class SMPrintJobParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SMPrintJob.jobID))]
  [PXParent(typeof (Select<SMPrintJob, Where<SMPrintJob.jobID, Equal<Current<SMPrintJobParameter.jobID>>>>))]
  [PXUIField(DisplayName = "Job ID")]
  public virtual int? JobID { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Parameter Name")]
  public virtual 
  #nullable disable
  string ParameterName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Parameter Value")]
  public virtual string ParameterValue { get; set; }

  public abstract class jobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMPrintJobParameter.jobID>
  {
  }

  public abstract class parameterName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrintJobParameter.parameterName>
  {
  }

  public abstract class parameterValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMPrintJobParameter.parameterValue>
  {
  }
}
