// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.MatchedRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.PushNotifications;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BusinessProcess;

public class MatchedRow
{
  [JsonConverter(typeof (FieldNameRowDictionaryConverter))]
  public Dictionary<KeyWithAlias, object> OldRow { get; set; }

  [JsonConverter(typeof (FieldNameRowDictionaryConverter))]
  public Dictionary<KeyWithAlias, object> NewRow { get; set; }
}
