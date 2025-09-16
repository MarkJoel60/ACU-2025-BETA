// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.IStatementReader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public interface IStatementReader
{
  void Read(byte[] aInput);

  bool IsValidInput(byte[] aInput);

  bool AllowsMultipleAccounts();

  void ExportToNew<T>(FileInfo aFileInfo, T current, out List<CABankTranHeader> aExported) where T : CABankTransactionsImport, new();
}
