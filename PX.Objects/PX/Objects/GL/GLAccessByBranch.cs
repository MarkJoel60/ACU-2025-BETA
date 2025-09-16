// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAccessByBranch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

public class GLAccessByBranch : GLAccessDetail
{
  public PXSave<PX.Objects.GL.Branch> Save;
  public PXCancel<PX.Objects.GL.Branch> Cancel;
  public PXFirst<PX.Objects.GL.Branch> First;
  public PXPrevious<PX.Objects.GL.Branch> Prev;
  public PXNext<PX.Objects.GL.Branch> Next;
  public PXLast<PX.Objects.GL.Branch> Last;
}
