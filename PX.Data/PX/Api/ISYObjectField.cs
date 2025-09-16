// Decompiled with JetBrains decompiler
// Type: PX.Api.ISYObjectField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

public interface ISYObjectField
{
  string ObjectName { get; set; }

  string ObjectNameHidden { get; set; }

  string FieldName { get; set; }

  string FieldNameHidden { get; set; }
}
