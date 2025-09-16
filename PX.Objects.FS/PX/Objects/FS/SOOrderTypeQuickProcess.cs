// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SOOrderTypeQuickProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXBreakInheritance]
[PXProjection(typeof (Select<PX.Objects.SO.SOOrderType>))]
[Serializable]
public class SOOrderTypeQuickProcess : PX.Objects.SO.SOOrderType
{
  public new abstract class orderType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeQuickProcess.orderType>
  {
  }

  public new abstract class behavior : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeQuickProcess.behavior>
  {
  }

  public new abstract class allowQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderTypeQuickProcess.allowQuickProcess>
  {
  }
}
