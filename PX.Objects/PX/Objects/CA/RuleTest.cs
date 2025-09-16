// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.RuleTest
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class RuleTest : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Test", Enabled = false)]
  public 
  #nullable disable
  string TestName { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Result", Enabled = false)]
  public bool? Result { get; set; }

  public abstract class result : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RuleTest.result>
  {
  }
}
