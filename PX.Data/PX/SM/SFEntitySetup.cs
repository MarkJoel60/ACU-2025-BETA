// Decompiled with JetBrains decompiler
// Type: PX.SM.SFEntitySetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

/// <summary>Dummy DAC for PX.Salesforce.SFEntitySetup</summary>
[PXHidden]
[Serializable]
public class SFEntitySetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  public virtual int? EntityType { get; set; }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  public virtual 
  #nullable disable
  string ImportScenario { get; set; }

  [PXString(128 /*0x80*/, IsUnicode = true)]
  public virtual string ExportScenario { get; set; }

  public abstract class entityType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SFEntitySetup.entityType>
  {
  }

  public abstract class importScenario : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SFEntitySetup.importScenario>
  {
  }

  public abstract class exportScenario : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SFEntitySetup.exportScenario>
  {
  }
}
