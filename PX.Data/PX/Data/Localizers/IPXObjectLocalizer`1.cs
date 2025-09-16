// Decompiled with JetBrains decompiler
// Type: PX.Data.Localizers.IPXObjectLocalizer`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Localizers;

internal interface IPXObjectLocalizer<T> : IPXObjectLocalizer where T : class
{
  void Localize(T localizableObject);
}
