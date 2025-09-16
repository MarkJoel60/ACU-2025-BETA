// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.DunningLetterMassProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
[PXHidden]
public class DunningLetterMassProcess : PXGraph<DunningLetterMassProcess>
{
  [PXViewName("DunningLetter")]
  public PXSelect<ARDunningLetter> docs;
  [PXViewName("DunningLetterDetail")]
  public PXSelect<ARDunningLetterDetail, Where<ARDunningLetterDetail.dunningLetterID, Equal<Required<ARDunningLetter.dunningLetterID>>>> docsDet;
}
