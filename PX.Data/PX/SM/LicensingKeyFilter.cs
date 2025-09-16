// Decompiled with JetBrains decompiler
// Type: PX.SM.LicensingKeyFilter
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
public class LicensingKeyFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Please Enter License Key")]
  [PXDBString(20, IsFixed = true, InputMask = "AAAA-AAAA-AAAA-AAAA-AAAA")]
  public virtual 
  #nullable disable
  string LicensingKey { get; set; }

  public abstract class licensingKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LicensingKeyFilter.licensingKey>
  {
  }
}
