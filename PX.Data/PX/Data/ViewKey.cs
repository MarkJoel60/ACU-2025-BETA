// Decompiled with JetBrains decompiler
// Type: PX.Data.ViewKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[Serializable]
public class ViewKey
{
  public readonly System.Type SelectCommand;
  public readonly bool IsViewReadonly;

  public ViewKey(System.Type selectCommand, bool isViewReadonly)
  {
    this.SelectCommand = selectCommand;
    this.IsViewReadonly = isViewReadonly;
  }

  public override bool Equals(object obj)
  {
    return obj is ViewKey viewKey && this.SelectCommand == viewKey.SelectCommand && this.IsViewReadonly == viewKey.IsViewReadonly;
  }

  public override int GetHashCode()
  {
    return this.SelectCommand.GetHashCode() ^ this.IsViewReadonly.GetHashCode();
  }
}
