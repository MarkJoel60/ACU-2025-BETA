// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCSyncStatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public static class CCSyncStatusCode
{
  public const 
  #nullable disable
  string None = "N";
  public const string Error = "E";
  public const string Warning = "W";
  public const string Success = "S";
  private static (SyncStatus, string)[] mapping = new (SyncStatus, string)[4]
  {
    (SyncStatus.None, "N"),
    (SyncStatus.Error, "E"),
    (SyncStatus.Warning, "W"),
    (SyncStatus.Success, "S")
  };

  public static Tuple<string, string>[] ValueLabelPairs()
  {
    return new Tuple<string, string>[4]
    {
      new Tuple<string, string>("N", "None"),
      new Tuple<string, string>("E", "Error"),
      new Tuple<string, string>("W", "Warning"),
      new Tuple<string, string>("S", "Success")
    };
  }

  public static SyncStatus GetSyncStatusBySyncStatusStr(string syncStatusCode)
  {
    if (!((IEnumerable<(SyncStatus, string)>) CCSyncStatusCode.mapping).Where<(SyncStatus, string)>((Func<(SyncStatus, string), bool>) (i => i.Item2 == syncStatusCode)).Any<(SyncStatus, string)>())
      throw new PXInvalidOperationException();
    return ((IEnumerable<(SyncStatus, string)>) CCSyncStatusCode.mapping).Where<(SyncStatus, string)>((Func<(SyncStatus, string), bool>) (i => i.Item2 == syncStatusCode)).Select<(SyncStatus, string), SyncStatus>((Func<(SyncStatus, string), SyncStatus>) (i => i.Item1)).First<SyncStatus>();
  }

  public static string GetSyncStatusStrBySyncStatus(SyncStatus syncStatus)
  {
    return ((IEnumerable<(SyncStatus, string)>) CCSyncStatusCode.mapping).Where<(SyncStatus, string)>((Func<(SyncStatus, string), bool>) (i => i.Item1 == syncStatus)).Select<(SyncStatus, string), string>((Func<(SyncStatus, string), string>) (i => i.Item2)).First<string>();
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CCSyncStatusCode.ValueLabelPairs())
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCSyncStatusCode.error>
  {
    public error()
      : base("E")
    {
    }
  }
}
