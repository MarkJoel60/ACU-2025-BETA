// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeActiveRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPShiftCodeActiveRestrictorAttribute : PXRestrictorAttribute
{
  public EPShiftCodeActiveRestrictorAttribute()
    : base(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPShiftCode.isActive, Equal<True>>>>>.And<BqlOperand<EPShiftCodeRate.shiftID, IBqlInt>.IsNotNull>>), "The shift code is not active or doesn't have any effective rates.", Array.Empty<Type>())
  {
  }
}
