// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Maximum`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public class Maximum<Expr1, Expr2> : IIf<Where<Expr1, Less<Expr2>>, Expr2, Expr1>
  where Expr1 : IBqlOperand
  where Expr2 : IBqlOperand
{
}
