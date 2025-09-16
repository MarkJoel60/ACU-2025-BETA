// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.HandlerInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Update;

internal class HandlerInfo
{
  public const string VerbAll = "*";

  public string Name { get; set; }

  public string Path { get; set; }

  public string Verb { get; set; }

  public string Type { get; set; }

  public HandlerInfo(string name, string path)
    : this(name, path, "*")
  {
  }

  public HandlerInfo(string name, string path, string verb)
    : this(name, path, verb, (string) null)
  {
  }

  public HandlerInfo(string name, string path, string verb, string type)
  {
    this.Name = name;
    this.Path = path;
    this.Verb = verb;
    this.Type = type;
  }

  public override bool Equals(object obj)
  {
    return !(obj.GetType() != typeof (HandlerInfo)) && string.Equals(((HandlerInfo) obj).Name, this.Name);
  }

  public override int GetHashCode() => this.Name.GetHashCode();
}
