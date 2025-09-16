// Decompiled with JetBrains decompiler
// Type: PX.SM.SMScanJobParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Scan Job Parameters")]
[Serializable]
public class SMScanJobParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SMScanJob.scanJobID))]
  [PXParent(typeof (Select<SMScanJob, Where<SMScanJob.scanJobID, Equal<Current<SMScanJobParameter.scanJobID>>>>))]
  [PXUIField(DisplayName = "Job ID")]
  public virtual int? ScanJobID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SMScanJob.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "View Name")]
  public virtual 
  #nullable disable
  string ViewName { get; set; }

  [PXDBString(255 /*0xFF*/, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Parameter Name")]
  public virtual string ParameterName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Parameter Value")]
  public virtual string ParameterValue { get; set; }

  public abstract class scanJobID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJobParameter.scanJobID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SMScanJobParameter.lineNbr>
  {
  }

  public abstract class viewName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMScanJobParameter.viewName>
  {
  }

  public abstract class parameterName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJobParameter.parameterName>
  {
  }

  public abstract class parameterValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMScanJobParameter.parameterValue>
  {
  }
}
