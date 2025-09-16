// Decompiled with JetBrains decompiler
// Type: PX.Data.SMLongOperation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

[PXHidden]
public class SMLongOperation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(128 /*0x80*/, IsKey = true)]
  public 
  #nullable disable
  string OperationKey { get; set; }

  [PXDBString(32 /*0x20*/)]
  public string ClusterName { get; set; }

  [PXDBString(64 /*0x40*/)]
  public string WebsiteID { get; set; }

  [PXDBString(16 /*0x10*/)]
  public string ScreenID { get; set; }

  public abstract class operationKey : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SMLongOperation.operationKey>
  {
  }

  public abstract class clusterName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMLongOperation.clusterName>
  {
  }

  public abstract class websiteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMLongOperation.websiteID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMLongOperation.screenID>
  {
  }
}
