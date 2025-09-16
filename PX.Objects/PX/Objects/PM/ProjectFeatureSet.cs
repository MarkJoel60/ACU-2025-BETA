// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectFeatureSet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

[Flags]
public enum ProjectFeatureSet : ushort
{
  None = 0,
  ProjectAccounting = 1,
  ChangeOrders = 2,
  ChangeRequests = 6,
  Construction = 8,
  ProjectManagement = 24, // 0x0018
  Inventory = 32, // 0x0020
  ProjectSpecificInventory = 96, // 0x0060
  Manufacturing = 128, // 0x0080
  Disabled = 65535, // 0xFFFF
}
