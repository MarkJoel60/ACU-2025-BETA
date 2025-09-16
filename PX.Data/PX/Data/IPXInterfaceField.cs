// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXInterfaceField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public interface IPXInterfaceField
{
  string DisplayName { get; set; }

  PXUIVisibility Visibility { get; set; }

  bool Enabled { get; set; }

  bool Visible { get; set; }

  string ErrorText { get; set; }

  object ErrorValue { get; set; }

  PXErrorLevel ErrorLevel { get; set; }

  int TabOrder { get; set; }

  PXCacheRights MapEnableRights { get; set; }

  PXCacheRights MapViewRights { get; set; }

  bool ViewRights { get; }

  void ForceEnabled();
}
