// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PXDataUtils
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public class PXDataUtils
{
  public static string FieldName<Field>() where Field : IBqlField
  {
    string name = typeof (Field).Name;
    return !string.IsNullOrEmpty(name) ? char.ToUpper(name[0]).ToString() + name.Substring(1) : name;
  }
}
