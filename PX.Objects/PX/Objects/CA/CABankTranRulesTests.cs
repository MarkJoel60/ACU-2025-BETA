// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranRulesTests
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranRulesTests : CABankTransactionsMaint
{
  public PXSelect<RuleTest> Tests;
  public PXAction<RuleTest> Run;

  public virtual bool IsDirty => false;

  public IEnumerable tests()
  {
    if (!((PXSelectBase) this.Tests).Cache.IsInsertedUpdatedDeleted)
    {
      foreach (RuleTestDefinition testsDefinition in this.TestsDefinitions)
        ((PXSelectBase<RuleTest>) this.Tests).Insert(testsDefinition.TestInfo);
    }
    foreach (object obj in ((PXSelectBase) this.Tests).Cache.Inserted)
      yield return obj;
  }

  [PXUIField(DisplayName = "Run tests")]
  [PXButton]
  public void run()
  {
    foreach (RuleTest test in ((PXSelectBase) this.Tests).Cache.Inserted)
      this.RunTest(test);
  }

  private void RunTest(RuleTest test)
  {
    RuleTestDefinition ruleTestDefinition = this.TestsDefinitions.First<RuleTestDefinition>((Func<RuleTestDefinition, bool>) (td => td.TestInfo.TestName == test.TestName));
    bool flag = true;
    string str = (string) null;
    try
    {
      ((PXSelectBase) this.Rules).Cache.Insert((object) ruleTestDefinition.Rule);
      if (ruleTestDefinition.Matches != null)
      {
        foreach (CABankTran match in ruleTestDefinition.Matches)
        {
          if (!this.CheckRuleMatches(match, ruleTestDefinition.Rule))
          {
            flag = false;
            str += $"Did not match {match.TranID}\n";
          }
        }
      }
      if (ruleTestDefinition.NotMatches != null)
      {
        foreach (CABankTran notMatch in ruleTestDefinition.NotMatches)
        {
          if (this.CheckRuleMatches(notMatch, ruleTestDefinition.Rule))
          {
            flag = false;
            str += $"Matched {notMatch.TranID}\n";
          }
        }
      }
      ((PXSelectBase) this.Rules).Cache.Clear();
    }
    catch (Exception ex)
    {
      flag = false;
      str = $"Exception occured: {ex.GetType().Name}\n{ex.Message}";
    }
    test.Result = new bool?(flag);
    ((PXSelectBase) this.Tests).Cache.Update((object) test);
    if (flag)
      return;
    PXUIFieldAttribute.SetError<RuleTest.result>(((PXSelectBase) this.Tests).Cache, (object) test, str);
  }

  protected virtual IEnumerable<RuleTestDefinition> TestsDefinitions
  {
    get
    {
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Description only matches description in all positions"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "BANK FEE"
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            TranDesc = "wow such BANK FEE asda"
          },
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "BANK FEE asda"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow such BANK FEE"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranDesc = "BANK FEE"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Description does not match missing/partial/split"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "BANK FEE"
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            TranDesc = "wow such asda"
          },
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "BANK asda"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow such FEE"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranDesc = "BANKFEE"
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranDesc = "BA NKFEE"
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranDesc = "BANK 111 FEE"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Amount Equal matches exact transaction amount"
        },
        Rule = new CABankTranRule()
        {
          AmountMatchingMode = "E",
          CuryTranAmt = new Decimal?((Decimal) 150)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 150),
            TranDesc = "BANK FEE asda"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?(149.99M)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 400)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?(0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Amount Equal matches together with description"
        },
        Rule = new CABankTranRule()
        {
          AmountMatchingMode = "E",
          CuryTranAmt = new Decimal?((Decimal) 150),
          BankTranDescription = "BANK FEE"
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 150),
            TranDesc = "BANK FEE asda"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "BANK FEE asda"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?((Decimal) 151),
            TranDesc = "BANK FEE asda"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            CuryTranAmt = new Decimal?((Decimal) 150),
            TranDesc = "B@NK FEE asda"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Rule with currency matches only trans in the specified currency"
        },
        Rule = new CABankTranRule() { TranCuryID = "EUR" },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), CuryID = "EUR" },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryID = "EUR",
            CuryTranAmt = new Decimal?(100.0M),
            TranDesc = "Something"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran() { TranID = new int?(1), TranDesc = "EUR" },
          new CABankTran() { TranID = new int?(2), CuryID = "USD" },
          new CABankTran()
          {
            TranID = new int?(3),
            CuryTranAmt = new Decimal?((Decimal) 150),
            TranDesc = "B@NK FEE asda",
            CuryID = "GBP"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Rule with currency matches trans in the specified currency even if there are extra spaces in currency field"
        },
        Rule = new CABankTranRule() { TranCuryID = "EUR" },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), CuryID = "EUR  " },
          new CABankTran() { TranID = new int?(1), CuryID = "  EUR" },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryID = "  EUR   "
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(2), CuryID = " USD " }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Rule with CashAccount matches only trans on the specified cash account"
        },
        Rule = new CABankTranRule()
        {
          BankTranCashAccountID = new int?(2)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            CashAccountID = new int?(2)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CashAccountID = new int?(2),
            CuryID = "EUR",
            CuryTranAmt = new Decimal?(100.0M),
            TranDesc = "Something"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran() { TranID = new int?(1), TranDesc = "EUR" },
          new CABankTran()
          {
            TranID = new int?(2),
            CashAccountID = new int?(12),
            CuryID = "USD"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            CashAccountID = new int?(12),
            CuryTranAmt = new Decimal?((Decimal) 150),
            TranDesc = "B@NK FEE asda",
            CuryID = "GBP"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "CashAccount matching works together with Description and Amount matching"
        },
        Rule = new CABankTranRule()
        {
          BankTranCashAccountID = new int?(2),
          AmountMatchingMode = "E",
          CuryTranAmt = new Decimal?((Decimal) 20),
          BankTranDescription = "FEE"
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            CashAccountID = new int?(2),
            CuryTranAmt = new Decimal?(20.0M),
            TranDesc = "FEE"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CashAccountID = new int?(2),
            CuryTranAmt = new Decimal?(20.0M),
            TranDesc = "BANK FEE"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CashAccountID = new int?(2),
            CuryTranAmt = new Decimal?(20.0M),
            TranDesc = "BANK EE"
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CashAccountID = new int?(2),
            CuryTranAmt = new Decimal?(21.0M),
            TranDesc = "BANK FEE"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CashAccountID = new int?(1),
            CuryTranAmt = new Decimal?(20.0M),
            TranDesc = "BANK FEE"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Amount Between matches transactions with amount in the specified range (inclusive)"
        },
        Rule = new CABankTranRule()
        {
          AmountMatchingMode = "B",
          CuryTranAmt = new Decimal?((Decimal) 100),
          MaxCuryTranAmt = new Decimal?((Decimal) 200)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 100)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?((Decimal) 200)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?(99.99M)
          },
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?(200.01M)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 400)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?(0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Amount None ignores both Amount Criteria when matching"
        },
        Rule = new CABankTranRule()
        {
          AmountMatchingMode = "N",
          CuryTranAmt = new Decimal?((Decimal) 100),
          MaxCuryTranAmt = new Decimal?((Decimal) 200)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?((Decimal) 150)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 100)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?((Decimal) 200)
          },
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?(99.99M)
          },
          new CABankTran()
          {
            TranID = new int?(0),
            CuryTranAmt = new Decimal?(200.01M)
          },
          new CABankTran()
          {
            TranID = new int?(1),
            CuryTranAmt = new Decimal?((Decimal) 400)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            CuryTranAmt = new Decimal?(0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Description Criteria ignores case by default"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "BANK FEE"
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            TranDesc = "Bank fee"
          },
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "bank fee"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "BaNk FEE"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranDesc = "BANK FEE"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Description Criteria matches case if asked to"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "BANK FEE",
          MatchDescriptionCase = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(3),
            TranDesc = "BANK FEE"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(0),
            TranDesc = "Bank fee"
          },
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "bank fee"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "BaNk FEE"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Tran Code criterion matches only equal tran codes (case ignored)"
        },
        Rule = new CABankTranRule() { TranCode = "BANKT" },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "bankT"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "NOTBANK"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "SOME BANKT"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = " BANKT"
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT "
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "",
            TranDesc = "BANKT"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Tran Code criterion works well with Description and Amount criteria"
        },
        Rule = new CABankTranRule()
        {
          TranCode = "BANKT",
          BankTranDescription = "fee",
          AmountMatchingMode = "E",
          CuryTranAmt = new Decimal?(100.0M)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "my bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "bankT",
            TranDesc = "FEE",
            CuryTranAmt = new Decimal?(100.0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "NOTBANK",
            TranDesc = "my bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "SOME BANKT",
            TranDesc = "my bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "my bank",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "my bank fee",
            CuryTranAmt = new Decimal?(60.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranDesc = "my bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Properly handles the '?' wildcard in the Description criterion"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f?e",
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "foe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "FEE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "FOE",
            CuryTranAmt = new Decimal?(100.0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "fooe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "fe2e some",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = " few",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "F?EE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "BANKT",
            TranDesc = "bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranCode = "BANKT",
            TranDesc = "fee some",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(7),
            TranCode = "BANKT",
            TranDesc = " fee",
            CuryTranAmt = new Decimal?(100.0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Properly handles the '?' wildcard in the Description criterion with Match Case"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f?e",
          MatchDescriptionCase = new bool?(true),
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "foe",
            CuryTranAmt = new Decimal?(100.0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "fooe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "FEE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "FOE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "F?E",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "BANKT",
            TranDesc = "fee some",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranCode = "BANKT",
            TranDesc = " fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(7),
            TranCode = "BANKT",
            TranDesc = "bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Properly handles the '*' wildcard in the Description criterion"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f*e",
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "foe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "fooe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "FEE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "BANKT",
            TranDesc = "FE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranCode = "BANKT",
            TranDesc = "F-qew12E",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(7),
            TranCode = "BANKT",
            TranDesc = "FOE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(8),
            TranCode = "BANKT",
            TranDesc = "F?E",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(9),
            TranCode = "BANKT",
            TranDesc = "F*E",
            CuryTranAmt = new Decimal?(100.0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "bank 11ee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "bank f111ee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "ee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = " fff",
            CuryTranAmt = new Decimal?(100.0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Properly handles the '*' wildcard in the Description criterion with Match Case"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f*E",
          MatchDescriptionCase = new bool?(true),
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "feE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "foE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "fooE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = "feE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "BANKT",
            TranDesc = "fE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranCode = "BANKT",
            TranDesc = "f-qew12E",
            CuryTranAmt = new Decimal?(100.0M)
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranCode = "BANKT",
            TranDesc = "FEE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranCode = "BANKT",
            TranDesc = "bank fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranCode = "BANKT",
            TranDesc = "fee som",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(4),
            TranCode = "BANKT",
            TranDesc = " fee",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranCode = "BANKT",
            TranDesc = "fe",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranCode = "BANKT",
            TranDesc = "F-qew12e",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(7),
            TranCode = "BANKT",
            TranDesc = "FOE",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(8),
            TranCode = "BANKT",
            TranDesc = "F?E",
            CuryTranAmt = new Decimal?(100.0M)
          },
          new CABankTran()
          {
            TranID = new int?(9),
            TranCode = "BANKT",
            TranDesc = "F*E",
            CuryTranAmt = new Decimal?(100.0M)
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Won't apply wildcards if not explicitly asked to"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f*o"
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), TranDesc = "f*o" },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow f*o"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow F*o wow"
          }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), TranDesc = "foo" },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow foo"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow foo wow"
          }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Matches description in 'Equals' mode when UseDescriptionWildcards is on"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "f*o",
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), TranDesc = "foo" }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "wow f*o"
          },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow foo"
          },
          new CABankTran() { TranID = new int?(3), TranDesc = "f*o*" },
          new CABankTran()
          {
            TranID = new int?(4),
            TranDesc = "F*o wow"
          },
          new CABankTran()
          {
            TranID = new int?(5),
            TranDesc = " foo wow"
          },
          new CABankTran() { TranID = new int?(6), TranDesc = " foo" }
        }
      };
      yield return new RuleTestDefinition()
      {
        TestInfo = new RuleTest()
        {
          TestName = "Start wildcards can be used for 'Contains' mode when UseDescriptionWildcards is on"
        },
        Rule = new CABankTranRule()
        {
          BankTranDescription = "*f*o*",
          UseDescriptionWildcards = new bool?(true)
        },
        Matches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran() { TranID = new int?(1), TranDesc = "foo" },
          new CABankTran()
          {
            TranID = new int?(2),
            TranDesc = "wow f*o"
          },
          new CABankTran()
          {
            TranID = new int?(3),
            TranDesc = "wow foo"
          },
          new CABankTran() { TranID = new int?(4), TranDesc = "f*o*" },
          new CABankTran()
          {
            TranID = new int?(5),
            TranDesc = "F*o wow"
          },
          new CABankTran()
          {
            TranID = new int?(6),
            TranDesc = " foo wow"
          },
          new CABankTran() { TranID = new int?(7), TranDesc = " foo" }
        },
        NotMatches = (IEnumerable<CABankTran>) new List<CABankTran>()
        {
          new CABankTran()
          {
            TranID = new int?(1),
            TranDesc = "ooo fff"
          },
          new CABankTran() { TranID = new int?(2), TranDesc = "" }
        }
      };
    }
  }
}
