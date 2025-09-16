// Decompiled with JetBrains decompiler
// Type: PX.SM.ScaleMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class ScaleMaint : PXGraph<ScaleMaint>
{
  public PXSave<SMScale> Save;
  public PXCancel<SMScale> Cancel;
  public PXSelect<SMScale> Scale;
}
