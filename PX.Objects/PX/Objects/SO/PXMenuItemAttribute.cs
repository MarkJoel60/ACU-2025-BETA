// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXMenuItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.SO;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PXMenuItemAttribute : Attribute
{
  public string Menu { get; }

  public int MenuItemID { get; }

  public PXMenuItemAttribute(string menu, int menuItemId)
  {
    this.Menu = menu;
    this.MenuItemID = menuItemId;
  }
}
