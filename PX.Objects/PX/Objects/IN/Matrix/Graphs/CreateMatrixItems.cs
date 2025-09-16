// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Graphs.CreateMatrixItems
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.GraphExtensions;

#nullable disable
namespace PX.Objects.IN.Matrix.Graphs;

public class CreateMatrixItems : PXGraph<CreateMatrixItems, EntryHeader>
{
  public CreateMatrixItems()
  {
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.Insert).SetVisible(false);
    ((PXAction) this.Delete).SetVisible(false);
    ((PXAction) this.CopyPaste).SetVisible(false);
    ((PXAction) this.Next).SetVisible(false);
    ((PXAction) this.Previous).SetVisible(false);
    ((PXAction) this.First).SetVisible(false);
    ((PXAction) this.Last).SetVisible(false);
  }

  public virtual bool CanClipboardCopyPaste() => false;

  public class CreateMatrixItemsImpl : CreateMatrixItemsExt<CreateMatrixItems, EntryHeader>
  {
  }
}
