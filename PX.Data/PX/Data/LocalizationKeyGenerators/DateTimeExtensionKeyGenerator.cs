// Decompiled with JetBrains decompiler
// Type: PX.Data.LocalizationKeyGenerators.DateTimeExtensionKeyGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.LocalizationKeyGenerators;

/// <exclude />
internal class DateTimeExtensionKeyGenerator : DateTimeBaseKeyGenerator
{
  public DateTimeExtensionKeyGenerator(System.Type extension)
  {
    if (extension == (System.Type) null)
      throw new PXArgumentException(nameof (extension));
    this.DateKey = extension.FullName + "_Date";
    this.TimeKey = extension.FullName + "_Time";
  }
}
