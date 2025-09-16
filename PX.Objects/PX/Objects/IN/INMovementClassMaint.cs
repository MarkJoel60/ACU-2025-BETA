// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INMovementClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

public class INMovementClassMaint : PXGraph<INMovementClassMaint>
{
  public PXSelect<INMovementClass> MovementClasses;
  public PXSavePerRow<INMovementClass> Save;
  public PXCancel<INMovementClass> Cancel;

  protected virtual void INMovementClass_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
  }

  public virtual void INMovementClass_CountsPerYear_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue != null && ((short) e.NewValue < (short) 0 || (short) e.NewValue > (short) 365))
      throw new PXSetPropertyException("This value should be between {0} and {1}", (PXErrorLevel) 4, new object[2]
      {
        (object) 0,
        (object) 365
      });
  }
}
