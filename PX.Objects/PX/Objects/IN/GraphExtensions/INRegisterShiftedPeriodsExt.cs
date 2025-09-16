// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterShiftedPeriodsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public class INRegisterShiftedPeriodsExt : 
  ShiftedPeriodsExt<INRegisterEntryBase, INRegister, INRegister.tranDate, INRegister.tranPeriodID, INTran>
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Documents = new PXSelectExtension<Document>((PXSelectBase) this.Base.INRegisterDataMember);
    this.Lines = new PXSelectExtension<DocumentLine>((PXSelectBase) this.Base.INTranDataMember);
  }
}
