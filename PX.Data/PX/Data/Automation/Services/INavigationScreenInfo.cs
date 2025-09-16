// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.Services.INavigationScreenInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation.Services;

internal interface INavigationScreenInfo
{
  string ScreenId { get; }

  System.Type GraphType { get; }

  string Url { get; }

  string DataViewName { get; }

  bool IsReport { get; }

  bool IsInquiry { get; }

  bool IsFilteredInquiry { get; }
}
