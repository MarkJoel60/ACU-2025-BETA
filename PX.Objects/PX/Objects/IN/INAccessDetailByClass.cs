// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAccessDetailByClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

public class INAccessDetailByClass : INAccessDetailItem
{
  public PXSave<INItemClass> Save;
  public PXCancel<INItemClass> Cancel;
  public PXFirst<INItemClass> First;
  public PXPrevious<INItemClass> Prev;
  public PXNext<INItemClass> Next;
  public PXLast<INItemClass> Last;
}
