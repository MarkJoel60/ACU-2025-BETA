// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Helpers.ActionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Api.Helpers;

public static class ActionExtensions
{
  public static bool IsSave(this PX.Api.Models.Action action)
  {
    return action.Descriptor.ButtonType == PXSpecialButtonType.Save;
  }

  public static bool IsInsert(this PX.Api.Models.Action action)
  {
    return action.Descriptor.ButtonType == PXSpecialButtonType.Insert;
  }

  public static bool IsNavigation(this PX.Api.Models.Action action)
  {
    return action.Descriptor.ButtonType == PXSpecialButtonType.First || action.Descriptor.ButtonType == PXSpecialButtonType.Next || action.Descriptor.ButtonType == PXSpecialButtonType.Prev || action.Descriptor.ButtonType == PXSpecialButtonType.Last;
  }
}
