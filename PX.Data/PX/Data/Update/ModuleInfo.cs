// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ModuleInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

internal class ModuleInfo
{
  public string Name { get; set; }

  public string Type { get; set; }

  public ModuleInfo(string name, string type)
  {
    this.Name = name;
    this.Type = type;
  }

  public override bool Equals(object obj)
  {
    return !(obj.GetType() != typeof (ModuleInfo)) && string.Equals(((ModuleInfo) obj).Name, this.Name);
  }

  public override int GetHashCode() => base.GetHashCode();
}
