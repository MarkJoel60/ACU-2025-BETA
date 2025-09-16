// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBOperationExt
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class PXDBOperationExt
{
  public static PXDBOperation Command(this PXDBOperation self) => self & PXDBOperation.Delete;

  public static PXDBOperation Option(this PXDBOperation self) => self & PXDBOperation.Option;

  public static PXDBOperation Place(this PXDBOperation self) => self & PXDBOperation.Place;
}
