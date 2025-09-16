// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ARTranAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.CS;

[PXProjection(typeof (Select4<ARTran, Where<ARTran.released, NotEqual<True>, And<ARTran.deferredCode, IsNotNull>>, Aggregate<GroupBy<ARTran.refNbr, GroupBy<ARTran.tranType>>>>))]
[Serializable]
public class ARTranAlias : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARTran.tranType))]
  public virtual string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (ARTran.refNbr))]
  public virtual string RefNbr { get; set; }

  public abstract class tranType : IBqlField, IBqlOperand
  {
  }

  public abstract class refNbr : IBqlField, IBqlOperand
  {
  }
}
