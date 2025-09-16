// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Tools.Dumper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Text;

#nullable disable
namespace PX.Objects.Common.Tools;

public class Dumper
{
  public static string Dump(params object[] items)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (object obj in items)
    {
      stringBuilder.Append(obj.Dump());
      stringBuilder.AppendLine();
    }
    return stringBuilder.ToString();
  }
}
