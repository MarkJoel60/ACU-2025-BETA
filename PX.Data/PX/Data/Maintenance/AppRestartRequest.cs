// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.AppRestartRequest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Maintenance;

[PXHidden]
internal class AppRestartRequest : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  public Guid? RequestId { get; set; }

  [PXDBDateAndTime]
  public System.DateTime? CreatedDateTime { get; set; }

  public abstract class requestId : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  AppRestartRequest.requestId>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYHistory.createdDateTime>
  {
  }
}
