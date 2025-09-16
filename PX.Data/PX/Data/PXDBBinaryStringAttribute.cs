// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBinaryStringAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBBinaryStringAttribute : PXDBBinaryAttribute
{
  protected override object DeserializeValue(byte[] bytes)
  {
    return (object) Encoding.UTF8.GetString(bytes);
  }

  protected override byte[] SerializeValue(object value)
  {
    return ((string) value).With<string, byte[]>(new Func<string, byte[]>(Encoding.UTF8.GetBytes));
  }
}
